using System;
using System.Data;

using DataEntity;


namespace Business
{
	/// <summary>
	/// TC�ճ̵�ҵ���߼���
	/// </summary>
	public class BUSTCSummary : BUSBase
	{
		/// <summary>
		/// ���Ŀ��TC��Ҫ�е�TC�����Ƿ��Ѿ�ȫ�����
		/// </summary>
		/// <param name="strTCSummaryIDKey">TC��Ҫ��IDKey</param>
		/// <returns>true:ȫ�����,false:����δ��˵ı���</returns>
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
