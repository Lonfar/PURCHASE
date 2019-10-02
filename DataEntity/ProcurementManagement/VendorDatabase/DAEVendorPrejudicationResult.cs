using System;
using System.Data.SqlClient;
using Cnwit.Utility;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// 供应商预审结果数据实体类 Added by Liujun at 10.20
	/// </summary>
	public class DAEVendorPrejudicationResult : DAEBase
	{
		public DAEVendorPrejudicationResult()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 数据访问实体
		/// </summary>
		DataAcess da = Common.GetProjectDataAcess.GetDataAcess();

		/// <summary>
		/// 通过供应商ID获得供应商编号(Added By Liujun at 10.20)
		/// </summary>
		/// <param name="VendorIDKey">供应商ID</param>
		/// <returns>供应商编号</returns>
		public string GetVendorNo ( string VendorIDKey )
		{
			string strVendorNo = string.Empty;

			string SelectSql = "Select dbo.GenerateCode ( '"+VendorIDKey+"' ) AS Code";

			using ( SqlDataReader dr = da.GetDataReader ( SelectSql ) )
			{
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        strVendorNo = Convert.ToString(dr["Code"]);
                    }
                }
			}

			return strVendorNo;
		}

		/// <summary>
		/// 通过供应商ID生成供应商编号( Added By Liujun at 1.11 )
		/// </summary>
		/// <param name="VendorIDKey">供应商ID</param>
		/// <returns>供应商编号,如果正常更新则返回编号,如果返回为空则表示更新失败</returns>
		public string UpdateNo ( string VendorIDKey )
		{
			string strVendorNo = string.Empty;

			strVendorNo = GetVendorNo ( VendorIDKey );

			if ( strVendorNo.Length > 0 )
			{
				da.ExecuteDMLSQL ( "UPDATE Vendor SET VendorNo = '"+strVendorNo+"'  WHERE IDKey = '"+VendorIDKey+"'" );	
			}

			return strVendorNo;
		}

		public void  UpdateNo ( string VendorIDKey , string VendorNo )
		{
			da.ExecuteDMLSQL ( "UPDATE Vendor SET VendorNo = '"+VendorNo+"'  WHERE IDKey = '"+VendorIDKey+"'" );
		}

        public string JugeVendorNo(string VendorNo)
        {
            string sSql = "select * from Vendor where VendorNo = '" + VendorNo + "' ";
            DataTable dt = da.GetDataTable(sSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "2")
                {
                    return "";
                }
                return "RepeateName";
            }
            else
            {
                return "";
            }
        }
	}
}
