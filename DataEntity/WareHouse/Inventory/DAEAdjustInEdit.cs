using System;
using System.Data;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEAdjustInEdit 的摘要说明。
	/// </summary>
	public class DAEAdjustInEdit : DAEBase
	{
		Cnwit.Utility.DataAcess _da ;

		public DAEAdjustInEdit()
		{
			_da = Common.GetProjectDataAcess.GetDataAcess() ;
		}

		#region 根据ItemCode获得物料其他信息

		#region UpdataDataTable_MaterialList

		public int UpdataDataTable_MaterialList ( DataTable dataTable , int nPOSeed,string sWHID)
		{
			string SelectSql = string.Empty;
			DataTable dt_Temp ;
			string BINID = GetBINIDFromWHID(sWHID);

			foreach ( DataRow dr in dataTable.Rows )
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					SelectSql = @"
						SELECT
							Material.ItemCode , 
							Material.MaterialName , 
							MaterialUOM.UOMID , 
							MaterialUOM.MaterialUomID
						From
							Material inner join MaterialUOM	on Material.ItemCode = MaterialUOM.ItemCode 
						WHERE
							Material.ItemCode = '" + dr["ItemCode"].ToString() + "'" ;

					dt_Temp = _da.GetDataTable ( SelectSql );

					if ( dt_Temp.Rows.Count > 0 )
					{
						dr["BINID"] = BINID;
						dr["WH_AdjustInMaterial__BINID"] = BINID;
						dr["WH_AdjustInMaterial__ItemCode"] = dr["ItemCode"] ;
						dr["MaterialName"] = dt_Temp.Rows[0]["MaterialName"] ; 
						dr["MaterialUomID"] = dt_Temp.Rows[0]["MaterialUomID"] ;
						dr["WH_AdjustInMaterial__MaterialUomID"] = dt_Temp.Rows[0]["UOMID"] ;
						if ( dr["RowStatus"].ToString() == "NEW" )
						{
							if ( dr["POID"].ToString().Length == 0 )
							{
								dr["POID"] = CreatePOID ( nPOSeed ) ;
								nPOSeed ++ ;
							}
						}
					}
				}
			}
//			dataTable.AcceptChanges();

			return nPOSeed ;
		}

		private string GetBINIDFromWHID(string sWHID)
		{
			string sSql = "SELECT BINID FROM WH_BI_BIN WHERE Status='1' AND WHID = '"+sWHID+"'";
			return this.BaseDataAccess.GetDataTable ( sSql ).Rows[0][0].ToString();			
		}

		#endregion

		#region UpdataDataTable_InventoryList

		public int UpdataDataTable_InventoryList ( DataTable dataTable , int nPOSeed )
		{
			string SelectSql = string.Empty;
			DataTable dt_Temp ;

			foreach ( DataRow dr in dataTable.Rows )
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					SelectSql = @"
						SELECT WH_InStoreMaterialDetail.ItemCode , WH_InStoreMaterialDetail.BINID , 
						WH_InStoreMaterialDetail.UnitPricePONatural , WH_InStoreMaterialDetail.UnitPricePOStandard , 
						WH_InStoreMaterialDetail.QuantityInBin , Material.MaterialName , MaterialUOM.UOMID ,MaterialUOM.MaterialUomID 
						From WH_InStoreMaterialDetail 
						inner join MaterialUOM on WH_InStoreMaterialDetail.UOMID = MaterialUOM.UOMID 
						AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1  
						inner join Material on WH_InStoreMaterialDetail.ItemCode = Material.ItemCode 
						WHERE WH_InStoreMaterialDetail.InStockMaterialID = '" + dr["InStockMaterialID"].ToString() + "'" ;

					dt_Temp = _da.GetDataTable ( SelectSql );

					if ( dt_Temp.Rows.Count > 0 )
					{
						dr["WH_AdjustInMaterial__InStockMaterialID"] = dr["InStockMaterialID"] ;
						dr["MaterialName"] = dt_Temp.Rows[0]["MaterialName"] ; 
						dr["ItemCode"] = dt_Temp.Rows[0]["ItemCode"] ;
						dr["BINID"] = dt_Temp.Rows[0]["BINID"] ;
						dr["WH_AdjustInMaterial__BINID"] = dt_Temp.Rows[0]["BINID"] ;
						dr["MaterialUomID"] = dt_Temp.Rows[0]["MaterialUomID"] ;
						dr["WH_AdjustInMaterial__MaterialUomID"] = dt_Temp.Rows[0]["UOMID"] ;
						dr["UnitPriceNatural"] = Double.Parse(dt_Temp.Rows[0]["UnitPricePONatural"].ToString()).ToString("f2");
						dr["UnitPriceStandard"] = Double.Parse(dt_Temp.Rows[0]["UnitPricePOStandard"].ToString()).ToString("f2");
						dr["QuantityInBin"] = Double.Parse(dt_Temp.Rows[0]["QuantityInBin"].ToString());
						if ( dr["POID"] != null )
						{ 
							if ( dr["RowStatus"].ToString() == "NEW" )
							{
								if ( dr["POID"].ToString().Length == 0 )
								{
									dr["POID"] = CreatePOID ( nPOSeed ) ;
									nPOSeed ++ ;
								}
							}
						}
					}
				}
			}
