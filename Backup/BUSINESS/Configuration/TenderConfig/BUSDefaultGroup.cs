using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSDefaultGroup ��ժҪ˵����
	/// </summary>
	public class BUSDefaultGroup:BUSBase
	{
		public BUSDefaultGroup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// У���Ƿ����б�С���Ա
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
	}
}
