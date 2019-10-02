using System;
using Cnwit.Utility;
using System.Data;
using System.Collections ;
using System.Text ;
using Common;

namespace DataEntity
{
	/// <summary>
	/// CInterfaceOfFinanceAccess 的摘要说明。
	/// </summary>
	public class CInterfaceOfFinanceAccess
	{
		private DataAcess pDataAcess = GetProjectDataAcess.GetDataAcess();
		public CInterfaceOfFinanceAccess()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public string GetBillType(BILLTYPE enBILLTYPE)
		{
			string sBillType = string.Empty ;

			switch(enBILLTYPE)
			{
				case BILLTYPE.TYPE_Receive : 
					sBillType = "REC" ;
					break ;
				case BILLTYPE.TYPE_Direct : 
					sBillType = "DIR" ;
					break ;
				case BILLTYPE.TYPE_Issue : 
					sBillType = "ISU" ;
					break ;
				case BILLTYPE.TYPE_Preserve : 
					sBillType = "PRE" ;
					break ;
				case BILLTYPE.TYPE_CancelPreserve : 
					sBillType = "CLP" ;
					break ;
				case BILLTYPE.TYPE_Return : 
					sBillType = "RET" ;
					break ;
				case BILLTYPE.TYPE_Reject : 
					sBillType = "REJ" ;
					break ;
				case BILLTYPE.TYPE_Borrow : 
					sBillType = "BOW" ;
					break ;
				case BILLTYPE.TYPE_AdjustIN : 
					sBillType = "AJI" ;
					break ;
				case BILLTYPE.TYPE_AdjustOut : 
					sBillType = "AJO" ;
					break ;
				case BILLTYPE.TYPE_TransferWH2WH : 
					sBillType = "TWW" ;
					break ;
				default : 
					sBillType = "" ;
					break ;
			}
			return sBillType ;
		}

		public virtual  bool OperateInterface(CInterfaceOfFinance pInterfaceOfFinance)
		{
			
			string[] sParams = {"Location","ItemCode","BinNo","BillNo","OperationType","Quantity","OperationDirection","UnitPriceStandard","Operater"} ;
			object[] objParamValues = {pInterfaceOfFinance.Location,pInterfaceOfFinance.ItemCode,pInterfaceOfFinance.BinNo,pInterfaceOfFinance.BillNo,pInterfaceOfFinance.OperationType,pInterfaceOfFinance.Quantity,pInterfaceOfFinance.OperationDirection,pInterfaceOfFinance.UnitPriceStandard,pInterfaceOfFinance.Operater} ; 
			SqlDbType[] paramTypes = { SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Real,SqlDbType.Int,SqlDbType.Money,SqlDbType.NVarChar} ;
			return pDataAcess.ExecuteSP("spInsertInterfaceOfFinance",sParams,objParamValues,paramTypes) ; 
		}
	}
}
