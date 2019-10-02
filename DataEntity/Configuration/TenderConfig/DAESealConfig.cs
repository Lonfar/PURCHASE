using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAESealConfig 的摘要说明。
	/// </summary>
	public class DAESealConfig:DAEBase
	{
		/// <summary>
		/// 数据存储类
		/// </summary>
		private Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAESealConfig()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 继承了基类的保存方法，并使用了排他方式进行更新其他的记录
		/// </summary>
		/// <returns></returns>
		public override string Save()
		{
			// 错误信息
			string ErrorMessage = string.Empty;
			
			if ( CurDataTable.Rows.Count > 0 )
			{
				ErrorMessage = base.Save();

				// 如果此行为有效密封标,则将其他数据有效性置为否
				if ( Convert.ToInt32( CurDataTable.Rows[0]["TI_SealConfig.IsValid"] ) == 1 )
				{
					string UpdateSql = "UPDATE TI_SealConfig SET IsValid = 0 WHERE IDKey <> '"+Convert.ToString( CurDataTable.Rows[0]["TI_SealConfig.IDKey"] )+"'";
				
					if ( ErrorMessage == "" )
					{
						ErrorMessage += _da.ExecuteDMLSQL ( UpdateSql );
					}
				}
			}

			return ErrorMessage;
		}
	
		/// <summary>
		/// 获得有效的密封标信息(IDKey)
		/// </summary>
		/// <returns></returns>
		public System.Data.SqlClient.SqlDataReader GetValidData ()
		{
			string SelectSql = "SELECT IDKey FROM TI_SealConfig WHERE IsValid = 1 ";

			System.Data.SqlClient.SqlDataReader drInfo = Common.GetProjectDataAcess.GetDataAcess().GetDataReader(SelectSql) ; 
				
			return drInfo;
		}
	}
}
