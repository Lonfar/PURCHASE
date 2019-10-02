using System;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEABCCalssScale 的摘要说明。
	/// </summary>
	public class DAEABCCalssScale : DAEBase
	{
		public DAEABCCalssScale()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public SqlDataReader GetData ()
		{
			string sSql = "SELECT ABCalssScaleID,AScale,BScale,CScale FROM WHABCCalssScale" ;
			System.Data.SqlClient.SqlDataReader drInfo = Common.GetProjectDataAcess.GetDataAcess().GetDataReader(sSql) ; 				
			return drInfo;
		}

		/// <summary>
		/// 保存A，B，C比例数据
		/// </summary>
		/// <param name="sAScale"></param>
		/// <param name="sBScale"></param>
		/// <param name="sCScale"></param>
		public void UpdateWHABCCalssScale(string sAScale,string sBScale,string sCScale)
		{
			string strUpdateSql = "UPDATE Projects SET AScale = '"+ sAScale +"',BScale = '"+ sBScale +"',CScale = '" + sCScale +"'";
			Common.GetProjectDataAcess.GetDataAcess().ExecuteDMLSQL( strUpdateSql );
		}

	}
}
