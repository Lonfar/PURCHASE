/*
 * Create nongbin 2007-06-28
 * 
 * 关于库间转料的页面
 * */
using System;
using Common;
using Cnwit.Utility;
using System.Data;
using System.Data.SqlClient;


namespace DataEntity
{
	/// <summary>
	/// DAETransferWH2WHEdit 的摘要说明。
	/// </summary>
	public class DAETransferWH2WHEdit:DAEBase
	{
		
		DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();


		public DAETransferWH2WHEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		#region	填充数据到子表DataTable

		/// <summary>
		/// 填充数据到子表DataTable
		/// </summary>
		/// <param name="dtTransferWH2WHEdit"></param>
		public void UpdateWH_TransferWH2WHMaterial(DataTable dtTransferWH2WHEdit,PriceType enWHPriceType,string sWHID)
		{
			string sql = "";
			string BINID = GetBINIDFromWHID(sWHID);
			
			foreach ( DataRow drTransferWH2WHEdit in dtTransferWH2WHEdit.Rows )
			{
				if(drTransferWH2WHEdit.RowState != DataRowState.Deleted)
				{
					sql = @"SELECT WH_InStoreMaterialDetail.*,MaterialUOM.MaterialUomID,Material.MaterialName,WH_InStoreMaterialDetail.QuantityInBin - WH_InStoreMaterialDetail.PreserveQuantity AS TransferQuantityInBin 
                        From WH_InStoreMaterialDetail LEFT JOIN  MaterialUOM 
						on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID  AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1
						left join Material on WH_InStoreMaterialDetail.ItemCode = Material.ItemCode
						WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+drTransferWH2WHEdit["InStockMaterialID"].ToString()+"'";
			
							
					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sql);
							
					if (dtTempInfo.Rows.Count > 0 )
					{
						//物料单位
						drTransferWH2WHEdit["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drTransferWH2WHEdit["WH_TransferWH2WHMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UOMID"] ;
			
						//订单编号	
						drTransferWH2WHEdit["POID"] = dtTempInfo.Rows[0]["POID"] ; 
						drTransferWH2WHEdit["WH_TransferWH2WHMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ; 

						drTransferWH2WHEdit["WH_TransferWH2WHMaterial__BINIDNew"] = BINID;
						drTransferWH2WHEdit["BINIDNew"] = BINID;
								
						if(enWHPriceType == PriceType.TYPE_PO)
						{
							//单价对本位币值
							drTransferWH2WHEdit["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"] ; 
								
							//单价对核算币值
							drTransferWH2WHEdit["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"] ;
						}
						else if(enWHPriceType == PriceType.TYPE_Average)
						{
							//基本单位单价(本)
							drTransferWH2WHEdit["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"] ;
							//基本单位单价(核)	
							drTransferWH2WHEdit["UnitPriceStandard"] = dtTempInfo.Rows[0]["AveragePriceStandard"] ;

						}
			
						//原库位
						drTransferWH2WHEdit["BINIDOld"] = dtTempInfo.Rows[0]["BINID"] ;
						drTransferWH2WHEdit["WH_TransferWH2WHMaterial__BINIDOld"] = dtTempInfo.Rows[0]["BINID"] ;
			
						//原库存  
						//					drTransferWH2WHEdit["TransferQuantity"] = dtTempInfo.Rows[0]["QuantityInBin"] ;
						drTransferWH2WHEdit["QuantityInOldBin"] = dtTempInfo.Rows[0]["TransferQuantityInBin"] ;

						drTransferWH2WHEdit["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;

						drTransferWH2WHEdit["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ;
			
					}
				}
			}
		}

		private string GetBINIDFromWHID(string sWHID)
		{
			string sSql = "SELECT BINID FROM WH_BI_BIN WHERE Status='1' AND WHID = '"+sWHID+"'";
			return this.BaseDataAccess.GetDataTable ( sSql ).Rows[0][0].ToString();			
		}

		#endregion

		public void CalTotalAmount(DataTable dtTransferQuantity ,ref  decimal decTotalAmountStandard,ref decimal decTotalAmountNatural)
		{
			foreach(DataRow drTransferQuantity in dtTransferQuantity.Rows)
			{
				if(drTransferQuantity.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(drTransferQuantity["WH_TransferWH2WHMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(drTransferQuantity["WH_TransferWH2WHMaterial.UnitPriceNatural"].ToString());
					decimal decTransferQuantity =  Convert.ToDecimal(drTransferQuantity["WH_TransferWH2WHMaterial.TransferQuantity"].ToString());
					decTotalAmountStandard += decUnitPriceStandard * decTransferQuantity;
					decTotalAmountNatural += decUnitPriceNatural * decTransferQuantity ;
				}
			}
		}

		
		#region	从库存物资明细表取得库位数量(QuantityInBin)

		public DataTable CheckIsTransferQuantity(string sql)
		{
			DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sql);
			return dtTempInfo;
		}

		#endregion

		
		#region	更新库间转料状态

		/// <summary>
		/// 更新库间转料状态
		/// </summary>
		/// <param name="PkValue"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public string UpdateWH_TransferWH2WHState ( string PkValue , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_TransferWH2WH SET Status = "+iState.ToString()+" WHERE TransferWH2WHID = '"+PkValue+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );

			if ( sErrorMsg .Length == 0 )
			{
				switch ( state )
				{
					case ApproveState.State_Approved :
					{
						string sql = @"SELECT a.* , b.* ,c.VendorID,d.PartNo,d.MFG
							FROM 
		WH_TransferWH2WHMaterial a 
		left join WH_TransferWH2WH b on a.TransferWH2WHID = b.TransferWH2WHID 
		left join PurchaseOrder c on a.POID = c.POID 
		left join Material as d on d.ItemCode = a.ItemCode
		WHERE a.TransferWH2WHID = '"+PkValue+"'";

						DataTable dt = this.BaseDataAccess.GetDataTable ( sql );
						
						#region						
						
						for(int i=0;i<dt.Rows.Count;i++)
						{
							//出库
										// 构造库存操作实体
							DataEntity.CInStoreMaterialDetail Out_entity = new DataEntity.CInStoreMaterialDetail();
										// 调用库存操作连接类
							DataEntity.CInStoreMaterialDetailAccess storeAccess = new CInStoreMaterialDetailAccess();

							Out_entity.StoreOperateType = DataEntity.STOREOPERATETYPE.TYPE_OUT;
							Out_entity.BINID = dt.Rows[i]["BINIDOld"].ToString();
							Out_entity.InStockMaterialID = dt.Rows[i]["InStockMaterialID"].ToString();;
							Out_entity.ItemCode = dt.Rows[i]["ItemCode"].ToString();
							Out_entity.POID = dt.Rows[i]["POID"].ToString();
							Out_entity.QuantityInBinSet = Decimal.Parse(dt.Rows[i]["TransferQuantity"].ToString());//库位数量 = 原库位数量 - 转料数量
							Out_entity.UnitPricePONatural = Decimal.Parse(dt.Rows[i]["UnitPriceNatural"].ToString());
							Out_entity.UnitPricePOStandard = Decimal.Parse(dt.Rows[i]["UnitPriceStandard"].ToString());
							Out_entity.WHID = dt.Rows[i]["WHIDOld"].ToString();
							Out_entity.VendorID = dt.Rows[i]["VendorID"].ToString();	//供应商ID
							Out_entity.PartNo = dt.Rows[i]["PartNo"].ToString();		//制造编号
							Out_entity.MFG = dt.Rows[i]["MFG"].ToString();				//制造商
							//Out_entity.PreserveQuantitySet = 0;						//预留数量
							Out_entity.OperateHistory = true;

							//把数据写入财务接口
							CInterfaceOfFinanceAccess　　pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess() ;
							CInterfaceOfFinance pInterfaceOfFinance = new CInterfaceOfFinance();
							//库房
							pInterfaceOfFinance.Location = dt.Rows[i]["WHIDOld"].ToString() ;
							//物资编码
							pInterfaceOfFinance.ItemCode = dt.Rows[i]["ItemCode"].ToString() ;
							//库位
							pInterfaceOfFinance.BinNo = dt.Rows[i]["BINIDOld"].ToString() ;
							//单据号
							pInterfaceOfFinance.BillNo = dt.Rows[i]["TransferWH2WHNO"].ToString() ;
							//操作人
							pInterfaceOfFinance.Operater = dt.Rows[i]["CreateBy"].ToString() ;
							//是出库还是入库
							pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_OUT ;
							//单据类型
							pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_TransferWH2WH) ;
							//基本单位的数量
							pInterfaceOfFinance.Quantity =  decimal.Parse(dt.Rows[i]["TransferQuantity"].ToString()) ;
							//核算单价
							pInterfaceOfFinance.UnitPriceStandard =  decimal.Parse(dt.Rows[i]["UnitPriceStandard"].ToString()) ;

							pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;
							
							bool IsOK = storeAccess.OperateStore ( Out_entity );

							//入库
							DataEntity.CInStoreMaterialDetail In_entity = new DataEntity.CInStoreMaterialDetail();
							
							In_entity.StoreOperateType = DataEntity.STOREOPERATETYPE.TYPE_IN;
							In_entity.BINID = dt.Rows[i]["BINIDNew"].ToString();
							In_entity.ItemCode = dt.Rows[i]["ItemCode"].ToString();
							In_entity.POID = dt.Rows[i]["POID"].ToString();
							In_entity.QuantityInBinSet = Decimal.Parse(dt.Rows[i]["TransferQuantity"].ToString());//库位数量 = 原库位数量 - 转料数量
							In_entity.UnitPricePONatural = Decimal.Parse(dt.Rows[i]["UnitPriceNatural"].ToString());
							In_entity.UnitPricePOStandard = Decimal.Parse(dt.Rows[i]["UnitPriceStandard"].ToString());
							In_entity.WHID = dt.Rows[i]["WHIDNew"].ToString();
							In_entity.VendorID = dt.Rows[i]["VendorID"].ToString();		//供应商ID
							In_entity.PartNo = dt.Rows[i]["PartNo"].ToString();			//制造编号
							In_entity.MFG = dt.Rows[i]["MFG"].ToString();				//制造商
							In_entity.OperateHistory = true;

							//把数据写入财务接口
							//库房
							pInterfaceOfFinance.Location = dt.Rows[i]["WHIDNew"].ToString() ;
							//物资编码
							pInterfaceOfFinance.ItemCode = dt.Rows[i]["ItemCode"].ToString() ;
							//库位
							pInterfaceOfFinance.BinNo = dt.Rows[i]["BINIDNew"].ToString() ;
							//单据号
							pInterfaceOfFinance.BillNo = dt.Rows[i]["TransferWH2WHNO"].ToString() ;
							//操作人
							pInterfaceOfFinance.Operater = dt.Rows[i]["CreateBy"].ToString() ;
							//是出库还是入库
							pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_IN ;
							//单据类型
							pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_TransferWH2WH) ;
							//基本单位的数量
							pInterfaceOfFinance.Quantity =  decimal.Parse(dt.Rows[i]["TransferQuantity"].ToString()) ;
							//核算单价
							pInterfaceOfFinance.UnitPriceStandard =  decimal.Parse(dt.Rows[i]["UnitPriceStandard"].ToString()) ;

							pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;

							IsOK = storeAccess.OperateStore ( In_entity );
						}

						#endregion

						break;
					}
				}
			}

			return sErrorMsg;
		}

		#endregion

		
		#region	在提交审核时进行校验( 实收数量是否大于可收数量 )
		/// <summary>
		/// 在提交审核时进行校验
		/// </summary>
		/// <param name="sReceiveID">转料单编号</param>
		/// <returns></returns>
		public string CheckNum ( string sTransferWH2WHID )
		{
			string sErrorMsg = string.Empty;

			string sSelectTransferWH2WHMaterial = @"select WH_InStoreMaterialDetail.ItemCode,WH_InStoreMaterialDetail.InStockMaterialID,TransferQuantity,
														WH_InStoreMaterialDetail.QuantityInBin 
														from WH_TransferWH2WHMaterial 
														left join WH_InStoreMaterialDetail on WH_TransferWH2WHMaterial.InStockMaterialID 
														= WH_InStoreMaterialDetail.InStockMaterialID
														where 
														QuantityInBin - TransferQuantity <0 AND TransferWH2WHID = '"+sTransferWH2WHID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectTransferWH2WHMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ 
				sErrorMsg = dt.Rows[0]["ItemCode"].ToString();
			}

			return sErrorMsg;
		}
		#endregion


	}
}
