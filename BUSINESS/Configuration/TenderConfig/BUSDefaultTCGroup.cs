using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// 默认审批小组逻辑类
	/// </summary>
	public class BUSDefaultTCGroup : BUSBase
	{
		public BUSDefaultTCGroup()
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
			return "NoMaterialSelected" ;
		}

		/// <summary>
		/// 校验数据
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessData(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			return base.CheckBusinessData( dt , fieldsList );
		}

		/// <summary>
		/// 校验计算
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_calculate(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			return base.CheckBusinessLogic_calculate (dt, fieldsList);
		}

		/// <summary>
		/// 校验业务规则
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			return base.CheckBusinessLogic_rule (dt, fieldsList);
		}


 
	}
}
