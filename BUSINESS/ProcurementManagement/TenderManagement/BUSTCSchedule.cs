using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// TC日程的业务逻辑类 Added By Liujun 
	/// </summary>
	public class BUSTCSchedule : BUSBase
	{
		public BUSTCSchedule()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 检查TC报告(当不存在TC报告的时候不能保存TC日程)

		/// <summary>
		/// 检查TC报告(当不存在TC报告的时候不能保存TC日程)
		/// </summary>
		/// <param name="dataTable">TC报告数据表</param>
		/// <returns>true:数量通过,false:数量不通过</returns>
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

		#region 检查是否存在相同的TC报告

		/// <summary>
		/// 检查是否存在相同的TC报告
		/// </summary>
		/// <param name="dataTable">TC报告数据表</param>
		/// <returns>true:没有重复的,false:存在重复的</returns>
		public bool CheckHasRepeatTCReport ( System.Data.DataTable dataTable )
		{
			// 临时表
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

		#region 是否在TCGroup表中已存在
		/// <summary>
		/// 是否在TCGroup表中存在
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
