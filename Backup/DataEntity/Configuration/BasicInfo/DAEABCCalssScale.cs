using System;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEABCCalssScale ��ժҪ˵����
	/// </summary>
	public class DAEABCCalssScale : DAEBase
	{
		public DAEABCCalssScale()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public SqlDataReader GetData ()
		{
			string sSql = "SELECT ABCalssScaleID,AScale,BScale,CScale FROM WHABCCalssScale" ;
			System.Data.SqlClient.SqlDataReader drInfo = Common.GetProjectDataAcess.GetDataAcess().GetDataReader(sSql) ; 				
			return drInfo;
		}

		/// <summary>
		/// ����A��B��C��������
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
