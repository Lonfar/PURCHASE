using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSSealConfig 的摘要说明。
	/// added by QsQ
	/// </summary>
	public class BUSSealConfig : BUSBase
	{
		public BUSSealConfig()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}		

		/// <summary>
		/// 非密封标的金额不能大于密封标的金额
		/// </summary>
		private const string BUSCHKERR001 = "BUSCHKERR001";

		/// <summary>
		/// 继承基类的逻辑规则校验并检查非密封标的金额不能大于密封标的金额
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			// 错误信息
			string strErrorMessage = string.Empty;

			foreach ( DataRow dataRow in dt.Rows )
			{
				if ( Convert.ToDecimal( dataRow["TI_SealConfig.SealLevel"] ) < Convert.ToDecimal ( dataRow["TI_SealConfig.UnSealLevel"]) )
				{
					strErrorMessage = BUSCHKERR001;

					break;
				}
			}
			return strErrorMessage;
		}
	}
}
