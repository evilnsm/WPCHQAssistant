using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DMAssistant
{
    struct well
    {
        public string well_name;//井号
        public string oil_pressure;//油压        
        public string water_content;//含水
        public string dfl;//动液面
        public string pump_nom_diam;//泵径
        public string pump_set_depth; //泵挂
        public string first_rod_nom;//1级杆径,且杆径只能取值‘CYG19’/'CYG22'等
        public string first_rod_length;//1级杆长
        public string second_rod_nom;//2级杆径
        public string second_rod_length;//2级杆长
        public string three_rod_nom;//3级杆径
        public string three_rod_length;//3级杆长
    }
    class OilWell   //不能把NULL井号、NULLCONN传入
    {
        public OilWell(SqlConnection sqlConnection,well onewell)
        {
            conn = sqlConnection ;
            oilWell = onewell;
        }

        //public int Update_time_press_water_dfl(); //在SRPProdDynamic表里需一次性更新日期、油压、含水、动液面，必须采用插入方式！！！
        //private bool Query_is_have(string well_name);//在更新动态数据前需要先查询下这口井是否上线，在静态表里查
        //public bool IsNum(string str); //检查相应字段
        //public int Update_time_ppnom_ppdepth(); //在SRPProdStatic表里需一次性更新日期、泵径、泵挂
        //public int Update_tube_string();//在TubeString表里管材型号等不变，仅更新update_time、step_length；
        //public int Update_tube_anchor();//在TubeAnchor表里仅更新update_time；pos_from_top
        //public int Update_rod();//更新油杆组合，需在RodString表里update_time,step_index_from_top,step_length;每一级一条记录
        //返回-1
        //返回0
        //返回1
        //返回88 拍克
        //返回99 缺源数据
        //返回77 未上线，在更新每张表之前都进行此项检查
        //返回66 非数字
        //返回55
        private SqlConnection conn;
        private well oilWell;
 
        private bool Query_is_have() //查询是否上线井
        {
            SqlCommand comand = new SqlCommand();
            conn.Open();
            comand.CommandText = MakeSQLQ(oilWell);
            comand.Connection = conn;
            comand.CommandType = System.Data.CommandType.Text;
            if (Convert.IsDBNull(comand.ExecuteScalar()) == false && Convert.ToInt16(comand.ExecuteScalar()) > 0)
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }

        public int Update_time_press_water_dfl()
        {
            if (oilWell.dfl == "" || oilWell.oil_pressure == "" || oilWell.water_content == "")
                return 99;
            if (oilWell.dfl == "拍克")
                return 88;
            if (Query_is_have() == false)
                return 77;
            if (IsNum(oilWell.water_content) == false || IsNum(oilWell.dfl) == false || IsNum(oilWell.oil_pressure) == false)
                return 66;
            SqlCommand comand = new SqlCommand();
            conn.Open();
            comand.CommandText = MakeSQLA(oilWell);
            comand.Connection = conn;
            comand.CommandType = System.Data.CommandType.Text;
            int rst = comand.ExecuteNonQuery();
            conn.Close();
            return rst;
        }



        public int Update_time_ppnom_ppdepth()
        {

            if (oilWell.pump_nom_diam == "" || oilWell.pump_set_depth == "")
                return 99;
            if (Query_is_have() == false)
                return 77;
            if (IsNum(oilWell.pump_nom_diam) == false || IsNum(oilWell.pump_set_depth) == false)
                return 66;

            SqlCommand comand = new SqlCommand();
            conn.Open();
            comand.CommandText = MakeSQLB(oilWell);
            comand.Connection = conn;
            comand.CommandType = System.Data.CommandType.Text;
            int rst = comand.ExecuteNonQuery();
            conn.Close();
            return rst;
        }

        public int Update_tube_string()
        {
            if (oilWell.pump_set_depth == "")
                return 99;
            if (Query_is_have() == false)
                return 77;
            if (IsNum(oilWell.pump_set_depth) == false)
                return 66;
            SqlCommand comand = new SqlCommand();
            conn.Open();
            comand.CommandText = MakeSQLTS(oilWell);
            comand.Connection = conn;
            comand.CommandType = System.Data.CommandType.Text;
            int rst = comand.ExecuteNonQuery();
            conn.Close();
            return rst;
        }

        public int Update_tube_anchor()
        {
             if (oilWell.pump_set_depth == "")
                return 99;
             if (Query_is_have() == false)
                 return 77;
             if (IsNum(oilWell.pump_set_depth) == false )
                 return 66;
            else
            {
                SqlCommand comand = new SqlCommand();
                conn.Open();
                comand.CommandText = MakeSQLTA(oilWell);
                comand.Connection = conn;
                comand.CommandType = System.Data.CommandType.Text;
                int rst = comand.ExecuteNonQuery();
                conn.Close();
                return rst;
            }
        }

        //private int Update_rod()
        //再新增，INSERT INTO RodString(well_name,update_datetime,step_index_from_top,spec,material_grade,every_length,step_length)VALUES('冯58-86','1999-11-11 0:00:00',1,'CYG19','钢D',0,1299)
        public int Update_rod()
        {
            if (oilWell.first_rod_length == "" || oilWell.first_rod_nom == "")
                return 99;
            if (Query_is_have() == false)
                return 77;
            if (IsNum(oilWell.first_rod_length) == false)
                return 66;
            else
            {
                SqlCommand comand1 = new SqlCommand();
                SqlCommand comand2 = new SqlCommand();
                conn.Open();
                comand1.CommandText = MakeSQLDR(oilWell);
                comand1.Connection = conn;
                comand1.CommandType = System.Data.CommandType.Text;//删除行
                int delrow = comand1.ExecuteNonQuery();
                comand2.CommandText = MakeSQLIR(oilWell);
                comand2.Connection = conn;
                comand2.CommandType = System.Data.CommandType.Text;//增加行，并且将增加的行数返回
                int rst = comand2.ExecuteNonQuery();
                conn.Close();
                return rst;
            }
        }

        private double[]  Get_gas_oil_factor()//获取气油比 和 调整因子
        {
            object[] obj = new object[2];
            double[] rst = {10,1};
            DateTime  lasttime = new DateTime();
            SqlCommand comand = new SqlCommand();
            //获取最大时间
            comand.CommandText = @"SELECT max(update_datetime) FROM SRPProdDynamic WHERE well_name='" + oilWell.well_name + "'";
            comand.Connection = conn;
            comand.CommandType = System.Data.CommandType.Text;
            if (Convert.IsDBNull(comand.ExecuteScalar()) == false) //可能原来没有动态数据，只有查询到非DBnull才行
            {
                lasttime = Convert.ToDateTime(comand.ExecuteScalar());
                //查询气油比
                comand.CommandText = @"SELECT gas_oil_ratio FROM SRPProdDynamic WHERE well_name='" + oilWell.well_name + "' AND update_datetime='" + lasttime.ToShortDateString() + " " + lasttime.ToLongTimeString() + "'";
                comand.Connection = conn;
                comand.CommandType = System.Data.CommandType.Text;
                obj[0] = comand.ExecuteScalar();
                //查询调整因子
                comand = new SqlCommand();
                comand.CommandText = @"SELECT adjust_factor FROM SRPProdDynamic WHERE well_name='" + oilWell.well_name + "' AND update_datetime='" + lasttime.ToShortDateString() + " " + lasttime.ToLongTimeString() + "'";
                comand.Connection = conn;
                comand.CommandType = System.Data.CommandType.Text;
                obj[1] = comand.ExecuteScalar();
                rst[0] = Convert.ToDouble(obj[0].ToString());
                rst[1] = Convert.ToDouble(obj[1].ToString());
            }
            return rst;
        }


        private string MakeSQLA(well anywell)
        {
            double[] gas_facor = new double[2];
            gas_facor = Get_gas_oil_factor();     
            string str = "INSERT INTO SRPProdDynamic(well_name,update_datetime,water_content,gas_oil_ratio,oil_pressure,casing_pressure,dfl,pu_working_para1,pu_working_para2,pu_working_para3,adjust_factor)VALUES('";
            DateTime now = DateTime.Now;
            str = str + anywell.well_name + "','";  
            str = str + now.ToShortDateString() + " " + now.ToLongTimeString()+ "'," ; 
                str = str + anywell.water_content + "," + gas_facor[0].ToString().Trim() + ",";
            str = str + anywell.oil_pressure + ",0,";
            str = str + anywell.dfl + ",0,0,0," + gas_facor[1].ToString().Trim() + ")";
          return str;
        }
        private string MakeSQLB(well anywell)
        {
            string str = "UPDATE SRPProdStatic SET update_datetime='";
            DateTime now = DateTime.Now;
            str = str + now.ToShortDateString() + "', pump_nom_diam=";
            str = str + anywell.pump_nom_diam + ",";
            str = str + "pump_set_depth=" + anywell.pump_set_depth;
            str = str + " WHERE well_name ='";
            str = str + anywell.well_name + "'";
            return str;
        }
        private string MakeSQLTS(well anywell)
        {
            string str = "UPDATE TubeString SET update_datetime='";
            DateTime now = DateTime.Now;
            str = str + now.ToString() + "', step_length=";
            str = str + anywell.pump_set_depth;
            str = str + " WHERE well_name ='";
            str = str + anywell.well_name + "'";
            return str;
        }
        private string MakeSQLTA(well anywell)
        {
            string str = "UPDATE TubeAnchor SET update_datetime='";
            DateTime now = DateTime.Now;
            str = str + now.ToString() + "', pos_from_top=";
            str = str + anywell.pump_set_depth;
            str = str + " WHERE well_name ='";
            str = str + anywell.well_name + "'";
            return str;
        }
        private string MakeSQLDR(well anywell)
        {
            string str = "DELETE FROM RodString WHERE well_name='";
            str = str + anywell.well_name + "'";
            return str;
        }
        private string MakeSQLIR(well anywell)
        {
            string str = null ;
            string str2 = null ;
            string str3 = null ;

            str = "INSERT INTO RodString(well_name,update_datetime,step_index_from_top,spec,material_grade,every_length,step_length)VALUES('";
            str = str + anywell.well_name + "','";
            DateTime now = DateTime.Now;
            str = str + now.ToString() + "',1,'";
            str = str + anywell.first_rod_nom + "','钢D',0,";
            str = str + anywell.first_rod_length + ")";
            if (oilWell.second_rod_length =="" || oilWell.second_rod_nom =="")
                return str;
            else
            {
                str2 = "INSERT INTO RodString(well_name,update_datetime,step_index_from_top,spec,material_grade,every_length,step_length)VALUES('";
                str2 = str2 + anywell.well_name + "','";
                str2 = str2 + now.ToString() + "',2,'";
                str2 = str2 + anywell.second_rod_nom + "','钢D',0,";
                str2 = str2 + anywell.second_rod_length + ")";
                str = str + ";" + str2;
            }
            if (oilWell.three_rod_nom == "" || oilWell.three_rod_length  =="")
                return str ;
            else
            {
                str3 = "INSERT INTO RodString(well_name,update_datetime,step_index_from_top,spec,material_grade,every_length,step_length)VALUES('";
                str3 = str3 + anywell.well_name + "','";
                str3 = str3 + now.ToString() + "',3,'";
                str3 = str3 + anywell.three_rod_nom + "','钢D',0,";
                str3 = str3 + anywell.three_rod_length + ")";
                str = str + ";" + str3;
                return str;
            }
        } //存在多级组合，用多条语句，采用分号连接多条SQL语句

        private string MakeSQLQ(well anywell)
        {
            string str = @"SELECT count(*) FROM SRPProdStatic WHERE well_name='"　+　anywell.well_name  + "'";
            return str;
        }

        private static bool IsNum(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] < '0' || str[i] > '9' ) && str[i] != '.')
                    return false;
            }
            return true;
        } 





     




    }
}
