using System;
using System.Data;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEReturnEdit 的摘要说明。
	/// </summary>
	public class DAEReturnEdit : DAEBase
	{
		Cnwit.Utility.DataAcess _da ;

		public DAEReturnEdit()
		{
			_da = Common.GetProjectDataAcess.GetDataAcess() ;
		}

		#region Get IssueInfos

		public DataTable GetIssueInfos ( string sIssueID )
		{
			string strSql = @"
				SELECT
					WH_Issue.WHID , WH_BI_WareHouse.WHName , 
					WH_Issue.DepID , BI_Department.DepartmentName , 
					WH_Issue.EmployeeIDReceive , BI_Employee.FullName , WH_Issue.AFEID, WH_BI_AFE.AFEDescription
				FROM
					WH_Issue inner join WH_BI_WareHouse
					on WH_Issue.WHID = WH_BI_WareHouse.WHID
					inner join BI_Department
					on WH_Issue.DepID = BI_Department.IDKey
					inner join BI_Employee
					on WH_Issue.EmployeeIDReceive = BI_Employee.IDKey
                    inner join WH_BI_AFE on WH_BI_AFE.AFEID = WH_Issue.AFEID
				WHERE
					WH_Issue.IssueID = '" + sIssueID + "'" ;

			return _da.GetDataTable ( strSql ) ;
		}

		#endregion

		#region 根据ItemCode获得物料其他信息

		public void UpdataDataTable_MaterialList ( DataTable dataTable,string sWHID )
		{
			string SelectSql = string.Empty;
			DataTable dt_Temp ;
			string BINID = GetBINIDFromWHID(sWHID);

			foreach ( DataRow dr in dataTable.Rows )
			{
				
				if(dr.RowState != DataRowState.Deleted)
				{
					SelectSql = @"
					SELECT
						WH_IssueMaterial.BINID , 
						WH_IssueMaterial.UnitPriceNatural , 
						WH_IssueMaterial.ItemCode , 
						WH_IssueMaterial.MaterialUomID , 
						MaterialUOM.UOMID , 
						Material.MaterialName , 
						WH_IssueMaterial.FactIssuedQuantity , 
						WH_IssueMaterial.POID , 
						WH_IssueMaterial.UnitPriceStandard
					From
						WH_IssueMaterial inner join MaterialUOM
						on WH_IssueMaterial.MaterialUomID = MaterialUOM.MaterialUomID
						inner join Material
						on WH_IssueMaterial.ItemCode = Material.ItemCode
					WHERE
						WH_IssueMaterial.IssueMaterialID = '" + 
						dr["IssueMaterialID"].ToString() + "'" ;

					dt_Temp = _da.GetDataTable ( SelectSql );

					if ( dt_Temp.Rows.Count > 0 )
					{
						if ( dr["WH_ReturnMaterial__IssueMaterialID"] != null )
						{ 
							dr["WH_ReturnMaterial__IssueMaterialID"] = dr["IssueMaterialID"] ;
						}
						if ( dr["BINID"] != null)
						{ 
							dr["WH_ReturnMaterial__BINID"] = BINID;
							dr["BINID"] = BINID; // dt_Temp.Rows[0]["BINID"].ToString() ; 
						}
//						if ( dr["WH_ReturnMaterial__BINID"] != null)
//						{ 
//							dr["WH_ReturnMaterial__BINID"] = "" ; // dt_Temp.Rows[0]["BINID"].ToString() ; 
//						}
						if ( dr["UnitPriceNatural"] != null )
						{ 
							dr["UnitPriceNatural"] = dt_Temp.Rows[0]["UnitPriceNatural"].ToString() ;
						}
						if ( dr["MaterialName"] != null)
						{ 
							dr["MaterialName"] = dt_Temp.Rows[0]["MaterialName"].ToString() ; 
						}
						if ( dr["ItemCode"] != null)
						{ 
							dr["ItemCode"] = dt_Temp.Rows[0]["ItemCode"].ToString() ; 
						}
						if ( dr["MaterialUomID"] != null)
						{ 
							dr["MaterialUomID"] = dt_Temp.Rows[0]["MaterialUomID"].ToString() ; 
						}
						if ( dr["WH_ReturnMaterial__MaterialUomID"] != null)
						{ 
							dr["WH_ReturnMaterial__MaterialUomID"] = dt_Temp.Rows[0]["UOMID"].ToString() ;
						}
						if ( dr["IssueQuantity"] != null )
						{ 
							dr["IssueQuantity"] = dt_Temp.Rows[0]["FactIssuedQuantity"].ToString() ;
						}
						if (dr["POID"] != null )
						{ 
							dr["POID"] = dt_Temp.Rows[0]["POID"].ToString() ;
						}
						if (dr["WH_ReturnMaterial__POID"] != null )
						{ 
							dr["WH_ReturnMaterial__POID"] = dt_Temp.Rows[0]["POID"].ToString() ;
						}
						if (dr["UnitPriceStandard"] != null )  
						{ 
							dr["UnitPriceStandard"] = Double.Parse(dt_Temp.Rows[0]["UnitPriceStandard"].ToString()).ToString("f2");
						}
						if (dr["CanReturnQuantity"] != null )  
						{
							dr["CanReturnQuantity"] = GetCanReturnQuantity ( 
								dr["IssueMaterialID"].ToString() ) ;
						}
					}


				}
			}
		}

		private string GetBINIDFromWHID(string sWHID)
		{
			string sSql = "SELECT BINID FROM WH_BI_BIN WHERE Status='1' AND WHID = '"+sWHID+"'";
			return _da.GetDataTable ( sSql ).Rows[0][0].ToString();			
		}

		#endregion

		#region Update Return State

		public string UpdateReturnState ( string sReturnID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_Return SET Status = " + 
				iState.ToString()+" WHERE ReturnID = '"+sReturnID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );

			if ( sErrorMsg.Length == 0 )
			{
				if ( state == ApproveState.State_Approved )
				{
					string sSql = @"
							SELECT
								WH_ReturnMaterial.ItemCode , 
								WH_ReturnMaterial.POID , 
								WH_ReturnMaterial.BINID , 
								WH_ReturnMaterial.UnitPriceNatural , 
								WH_ReturnMaterial.UnitPriceStandard , 
								WH_ReturnMaterial.FactReturnQuantity , 
								WH_ReturnMaterial.depreciationRate , 
								WH_InStoreMaterialDetail.VendorID,
								WH_Return.WHIDReceive ,WH_Return.CreateBy ,WH_Return.ReturnNo 
							FROM
								WH_Return inner join WH_ReturnMaterial
								on WH_Return.ReturnID = WH_ReturnMaterial.ReturnID	
								inner join WH_IssueMaterial on WH_ReturnMaterial.IssueMaterialID = WH_IssueMaterial.IssueMaterialID
								inner join WH_InStoreMaterialDetail on WH_IssueMaterial.InStockMaterialID = WH_InStoreMaterialDetail.InStockMaterialID
							WHERE
								WH_ReturnMaterial.ReturnID = '" + sReturnID + "'" ;

					DataTable dt = _da.GetDataTable ( sSql ) ;

					foreach ( DataRow dr in dt.Rows )
					{
						//入库
						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
						CInStoreMaterialDetail pInStore = new CInStoreMaterialDetail();
						pInStore.StoreOperateType = STOREOPERATETYPE.TYPE_IN ;
						pInStore.OperateHistory = true;
						pInStore.BINID = dr["BINID"].ToString() ;
						pInStore.VendorID = dr["VendorID"].ToString() ;
						pInStore.ItemCode = dr["ItemCode"].ToString() ;
						pInStore.POID = dr["POID"].ToString() ;
						pInStore.WHID = dr["WHIDReceive"].ToString() ;
						decimal dUnitPriceNatural = Decimal.Parse ( dr["UnitPriceNatural"].ToString() ) ;
						decimal dUnitPriceStandard = Decimal.Parse ( dr["UnitPriceStandard"].ToString() ) ;
						decimal dDepreciationRate = Decimal.Parse ( dr["depreciationRate"].ToString() ) ;
						pInStore.UnitPricePONatural = dUnitPriceNatural * dDepreciationRate ;
						pInStore.UnitPricePOStandard = dUnitPriceStandard * dDepreciationRate ;
						pInStore.QuantityInBinSet = Decimal.Parse(dr["FactReturnQuantity"].ToString()) ;
						pInStoreMaterialDetailAccess.OperateStore(pInStore);

						//把数据写入财务接口
						CInterfaceOfFinanceAccess　　pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess() ;
						CInterfaceOfFinance pInterfaceOfFinance = new CInterfaceOfFinance();
						//库房
						pInterfaceOfFinance.Location = dr["WHIDReceive"].ToString() ;
						//物资编码
						pInterfaceOfFinance.ItemCode = dr["ItemCode"].ToString() ;
						//库位
						pInterfaceOfFinance.BinNo = dr["BINID"].ToString() ;
						//单据号
						pInterfaceOfFinance.BillNo = dr["ReturnNo"].ToString() ;
						//操作人
						pInterfaceOfFinance.Operater = dr["CreateBy"].ToString() ;
						//是出库还是入库
						pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_IN ;
						//单据类型
						pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_Return) ;
						//基本单位的数量
						pInterfaceOfFinance.Quantity =  decimal.Parse(dr["FactReturnQuantity"].ToString()) ;
						//核算单价
						pInterfaceOfFinance.UnitPriceStandard =  decimal.Parse(dr["UnitPriceStandard"].ToString()) ;

						pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;
					}
				}
			}

			return sErrorMsg;
		}

		#endregion

		#region Get IssueMaterial Count

		public int GetIssueMaterialCount ( string sIssueMaterialID )
		{
			string strSql = @"
				SELECT
					sum ( FactIssuedQuantity )
				FROM
					WH_IssueMaterial
				WHERE
					IssueMaterialID = '" + sIssueMaterialID + "'" ;

			DataTable dt = _da.GetDataTable ( strSql ) ;
			if ( dt.Rows[0][0] != System.DBNull.Value )
			{
				return Convert.ToInt32 ( dt.Rows[0][0] ) ;
			}
			else
			{
				return 0 ;
			}
		}

		#endregion

		#region Get ReturnMaterial Count

		public int GetReturnMaterialCount ( string sIssueMaterialID )
		{
			int nState = Convert.ToInt32 ( DataEntity.ApproveState.State_Approved  ) ;

			string strSql = @"
				SELECT
					sum ( WH_ReturnMaterial.FactReturnQuantity )
				FROM
					WH_ReturnMaterial inner join WH_Return
					on WH_ReturnMaterial.ReturnID = WH_Return.ReturnID
				WHERE
					WH_Return.Status = " + nState.ToString() + 
				" AND WH_ReturnMaterial.IssueMaterialID = '" + sIssueMaterialID + "'" ;

			DataTable dt = _da.GetDataTable ( strSql ) ;
			if ( dt.Rows[0][0] != System.DBNull.Value )
			{
				return Convert.ToInt32 ( dt.Rows[0][0] ) ;
			}
			else
			{
				return 0 ;
			}
		}

		#endregion

		#region Get CanReturnQuantity

		public int GetCanReturnQuantity ( string sIssueMaterialID )
		{
			int nCanReturnQuantity = 
				GetIssueMaterialCount ( sIssueMaterialID ) - 
				GetReturnMaterialCount ( sIssueMaterialID ) ;

			if ( nCanReturnQuantity > 0 )
			{
				return nCanReturnQuantity ;
			}
			else
			{
				return 0 ;
			}
		}

		#endregion

		#region Check ReturnQuantity - for Approve

		public string CheckReturnQuantity ( string sReturnID )
		{
			string strSql = @"
				SELECT
					IssueMaterialID , FactReturnQuantity
				FROM
					WH_ReturnMaterial
				WHERE
					ReturnID = '" + sReturnID + "'" ;

			DataTable dtReturnMaterial = _da.GetDataTable ( strSql ) ;
			int nCount = dtReturnMaterial.Rows.Count ;

			if ( nCount > 0 )
			{
				for ( int i = 0 ; i < nCount ; i ++ )
				{
					string sIssueMaterialID = 
						dtReturnMaterial.Rows[i]["IssueMaterialID"].ToString() ;
					int nFactReturnQuantity = 
						Convert.ToInt32 ( dtReturnMaterial.Rows[i]["FactReturnQuantity"] ) ;

					int nCanReturnQuantity = GetCanReturnQuantity ( sIssueMaterialID ) ;

					if ( nFactReturnQuantity > nCanReturnQuantity ) return "Error01" ;
				}
			}

			return string.Empty ;
		}

		#endregion
	}
}
