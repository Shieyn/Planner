using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Planner
{
    public partial class MainForm : Form
    {
        public static Save MainSave;
        public static Save Finished;

        bool explain;
        
        public MainForm()
        {
            InitializeComponent();
            monthCalendar1.MaxSelectionCount = 1;

            Activated += MainForm_GotFocus;
            FormClosing += MainForm_FormClosing;

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Library.SaveFile(MainSave);
            e.Cancel = true;
            Hide();

            if (!explain)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000, "I'm down here!", "To fully close the program, use the file dropdown", ToolTipIcon.Info);
                explain = true;
            }
        }

        private void MainForm_GotFocus(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Library.MainSavePath))
            {
                var s = new Save();
                Library.SaveFile(s);
            }

            if (!File.Exists(Library.FinishedPath))
            {
                var s = new Save();
                Library.SaveFile(s, Library.FinishedPath);
            }

            MainSave = Library.LoadFile();
            Finished = Library.LoadFile(Library.FinishedPath);

            UpdateList();
        }

        public void UpdateList(string search = null)
        {
            date.Text = $"Tasks for {monthCalendar1.SelectionRange.Start.ToLongDateString()}";

            Items.Items.Clear();
            List <Event> a;

            Predicate<Event> p;

            if (string.IsNullOrEmpty(search))
                p = s => s.Date == monthCalendar1.SelectionRange.Start;
            else
                p = s => s.Date == monthCalendar1.SelectionRange.Start && s.Keyword.Equals (search, StringComparison.InvariantCultureIgnoreCase);

            a = MainSave.Plan.FindAll(p);

            a.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            a.Reverse();

            foreach (Event e in a)
            {
                Items.Items.Add(e);
            }

            countText.Text = a.Count == 1 ? $"(1 result found)" : $"({a.Count} results found)"; //edge: one million
        }

        private void New_Click(object sender, EventArgs e)
        {
            NewEvent n = new NewEvent(new Event (monthCalendar1.SelectionRange.Start));
            n.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            UpdateList();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            NewEvent n = new NewEvent((Event)Items.SelectedItem);
            n.Show();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            MainSave.Plan.Remove((Event)Items.SelectedItem);
            Library.SaveFile(MainSave);
            UpdateList();
        }

        private void Push_Click(object sender, EventArgs e)
        {
            Event i = Items.SelectedItem as Event;
            MainSave.Plan.Remove(i);
            i.Date = i.Date.AddDays(1);
            MainSave.Plan.Add(i);

            UpdateList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateList(textBox1.Text);
        }

        private void Done_Click(object sender, EventArgs e)
        {
            done();
        }

        private void done()
        {
            var i = (Event)Items.SelectedItem;
            Finished.Plan.Add(i);
            MainSave.Plan.Remove(i);
            Library.SaveFile(MainSave);
            Library.SaveFile(Finished, Library.FinishedPath);
            UpdateList();
        }

        private void viewFinishedTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishedTasks f = new FinishedTasks();
            f.Show();
        }

        private void Items_DoubleClick(object sender, EventArgs e)
        {
            if (Items.SelectedItem != null)
            {
                done();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Items.SelectedItem != null)
            {
                done();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actualExit();
        }

        private void actualExit()
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Library.SaveFile(MainSave);
                Environment.Exit(0);
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pronk k = new pronk();
            k.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void newtask_Click(object sender, EventArgs e)
        {
            NewEvent n = new NewEvent(new Event(DateTime.Today));
            n.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            actualExit();
        }

        private void open_Click(object sender, EventArgs e)
        {
            Show();
        }
    }
}
