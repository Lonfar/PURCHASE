using System;
using System.Data;
using System.Collections;

using Common;
using Cnwit.Utility;


namespace DataEntity
{
	/// <summary>
	/// 发标的数据实体类
	/// </summary>
	public class DAETenderSend : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();	// 数据访问
		CEntityUitlity cEntity= new CEntityUitlity();

		public  DAETenderSend()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//

		}


		public  void GetVendorList(DataTable dtTemp,string sTenderID)
		{
			string strSQL = "SELECT ITBVendorList.VendorID FROM TCStrategyVendor,Vendor WHERE TCStrategyVendor.VendorCode = Vendor.ITBIDKey AND ITBDocument.TenderID = '"+sTenderID+"'";
			DataTable dt = _da.GetDataTable(strSQL);
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
				DataRow dr = dtTemp.NewRow();
				dr["IDKey"] = System.Guid.NewGuid().ToString();
				dr["VendorID"] = dt.Rows[i][0].ToString();
				dr["RowStatus"] = "New";
				dtTemp.Rows.Add(dr);

			}

		}

		/// <summary>
		/// 回写招标计划表中的"发标时间"
		/// </summary>
		/// <returns>错误信息</returns>
		public string SetTenderSendDate ( string strTendorIDKey , string TendorSendDate )
		{
			string UpdateSql = "UPDATE TCStrategyPlan SET PlanBeginDate = '"+TendorSendDate+"' WHERE TenderID = '"+strTendorIDKey+"'";

			return _da.ExecuteDMLSQL( UpdateSql );
		}

		/// <summary>
		/// 更新标书的状态
		/// </summary>
		/// <param name="strITBDocumentID">标书的IDKey</param>
		/// <param name="State">状态，本处应该为2（截标阶段）</param>
		/// <returns>错误信息</returns>
		public string SetITBDocumentState ( string strITBDocumentID , int State )
		{
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE ITBDocument SET State = "+State+" WHERE ITBIDKey = '"+strITBDocumentID+"'" ;

			strErrorMsg =  _da.ExecuteDMLSQL ( UpdateSql );

			return strErrorMsg;
		}

		/// <summary>
		/// 获得标书的详细内容
		/// </summary>
		/// <param name="ITBNumber">标书编号</param>
		/// <returns></returns>
		public Hashtable GetITBInfo ( string ITBNumber )
		{
			string SelectSql = "SELECT TenderID , ObjectName  FROM ITBDocument WHERE ITBIDKey = '"+ITBNumber+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			Hashtable ht = new Hashtable(3);

			if ( dt_Temp.Rows.Count > 0 )
			{
				ht.Add("TenderID" ,Convert.ToString( dt_Temp.Rows[0]["TenderID"] ) );
				ht.Add("ObjectName" ,Convert.ToString( dt_Temp.Rows[0]["ObjectName"] ) );
			}

			return ht;
		}
	}
}

