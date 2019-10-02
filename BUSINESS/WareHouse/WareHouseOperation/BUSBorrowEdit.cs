using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSBorrowEdit ��ժҪ˵����
	/// </summary>
	public class BUSBorrowEdit:BUSBase
	{
		public BUSBorrowEdit()
		{

		}


		#region  ҵ���߼�����

		/// <summary>
		/// ������Ϻ���ͱ�λ�ܶ�
		/// </summary>
		/// <param name="dtBorrowEdit">Edit��</param>
		/// <param name="decTotalAmountStandard">�����ܶ�</param>
		/// <param name="decTotalAmountNatural">��λ�ܶ�</param>
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

		#region  ҵ�������֤
		/// <summary>
		/// ��֤�����ӱ��н��ϵ������Ƿ���ڿ������
		/// </summary>
		/// <param name="dtBorrowMaterial">�ӱ�</param>
		/// <returns>sErrMsg</returns>
		public string CheckChildData(DataTable dtBorrowMaterial)
		{
			string sErrMsg = "";
			foreach(DataRow drBorrowMaterial in dtBorrowMaterial.Rows)
			{
				if(drBorrowMaterial.RowState != DataRowState.Deleted)
				{
						//�ⷿ����
						decimal decQuantityInBin = Convert.ToDecimal(drBorrowMaterial["WH_BorrowMaterial.QuantityInBin"].ToString());
						//��������
						decimal decQuantityBorrow = Convert.ToDecimal(drBorrowMaterial["WH_BorrowMaterial.QuantityBorrow"].ToString());
						//��������ӦС�ڿⷿ����
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
		/// ��֤�����ӱ��н��ϵ������Ƿ���ڿ������
		/// </summary>
		/// <param name="dt">Edit��</param>
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
