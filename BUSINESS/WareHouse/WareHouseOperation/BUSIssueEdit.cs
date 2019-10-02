using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSIssueEdit 的摘要说明。
	/// </summary>
	public class BUSIssueEdit:BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSIssueEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="sErrMsg"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dt)
		{
			string sErrMsg = "";
			foreach(DataRow row in dt.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					//实际发送数量
					decimal decFactIssuedQuantity=Convert.ToDecimal(row["WH_IssueMaterial.FactIssuedQuantity"].ToString());
					//可发数量
					decimal decCanIssuedQuantity=Convert.ToDecimal(row["WH_IssueMaterial.CanIssuedQuantity"].ToString());
					//其中预留数量
					decimal decPreserveQuantityInFact = (row["WH_IssueMaterial.PreserveQuantityInFact"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WH_IssueMaterial.PreserveQuantityInFact"].ToString()));
					//预留数量
					decimal decPreserveQuantity = (row["WH_IssueMaterial.PreserveQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WH_IssueMaterial.PreserveQuantity"].ToString()));


					//实发数量应小于可发数量
					if(decFactIssuedQuantity > decCanIssuedQuantity)
					{
						sErrMsg ="CheckErrMsg1";
						break;

					}
					//其中预留数量应小于实发数量
					if(decPreserveQuantityInFact > decFactIssuedQuantity )
					{
						sErrMsg ="CheckErrMsg2";
						break;
					}
					
					//其中预留数量应小于预留数量
					if(decPreserveQuantityInFact > decPreserveQuantity)
					{
						sErrMsg ="CheckErrMsg3";
						break;
					}

					//其中预留料数量填写不足
					if(decPreserveQuantityInFact < decFactIssuedQuantity -(decCanIssuedQuantity - decPreserveQuantity))
					{
						sErrMsg ="CheckErrMsg4";
						break;
					}
				}
			}	
			return sErrMsg;
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtBorrowMaterial"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtBorrowMaterial)
		{
			foreach(DataRow dr in dtBorrowMaterial.Rows)
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
