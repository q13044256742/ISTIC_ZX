﻿namespace 数据采集档案管理系统___加工版
{
    partial class Frm_SetContextPath
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ContextPath = new System.Windows.Forms.TextBox();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.btn_ChoosePath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前路径";
            // 
            // txt_ContextPath
            // 
            this.txt_ContextPath.Location = new System.Drawing.Point(30, 60);
            this.txt_ContextPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_ContextPath.Name = "txt_ContextPath";
            this.txt_ContextPath.Size = new System.Drawing.Size(302, 26);
            this.txt_ContextPath.TabIndex = 1;
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(181, 145);
            this.btn_Sure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(90, 31);
            this.btn_Sure.TabIndex = 2;
            this.btn_Sure.Text = "确认(&S)";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // btn_ChoosePath
            // 
            this.btn_ChoosePath.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_ChoosePath.Location = new System.Drawing.Point(340, 58);
            this.btn_ChoosePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ChoosePath.Name = "btn_ChoosePath";
            this.btn_ChoosePath.Size = new System.Drawing.Size(78, 31);
            this.btn_ChoosePath.TabIndex = 3;
            this.btn_ChoosePath.Text = "选择路径";
            this.btn_ChoosePath.UseVisualStyleBackColor = true;
            this.btn_ChoosePath.Click += new System.EventHandler(this.btn_ChoosePath_Click);
            // 
            // Frm_SetContextPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(452, 201);
            this.Controls.Add(this.btn_ChoosePath);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.txt_ContextPath);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Frm_SetContextPath";
            this.Text = "指定全文路径";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ContextPath;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.Button btn_ChoosePath;
    }
}