using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using Common;
using Cnwit;

namespace DataEntity
{
	/// <summary>
	/// DAETenderOpen ��������ʵ�� Added by QSQ 12.11
	/// </summary>
	public class DAETenderOpen : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
				CEntityUitlity ceu = new CEntityUitlity();
		public DAETenderOpen()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public DataTable CheckState(String strTenderID )
		{
			string[] sParams = {"tableName","tablePK","tablePKValue","correlationField","ifCorrelation","correlationTable","correlationTableField","correlationFieldTable"} ;
			object[] objParamValues = {"BidOpen","IDKey",strTenderID,"TenderID","1","ITBDocument","ITBIDKey","ITBIDKey" } ; 
			SqlDbType[] paramTypes = { SqlDbType.VarChar , SqlDbType.VarChar , SqlDbType.VarChar, SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar } ;
			DataTable dt = BaseDataAccess.ExecuteSPQueryDataTable("sp_ControlState",sParams,objParamValues,paramTypes );
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select TCStrategy.Status as State From BidOpen  Inner Join ITBDocument On BidOpen.ITBIDKey = ITBDocument.ITBIDKey Inner Join TCStrategy On ITBDocument.TenderID = TCStrategy.TenderID Where BidOpen.IDKey='" + strPKValue + "'" ;
			DataTable dt = _da.GetDataTable(strSql) ; 
			return dt ; 
		}

		//****************************************************

		public DataTable GetType(string strTenderID)
		{
			string strSQL = "SELECT * FROM TCStrategy WHERE  TenderID = '"+strTenderID+"'";
			DataTable dt = _da.GetDataTable(strSQL);
			return dt ;
		}

