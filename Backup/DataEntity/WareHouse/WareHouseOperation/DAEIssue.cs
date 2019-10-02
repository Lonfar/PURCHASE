using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEIssue ��ժҪ˵����
	/// </summary>
	public class DAEIssue : DAEBase
	{
		CEntityUitlity  cen = new CEntityUitlity();
		
		public DAEIssue()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		//���ʱ������
		public string GetItemCode(string MaterialUomID)
		{
		   string sSql = @"Select * From MaterialUOM WHERE MaterialUomID ='"+ MaterialUomID +"'";
		   DataTable dtBaseUnit =  BaseDataAccess.GetDataTable (sSql);

		   string strItemCode ="";

			if(dtBaseUnit != null )
			{
				if(dtBaseUnit.Rows.Count != 0)
				{
					strItemCode = dtBaseUnit.Rows[0]["ItemCode"].ToString();
				}
			}
			return strItemCode;
		}

        public string getAFEinfo(string afeNo)
        {
            string strSql = "SELECT AFEDescription FROM WH_BI_AFE WHERE (AFEID = '" + afeNo + "')";
            return BaseDataAccess.GetDataTable(strSql).Rows[0][0].ToString();

        }

		public string GetIssueMaterialBaseUomID(string strInStockMaterialID)
		{
			string sSql = @"SELECT WH_InStoreMaterialDetail.*,MaterialUOM.MaterialUomID From WH_InStoreMaterialDetail LEFT JOIN  MaterialUOM 
								on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID  AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1
								WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+strInStockMaterialID+"'";
			DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
			return dtTempInfo.Rows[0]["MaterialUomID"].ToString();
		}

	
		#region ���³��ⵥ��״̬

		public string UpdateWH_IssueState ( string IssueID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
            string sUpdateSql = "UPDATE WH_Issue SET Status = " + iState.ToString() + "WHERE IssueID = '" + IssueID + "' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			if(sErrorMsg.Length == 0)
			{			
				if(state == ApproveState.State_Approved)
				{
					string sSql = "SELECT a.* , b.* FROM WH_Issue a left join WH_IssueMaterial b on a.IssueID = b.IssueID WHERE a.IssueID = '"+IssueID+"'";
					DataTable IssueEdit = this.BaseDataAccess.GetDataTable ( sSql );
					
					foreach(DataRow drIssueEdit in IssueEdit.Rows)
					{
						//����
						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
						CInStoreMaterialDetail pOutStore = new CInStoreMaterialDetail();
						pOutStore.StoreOperateType = STOREOPERATETYPE.TYPE_OUT ;
						pOutStore.OperateHistory = true;
						pOutStore.InStockMaterialID = drIssueEdit["InStockMaterialID"].ToString() ;		 
						pOutStore.QuantityInBinSet  =  cen.ChangeToBaseUOM( drIssueEdit["ItemCode"].ToString(),drIssueEdit["MaterialUomID"].ToString(),decimal.Parse(drIssueEdit["FactIssuedQuantity"].ToString()));
						decimal decPreserveQuantityInFact = Convert.ToDecimal(drIssueEdit["PreserveQuantityInFact"] == DBNull.Value ? 0 : drIssueEdit["PreserveQuantityInFact"]);
						pOutStore.PreserveQuantitySet  =  cen.ChangeToBaseUOM( drIssueEdit["ItemCode"].ToString(),drIssueEdit["MaterialUomID"].ToString(),decPreserveQuantityInFact);
				 						
						pInStoreMaterialDetailAccess.OperateStore(pOutStore);

						//������д�����ӿ�
						CInterfaceOfFinanceAccess����pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess() ;
						CInterfaceOfFinance pInterfaceOfFinance = new CInterfaceOfFinance();
						//�ⷿ
						pInterfaceOfFinance.Location = drIssueEdit["WHID"].ToString() ;
						//���ʱ���
						pInterfaceOfFinance.ItemCode = drIssueEdit["ItemCode"].ToString() ;
						//��λ
						pInterfaceOfFinance.BinNo = drIssueEdit["BINID"].ToString() ;
						//���ݺ�
						pInterfaceOfFinance.BillNo = drIssueEdit["IssueNo"].ToString() ;
						//������
						pInterfaceOfFinance.Operater = drIssueEdit["CreateBy"].ToString() ;
						//�ǳ��⻹�����
						pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_OUT ;
						//��������
						pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_Issue) ;
						//������λ������
						pInterfaceOfFinance.Quantity =  decimal.Parse(drIssueEdit["FactIssuedQuantity"].ToString()) ;
						//���㵥��
						pInterfaceOfFinance.UnitPriceStandard =  decimal.Parse(drIssueEdit["UnitPriceStandard"].ToString()) ;

						pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;
					
					}
				}
			}
			return sErrorMsg;
		}

		#endregion


		/// ���ύ���ʱ����У��
		/// </summary>
		/// <param name="sReceiveID">���ϵ����</param>
		/// <returns></returns>
		public string CheckNum ( string sIssueID )
		{
			string sErrorMsg = string.Empty;

			string sSelectReceiveMaterial = @"select WH_IssueMaterial.ItemCode,WH_IssueMaterial.MaterialUomID,WH_IssueMaterial.QuantityInBin,
												(CASE WHEN IsBaseUOM =1 THEN FactIssuedQuantity ELSE FactIssuedQuantity*MultipleOfBaseUOM END)
												AS FactIssuedBaseQuantity,
												IsBaseUOM
												from WH_IssueMaterial 
												LEFT JOIN WH_InStoreMaterialDetail 
												ON WH_InStoreMaterialDetail.InStockMaterialID = WH_IssueMaterial.InStockMaterialID
												LEFT JOIN MaterialUOM
												on WH_IssueMaterial.MaterialUomID = MaterialUOM.MaterialUomID
												WHERE 
												WH_InStoreMaterialDetail.QuantityInBin -  (CASE WHEN IsBaseUOM =1 THEN FactIssuedQuantity ELSE FactIssuedQuantity*MultipleOfBaseUOM END) <0 and
												IssueID = '"+sIssueID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectReceiveMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ sErrorMsg = dt.Rows[0]["ItemCode"].ToString();	}

			return sErrorMsg;
		}

        public DataTable GetExcelData(string IssueID)
        {
            string strSql = "SELECT '' as No,ItemCode,MaterialName,IssueDate,UOMID,FactIssuedQuantity,UnitPriceStandard,TotalStandard,ContractTotalCostCUR,UnitPrice,TotalCost,BinID,POID,AFEID,Comment,IssueNo,EmployeeIDReceive,WHName FROM v_Report_Issue WHERE IssueID = '" + IssueID + "' ORDER BY v_Report_Issue.OrderID ";

            DataTable dt = this.BaseDataAccess.GetDataTable(strSql);

            return dt;
        }
        public DataTable GetReportData(string filter)
        {
            if (filter.Length != 0)
            {
                filter = "WHERE " + filter;
            }
            string strSql = "SELECT '' as No,ItemCode,MaterialName,UOMID,FactIssuedQuantity,UnitPriceStandard,TotalStandard,BinID,AFEID,IssueDate,POID,ContractTotalCostCUR,UnitPrice,TotalCost,Comment FROM v_Report_Issue  " + filter + " ORDER BY v_Report_Issue.OrderID ";

            DataTable dt = this.BaseDataAccess.GetDataTable(strSql);

            return dt;
        }
	}
}
