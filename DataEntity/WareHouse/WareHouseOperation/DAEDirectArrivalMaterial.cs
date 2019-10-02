using System;
using System.Data;
using DataEntity;

namespace DataEntity.WareHouseManagment
{
	/// <summary>
	/// 直达料数据实体类 Liujun Add at 2007-6-22
	/// </summary>
	public class DAEDirectArrivalMaterial : DAEBase
	{
		public DAEDirectArrivalMaterial()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 根据所选的PO物资带出对应值

		/// <summary>
		/// 根据所选的PO物资带出对应值
		/// </summary>
		/// <param name="dtReceiveMaterial"></param>
		public void GetPOMaterialInfo ( DataTable dtReceiveMaterial , string sPOID )
		{
			string sSelectSql = string.Empty;
			DataTable dtPOMaterial;
			CEntityUitlity cEntity = new CEntityUitlity();
			string sItemCode = string.Empty;

			// 需要计算可收数量
			foreach ( DataRow dr in dtReceiveMaterial.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					// 查询基本
					sSelectSql = @" SELECT POMaterial.MaterialUomID ,POMaterial.ItemCode,POMaterial.ReceiveBaseQuantity,MaterialUom.UOMID,MaterialUom.UomID, 
                                    POMaterial.PartNo , POMaterial.UnitPrice , POMaterial.TotalCost, POMaterial.POQuantity ,Material.MaterialName
									FROM POMaterial 
									INNER JOIN MaterialUOM ON POMaterial.MaterialUomID = MaterialUOM.MaterialUomID  
									INNER JOIN Material ON Material.ItemCode = POMaterial.ItemCode WHERE POID = '"+sPOID+"' AND POMaterialID = '"+dr["POMaterialID"].ToString()+"'";

					dtPOMaterial = this.BaseDataAccess.GetDataTable ( sSelectSql );

				
					dr["MaterialUomID"] = dtPOMaterial.Rows[0]["MaterialUomID"];
					dr["WH_ReceiveMaterial__MaterialUomID"] = dtPOMaterial.Rows[0]["UomID"];
					dr["PartNO"] = dtPOMaterial.Rows[0]["PartNo"];
					dr["UnitPrice"] = dtPOMaterial.Rows[0]["UnitPrice"];
					dr["POQuantity"] = dtPOMaterial.Rows[0]["POQuantity"];
					dr["ItemCode"] = dtPOMaterial.Rows[0]["ItemCode"].ToString();
					dr["MaterialName"] = dtPOMaterial.Rows[0]["MaterialName"].ToString();

					if ( dtPOMaterial.Rows[0]["ReceiveBaseQuantity"] == DBNull.Value || Convert.ToDecimal( dtPOMaterial.Rows[0]["ReceiveBaseQuantity"] ) == 0  )
					{
						dr["ReceivedQuantity"] = 0;
						dr["CanReceivedQuantity"] = dr["POQuantity"];
					}
					else
					{
						// 将已收数量转换为当前的单位
						decimal fReceivedQuattity =  cEntity.ChangeFromBaseUON ( dtPOMaterial.Rows[0]["ItemCode"].ToString() , dtPOMaterial.Rows[0]["MaterialUomID"].ToString() , Convert.ToDecimal( dtPOMaterial.Rows[0]["ReceiveBaseQuantity"] ));

						dr["ReceivedQuantity"] = fReceivedQuattity;
						dr["CanReceivedQuantity"] = Convert.ToDecimal( dr["POQuantity"] )- fReceivedQuattity;
					}
				}
			}
		}

		#endregion

