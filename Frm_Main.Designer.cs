namespace 数据采集档案管理系统___加工版
{
    partial class Frm_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_WorkLog = new System.Windows.Forms.Button();
            this.btn_MyWork = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btn_MyWork);
            this.groupBox1.Controls.Add(this.btn_WorkLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 87);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btn_WorkLog
            // 
            this.btn_WorkLog.Location = new System.Drawing.Point(12, 15);
            this.btn_WorkLog.Name = "btn_WorkLog";
            this.btn_WorkLog.Size = new System.Drawing.Size(71, 61);
            this.btn_WorkLog.TabIndex = 0;
            this.btn_WorkLog.Text = "加工登记";
            this.btn_WorkLog.UseVisualStyleBackColor = true;
            this.btn_WorkLog.Click += new System.EventHandler(this.btn_WorkLog_Click);
            // 
            // btn_MyWork
            // 
            this.btn_MyWork.Location = new System.Drawing.Point(99, 15);
            this.btn_MyWork.Name = "btn_MyWork";
            this.btn_MyWork.Size = new System.Drawing.Size(71, 61);
            this.btn_MyWork.TabIndex = 1;
            this.btn_MyWork.Text = "加工中";
            this.btn_MyWork.UseVisualStyleBackColor = true;
            this.btn_MyWork.Click += new System.EventHandler(this.btn_MyWork_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(188, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 61);
            this.button2.TabIndex = 2;
            this.button2.Text = "已返工";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(780, 494);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "Frm_Main";
            this.Text = "档案数据采集系统 - 著录加工";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_WorkLog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_MyWork;
    }
}

