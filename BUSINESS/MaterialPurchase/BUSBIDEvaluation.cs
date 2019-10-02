using System;
using DataEntity;
using System.Data;


namespace Business
{
	/// <summary>
	/// BUSMaterialRequest 的摘要说明。
	/// </summary>
	public class BUSBIDEvaluation : BUSBase
	{
		DAEServiceRequistion daeMR = new DAEServiceRequistion();
		DAEBIDEvaluation daeBIDEvaluation = new DAEBIDEvaluation();
		CEntityUitlity cEntityUitlity = new CEntityUitlity();
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		/// <summary>
		/// 
		/// </summary>
		public BUSBIDEvaluation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 验证借料子表中借料的数量是否大于库存数量
		/// </summary>
		/// <param name="dtBIDEvaluation">Edit表</param>
		/// <returns>sErrMsg</returns>
		public string CheckChildRows(DataTable dtBIDEvaluation)
		{
			string sErrMsg = "";
			DataTable dtCheckRow = dtBIDEvaluation.Copy();
			dtCheckRow.AcceptChanges();
			if(dtCheckRow.Rows.Count == 0)
			{
				sErrMsg= "NoBIDSummary" ;
				
				return sErrMsg;
			}

			#region modified by wanglijie on 2008-01-16 首次保存不必一定要选择Award=true
//			else
//			{
//				
//				foreach( DataRow row in dtCheckRow.Rows)
//				{
//					if( row["MR_BIDSummary.ISAwarded"].Equals(true))
//					{
//						isAwarded = 1 ;
//						break ;
//					}
//				}
//				if(isAwarded == 0)
//				{
//					sErrMsg = "NoISAwarded" ; 
//					return sErrMsg ;
//				}
//			}
			#endregion

			return "";
		}


		/// <summary>
		/// add by wanglijie on 2008-01-16
		/// 提交记录审批时，需要验证Award=true
		/// </summary>
		/// <param name="dtBIDEvaluation"></param>
		/// <returns></returns>
		public string IsAwarded(DataTable dtBIDEvaluation)
		{
			int isAwarded = 0 ;
			string sErrMsg = "";
			foreach( DataRow row in dtBIDEvaluation.Rows)
			{
				if( row["MR_BIDSummary.ISAwarded"].Equals(true))
				{
					isAwarded = 1 ;
					break ;
				}
			}
			if(isAwarded == 0)
			{
				sErrMsg = "NoISAwarded" ; 				
			}
			return sErrMsg ;
		}


		/// <summary>
		/// 是否在SeePerson表中存在
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtSeePerson )
		{
			foreach(DataRow dr in dtSeePerson.Rows)
			{
				if ( IDKey == dr["MR_Viewer.ViewerName"].ToString()) return true;
			}
			return false;
		}

		/// <param name="sCurrentUserID">登陆编号</param>
		/// <returns>popedomDepID</returns>

		public string GetUserDepartmentID(string sCurrentUserID)
		{
			string popedomDepID=string.Empty;
			//数据权限的控制
			if(daeMR.GetAllDepartmentID(sCurrentUserID) != null)
			{
				//取得登陆者的所在部门的ID（可能包含两种：一种他是部门的领导，一种他不是部门的领导）
				DataSet ds = daeMR.GetAllDepartmentID(sCurrentUserID);
				
				int nCount = ds.Tables.Count;
				//在这些部门中进行循环
				for(int i=0;i<nCount;i++ )
				{
					int k = ds.Tables[i].Rows.Count;
					for(int j = 0; j<k ;j++)
					{
						//循环的时候发现ds.Tables[i].Rows[j][1].ToString()=="1" 表明是此部门的主管
						if(ds.Tables[i].Rows[j][1] != System.DBNull.Value && ds.Tables[i].Rows[j][1].ToString() == "1")
						{
							//循环加上DepID
							popedomDepID += ","+ds.Tables[i].Rows[j][0].ToString();
						}
					}
				}
			}
			return popedomDepID;
		}


		#region 获取评标打印单据数据

		/// <summary>
		/// 获取评标打印单据数据
		/// </summary>
		/// <param name="sBIDEvaluationID"></param>
		/// <returns></returns>
		public DataTable GetPrintData ( string sBIDEvaluationID )
		{
			string sSelectSql = "SELECT * FROM v_Report_BIDEvaluationPrint WHERE BIDEvaluationID = '"+sBIDEvaluationID+"'";
			string sErrorMsg = string.Empty;

			DataTable dtData = this._da.GetDataTable ( sSelectSql );

			return dtData;
		}

		#endregion

		// Add by ZZH on 2008-1-18 添加验证是否可以删除的方法
		public bool CheckState(string strMREnquiryPrice , MRState state)
		{
			DataTable dt = daeBIDEvaluation.CheckState(strMREnquiryPrice);
			int strState = -1 ; 
			int intState = (int)state ; 
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["CheckState"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CheckState"]);
			}
			return  strState >= intState ;

		}

		//  Add by ZZH on 2008-1-21 添加验证是否可以删除的方法当节点被下一节点引用时，不应被删除
		public bool CheckDeleteRecord(String strPKValue , MRState state )
		{
			int strState = -1 ; 
			int intState = (int)state ; 
			DataTable dt = daeBIDEvaluation.GetRecord(strPKValue) ;
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["State"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["State"]);
			}
			return  strState <= intState ;
		}
		//*********************************************************
	}
}
