using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSServiceRequistion_Edit ��ժҪ˵����
	/// </summary>
	public class BUSServiceRequistion_Edit : BUSBase
	{
		public BUSServiceRequistion_Edit() 
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �Ƿ���SeePerson���д���
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtSeePerson )
		{
			foreach(DataRow dr in dtSeePerson.Rows)
			{
				if ( IDKey == dr["ServiceRequestViewer.ViewerID"].ToString()) return true;
			}
			return false;
		}
	}
}
