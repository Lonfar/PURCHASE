using System;
using DataEntity;
using System.Data;


namespace Business
{
	/// <summary>
	/// ����׫д��ҵ���߼��� Added by Liujun at 11.30
	/// </summary>
	public class BUSITBDocumentWrite : BUSBase
	{
		DAEITBDocumentWrite _daeITBDocumentWrite = new DAEITBDocumentWrite() ; 
		/// <summary>
		/// 
		/// </summary>
		public BUSITBDocumentWrite()
		{
		
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public bool CheckState(String strTenderID , TenderState state)
		{
			DataTable dt = _daeITBDocumentWrite.CheckState(strTenderID);
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
			DataTable dt = _daeITBDocumentWrite.GetRecord(strPKValue) ;
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
				if ( IDKey == dr["ITBDocumentViewer.ViewerID"].ToString()) return true;
			}
			return false;
		}
	}
}
