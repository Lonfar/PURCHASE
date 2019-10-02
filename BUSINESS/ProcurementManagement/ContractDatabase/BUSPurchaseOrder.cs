using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BusPurchaseOrder ��ժҪ˵����
	/// </summary>
	public class BUSPurchaseOrder:BUSBase
	{
		public BUSPurchaseOrder()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
			//��ͬ�ǰ��
			decimal decTotalAmount = 0.0m ;
			//�ƻ��ǰ��
			decimal decPlanTotalAmount = 0.0m ;
			//��ͬ�λ����
			decimal decContractNaturalER = 0.0m ;
			//��ͬ��������
			decimal decContractStandardER = 0.0m ;
			//�ƻ��λ����
			decimal decPlanNaturalER = 0.0m ;
			//�ƻ���������
			decimal decPlanStandardER = 0.0m ;
			//�����ͬ�ǰ��
			this.CalContractTotalAmount(dtPOMaterial,ref decTotalAmount);
			//�ƻ��ܶ�
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCost"] != System.DBNull.Value)
			{
				decPlanTotalAmount =  Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCost"]);
			}
		
			//�����ĺ�ͬ�ǰ��
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.TaxCost"] != System.DBNull.Value)
			{
				//��˰��
				decTotalAmount = decTotalAmount + Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.TaxCost"]);
			}
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.Discount"] != System.DBNull.Value)
			{
				//���ۿ�
				decTotalAmount = decTotalAmount - Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.Discount"]);
			}
			//��ǰ��ͬ��λ����
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostNaturalER"] != System.DBNull.Value)
			{
				decContractNaturalER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostNaturalER"]);
			}
			//��ǰ��ͬ�������
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostStandardER"] != System.DBNull.Value)
			{
				decContractStandardER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostStandardER"]);
			}
			//��ǰ�ƻ���λ����
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostNaturalER"] != System.DBNull.Value)
			{
				decPlanNaturalER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostNaturalER"]);
			}
			//��ǰ�ƻ��������
			if(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostStandardER"] != System.DBNull.Value)
			{
				decPlanStandardER = Convert.ToDecimal(dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostStandardER"]);
			}
			//��ǰ��ͬ��
			dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCost"] = decTotalAmount ;
			//��ǰ��ͬ�λ��
			dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostNatural"] = decTotalAmount * decContractNaturalER ;
			//��ǰ��ͬ������
			dtPurchaseOrder.Rows[0]["PurchaseOrder.ContractTotalCostStandard"] = decTotalAmount * decContractStandardER; 
			//��ǰ�ƻ��λ��
			dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostNatural"] = decPlanTotalAmount * decPlanNaturalER ;
			//��ǰ�ƻ�������
			dtPurchaseOrder.Rows[0]["PurchaseOrder.PlanTotalCostStandard"] = decPlanTotalAmount * decPlanStandardER; 
		}

		
		/// <summary>
		/// ��֤���м�¼����
		/// </summary>
		/// <param name="dt">Edit��</param>
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
