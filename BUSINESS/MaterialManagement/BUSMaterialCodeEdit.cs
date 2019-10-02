using System;
using Common;
using Cnwit.Utility;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSMaterialCodeEdit ��ժҪ˵����
	/// </summary>
	public class BUSMaterialCodeEdit : BUSBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		/// <summary>
		/// 
		/// </summary>
		public BUSMaterialCodeEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtChild)
		{
			string sErrMsg = "";
			if(dtChild.Rows.Count <= 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}

		/// <summary>
		/// �ж� ��ǰ�ĵ�λ�Ƿ����ӱ����Ѿ����ڣ�IsBaseUOM = 0��
		/// </summary>
		/// <param name="sPKValue"></param>
		/// <returns></returns>
		public string IsUOMID(string sPKValue)
		{
			string sSql = "SELECT UOMID FROM MaterialUOM WHERE ItemCode = '"+sPKValue+"' AND IsBaseUOM = 0";
			DataTable dt = _da.GetDataTable(sSql);
			if (dt.Rows.Count > 0)
			{
				return "NoMaterialSelected" ;
			}
			else
			{
				return "";
			}
		}
	}
}