		public  string GetTenderID(string sIDKey)
		{
			string strSQL = "SELECT TCStrategy.TenderID FROM TCStrategy,ITBDocument,BidOpen WHERE TCStrategy.TenderID = ITBDocument.TenderID AND "+
						" ITBDocument.ITBIDKey = BidOpen.ITBIDKey AND BidOpen.IDKey = '"+sIDKey+"'";
			 int nCount = _da.GetDataTable(strSQL).Rows.Count;
			if(nCount>0)
			{
				return _da.GetDataTable(strSQL).Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}

		public  string GetTCState(string sTenderID)
		{
			string sTenderState  = string.Empty;
			string strSQL = "SELECT status FROM TCStrategy WHERE TCStrategy.TenderID='"+sTenderID+"'";
			if(_da.GetDataTable(strSQL).Rows.Count>0)
			{
				sTenderState = _da.GetDataTable(strSQL).Rows[0][0].ToString();
			}
			else
			{
				sTenderState = "";
			}
			return sTenderState;
		}


		

		public bool IsTechBID(string sTenderID,string sPkvalue)
		{
			//�ڿ����������Ѿ�����
//			string strSQL ="SELECT 1 FROM ServiceRequistion,BidOpen,ITBDocument WHERE BidOpen.ITBIDKey = ITBDocument.ITBIDKey AND "+
//							" ServiceRequistion.SRID = ITBDocument.SRID "+
//                            "  AND ITBDocument.TenderID = '"+sTenderID+"' AND BidOpen.IDKey='"+sPkvalue+"'  AND  EXISTS(SELECT 1 FROM TechEvaluation WHERE TenderID =ITBDocument.TenderID )";
			
			string strSQL ="SELECT 1 FROM TechEvaluation,BidOpen,ITBDocument "+
				" WHERE TechEvaluation.TenderID ='"+sTenderID+"' AND ITBDocument.ITBIDKey = BidOpen.ITBIDKey   AND ITBDocument.TenderID = TechEvaluation.TenderID AND BidOpen.IDKey='"+sPkvalue+"' ";
			int nCount = _da.GetDataTable(strSQL).Rows.Count;
			if(nCount>0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public  void GetIsRecevVendorList(DataTable dtTemp,string sTenderID)
		{
//			string strSQL = "SELECT ITBOpenVendorList.VendorID FROM ITBOpenVendorList,ITBDocument,BidOpen "+
//							" WHERE ITBOpenVendorList.BidOpenID = BidOpen.IDKey AND BidOpen.ITBIDKey=ITBDocument.ITBIDKey AND "+
//				            " ITBDocument.TenderID= '"+sTenderID+"' AND ITBOpenVendorList.Accede = 1 ";
			string strSQL =@"SELECT TechResult.VendorID,Vendor.VendorName FROM TechResult,TechEvaluation,Vendor
									WHERE TechResult.TechEvaluationID = TechEvaluation.IDKey  AND Vendor.IDkey =  TechResult.VendorID
									AND TechEvaluation.TenderID = '"+sTenderID+"' AND TechResult.Passed = 1";
			DataTable dt = _da.GetDataTable(strSQL);
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
				DataRow dr = dtTemp.NewRow();
				dr["IDKey"] = System.Guid.NewGuid().ToString();
				dr["VendorID"] = dt.Rows[i][0].ToString();
				dr["ITBOpenVendorList__VendorID"]  = dt.Rows[i][1].ToString();
				dr["RowStatus"] = "New";
				dtTemp.Rows.Add(dr);

			}



		}

		public int GetOpenTypeID ( string BidOpenIDKey )
		{
			string strSQL = "SELECT OpenTypeID FROM BidOpen WHERE IDKey = '"+BidOpenIDKey+"'";
			DataTable dt = _da.GetDataTable(strSQL);

			if ( dt.Rows.Count > 0 )
			{
				return Convert.ToInt32 ( dt.Rows[0][0].ToString() ) ;
			}
			else
				return 0 ;
		}


		public  void GetVendorList(DataTable dtTemp,string sTenderID)
		{
			string strSQL = "SELECT ITBVendorList.VendorID,Vendor.VendorName FROM ITBVendorList,ITBDocument,Vendor "+
				" WHERE Vendor.IDkey =ITBVendorList.VendorID AND  ITBVendorList.ITBDocumentID = ITBDocument.ITBIDKey AND ITBDocument.TenderID = '"+sTenderID+"'";
			DataTable dt = _da.GetDataTable(strSQL);
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
				DataRow dr = dtTemp.NewRow();
				dr["IDKey"] = System.Guid.NewGuid().ToString();
				dr["ITBOpenVendorList__VendorID"]  = dt.Rows[i][1].ToString();
				dr["VendorID"] = dt.Rows[i][0].ToString();
				dr["RowStatus"] = "New";
				dtTemp.Rows.Add(dr);
			}

		}


		public bool HasTenderOpen(string strTenderID )
		{
			string strSQL = "SELECT * FROM BidOpen,ITBDocument WHERE   BidOpen.ITBIDKey =ITBDocument.ITBIDKey and ITBDocument.TenderID = '"+strTenderID+"'";
            int nCount = _da.GetDataTable(strSQL).Rows.Count;
			if(nCount>0)
			{
				return true;
			}
			else if(nCount==0)
			{
				return false;
			}
			else
			{
				return false;
			}


		}


		/// <summary>
		/// ����SR��״̬
		/// </summary>
		/// <param name="strITBDocumentID">�����IDKey</param>
		/// <param name="State">״̬������Ӧ��Ϊ3������׶Σ�</param>
		/// <returns>������Ϣ</returns>
		public void SetTenderState ( string strITBDocumentID)
		{
		    string strSql = "SELECT TenderID FROM ITBDocument WHERE ITBIDKey = '"+strITBDocumentID+"'";			
			string sTendorID = _da.GetDataTable(strSql).Rows[0][0].ToString();
			string strErrorMsg = string.Empty;

			if(sTendorID!="")
			{
				ceu.UpdateStrategyState(sTendorID,TenderState.State_ITBTechOpen);
			
				string UpdateSql = "UPDATE ITBDocument SET State = "+(int)TenderState.State_ITBTechOpen+" WHERE ITBIDKey = '"+strITBDocumentID+"'" ; 
				strErrorMsg =  _da.ExecuteDMLSQL ( UpdateSql );
			}

		}



		public void SetSRAndITBDocumentState ( string strITBDocumentID )
		{
			string strErrorMsg = string.Empty;
			string strSql = "SELECT TenderID FROM ITBDocument WHERE ITBIDKey = '"+strITBDocumentID+"'";			
			string sTendorID = _da.GetDataTable(strSql).Rows[0][0].ToString();

			if(sTendorID!="")
			{
				ceu.UpdateStrategyState(sTendorID,TenderState.State_ITBCommOpen);
				string UpdateSql = "UPDATE ITBDocument SET State = "+(int)TenderState.State_ITBCommOpen+" WHERE ITBIDKey = '"+strITBDocumentID+"'" ;
				strErrorMsg =  _da.ExecuteDMLSQL ( UpdateSql );
			}

		}



		#region �鿴����ط���
		/// <summary>
		/// �õ����в鿴����Ϣ
		/// </summary>
		/// <returns></returns>
		public DataTable GetSeePerson( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName],
							BI_PositionType.PositionName as [BI_PositionType.PositionName],
							BidOpenPerson.ViewerID as [BidOpenPerson.ViewerID],
							'Edit' RowStatus,
							BidOpenPerson.IDKey as [BidOpenPerson.IDKey],
							BidOpenPerson.OpenID as [BidOpenPerson.OpenID]
							  
						from (((BidOpenPerson left outer join BI_Employee on BidOpenPerson.ViewerID = BI_Employee.IDKey)
						left join  BI_DepartmentEmployee on BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID)
						left join  BI_PositionType on  BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey)
						where BidOpenPerson.OpenID = '"+pkValue+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}

		/// <summary>
		/// ����BI_DepartmentEmployee.IDKey �õ�BI_Employee.IDKey
		/// </summary>
		/// <returns></returns>
		public string GetBI_EmployeeID( string pkValue )
		{
			string strSql = "";
			strSql = @"select	BI_Employee.IDKey
							  
						from 
							BI_Employee,
							BI_DepartmentEmployee
							
						where 
							BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and BI_DepartmentEmployee.IDKey = '"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			if(dt.Rows.Count > 0)
			{
				return dt.Rows[0]["IDKey"].ToString();
			}
			else return "";
		}
		/// <summary>
		/// �õ�ָ�����ŵ���Ա��Ϣ
		/// </summary>
		/// <param name="DepartmentID">����ID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Department( string DepartmentID )
		{
			string strSql = "";
			strSql = @"select  BI_Employee.FullName,
							   BI_PositionType.PositionName,
							   BI_Employee.IDKey,
							   BI_DepartmentEmployee.DepartmentID

						from BI_Employee,
							 BI_Department,
							 BI_DepartmentEmployee,
							 BI_PositionType
						where BI_DepartmentEmployee.DepartmentID = '"+DepartmentID+"'";
			strSql += @"and BI_Department.IDKey = BI_DepartmentEmployee.DepartmentID
					   and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
					   and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						";

			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		/// <summary>
		/// �õ������û���Ϣ
		/// </summary>
		/// <param name="UserID">�û�ID(Ա�����е�IDKey,BI_Employee)</param>
		/// <returns></returns>
		public DataTable GetSeePerson_User( string UserID )
		{
			string strSql = "";
			strSql = @"select BI_Employee.FullName ,
							BI_Employee.IDKey,
							BI_PositionType.PositionName
						from (BI_Employee left outer join BI_DepartmentEmployee 
							on BI_DepartmentEmployee.EmployeeID = BI_Employee.IDKey)
							left outer join BI_PositionType
							on BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						where BI_Employee.IDKey='"+UserID+"'";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// �õ�ָ�������Ա��Ϣ
		/// </summary>
		/// <param name="UserID">GroupID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Group( string GroupID )
		{
			string strSql = "";
			strSql = @"select  
							BI_Employee.FullName,
							BI_PositionType.PositionName,
  							BI_Employee.IDKey,
							BI_DepartmentEmployee.DepartmentID

						from TI_DefaultGroupMember,
							 BI_Employee,
							 BI_PositionType,
							 BI_DepartmentEmployee

						where TI_DefaultGroupMember.IDKEY = '"+GroupID+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		public void DelViewer(string strIDKey)
		{
			string strSql = "delete BidOpenPerson where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		/// <summary>
		/// ��ñ������ϸ����
		/// </summary>
		/// <param name="ITBNumber">������</param>
		/// <returns></returns>
		public DataTable GetITBInfo ( string strTenderID )
		{
			string SelectSql = "SELECT ITBIDKey,ITBNumber,ObjectName FROM ITBDocument WHERE TenderID = '"+strTenderID+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			
			return dt_Temp;
		}
	}
}
