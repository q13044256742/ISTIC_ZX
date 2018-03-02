namespace 数据采集档案管理系统___加工版
{
    partial class Frm_Export
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
            this.txt_Export_Path = new System.Windows.Forms.TextBox();
            this.btn_Choose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择导出目录：";
            // 
            // txt_Export_Path
            // 
            this.txt_Export_Path.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Export_Path.Location = new System.Drawing.Point(24, 52);
            this.txt_Export_Path.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Export_Path.Name = "txt_Export_Path";
            this.txt_Export_Path.Size = new System.Drawing.Size(329, 29);
            this.txt_Export_Path.TabIndex = 1;
            // 
            // btn_Choose
            // 
            this.btn_Choose.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Choose.Location = new System.Drawing.Point(363, 49);
            this.btn_Choose.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Choose.Name = "btn_Choose";
            this.btn_Choose.Size = new System.Drawing.Size(102, 33);
            this.btn_Choose.TabIndex = 2;
            this.btn_Choose.Text = "选择文件夹";
            this.btn_Choose.UseVisualStyleBackColor = true;
            this.btn_Choose.Click += new System.EventHandler(this.btn_Choose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(20, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "当前进度：";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 148);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(441, 28);
            this.progressBar1.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button2.Location = new System.Drawing.Point(188, 203);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 33);
            this.button2.TabIndex = 5;
            this.button2.Text = "开始导出(&S)";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Frm_Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 255);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Choose);
            this.Controls.Add(this.txt_Export_Path);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Export";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据移交";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Export_Path;
        private System.Windows.Forms.Button btn_Choose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button2;
    }
}