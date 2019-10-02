using System;
using System.Data.SqlClient;//新加
using Common;
using Cnwit.Utility;
namespace DataEntity
{
	/// <summary>
	/// DAESex 的摘要说明。
	/// </summary>
	public class DAEBIER:DAEBase
	{
		private DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEBIER()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
