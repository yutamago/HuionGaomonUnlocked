using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HuionTablet.Forms.profileSwitcher
{
    public partial class FormSelectRunningProcess : Form
    {
        private readonly Timer _checkProcessesTimer = new Timer();
        public Process SelectedProcess;

        public FormSelectRunningProcess()
        {
            InitializeComponent();
            CheckProcesses(null, null);
            listView1.ListViewItemSorter = new Sorter();
            //_checkProcessesTimer.Interval = 500;
            //_checkProcessesTimer.Tick += CheckProcesses;
            //_checkProcessesTimer.Start();
        }

        private void CheckProcesses(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                try
                {
                    ListViewItem processItem = new ListViewItem(new[]
                    {
                        process.Id.ToString(), Path.GetFileName(process.MainModule.FileName),
                        process.MainModule.FileName
                    });
                    listView1.Items.Add(processItem);
                }
                catch (Win32Exception)
                {
                }
            }

            listView1.Invalidate(true);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedProcess = listView1.SelectedItems.Count == 0
                ? null
                : Process.GetProcessById(Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckProcesses(null, null);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sorter sorter = (Sorter) listView1.ListViewItemSorter;
            sorter.Column = e.Column;

            if (sorter.Order == SortOrder.Ascending)
            {
                sorter.Order = SortOrder.Descending;
            }
            else
            {
                sorter.Order = SortOrder.Ascending;
            }
            listView1.Sort();
        }
    }

    class Sorter : IComparer
    {
        public int Column;
        public SortOrder Order = SortOrder.Ascending;
        public int Compare(object x, object y) // IComparer Member
        {
            if (!(x is ListViewItem))
                return (0);
            if (!(y is ListViewItem))
                return (0);

            ListViewItem l1 = (ListViewItem)x;
            ListViewItem l2 = (ListViewItem)y;

            if (l1.ListView.Columns[Column].Tag == null)
            {
                l1.ListView.Columns[Column].Tag = "Text";
            }

            if (l1.ListView.Columns[Column].Tag.ToString() == "Numeric")
            {
                float fl1 = float.Parse(l1.SubItems[Column].Text);
                float fl2 = float.Parse(l2.SubItems[Column].Text);

                if (Order == SortOrder.Ascending)
                {
                    return fl1.CompareTo(fl2);
                }

                return fl2.CompareTo(fl1);
            }

            string str1 = l1.SubItems[Column].Text;
            string str2 = l2.SubItems[Column].Text;

            if (Order == SortOrder.Ascending)
            {
                return str1.CompareTo(str2);
            }

            return str2.CompareTo(str1);
        }
    }
}