		#region  根据所选的PO物资带出OSD对应值
		public void GetPOMaterialOSDInfo ( DataTable dtReceiveMaterial)
		{
			string sSelectSql = string.Empty;
			DataTable dtPOMaterial;

			foreach ( DataRow dr in dtReceiveMaterial.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					sSelectSql = @" SELECT ItemCode,POID,PartNo,MFG FROM POMaterial 
                                    WHERE POMaterialID = '"+dr["POMaterialID"].ToString()+"'";
					dtPOMaterial = this.BaseDataAccess.GetDataTable ( sSelectSql );				
					dr["ItemCode"] = dtPOMaterial.Rows[0]["ItemCode"];
					dr["PartNo"] = dtPOMaterial.Rows[0]["PartNo"];
					dr["MFG"] = dtPOMaterial.Rows[0]["MFG"].ToString();
				}
			}
		}

		#endregion

		#region 根据选择的PO来获得PO对应的详细信息

		/// <summary>
		/// 根据选择的PO来获得PO对应的详细信息
		/// </summary>
		/// <param name="sPOID">POID</param>
		/// <returns></returns>
		public DataTable GetPODetails ( string sPOID )
		{
			string sSelectSql = "SELECT * FROM PurchaseOrder WHERE POID = '"+sPOID+"'";

			DataTable dtPODetails = this.BaseDataAccess.GetDataTable ( sSelectSql );

			return dtPODetails;
		}

		#endregion

		#region 在提交审核时进行校验( 实收数量是否大于可收数量 )

		/// <summary>
		/// 在提交审核时进行校验
		/// </summary>
		/// <param name="sReceiveID">收料单编号</param>
		/// <returns></returns>
		public string CheckNum ( string sReceiveID )
		{
			string sErrorMsg = string.Empty;

			string sSelectReceiveMaterial = @"SELECT ItemCode FROM 
															(SELECT 
															WH_Receive.ReceiveID,
															WH_ReceiveMaterial.ItemCode,
															WH_Receive.POID ,
															WH_ReceiveMaterial.MaterialUomID,
															WH_ReceiveMaterial.FactReceivedQuantity ,
															POMaterial.BaseQuantity,
															POMaterial.ReceiveBaseQuantity,
															(POMaterial.BaseQuantity - POMaterial.ReceiveBaseQuantity ) as CanReceive,
															(WH_ReceiveMaterial.FactReceivedQuantity * MaterialUOM.MultipleOfBaseUOM ) as FactReceive

															FROM WH_ReceiveMaterial
															INNER JOIN WH_Receive ON WH_Receive.ReceiveID = WH_ReceiveMaterial.ReceiveID
															INNER JOIN PurchaseOrder ON PurchaseOrder.POID = WH_Receive.POID
															INNER JOIN POMaterial ON POMaterial.POID = PurchaseOrder.POID
															INNER JOIN MaterialUOM ON MaterialUOM.MaterialUOMID = WH_ReceiveMaterial.MaterialUOMID 

															WHERE POMaterial.ItemCode = WH_ReceiveMaterial.ItemCode 
															) AS a 
															WHERE a.CanReceive < a.FactReceive AND ReceiveID = '"+sReceiveID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectReceiveMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ sErrorMsg = dt.Rows[0]["ItemCode"].ToString();	}

			return sErrorMsg;
		}

		#endregion

		#region 更新直达收料单状态

		/// <summary>
		/// 更新直达收料单状态
		/// </summary>
		/// <param name="sReceiveID">主键</param>
		/// <param name="state">目标状态</param>
		/// <returns></returns>
		public string UpdateDirectState ( string sIssueID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_Receive SET Status = "+iState.ToString()+" WHERE ReceiveID = '"+sIssueID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );

			sUpdateSql = "UPDATE WH_Issue SET Status = "+iState.ToString()+" WHERE IssueID = '"+sIssueID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );

			switch ( state )
			{
				case ApproveState.State_Approved :
				{
					// 收料更新(库存,入库)
					string[] sParams = {"ReceiveID"};
					object[] objParamValues = {sIssueID} ; 
					SqlDbType[] paramTypes = { SqlDbType.NVarChar} ;

					bool bRetVal =  this.BaseDataAccess.ExecuteSP("spUpdatePOMaterialANDOperateStoreMaterialDetail",sParams,objParamValues,paramTypes) ; 

					if ( bRetVal == false )
					{
						sErrorMsg = "OperateStoreFailed";
					}


					string sSql = "SELECT a.* , b.WHID,b.POID,b.ReceiveNo,b.CreateBy FROM WH_ReceiveMaterial a left join WH_Receive b on a.ReceiveID = b.ReceiveID WHERE a.ReceiveID = '"+sIssueID+"'";
					DataTable dtReceiveMaterial = this.BaseDataAccess.GetDataTable ( sSql );
					CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
				
					// 财务接口处理类
					CInterfaceOfFinanceAccess pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess();

					foreach(DataRow drReceiveMaterial in dtReceiveMaterial.Rows)
					{
						
						// 财务接口实体
						CInterfaceOfFinance pInStoreInterfaceOfFinance = new CInterfaceOfFinance();
				
						pInStoreInterfaceOfFinance.Location = drReceiveMaterial["WHID"].ToString();
						pInStoreInterfaceOfFinance.ItemCode = drReceiveMaterial["ItemCode"].ToString();
						pInStoreInterfaceOfFinance.BinNo = drReceiveMaterial["BINID"].ToString();
						pInStoreInterfaceOfFinance.BillNo = drReceiveMaterial["ReceiveNo"].ToString();
						pInStoreInterfaceOfFinance.Operater = drReceiveMaterial["CreateBy"].ToString();
						pInStoreInterfaceOfFinance.Quantity = decimal.Parse ( drReceiveMaterial["FactReceivedQuantity"].ToString());
						pInStoreInterfaceOfFinance.UnitPriceStandard = decimal.Parse ( drReceiveMaterial["UnitPriceStandard"].ToString());
						pInStoreInterfaceOfFinance.OperationDirection = DIRECTIONTYPE.TYPE_IN;
						pInStoreInterfaceOfFinance.OperationType = pInterfaceOfFinanceAccess.GetBillType( BILLTYPE.TYPE_Receive ) ;

						pInterfaceOfFinanceAccess.OperateInterface( pInStoreInterfaceOfFinance ) ;



						// 出库
						CInStoreMaterialDetail pOutStore = new CInStoreMaterialDetail();
						pOutStore.StoreOperateType = STOREOPERATETYPE.TYPE_OUT ;
						pOutStore.OperateHistory = true;

						pOutStore.BINID = drReceiveMaterial["BINID"].ToString() ;
						pOutStore.ItemCode = drReceiveMaterial["ItemCode"].ToString() ;
						pOutStore.POID = drReceiveMaterial["POID"].ToString() ;
						pOutStore.WHID = drReceiveMaterial["WHID"].ToString() ;

						pOutStore.UnitPricePONatural = Decimal.Parse(drReceiveMaterial["UnitPriceNatural"].ToString()) ;
						pOutStore.UnitPricePOStandard = Decimal.Parse(drReceiveMaterial["UnitPriceStandard"].ToString()) ;
						pOutStore.QuantityInBinSet  = Decimal.Parse(drReceiveMaterial["FactReceivedQuantity"].ToString()) ;

						pInStoreMaterialDetailAccess.OperateStore(pOutStore);
					

						// 财务接口实体
						CInterfaceOfFinance pOutStoreInterfaceOfFinance = new CInterfaceOfFinance();
				
						pOutStoreInterfaceOfFinance.Location = drReceiveMaterial["WHID"].ToString();
						pOutStoreInterfaceOfFinance.ItemCode = drReceiveMaterial["ItemCode"].ToString();
						pOutStoreInterfaceOfFinance.BinNo = drReceiveMaterial["BINID"].ToString();
						pOutStoreInterfaceOfFinance.BillNo = drReceiveMaterial["ReceiveNo"].ToString();
						pOutStoreInterfaceOfFinance.Operater = drReceiveMaterial["CreateBy"].ToString();
						pOutStoreInterfaceOfFinance.Quantity = decimal.Parse ( drReceiveMaterial["FactReceivedQuantity"].ToString());
						pOutStoreInterfaceOfFinance.UnitPriceStandard = decimal.Parse ( drReceiveMaterial["UnitPriceStandard"].ToString());
						pOutStoreInterfaceOfFinance.OperationDirection = DIRECTIONTYPE.TYPE_OUT;
						pOutStoreInterfaceOfFinance.OperationType = pInterfaceOfFinanceAccess.GetBillType( BILLTYPE.TYPE_Issue ) ;

						pInterfaceOfFinanceAccess.OperateInterface( pOutStoreInterfaceOfFinance ) ;
					}

					break;
				}
			}
		
			return sErrorMsg;
		}

		#endregion

		#region 查看发料单是否已经存在

		/// <summary>
		/// 查看发料单是否已经存在
		/// </summary>
		/// <param name="sIssueID"></param>
		/// <returns></returns>
		public bool IsIssueExists ( string sIssueID )
		{
			string sSelectSql = "SELECT * FROM WH_Issue WHERE IssueID = '"+sIssueID+"'";

			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectSql );

			if ( dt.Rows.Count > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region	向发料物资同步直达料物资信息

		/// <summary>
		/// 向发料物资同步直达料物资信息
		/// </summary>
		/// <param name="dtReceiveMaterial">直达料物资数据</param>
		public void SaveIssueMaterial ( DataTable dtReceiveMaterial )
		{
			string sIssueMaterialID = string.Empty;
			string sBINID = string.Empty;
			string sMaterialUomID = string.Empty;
			string sItemCode = string.Empty;
			string sMaterialName = string.Empty;
			string sIssueID = string.Empty;
			string sFactIssuedQuantity = string.Empty;
			string sUnitPriceNatural = string.Empty;
			string sUnitPriceStandard = string.Empty;

			string sSql = string.Empty;
			string sErrorMsg = string.Empty;

			foreach ( DataRow dr in dtReceiveMaterial.Rows )
			{
				sIssueMaterialID = dr["WH_ReceiveMaterial.ReceiveMaterialID"].ToString();
				sBINID = dr["WH_ReceiveMaterial.BINID"].ToString();
				sMaterialUomID = dr["WH_ReceiveMaterial.MaterialUomID"].ToString();
				sItemCode = dr["WH_ReceiveMaterial.ItemCode"].ToString();
				sMaterialName = dr["WH_ReceiveMaterial.MaterialName"].ToString();
				sIssueID = dr["WH_ReceiveMaterial.ReceiveID"].ToString();
				sFactIssuedQuantity = dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString();
				sUnitPriceNatural = dr["WH_ReceiveMaterial.UnitPriceNatural"].ToString();
				sUnitPriceStandard = dr["WH_ReceiveMaterial.UnitPriceStandard"].ToString();

				if(dr.RowState != DataRowState.Deleted)
				{
					if ( dr["RowStatus"].ToString() == "NEW" )
					{
						sSql = @"INSERT INTO WH_IssueMaterial ( IssueMaterialID ,BINID , MaterialUomID , ItemCode ,MaterialName , IssueID , FactIssuedQuantity ,UnitPriceNatural,UnitPriceStandard  )
										Values ( '"+Guid.NewGuid()+"','"+sBINID+"','"+sMaterialUomID+"','"+sItemCode+"','"+sMaterialName+"','"+sIssueID+"',"+sFactIssuedQuantity+","+sUnitPriceNatural+","+sUnitPriceStandard +")";
					}
					else if ( dr["RowStatus"].ToString() == "EDIT" )
					{
						// 库位 , 数量 ,单价
						sSql = @"UPDATE WH_IssueMaterial SET BINID = '"+sBINID+"', FactIssuedQuantity = "+sFactIssuedQuantity+", UnitPriceNatural = "+sUnitPriceNatural+", UnitPriceStandard = "+sUnitPriceStandard+" WHERE IssueID = '"+sIssueID+"' AND ItemCode = '"+sItemCode+"'";
					}

					if ( sErrorMsg.Length == 0 )
					{
						sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sSql );
					}
				}
				else
				{
					// 删除发料物资
					sSql = @"DELETE FROM WH_IssueMaterial WHERE IssueID = '"+sIssueID+"' AND ItemCode = '"+sItemCode+"'";

					if ( sErrorMsg.Length == 0 )
					{
						sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sSql );
					}
				}
			}
		}

