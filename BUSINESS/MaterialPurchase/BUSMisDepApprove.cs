using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSMisDepApprove 的摘要说明。
	/// </summary>
	public class BUSMisDepApprove : BUSBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		/// <summary>
		/// 验证申请编号是否存在
		/// wanglijie on 2008-02-03
		/// </summary>
		/// <param name="MRNO"></param>
		/// <returns></returns>
		public string CheckMRNO(string MRNO)
		{
			string sSql = "SELECT * FROM MR_MaterialRequisition WHERE MRNO = '"+MRNO+"'";
			if(_da.GetDataTable(sSql).Rows.Count > 0)			
			{
				return "ExistMRNO";
			}
			return "";
		}

		#region 更新物资表状态

		/// <summary>
		/// 根据MRID得到物资子表
		/// </summary>
		/// <param name="sMRID"></param>
		/// <returns></returns>
		public DataTable GetMaterialByMRID(string sMRID)
		{
			string sSql = "Select * from MR_Material where MRID = '"+sMRID+"'";
			DataTable  dtTempInfo = _da.GetDataTable (sSql);
			return dtTempInfo;
		}

		/// <summary>
		/// 更新物资子表状态
		/// </summary>
		/// <param name="sMRID"></param>
		/// <param name="sStatus"></param>
		/// <returns></returns>
		public string UpdateMaterialList(string sMRID, string sStatus)
		{
			string sSql = "Update MR_Material SET Status = '"+sStatus+"' where MRID = '"+sMRID+"'";
			return _da.ExecuteDMLSQL (sSql);			
		}

		#endregion
	}
}
