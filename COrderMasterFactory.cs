using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace prjMenu.Models
{
    public class COrderMasterFactory
    {
        public COrderMasterDetailList queryById(int fM_Id)
        {
            // Step1.找出目前登入會員且已送出的訂單編號的所需訂單主檔資訊
            SqlConnection con = new SqlConnection(@"Server=tcp:team2.database.windows.net,1433;Initial Catalog=dbRecipe;Persist Security Info=False;User ID=team2;Password=Team31649700;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tOrderMaster WHERE fM_Id= @AA AND fO_Finished = @BB", con);
            cmd.Parameters.AddWithValue("@AA", fM_Id);
            cmd.Parameters.AddWithValue("@BB", 1);
            SqlDataReader reader = cmd.ExecuteReader();
            COrderMasterDetailList 總項 = new COrderMasterDetailList();
            List<COrderMasterDetail> 回傳list = new List<COrderMasterDetail>();
            COrderMasterDetail comi = null;
            while (reader.Read())
            {
                comi = new COrderMasterDetail();
                comi.f訂購日期 = (string)reader["fO_OrderDate"];
                comi.f訂單編號 = (string)reader["fO_Id"];
                comi.f訂單總價 = (int)reader["fO_Total"];
                comi.f訂單狀態 = (bool)reader["fO_Finished"];
                回傳list.Add(comi);
                總項.list用戶訂單 = 回傳list;
            }
            con.Close();

            // Step2.根據找到的訂單編號來查詢細項
            if (comi != null)
            {
                List<COrderDetail> 訂單細項 = new List<COrderDetail>();
                COrderDetail cod = null;
                string sql = "SELECT * FROM tOrderDetail WHERE fO_Id=";
                for (int i = 0; i < 回傳list.Count; i++)
                {
                    sql += "'" + 回傳list[i].f訂單編號 + "'";
                    if (i == 回傳list.Count - 1)
                    {
                        break;
                    }
                    sql += "OR fO_Id=";
                }
                con.Open();
                SqlCommand cmd2 = new SqlCommand(sql, con);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    cod = new COrderDetail();
                    cod.f是否購買 = (bool)reader2["fOD_Check"];
                    if (cod.f是否購買 == true)
                    {
                        cod.f訂單編號 = (string)reader2["fO_Id"];
                        cod.f食材名稱 = reader2["fRD_Ingredients"].ToString();
                        cod.f訂購數量 = (int)reader2["fQty"];
                        cod.f食材單位 = (string)reader2["fRD_Unit"];
                        訂單細項.Add(cod);
                    }
                }
                con.Close();

                // Step3.將細項和主檔資訊比對訂單編號後存到List
                List<COrderDetail> temp;
                COrderDetail odtemp;

                for (int j = 0; j < 總項.list用戶訂單.Count; j++)
                {
                    temp = new List<COrderDetail>();
                    var query = from t1 in 訂單細項
                                where t1.f訂單編號 == 總項.list用戶訂單[j].f訂單編號
                                group t1 by t1.f食材名稱 into g
                                select new
                                {
                                    f食材名稱 = g.Key,
                                    f訂購數量 = g.Sum(x => x.f訂購數量),
                                    g.First().f食材單位
                                };

                    foreach (var i in query)
                    {
                        odtemp = new COrderDetail();
                        odtemp.f食材名稱 = i.f食材名稱;
                        odtemp.f訂購數量 = i.f訂購數量;
                        odtemp.f食材單位 = i.f食材單位;
                        temp.Add(odtemp);
                    }
                    總項.list用戶訂單[j].list訂單明細 = temp;
                }
                return 總項;
            }
            else
            {
                return null;
            }
        }
    }
}
