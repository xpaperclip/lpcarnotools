namespace LpCarno
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.iDConformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapNameConformanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvwList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRefreshLayouts = new System.Windows.Forms.Button();
            this.btnOpenLayoutsFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPageLayouts = new System.Windows.Forms.ComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.runProfileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripSeparator1,
            this.btnAdd,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnRun,
            this.toolStripSeparator3,
            this.toolStripSplitButton1,
            this.toolStripSeparator4,
            this.runProfileButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(581, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(46, 22);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Remove";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRun
            // 
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(46, 22);
            this.btnRun.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iDConformToolStripMenuItem,
            this.mapNameConformanceToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(57, 22);
            this.toolStripSplitButton1.Text = "Edit";
            // 
            // iDConformToolStripMenuItem
            // 
            this.iDConformToolStripMenuItem.Name = "iDConformToolStripMenuItem";
            this.iDConformToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.iDConformToolStripMenuItem.Text = "Player/Team ID Conformance";
            this.iDConformToolStripMenuItem.Click += new System.EventHandler(this.iDConformToolStripMenuItem_Click);
            // 
            // mapNameConformanceToolStripMenuItem
            // 
            this.mapNameConformanceToolStripMenuItem.Name = "mapNameConformanceToolStripMenuItem";
            this.mapNameConformanceToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.mapNameConformanceToolStripMenuItem.Text = "Map Name Conformance";
            this.mapNameConformanceToolStripMenuItem.Click += new System.EventHandler(this.mapNameConformanceToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvwList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnRefreshLayouts);
            this.splitContainer1.Panel2.Controls.Add(this.btnOpenLayoutsFolder);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.cmbPageLayouts);
            this.splitContainer1.Size = new System.Drawing.Size(581, 482);
            this.splitContainer1.SplitterDistance = 425;
            this.splitContainer1.TabIndex = 1;
            // 
            // lvwList
            // 
            this.lvwList.AllowDrop = true;
            this.lvwList.CheckBoxes = true;
            this.lvwList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvwList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwList.Location = new System.Drawing.Point(0, 0);
            this.lvwList.Name = "lvwList";
            this.lvwList.Size = new System.Drawing.Size(581, 425);
            this.lvwList.TabIndex = 0;
            this.lvwList.UseCompatibleStateImageBehavior = false;
            this.lvwList.View = System.Windows.Forms.View.Details;
            this.lvwList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwList_ItemChecked);
            this.lvwList.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwList_DragDrop);
            this.lvwList.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwList_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Page";
            this.columnHeader1.Width = 550;
            // 
            // btnRefreshLayouts
            // 
            this.btnRefreshLayouts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshLayouts.FlatAppearance.BorderSize = 0;
            this.btnRefreshLayouts.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRefreshLayouts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshLayouts.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshLayouts.Image")));
            this.btnRefreshLayouts.Location = new System.Drawing.Point(516, 13);
            this.btnRefreshLayouts.Name = "btnRefreshLayouts";
            this.btnRefreshLayouts.Size = new System.Drawing.Size(25, 23);
            this.btnRefreshLayouts.TabIndex = 1;
            this.btnRefreshLayouts.UseVisualStyleBackColor = true;
            this.btnRefreshLayouts.Click += new System.EventHandler(this.btnRefreshLayouts_Click);
            // 
            // btnOpenLayoutsFolder
            // 
            this.btnOpenLayoutsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenLayoutsFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenLayoutsFolder.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOpenLayoutsFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenLayoutsFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLayoutsFolder.Image")));
            this.btnOpenLayoutsFolder.Location = new System.Drawing.Point(542, 13);
            this.btnOpenLayoutsFolder.Name = "btnOpenLayoutsFolder";
            this.btnOpenLayoutsFolder.Size = new System.Drawing.Size(25, 23);
            this.btnOpenLayoutsFolder.TabIndex = 2;
            this.btnOpenLayoutsFolder.UseVisualStyleBackColor = true;
            this.btnOpenLayoutsFolder.Click += new System.EventHandler(this.btnOpenLayoutsFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Page Layout";
            // 
            // cmbPageLayouts
            // 
            this.cmbPageLayouts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPageLayouts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageLayouts.FormattingEnabled = true;
            this.cmbPageLayouts.Location = new System.Drawing.Point(106, 13);
            this.cmbPageLayouts.Name = "cmbPageLayouts";
            this.cmbPageLayouts.Size = new System.Drawing.Size(406, 23);
            this.cmbPageLayouts.TabIndex = 0;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // runProfileButton
            // 
            this.runProfileButton.Image = ((System.Drawing.Image)(resources.GetObject("runProfileButton.Image")));
            this.runProfileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runProfileButton.Name = "runProfileButton";
            this.runProfileButton.Size = new System.Drawing.Size(152, 22);
            this.runProfileButton.Text = "Run Profile (experimental)";
            this.runProfileButton.Click += new System.EventHandler(this.runProfileButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 507);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Carno";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem iDConformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapNameConformanceToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbPageLayouts;
        private System.Windows.Forms.Button btnOpenLayoutsFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefreshLayouts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton runProfileButton;
    }
}