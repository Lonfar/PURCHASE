using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// �б�ƻ�(�༭����)���߼���
	/// </summary>
	public class BUSTenderPlan_Edit : BUSBase
	{
		public BUSTenderPlan_Edit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// У���Ƿ���ίԱ���Ա
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
			return "NoChildInfoSelected" ;
		}
	}
}
