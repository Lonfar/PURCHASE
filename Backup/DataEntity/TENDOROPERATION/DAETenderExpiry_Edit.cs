using System;
using System.Data;
using Common;
using Cnwit.Utility;

namespace DataEntity
{
	/// <summary>
	/// DAETender 的摘要说明。
	/// </summary>
	public class DAETenderExpiry_Edit: DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql = string.Empty;
		public DAETenderExpiry_Edit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// <summary>
		/// 更新标书的状态
		/// </summary>
		/// <param name="strITBDocumentID">标书的IDKey</param>
		/// <param name="State">状态，本处应该为3（开标阶段）</param>
		/// <returns>错误信息</returns>
		public string SetITBDocumentState ( string strITBDocumentID , int State)
		{
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE ITBDocument SET State = "+State+" WHERE ITBIDKey = '"+strITBDocumentID+"'" ;

			strErrorMsg =  _da.ExecuteDMLSQL ( UpdateSql );

			return strErrorMsg;
		}



		public string SetSRAndITBDocumentState ( string strITBDocumentID , int nState)
		{
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE ITBDocument SET State = "+nState+" WHERE ITBIDKey = '"+strITBDocumentID+"'" ;

			strErrorMsg =  _da.ExecuteDMLSQL ( UpdateSql );

			string  UpdateTCstate= "UPDATE TCStrategy SET status = "+nState+" WHERE TCStrategy.TenderID in (SELECT TenderID From ITBDocument WHERE ITBIDKey = '"+strITBDocumentID+"') ";

			strErrorMsg += _da.ExecuteDMLSQL ( UpdateTCstate );

			string SelectChildSR = "SELECT SRID FROM TCStrategySR,ITBDocument WHERE TCStrategySR.TenderID =ITBDocument.TenderID AND  ITBIDKey = '"+strITBDocumentID+"'";

			DataTable dtSR = _da.GetDataTable(SelectChildSR);

			for(int i=0;i<dtSR.Rows.Count;i++)
			{
				string UpdateTenderState = "UPDATE ServiceRequistion SET SRState = "+nState+" WHERE SRID = '"+dtSR.Rows[i][0].ToString()+"'";
				strErrorMsg += _da.ExecuteDMLSQL(UpdateTenderState);
			}
			return strErrorMsg;
		}



		
		public bool GetTenderExpiryBtnVisable (string strITBDocumentID)
		{
			string strErrorMsg = string.Empty;
			int nCount = 0;
			string strSql = "select * from ITBDocument,ServiceRequistion where "+
							" State = 3 and  ITBDocument.SRID = ServiceRequistion.SRID AND ServiceRequistion.TenderState = "+(int)TenderState.State_ITBStart+" AND ITBIDKey = '"+strITBDocumentID+"'";
			nCount = _da.GetDataTable ( strSql ).Rows.Count;
			if(nCount >0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

	}
}
