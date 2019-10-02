using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using Common;
using Cnwit;

namespace DataEntity
{
	/// <summary>
	/// 标书撰写的数据实体类 Added by Liujun at 11.30
	/// </summary>
	public class DAEITBDocumentWrite : DAEBase
	{

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
		public DataTable CheckState(String strTenderID )
		{
			string[] sParams = {"tableName","tablePK","tablePKValue","correlationField","ifCorrelation","correlationTable","correlationTableField","correlationFieldTable"} ;
			object[] objParamValues = {"ITBDocument","ITBIDKey",strTenderID,"TenderID","0","","","" } ; 
			SqlDbType[] paramTypes = { SqlDbType.VarChar , SqlDbType.VarChar , SqlDbType.VarChar, SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar } ;
			DataTable dt = BaseDataAccess.ExecuteSPQueryDataTable("sp_ControlState",sParams,objParamValues,paramTypes );
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select TCStrategy.Status as State From ITBDocument Inner Join TCStrategy On ITBDocument.TenderID = TCStrategy.TenderID  Where ITBIDKey='" + strPKValue + "'" ;
			DataTable dt = BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}

		//****************************************************

		#region 通过获得参考标书文件列表
		/// <summary>
		/// 通过获得参考标书文件列表
		/// </summary>
		/// <param name="strITBIDKey">标书编号</param>
		/// <returns>数据表</returns>
		public DataTable GetRefITBDocumentTable ( string strITBIDKey , string strFilter )
		{
//			string SelectSql = @" SELECT 
//										ITBDocument.ITBNumber ,
//										ITBDocument.SRID As SRName,
//										ITBDocument.ObjectName As ProjectName, 
//										Attachments.IDKey, 
//										Attachments.AttachName , 
//										Attachments.AttachSize , 
//										Attachments.UploadTime ,
//										Attachments.AttachAddr
//										FROM ITBDocument,Attachments
//										WHERE ITBDocument.ITBIDKey = Attachments.ObjectiveID
//										AND Attachments.ObjectiveType = 'Topis.TendorOperation.ITB'
//										AND ITBDocument.ITBIDKey = '"+strITBIDKey+"' AND ( ITBDocument.ITBNumber LIKE '%"+strFilter+"%' OR ITBDocument.SRID LIKE '%"+strFilter+"%' OR ITBDocument.ObjectName LIKE '%"+strFilter+"%' OR  Attachments.AttachName LIKE '%"+strFilter+"%' )" ;

			// =================== Modified by Liujun at 12.19 ================ //
			// 选择标书数据库中状态为历史纪录（State=5）的所有标书的附件 
			string SelectSql = @" SELECT 
										ITBDocument.ITBNumber ,
										ITBDocument.ObjectName As ProjectName, 
										Attachments.IDKey, 
										Attachments.AttachName , 
										Attachments.AttachSize , 
										Attachments.UploadTime ,
										Attachments.AttachAddr
										FROM ITBDocument,Attachments
										WHERE ITBDocument.ITBIDKey = Attachments.ObjectiveID
										AND ITBDocument.State = "+(int)TenderState.State_ContractSinged+" AND ( ITBDocument.ITBNumber LIKE '%"+strFilter+"%' OR ITBDocument.TenderID LIKE '%"+strFilter+"%' OR ITBDocument.ObjectName LIKE '%"+strFilter+"%' OR  Attachments.AttachName LIKE '%"+strFilter+"%' )" ;

			// AND (Attachments.ObjectiveType = 'Topis.TendorOperation.ITB' OR Attachments.ObjectiveType = 'Topis.ProcurementManagement.ITBDocumentDatabase.ITBDocument' )

			DataTable dt_Temp = BaseDataAccess.GetDataTable ( SelectSql );

			return dt_Temp;
		}
		#endregion

