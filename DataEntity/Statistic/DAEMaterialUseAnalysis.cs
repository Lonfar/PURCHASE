using System;
using System.Data;

namespace DataEntity.Statistic
{
	/// <summary>
	/// ����ʹ�÷�����������ʵ���� Added by Liujun at 2007-7-3
	/// </summary>
	public class DAEMaterialUseAnalysis : DAEBase
	{
		/// <summary>
		/// ����ʹ�÷�����������ʵ����
		/// </summary>
		public DAEMaterialUseAnalysis()
		{

		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="sWhereSql">ɸѡ����</param>
		/// <returns>��Ӧ���ݱ�</returns>
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
