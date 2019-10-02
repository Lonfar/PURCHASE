using System;
using System.Data;

namespace DataEntity.Statistic
{
	/// <summary>
	/// 物资使用分析报表数据实体类 Added by Liujun at 2007-7-3
	/// </summary>
	public class DAEMaterialUseAnalysis : DAEBase
	{
		/// <summary>
		/// 物资使用分析报表数据实体类
		/// </summary>
		public DAEMaterialUseAnalysis()
		{

		}

		/// <summary>
		/// 检索数据
		/// </summary>
		/// <param name="sWhereSql">筛选条件</param>
		/// <returns>对应数据表</returns>
		public DataTable GetRptData ( string sWhereSql )
		{
			DataTable dtData;
			string sSelectSql = "SELECT * FROM v_Report_MaterialUseAnalysis";

			if ( sWhereSql != null && sWhereSql.Length > 0 )
			{
				sSelectSql += " WHERE " + sWhereSql;
			}

			dtData = this.BaseDataAccess.GetDataTable ( sSelectSql );

			return dtData;
		}
	}
}
