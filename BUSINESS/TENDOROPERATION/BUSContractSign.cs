using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSContractSign ��ժҪ˵����
	/// </summary>
	public class BUSContractSign:BUSBase
	{
		public BUSContractSign()
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
				if ( IDKey == dr["ContractView.ViewerID"].ToString()) return true;
			}
			return false;
		}
	}
}
