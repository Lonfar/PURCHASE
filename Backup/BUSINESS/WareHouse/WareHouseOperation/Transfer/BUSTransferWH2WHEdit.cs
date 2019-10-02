/*
 * Create nongbin 2007-06-28
 * 
 * ���ڿ��ת�ϵ�ҳ��
 * */
using System;
using System.Data;
using DataEntity;


namespace Business
{
	/// <summary>
	/// BUSTransferWH2WHEdit ��ժҪ˵����
	/// </summary>
	public class BUSTransferWH2WHEdit:BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSTransferWH2WHEdit()
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
					decimal decUnitPriceStandard =  Convert.ToDecimal(row["WH_TransferWH2WHMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(row["WH_TransferWH2WHMaterial.UnitPriceNatural"].ToString());
					decimal decTransferQuantity =  Convert.ToDecimal(row["WH_TransferWH2WHMaterial.TransferQuantity"].ToString());
					decTotalAmountStandard += decUnitPriceStandard * decTransferQuantity;
					decTotalAmountNatural += decUnitPriceNatural * decTransferQuantity ;
				}
			}
			
			dtEdit.Rows[0]["WH_TransferWH2WH.TotalPriceStandard"] = decTotalAmountStandard;
			dtEdit.Rows[0]["WH_TransferWH2WH.TotalPriceNatural"] = decTotalAmountNatural; 
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
			if(dtChild.Rows.Count >0)
			{
				foreach(DataRow row in dtChild.Rows)
				{
					if(row.RowState != DataRowState.Deleted)
					{
						//�ⷿ����
						decimal decQuantityInBin = Convert.ToDecimal(row["WH_TransferWH2WHMaterial.TransferQuantity"].ToString());
						//��������
						decimal decQuantityBorrow = Convert.ToDecimal(row["WH_TransferWH2WHMaterial.QuantityInOldBin"].ToString());
						//��������ӦС�ڿⷿ����
						if(decQuantityBorrow < decQuantityInBin)
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
		

		#region ��ÿⷿID��ɸѡ����
		/// <summary>
		/// ��ÿⷿID��ɸѡ����
		/// </summary>
		public string GetWHIDFilter ()
		{
			// ��λ����ʵ����
			DataEntity.DAEBIBIN dAEBIBIN = new DataEntity.DAEBIBIN();
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
			DataEntity.DAEBIBIN dAEBIBIN = new DataEntity.DAEBIBIN();
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

		
		#region ��дCheckBusinessData(),��� ת���������ܴ��ڿ������
		
		/// <summary>
		/// ��дCheckBusinessData(),��� ת���������ܴ��ڿ������
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessData(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			string sErrorMsg = string.Empty;

			switch ( this.IEntity.TableName )
			{
				case "WH_TransferWH2WH" :
				{
					// У����������
					break;
				}
				case "WH_TransferWH2WHMaterial" :
				{
					if(CheckIsTransferQuantityTooLarge(dt,fieldsList) == true)
					{
						sErrorMsg = fieldsList[0].ToString();	//û�ж���
					}
					break;
				}
			}

			if ( sErrorMsg.Length == 0 )
			{
				sErrorMsg = base.CheckBusinessData ( dt , fieldsList );
			}

			return sErrorMsg;
		}


		private bool CheckIsTransferQuantityTooLarge(DataTable dt,System.Collections.ArrayList fieldsList)
		{
			//�õ�һ������ת������dt
			//��ʼѭ��
			DAETransferWH2WHEdit DAETransferWH2WHEdit = new DAETransferWH2WHEdit(); 
			bool flag = false;
			
			if(dt.Rows.Count < 1) return false;						//���û���У��򷵻�
			
			foreach ( DataRow dr in dt.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					string  sql = @"select QuantityInBin 
								from WH_InStoreMaterialDetail 
				where InStockMaterialID = '"+dr["WH_TransferWH2WHMaterial.InStockMaterialID"].ToString()+
						"' and BINID = '"+dr["WH_TransferWH2WHMaterial.BINIDOld"].ToString()+
						"' and POID = '"+dr["WH_TransferWH2WHMaterial.POID"].ToString() +"'";

				
					DataTable dtQuantityInBin = DAETransferWH2WHEdit.CheckIsTransferQuantity(sql);
			
				
					if(dtQuantityInBin.Rows.Count < 1)	continue;		//������ص�"���������ϸ��"��ļ�¼Ϊ��

					Decimal TransferQuantity = Convert.ToDecimal(dr["WH_TransferWH2WHMaterial.TransferQuantity"]);
					Decimal QuantityInBin = Convert.ToDecimal(dtQuantityInBin.Rows[0]["QuantityInBin"]);

				
					//���dataTable��ǰѭ����>��"���������ϸ��"ȡ�õ� ��λ����
					if(TransferQuantity > QuantityInBin)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		#endregion

	}
}
