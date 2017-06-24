using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace DMAssistant
{
    public partial class GTStep2 : Form
    {
        public event MyDelegate MyEvent;
        public GTStep2(DataTable datatable)
        {
            InitializeComponent();
            dt = datatable; //在构造函数中添加实参
        }
        DataTable dt;//形参传实参
        DataTable rstt = new DataTable();//存放结果的表
        int count = 0;
        private void buttonStep2Cancel_Click(object sender, EventArgs e)
        {
            MyEvent("OK");
            this.Close();
        }

        private void GTStep2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt.DefaultView;
            progressBar1.Maximum = dt.Rows.Count;
        }

        private void buttonStep2Next_Click(object sender, EventArgs e)
        {
            count++;
            switch(count)
            {
                case 1:
                {
                    buttonStep2Next.Text = "核对数据";
                    buttonStep2Next.Enabled = false;
                    buttonStep2Next.Enabled = CheckDataFormat();
                    label4.Text = "再次单击下一步将开始更新...";
                    buttonStep2Next.Text = "下一步";
                    break;
                }
                case 2:
                {
                    buttonStep2Next.Text = "正在更新";
                    buttonStep2Next.Enabled = false;
                    progressBar1.Visible = true;
                    buttonStep2Next.Enabled = StartSync();
                    dataGridView1.DataSource = rstt.DefaultView;
                    buttonStep2Next.Text = "下一步";
                    buttonStep2Save.Enabled = buttonStep2Next.Enabled;
                    break;
                }
                default:
                {
                    GTStep3 gtStep3 = new GTStep3();
                    gtStep3.MyEvent += new MyDelegate(gtStep3_MyEvent);
                    gtStep3.Show();
                    break;
                }
            }
        }

        void gtStep3_MyEvent(string text)
        {
            this.Close();
        }
        private bool CheckDataFormat()
        {
            bool ck = true;
            textBox1.Text = "程序检查结果如下：";
            for(int i =0;i<dt.Rows.Count;i++)
            {
                bool havejh = false ;                
                if (dt.Rows[i]["油压"].ToString() == "" || dt.Rows[i]["含水"].ToString() == "" || dt.Rows[i]["动液面"].ToString() == "")  
                {
                    ck = false;
                    textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误: 油压、含水、动液面等三项不允许空值.";
                    continue;
                }                
                if (Convert.ToDouble(dt.Rows[i]["油压"]) > 6 )
                {                    
                    ck = false;
                    textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误: 油压超过6MPa.";
                    havejh = true; 
                }
                if (Convert.ToDouble(dt.Rows[i]["含水"]) > 100 )
                {                        
                    ck = false;
                    if (havejh)
                        textBox1.Text += "错误：含水超限.";
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：含水超过100%.";
                        havejh = true;
                    }
                }
                if(dt.Rows[i]["动液面"].ToString() == "拍克")
                {                       

                    if(havejh)
                        textBox1.Text +="警告：动液面拍克.";
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "警告：动液面拍克.";
                        havejh = true;                   
                    }
                }
                else
                {
                    if (dt.Rows[i]["泵挂"].ToString() != "" && (Convert.ToDouble(dt.Rows[i]["动液面"]) > Convert.ToDouble(dt.Rows[i]["泵挂"])))
                    {                           
                        ck = false;
                        if (havejh)
                            textBox1.Text += "错误：动液面超过泵挂.";
                        else
                        {
                            textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：动液面超限.";
                            havejh = true;
                        }

                    }
                }
                if (dt.Rows[i]["泵径"].ToString() != "" && dt.Rows[i]["泵径"].ToString() != "28" && dt.Rows[i]["泵径"].ToString() != "32" 
                    && dt.Rows[i]["泵径"].ToString() != "38" && dt.Rows[i]["泵径"].ToString() != "44"  && dt.Rows[i]["泵径"].ToString() != "56" )
                {                       
                    ck = false;
                    if (havejh)
                        textBox1.Text += "错误：泵径不合规格.";
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：泵径不合规格.";
                        havejh = true;
                    }
                }
                if(dt.Rows[i]["泵挂"].ToString() != "" && Convert.ToDouble(dt.Rows[i]["泵挂"]) > 2500)
                {                       
                    ck = false;
                    if (havejh)
                        textBox1.Text += "错误：该井泵挂已远超三叠系常见井深.";
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：该井泵挂已远超三叠系常见井深.";
                        havejh = true;
                    }
                }
                if (dt.Rows[i]["一级杆径"].ToString() != "" && dt.Rows[i]["一级杆径"].ToString() != "CYG16" && dt.Rows[i]["一级杆径"].ToString() != "CYG19" 
                    && dt.Rows[i]["一级杆径"].ToString() != "CYG22" && dt.Rows[i]["一级杆径"].ToString() != "CYG25")
                {
                    ck = false;
                    if (havejh)
                        textBox1.Text += "错误：一级杆径不合规格.";      
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：一级杆径不合规格.";
                        havejh = true;
                    }
                }
                if (dt.Rows[i]["一级杆长"].ToString() != "" && dt.Rows[i]["泵挂"].ToString() != "" && (Convert.ToDouble(dt.Rows[i]["一级杆长"]) > Convert.ToDouble(dt.Rows[i]["泵挂"])))
                {                        
                    ck = false;
                    if (havejh)
                        textBox1.Text += "错误：一级杆长超过泵挂.";
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：一级杆长超过泵挂.";
                        havejh = true;
                    }
                }
                if(dt.Rows[i]["二级杆径"].ToString() != "" && dt.Rows[i]["二级杆径"].ToString() != "CYG16" && dt.Rows[i]["二级杆径"].ToString() != "CYG19" 
                    && dt.Rows[i]["二级杆径"].ToString() != "CYG22" && dt.Rows[i]["二级杆径"].ToString() != "CYG25")
                {
                    ck = false;                    
                    if(havejh)
                        textBox1.Text += "错误：二级杆径不合规格.";
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：二级杆径不合规格.";
                        havejh = true;
                    }

                }

                if(dt.Rows[i]["三级杆径"].ToString() != "" && dt.Rows[i]["三级杆径"].ToString() != "CYG16" &&dt.Rows[i]["三级杆径"].ToString() != "CYG19" 
                    && dt.Rows[i]["三级杆径"].ToString() != "CYG22" && dt.Rows[i]["三级杆径"].ToString() != "CYG25")
                {                    
                    ck = false;
                    if(havejh)
                        textBox1.Text += "错误：三级杆径不合规格.";
                    else
                    {
                        textBox1.Text += "\r\n" + dt.Rows[i]["井号"].ToString() + "错误：三级杆径不合规格.";
                        havejh = true;
                    }
                }
                /*if(System.Math.Abs(Convert.ToDouble(dt.Rows[i]["一级杆长"]) + Convert.ToDouble(dt.Rows[i]["二级杆长"]) + Convert.ToDouble(dt.Rows[i]["三级杆长"])) > 30 )
                {
                    textBox1.Text +="抽油杆总长与泵挂相差大于30米.";
                    ck = false;
                }*/
            }
            return ck;
        }
        private bool StartSync()
        {
            SqlConnection conn = new SqlConnection("server=10.78.*.*;database=******;uid=******;pwd=******");
            DataColumn dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int16");
            dc.ColumnName = "序号";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "井号";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "油压更新结果";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "含水更新结果";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "液面更新结果";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "泵径更新结果";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "泵挂更新结果";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "油管深度更新";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "油管锚定更新";
            rstt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "杆柱组合更新级数";
            rstt.Columns.Add(dc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                well singleoil;
                singleoil.well_name = dt.Rows[i]["井号"].ToString().Trim();
                singleoil.oil_pressure = dt.Rows[i]["油压"].ToString().Trim();
                singleoil.water_content = dt.Rows[i]["含水"].ToString().Trim();
                singleoil.dfl = dt.Rows[i]["动液面"].ToString().Trim();
                singleoil.pump_nom_diam = dt.Rows[i]["泵径"].ToString().Trim();
                singleoil.pump_set_depth = dt.Rows[i]["泵挂"].ToString().Trim();
                singleoil.first_rod_length = dt.Rows[i]["一级杆长"].ToString().Trim();
                singleoil.first_rod_nom = dt.Rows[i]["一级杆径"].ToString().Trim();
                singleoil.second_rod_length = dt.Rows[i]["二级杆长"].ToString().Trim();
                singleoil.second_rod_nom = dt.Rows[i]["二级杆径"].ToString().Trim();
                singleoil.three_rod_length = dt.Rows[i]["三级杆长"].ToString().Trim();
                singleoil.three_rod_nom = dt.Rows[i]["三级杆径"].ToString().Trim();

                OilWell htl = new OilWell(conn, singleoil);
                DataRow dr = rstt.NewRow();
                dr["序号"] = i + 1;
                dr["井号"] = dt.Rows[i]["井号"];
                switch (htl.Update_time_press_water_dfl()) //往动态这张表里插入操作
                {
                    case 1: //成功
                        {
                            dr["油压更新结果"] = "成功";
                            dr["含水更新结果"] = "成功";
                            dr["液面更新结果"] = "成功";
                            break;
                        }
                    case 66:
                        {
                            dr["油压更新结果"] = "数据非数字";
                            dr["含水更新结果"] = "数据非数字";
                            dr["液面更新结果"] = "数据非数字";
                            break;
                        }
                    case 77:
                        {
                            dr["油压更新结果"] = "未找到或未上线";
                            dr["含水更新结果"] = "未找到或未上线";
                            dr["液面更新结果"] = "未找到或未上线";
                            break;
                        }
                    case 88:
                        {
                            dr["油压更新结果"] = "拍克错误";
                            dr["含水更新结果"] = "拍克错误";
                            dr["液面更新结果"] = "拍克错误";
                            break;
                        }
                    case 99:
                        {
                            dr["油压更新结果"] = "缺少源数据";
                            dr["含水更新结果"] = "缺少源数据";
                            dr["液面更新结果"] = "缺少源数据";
                            break;
                        }

                    default:
                        {
                            dr["油压更新结果"] = "未知错误!";
                            dr["含水更新结果"] = "未知错误!";
                            dr["液面更新结果"] = "未知错误!";
                            break;
                        }
                }
                switch (htl.Update_time_ppnom_ppdepth()) //在静态数据表里直接更新
                {
                    case 1: //成功
                        {
                            dr["泵径更新结果"] = "成功";
                            dr["泵挂更新结果"] = "成功";
                            break;
                        }
                    case 66:
                                {
                                    dr["泵径更新结果"] = "数据非数字";
                                    dr["泵挂更新结果"] = "数据非数字";
                                    break;
                                }
                            case 77:
                                {
                                    dr["泵径更新结果"] = "未找到或未上线";
                                    dr["泵挂更新结果"] = "未找到或未上线";
                                    break;
                                }
                            case 99:
                                {
                                    dr["泵径更新结果"] = "缺少源数据";
                                    dr["泵挂更新结果"] = "缺少源数据";
                                    break;
                                }
                            default:
                                {
                                    dr["泵径更新结果"] = "未知错误!";
                                    dr["泵挂更新结果"] = "未知错误!";
                                    break;
                                }
                }
                switch (htl.Update_tube_string())//更新油管深度
                {
                            case 1:
                                {
                                    dr["油管深度更新"] = "成功";
                                    break;
                                }
                            case 66:
                                {
                                    dr["油管深度更新"] = "数据非数字";
                                    break;
                                }
                            case 77:
                                {
                                    dr["油管深度更新"] = "未找到或未上线";
                                    break;
                                }
                            case 99:
                                {
                                    dr["油管深度更新"] = "缺少源数据";
                                    break;
                                }
                            default:
                                {
                                    dr["油管深度更新"] = "未知错误！";
                                    break;
                                }
                }
                switch (htl.Update_tube_anchor())//更新油管锚定
                {
                            case 1:
                                {
                                    dr["油管锚定更新"] = "成功";
                                    break;
                                }
                            case 66:
                                {
                                    dr["油管锚定更新"] = "数据非数字";
                                    break;
                                }
                            case 77:
                                {
                                    dr["油管锚定更新"] = "未找到或未上线";
                                    break;
                                }
                            case 99:
                                {
                                    dr["油管锚定更新"] = "缺少源数据";
                                    break;
                                }
                            default:
                                {
                                    dr["油管深度更新"] = "未知错误！";
                                    break;
                                }
                }
                int rr = htl.Update_rod();//更新杆柱组合，先删除后插入
                switch (rr)
                {
                            case 66:
                                {
                                    dr["杆柱组合更新级数"] = "数据非数字";
                                    break;
                                }
                            case 77:
                                {
                                    dr["杆柱组合更新级数"] = "未找到或未上线";
                                    break;
                                }
                            case 99:
                                {
                                    dr["杆柱组合更新级数"] = "缺少源数据";
                                    break;
                                }
                            default:
                                {
                                    dr["杆柱组合更新级数"] = rr.ToString();
                                    break;
                                }
                }
                rstt.Rows.Add(dr);
                    progressBar1.Value = i;
            }
                return true;
         }
        public void DataToExcel(DataTable m_DataTable)
        {
            SaveFileDialog kk = new SaveFileDialog();
            kk.Title = "保存EXECL文件";
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if (kk.ShowDialog() == DialogResult.OK)
            {
                string FileName = kk.FileName + ".xls";
                if (File.Exists(FileName))
                    File.Delete(FileName);
                FileStream objFileStream;
                StreamWriter objStreamWriter;
                string strLine = "";
                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
                for (int i = 0; i < m_DataTable.Columns.Count; i++)
                {
                    strLine = strLine + m_DataTable.Columns[i].Caption.ToString() + Convert.ToChar(9);
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";

                for (int i = 0; i < m_DataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < m_DataTable.Columns.Count; j++)
                    {
                        if (m_DataTable.Rows[i].ItemArray[j] == null)
                            strLine = strLine + " " + Convert.ToChar(9);
                        else
                        {
                            string rowstr = "";
                            rowstr = m_DataTable.Rows[i].ItemArray[j].ToString();
                            if (rowstr.IndexOf("\r\n") > 0)
                                rowstr = rowstr.Replace("\r\n", " ");
                            if (rowstr.IndexOf("\t") > 0)
                                rowstr = rowstr.Replace("\t", " ");
                            strLine = strLine + rowstr + Convert.ToChar(9);
                        }
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
                objStreamWriter.Close();
                objFileStream.Close();
                MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonStep2Save_Click(object sender, EventArgs e)
        {
            DataToExcel(rstt);
        }

        private void GTStep2_Move(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            Point pt = new Point();
            pt.X = (rect.Width - 1400) / 2;
            pt.Y = (rect.Height - 768) / 2;
            this.Location = pt;
        }

        private void GTStep2_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyEvent("OK");
        }
    }
}
