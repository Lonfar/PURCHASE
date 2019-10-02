using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using Common;
using Cnwit;

namespace DataEntity
{
	/// <summary>
	/// ����׫д������ʵ���� Added by Liujun at 11.30
	/// </summary>
	public class DAEITBDocumentWrite : DAEBase
	{

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
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

		#region ͨ����òο������ļ��б�
		/// <summary>
		/// ͨ����òο������ļ��б�
		/// </summary>
		/// <param name="strITBIDKey">������</param>
		/// <returns>���ݱ�</returns>
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
			// ѡ��������ݿ���״̬Ϊ��ʷ��¼��State=5�������б���ĸ��� 
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

		#region ͨ��ITBIDKey���TenderID
		/// <summary>
		/// ͨ��ITBIDKey���TenderID
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

		#region ��ѯ������û��ʹ�õĲ���

		/// <summary>
		/// ��ѯ������û��ʹ�õĲ���
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
					// �����һ��","ȥ��
					strTenderID = sb.Remove( sb.Length - 1 , 1 ).ToString();
				}
			}

			return strTenderID ;
		}

		#endregion

		#region ����TenderID����ö�Ӧ��SR״̬

		/// <summary>
		/// ����TenderID�����ÿ���Ӧ��SR״̬
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

		#region ͨ�� ITBIDKey ����ö�Ӧ���Ե�����

		/// <summary>
		/// ͨ�� ITBIDKey ����ö�Ӧ���Ե�����
		/// </summary>
		/// <param name="ITBIDKey">ITBIDKey</param>
		/// <returns>0:�����ݣ�1:MR��2:SR</returns>
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

		#region ͨ�� TenderID ����ò��Ե�����

		/// <summary>
		/// ͨ�� TenderID ����ö�Ӧ���Ե�����
		/// </summary>
		/// <param name="ITBIDKey">TenderID</param>
		/// <returns>0:�����ݣ�1:MR��2:SR</returns>
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
