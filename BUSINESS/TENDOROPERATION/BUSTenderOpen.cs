using System;
using System.Data;
using DataEntity;
namespace Business
{
	/// <summary>
	/// BUSTenderOpen Added by QSQ 12.11
	/// </summary>
	public class BUSTenderOpen : BUSBase
	{
		
		private DAETenderOpen dataEntity = new DAETenderOpen();
		public BUSTenderOpen()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public bool CheckState(String strTenderID , TenderState state)
		{
			DataTable dt = dataEntity.CheckState(strTenderID);
			int strState = -1 ; 
			int intState = (int)state ; 
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["CheckState"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CheckState"]);
			}
			return  strState >= intState ;

		}

		//  Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ������ڵ㱻��һ�ڵ�����ʱ����Ӧ��ɾ��
		public bool CheckDeleteRecord(String strPKValue , TenderState state )
		{
			int strState = -1 ; 
			int intState = (int)state ; 
			DataTable dt = dataEntity.GetRecord(strPKValue) ;
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["State"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["State"]);
			}
			return  strState <= intState ;
		}
		//*********************************************************

		/// <summary>
		/// �Ƿ���SeePerson���д���
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtSeePerson )
		{
			foreach(DataRow dr in dtSeePerson.Rows)
			{
				if ( IDKey == dr["BidOpenPerson.ViewerID"].ToString()) return true;
			}
			return false;
		}

		public bool CheckIsSR(String sTenderID)
		{
			DataTable dt = dataEntity.GetType(sTenderID);
			String ifSR = dt.Rows[0]["MRTypeID"].ToString();
			return ifSR.Equals("2");
		}
	}
}
