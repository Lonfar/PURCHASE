using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// TC�ճ̵�ҵ���߼��� Added By Liujun 
	/// </summary>
	public class BUSTCSchedule : BUSBase
	{
		public BUSTCSchedule()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���TC����(��������TC�����ʱ���ܱ���TC�ճ�)

		/// <summary>
		/// ���TC����(��������TC�����ʱ���ܱ���TC�ճ�)
		/// </summary>
		/// <param name="dataTable">TC�������ݱ�</param>
		/// <returns>true:����ͨ��,false:������ͨ��</returns>
		public bool CheckTCReport( System.Data.DataTable dataTable )
		{
			if ( dataTable.Rows.Count > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region ����Ƿ������ͬ��TC����

		/// <summary>
		/// ����Ƿ������ͬ��TC����
		/// </summary>
		/// <param name="dataTable">TC�������ݱ�</param>
		/// <returns>true:û���ظ���,false:�����ظ���</returns>
		public bool CheckHasRepeatTCReport ( System.Data.DataTable dataTable )
		{
			// ��ʱ��
			DataTable dt_Temp = dataTable.Copy();

			bool IsOK = true;
			
			foreach ( DataRow dr_Temp in dt_Temp.Rows )
			{
				int i = 0;

				if( dr_Temp.RowState != DataRowState.Deleted )
				{
					foreach ( DataRow dr in dataTable.Rows )
					{
						if( dr.RowState != DataRowState.Deleted )
						{
							if ( Convert.ToString( dr_Temp["TCMeetingReport.PutINIDKey"] ) == Convert.ToString ( dr["TCMeetingReport.PutINIDKey"] ))
							{
								i++;
							}
							if ( i > 1 )
							{
								IsOK = false;
								break;
							}
						}
					}

					if ( i > 1 )
					{
						IsOK = false;
						break;
					}
				}
			}

			return IsOK;
		}

		#endregion

		#region �Ƿ���TCGroup�����Ѵ���
		/// <summary>
		/// �Ƿ���TCGroup���д���
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtTCGroup )
		{
			foreach(DataRow dr in dtTCGroup.Rows)
			{
				if ( IDKey == dr["TCMeetingMember.UserID"].ToString()) return true;
			}
			return false;
		}
		#endregion
	}
}
