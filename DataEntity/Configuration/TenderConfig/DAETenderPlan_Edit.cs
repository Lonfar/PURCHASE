using System;

namespace DataEntity
{
	/// <summary>
	/// 审批处理(编辑页面)的数据实体类
	/// </summary>
	public class DAETenderPlan_Edit : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETenderPlan_Edit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 更新SR的状态
		/// </summary>
		/// <param name="State">状态</param>
		/// <param name="IDKey">SR主键</param>
		public void UpdateTenderState ( string IDKey , string State )
		{
			_da.ExecuteDMLSQL ( "UPDATE ServiceRequistion SET SRState = '"+State+"'WHERE ServiceRequistion.IDKey = '"+IDKey+"'" );
		}

		/// <summary>
		/// 更改提交表的状态
		/// </summary>
		/// <param name="ObjectiveID">对象ID</param>
		/// <param name="ObjectiveType">对象类型</param>
		/// <param name="State">对象状态</param>
		public void UpdatePutInState ( string IDKey , int State )
		{
			_da.ExecuteDMLSQL ( "UPDATE PutIn SET State = "+State+" WHERE IDKey = '"+IDKey+"'" );
		}
	}
}
