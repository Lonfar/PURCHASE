using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSNoFlowContractSign ��ժҪ˵����
	/// </summary>
	public class BUSNoFlowContractSign:BUSBase
	{
		public BUSNoFlowContractSign()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����������Ϣ
		/// <summary>
		/// У���ӱ��Ƿ�������
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// ������Ϣ
			string sErrorMsg = string.Empty;
			// У���ӱ��Ƿ�������
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
