using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSDefaultGroup 的摘要说明。
	/// </summary>
	public class BUSDefaultGroup:BUSBase
	{
		public BUSDefaultGroup()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 校验是否有招标小组成员
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
			return "NoMaterialSelected" ;
		}
	}
}
