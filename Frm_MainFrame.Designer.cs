namespace 数据采集档案管理系统___加工版
{
    partial class Frm_MainFrame
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
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("008ZX02101-001");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("008ZX02101-002");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("008ZX02101", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("008ZX02102");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("863计划", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_Add = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pic_Import = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pic_Export = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pic_Manager = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pic_Check = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Query = new System.Windows.Forms.Button();
            this.txt_Query_Name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Query_Code = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.lbl_TotalAmount = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_SH = new System.Windows.Forms.Button();
            this.tv_DataTree = new System.Windows.Forms.TreeView();
            this.dgv_DataList = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Add)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Import)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Export)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Manager)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Check)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pic_Add);
            this.panel1.Location = new System.Drawing.Point(107, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(91, 82);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "录入编辑";
            // 
            // pic_Add
            // 
            this.pic_Add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Add.Image = global::数据采集档案管理系统___加工版.Properties.Resources.png_0289;
            this.pic_Add.Location = new System.Drawing.Point(18, 7);
            this.pic_Add.Name = "pic_Add";
            this.pic_Add.Size = new System.Drawing.Size(55, 48);
            this.pic_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Add.TabIndex = 0;
            this.pic_Add.TabStop = false;
            this.pic_Add.Click += new System.EventHandler(this.Pic_Add_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pic_Import);
            this.panel2.Location = new System.Drawing.Point(6, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(91, 82);
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据导入";
            // 
            // pic_Import
            // 
            this.pic_Import.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Import.Image = global::数据采集档案管理系统___加工版.Properties.Resources.png_0222;
            this.pic_Import.Location = new System.Drawing.Point(18, 7);
            this.pic_Import.Name = "pic_Import";
            this.pic_Import.Size = new System.Drawing.Size(55, 48);
            this.pic_Import.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Import.TabIndex = 0;
            this.pic_Import.TabStop = false;
            this.pic_Import.Click += new System.EventHandler(this.Pic_Import_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.pic_Export);
            this.panel3.Location = new System.Drawing.Point(309, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(91, 82);
            this.panel3.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "档案移交";
            // 
            // pic_Export
            // 
            this.pic_Export.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Export.Image = global::数据采集档案管理系统___加工版.Properties.Resources.png_0228;
            this.pic_Export.Location = new System.Drawing.Point(18, 7);
            this.pic_Export.Name = "pic_Export";
            this.pic_Export.Size = new System.Drawing.Size(55, 48);
            this.pic_Export.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Export.TabIndex = 0;
            this.pic_Export.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Location = new System.Drawing.Point(4, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1006, 96);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.pic_Manager);
            this.panel5.Location = new System.Drawing.Point(410, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(91, 82);
            this.panel5.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "后台管理";
            // 
            // pic_Manager
            // 
            this.pic_Manager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Manager.Image = global::数据采集档案管理系统___加工版.Properties.Resources.png_0228;
            this.pic_Manager.Location = new System.Drawing.Point(18, 7);
            this.pic_Manager.Name = "pic_Manager";
            this.pic_Manager.Size = new System.Drawing.Size(55, 48);
            this.pic_Manager.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Manager.TabIndex = 0;
            this.pic_Manager.TabStop = false;
            this.pic_Manager.Click += new System.EventHandler(this.pic_Manager_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.pic_Check);
            this.panel4.Location = new System.Drawing.Point(208, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(91, 82);
            this.panel4.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "校验/比对";
            // 
            // pic_Check
            // 
            this.pic_Check.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Check.Image = global::数据采集档案管理系统___加工版.Properties.Resources.png_0281;
            this.pic_Check.Location = new System.Drawing.Point(18, 7);
            this.pic_Check.Name = "pic_Check";
            this.pic_Check.Size = new System.Drawing.Size(55, 48);
            this.pic_Check.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Check.TabIndex = 0;
            this.pic_Check.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::数据采集档案管理系统___加工版.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(522, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(484, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btn_Query);
            this.groupBox2.Controls.Add(this.txt_Query_Name);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt_Query_Code);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btn_Delete);
            this.groupBox2.Controls.Add(this.lbl_TotalAmount);
            this.groupBox2.Location = new System.Drawing.Point(4, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1006, 70);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // btn_Query
            // 
            this.btn_Query.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Query.Location = new System.Drawing.Point(714, 23);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(75, 28);
            this.btn_Query.TabIndex = 6;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.Btn_Query_Click);
            // 
            // txt_Query_Name
            // 
            this.txt_Query_Name.Location = new System.Drawing.Point(531, 27);
            this.txt_Query_Name.Name = "txt_Query_Name";
            this.txt_Query_Name.Size = new System.Drawing.Size(176, 21);
            this.txt_Query_Name.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(496, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "名称";
            // 
            // txt_Query_Code
            // 
            this.txt_Query_Code.Location = new System.Drawing.Point(356, 27);
            this.txt_Query_Code.Name = "txt_Query_Code";
            this.txt_Query_Code.Size = new System.Drawing.Size(100, 21);
            this.txt_Query_Code.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(321, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "编号";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(114, 22);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 29);
            this.btn_Delete.TabIndex = 1;
            this.btn_Delete.Text = "删除(&D)";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // lbl_TotalAmount
            // 
            this.lbl_TotalAmount.AutoSize = true;
            this.lbl_TotalAmount.Location = new System.Drawing.Point(14, 32);
            this.lbl_TotalAmount.Name = "lbl_TotalAmount";
            this.lbl_TotalAmount.Size = new System.Drawing.Size(83, 12);
            this.lbl_TotalAmount.TabIndex = 0;
            this.lbl_TotalAmount.Text = "共有 0 条数据";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btn_SH);
            this.groupBox3.Controls.Add(this.tv_DataTree);
            this.groupBox3.Controls.Add(this.dgv_DataList);
            this.groupBox3.Location = new System.Drawing.Point(4, 136);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1006, 346);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // btn_SH
            // 
            this.btn_SH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SH.Location = new System.Drawing.Point(197, 17);
            this.btn_SH.Name = "btn_SH";
            this.btn_SH.Size = new System.Drawing.Size(11, 327);
            this.btn_SH.TabIndex = 2;
            this.btn_SH.Text = "<";
            this.btn_SH.UseVisualStyleBackColor = true;
            this.btn_SH.Click += new System.EventHandler(this.Btn_SH_Click);
            // 
            // tv_DataTree
            // 
            this.tv_DataTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tv_DataTree.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tv_DataTree.Location = new System.Drawing.Point(3, 17);
            this.tv_DataTree.Name = "tv_DataTree";
            treeNode11.Name = "节点3";
            treeNode11.Text = "008ZX02101-001";
            treeNode12.Name = "节点4";
            treeNode12.Text = "008ZX02101-002";
            treeNode13.Name = "节点0";
            treeNode13.Text = "008ZX02101";
            treeNode14.Name = "节点2";
            treeNode14.Text = "008ZX02102";
            treeNode15.Name = "863计划";
            treeNode15.Text = "863计划";
            this.tv_DataTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode15});
            this.tv_DataTree.Size = new System.Drawing.Size(205, 326);
            this.tv_DataTree.TabIndex = 1;
            // 
            // dgv_DataList
            // 
            this.dgv_DataList.AllowUserToAddRows = false;
            this.dgv_DataList.AllowUserToDeleteRows = false;
            this.dgv_DataList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_DataList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DataList.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgv_DataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DataList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_DataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DataList.Location = new System.Drawing.Point(208, 17);
            this.dgv_DataList.Name = "dgv_DataList";
            this.dgv_DataList.ReadOnly = true;
            this.dgv_DataList.RowTemplate.Height = 23;
            this.dgv_DataList.Size = new System.Drawing.Size(797, 329);
            this.dgv_DataList.TabIndex = 0;
            this.dgv_DataList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_DataList_CellContentClick);
            // 
            // Frm_MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 483);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "著录加工系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MainFrame_Load);
            this.Shown += new System.EventHandler(this.Frm_MainFrame_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Add)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Import)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Export)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Manager)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Check)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Add;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pic_Import;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pic_Export;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.TextBox txt_Query_Name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Query_Code;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label lbl_TotalAmount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_DataList;
        private System.Windows.Forms.TreeView tv_DataTree;
        private System.Windows.Forms.Button btn_SH;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pic_Check;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pic_Manager;
    }
}