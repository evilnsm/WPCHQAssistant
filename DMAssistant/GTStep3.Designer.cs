namespace DMAssistant
{
    partial class GTStep3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTStep3));
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonGTOK = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(996, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "正在自动登录，请在页面加载完成后勾选所有站点，点击“同步到井区、站队”，否则数据将不会被采纳。";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(10, 40);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1410, 650);
            this.webBrowser1.TabIndex = 12;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // buttonGTOK
            // 
            this.buttonGTOK.Location = new System.Drawing.Point(1313, 701);
            this.buttonGTOK.Name = "buttonGTOK";
            this.buttonGTOK.Size = new System.Drawing.Size(107, 31);
            this.buttonGTOK.TabIndex = 14;
            this.buttonGTOK.Text = "完毕";
            this.buttonGTOK.UseVisualStyleBackColor = true;
            this.buttonGTOK.Click += new System.EventHandler(this.buttonGTOK_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(10, 696);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1420, 2);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // GTStep3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 734);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonGTOK);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GTStep3";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "第三步：数据同步";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GTStep3_FormClosed);
            this.Load += new System.EventHandler(this.GTStep3_Load);
            this.Move += new System.EventHandler(this.GTStep3_Move);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button buttonGTOK;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}