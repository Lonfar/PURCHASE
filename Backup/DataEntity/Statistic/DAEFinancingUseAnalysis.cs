using System;
using System.Data;

namespace DataEntity.Statistic
{
	/// <summary>
	///资金占用分析报表数据实体类 Added by Liujun at 2007-7-6
	/// </summary>
	public class DAEFinancingUseAnalysis : DAEBase
	{
		/// <summary>
		/// 物资使用分析报表数据实体类
		/// </summary>
		public DAEFinancingUseAnalysis()
		{

		}

		/// <summary>
		/// 检索数据
		/// </summary>
		/// <param name="sWhereSql">筛选条件</param>
		/// <returns>对应数据表</returns>
		public DataTable GetRptData ( string sWhereSql )
		{
			if ( sWhereSql.Length > 0 )
			{
				sWhereSql = " WHERE " +sWhereSql;
			}

			DataTable dtData;

			string sSelectSql = @"SELECT MAX(t.WHID) AS WHID , t.yearmonth , SUM (TotalAmount) AS TotalAmount FROM
										(SELECT a.WHID,a.ItemCode,a.StockDate,a.yearmonth,(b.AvePriceStandard*b.StockTotal) AS TotalAmount FROM 	
											(SELECT WHID,ItemCode,CONVERT(nvarchar(7), StockDate, 120) AS yearmonth,MAX(StockDate) AS StockDate FROM
											WH_InStockMaterialHistory "+sWhereSql+@"
											GROUP BY WHID,ItemCode,CONVERT(nvarchar(7), StockDate, 120)
											) a 
											JOIN WH_InStockMaterialHistory b
												ON a.WHID = b.WHID	and 
												a.ItemCode =b.ItemCode	and
												a.StockDate = b.StockDate 
										) t GROUP BY yearmonth";

			dtData = this.BaseDataAccess.GetDataTable ( sSelectSql );

			return dtData;
		}

		/// <summary>
		/// 获得报表数据
		/// </summary>
		/// <param name="sWHID">库房ID</param>
		/// <param name="sItemCode">ItemCode</param>
		/// <param name="sYear">计算年份</param>
		/// <returns></returns>
		public DataTable GetRptData ( string sWHID , string sItemCode , string sYear )
		{
			DataTable dtData = new DataTable();
			string sSelectWHID = "SELECT DISTINCT WHID FROM WH_InStockMaterialHistory ";
			string sSelectItemCode = "SELECT DISTINCT ItemCode FROM WH_InStockMaterialHistory ";
			DataTable dtTemp;

			string [] sParms = {"WHID","ItemCode","YearOrMonth"};
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar};

			// 选物资，选库房
			if ( sItemCode.Length != 0  && sWHID.Length != 0)
			{
		
				object[] objParamValues = {sWHID,sItemCode,sYear};
		
				dtData = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );
			}
			
