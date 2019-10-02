using System;
using System.Data;
using DataEntity;

namespace Business
{
	/// <summary>
	/// BUSTenderEvaluation ��ժҪ˵����
	/// </summary>
	public class BUSTenderEvaluation:BUSBase
	{
		DAETenderEvaluation _daeTenderEvaluation = new DAETenderEvaluation();
		public BUSTenderEvaluation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public bool CheckState(String strTenderID , TenderState state)
		{
			DataTable dt = _daeTenderEvaluation.CheckState(strTenderID);
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
			DataTable dt = _daeTenderEvaluation.GetRecord(strPKValue) ;
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
				if ( IDKey == dr["TechEvaluationView.ViewerID"].ToString()) return true;
			}
			return false;
		}

		/// <summary>
		/// �Ƿ��Ѿ�����
		/// </summary>
		/// <param name="strIDKey">��Ա</param>
		/// <param name="dt_Temp">���ݱ�</param>
		/// <param name="strIDKeyName">��������</param>
		/// <returns></returns>
		public bool CheckExist ( string strIDKey , DataTable dt_Temp , string strIDKeyName )
		{
			foreach ( DataRow dr in dt_Temp.Rows )
			{
				if ( strIDKey == dr[strIDKeyName].ToString() )return true;
			}
			return false;
		}

        public bool CheckTechGroupExist(string IDKey, DataTable dtTechEvaluationGroup)
        {
            foreach (DataRow dr in dtTechEvaluationGroup.Rows)
            {
                if (IDKey == dr["TechEvaluationGroup.personID"].ToString()) return true;
            }
            return false;
        }
	}
}
