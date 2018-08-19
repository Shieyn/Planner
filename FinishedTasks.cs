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
    public partial class FinishedTasks : Form
    {
        public FinishedTasks()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FinishedTasks_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        void UpdateList()
        {
            listBox1.Items.Clear();
            foreach (Event e in MainForm.Finished.Plan)
            {
                listBox1.Items.Add(e);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var i = (Event)listBox1.SelectedItem;
                MainForm.Finished.Plan.Remove(i);
                MainForm.MainSave.Plan.Add(i);
                Library.SaveFile(MainForm.MainSave);
                Library.SaveFile(MainForm.Finished, Library.FinishedPath);
                UpdateList();
            }
        }
    }
}