//			dataTable.AcceptChanges();

			return nPOSeed ;
		}

		#endregion

		#endregion

		#region Create POID

		private string CreatePOID ( int nPOSeed )
		{
			string sLastPOID = GetLastPOID () ;
			// Style: AJIN 00001
			int nNewNo = 1 + Convert.ToInt32 ( sLastPOID.Substring ( 5 ) ) + nPOSeed ;
			return String.Format ( "AJIN {0:D5}" , nNewNo ) ;
		}

		#endregion

		#region Get Last POID

		public string GetLastPOID ()
		{
			string sSql = @"SELECT Top 1 POID FROM WH_AdjustInMaterial ORDER BY	POID DESC" ;

			DataTable dt = _da.GetDataTable ( sSql ) ;

			if ( dt.Rows.Count > 0 )
			{
				return dt.Rows[0]["POID"].ToString () ;
			}
			else
			{
				return "AJIN 00000" ;
			}
		}

		#endregion

		#region Update AdjustIn State

		public string UpdateAdjustInState ( string sAdjustInID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_AdjustIN SET Status = " + 
				iState.ToString()+" WHERE AdjustInID = '"+sAdjustInID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );

			if ( sErrorMsg.Length == 0 )
			{
				if ( state == ApproveState.State_Approved )
				{
					string sSql = @"
							SELECT
								WH_AdjustInMaterial.ItemCode ,
								WH_AdjustInMaterial.POID , 
								WH_AdjustInMaterial.BINID , 
								WH_AdjustInMaterial.UnitPriceNatural , 
								WH_AdjustInMaterial.UnitPriceStandard , 
								WH_AdjustInMaterial.AdjustInQuantity , 
								WH_InStoreMaterialDetail.VendorID,
								WH_AdjustIN.WHID,WH_AdjustIN.CreateBy,  WH_AdjustIN.AdjustInNO  
							FROM
								WH_AdjustIN inner join WH_AdjustInMaterial
								on WH_AdjustIN.AdjustInID = WH_AdjustInMaterial.AdjustInID
								LEFT JOIN WH_InStoreMaterialDetail ON WH_InStoreMaterialDetail.InStockMaterialID = WH_AdjustInMaterial.InStockMaterialID
							WHERE
								WH_AdjustInMaterial.AdjustInID = '" + sAdjustInID + "'" ;

					DataTable dt = _da.GetDataTable ( sSql ) ;

					foreach ( DataRow dr in dt.Rows )
					{
						//入库
						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
						CInStoreMaterialDetail pInStore = new CInStoreMaterialDetail();
						pInStore.StoreOperateType = STOREOPERATETYPE.TYPE_IN ;
						pInStore.OperateHistory = true;
						pInStore.BINID = dr["BINID"].ToString() ;
						pInStore.ItemCode = dr["ItemCode"].ToString() ;
						pInStore.POID = dr["POID"].ToString() ;
						pInStore.WHID = dr["WHID"].ToString() ;
						pInStore.VendorID = dr["VendorID"].ToString() ;
						decimal dUnitPriceNatural = Decimal.Parse ( dr["UnitPriceNatural"].ToString() ) ;
						decimal dUnitPriceStandard = Decimal.Parse ( dr["UnitPriceStandard"].ToString() ) ;
						pInStore.UnitPricePONatural = dUnitPriceNatural ;
						pInStore.UnitPricePOStandard = dUnitPriceStandard ;
						pInStore.QuantityInBinSet = Decimal.Parse(dr["AdjustInQuantity"].ToString()) ;
						pInStoreMaterialDetailAccess.OperateStore(pInStore);

						//把数据写入财务接口
						CInterfaceOfFinanceAccess　　pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess() ;
						CInterfaceOfFinance pInterfaceOfFinance = new CInterfaceOfFinance();
						//库房
						pInterfaceOfFinance.Location = dr["WHID"].ToString() ;
						//物资编码
						pInterfaceOfFinance.ItemCode = dr["ItemCode"].ToString() ;
						//库位
						pInterfaceOfFinance.BinNo = dr["BINID"].ToString() ;
						//单据号
						pInterfaceOfFinance.BillNo = dr["AdjustInNO"].ToString() ;
						//操作人
						pInterfaceOfFinance.Operater = dr["CreateBy"].ToString() ;
						//是出库还是入库
						pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_IN ;
						//单据类型
						pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_AdjustIN) ;
						//基本单位的数量
						pInterfaceOfFinance.Quantity =  decimal.Parse(dr["AdjustInQuantity"].ToString()) ;
						//核算单价
						pInterfaceOfFinance.UnitPriceStandard =  decimal.Parse(dr["UnitPriceStandard"].ToString()) ;

						pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;
					}
				}
			}

			return sErrorMsg;
		}

		#endregion

		#region Get Material's Bins in the WareHouse

		public string GetMaterialBins ( string sWHID )
		{
			string sSql = @"
				SELECT   WH_BI_BIN.BINID 
				FROM   WH_BI_BIN 
				WHERE   WH_BI_BIN.WHID =  '" + sWHID + "'";
			DataTable dt = _da.GetDataTable ( sSql ) ;
			int nCount = dt.Rows.Count ;
			string sResult = String.Empty ;

			if ( nCount > 0 )
			{
				for ( int i = 0 ; i < nCount ; i ++ )
				{
					sResult += ",'" + dt.Rows[i]["BINID"].ToString() + "'" ;
				}
				sResult = sResult.Substring ( 1 ) ;
			}

			return sResult ;
		}

		#endregion
	}
}
