using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// 招标计划(编辑界面)的逻辑类
	/// </summary>
	public class BUSTenderPlan_Edit : BUSBase
	{
		public BUSTenderPlan_Edit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 校验是否有委员会成员
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtChild)
		{
			foreach(DataRow dr in dtChild.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoChildInfoSelected" ;
		}
	}
}
