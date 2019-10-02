using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace Business
{
	/// <summary>
	/// BUSAdjustOutEdit 的摘要说明。
	/// </summary>
	public class BUSAdjustOutEdit : BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSAdjustOutEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal decTotalAmountStandard = 0.0m ;
			decimal decTotalAmountNatural = 0.0m ;
			foreach(DataRow drChild in dtChild.Rows)
			{
				if (drChild.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(drChild["WH_AdjustOutMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(drChild["WH_AdjustOutMaterial.UnitPriceNatural"].ToString());
					decimal decQuantityReject =  Convert.ToDecimal(drChild["WH_AdjustOutMaterial.QuantityReject"].ToString());
					decTotalAmountStandard += decUnitPriceStandard * decQuantityReject;
					decTotalAmountNatural += decUnitPriceNatural * decQuantityReject ;
				}
			}
			dtEdit.Rows[0]["WH_AdjustOut.TotalPriceStandard"] = decTotalAmountStandard;
			dtEdit.Rows[0]["WH_AdjustOut.TotalPriceNatural"] = decTotalAmountNatural; 
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtEdit"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dtEdit)
		{
			string errMessage = "";
			foreach (DataRow drChild in dtEdit.Rows)
			{
				if ( drChild.RowState != DataRowState.Deleted)
				{
					//判断物料单位子表库存数量
					Decimal iTransQuan = Decimal.Parse(drChild["WH_AdjustOutMaterial.QuantityReject"].ToString()); 
					Decimal iTransQuanOld = Decimal.Parse(drChild["WH_AdjustOutMaterial.QuantityInBin"].ToString()) ; 
					if (iTransQuan > iTransQuanOld)
					{
						errMessage = "ErrQuantity";
						break;
					}
				}
			}
			return errMessage;
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

	}
}
