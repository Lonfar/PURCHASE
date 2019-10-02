using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEMaterialCodeEdit 的摘要说明。
	/// </summary>
	public class DAEMaterialCodeEdit : DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();

		public DAEMaterialCodeEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		
		public DataTable GetMaterialInfo ( string sItemCode )
		{
			string sSql = @"
				SELECT
					Material.* , 
					MaterialUOM.MaterialUomID , 
					WH_BT_PriceType.TypeDescription , 
					MaterialCatalog.CatalogDescription
				FROM
					Material left join MaterialUOM
					on Material.ItemCode = MaterialUOM.ItemCode
					left join WH_BT_PriceType
					on Material.IDKey = WH_BT_PriceType.IDKey
					left join MaterialCatalog
					on Material.CatalogID = MaterialCatalog.CatalogID
				WHERE
					Material.ItemCode = '"+ sItemCode +"'" ;

			return _da.GetDataTable(sSql);
		}

		public void UpdataMaterialVendor ( DataTable dtMaterialVendor)
		{
			foreach ( DataRow drMaterialVendor in dtMaterialVendor.Rows )
			{
				if (drMaterialVendor.RowState != DataRowState.Deleted)
				{
					string sSql = @"SELECT  Vendor.* 
									FROM    Vendor 
				                    WHERE Vendor.IDKey = '"+drMaterialVendor["VendorID"].ToString()+"'";
					DataTable  dtTempInfo = _da.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{		
						drMaterialVendor["VendorNO"] = dtTempInfo.Rows[0]["VendorNO"] ;  
						drMaterialVendor["VendorName"] = dtTempInfo.Rows[0]["VendorName"] ; 					   					
					}
				}
			}
		}
	}
}
