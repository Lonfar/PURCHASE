using System;
using Cnwit.Utility;
using Common;
using System.Data;
namespace DataEntity
{
	/// <summary>
	/// DAETCMeetingReport ��ժҪ˵����
	/// Added by QSQ 12.02
	/// </summary>
	public class DAETCMeetingReport:DAEBase
	{
		// Add by ZZH on 2008-1-11 �����MR״̬Ҫ���õ�MR ����ʵ�������࣬�Ե��ù�������
		private CEntityUitlity pEntityUitlity = new CEntityUitlity();
		//******************************************************************************
		DataAcess _da = GetProjectDataAcess.GetDataAcess();
		public DAETCMeetingReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ͨ��ID�õ�TCMeetingReport�ļ�¼
		/// </summary>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		public DataTable getTCMeetingReport(string pkValue)
		{
			DataTable dt = new DataTable();
			string sql = "select * from TCMeetingReport where ID = '"+pkValue+"'";
			dt = _da.GetDataTable(sql);
			return dt;
		}

		#region ����״̬

		#region ���²��Ե�״̬
		/// <summary>
		/// ���²���״̬
		/// </summary>
		/// <param name="state">״̬</param>
		/// <param name="strTenderID">����ID</param>
		/// <returns>������Ϣ</returns>
		public string UpdateStrategyState ( string strTenderIDKey , TenderState state )
		{
			// Add by ZZH on 2008-1-11 �����MR״̬Ҫ���õ�MR ����ʵ�������࣬�Ե��ù�������
			string strErrorMsg = pEntityUitlity.UpdateStrategyState(strTenderIDKey , state ) ; 
//			int iState = Convert.ToInt32 ( state );
//			
//			string strErrorMsg = string.Empty;
//
//			//string UpdateSql = "UPDATE TCStrategy Set Status = "+iState+" WHERE TenderID = ( SELECT ObjectiveID FROM PutIn Where IDKey = '"+strPutInIDKey+"')";
//			string UpdateSql = "UPDATE TCStrategy Set Status = "+iState+" WHERE TenderID = '"+strTenderIDKey+"'";
//
//			strErrorMsg = _da.ExecuteDMLSQL( UpdateSql );
//
//			// ========= Modified by Liujun at 11.29 ================= //
//			/* ����TC���Ե�ͬʱ,ͬ������SR */
//			string UpdateSql_SR = "Update ServiceRequistion set ServiceRequistion.TenderState = "+iState+" WHERE ServiceRequistion.IDKey = (SELECT TCStrategy.SRIDKey From TCStrategy WHERE  TCStrategy.TenderID = '"+strTenderIDKey+"' )";
//			strErrorMsg +=  _da.ExecuteDMLSQL ( UpdateSql_SR );
//			// ==================================================== //

			return strErrorMsg;
			//********************************************************************************
		}

		#endregion

		#region  ����SR��״̬

		/// <summary>
		/// ����SR��IDKey
		/// </summary>
		/// <param name="strPutInIDKey">�ύIDKey</param>
		/// <param name="state"></param>
		/// <returns></returns>
		public string UpdateTenderState ( string strPutInIDKey , TenderState state )
		{
			int iState = Convert.ToInt32 ( state );

			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE ServiceRequistion Set SRState = "+iState+" WHERE IDKey = ( SELECT ObjectiveID FROM PutIn Where IDKey = '"+strPutInIDKey+"')";

			strErrorMsg = _da.ExecuteDMLSQL( UpdateSql );

			return strErrorMsg ;
		}

		#endregion

		#region �����ύ���е�״̬

		/// <summary>
		/// �����ύ���״̬,���������Ϊ1
		/// </summary>
		/// <param name="strPutInIDKey">�ύIDKey</param>
		/// <param name="iState">1</param>
		/// <returns></returns>
		public string UpdatePutInState ( string strPutInIDKey , int iState )
		{
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE PutIn SET State = "+iState+" WHERE IDKey = '"+strPutInIDKey+"'";

			strErrorMsg = _da.ExecuteDMLSQL( UpdateSql );

			return strErrorMsg;
		}

		#endregion

		#region ���ָ���ύ��¼�Ķ������� Added by Liujun at 12..21

		/// <summary>
		/// ���ָ���ύ��¼�Ķ�����Ϣ
		/// </summary>
		/// <param name="strPutInIDKey">�ύID</param>
		/// <returns></returns>
		public System.Collections.Hashtable GetObjectiveInfo ( string strPutInIDKey )
		{
			string strType = string.Empty;
			System.Collections.Hashtable hashtable = new System.Collections.Hashtable(2);

			string SelectSql = "SELECT ObjectiveType , ObjectiveID FROM PutIn WHERE IDKey = '"+strPutInIDKey+"'" ;

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );

			if ( dt_Temp.Rows.Count > 0 )
			{
				hashtable.Add( "ObjectiveType" , dt_Temp.Rows[0]["ObjectiveType"].ToString() );
				hashtable.Add( "ObjectiveID" , dt_Temp.Rows[0]["ObjectiveID"].ToString() );
			}

			return hashtable;
		}

		#endregion

		#endregion
	}
}
