using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEMaxMinMaterialEdit ��ժҪ˵����
	/// </summary>
	public class DAEMaxMinMaterialEdit : DAEBase
	{
		public DAEMaxMinMaterialEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
