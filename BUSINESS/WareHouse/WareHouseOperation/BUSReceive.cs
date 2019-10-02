using System;
using DataEntity;
using System.Data;
using System.Collections;

namespace Business.WareHouseManagment
{
	/// <summary>
	/// ����ҵ���߼��� Liujun Add at 2007-6-22
	/// </summary>
	public class BUSReceive : BUSBase
	{
		/// <summary>
		/// ����ҵ���߼���
		/// </summary>
		public BUSReceive()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��ö�����ŵ�ɸѡ

		/// <summary>
		/// ��ö�����ŵ�ɸѡ����
		/// </summary>
		/// <returns></returns>
		public string GetPOIDFilter ()
		{
			// ����PO״̬ΪOpen��.
			return " PurchaseOrder.Status = "+ (int)POReceiveState.State_Open;
		}

		#endregion

		#region ��ÿⷿID��ɸѡ����

		/// <summary>
		/// ��ÿⷿID��ɸѡ����
		/// </summary>
		/// <returns></returns>
		public string GetWHIDFilter ()
		{
			// ��λ����ʵ����
			DataEntity.DAEBIBIN dAEBIBIN = new DAEBIBIN();
			string sFilter = string.Empty;

			// ��ѯ
			string sWHIDs = dAEBIBIN.GetWHHasBIN();
			if ( sWHIDs.Length > 0 )
			{
				sFilter = " WH_BI_WareHouse.WHID IN ( " + sWHIDs + ")";
			}

			return sFilter;
		}

		#endregion

		#region ��ÿ�λ��ɸѡ����

		/// <summary>
		/// ���ݿⷿ��ö�Ӧ��λ��ɸѡ����
		/// </summary>
		/// <param name="sWHID">WHID</param>
		/// <returns></returns>
		public string GetBINIDFilter ( string sWHID )
		{
			// ��λ����ʵ����
			DataEntity.DAEBIBIN dAEBIBIN = new DAEBIBIN();
			string sFilter = string.Empty;

			// ��ѯ
			string sBINIDs = dAEBIBIN.GetAllBINIDByWHID ( sWHID );
			if ( sBINIDs.Length > 0 )
			{
				sFilter = "WH_BI_BIN.BINID IN ( "+sBINIDs+" )";
			}

			return sFilter;
		}

		#endregion

		#region У�������߼�

		#region У��ʵ���������ۼ��Ƿ���ڿ�������(��ֹһ����Ʒ���ڶ����λ�϶�������������)

		/// <summary>
		/// У��ʵ���������ۼ��Ƿ���ڿ�������
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dt)
		{
			string sErrMsg = "";
			foreach(DataRow row in dt.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					//ʵ������
					decimal decFactReceivedQuantity = Convert.ToDecimal(row["WH_ReceiveMaterial.FactReceivedQuantity"].ToString());
					//��������
					decimal decCanReceivedQuantity = Convert.ToDecimal(row["WH_ReceiveMaterial.CanReceivedQuantity"].ToString());

					//ʵ������ӦС�ڿ�������
					if(decFactReceivedQuantity > decCanReceivedQuantity)
					{
						sErrMsg ="FactQuantityAboveCanReceiveQuantity";
						break;

					}
				}
			}	
			return sErrMsg;
		}


		#endregion

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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtEdit"></param>
		/// <param name="dtChild"></param>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			// ʵ�������ܶ�
			decimal mTotal = 0;
			decimal mTotalPriceNaturalER = 0;
			decimal mTotalPriceStandardlER = 0;
			
			// ����ӱ��д���ʵ��������������ܼ�
			foreach ( DataRow dr in dtChild.Rows )
			{
				//�Ա�λ�һ���
				decimal dTotalPriceNaturalER = Convert.ToDecimal ( dtEdit.Rows[0]["WH_Receive.TotalPriceNaturalER"].ToString() ) ;
				//�Ժ���һ���
				decimal dTotalPriceStandardlER = Convert.ToDecimal ( dtEdit.Rows[0]["WH_Receive.TotalPriceStandardlER"].ToString() ) ;
				if(dr.RowState != DataRowState.Deleted)
				{
					dr["WH_ReceiveMaterial.UnitPriceNatural"] = Convert.ToDecimal ( dr["WH_ReceiveMaterial.UnitPrice"] ) * dTotalPriceNaturalER;
					dr["WH_ReceiveMaterial.UnitPriceStandard"] = Convert.ToDecimal ( dr["WH_ReceiveMaterial.UnitPrice"] )  * dTotalPriceStandardlER;
					dr["WH_ReceiveMaterial.SumPrice"] = Convert.ToDecimal( dr["WH_ReceiveMaterial.UnitPrice"].ToString() ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() );
					mTotal += Convert.ToDecimal( dr["WH_ReceiveMaterial.SumPrice"].ToString() );
					mTotalPriceNaturalER += Convert.ToDecimal( dr["WH_ReceiveMaterial.UnitPriceNatural"].ToString() ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() ) ;
					mTotalPriceStandardlER += Convert.ToDecimal( dr["WH_ReceiveMaterial.UnitPriceStandard"].ToString() ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() ) ;
				}
			}
			dtEdit.Rows[0]["WH_Receive.TotalPrice"] = mTotal ;
			dtEdit.Rows[0]["WH_Receive.TotalPriceStandarCUR"] = mTotalPriceStandardlER ;
			dtEdit.Rows[0]["WH_Receive.TotalPriceNaturalCUR"] = mTotalPriceNaturalER ;			
		}		

	}

	#region ����ʵ��

	/// <summary>
	///  ����ʵ��
	/// </summary>
	public class ReceiveMaterialEntity
	{
		private string _ItemCode;

		/// <summary>
		/// ���ʱ���
		/// </summary>
		public string ItemCode
		{
			set { this._ItemCode = value; }
			get { return this._ItemCode; }
		}

		private double _CanReceivedQuantity;

		/// <summary>
		/// ��������
		/// </summary>
		public double CanReceivedQuantity
		{
			set { this._CanReceivedQuantity = value; }
			get { return this._CanReceivedQuantity; }
		}

		private double _FactReceiveQuantity;

		/// <summary>
		/// ʵ������
		/// </summary>
		public double FactReceiveQuantity
		{
			set { this._FactReceiveQuantity = value; }
			get { return this._FactReceiveQuantity; }
		}
	}

	#endregion
}
