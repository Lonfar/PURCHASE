using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSTransferDEP2DEPEdit 的摘要说明。
	/// </summary>
	public class BUSTransferDEP2DEPEdit :BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSTransferDEP2DEPEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region  业务逻辑运算
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtEdit"></param>
		/// <param name="dtChild"></param>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal decTotalAmountStandard = 0.0m ;
			decimal decTotalAmountNatural = 0.0m ;
			decimal decDepreciationRate = 0.0m;

			foreach(DataRow row in dtChild.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
				 decimal   decUnitPriceStandard =  Convert.ToDecimal(row["WH_TransferDEP2DEPMaterial.UnitPriceStandard"].ToString());
				 decimal   decUnitPriceNatural =  Convert.ToDecimal(row["WH_TransferDEP2DEPMaterial.UnitPriceNatural"].ToString());
				 decimal   decTransferQuantity =  Convert.ToDecimal(row["WH_TransferDEP2DEPMaterial.FactIssuedQuantity"].ToString());
				 decDepreciationRate=  Convert.ToDecimal(row["WH_TransferDEP2DEPMaterial.depreciationRate"].ToString());
				 decTotalAmountStandard += decUnitPriceStandard * decTransferQuantity*decDepreciationRate;
				  decTotalAmountNatural += decUnitPriceNatural * decTransferQuantity*decDepreciationRate;
				}
			}
		
			dtEdit.Rows[0]["WH_TransferDEP2DEP.TotalPriceStandard"] = decTotalAmountStandard;
			dtEdit.Rows[0]["WH_TransferDEP2DEP.TotalPriceNatural"] = decTotalAmountNatural; 
			
		}
		#endregion


		#region  业务规则验证
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dtChild)
		{
			string sErrMsg = "";

			if (dtChild.Rows.Count > 0)
			{
				foreach(DataRow row in dtChild.Rows)
				{
					if(row.RowState != DataRowState.Deleted)
					{
						decimal decFactIssuedQuantity = Decimal.Parse(row["WH_TransferDEP2DEPMaterial.FactIssuedQuantity"].ToString()); 
						decimal decIssueQuantity = Decimal.Parse(row["WH_TransferDEP2DEPMaterial.IssueQuantity"].ToString()) ; 
						decimal decDepreciationRate = Decimal.Parse(row["WH_TransferDEP2DEPMaterial.depreciationRate"].ToString()) ; 
						
						if( decFactIssuedQuantity > decIssueQuantity )
						{
							sErrMsg ="CheckErrMsg1";
							break;
						}
						if( decDepreciationRate > 1 || decDepreciationRate < 0 ) 
						{
							sErrMsg = "CheckErrMsg2" ;
							break;
						}
		
					}
				}
			}

			return sErrMsg;
		}
		
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtChild)
		{
			foreach(DataRow dr in dtChild.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoMaterialSelected" ;
		}

		#endregion

	}
}
