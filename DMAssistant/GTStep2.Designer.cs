namespace DMAssistant
{
    partial class GTStep2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTStep2));
            this.buttonStep2Next = new System.Windows.Forms.Button();
            this.buttonStep2Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonStep2Save = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStep2Next
            // 
            this.buttonStep2Next.Location = new System.Drawing.Point(1313, 694);
            this.buttonStep2Next.Name = "buttonStep2Next";
            this.buttonStep2Next.Size = new System.Drawing.Size(107, 31);
            this.buttonStep2Next.TabIndex = 2;
            this.buttonStep2Next.Text = "下一步";
            this.buttonStep2Next.UseVisualStyleBackColor = true;
            this.buttonStep2Next.Click += new System.EventHandler(this.buttonStep2Next_Click);
            // 
            // buttonStep2Cancel
            // 
            this.buttonStep2Cancel.Location = new System.Drawing.Point(1050, 694);
            this.buttonStep2Cancel.Name = "buttonStep2Cancel";
            this.buttonStep2Cancel.Size = new System.Drawing.Size(107, 31);
            this.buttonStep2Cancel.TabIndex = 1;
            this.buttonStep2Cancel.Text = "取消";
            this.buttonStep2Cancel.UseVisualStyleBackColor = true;
            this.buttonStep2Cancel.Click += new System.EventHandler(this.buttonStep2Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "已获得如下数据：";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(8, 651);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1420, 2);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1400, 600);
            this.dataGridView1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1033, 666);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(377, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "若人工核对无误请单击“下一步”由程序再次核对，返回请按“取消”";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 663);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1019, 62);
            this.textBox1.TabIndex = 15;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "程序将根据如下特性自动检查数据有效性：\r\n0≤油压≤6;        0≤含水≤100;                0≤动液面≤泵挂≤2500;    泵径" +
                "=28/32/38/44/56;\r\n每级杆长≤泵挂;    |各级杆长总和-泵挂|≤30;    上述值类型为阿拉伯数字(可带小数位)\r\n每级杆径=CYG16/" +
                "CYG19/CYG22/CYG25,井号为中/英文字符.";
            // 
            // buttonStep2Save
            // 
            this.buttonStep2Save.Enabled = false;
            this.buttonStep2Save.Location = new System.Drawing.Point(1181, 694);
            this.buttonStep2Save.Name = "buttonStep2Save";
            this.buttonStep2Save.Size = new System.Drawing.Size(107, 31);
            this.buttonStep2Save.TabIndex = 1;
            this.buttonStep2Save.Text = "导出至Excel";
            this.buttonStep2Save.UseVisualStyleBackColor = true;
            this.buttonStep2Save.Click += new System.EventHandler(this.buttonStep2Save_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1033, 666);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(387, 19);
            this.progressBar1.TabIndex = 16;
            this.progressBar1.Visible = false;
            // 
            // GTStep2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 734);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStep2Next);
            this.Controls.Add(this.buttonStep2Save);
            this.Controls.Add(this.buttonStep2Cancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GTStep2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "第二步：核对数据开始更新";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GTStep2_FormClosed);
            this.Load += new System.EventHandler(this.GTStep2_Load);
            this.Move += new System.EventHandler(this.GTStep2_Move);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStep2Next;
        private System.Windows.Forms.Button buttonStep2Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonStep2Save;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}