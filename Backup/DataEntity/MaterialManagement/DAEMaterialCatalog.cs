using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEMaterialCatalog 的摘要说明。
	/// </summary>
	public class DAEMaterialCatalog : DAEBase
	{

		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAEMaterialCatalog()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		public String GetDescription(String idStr)
		{
			String sqlStr = " Select CatalogDescription From MaterialCatalog Where CatalogID='" + idStr + "'"; 
			DataTable dt = _da.GetDataTable(sqlStr);
			if( dt != null && dt.Rows.Count > 0 ) return dt.Rows[0]["CatalogDescription"] == DBNull.Value ? "" : dt.Rows[0]["CatalogDescription"].ToString();
			return String.Empty ; 
		}

		

		public DataTable CatalogDeep()
		{
			string  strSql="Select CatalogID,CatalogDescription,ParentCatalog From MaterialCatalog Where CatalogDeep = 0 AND IsLeafCatalog = 0 ";
			DataTable dtGetDeep = _da.GetDataTable(strSql);
			return dtGetDeep;
		}


		public decimal GetDeep(string CatalogID)
		{
			DataTable dtGetDeep;
			decimal objReNum = 0;
			if(CatalogID != null)
			{
				if(CatalogID.Length != 0)
				{
					string  strSql="Select CatalogDeep From MaterialCatalog Where CatalogID ='"+CatalogID+"'";
					dtGetDeep = _da.GetDataTable(strSql);
				 
					if(dtGetDeep != null)
					{
						if(dtGetDeep.Rows.Count != 0)
						{
							objReNum = System.Convert.ToDecimal(dtGetDeep.Rows[0]["CatalogDeep"]);
							
						}

					}

				}
			
			}

			return objReNum ;
		}



		/// <summary>
		/// 检查是否有子节点
		/// </summary>
		/// <param name="sBinid"></param>
		/// <returns></returns>
		public bool CheckChildren(string sCatalogID)
		{
			bool check = false;

			if(sCatalogID != null)
			{
				if(sCatalogID.Length != 0)
				{
					string strSql ="Select CatalogID From MaterialCatalog Where  CatalogID != '"+sCatalogID+"' and CatalogID like '"+sCatalogID+"%'";
					DataTable dtChildren = Common.GetProjectDataAcess.GetDataAcess().GetDataTable(strSql);
					if(dtChildren != null)
					{
						if(dtChildren.Rows.Count != 0)
						{
							check = true;

						}

					}

				}

			}
			return check;

		}


	}
}
