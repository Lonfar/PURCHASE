using System;

namespace DataEntity
{
	/// <summary>
	/// ��������(�༭ҳ��)������ʵ����
	/// </summary>
	public class DAETenderPlan_Edit : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETenderPlan_Edit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����SR��״̬
		/// </summary>
		/// <param name="State">״̬</param>
		/// <param name="IDKey">SR����</param>
		public void UpdateTenderState ( string IDKey , string State )
		{
			_da.ExecuteDMLSQL ( "UPDATE ServiceRequistion SET SRState = '"+State+"'WHERE ServiceRequistion.IDKey = '"+IDKey+"'" );
		}

		/// <summary>
		/// �����ύ���״̬
		/// </summary>
		/// <param name="ObjectiveID">����ID</param>
		/// <param name="ObjectiveType">��������</param>
		/// <param name="State">����״̬</param>
		public void UpdatePutInState ( string IDKey , int State )
		{
			_da.ExecuteDMLSQL ( "UPDATE PutIn SET State = "+State+" WHERE IDKey = '"+IDKey+"'" );
		}
	}
}
