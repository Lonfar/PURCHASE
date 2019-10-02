using System;
using System.Data;

using DataEntity;


namespace Business
{
	/// <summary>
	/// TC日程得业务逻辑类
	/// </summary>
	public class BUSTCSummary : BUSBase
	{
		/// <summary>
		/// 检查目标TC纪要中得TC报告是否已经全部审核
		/// </summary>
		/// <param name="strTCSummaryIDKey">TC纪要的IDKey</param>
		/// <returns>true:全部审核,false:存在未审核的报告</returns>
		public bool CheckTCMeetingReport ( string strTCSummaryIDKey )
		{
			DAETCMeetingReport dataEntity = new DAETCMeetingReport() ;
			
			bool bIsOk = true;

			using ( DataTable dtTCMeetingReport = dataEntity.getTCMeetingReport ( 	strTCSummaryIDKey ) )
			{
				foreach ( DataRow dr in dtTCMeetingReport.Rows )
				{
					if ( dr["IsPass"] == DBNull.Value )
					{
						bIsOk = false;
					}
				}
			}

			return bIsOk ;
		}
	}
}
