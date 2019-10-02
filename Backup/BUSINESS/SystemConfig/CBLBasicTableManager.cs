using System;
using Cnwit.Utility ;
using DictionaryAccess ;
namespace Business.SystemConfig
{
	/// <summary>
	/// CBLBasicTableManager ��ժҪ˵����
	/// </summary>
	public class CBLBasicTableManager:CBLBase
	{
		public CBLBasicTableManager(string sTableCode):base(sTableCode)
		{
			
		}

		/// <summary>
		/// �������ݵ����ݿ���
		/// </summary>
		/// <param name="pDTInfo"></param>
		/// <returns></returns>
		public Common.CResultInfo Add(System.Data.DataTable pDTInfo)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ; ;
			try
			{
				string sParentID = pDTInfo.Rows[0][base.TableCode + ".vParentID"].ToString() ;
				pResultInfo = base.Add(pDTInfo)  ;
				if(pResultInfo.bSuccess == false)
				{
					return pResultInfo ;
				}
				if(sParentID.Trim() != "")
				{
					Cnwit.Utility.DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
					pDataAccess.scExecuteQuery("update " + base.TableCode + " set bChildFlag = 0 where vID = '" + sParentID + "';update " + base.TableCode + " set vParentID = '" + sParentID + "' where vID = '" + pDTInfo.Rows[0][base.TableCode + ".vID"].ToString() + "'") ;
					return pResultInfo ;
				}
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
//		private string m_sTableCode = "" ;
//		private System.Collections.Hashtable m_hashDataInfo = null ; 
//
//		public CBLBasicTableManager(string sTableCode)
//		{
//			m_sTableCode = sTableCode ;
//		}
//
//		/// <summary>
//		/// ����ʱ������У��
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		public Common.CResultInfo AddCheck(System.Data.DataTable pDTInfo)
//		{
//			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
//			try
//			{
//				if(Common.CCommonCheck.CheckFieldValueExist(m_sTableCode,"vID",pDTInfo.Rows[0][m_sTableCode+".vID"].ToString()) == true)
//				{
//					pResultInfo.bSuccess = false ;
//					pResultInfo.ErrorDesc = "ID�Ѿ����ڣ�������¼��!" ;
//					pResultInfo.ErrorResID = "IDAlreayExist" ;
//					return pResultInfo ;
//				} 				
//			}
//			catch(System.Data.SqlClient.SqlException  sqlErr)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
//			}
//			catch(System.Exception err)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorDesc = err.Message ;				
//			}
//			return pResultInfo ;
//		}
//		
//		/// <summary>
//		/// �������ݵ����ݿ���
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		public Common.CResultInfo Add(System.Data.DataTable pDTInfo)
//		{
//			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
//			try
//			{
//				string sInsertSql = GenerateInsertSql(pDTInfo) ;
//				DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
//				pDataAccess.scExecuteQuery(sInsertSql) ; 
//				pResultInfo.bSuccess = true ;
//			}
//			catch(System.Data.SqlClient.SqlException sqlErr)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
//			}
//			catch(System.Exception err)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorDesc = err.Message ;				
//			}
//			return pResultInfo ;
//		}
//		
//		
//		/// <summary>
//		/// �������Ӽ�¼��sql���
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		private string GenerateInsertSql(System.Data.DataTable pDTInfo)
//		{			
//			string sInsertFieldSql = "insert into " + m_sTableCode + "(" ;
//			string sInsertValueSql = "values (" ;
//			foreach(System.Data.DataColumn pDCInfo in pDTInfo.Columns)
//			{
//				if(pDCInfo.ColumnName.ToLower() != "RowStatus")
//				{
//					sInsertFieldSql = sInsertFieldSql + pDCInfo.ColumnName.Substring(pDCInfo.ColumnName.IndexOfAny(".".ToCharArray())+1) + "," ;    
//					sInsertValueSql = sInsertValueSql + pDTInfo.Rows[0][pDCInfo.ColumnName].ToString().Trim() + "," ;
//				}
//			}
//			sInsertFieldSql = sInsertFieldSql.TrimEnd(',') + ")" ;
//			sInsertValueSql = sInsertValueSql.TrimEnd(',') + ")" ;  
//		
//			return sInsertFieldSql + " " + sInsertValueSql ;
//		}
//
//		/// <summary>
//		/// �޸����ݵ�У��
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		public Common.CResultInfo ModifyCheck(System.Data.DataTable pDTInfo)
//		{
//			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
//			try
//			{
//				if(Common.CCommonCheck.CheckDataModified(m_sTableCode,"vID",pDTInfo.Rows[0][m_sTableCode+".vID"].ToString(),pDTInfo.Rows[0]["vTP"].ToString()) == true)
//				{
//					pResultInfo.bSuccess = false ;
//					pResultInfo.ErrorDesc = "�����Ѿ����޸ģ��޸�ʧ��!" ;
//					pResultInfo.ErrorResID = "DataModified" ;
//					return pResultInfo ;
//				}
//			}
//			catch(System.Data.SqlClient.SqlException  sqlErr)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
//			}
//			catch(System.Exception err)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorDesc = err.Message ;				
//			}
//			return pResultInfo ;
//		}
//
//		/// <summary>
//		/// �޸����ݵ����ݿ���
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		public Common.CResultInfo Modify(System.Data.DataTable pDTInfo)
//		{
//			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
//			try
//			{
//				string sModifySql = GenerateModifySql(pDTInfo) ;
//				DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
//				pDataAccess.scExecuteQuery(sModifySql) ;
//				pResultInfo.bSuccess = true ;
//			}
//			catch(System.Data.SqlClient.SqlException  sqlErr)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
//			}
//			catch(System.Exception err)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorDesc = err.Message ;				
//			}
//			return pResultInfo ;
//		}
//
//		private string GenerateModifySql(System.Data.DataTable pDTInfo)
//		{
//			string sModifySql = "update " + m_sTableCode + " set " ;
//			foreach(System.Data.DataColumn pDCInfo in pDTInfo.Columns)
//			{
//				if(pDCInfo.ColumnName != "RowState" && pDCInfo.ColumnName != m_sTableCode + ".vID")
//				{
//					sModifySql = sModifySql + pDCInfo.ColumnName + " = '" + pDTInfo.Rows[0][pDCInfo.ColumnName].ToString() + "'," ; 
//				}
//			}
//			return sModifySql ;
//		}
//
//		/// <summary>
//		/// ɾ������У��
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		public Common.CResultInfo DeleteCheck(System.Data.DataTable pDTInfo)
//		{
//			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
//			try
//			{
//				if(Common.CCommonCheck.CheckDataDeleted(m_sTableCode,"vID",pDTInfo.Rows[0][m_sTableCode+".vID"].ToString()) == true)
//				{
//					pResultInfo.bSuccess = false ;
//					pResultInfo.ErrorDesc = "�����Ѿ���ɾ����ɾ��ʧ��!" ;
//					pResultInfo.ErrorResID = "DataDeleted" ;
//					return pResultInfo ;
//				}
//			}
//			catch(System.Data.SqlClient.SqlException  sqlErr)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
//			}
//			catch(System.Exception err)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorDesc = err.Message ;				
//			}
//			return pResultInfo ;
//		}
//		
//		/// <summary>
//		/// �����ݿ���ɾ������
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		public Common.CResultInfo Delete(System.Data.DataTable pDTInfo)
//		{
//			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
//			try
//			{
//				string sDeleteSql = GenerateDeleteSql(pDTInfo) ;
//				DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
//				pDataAccess.scExecuteQuery(sDeleteSql) ;
//				pResultInfo.bSuccess = true ;
//			}
//			catch(System.Data.SqlClient.SqlException  sqlErr)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
//			}
//			catch(System.Exception err)
//			{
//				pResultInfo.bSuccess = false ;
//				pResultInfo.ErrorDesc = err.Message ;				
//			}
//			return pResultInfo ;
//		}
//
//		/// <summary>
//		/// ����ɾ�����ݵ�sql���
//		/// </summary>
//		/// <param name="pDTInfo"></param>
//		/// <returns></returns>
//		private string GenerateDeleteSql(System.Data.DataTable pDTInfo)
//		{
//			string sDeleteSql = "delete " + m_sTableCode + " where vID = '" + pDTInfo.Rows[0][m_sTableCode+".vID"].ToString() + "'"  ;
//			return sDeleteSql ;
//		}

	}
}