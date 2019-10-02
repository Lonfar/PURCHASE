using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEAdjustOutEdit 的摘要说明。
	/// </summary>
	public class DAEAdjustOutEdit : DAEBase
	{
		/// <summary>
		/// 
		/// </summary>
		public DAEAdjustOutEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public void UpdateAdjustOutMaterial(DataTable dtAdjustOutMaterial,PriceType enWHPriceType)
		{
			foreach ( DataRow drAdjustOutMaterial in dtAdjustOutMaterial.Rows )
			{
				if (drAdjustOutMaterial.RowState != DataRowState.Deleted)
				{
					string sSql = @"SELECT WH_InStoreMaterialDetail.*,Material.MaterialName,MaterialUOM.MaterialUomID,QuantityReject From WH_InStoreMaterialDetail LEFT JOIN 
								Material on Material.ItemCode = WH_InStoreMaterialDetail.ItemCode LEFT JOIN  MaterialUOM 
								on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID  AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1
								LEFT JOIN WH_AdjustOutMaterial ON WH_AdjustOutMaterial.InStockMaterialID = WH_InStoreMaterialDetail.InStockMaterialID 
								WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+drAdjustOutMaterial["InStockMaterialID"].ToString()+"'";
					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{					
						//订单编号	
						drAdjustOutMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ; 
						drAdjustOutMaterial["WH_AdjustOutMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ; 
						//库存
						drAdjustOutMaterial["QuantityInBin"] = dtTempInfo.Rows[0]["QuantityInBin"] ;
						//库位	
						drAdjustOutMaterial["BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						drAdjustOutMaterial["WH_AdjustOutMaterial__BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						//制造编号
						drAdjustOutMaterial["PartNO"] = dtTempInfo.Rows[0]["PartNO"] ;
						drAdjustOutMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;
						//盘亏数量
						//					drAdjustOutMaterial["QuantityReject"] = dtTempInfo.Rows[0]["QuantityReject"] ;
						//单位	
						drAdjustOutMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drAdjustOutMaterial["WH_AdjustOutMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UomID"] ;
						if(enWHPriceType == PriceType.TYPE_PO)
						{
							//基本单位单价(本)	
							drAdjustOutMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"] ;
							//基本单位单价(核)	
							drAdjustOutMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"] ;	
						}
						else if(enWHPriceType == PriceType.TYPE_Average)
						{
							//基本单位单价(本)
							drAdjustOutMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"] ;
							//基本单位单价(核)	
							drAdjustOutMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["AveragePriceStandard"] ;

						}
						//物资名称
						drAdjustOutMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ;
					}
				}
			}
		}
		
		#region 在提交审核时进行校验( 报废数量是否大于库存数量 )

		/// <summary>
		/// 在提交审核时进行校验
		/// </summary>
		/// <param name="sReceiveID">转料单编号</param>
		/// <returns></returns>
		public string CheckNum ( string sAdjustOutID )
		{
			string sErrorMsg = string.Empty;

			string sSelectRejectMaterial = @"select WH_InStoreMaterialDetail.ItemCode,WH_InStoreMaterialDetail.InStockMaterialID,QuantityReject,
														WH_InStoreMaterialDetail.QuantityInBin 
														from WH_AdjustOutMaterial 
														left join WH_InStoreMaterialDetail on WH_AdjustOutMaterial.InStockMaterialID 
														= WH_InStoreMaterialDetail.InStockMaterialID
														where 
														WH_InStoreMaterialDetail.QuantityInBin - WH_AdjustOutMaterial.QuantityReject < 0 AND AdjustOutID = '"+sAdjustOutID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectRejectMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ 
				sErrorMsg = dt.Rows[0]["ItemCode"].ToString();
			}

			return sErrorMsg;
		}

		#endregion

		#region 更新报废单的状态

		public string UpdateAdjustOutState ( string sAdjustOutID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_AdjustOut SET Status = "+iState.ToString()+" WHERE AdjustOutID = '"+sAdjustOutID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			if(sErrorMsg.Length == 0)
			{			
				if(state == ApproveState.State_Approved)
				{
					string sSql = "SELECT a.* , b.WHID,b.AdjustOutNO,b.CreateBy FROM WH_AdjustOutMaterial a left join WH_AdjustOut b on a.AdjustOutID = b.AdjustOutID WHERE a.AdjustOutID = '"+sAdjustOutID+"'";
					DataTable dtRejectMaterial = this.BaseDataAccess.GetDataTable ( sSql );
					
					foreach(DataRow drRejectMaterial in dtRejectMaterial.Rows)
					{
						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
						//出库
						CInStoreMaterialDetail pInStore = new CInStoreMaterialDetail();
						pInStore.StoreOperateType = STOREOPERATETYPE.TYPE_OUT ;
						pInStore.OperateHistory = true;
						pInStore.InStockMaterialID = drRejectMaterial["InStockMaterialID"].ToString() ;
						pInStore.QuantityInBinSet  = decimal.Parse(drRejectMaterial["QuantityReject"].ToString()) ;
						pInStoreMaterialDetailAccess.OperateStore(pInStore);	
				
						//把数据写入财务接口
						CInterfaceOfFinanceAccess　　pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess() ;
						CInterfaceOfFinance pInterfaceOfFinance = new CInterfaceOfFinance();
						//库房
						pInterfaceOfFinance.Location = drRejectMaterial["WHID"].ToString() ;
						//物资编码
						pInterfaceOfFinance.ItemCode = drRejectMaterial["ItemCode"].ToString() ;
						//库位
						pInterfaceOfFinance.BinNo = drRejectMaterial["BINID"].ToString() ;
						//单据号
						pInterfaceOfFinance.BillNo = drRejectMaterial["AdjustOutNO"].ToString() ;
						//操作人
						pInterfaceOfFinance.Operater = drRejectMaterial["CreateBy"].ToString() ;
						//是出库还是入库
						pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_OUT ;
						//单据类型
						pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_AdjustOut) ;
						//基本单位的数量
						pInterfaceOfFinance.Quantity =  decimal.Parse(drRejectMaterial["QuantityReject"].ToString()) ;
						//核算单价
						pInterfaceOfFinance.UnitPriceStandard =  decimal.Parse(drRejectMaterial["UnitPriceStandard"].ToString()) ;

						pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;
					}
				}
			}
			return sErrorMsg;
		}

		#endregion
	}
}
