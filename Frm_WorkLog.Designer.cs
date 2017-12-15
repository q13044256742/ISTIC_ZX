namespace 数据采集档案管理系统___加工版
{
    partial class Frm_WorkLog
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
            this.dgv_WorkLog = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Back = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Find = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkLog)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_WorkLog
            // 
            this.dgv_WorkLog.AllowUserToAddRows = false;
            this.dgv_WorkLog.AllowUserToDeleteRows = false;
            this.dgv_WorkLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_WorkLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_WorkLog.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_WorkLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_WorkLog.Location = new System.Drawing.Point(0, 70);
            this.dgv_WorkLog.Name = "dgv_WorkLog";
            this.dgv_WorkLog.ReadOnly = true;
            this.dgv_WorkLog.RowTemplate.Height = 23;
            this.dgv_WorkLog.Size = new System.Drawing.Size(739, 374);
            this.dgv_WorkLog.TabIndex = 0;
            this.dgv_WorkLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_WorkLog_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Back);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_Find);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 64);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.Location = new System.Drawing.Point(656, 17);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 39);
            this.btn_Back.TabIndex = 2;
            this.btn_Back.Text = "返回(&B)";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(315, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 36);
            this.textBox1.TabIndex = 1;
            // 
            // btn_Find
            // 
            this.btn_Find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Find.Location = new System.Drawing.Point(575, 17);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(75, 39);
            this.btn_Find.TabIndex = 0;
            this.btn_Find.Text = "查询(&F)";
            this.btn_Find.UseVisualStyleBackColor = true;
            // 
            // Frm_WorkLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 444);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_WorkLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Frm_WorkLog";
            this.Text = "加工登记";
            this.Load += new System.EventHandler(this.Frm_WorkLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkLog)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_WorkLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_Find;
        private System.Windows.Forms.Button btn_Back;
    }
}