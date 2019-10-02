using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSMaxMinMaterialEdit ��ժҪ˵����
	/// </summary>
	public class BUSMaxMinMaterialEdit : BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSMaxMinMaterialEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		public string CheckMaxMinCapacity(DataTable dtChild)
		{
			string errMessage = string.Empty;
			string  strMaxCapacity = dtChild.Rows[0]["MaxMinMaterial.MaxCapacity"].ToString(); 
			string strMinCapacity = dtChild.Rows[0]["MaxMinMaterial.MinCapacity"].ToString();

			if( strMaxCapacity != string.Empty && strMinCapacity != string.Empty )
			{
				decimal decMax = Convert.ToDecimal(strMaxCapacity.ToString());
				decimal decMin = Convert.ToDecimal(strMinCapacity.ToString());

				if( decMin > decMax )
				{
					errMessage = "MaxToMin";
				}
			}
			return errMessage;
		}



//Add by ZZH on 2008-1-11 ����֤�߼������༭ʱ�����޸���ҵ���������Բ���Ҫ����֤�߼���
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
//		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
//		{
//			// ������Ϣ
//			string sErrorMsg = string.Empty;
//
//			if ( dt.Rows.Count > 0 )
//			{
//				// ���ȶԿ��ݽ����ж� ��С����<������
//				if ( Convert.ToDecimal( dt.Rows[0]["MaxMinMaterial.MaxCapacity"] ) <= Convert.ToDecimal( dt.Rows[0]["MaxMinMaterial.MinCapacity"] ) )
//				{
//					sErrorMsg = "MaxToMin";
//				}
//
//				if ( sErrorMsg.Length == 0 )
//				{
//					// ͬһ�ⷿ��ItemCode�������ظ�
//					DataEntity.DAEMaxMinMaterialEdit dataEntity = this.IEntity as DataEntity.DAEMaxMinMaterialEdit;
//
//					int iNum = dataEntity.ExistsItemCode ( dt.Rows[0]["MaxMinMaterial.ItemCode"].ToString() , dt.Rows[0]["MaxMinMaterial.WHID"].ToString() );
//					if ( iNum > 0)
//					{
//						sErrorMsg = "ItemCode";				
//					}
//				}
//			}
//
//			return sErrorMsg;
//		}
//*********************************************
	}
}
