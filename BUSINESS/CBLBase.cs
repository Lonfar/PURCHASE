using System;
using DictionaryAccess ;
using Cnwit.Utility ;

namespace Business
{
	/// <summary>
	/// CBLBase ��ժҪ˵����
	/// </summary>
	public class CBLBase
	{
		private string m_sTableCode = "" ;
		private string m_sPKField = "" ;
		private System.Collections.Hashtable m_hashDataInfo = null ; 

		public CBLBase(string sTableCode)
		{
			m_sTableCode = sTableCode ;
			m_sPKField = CDictionaryAccess.GetTableDicInfo(sTableCode).PrimaryField ; 			
		}
		
		public string TableCode
		{
			get
			{
				return m_sTableCode ;
			}
		}

		/// <summary>
		/// ����ʱ������У��
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		public Common.CResultInfo AddCheck(System.Data.DataTable pDTInfo)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
			try
			{
				if(Common.CCommonCheck.CheckFieldValueExist(m_sTableCode,m_sPKField,pDTInfo.Rows[0][m_sTableCode+"." + m_sPKField].ToString()) == true)
				{
					pResultInfo.bSuccess = false ;
					pResultInfo.ErrorDesc = "��¼�Ѿ����ڣ�������¼��!" ;
					pResultInfo.ErrorResID = "IDAlreayExist" ;
					return pResultInfo ;
				} 				
			}
			catch(System.Data.SqlClient.SqlException  sqlErr)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
			}
			catch(System.Exception err)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorDesc = err.Message ;				
			}
			return pResultInfo ;
		}
		
		/// <summary>
		/// �������ݵ����ݿ���
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		public Common.CResultInfo Add(System.Data.DataTable pDTInfo)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
			try
			{
				string sInsertSql = GenerateInsertSql(pDTInfo) ;
				DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
				pDataAccess.scExecuteQuery(sInsertSql) ; 
				pResultInfo.bSuccess = true ;
			}
			catch(System.Data.SqlClient.SqlException sqlErr)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
			}
			catch(System.Exception err)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorDesc = err.Message ;				
			}
			return pResultInfo ;
		}
		
		
		/// <summary>
		/// �������Ӽ�¼��sql���
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		private string GenerateInsertSql(System.Data.DataTable pDTInfo)
		{			
			string sInsertFieldSql = "insert into " + m_sTableCode + "(" ;
			string sInsertValueSql = "values (" ;
			foreach(System.Data.DataColumn pDCInfo in pDTInfo.Columns)
			{
				if(pDCInfo.ColumnName.ToLower() != "RowStatus".ToLower())
				{
					sInsertFieldSql = sInsertFieldSql + pDCInfo.ColumnName.Substring(pDCInfo.ColumnName.IndexOfAny(".".ToCharArray())+1) + "," ;    
					if(pDTInfo.Rows[0][pDCInfo.ColumnName] == DBNull.Value)
					{
						sInsertValueSql = sInsertValueSql + "null," ;
					}
					else
					{
						sInsertValueSql = sInsertValueSql + "'" + pDTInfo.Rows[0][pDCInfo.ColumnName].ToString().Trim() + "'," ;
					}
				}
			}
			sInsertFieldSql = sInsertFieldSql.TrimEnd(',') + ")" ;
			sInsertValueSql = sInsertValueSql.TrimEnd(',') + ")" ;  
		
			return sInsertFieldSql + " " + sInsertValueSql ;
		}

		/// <summary>
		/// �޸����ݵ�У��
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		public Common.CResultInfo ModifyCheck(System.Data.DataTable pDTInfo)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
			try
			{
				if(Common.CCommonCheck.CheckDataModified(m_sTableCode,m_sPKField,pDTInfo.Rows[0][m_sTableCode+"."+m_sPKField].ToString(),pDTInfo.Rows[0]["vTP"].ToString()) == true)
				{
					pResultInfo.bSuccess = false ;
					pResultInfo.ErrorDesc = "�����Ѿ����޸ģ��޸�ʧ��!" ;
					pResultInfo.ErrorResID = "DataModified" ;
					return pResultInfo ;
				}
			}
			catch(System.Data.SqlClient.SqlException  sqlErr)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
			}
			catch(System.Exception err)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorDesc = err.Message ;				
			}
			return pResultInfo ;
		}

		/// <summary>
		/// �޸����ݵ����ݿ���
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		public Common.CResultInfo Modify(System.Data.DataTable pDTInfo)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
			try
			{
				string sModifySql = GenerateModifySql(pDTInfo) ;
				DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
				pDataAccess.scExecuteQuery(sModifySql) ;
				pResultInfo.bSuccess = true ;
			}
			catch(System.Data.SqlClient.SqlException  sqlErr)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
			}
			catch(System.Exception err)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorDesc = err.Message ;				
			}
			return pResultInfo ;
		}

		private string GenerateModifySql(System.Data.DataTable pDTInfo)
		{
			string sModifySql = "update " + m_sTableCode + " set " ;
			foreach(System.Data.DataColumn pDCInfo in pDTInfo.Columns)
			{
				if(pDCInfo.ColumnName.ToLower() != "RowStatus".ToLower() && pDCInfo.ColumnName.ToLower() != m_sTableCode.ToLower() + "." + m_sPKField.ToLower())
				{
					if(pDTInfo.Rows[0][pDCInfo.ColumnName] == DBNull.Value)
					{
						sModifySql = sModifySql + "null," ;
					}
					else
					{
						sModifySql = sModifySql + pDCInfo.ColumnName + " = '" + pDTInfo.Rows[0][pDCInfo.ColumnName].ToString() + "'," ; 
					}
				}
			}
			return sModifySql ;
		}

		/// <summary>
		/// ɾ������У��
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		public Common.CResultInfo DeleteCheck(System.Data.DataTable pDTInfo)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
			try
			{
				foreach(System.Data.DataRow pDRInfo in pDTInfo.Rows)
				{
					    
				}
			}
			catch(System.Data.SqlClient.SqlException  sqlErr)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
			}
			catch(System.Exception err)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorDesc = err.Message ;				
			}
			return pResultInfo ;
		}
		
		/// <summary>
		/// �����ݿ���ɾ������
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		public Common.CResultInfo Delete(System.Data.DataTable pDTInfo)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
			try
			{
				string sDeleteSql = GenerateDeleteSql(pDTInfo) ;
				DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
				pDataAccess.scExecuteQuery(sDeleteSql) ;
				pResultInfo.bSuccess = true ;
			}
			catch(System.Data.SqlClient.SqlException  sqlErr)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
			}
			catch(System.Exception err)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorDesc = err.Message ;				
			}
			return pResultInfo ;
		}

		/// <summary>
		/// ����ɾ�����ݵ�sql���
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		private string GenerateDeleteSql(System.Data.DataTable pDTInfo)
		{
			string sDeleteSql = "" ;
			foreach(System.Data.DataRow pDRInfo in pDTInfo.Rows)
			{
				sDeleteSql = sDeleteSql + "delete " + m_sTableCode + " where " + m_sPKField + " = '" + pDTInfo.Rows[0][m_sTableCode+"."+m_sPKField].ToString() + "';"  ;
			}
			return sDeleteSql ;
		}
	}
}