//		/// <summary>
//		/// 删除发料物资
//		/// </summary>
//		/// <param name="sPKs"></param>
//		public void DeleteIssueMaterial ( string[] sPKs )
//		{
//			string sSql = @"DELETE FROM WH_IssueMaterial WHERE WH_IssueMaterial.IssueMaterialID IN(
//							SELECT WH_IssueMaterial.IssueMaterialID FROM WH_IssueMaterial,WH_Receive,WH_ReceiveMaterial 
//							WHERE WH_ReceiveMaterial.ReceiveID = WH_Receive.ReceiveID
//							AND WH_Receive.ReceiveID = WH_IssueMaterial.IssueID
//							AND WH_IssueMaterial.ItemCode = WH_ReceiveMaterial.ItemCode
//							AND WH_ReceiveMaterial.ReceiveMaterialID = '";
//			string sErrorMsg = string.Empty;
//
//			foreach ( string sReceiveMaterialID in sPKs )
//			{
//				sSql += sReceiveMaterialID+"')";
//
//				sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sSql );
//			}
//		}

		#endregion

		#region 重写Save方法

		public override string Save()
		{
			if ( this.TableName == "WH_ReceiveMaterial" )
			{
				// 获得数据
				DataTable dt = this.CurDataTable.Copy();

				// 首先保存收料物资
				base.Save ();

				// 整理数据
				#region 修改列名

				dt.TableName = "WH_IssueMaterial";

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.ReceiveMaterialID" ))
				{
					dt.Columns["WH_ReceiveMaterial.ReceiveMaterialID"].ColumnName = "WH_IssueMaterial.IssueMaterialID";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.BINID" ))
				{
					dt.Columns["WH_ReceiveMaterial.BINID"].ColumnName = "WH_IssueMaterial.BINID";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.MaterialUomID" ))
				{
					dt.Columns["WH_ReceiveMaterial.MaterialUomID"].ColumnName = "WH_IssueMaterial.MaterialUomID";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.ReceiveMaterialID" ))
				{
					dt.Columns["WH_ReceiveMaterial.ReceiveMaterialID"].ColumnName = "WH_IssueMaterial.IssueMaterialID";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.ItemCode" ))
				{
					dt.Columns["WH_ReceiveMaterial.ItemCode"].ColumnName = "WH_IssueMaterial.ItemCode";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.MaterialName" ))
				{
					dt.Columns["WH_ReceiveMaterial.MaterialName"].ColumnName = "WH_IssueMaterial.MaterialName";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.FactReceivedQuantity" ))
				{
					dt.Columns["WH_ReceiveMaterial.FactReceivedQuantity"].ColumnName = "WH_IssueMaterial.FactIssuedQuantity";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.UnitPriceNatural" ))
				{
					dt.Columns["WH_ReceiveMaterial.UnitPriceNatural"].ColumnName = "WH_IssueMaterial.UnitPriceNatural";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.UnitPriceStandard" ))
				{
					dt.Columns["WH_ReceiveMaterial.UnitPriceStandard"].ColumnName = "WH_IssueMaterial.UnitPriceStandard";
				}

				if ( dt.Columns.Contains( "WH_ReceiveMaterial.ReceiveID" ))
				{
					dt.Columns["WH_ReceiveMaterial.ReceiveID"].ColumnName = "WH_IssueMaterial.IssueID";
				}

				#endregion


				this.CurDataTable = dt;
				this.TableName = "WH_IssueMaterial";
				this.PKFieldName = "IssueMaterialID";
				this.BusPKFieldName = "IssueID,ItemCode,BINID";
			}

			return base.Save();
		}

		#endregion

	}
}
