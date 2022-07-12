namespace CreateTOCFromBookmarks
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSimilarIndividualsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSamelayerEquivalentNeighborToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtNodeInfo = new System.Windows.Forms.RichTextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.button7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.outputToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1139, 27);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPdfToolStripMenuItem,
            this.loadTreeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(42, 24);
            this.fileToolStripMenuItem.Text = "file";
            // 
            // loadPdfToolStripMenuItem
            // 
            this.loadPdfToolStripMenuItem.Name = "loadPdfToolStripMenuItem";
            this.loadPdfToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.loadPdfToolStripMenuItem.Text = "load pdf";
            this.loadPdfToolStripMenuItem.Click += new System.EventHandler(this.loadPdfToolStripMenuItem_Click);
            // 
            // loadTreeToolStripMenuItem
            // 
            this.loadTreeToolStripMenuItem.Name = "loadTreeToolStripMenuItem";
            this.loadTreeToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.loadTreeToolStripMenuItem.Text = "load tree";
            this.loadTreeToolStripMenuItem.Click += new System.EventHandler(this.loadTreeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeThisToolStripMenuItem,
            this.removeSimilarIndividualsToolStripMenuItem,
            this.removeSamelayerEquivalentNeighborToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.editToolStripMenuItem.Text = "edit";
            // 
            // removeThisToolStripMenuItem
            // 
            this.removeThisToolStripMenuItem.Name = "removeThisToolStripMenuItem";
            this.removeThisToolStripMenuItem.Size = new System.Drawing.Size(363, 26);
            this.removeThisToolStripMenuItem.Text = "remove this";
            this.removeThisToolStripMenuItem.Click += new System.EventHandler(this.removeThisToolStripMenuItem_Click);
            // 
            // removeSimilarIndividualsToolStripMenuItem
            // 
            this.removeSimilarIndividualsToolStripMenuItem.Name = "removeSimilarIndividualsToolStripMenuItem";
            this.removeSimilarIndividualsToolStripMenuItem.Size = new System.Drawing.Size(363, 26);
            this.removeSimilarIndividualsToolStripMenuItem.Text = "remove similar individuals";
            this.removeSimilarIndividualsToolStripMenuItem.Click += new System.EventHandler(this.removeSimilarIndividualsToolStripMenuItem_Click);
            // 
            // removeSamelayerEquivalentNeighborToolStripMenuItem
            // 
            this.removeSamelayerEquivalentNeighborToolStripMenuItem.Name = "removeSamelayerEquivalentNeighborToolStripMenuItem";
            this.removeSamelayerEquivalentNeighborToolStripMenuItem.Size = new System.Drawing.Size(363, 26);
            this.removeSamelayerEquivalentNeighborToolStripMenuItem.Text = "remove same-layer equivalent neighbor";
            this.removeSamelayerEquivalentNeighborToolStripMenuItem.Click += new System.EventHandler(this.removeSamelayerEquivalentNeighborToolStripMenuItem_Click);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pictureToolStripMenuItem,
            this.pdfToolStripMenuItem});
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.outputToolStripMenuItem.Text = "output";
            // 
            // pictureToolStripMenuItem
            // 
            this.pictureToolStripMenuItem.Name = "pictureToolStripMenuItem";
            this.pictureToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.pictureToolStripMenuItem.Text = "picture";
            this.pictureToolStripMenuItem.Click += new System.EventHandler(this.pictureToolStripMenuItem_Click);
            // 
            // pdfToolStripMenuItem
            // 
            this.pdfToolStripMenuItem.Name = "pdfToolStripMenuItem";
            this.pdfToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.pdfToolStripMenuItem.Text = "pdf";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(646, 108);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 19);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(756, 128);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(646, 144);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "load tree";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(520, 128);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(624, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "_";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(711, 19);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create TOC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(587, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 17;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(865, 80);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(106, 101);
            this.button8.TabIndex = 14;
            this.button8.Text = "Same-Layer Neighbor Equivalent Removal";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(408, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "load PDF";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtNodeInfo
            // 
            this.txtNodeInfo.Location = new System.Drawing.Point(818, 281);
            this.txtNodeInfo.Name = "txtNodeInfo";
            this.txtNodeInfo.Size = new System.Drawing.Size(204, 182);
            this.txtNodeInfo.TabIndex = 7;
            this.txtNodeInfo.Text = "";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 6;
            this.numericUpDown1.Location = new System.Drawing.Point(727, 49);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(206, 25);
            this.numericUpDown1.TabIndex = 15;
            this.numericUpDown1.ThousandsSeparator = true;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(423, 112);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(76, 55);
            this.button6.TabIndex = 11;
            this.button6.Text = "remove this";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(612, 19);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 18;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 6;
            this.numericUpDown2.Location = new System.Drawing.Point(939, 48);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(171, 25);
            this.numericUpDown2.TabIndex = 16;
            this.numericUpDown2.ThousandsSeparator = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1017, 125);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(93, 56);
            this.button7.TabIndex = 13;
            this.button7.Text = "remove similar individuals";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1005, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(528, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(1139, 647);
            this.treeView1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 674);
            this.Controls.Add(this.txtNodeInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.textBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPdfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSimilarIndividualsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSamelayerEquivalentNeighborToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pdfToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox txtNodeInfo;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeView1;
    }
}

