using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planner
{
    public partial class NewEvent : Form
    {
        Event selected;

        public NewEvent(Event e)
        {
            InitializeComponent();
            selected = e;
            Text = $"Create event @{selected.Date.ToLongDateString()}";
            StartPosition = FormStartPosition.CenterParent;
        }

        private void NewEvent_Load(object sender, EventArgs e)
        {
            title.Text = selected.Title;
            keyword.Text = selected.Keyword;
            priority.Value = selected.Priority;
            dateTimePicker1.Value = selected.Date;

            setfinish();
        }

        //Finish
        private void button1_Click(object sender, EventArgs e)
        {
            Finish();
        }

        void Finish()
        {
            MainForm.MainSave.Plan.Remove(selected);

            MainForm.MainSave.Plan.Add(new Event(
                dateTimePicker1.Value, title.Text, keyword.Text, priority.Value
            ));

            Library.SaveFile(MainForm.MainSave);
            Close();
        }

        private void title_TextChanged(object sender, EventArgs e)
        {
            setfinish();
        }

        void setfinish() => finishb.Enabled = !string.IsNullOrWhiteSpace(title.Text);

        private void NewEvent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Finish();
            }
        }
    }
}
