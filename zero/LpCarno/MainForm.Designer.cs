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
            this.chkIncludeAce = new System.Windows.Forms.CheckBox();
            this.chkIncludeTeam = new System.Windows.Forms.CheckBox();
            this.chkIncludeAllKills = new System.Windows.Forms.CheckBox();
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
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(584, 25);
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
            this.btnAdd.Size = new System.Drawing.Size(49, 22);
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
            this.btnRun.Size = new System.Drawing.Size(48, 22);
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
            this.toolStripSplitButton1.Size = new System.Drawing.Size(59, 22);
            this.toolStripSplitButton1.Text = "Edit";
            // 
            // iDConformToolStripMenuItem
            // 
            this.iDConformToolStripMenuItem.Name = "iDConformToolStripMenuItem";
            this.iDConformToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.iDConformToolStripMenuItem.Text = "Player/Team ID Conformance";
            this.iDConformToolStripMenuItem.Click += new System.EventHandler(this.iDConformToolStripMenuItem_Click);
            // 
            // mapNameConformanceToolStripMenuItem
            // 
            this.mapNameConformanceToolStripMenuItem.Name = "mapNameConformanceToolStripMenuItem";
            this.mapNameConformanceToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
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
            this.splitContainer1.Panel2.Controls.Add(this.chkIncludeAce);
            this.splitContainer1.Panel2.Controls.Add(this.chkIncludeTeam);
            this.splitContainer1.Panel2.Controls.Add(this.chkIncludeAllKills);
            this.splitContainer1.Size = new System.Drawing.Size(584, 366);
            this.splitContainer1.SplitterDistance = 253;
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
            this.lvwList.Size = new System.Drawing.Size(584, 253);
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
            // chkIncludeAce
            // 
            this.chkIncludeAce.AutoSize = true;
            this.chkIncludeAce.Checked = true;
            this.chkIncludeAce.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeAce.Location = new System.Drawing.Point(12, 69);
            this.chkIncludeAce.Name = "chkIncludeAce";
            this.chkIncludeAce.Size = new System.Drawing.Size(184, 19);
            this.chkIncludeAce.TabIndex = 2;
            this.chkIncludeAce.Text = "Include Ace Matches statistics";
            this.chkIncludeAce.UseVisualStyleBackColor = true;
            // 
            // chkIncludeTeam
            // 
            this.chkIncludeTeam.AutoSize = true;
            this.chkIncludeTeam.Checked = true;
            this.chkIncludeTeam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeTeam.Location = new System.Drawing.Point(12, 19);
            this.chkIncludeTeam.Name = "chkIncludeTeam";
            this.chkIncludeTeam.Size = new System.Drawing.Size(146, 19);
            this.chkIncludeTeam.TabIndex = 0;
            this.chkIncludeTeam.Text = "Include Team statistics";
            this.chkIncludeTeam.UseVisualStyleBackColor = true;
            this.chkIncludeTeam.CheckedChanged += new System.EventHandler(this.chkIncludeTeam_CheckedChanged);
            // 
            // chkIncludeAllKills
            // 
            this.chkIncludeAllKills.AutoSize = true;
            this.chkIncludeAllKills.Checked = true;
            this.chkIncludeAllKills.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeAllKills.Location = new System.Drawing.Point(12, 44);
            this.chkIncludeAllKills.Name = "chkIncludeAllKills";
            this.chkIncludeAllKills.Size = new System.Drawing.Size(152, 19);
            this.chkIncludeAllKills.TabIndex = 1;
            this.chkIncludeAllKills.Text = "Include All-Kills column";
            this.chkIncludeAllKills.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 391);
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
        private System.Windows.Forms.CheckBox chkIncludeTeam;
        private System.Windows.Forms.CheckBox chkIncludeAllKills;
        private System.Windows.Forms.CheckBox chkIncludeAce;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem iDConformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapNameConformanceToolStripMenuItem;
    }
}