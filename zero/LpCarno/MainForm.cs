using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LxTools;
using LxTools.Liquipedia;
using LxTools.CarnoZ;

namespace LpCarno
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists("carno.dict"))
                return;

            using (var sr = new StreamReader("carno.dict"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(s) || s.StartsWith(";"))
                        continue;

                    if (s.StartsWith("$"))
                    {
                        int equals = s.IndexOf('=');
                        bool value = (s.Substring(equals + 1).Trim() == "1");
                        switch (s.Substring(1, equals - 1).Trim())
                        {
                            case "team": chkIncludeTeam.Checked = value; break;
                            case "allkills": chkIncludeAllKills.Checked = value; break;
                            case "ace": chkIncludeAce.Checked = value; break;
                        }
                    }

                    if (s.StartsWith("Source"))
                    {
                        int equals = s.IndexOf('=');
                        string param = s.Substring(equals + 1).Trim();

                        NewSource(param.Substring(2), (param[0] == '1'));
                    }
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var sw = new StreamWriter("carno.dict"))
            {
                sw.WriteLine("$team={0}", chkIncludeTeam.Checked ? "1" : "0");
                sw.WriteLine("$allkills={0}", chkIncludeAllKills.Checked ? "1" : "0");
                sw.WriteLine("$ace={0}", chkIncludeAce.Checked ? "1" : "0");

                foreach (ListViewItem item in lvwList.Items)
                {
                    sw.WriteLine("Source={0},{1}", item.Checked ? "1" : "0", item.Text);
                }
            }
        }

        private void chkIncludeTeam_CheckedChanged(object sender, EventArgs e)
        {
            chkIncludeAce.Enabled = chkIncludeTeam.Checked;
            chkIncludeAce.Enabled = chkIncludeTeam.Checked;
        }

        #region Source Management

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string value = null;
            if (UI.InputBox("Add Source", "Please enter the page title.", ref value) == DialogResult.OK)
            {
                NewSource(value, true);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwList.SelectedItems)
                lvwList.Items.Remove(item);
        }
        
        private void lvwList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.ForeColor = e.Item.Checked ? lvwList.ForeColor : SystemColors.ControlDark;
        }
        private void lvwList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = e.Data.GetData(DataFormats.Text).ToString();
                if (LiquipediaClient.IsValidLiquipediaLink(text))
                    e.Effect = DragDropEffects.Link;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void lvwList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = e.Data.GetData(DataFormats.Text).ToString();
                if (LiquipediaClient.IsValidLiquipediaLink(text))
                    NewSource(text, true);
            }
        }

        private void NewSource(string text, bool use)
        {
            var item = new ListViewItem();
            item.Checked = use;
            item.Text = text;
            lvwList.Items.Add(item);
        }
        
        #endregion

        #region Edit Menu

        private void iDConformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "playerpka.dict");
        }
        private void mapNameConformanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", "mapakas.dict");
        }
        
        #endregion

        private void btnRun_Click(object sender, EventArgs e)
        {
            CarnoServiceEventSink sink = new CarnoServiceEventSink();
            CarnoGenerator.LoadRewriter("playerpka.dict", sink.IdRewriter);
            CarnoGenerator.LoadRewriter("mapakas.dict", sink.MapRewriter);

            foreach (ListViewItem item in lvwList.CheckedItems)
            {
                try
                {
                    CarnoService.Accumulate(item.Text, sink);
                    item.ForeColor = Color.Green;
                }
                catch
                {
                    // just swallow errors for now
                    item.ForeColor = Color.Red;
                }
                Application.DoEvents();
            }

            string result = CarnoGenerator.Generate(sink, 
                teamStats: chkIncludeTeam.Checked, 
                aceMatches: chkIncludeAce.Checked, 
                allKills: chkIncludeAllKills.Checked);
            UI.ShowDialog(new UIDocument("Statistics", result));

            foreach (ListViewItem item in lvwList.CheckedItems)
            {
                item.ForeColor = lvwList.ForeColor;
            }
        }
    }
}
