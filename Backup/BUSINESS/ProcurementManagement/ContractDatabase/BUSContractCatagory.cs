using System;
using DataEntity;

namespace Business
{
	/// <summary>
	/// ��ͬ����ҵ���߼��� Added by Liujun at 11.17
	/// </summary>
	public class BUSContractCatagory : BUSBase
	{
		/// <summary>
		/// ����ʵ���
		/// </summary>
		private DataEntity.DAEContractCatagory dataEntity;

		public BUSContractCatagory()
		{
			dataEntity = new DAEContractCatagory();
		}

		/// <summary>
		/// ��ͬһ�������Ƿ�����ͬ������
		/// </summary>
		/// <param name="strContractDescription">��ͬ��������</param>
		/// <param name="strConID">����</param>
		/// <param name="strParentID">��ID</param>
		/// <returns>true:��ͬ�����ظ�,false:��ͬ����δ�ظ�</returns>
		public bool IsRepeatDescription ( string strContractDescription , string strConID , string strParentID )
		{
			int iCount = dataEntity.ContractNum( strContractDescription , strConID , strParentID );

			if ( iCount > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
