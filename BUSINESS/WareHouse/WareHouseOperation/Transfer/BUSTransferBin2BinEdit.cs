using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSTransferBin2BinEdit ��ժҪ˵����
	/// </summary>
	public class BUSTransferBin2BinEdit :BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSTransferBin2BinEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region  ҵ���߼�����
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

		#region  ҵ�������֤
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
						//�ⷿ����
						decimal iTransQuan = Convert.ToDecimal(drBorrowMaterial["WH_TransferBin2BinMaterial.TransferQuantity"].ToString());
						//��������
						decimal iTransQuanOld = Convert.ToDecimal(drBorrowMaterial["WH_TransferBin2BinMaterial.TransferQuantityOld"].ToString());
						//��������ӦС�ڿⷿ����
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