		#region 通过ITBIDKey获得TenderID
		/// <summary>
		/// 通过ITBIDKey获得TenderID
		/// </summary>
		/// <param name="ITBIDKey"></param>
		/// <returns></returns>
		public string GetTenderID ( string ITBIDKey  )
		{
			string SelectSql = " select TenderID from ITBDocument where ITBIDKey = '"+ITBIDKey+"'";
			DataTable dt_Temp = BaseDataAccess.GetDataTable ( SelectSql );
			if (dt_Temp.Rows.Count >0)
			{
				return dt_Temp.Rows[0][0].ToString();
			}
			else return "";
		}
		#endregion

		#region 查询标书中没有使用的策略

		/// <summary>
		/// 查询标书中没有使用的策略
		/// </summary>
		/// <returns></returns>
		public string GetTCStrategyNoUseInITBDocument ()
		{
			string strTenderID = string.Empty;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			string SelectSql = "SELECT TenderID FROM TCStrategy WHERE  NOT EXISTS (SELECT 1 FROM ITBDocument where ITBDocument.TenderID =TCStrategy.TenderID)";

			DataTable dt_Temp = BaseDataAccess.GetDataTable ( SelectSql );

			foreach ( DataRow dr in dt_Temp.Rows )
			{
				sb.Append ( "'"+Convert.ToString( dr["TenderID"] ) + "'," );
			}

			if  ( sb.Length > 0 )
			{
				if ( sb[sb.Length-1] == ',' )
				{
					// 将最后一个","去掉
					strTenderID = sb.Remove( sb.Length - 1 , 1 ).ToString();
				}
			}

			return strTenderID ;
		}

		#endregion

		#region 根据TenderID来获得对应的SR状态

		/// <summary>
		/// 根据TenderID来获得每项对应的SR状态
		/// </summary>
		/// <param name="dt_Source"></param>
		public string GetTenderStateByTenderIDs ( string strTenderID )
		{
			string SelectSql = @" SELECT BT_TenderStatus.TypeDescription FROM BT_TenderStatus 
										INNER JOIN TCStrategy ON TCStrategy.status = BT_TenderStatus.IDKey
										WHERE TCStrategy.TenderID = '"+strTenderID+"'";

			string strSRState = string.Empty;

			DataTable dt_Temp = BaseDataAccess.GetDataTable ( SelectSql );

			if ( dt_Temp.Rows.Count > 0 )
			{
				strSRState = Convert.ToString ( dt_Temp.Rows[0]["TypeDescription"] ) ;
			}

			return strSRState;
		}

		#endregion

		#region 通过 ITBIDKey 来获得对应策略的类型

		/// <summary>
		/// 通过 ITBIDKey 来获得对应策略的类型
		/// </summary>
		/// <param name="ITBIDKey">ITBIDKey</param>
		/// <returns>0:无数据，1:MR，2:SR</returns>
		public int GetTypeByITB ( string ITBIDKey )
		{
			int iType = 0;
			string sSelectSql = "SELECT MRTypeID FROM TCStrategy INNER JOIN ITBDocument ON TCStrategy.TenderID = ITBDocument.TenderID WHERE ITBDocument.ITBIDKey = '"+ITBIDKey+"' ";

			DataTable dtData = BaseDataAccess.GetDataTable( sSelectSql );
			if ( dtData.Rows.Count > 0 )
			{
				iType = Convert.ToInt32( dtData.Rows[0][0] );
			}

			return iType;
		}

		#endregion

		#region 通过 TenderID 来获得策略的类型

		/// <summary>
		/// 通过 TenderID 来获得对应策略的类型
		/// </summary>
		/// <param name="ITBIDKey">TenderID</param>
		/// <returns>0:无数据，1:MR，2:SR</returns>
		public int GetTypeByStrategy ( string TenderID )
		{
			int iType = 0;
			string sSelectSql = "SELECT MRTypeID FROM TCStrategy  WHERE TenderID = '"+TenderID+"' ";

			DataTable dtData = BaseDataAccess.GetDataTable( sSelectSql );
			if ( dtData.Rows.Count > 0 )
			{
				iType = Convert.ToInt32( dtData.Rows[0][0] );
			}

			return iType;
		}

		#endregion
	}
}
