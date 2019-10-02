using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEMaxMinMaterialEdit 的摘要说明。
	/// </summary>
	public class DAEMaxMinMaterialEdit : DAEBase
	{
		public DAEMaxMinMaterialEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetMaterialInfos ( string sItemCode )
		{
			string strSql = @"select MaterialName , UOMID from Material 
				where ItemCode = '"+sItemCode+"'";

			Cnwit.Utility.DataAcess da = Common.GetProjectDataAcess.GetDataAcess();
			return da.GetDataTable ( strSql ) ;
		}

		public int ExistsItemCode ( string sItemCode, string sWHID )
		{
			string strSql = @"select ItemCode from MaxMinMaterial 
				where ItemCode = '"+sItemCode+"' and WHID='"+sWHID+"'";

			Cnwit.Utility.DataAcess da = Common.GetProjectDataAcess.GetDataAcess();
			DataTable dt = da.GetDataTable ( strSql );
			return dt.Rows.Count;
		}
	}
}
