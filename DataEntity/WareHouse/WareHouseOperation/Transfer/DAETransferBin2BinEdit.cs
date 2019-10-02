using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAETransferBin2BinEdit 的摘要说明。
	/// </summary>
	public class DAETransferBin2BinEdit : DAEBase
	{
		/// <summary>
		/// 
		/// </summary>
		public DAETransferBin2BinEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public void UpdateTransferBin2BinMaterial(DataTable dtTransferBin2BinMaterial,PriceType enWHPriceType,string sWHID)
		{
            //string BINID = GetBINIDFromWHID(sWHID); 缺省库位
			foreach ( DataRow drTransferBin2BinMaterial in dtTransferBin2BinMaterial.Rows )
			{
				if(drTransferBin2BinMaterial.RowState != DataRowState.Deleted)
				{
					string sSql = @"SELECT WH_InStoreMaterialDetail.*,MaterialUOM.MaterialUomID,Material.MaterialName,WH_InStoreMaterialDetail.QuantityInBin - WH_InStoreMaterialDetail.PreserveQuantity AS TransferQuantityInBin 
								From WH_InStoreMaterialDetail LEFT JOIN Material ON 
								Material.ItemCode = WH_InStoreMaterialDetail.ItemCode LEFT JOIN  MaterialUOM 
								on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID  AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1
								WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+drTransferBin2BinMaterial["InStockMaterialID"].ToString()+"'";
					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{
						//原库位	
						drTransferBin2BinMaterial["BINIDOld"] = dtTempInfo.Rows[0]["BINID"] ;
						drTransferBin2BinMaterial["WH_TransferBin2BinMaterial__BINIDOld"] = dtTempInfo.Rows[0]["BINID"] ;
						//原库存	
						//					drTransferBin2BinMaterial["TransferQuantityOld"] = dtTempInfo.Rows[0]["QuantityInBin"] ;
						drTransferBin2BinMaterial["TransferQuantityOld"] = dtTempInfo.Rows[0]["TransferQuantityInBin"];
						//单位	
						drTransferBin2BinMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drTransferBin2BinMaterial["WH_TransferBin2BinMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UOMID"] ;
						//订单编号	
						drTransferBin2BinMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ;
						drTransferBin2BinMaterial["WH_TransferBin2BinMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ;

                        //drTransferBin2BinMaterial["WH_TransferBin2BinMaterial__BINIDNew"] = BINID;
                        //drTransferBin2BinMaterial["BINIDNew"] = BINID;

						if(enWHPriceType == PriceType.TYPE_PO)
						{
							//基本单位单价(本)
							drTransferBin2BinMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"] ;
							//基本单位单价(核)	
							drTransferBin2BinMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"] ;
						}
						else if(enWHPriceType == PriceType.TYPE_Average)
						{
							//基本单位单价(本)
							drTransferBin2BinMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"] ;
							//基本单位单价(核)	
							drTransferBin2BinMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["AveragePriceStandard"] ;

						}

						drTransferBin2BinMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;
						//物资名称
						drTransferBin2BinMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ;
					}
				}
			}
		}

		private string GetBINIDFromWHID(string sWHID)
		{
			string sSql = "SELECT BINID FROM WH_BI_BIN WHERE Status='1' AND WHID = '"+sWHID+"'";
			return this.BaseDataAccess.GetDataTable ( sSql ).Rows[0][0].ToString();			
		}

		public void CalTotalAmount(DataTable dtTransferQuantity ,ref  decimal decTotalAmountStandard,ref decimal decTotalAmountNatural)
		{
			foreach(DataRow drTransferQuantity in dtTransferQuantity.Rows)
			{
				if(drTransferQuantity.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(drTransferQuantity["WH_TransferBin2BinMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(drTransferQuantity["WH_TransferBin2BinMaterial.UnitPriceNatural"].ToString());
					decimal decTransferQuantity =  Convert.ToDecimal(drTransferQuantity["WH_TransferBin2BinMaterial.TransferQuantity"].ToString());
					decTotalAmountStandard += decUnitPriceStandard * decTransferQuantity;
					decTotalAmountNatural += decUnitPriceNatural * decTransferQuantity ;
				}
			}
		}

		#region 在提交审核时进行校验( 实收数量是否大于可收数量 )

		/// <summary>
		/// 在提交审核时进行校验
		/// </summary>
		/// <param name="sReceiveID">转料单编号</param>
		/// <returns></returns>
		public string CheckNum ( string sTransferBin2BinID )
		{
			string sErrorMsg = string.Empty;

			string sSelectTransferBin2BinMaterial = @"select WH_InStoreMaterialDetail.ItemCode,WH_InStoreMaterialDetail.InStockMaterialID,TransferQuantity,
														WH_InStoreMaterialDetail.QuantityInBin 
														from WH_TransferBin2BinMaterial 
														left join WH_InStoreMaterialDetail on WH_TransferBin2BinMaterial.InStockMaterialID 
														= WH_InStoreMaterialDetail.InStockMaterialID
														where 
														QuantityInBin - TransferQuantity <0 AND TransferBin2BinID = '"+sTransferBin2BinID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectTransferBin2BinMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ 
				sErrorMsg = dt.Rows[0]["ItemCode"].ToString();
			}

			return sErrorMsg;
		}

		#endregion


		#region 更新转料单的状态

		public string UpdateTransferBin2BinState ( string sTransferBin2BinID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_TransferBin2Bin SET Status = "+iState.ToString()+" WHERE TransferBin2BinID = '"+sTransferBin2BinID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			if(sErrorMsg.Length == 0)
			{			
				if(state == ApproveState.State_Approved)
				{
					string sSql = "SELECT a.* , b.WHID,c.VendorID,c.MFG,c.PartNo,c.Comment FROM WH_TransferBin2BinMaterial a left join WH_TransferBin2Bin b on a.TransferBin2BinID = b.TransferBin2BinID join WH_InStoreMaterialDetail c on c.InStockMaterialID = a.InStockMaterialID WHERE a.TransferBin2BinID = '"+sTransferBin2BinID+"'";
					DataTable dtTransferBin2BinMaterial = this.BaseDataAccess.GetDataTable ( sSql );
					
					foreach(DataRow drTransferBin2BinMaterial in dtTransferBin2BinMaterial.Rows)
					{
						//出库
						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
						CInStoreMaterialDetail pOutStore = new CInStoreMaterialDetail();
						pOutStore.StoreOperateType = STOREOPERATETYPE.TYPE_OUT ;
						pOutStore.OperateHistory = false;
						pOutStore.InStockMaterialID = drTransferBin2BinMaterial["InStockMaterialID"].ToString() ;
						pOutStore.QuantityInBinSet  = Decimal.Parse(drTransferBin2BinMaterial["TransferQuantity"].ToString()) ;
						pInStoreMaterialDetailAccess.OperateStore(pOutStore);

						//入库
						CInStoreMaterialDetail pInStore = new CInStoreMaterialDetail();
						pInStore.StoreOperateType = STOREOPERATETYPE.TYPE_IN ;
						pInStore.OperateHistory = false;
						pInStore.BINID = drTransferBin2BinMaterial["BINIDNew"].ToString() ;
						pInStore.ItemCode = drTransferBin2BinMaterial["ItemCode"].ToString() ;
						pInStore.POID = drTransferBin2BinMaterial["POID"].ToString() ;
						pInStore.WHID = drTransferBin2BinMaterial["WHID"].ToString() ;;
						pInStore.UnitPricePOStandard = Decimal.Parse(drTransferBin2BinMaterial["UnitPriceStandard"].ToString()) ;
						pInStore.UnitPricePONatural = Decimal.Parse(drTransferBin2BinMaterial["UnitPriceNatural"].ToString()) ;
						pInStore.VendorID  = drTransferBin2BinMaterial["VendorID"].ToString() ;
						pInStore.MFG  = drTransferBin2BinMaterial["MFG"].ToString() ;
						pInStore.PartNo  = drTransferBin2BinMaterial["PartNo"].ToString() ;
						pInStore.Comment  = drTransferBin2BinMaterial["Comment"].ToString() ;
						pInStore.QuantityInBinSet  = Decimal.Parse(drTransferBin2BinMaterial["TransferQuantity"].ToString()) ;
						pInStoreMaterialDetailAccess.OperateStore(pInStore);
					
					}
				}
			}
			return sErrorMsg;
		}

		#endregion
	}
}
