using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMAssistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool Ping()
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions op = new System.Net.NetworkInformation.PingOptions();
            op.DontFragment = true;
            string data = "Test";
            byte[] buf = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            System.Net.NetworkInformation.PingReply rp = p.Send("10.78.*.*",timeout,buf,op);
            if (rp.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "正在测试网络状态...";
            toolStripStatusLabel2.Text = "版权所有(C)";
            toolStripStatusLabel3.Text = "E-mail:******@qq.com";
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void buttonWellPro_Click(object sender, EventArgs e)
        {

                GTStep1 gtStep1 = new GTStep1();
                gtStep1.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            if (Ping() == true)
            {
                buttonWellPro.Enabled = true;
                toolStripStatusLabel1.Text = "成功连接至http://10.78.*.*...";
            }
            else
                MessageBox.Show("网络不通，请检查网络设置！\r\n无法启用数据更新向导！");

        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            Point pt = new Point();
            pt.X = (rect.Width-600)/2;
            pt.Y = (rect.Height - 400)/2;
            this.Location = pt;
        }



    }
}