			// 不选物资，不选库房
			if ( sItemCode.Length == 0  && sWHID.Length == 0)
			{
				DataTable dtWHID = this.BaseDataAccess.GetDataTable( sSelectWHID );
				DataTable dtItemCode = this.BaseDataAccess.GetDataTable( sSelectItemCode );

				foreach ( DataRow drWHID in dtWHID.Rows )
				{
					foreach ( DataRow drItemCode in dtItemCode.Rows )
					{
						object[] objParamValues = {drWHID["WHID"],drItemCode["ItemCode"],sYear};

						if ( dtData.Rows.Count == 0 )
						{
							dtData = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );
						}
						else
						{
							dtTemp = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );

							for ( int i = 0 ; i < dtTemp.Rows.Count ; i ++ )
							{
								dtData.Rows[i]["TotalAmount"] = Convert.ToDecimal( dtTemp.Rows[i]["TotalAmount"] ) + Convert.ToDecimal( dtData.Rows[i]["TotalAmount"] );
							}
						}
					}
				}
			}

			// 选物资，不选库房
			if ( sItemCode.Length != 0  && sWHID.Length == 0)
			{
				DataTable dtWHID = this.BaseDataAccess.GetDataTable( sSelectWHID );
				

				foreach ( DataRow dr in dtWHID.Rows )
				{
					
					object[] objParamValues = {dr["WHID"],sItemCode,sYear};

					if ( dtData.Rows.Count == 0 )
					{
						dtData = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );
					}
					else
					{
						dtTemp = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );

						for ( int i = 0 ; i < dtTemp.Rows.Count ; i ++ )
						{
							dtData.Rows[i]["TotalAmount"] = Convert.ToDecimal( dtTemp.Rows[i]["TotalAmount"] ) + Convert.ToDecimal( dtData.Rows[i]["TotalAmount"] );
						}
					}
				}
			}

			// 不选物资，选库房
			if ( sItemCode.Length == 0  && sWHID.Length != 0)
			{
				DataTable dtItemCode = this.BaseDataAccess.GetDataTable( sSelectItemCode );

				foreach ( DataRow dr in dtItemCode.Rows )
				{
					object[] objParamValues = {sWHID,dr["ItemCode"],sYear};

					if ( dtData.Rows.Count == 0 )
					{
						dtData = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );
					}
					else
					{
						dtTemp = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );

						for ( int i = 0 ; i < dtTemp.Rows.Count ; i ++ )
						{
							dtData.Rows[i]["TotalAmount"] = Convert.ToDecimal( dtTemp.Rows[i]["TotalAmount"] ) + Convert.ToDecimal( dtData.Rows[i]["TotalAmount"] );
						}
					}
				}
			}

			return dtData;
		}

		public DataTable GetRptData_ImportantMaterial ( string sWHID , string sCatalogID ,string sItemCode , string sYear )
		{
			DataTable dtData = new DataTable();
			string [] sParms = {"WHID","ItemCode","YearOrMonth"};
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar};
			DataTable dtTemp;

			// 选库房，不选类别，不选物资
			if (  sItemCode.Length == 0 && sWHID.Length != 0 && sCatalogID.Length == 0 )
			{
				string sSelectSql = "SELECT ItemCode FROM V_ImportantMaterial WHERE WHID = '"+sWHID+"'";

				DataTable dtImportantMaterial = this.BaseDataAccess.GetDataTable ( sSelectSql ); 

				foreach ( DataRow dr in dtImportantMaterial.Rows )
				{
					object[] objParamValues = { sWHID,dr["ItemCode"],sYear};

					if ( dtData.Rows.Count == 0 )
					{
						dtData = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );
					}
					else
					{
						dtTemp = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );

						for ( int i = 0 ; i < dtTemp.Rows.Count ; i ++ )
						{
							dtData.Rows[i]["TotalAmount"] = Convert.ToDecimal( dtTemp.Rows[i]["TotalAmount"] ) + Convert.ToDecimal( dtData.Rows[i]["TotalAmount"] );
						}
					}
				}
			}

			// 选库房，选类别，不选物资
			if (  sItemCode.Length == 0 && sWHID.Length != 0 && sCatalogID.Length != 0 )
			{
				string sSelectSql = "SELECT ItemCode FROM V_ImportantMaterial WHERE WHID = '"+sWHID+"' AND CatalogID = '"+sCatalogID+"'";

				DataTable dtImportantMaterial = this.BaseDataAccess.GetDataTable ( sSelectSql ); 

				foreach ( DataRow dr in dtImportantMaterial.Rows )
				{
					object[] objParamValues = { sWHID,dr["ItemCode"],sYear};

					if ( dtData.Rows.Count == 0 )
					{
						dtData = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );
					}
					else
					{
						dtTemp = this.BaseDataAccess.ExecuteSPQueryDataTable( "spFinancingUseAnalysis" , sParms , objParamValues , paramTypes );

						for ( int i = 0 ; i < dtTemp.Rows.Count ; i ++ )
						{
							dtData.Rows[i]["TotalAmount"] = Convert.ToDecimal( dtTemp.Rows[i]["TotalAmount"] ) + Convert.ToDecimal( dtData.Rows[i]["TotalAmount"] );
						}
					}
				}
			}

			return dtData;
		}
	}
}
