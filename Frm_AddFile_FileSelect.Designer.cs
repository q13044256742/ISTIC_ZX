﻿namespace 数据采集档案管理系统___加工版
{
    partial class Frm_AddFile_FileSelect
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
            if(disposing && (components != null))
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
            this.tv_file = new System.Windows.Forms.TreeView();
            this.btn_sure = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_filename = new System.Windows.Forms.Label();
            this.rdo_ShowAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tv_file
            // 
            this.tv_file.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tv_file.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tv_file.LineColor = System.Drawing.Color.DimGray;
            this.tv_file.Location = new System.Drawing.Point(2, 47);
            this.tv_file.Name = "tv_file";
            this.tv_file.Size = new System.Drawing.Size(647, 301);
            this.tv_file.TabIndex = 0;
            this.tv_file.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_file_AfterSelect);
            // 
            // btn_sure
            // 
            this.btn_sure.Location = new System.Drawing.Point(196, 360);
            this.btn_sure.Name = "btn_sure";
            this.btn_sure.Size = new System.Drawing.Size(75, 31);
            this.btn_sure.TabIndex = 1;
            this.btn_sure.Text = "确定";
            this.btn_sure.UseVisualStyleBackColor = true;
            this.btn_sure.Click += new System.EventHandler(this.btn_sure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前已选择：";
            // 
            // lbl_filename
            // 
            this.lbl_filename.AutoSize = true;
            this.lbl_filename.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_filename.Location = new System.Drawing.Point(78, 15);
            this.lbl_filename.Name = "lbl_filename";
            this.lbl_filename.Size = new System.Drawing.Size(32, 17);
            this.lbl_filename.TabIndex = 3;
            this.lbl_filename.Text = "null";
            // 
            // rdo_ShowAll
            // 
            this.rdo_ShowAll.AutoSize = true;
            this.rdo_ShowAll.Location = new System.Drawing.Point(560, 15);
            this.rdo_ShowAll.Name = "rdo_ShowAll";
            this.rdo_ShowAll.Size = new System.Drawing.Size(72, 16);
            this.rdo_ShowAll.TabIndex = 4;
            this.rdo_ShowAll.Text = "显示全部";
            this.rdo_ShowAll.UseVisualStyleBackColor = true;
            this.rdo_ShowAll.CheckedChanged += new System.EventHandler(this.rdo_ShowAll_CheckedChanged);
            // 
            // Frm_AddFile_FileSelect
            // 
            this.AcceptButton = this.btn_sure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 480);
            this.Controls.Add(this.rdo_ShowAll);
            this.Controls.Add(this.lbl_filename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_sure);
            this.Controls.Add(this.tv_file);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AddFile_FileSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件选择";
            this.Load += new System.EventHandler(this.Frm_AddFile_FileSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_file;
        private System.Windows.Forms.Button btn_sure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_filename;
        private System.Windows.Forms.CheckBox rdo_ShowAll;
    }
}