using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAECheckStock 的摘要说明。
	/// </summary>
	public class DAECheckStock:DAEBase
	{
		public DAECheckStock()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		public string GetStockWareHouseIDByPKValue(string pkValue)
		{		
			string returnString = string.Empty;
			string strSql = "Select WHID From WH_CheckStock Where CheckStockID = '" + pkValue + "'";
			DataTable dt = this.BaseDataAccess.GetDataTable( strSql );
			
			if( dt != null && dt.Rows.Count > 0 )
			{
				returnString = dt.Rows[0]["WHID"].ToString();
			}			
			return returnString;
		}

	}
}
