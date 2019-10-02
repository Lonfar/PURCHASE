using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSMisDepApprove ��ժҪ˵����
	/// </summary>
	public class BUSMisDepApprove : BUSBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		/// <summary>
		/// ��֤�������Ƿ����
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

		#region �������ʱ�״̬

		/// <summary>
		/// ����MRID�õ������ӱ�
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
		/// ���������ӱ�״̬
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
