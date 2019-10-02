using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSNoFlowContractSign 的摘要说明。
	/// </summary>
	public class BUSNoFlowContractSign:BUSBase
	{
		public BUSNoFlowContractSign()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 检验物资信息
		/// <summary>
		/// 校验子表是否有数据
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// 错误信息
			string sErrorMsg = string.Empty;
			// 校验子表是否有数据
			sErrorMsg = CheckChildRows(dt);
			
			return sErrorMsg;
		}

		private string CheckChildRows(DataTable dtChild)
		{
			string sErrMsg = "";
			DataTable dtCheckRow = dtChild.Copy();
			dtCheckRow.AcceptChanges();
			if(dtCheckRow.Rows.Count == 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}

		#endregion

	}
}
