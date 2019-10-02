using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSBorrowEdit 的摘要说明。
	/// </summary>
	public class BUSBorrowEdit:BUSBase
	{
		public BUSBorrowEdit()
		{

		}


		#region  业务逻辑运算

		/// <summary>
		/// 计算借料核算和本位总额
		/// </summary>
		/// <param name="dtBorrowEdit">Edit表</param>
		/// <param name="decTotalAmountStandard">核算总额</param>
		/// <param name="decTotalAmountNatural">本位总额</param>
		/// <returns></returns>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal decTotalAmountStandard = 0.0m ;
			decimal decTotalAmountNatural = 0.0m ;
			foreach(DataRow row in dtChild.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(row["WH_BorrowMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(row["WH_BorrowMaterial.UnitPriceNatural"].ToString());
					decimal decQuantityBorrow =  Convert.ToDecimal(row["WH_BorrowMaterial.QuantityBorrow"].ToString());
					decTotalAmountStandard += decUnitPriceStandard*decQuantityBorrow;
					decTotalAmountNatural += decUnitPriceNatural*decQuantityBorrow;
				}
			}
			dtEdit.Rows[0]["WH_Borrow.TotalPriceStandard"] = decTotalAmountStandard;
			dtEdit.Rows[0]["WH_Borrow.TotalPriceNatural"] = decTotalAmountNatural; 

		}

		#endregion

		#region  业务规则验证
		/// <summary>
		/// 验证借料子表中借料的数量是否大于库存数量
		/// </summary>
		/// <param name="dtBorrowMaterial">子表</param>
		/// <returns>sErrMsg</returns>
		public string CheckChildData(DataTable dtBorrowMaterial)
		{
			string sErrMsg = "";
			foreach(DataRow drBorrowMaterial in dtBorrowMaterial.Rows)
			{
				if(drBorrowMaterial.RowState != DataRowState.Deleted)
				{
						//库房数量
						decimal decQuantityInBin = Convert.ToDecimal(drBorrowMaterial["WH_BorrowMaterial.QuantityInBin"].ToString());
						//借料数量
						decimal decQuantityBorrow = Convert.ToDecimal(drBorrowMaterial["WH_BorrowMaterial.QuantityBorrow"].ToString());
						//借料数量应小于库房数量
						if(decQuantityBorrow > decQuantityInBin)
						{
							sErrMsg ="CheckErrMsg1";
							break;
						}
				 }
			}
			return sErrMsg;
		}


		/// <summary>
		/// 验证借料子表中借料的数量是否大于库存数量
		/// </summary>
		/// <param name="dt">Edit表</param>
		/// <returns>sErrMsg</returns>
		public string CheckChildRows(DataTable dtBorrowMaterial)
		{
			foreach(DataRow dr in dtBorrowMaterial.Rows)
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
