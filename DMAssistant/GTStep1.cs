using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO; 

namespace DMAssistant
{

    public partial class GTStep1 : Form
    {
        public GTStep1()
        {
            InitializeComponent();
        }
        public DataTable dt;//形参，要传递给下个FORM
        private void buttonStep1Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStep1Next_Click(object sender, EventArgs e)
        {
            buttonStep1Next.Text  = "正在整理数据";
            DataTable datatable = ArrangeMyData(dt);
            GTStep2 gtStep2 = new GTStep2(datatable);
            gtStep2.MyEvent += new MyDelegate(gtStep2_MyEvent);
            gtStep2.Show();
        }

        void gtStep2_MyEvent(string text)
        {
            this.Close();
        }

        private void GTStep1_Load(object sender, EventArgs e)
        {
            File.Delete(@"C:\temp.dbf");
            File.Delete(@"C:\temp.fpt");
        }


        private void ChangeVisable()
        {
            textBox1.Enabled = radioButton1.Checked;
            button1.Enabled = radioButton1.Checked;
            textBox2.Enabled = radioButton2.Checked;
            button2.Enabled = radioButton2.Checked;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisable();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVisable();
        }

        private void button1_Click(object sender, EventArgs e)  //浏览透明
        {
             OpenFileDialog openDialog = new OpenFileDialog();
             openDialog.Filter = "透明数据库文件(*.dbf)|*.dbf";
             if (DialogResult.OK == openDialog.ShowDialog())
             {
                 textBox1.Text = openDialog.FileName;
                 //在此把文件copy至c:\temp.dbf 和c:\temp.fpt
                 File.Delete(@"C:\temp.dbf");
                 File.Delete(@"C:\temp.fpt");
                 File.Copy(textBox1.Text, @"C:\temp.dbf", true);
                 string fpt = Path.GetFileNameWithoutExtension(openDialog.FileName) + ".fpt";
                 try
                 {
                     File.Copy(fpt, @"C:\temp.fpt", true);
                 }
                 catch (System.IO.FileNotFoundException)
                 {
                     MessageBox.Show("未找到相应的fpt文件，请确认在透明数据库同一文件夹下存在同名的fpt文件！");
                     System.Diagnostics.Process.Start(Path.GetDirectoryName(openDialog.FileName));
                     return;
                 }
                 buttonStep1Next.Enabled = ReadToTable();
             }
             else
                 MessageBox.Show("没有选择任何有效文件，请重新选择！");
            return;
        }

        private void button2_Click(object sender, EventArgs e)  //浏览日报
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "日报文件(*.xls)|*.xls";
            if (DialogResult.OK == openDialog.ShowDialog())
            {
                textBox3.Text = "正在加载日报文件...";
                textBox3.Refresh();
                textBox2.Text = openDialog.FileName;
                buttonStep1Next.Enabled = ReadToTable(openDialog.FileName);
            }
            else
                MessageBox.Show("没有选择任何有效文件，请重新选择！");
            return;
        }


/* 用OLEDB导入excel报错：找不到可安装的ISAM 或者 外部表不是预期格式。
 * dt = new DataTable();
            OleDbConnection conn = new OleDbConnection();
            string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fpath + ";Extended Properties='Excel11.0;HDR=NO;IMEX=1'";
            conn.ConnectionString = connStr;
            conn.Open();
            string sql = @"Select * from [油井]";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);                             
            da.Fill(dt);        
            //con.Close(); */ 
        
