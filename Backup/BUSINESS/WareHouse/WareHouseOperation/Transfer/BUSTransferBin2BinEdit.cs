using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSTransferBin2BinEdit 的摘要说明。
	/// </summary>
	public class BUSTransferBin2BinEdit :BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSTransferBin2BinEdit()
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
			foreach(DataRow row in dtChild.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(row["WH_TransferBin2BinMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(row["WH_TransferBin2BinMaterial.UnitPriceNatural"].ToString());
					decimal decTransferQuantity =  Convert.ToDecimal(row["WH_TransferBin2BinMaterial.TransferQuantity"].ToString());
					decTotalAmountStandard += decUnitPriceStandard * decTransferQuantity;
					decTotalAmountNatural += decUnitPriceNatural * decTransferQuantity ;

				}
			}
		
			dtEdit.Rows[0]["WH_TransferBin2Bin.TotalPriceStandard"] = decTotalAmountStandard;
			dtEdit.Rows[0]["WH_TransferBin2Bin.TotalPriceNatural"] = decTotalAmountNatural; 

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
				foreach(DataRow drBorrowMaterial in dtChild.Rows)
				{
					if(drBorrowMaterial.RowState != DataRowState.Deleted)
					{
						//库房数量
						decimal iTransQuan = Convert.ToDecimal(drBorrowMaterial["WH_TransferBin2BinMaterial.TransferQuantity"].ToString());
						//借料数量
						decimal iTransQuanOld = Convert.ToDecimal(drBorrowMaterial["WH_TransferBin2BinMaterial.TransferQuantityOld"].ToString());
						//借料数量应小于库房数量
						if(iTransQuan > iTransQuanOld)
						{
							sErrMsg ="ErrQuantity";
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
