namespace 数据采集档案管理系统___加工版
{
    partial class Frm_MyWork
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Find = new System.Windows.Forms.Button();
            this.dgv_MyWork = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MyWork)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_Find);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 64);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(282, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 36);
            this.textBox1.TabIndex = 1;
            // 
            // btn_Find
            // 
            this.btn_Find.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Find.Location = new System.Drawing.Point(542, 17);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(75, 39);
            this.btn_Find.TabIndex = 0;
            this.btn_Find.Text = "查询(&F)";
            this.btn_Find.UseVisualStyleBackColor = true;
            // 
            // dgv_MyWork
            // 
            this.dgv_MyWork.AllowUserToAddRows = false;
            this.dgv_MyWork.AllowUserToDeleteRows = false;
            this.dgv_MyWork.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_MyWork.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_MyWork.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_MyWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_MyWork.Location = new System.Drawing.Point(0, 70);
            this.dgv_MyWork.Name = "dgv_MyWork";
            this.dgv_MyWork.ReadOnly = true;
            this.dgv_MyWork.RowTemplate.Height = 23;
            this.dgv_MyWork.Size = new System.Drawing.Size(622, 301);
            this.dgv_MyWork.TabIndex = 2;
            this.dgv_MyWork.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_MyWork_CellClick);
            // 
            // Frm_MyWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 371);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_MyWork);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Frm_MyWork";
            this.Text = "我的加工 - 加工中";
            this.Load += new System.EventHandler(this.Frm_MyWork_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MyWork)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_Find;
        private System.Windows.Forms.DataGridView dgv_MyWork;
    }
}