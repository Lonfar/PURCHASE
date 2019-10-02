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
			// TODO: 在此处添加构造函数逻辑
			//
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
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

		//  Add by ZZH on 2008-1-21 添加验证是否可以删除的方法当节点被下一节点引用时，不应被删除
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
		/// 是否在SeePerson表中存在
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
