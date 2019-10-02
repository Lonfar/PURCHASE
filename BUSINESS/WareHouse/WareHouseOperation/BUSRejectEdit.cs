using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSRejectEdit 的摘要说明。
	/// </summary>
	public class BUSRejectEdit : BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSRejectEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public string CheckRejectMaterial ( DataTable dtRejectMaterial )
		{
			foreach(DataRow drRejectMaterial in dtRejectMaterial.Rows)
			{
				if (drRejectMaterial.RowState != DataRowState.Deleted)
				{
					decimal iTransQuan = Decimal.Parse(drRejectMaterial["WH_RejectMaterial.QuantityReject"].ToString()); 
					decimal iTransQuanOld = Decimal.Parse(drRejectMaterial["WH_RejectMaterial.QuantityInBin"].ToString()) ; 
					if (iTransQuan > iTransQuanOld)
					{	
						return "Error01" ;
					}
				}
			}

			return string.Empty ;
		}

		/// <summary>
		/// 校验数据逻辑
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// 错误信息
			string sErrorMsg = string.Empty;
			// 校验子表是否有数据
			sErrorMsg = CheckChildRows(dt);
			if ( sErrorMsg.Trim().Length == 0 )
			{
				// 校验业务主键           
				sErrorMsg = CheckRejectMaterial ( dt );
				if ( sErrorMsg.Trim().Length > 0 )
				{ 
					return sErrorMsg;
				}
			}
			return sErrorMsg;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt">Edit表</param>
		/// <returns>sErrMsg</returns>
		private string CheckChildRows(DataTable dtChild)
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
