using System;
using System.Data;
using System.Data.SqlClient;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEPurchaseOrderEdit ��ժҪ˵����
	/// </summary>
	public class DAEPurchaseOrderEdit:DAEBase
	{
		DataAcess _da ;

		public DAEPurchaseOrderEdit()
		{
			_da = Common.GetProjectDataAcess.GetDataAcess();
		}

		//������ϵ�λ 
		public DataTable GetMaterialUOM(string strMaterialUomID)
		{
			string strSql =@"SELECT * FROM MaterialUOM WHERE MaterialUomID = '"+strMaterialUomID+"'";
			DataTable dtMaterialUOM = _da.GetDataTable(strSql);
			return dtMaterialUOM;
		}

		public int GetPOState ( string sPkValue , string sUserID )
		{
			string sSql = @"
				SELECT ApproveStatus FROM PurchaseOrder 
				WHERE
					POID = '" + sPkValue + @"' 
				AND
					CreateBy = '" + sUserID + "'" ;

			DataTable dtPOState = _da.GetDataTable(sSql) ;
			if ( dtPOState != null && dtPOState.Rows.Count > 0 )
			{
				if ( dtPOState.Rows[0]["ApproveStatus"] != System.DBNull.Value )
				{
					return Convert.ToInt32 ( dtPOState.Rows[0]["ApproveStatus"] ) ;
				}
				else
				{
					return 0 ;
				}
			}
			else
			{
				return 0 ;
			}
		}

		#region ����ItemCode�������������Ϣ

		public void UpdataDataTable_MaterialList ( DataTable dataTable )
		{
			string SelectSql = string.Empty;
			DataTable dt_Temp ;

			foreach ( DataRow dr in dataTable.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					// Modified By Liujun at 2007-7-30
					// ֮ǰ�汾�����ʱ��UOMID�����ϵ�λ�������������ʹ��ItemCode�������õ�Ψһ��λ

					SelectSql = @"select Material.ItemCode AS ItemCode,
								Material.MaterialName AS MaterialName,	
								MaterialUOM.MaterialUomID AS MaterialUomID,
								MaterialUOM.UOMID AS UOMID
								from 
								Material
								lEFT JOIN MaterialUOM ON Material.ItemCode = MaterialUOM.ItemCode
							WHERE
							 Material.ItemCode = '" + dr["ItemCode"].ToString() + "' AND IsBaseUom = 1" ;

					dt_Temp = _da.GetDataTable ( SelectSql );

					if ( dt_Temp.Rows.Count > 0 )
					{
						if ( dr["MaterialDescription"] != null )
						{ 
							dr["MaterialDescription"] = dt_Temp.Rows[0]["MaterialName"].ToString() ;
						}
						if ( dr["MaterialUomID"] != null )
						{ 
							dr["MaterialUomID"] = dt_Temp.Rows[0]["MaterialUomID"].ToString() ;
						}
						if ( dr["POMaterial__MaterialUomID"] != null )
						{ 
							dr["POMaterial__MaterialUomID"] = dt_Temp.Rows[0]["UOMID"].ToString() ;
						}
					}
				}
			}
		}
		







		#endregion


		//-----------------------add wudi  2007-7-20--------------------
		public string GetCountry(string strVendorID,string strCountryName )
		{
           string strname="";
           string strSql = " select " + strCountryName + " from Country " +
                           " Inner join Vendor   " +
                           " on  Country.CountyIDKey =Vendor.CountryID " +
                           " where  Vendor.IDKey='" + strVendorID + "' ";
           //string strSql =" select "+ strCountryName  +" from VendorCountry "+
           //                 " left join Country   "+
           //                 " on  Country.CountyIDKey =VendorCountry.CountryID "+
           //                 " where  VendorCountry.VendorID='"+strVendorID+"' "+
           //                 " and VendorCountry.State ='1'";
		  DataTable dt =_da.GetDataTable(strSql);

			if(dt != null && dt.Rows.Count != 0)
			{
				strname = dt.Rows[0][0].ToString();
			}
			return strname;
		}
	}
}
