using System;
using Common;
using Cnwit.Utility;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEVendorMaterial 的摘要说明。
	/// </summary>
	public class DAEVendorMaterial : DAEBase
	{
		public DAEVendorMaterial()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		DataAcess _da = GetProjectDataAcess.GetDataAcess();

		public DataTable GetMaterialVendor(string sPKValue)
		{
			string sSql = "";
			if ( sPKValue != "" && sPKValue.Length > 0)
			{
				sSql = " SELECT WH_MaterialVendor.* FROM WH_MaterialVendor WHERE WH_MaterialVendor.VendorID = '"+sPKValue+"'";
			}
			else
			{
				sSql = " SELECT WH_MaterialVendor.* FROM WH_MaterialVendor WHERE 1 > 2";
			}
			DataTable dt = _da.GetDataTable ( sSql );
			return dt;
		}

		public string InsertMaterialVendor(string sItemCode,string sMaterialName,string sVendorID,string sComment,string sVendorName)
		{
			string errorMessage = "";
			string sSql = "insert into WH_MaterialVendor(MaterialVendorID,ItemCode,VendorID,MaterialName,Comment,VendorName) values ('"+System.Guid.NewGuid().ToString() +"',"+
				"'"+sItemCode+"','"+sVendorID+"','"+sMaterialName+"','"+sComment+"','"+sVendorName+"')";
			errorMessage = _da.ExecuteDMLSQL(sSql);
			return errorMessage;
		}

		/// <summary>
		/// 删除物资
		/// </summary>
		/// <param name="strIDKey"></param>
		public string DeleteMaterialVendor(string sItemCode)
		{
			string errorMessage = "";
			
			string sSql;
			if (sItemCode != null && sItemCode.Length > 0)
			{
				sSql = "delete WH_MaterialVendor where ItemCode ='"+sItemCode+"'";
			}
			else
			{
				sSql = "delete WH_MaterialVendor where 1 > 2 ";
			}
			errorMessage = _da.ExecuteDMLSQL(sSql);
			return errorMessage;
		}

		public DataTable GetMaterial( string sIDKey )
		{
			string strSql = "";
			if (sIDKey != null && sIDKey.Length > 0)
			{
				strSql = @"select * from WH_MaterialVendor where WH_MaterialVendor.VendorID = '"+sIDKey+"'";
			}
			else
			{
				strSql = @"select * from WH_MaterialVendor where 1 > 2 ";
			}
			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		public DataTable GetItemCode(string sItemCode)
		{
			string strSql = @"select * from Material where Material.ItemCode = '"+sItemCode+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
	}
}
