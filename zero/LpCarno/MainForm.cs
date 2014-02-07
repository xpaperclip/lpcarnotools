using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LxTools;
using LxTools.Liquipedia;
using LxTools.Carno;

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
            LoadPageLayouts();
            LoadConfiguration();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var sw = new StreamWriter("carno.dict"))
            {
                sw.WriteLine("$layout={0}", cmbPageLayouts.SelectedItem);

                foreach (ListViewItem item in lvwList.Items)
                {
                    sw.WriteLine("Source={0},{1}", item.Checked ? "1" : "0", item.Text);
                }
            }
        }

        private string layoutfolder;
        private void LoadPageLayouts()
        {
            var selectedItem = cmbPageLayouts.SelectedItem;

            cmbPageLayouts.Items.Clear();
            layoutfolder = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            layoutfolder = Path.Combine(Path.GetDirectoryName(layoutfolder), "pages");
            foreach (string file in Directory.GetFiles(layoutfolder, "*.xml"))
            {
                cmbPageLayouts.Items.Add(Path.GetFileNameWithoutExtension(file));
            }

            cmbPageLayouts.SelectedItem = selectedItem;

            if (cmbPageLayouts.SelectedIndex < 0)
                cmbPageLayouts.SelectedIndex = 0;
        }

        private void LoadConfiguration()
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
                        string value = s.Substring(equals + 1);
                        switch (s.Substring(1, equals - 1).Trim())
                        {
                            case "layout":
                                if (cmbPageLayouts.Items.Contains(value))
                                    cmbPageLayouts.SelectedItem = value;
                                break;
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
            DataStore data = new DataStore();
            DataStore.LoadRewriter("playerpka.dict", data.IdRewriter);
            DataStore.LoadRewriter("mapakas.dict", data.MapRewriter);

            foreach (ListViewItem item in lvwList.CheckedItems)
            {
                try
                {
                    data.Accumulate(item.Text);
                    item.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    // just swallow errors for now
                    item.ForeColor = Color.Red;
                }
                Application.DoEvents();
            }

            PageGenerator pagegen = PageGenerator.FromXml(System.Xml.Linq.XDocument.Load("pages/" + cmbPageLayouts.SelectedItem + ".xml"));
            string result = pagegen.Emit(data);
            UI.ShowDialog(new UIDocument("Statistics", result));

            foreach (ListViewItem item in lvwList.CheckedItems)
            {
                item.ForeColor = lvwList.ForeColor;
            }
        }

        private void btnRefreshLayouts_Click(object sender, EventArgs e)
        {
            LoadPageLayouts();
        }
        private void btnOpenLayoutsFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(layoutfolder);
        }

        private void runProfileButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Carno Profiles (*.xml)|*.xml";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    RunProfile.Execute(dlg.FileName);
                    Cursor.Current = Cursors.Default;
                }
            }
        }
    }
}
