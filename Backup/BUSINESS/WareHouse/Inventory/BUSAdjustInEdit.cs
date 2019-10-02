using System;
using System.Data;
using System.Data.SqlClient;
namespace Business
{
	/// <summary>
	/// BUSAdjustInEdit ��ժҪ˵����
	/// </summary>
	public class BUSAdjustInEdit : BUSBase
	{
		public BUSAdjustInEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region  ҵ���߼�����

		/// <summary>
		/// ������Ϻ���ͱ�λ�ܶ�
		/// </summary>
		/// <param name="dtBorrowEdit">Edit��</param>
		/// <param name="decTotalAmountStandard">�����ܶ�</param>
		/// <param name="decTotalAmountNatural">��λ�ܶ�</param>
		/// <returns></returns>
		public void CalTotalAmount(DataTable dtAdjustIN,DataTable dtMaterialList,DataTable dtInventoryList)
		{
			decimal dAdjustInSumPriceStandard = 0 ;
			decimal dAdjustInSumPriceNatural = 0 ;

			foreach ( DataRow drMaterialList in dtMaterialList.Rows )
			{
				if (drMaterialList.RowState != DataRowState.Deleted)
				{
					decimal dAdjustInQuantity = Convert.ToDecimal ( drMaterialList["WH_AdjustInMaterial.AdjustInQuantity"].ToString() ) ;
					decimal dUnitPriceStandard = Convert.ToDecimal ( drMaterialList["WH_AdjustInMaterial.UnitPriceStandard"].ToString() ) ;
					decimal dUnitPriceNatural = Convert.ToDecimal ( drMaterialList["WH_AdjustInMaterial.UnitPriceNatural"].ToString() ) ;

					decimal dSumPriceStandard = dAdjustInQuantity * dUnitPriceStandard ;
					dAdjustInSumPriceStandard += dSumPriceStandard ;

					decimal dSumPriceNatural = dAdjustInQuantity * dUnitPriceNatural ;
					dAdjustInSumPriceNatural += dSumPriceNatural ;

					drMaterialList["WH_AdjustInMaterial.SumPriceStandard"] = dSumPriceStandard ;
					drMaterialList["WH_AdjustInMaterial.QuantityInBin"] = dAdjustInQuantity ;
				}
			}

			foreach ( DataRow drInventoryList in dtInventoryList.Rows )
			{
				if (drInventoryList.RowState != DataRowState.Deleted)
				{
					decimal dAdjustInQuantity = Convert.ToDecimal ( drInventoryList["WH_AdjustInMaterial.AdjustInQuantity"].ToString() ) ;
					decimal dUnitPriceStandard = Convert.ToDecimal ( drInventoryList["WH_AdjustInMaterial.UnitPriceStandard"].ToString() ) ;
					decimal dUnitPriceNatural = Convert.ToDecimal ( drInventoryList["WH_AdjustInMaterial.UnitPriceNatural"].ToString() ) ;

					decimal dSumPriceStandard = dAdjustInQuantity * dUnitPriceStandard ;
					dAdjustInSumPriceStandard += dSumPriceStandard ;

					decimal dSumPriceNatural = dAdjustInQuantity * dUnitPriceNatural ;
					dAdjustInSumPriceNatural += dSumPriceNatural ;

					drInventoryList["WH_AdjustInMaterial.SumPriceStandard"] = dSumPriceStandard ;
				}
			}

			dtAdjustIN.Rows[0]["WH_AdjustIN.TotalPriceStandard"] = dAdjustInSumPriceStandard ;
			dtAdjustIN.Rows[0]["WH_AdjustIN.TotalPriceNatural"] = dAdjustInSumPriceNatural ;
		}

		#endregion

		#region  ҵ�������֤		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtchild1"></param>
		/// <param name="dtchild2"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtchild)
		{
			foreach( DataRow dr in dtchild.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoMaterialSelected" ;			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dtChild)
		{
			string sErrMsg = "";
			if(dtChild.Rows.Count >0)
			{
				foreach(DataRow row in dtChild.Rows)
				{
					if(row.RowState != DataRowState.Deleted)
					{
						//�ⷿ����
						decimal daeQuantityInBin = Convert.ToDecimal( row["WH_AdjustInMaterial.QuantityInBin"] == DBNull.Value ? "0" : row["WH_AdjustInMaterial.QuantityInBin"].ToString());
						// ��Ӯ����
                        decimal daeAdjustInQuantity = Convert.ToDecimal(row["WH_AdjustInMaterial.AdjustInQuantity"] == DBNull.Value ? "0" : row["WH_AdjustInMaterial.AdjustInQuantity"].ToString());
						//��Ӯ����ӦС�ڿⷿ����
						if(daeQuantityInBin < daeAdjustInQuantity)
						{
							sErrMsg ="ErrQuantity";
							break;
						}
					}
				}
			}
			return sErrMsg;
		}

		#endregion
	}
}
