using System;
using System.Data;

using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// ��ִͬ���������ʵ����
	/// </summary>
	public class DAEContractExecute : DAEBase
	{
		/// <summary>
		/// ���ݷ���
		/// </summary>
		DataAcess _da ;

		public DAEContractExecute()
		{
			_da = Common.GetProjectDataAcess.GetDataAcess();
		}

		/// <summary>
		/// �ж�Ŀ���ͬ�Ƿ���ں�ִͬ�����
		/// </summary>
		/// <param name="strContractID">��ͬID</param>
		/// <returns>true:����,false:������</returns>
		public bool IsExistsContractExecute ( string strContractID )
		{
			bool IsExists = false;

			string SelectSql = "SELECT COUNT(*) AS Num FROM ContractExecute WHERE ContractID = '"+strContractID+"'";

			DataTable dt_Temp = _da.GetDataTable( SelectSql );

			if ( dt_Temp.Rows.Count > 0 )
			{
				if ( Convert.ToInt32( dt_Temp.Rows[0]["Num"] ) > 0 )
				{
					IsExists = true;
				}
			}

			return IsExists ;
		}

		#region ���ĺ�ͬ��״̬
		public string UpdateContratState(string IDKey,int nState)
		{
			string UpdateSql = " UPDATE Contract SET State = "+nState+" WHERE IDKey = '"+IDKey+"'";
			return  _da.ExecuteDMLSQL(UpdateSql);
		}
		#endregion
	}
}
