using System;

using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEMaterialCapacity 的摘要说明。
	/// </summary>
	public class DAEMaterialCapacity : DAEBase
	{
		

		public DAEMaterialCapacity()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  DataTable GetRptData(string  sWhere)
		{
			string sSql = string.Empty;
			sSql = @"SELECT 
					b.MaxCapacity AS MaxNum ,
					b.MinCapacity AS MinNum, 
					ISNULL(a.QuantityInBin,0) AS CurrentNum,
					b.ItemCode, 
					b.WHID, 
					c.MaterialName 
					FROM dbo.MaxMinMaterial b Left JOIN 
					(SELECT SUM(QuantityInBin) AS QuantityInBin, WHID, ItemCode FROM WH_InStoreMaterialDetail GROUP BY WHID, ItemCode)
					a ON a.ItemCode = b.ItemCode AND a.WHID = b.WHID 
					Left JOIN dbo.Material c ON b.ItemCode = c.ItemCode";
			if(sWhere != null && sWhere.Length > 0)
			{
				sSql += " WHERE "+sWhere+"";
			}			
			DataTable dt = this.BaseDataAccess.GetDataTable(sSql);
			if ( dt.Rows.Count == 0 )
			{
				DataRow dr = dt.NewRow();
				dr["WHID"] = "0";
				dr["ItemCode"] = "0";
				dr["MaxNum"] = "0";
				dr["CurrentNum"] = "0";
				dr["MinNum"] = "0"; 
				dt.Rows.Add ( dr );
			}

			return dt;
		}
	}
}
