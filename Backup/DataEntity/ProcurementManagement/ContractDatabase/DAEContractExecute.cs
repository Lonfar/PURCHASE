using System;
using System.Data;

using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// 合同执行情况数据实体类
	/// </summary>
	public class DAEContractExecute : DAEBase
	{
		/// <summary>
		/// 数据访问
		/// </summary>
		DataAcess _da ;

		public DAEContractExecute()
		{
			_da = Common.GetProjectDataAcess.GetDataAcess();
		}

		/// <summary>
		/// 判断目标合同是否存在合同执行情况
		/// </summary>
		/// <param name="strContractID">合同ID</param>
		/// <returns>true:存在,false:不存在</returns>
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

		#region 更改合同的状态
		public string UpdateContratState(string IDKey,int nState)
		{
			string UpdateSql = " UPDATE Contract SET State = "+nState+" WHERE IDKey = '"+IDKey+"'";
			return  _da.ExecuteDMLSQL(UpdateSql);
		}
		#endregion
	}
}
