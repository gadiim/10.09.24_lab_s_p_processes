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
            // This line calls the base class (Form) constructor to initialize the form's components.
            InitializeComponent();

            // This line sets the view of the 'listView1' control to 'Details', which displays columns for process information.
            listView1.View = View.Details;

            // This line adds two columns to the 'listView1' control:
            // - "id" with a width of 150 pixels
            // - "Process" with a width of 500 pixels
            listView1.Columns.Add("id", 150);
            listView1.Columns.Add("Process", 300);
            // This line enables full row selection in the 'listView1' control.
            listView1.FullRowSelect = true;

            // Initialize the ListView for threads
            listViewThreads.View = View.Details;
            listViewThreads.Columns.Add("Thread ID", 150);
            listViewThreads.Columns.Add("Priority", 150);
            listViewThreads.Columns.Add("Start Time", 300);
            listViewThreads.FullRowSelect = true;

            // Initialize the ListView for modules
            listViewModules.View = View.Details;
            listViewModules.Columns.Add("Module Name", 300);
            listViewModules.Columns.Add("Module Path", 500);
            listViewModules.FullRowSelect = true;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // This event handler is triggered when the button with the name 'button3' is clicked.
            // This line clears all items from the 'listView1' control.
            listView1.Items.Clear();

                // This line retrieves a list of all currently running processes.
                Process[] processes = Process.GetProcesses();

                // This loop iterates through each process in the 'processes' array.
                foreach (Process process in processes)
                {
                    // This line creates a new ListViewItem object with the process ID as the first subitem.
                    ListViewItem listViewItem = new ListViewItem(process.Id.ToString());

                    // This line adds the process name as the second subitem to the 'listViewItem'.
                    listViewItem.SubItems.Add(process.ProcessName);

                    // This line adds the newly created 'listViewItem' to the 'listView1' control.
                    listView1.Items.Add(listViewItem);
                }
            
        }

        private void process1_Exited(object sender, EventArgs e)
        {
            // This event handler is triggered when the process managed by 'process1' exits (finishes execution).
            // You can add logic here to handle the process termination, such as:
            // - Displaying a message indicating the process has exited
            // - Performing cleanup tasks related to the process
            // - Starting another process automatically (if desired)

            // In this example, the code currently doesn't perform any specific actions within this event handler.
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This event handler is triggered whenever the selection in 'listView1' changes.
            // It checks if there's at least one selected item in the list view.

            if (listView1.SelectedItems.Count > 0)
            {
                // If there's a selection, this line extracts the process ID from the text of the first selected item (assuming the ID is displayed in the first column).
                int id = int.Parse(listView1.SelectedItems[0].Text);

                try
                {
                    // This line retrieves the Process object representing the selected process by its ID.
                    Process process = Process.GetProcessById(id);

                    // This line retrieves the number of threads associated with the selected process and displays it in a message box.
                    // MessageBox.Show($"{process.Threads.Count} threads");

                    // Clear previous thread information
                    listViewThreads.Items.Clear();
                    // Clear previous module information
                    listViewModules.Items.Clear();

                    // Iterate through each thread in the selected process
                    foreach (ProcessThread thread in process.Threads)
                    {
                        try
                        {
                            ListViewItem item = new ListViewItem(thread.Id.ToString());
                            item.SubItems.Add(thread.PriorityLevel.ToString());
                            item.SubItems.Add(thread.StartTime.ToString());
                            listViewThreads.Items.Add(item);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Thread information:\n{ex.Message}");
                        }
                    }
                    // Iterate through each modules in the selected process
                    foreach (ProcessModule module in process.Modules)
                    {
                        try
                        {
                            ListViewItem item = new ListViewItem(module.ModuleName);
                            item.SubItems.Add(module.FileName);
                            listViewModules.Items.Add(item);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не вдалося отримати доступ до модуля: {ex.Message}");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Process information:\n{ex.Message}");
                }
            }
        }

        private void listViewThreads_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void listViewModules_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
