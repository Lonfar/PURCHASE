using System;
using Common;
using Cnwit.Utility;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEMaterialCode ��ժҪ˵����
	/// </summary>
	public class DAEMaterialCode : DAEBase
	{
		DataAcess da = Common.GetProjectDataAcess.GetDataAcess();

		/// <summary>
		/// 
		/// </summary>
		public DAEMaterialCode()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sCatalogID"></param>
		/// <returns></returns>
		public bool IsLeafCatalog(string sCatalogID)
		{
			string sSql = "SELECT IsLeafCatalog FROM MaterialCatalog where CatalogID='" + sCatalogID +"'";
			DataTable dtIsLeafCatalog  = da.GetDataTable(sSql);
			if(dtIsLeafCatalog != null)
			{
				if(dtIsLeafCatalog.Rows.Count != 0)
				{
					if (dtIsLeafCatalog.Rows[0]["IsLeafCatalog"] != System.DBNull.Value &&  dtIsLeafCatalog.Rows[0]["IsLeafCatalog"].ToString().ToUpper() == "TRUE")
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
	}
}