        private bool ReadToTable()//来自dbf
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = 10;
            progressBar1.Value = 5;
            dt = new DataTable();
            OdbcConnection conn = new OdbcConnection();
            string connStr = @"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=c:\temp.dbf";
            conn.ConnectionString = connStr;
            conn.Open();
            string sql = @"SELECT jh,yy,yco,ycw,ym,bj,bs_sy,YGLX1,YGLX2,YGLX3,YGSL1,YGSL2,YGSL3,YGSL21,YGSL22,YGSL23,YGSL31,YGSL32,YGSL33 FROM C:\temp.dbf WHERE CW!='洛河层' AND hjsj>0";
            OdbcDataAdapter da = new OdbcDataAdapter(sql, conn);
            da.Fill(dt);
            progressBar1.Value = 10;
            textBox3.Text = "已读取透明数据！";
            return true;
        }

        private bool ReadToTable(string fpath)//来自日报文件
        {
            progressBar1.Visible = true;          
            dt = new DataTable();           
            DataColumn myColumn = new DataColumn();
            myColumn.DataType = System.Type.GetType("System.Int16"); //该列的数据类型
            myColumn.ColumnName = "序号";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.DataType = System.Type.GetType("System.String"); //该列的数据类型
            myColumn.ColumnName = "井号";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "油压";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "含水";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "动液面";        //该列的名称
            dt.Columns.Add(myColumn);

            myColumn = new DataColumn();
            myColumn.ColumnName = "泵径";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "泵挂";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "一级杆径";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "一级杆长";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "二级杆径";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "二级杆长";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "三级杆径";        //该列的名称
            dt.Columns.Add(myColumn);
            myColumn = new DataColumn();
            myColumn.ColumnName = "三级杆长";        //该列的名称
            dt.Columns.Add(myColumn);

            Excel.Application excel = null;
            Excel.Workbooks wbs = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            object Nothing = System.Reflection.Missing.Value;
            excel = new Excel.Application();
            excel.UserControl = true;
            excel.DisplayAlerts = false;
            excel.Application.Workbooks.Open(fpath, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
            wbs = excel.Workbooks;
            wb = wbs[1];
            ws = (Excel.Worksheet)wb.Worksheets["油井"];
            int count = 0;
            progressBar1.Maximum = ws.UsedRange.Rows.Count;  
            for (int i = 0; i < ws.UsedRange.Rows.Count; i++)
            {
                if (ws.Cells[i + 4, 4].Value2 > 0 )//开井时间=0或空的井不读
                {
                    DataRow myRow = dt.NewRow();            //0井号,1油压，2含水，3动液面
                    myRow["序号"] = ++count;
                    myRow["井号"] = Convert.ToString(ws.Cells[i + 4, 2].Value);
                    myRow["油压"] = Convert.ToString(ws.Cells[i + 4, 5].Value);
                    myRow["含水"] = Convert.ToString(ws.Cells[i + 4, 8].Value);
                    myRow["动液面"] = Convert.ToString(ws.Cells[i + 4, 9].Value);
                    myRow["泵径"] = "";
                    myRow["泵挂"] = "";
                    myRow["一级杆径"] = "";
                    myRow["一级杆长"] = "";
                    myRow["二级杆径"] = "";
                    myRow["二级杆长"] = "";
                    myRow["三级杆径"] = "";
                    myRow["三级杆长"] = "";
                    dt.Rows.Add(myRow);
                    if (i != 0 && i % 30 == 0)
                    {
                        textBox3.Text = "正在读取第" + Convert.ToString(dt.Rows.Count) + "口实开井数据！";
                        textBox3.Refresh();

                    }
                        progressBar1.Value = i ;
                }
                else                       
                    continue;
            }
            textBox3.Text = "已读取" + Convert.ToString(dt.Rows.Count) + "口实开井数据！";
            textBox3.Refresh();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            ws = null;
            wb.Close(false, Nothing, Nothing);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            wb = null;
            wbs.Close();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wbs);
            wbs = null;
            excel.Application.Workbooks.Close();
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            excel = null;
            GC.Collect();   
            return true;
        }

        private string GetMeter(string ygsl) //从根数*米数中取得米数
        {
            string str = ygsl.Trim();
            str = str.ToLower();
            int pos = str.IndexOf('x');
            return str.Substring(pos + 1, str.Length - pos - 1);
        }


        private DataTable ArrangeMyData(DataTable dt)   //整理数据
        {
            if (dt.Columns[0].ColumnName == "序号")//来自日报无空格，且数据都是string
                return dt;
            //@"SELECT jh,yy,yco,ycw,ym,bj,bs_sy,YGLX1,YGLX2,YGLX3,YGSL1,YGSL2,YGSL3,YGSL21,YGSL22,YGSL23,YGSL31,YGSL32,YGSL33 
            dt.Columns[0].ColumnName = "jh";
            dt.Columns[1].ColumnName = "yy";
            dt.Columns[2].ColumnName = "yco";
            dt.Columns[3].ColumnName = "ycw";
            dt.Columns[4].ColumnName = "ym";
            dt.Columns[5].ColumnName = "bj";
            dt.Columns[6].ColumnName = "bs_sy";
            dt.Columns[7].ColumnName = "YGLX1";
            dt.Columns[8].ColumnName = "YGLX2";
            dt.Columns[9].ColumnName = "YGLX3";
            dt.Columns[10].ColumnName = "YGSL1";
            dt.Columns[11].ColumnName = "YGSL2";
            dt.Columns[12].ColumnName = "YGSL3";
            dt.Columns[13].ColumnName = "YGSL21";
            dt.Columns[14].ColumnName = "YGSL22";
            dt.Columns[15].ColumnName = "YGSL23";               
            dt.Columns[16].ColumnName = "YGSL31";
            dt.Columns[17].ColumnName = "YGSL32";
            dt.Columns[18].ColumnName = "YGSL33";                                      
            DataColumn dc = new DataColumn();
            dc.ColumnName = "序号";
            dc.DataType = System.Type.GetType("System.Int16");
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "井号";
            dc.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(dc);
                
            //增加油压列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "油压";
            dt.Columns.Add(dc);

            //增加含水列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "含水";
            dt.Columns.Add(dc);

            //增加动液面列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "动液面";
            dt.Columns.Add(dc);

            //增加泵径列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "泵径";
            dt.Columns.Add(dc);

            //增加泵挂列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "泵挂";
            dt.Columns.Add(dc);

            //增加1级杆径列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "一级杆径";
            dt.Columns.Add(dc);

            //增加2级杆径列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "二级杆径";
            dt.Columns.Add(dc);

            //增加3级杆径列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "三级杆径";
            dt.Columns.Add(dc);


            //增加1级杆长列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "一级杆长";
            dt.Columns.Add(dc);

            //增加2级杆长列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "二级杆长";
            dt.Columns.Add(dc);

            //增加3级杆长列
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "三级杆长";
            dt.Columns.Add(dc);
                
            for(int i = 0;i<dt.Rows.Count;i++) //经过本次循环，若修井不足3次则把第2次或者第1次的数据复制到第3次里了
            {
                if(dt.Rows[i]["YGSL31"].ToString().Trim() == "")//不可能连1级杆都没有吧
                {
                    dt.Rows[i]["YGSL31"] = dt.Rows[i]["YGSL21"].ToString().Trim();
                    dt.Rows[i]["YGSL32"] = dt.Rows[i]["YGSL22"].ToString().Trim();
                    dt.Rows[i]["YGSL33"] = dt.Rows[i]["YGSL23"].ToString().Trim();
                }
                if (dt.Rows[i]["YGSL31"].ToString().Trim() == "")
                {
                    dt.Rows[i]["YGSL31"] = dt.Rows[i]["YGSL1"].ToString().Trim();
                    dt.Rows[i]["YGSL32"] = dt.Rows[i]["YGSL2"].ToString().Trim();
                    dt.Rows[i]["YGSL33"] = dt.Rows[i]["YGSL3"].ToString().Trim();
                }
                //利用此循环同时把井号格式化好、序号填充好
                dt.Rows[i]["序号"] = i+1;
                dt.Rows[i]["井号"] = dt.Rows[i]["jh"].ToString().Trim();
            }

            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                //改油压类型的循环
                dt.Rows[i]["油压"] = dt.Rows[i]["yy"].ToString().Trim();
                //计算含水的循环
                double water = Convert.ToDouble(dt.Rows[i]["ycw"]);
                double oil = Convert.ToDouble(dt.Rows[i]["yco"]);
                double hs = 0;
                if (water != 0 || oil != 0)
                    hs = 100 * water / (water + oil / 0.85);                    
                dt.Rows[i]["含水"] = hs.ToString("n").Substring(0,4);
                //改动液面类型的循环
                dt.Rows[i]["动液面"] = dt.Rows[i]["ym"].ToString().Trim();
                //改动泵径类型的循环
                dt.Rows[i]["泵径"] = dt.Rows[i]["bj"].ToString().Trim();
                //改动泵挂类型的循环
                dt.Rows[i]["泵挂"] = dt.Rows[i]["bs_sy"].ToString().Trim();
                //将一级杆径改成CYG22类似的循环
                dt.Rows[i]["一级杆径"] = "CYG" + dt.Rows[i]["YGLX1"].ToString().Substring(1, 2);
                //改一级杆长类型的循环，同时从根数*米数中取得米数
                dt.Rows[i]["一级杆长"] = GetMeter(dt.Rows[i]["YGSL31"].ToString().Trim());
                //若二级杆径/杆长都不为空
                if (dt.Rows[i]["YGLX2"].ToString().Trim() != "" && dt.Rows[i]["YGSL32"].ToString().Trim() != "")
                {
                    //将二级杆径改成CYG22类似的循环
                    dt.Rows[i]["二级杆径"] = "CYG" + dt.Rows[i]["YGLX2"].ToString().Substring(1, 2);
                    //改二级杆长类型的循环,同时从根数*米数中取得米数
                    dt.Rows[i]["二级杆长"] = GetMeter(dt.Rows[i]["YGSL32"].ToString().Trim());
                    //再看三级杆，若有3级
                    if (dt.Rows[i]["YGLX3"].ToString().Trim() != "" && dt.Rows[i]["YGSL33"].ToString().Trim() != "")
                    {                            
                        //将三级杆径改成CYG22类似的循环
                        dt.Rows[i]["三级杆径"] = "CYG" + dt.Rows[i]["YGLX3"].ToString().Substring(1, 2);
                        //改三级杆长类型的循环,同时从根数*米数中取得米数                       
                        dt.Rows[i]["三级杆长"] = GetMeter(dt.Rows[i]["YGSL33"].ToString().Trim());
                    }
                    else
                    {
                        //若没有，则将3级置空
                        dt.Rows[i]["三级杆径"] = "";
                        dt.Rows[i]["三级杆长"] = "";                            

                    }
                }
            } 
            //删除多余列   
            dt.Columns.Remove("jh");             
            dt.Columns.Remove("yy");
            dt.Columns.Remove("yco");
            dt.Columns.Remove("ycw");
            dt.Columns.Remove("ym");
            dt.Columns.Remove("bj");
            dt.Columns.Remove("bs_sy");
            dt.Columns.Remove("YGLX1");
            dt.Columns.Remove("YGLX2");
            dt.Columns.Remove("YGLX3");
            dt.Columns.Remove("YGSL1");
            dt.Columns.Remove("YGSL2");
            dt.Columns.Remove("YGSL3");
            dt.Columns.Remove("YGSL21");
            dt.Columns.Remove("YGSL22");
            dt.Columns.Remove("YGSL23");
            dt.Columns.Remove("YGSL31");
            dt.Columns.Remove("YGSL32");
            dt.Columns.Remove("YGSL33");
            return dt;
        }

        private void GTStep1_Move(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect = Screen.GetWorkingArea(this);
            Point pt = new Point();
            pt.X = (rect.Width - 600) / 2;
            pt.Y = (rect.Height - 400) / 2;
            this.Location = pt;
        }
   
    }
          
}
