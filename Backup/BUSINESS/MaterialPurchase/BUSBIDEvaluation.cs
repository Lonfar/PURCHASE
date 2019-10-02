using System;
using DataEntity;
using System.Data;


namespace Business
{
	/// <summary>
	/// BUSMaterialRequest ��ժҪ˵����
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
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��֤�����ӱ��н��ϵ������Ƿ���ڿ������
		/// </summary>
		/// <param name="dtBIDEvaluation">Edit��</param>
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

			#region modified by wanglijie on 2008-01-16 �״α��治��һ��Ҫѡ��Award=true
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
		/// �ύ��¼����ʱ����Ҫ��֤Award=true
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
		/// �Ƿ���SeePerson���д���
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

		/// <param name="sCurrentUserID">��½���</param>
		/// <returns>popedomDepID</returns>

		public string GetUserDepartmentID(string sCurrentUserID)
		{
			string popedomDepID=string.Empty;
			//����Ȩ�޵Ŀ���
			if(daeMR.GetAllDepartmentID(sCurrentUserID) != null)
			{
				//ȡ�õ�½�ߵ����ڲ��ŵ�ID�����ܰ������֣�һ�����ǲ��ŵ��쵼��һ�������ǲ��ŵ��쵼��
				DataSet ds = daeMR.GetAllDepartmentID(sCurrentUserID);
				
				int nCount = ds.Tables.Count;
				//����Щ�����н���ѭ��
				for(int i=0;i<nCount;i++ )
				{
					int k = ds.Tables[i].Rows.Count;
					for(int j = 0; j<k ;j++)
					{
						//ѭ����ʱ����ds.Tables[i].Rows[j][1].ToString()=="1" �����Ǵ˲��ŵ�����
						if(ds.Tables[i].Rows[j][1] != System.DBNull.Value && ds.Tables[i].Rows[j][1].ToString() == "1")
						{
							//ѭ������DepID
							popedomDepID += ","+ds.Tables[i].Rows[j][0].ToString();
						}
					}
				}
			}
			return popedomDepID;
		}


		#region ��ȡ�����ӡ��������

		/// <summary>
		/// ��ȡ�����ӡ��������
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

		// Add by ZZH on 2008-1-18 �����֤�Ƿ����ɾ���ķ���
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

		//  Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ������ڵ㱻��һ�ڵ�����ʱ����Ӧ��ɾ��
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
