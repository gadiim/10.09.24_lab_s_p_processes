using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10._09._24_system_programming_processes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            process1.StartInfo = new System.Diagnostics.ProcessStartInfo("notepad.exe");
            listView1.View = View.Details;
            listView1.Columns.Add("id", 150);
            listView1.Columns.Add("Process", 500);
            listView1.FullRowSelect = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            process1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            process1.CloseMainWindow();
            process1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                ListViewItem listViewItem = new ListViewItem(process.Id.ToString());
                listViewItem.SubItems.Add(process.ProcessName);
                listView1.Items.Add(listViewItem);
            }
        }

        private void process1_Exited(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                int id = int.Parse(listView1.SelectedItems[0].Text);
                Process process = Process.GetProcessById(id);
                MessageBox.Show($"{process.Threads.Count} threads");
            }
        }

    }
}
