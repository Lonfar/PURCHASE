using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BusPurchaseOrder 的摘要说明。
	/// </summary>
	public class BUSPurchaseOrder:BUSBase
	{
		public BUSPurchaseOrder()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        

		public void CalContractTotalAmount(DataTable dtPOMaterial,ref  decimal decContractTotalCost)
		{
			foreach(DataRow drPOMaterial in dtPOMaterial.Rows)
			{
				if(drPOMaterial.RowState != DataRowState.Deleted)
				{
					decimal decUnitPrice =  Convert.ToDecimal(drPOMaterial["POMaterial.UnitPrice"].ToString());
					decimal decPOQuantity =  Convert.ToDecimal(drPOMaterial["POMaterial.POQuantity"].ToString());
					decContractTotalCost += decPOQuantity*decUnitPrice ;
				}
			}
		}

		public decimal SetPOMaterialSum(string str1,string str2)
		{
			decimal decsum =-1;
			if(str1 != null && str2 != null )
			{
				if(str1.Length != 0 && str2.Length != 0)
				{
					decsum = Convert.ToDecimal(str1)* Convert.ToDecimal(str2);
				}
			}
			return decsum;
		}
		
		public void SetContractTotalAmount(DataTable dtPurchaseOrder , DataTable dtPOMaterial)
		{
			//合同额当前币
			decimal decTotalAmount = 0.0m ;
			//计划额当前币
			decimal decPlanTotalAmount = 0.0m ;
			//合同额本位汇率
			decimal decContractNaturalER = 0.0m ;
			//合同额核算汇率
			decimal decContractStandardER = 0.0m ;
			//计划额本位汇率
			decimal decPlanNaturalER = 0.0m ;
			//计划额核算汇率
			decimal decPlanStandardER = 0.0m ;
			//计算合同额当前币
			this.CalContractTotalAmount(dtPOMaterial,ref decTotalAmount);
			//计划总额
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCost"] != System.DBNull.Value)
			{
				decPlanTotalAmount =  Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCost"]);
			}
		
			//真正的合同额当前币
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.TaxCost"] != System.DBNull.Value)
			{
				//加税收
				decTotalAmount = decTotalAmount + Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.TaxCost"]);
			}
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.Discount"] != System.DBNull.Value)
			{
				//减折扣
				decTotalAmount = decTotalAmount - Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.Discount"]);
			}
			//当前合同本位汇率
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostNaturalER"] != System.DBNull.Value)
			{
				decContractNaturalER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostNaturalER"]);
			}
			//当前合同核算汇率
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostStandardER"] != System.DBNull.Value)
			{
				decContractStandardER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostStandardER"]);
			}
			//当前计划本位汇率
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostNaturalER"] != System.DBNull.Value)
			{
				decPlanNaturalER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostNaturalER"]);
			}
			//当前计划核算汇率
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostStandardER"] != System.DBNull.Value)
			{
				decPlanStandardER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostStandardER"]);
			}
			//当前合同额
			dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCost"] = decTotalAmount ;
			//当前合同额本位币
			dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostNatural"] = decTotalAmount * decContractNaturalER ;
			//当前合同额核算币
			dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostStandard"] = decTotalAmount * decContractStandardER; 
			//当前计划额本位币
			dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostNatural"] = decPlanTotalAmount * decPlanNaturalER ;
			//当前计划额核算币
			dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostStandard"] = decPlanTotalAmount * decPlanStandardER; 
		}

		
		/// <summary>
		/// 验证表中记录行数
		/// </summary>
		/// <param name="dt">Edit表</param>
		/// <returns>sErrMsg</returns>
		public string CheckChildRows(DataTable dtBorrowMaterial)
		{
			string sErrMsg = "";
			if(dtBorrowMaterial.Rows.Count <0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}


	}
}
