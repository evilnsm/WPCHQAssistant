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
    public delegate void MyDelegate(string text);

    public partial class GTStep3 : Form
    {
        public event MyDelegate MyEvent;

        public GTStep3()
        {
            InitializeComponent();
        }

        int fstLogin = 0;

        private void GTStep3_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://10.78.*.*");
        }
        private void buttonGTOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            fstLogin++;
            if (fstLogin == 1)
            {
                HtmlElement btn = null;
                HtmlDocument doc = webBrowser1.Document;
                foreach (HtmlElement em in doc.All)
                {
                    string str = em.Name;
                    if (str == "TextBox1" || str == "TextBox2" || str == "Button1")
                        switch (str)
                        {
                            case "TextBox1": em.SetAttribute("value", "admin***"); break;
                            case "TextBox2": em.SetAttribute("value", "admin******"); break;
                            case "Button1": btn = em; break;
                            default: break;
                        }
                }
                btn.InvokeMember("click");
            }
            else if (fstLogin == 2)
                //webBrowser1.Navigate("http://10.78.*.*/DBManage.aspx");
                webBrowser1.Navigate("http://10.78.*.*/DBManage/OrganizationsL4.aspx?ParentOrgan=******");
            else
                return;
        }

        private void GTStep3_Move(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            Point pt = new Point();
            pt.X = (rect.Width - 1400) / 2;
            pt.Y = (rect.Height - 768) / 2;
            this.Location = pt;
        }

        private void GTStep3_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyEvent("ok");
        }


    }
}
