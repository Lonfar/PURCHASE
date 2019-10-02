using System;
using Cnwit.Utility;
using Common;
using System.Data;
namespace DataEntity
{
	/// <summary>
	/// DAETCMeetingReport 的摘要说明。
	/// Added by QSQ 12.02
	/// </summary>
	public class DAETCMeetingReport:DAEBase
	{
		// Add by ZZH on 2008-1-11 如果是MR状态要回置到MR 所以实例化这类，以调用公共函数
		private CEntityUitlity pEntityUitlity = new CEntityUitlity();
		//******************************************************************************
		DataAcess _da = GetProjectDataAcess.GetDataAcess();
		public DAETCMeetingReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 通过ID得到TCMeetingReport的记录
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

		#region 更新状态

		#region 更新策略的状态
		/// <summary>
		/// 更新策略状态
		/// </summary>
		/// <param name="state">状态</param>
		/// <param name="strTenderID">策略ID</param>
		/// <returns>错误信息</returns>
		public string UpdateStrategyState ( string strTenderIDKey , TenderState state )
		{
			// Add by ZZH on 2008-1-11 如果是MR状态要回置到MR 所以实例化这类，以调用公共函数
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
//			/* 更新TC策略得同时,同步更新SR */
//			string UpdateSql_SR = "Update ServiceRequistion set ServiceRequistion.TenderState = "+iState+" WHERE ServiceRequistion.IDKey = (SELECT TCStrategy.SRIDKey From TCStrategy WHERE  TCStrategy.TenderID = '"+strTenderIDKey+"' )";
//			strErrorMsg +=  _da.ExecuteDMLSQL ( UpdateSql_SR );
//			// ==================================================== //

			return strErrorMsg;
			//********************************************************************************
		}

		#endregion

		#region  更新SR的状态

		/// <summary>
		/// 更新SR的IDKey
		/// </summary>
		/// <param name="strPutInIDKey">提交IDKey</param>
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

		#region 更新提交表中的状态

		/// <summary>
		/// 更新提交表的状态,在这里更新为1
		/// </summary>
		/// <param name="strPutInIDKey">提交IDKey</param>
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

		#region 获得指定提交记录的对象类型 Added by Liujun at 12..21

		/// <summary>
		/// 获得指定提交记录的对象信息
		/// </summary>
		/// <param name="strPutInIDKey">提交ID</param>
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
