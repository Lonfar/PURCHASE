using System;
using System.Data;
using DataEntity;

namespace Business
{
	/// <summary>
	/// BUSTenderEvaluation 的摘要说明。
	/// </summary>
	public class BUSTenderEvaluation:BUSBase
	{
		DAETenderEvaluation _daeTenderEvaluation = new DAETenderEvaluation();
		public BUSTenderEvaluation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
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

		//  Add by ZZH on 2008-1-21 添加验证是否可以删除的方法当节点被下一节点引用时，不应被删除
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
		/// 是否在SeePerson表中存在
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
		/// 是否已经存在
		/// </summary>
		/// <param name="strIDKey">人员</param>
		/// <param name="dt_Temp">数据表</param>
		/// <param name="strIDKeyName">主键列名</param>
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
