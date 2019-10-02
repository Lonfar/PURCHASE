using System;
using System.Data;
using Cnwit.Utility;
using Common;
using System.Collections;
namespace DataEntity
{
	/// <summary>
	/// DAETenderGroup ��ժҪ˵����
	/// </summary>
	public class DAEApproveFlow:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEApproveFlow()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �õ���Ӧ�����б������Ľ������
		/// </summary>
		/// <returns></returns>
		public decimal GetUpperBudget( string strType,string idKey)
		{
			string sql="";
			sql =@"SELECT  MAX(UpperBudget) as maxBudget
					FROM TI_ApproveFlow
					WHERE (ApprovalTypeID = '"+strType+"') and idKey<>'"+idKey+"' GROUP BY ApprovalTypeID";  

			DataTable dt=_da.GetDataTable(sql);
			if (dt.Rows.Count==0){return -1;}
			if(dt.Rows[0][0]!=System.DBNull.Value)
			{
				return (decimal)dt.Rows[0][0];
			}
			else
			{
				return -1;
			}
		}
		/// <summary>
		/// �õ���Ӧ�����б�����С�Ľ������
		/// </summary>
		/// <returns></returns>
		public decimal GetLowerBudget( string strType,string idKey)
		{
			string sql="";
			sql =@"SELECT  MIN(LowerBudget) as maxBudget
					FROM TI_ApproveFlow
					WHERE (ApprovalTypeID = '"+strType+"') and idKey<>'"+idKey+"' GROUP BY ApprovalTypeID";  

			DataTable dt=_da.GetDataTable(sql);
			if (dt.Rows.Count==0){return -1;}
			if(dt.Rows[0][0]!=System.DBNull.Value)
			{
				return (decimal)dt.Rows[0][0];
			}
			else
			{
				return -1;
			}
		}
		
		/// <summary>
		/// �õ������Լ���������н���ʱ��
		/// </summary>
		/// <returns></returns>
		public DataTable GetTable( string strType,string idKey)
		{
			DataTable dt1 = new DataTable();
			string sql="";
			sql =@"SELECT  IDKey,
						   ApprovalTypeID,
						   UpperBudget as 'TI_ApproveFlow.UpperBudget',
						   LowerBudget as 'TI_ApproveFlow.LowerBudget'
					FROM TI_ApproveFlow
					WHERE (ApprovalTypeID = '"+strType+"') and idKey<>'"+idKey+"'";  

			dt1 =_da.GetDataTable(sql);
			return dt1;
		}
		/// <summary>
		/// ת��DataTable�е�LowerBudget��UpperBudget��2ά������
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public decimal[,] GetArray(System.Data.DataTable dt)
		{
			//Num ��¼dt�ļ�¼�����ж��ٸ����Σ�
			int Num= 0;
			Num = dt.Rows.Count;
			
			decimal [,]myArr = new decimal[Num,2];
			for(int i=0;i<Num;i++)
			{
				myArr[i,0] = Convert.ToDecimal(dt.Rows[i]["TI_ApproveFlow.LowerBudget"]);
				myArr[i,1] = Convert.ToDecimal(dt.Rows[i]["TI_ApproveFlow.UpperBudget"]);
			}

			return myArr;
		}

		#region Added by Liujun at 11.24
		/// <summary>
		/// ��������Ƿ�����(�Ƿ�����)
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public bool ValidateProcess ( DataTable dt , ref int iMaxStep )
		{
			ArrayList iStepList = new ArrayList();	// �����б�
			// int iMaxStep = 0;	// �������
			int iTemp = 0;		// ��ʱ����
			bool IsOk = true;	// ����״̬

			// �ҵ������
			foreach ( DataRow dr in dt.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					iTemp = Convert.ToInt32( dr["TI_ApproveFlowMember.ApproeLevel"] );

					if ( iTemp > iMaxStep )
					{
						iMaxStep = iTemp;
					}


					// �����в����������б���
					iStepList.Add ( iTemp );
				}
			}

			for ( int i = 1 ; i < iMaxStep + 1 ; i ++ )
			{
				if ( !iStepList.Contains( i ) )
				{
					IsOk = false;
					break;
				}
			}
			return IsOk;
		}


		/// <summary>
		/// У�����������Ƿ���һ�����ϵ�ͨ·
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="iStepList"></param>
		/// <returns></returns>
		public string ValidateData ( DataTable dt , int iMaxStep )
		{
			DataRow[] drs;

			string strStep = string.Empty;

			// modified by wanglijie on 2008-02-01
//			for ( int i = 2 ; i < iMaxStep + 1 ; i ++ )
			for ( int i = 1 ; i < iMaxStep + 1 ; i ++ )		
			{
				drs = dt.Select( "TI_ApproveFlowMember.ApproeLevel = "+ i );
				
				bool IsOk = false; // Ϊ��һ������(����)�Ƿ����һ��ͨ·

				for ( int j = 0 ; j < drs.Length ; j ++ )
				{
					if ( ValidateData ( drs[j] ).Length == 0 ) 
					{
						IsOk = true;
						break;
					}					
				}
				if ( !IsOk )  
				{
					strStep = i.ToString();
				}
				break;
			}

			return strStep;
		}

		/// <summary>
		/// У�����������Ƿ���һ�����ϵ�ͨ·(������һ����)
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="iStepList"></param>
		/// <returns></returns>
		public string ValidateData ( DataTable dt , int iMaxStep , int iState )
		{
			DataRow[] drs;

			string strStep = string.Empty;

			for ( int i = 1 ; i < iMaxStep + 1 ; i ++ )
			{
				drs = dt.Select( "TI_ApproveFlowMember.ApproeLevel = "+ i );
				
				bool IsOk = false; // Ϊ��һ������(����)�Ƿ����һ��ͨ·

				for ( int j = 0 ; j < drs.Length ; j ++ )
				{
					if ( ValidateData ( drs[j] ).Length == 0 ) {IsOk = true;break;}
					
				}

				if ( !IsOk )  {strStep = i.ToString();}
				break;
			}

			return strStep;
		}
		
		/// <summary>
		/// ���ÿ�����̲����Ƿ�����
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string ValidateData ( DataTable dt , DataRow dr , int index )
		{
			string strErrorMsg = string.Empty;	// ������Ϣ
			string SelectSql = string.Empty;	
			DataTable dt_Temp;
			//
			//			foreach ( DataRow dr in dt.Rows )
			//			{
			if ( Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] ) != "" && Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] ) != "" )
			{
			
				SelectSql = @"SELECT * 
									FROM BI_DepartmentEmployee 
									JOIN BI_Department ON BI_Department.IDKey = BI_DepartmentEmployee.DepartmentID
									JOIN BI_PositionType ON BI_PositionType.IDKey = BI_DepartmentEmployee.PositionID
									WHERE DepartmentID = '"+Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] )+"' AND PositionID = '"+Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] )+"'";
				dt_Temp =_da.GetDataTable( SelectSql );
				if ( dt_Temp.Rows.Count == 0 )
				{
					string selectSql_Department = "SELECT BI_Department.DepartmentName From BI_Department WHERE IDKey = '"+Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] )+"'";

					dt_Temp = _da.GetDataTable ( selectSql_Department ) ; 
					if ( dt_Temp.Rows.Count > 0 )
					{
						// ��ò������� 
						strErrorMsg += Convert.ToString( dt_Temp.Rows[0]["DepartmentName"] );
					}
					string selectSql_Position = "SELECT BI_PositionType.PositionName  FROm BI_PositionType WHERE IDKey = '"+Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] )+"'";

					dt_Temp = _da.GetDataTable ( selectSql_Position) ;
					if ( dt_Temp.Rows.Count > 0 )
					{
						strErrorMsg += Convert.ToString(dt_Temp .Rows[0]["PositionName"] );
					}
					dt.Rows.RemoveAt ( index );

					dt.AcceptChanges();
				}
			}
			//			}
			return strErrorMsg;
		}

		/// <summary>
		/// ���ָ���Ĳ����Ƿ�����
		/// </summary>
		/// <param name="dr"></param>
		/// <returns>����ְλ�Ͳ��ŵ�����</returns>
		public string ValidateData ( DataRow dr )
		{
			string strErrorMsg = string.Empty;	// ������Ϣ
			string SelectSql = string.Empty;	
			DataTable dt_Temp;
			if ( Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] ) != "" && Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] ) != "" )
			{
			
				SelectSql = @"SELECT * 
									FROM BI_DepartmentEmployee 
									JOIN BI_Department ON BI_Department.IDKey = BI_DepartmentEmployee.DepartmentID
									JOIN BI_PositionType ON BI_PositionType.IDKey = BI_DepartmentEmployee.PositionID
									WHERE DepartmentID = '"+Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] )+"' AND PositionID = '"+Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] )+"'";
				dt_Temp =_da.GetDataTable( SelectSql );
				if ( dt_Temp.Rows.Count == 0 )
				{
					string selectSql_Department = "SELECT BI_Department.DepartmentName From BI_Department WHERE IDKey = '"+Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] )+"'";

					dt_Temp = _da.GetDataTable ( selectSql_Department ) ; 
					if ( dt_Temp.Rows.Count > 0 )
					{
						// ��ò������� 
						strErrorMsg += Convert.ToString( dt_Temp.Rows[0]["DepartmentName"] );
					}
					string selectSql_Position = "SELECT BI_PositionType.PositionName  FROm BI_PositionType WHERE IDKey = '"+Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] )+"'";

					dt_Temp = _da.GetDataTable ( selectSql_Position) ;
					if ( dt_Temp.Rows.Count > 0 )
					{
						strErrorMsg += Convert.ToString(dt_Temp .Rows[0]["PositionName"] );
					}
				}
			}
		
			return strErrorMsg;
		}

		/// <summary>
		/// ���ÿ�����̲����Ƿ�����
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string ValidateData ( DataTable dt )
		{
			string strErrorMsg = string.Empty;	// ������Ϣ
			string SelectSql = string.Empty;	
			DataTable dt_Temp;
			
			foreach ( DataRow dr in dt.Rows )
			{
				if ( Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] ) != "" && Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] ) != "" )
				{
					SelectSql = @"SELECT * 
										FROM BI_DepartmentEmployee 
										JOIN BI_Department ON BI_Department.IDKey = BI_DepartmentEmployee.DepartmentID
										JOIN BI_PositionType ON BI_PositionType.IDKey = BI_DepartmentEmployee.PositionID
										WHERE DepartmentID = '"+Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] )+"' AND PositionID = '"+Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] )+"'";

					dt_Temp =_da.GetDataTable( SelectSql );
					if ( dt_Temp.Rows.Count == 0 )
					{
						string selectSql_Department = "SELECT BI_Department.DepartmentName From BI_Department WHERE IDKey = '"+Convert.ToString( dr["TI_ApproveFlowMember.ApproeDepartmentID"] )+"'";

						dt_Temp = _da.GetDataTable ( selectSql_Department ) ; 
						if ( dt_Temp.Rows.Count > 0 )
						{
							// ��ò������� 
							strErrorMsg += Convert.ToString( dt_Temp.Rows[0]["DepartmentName"] );
						}
						string selectSql_Position = "SELECT BI_PositionType.PositionName  FROm BI_PositionType WHERE IDKey = '"+Convert.ToString( dr["TI_ApproveFlowMember.PositionID"] )+"'";

						dt_Temp = _da.GetDataTable ( selectSql_Position) ;
						if ( dt_Temp.Rows.Count > 0 )
						{
							strErrorMsg += Convert.ToString(dt_Temp .Rows[0]["PositionName"] );
						}

						dt.AcceptChanges();
					}
				}
			}
			return strErrorMsg;
		}

		#region 

		/// <summary>
		/// ����������ID�����ÿ���������Ƿ��ж�Ӧ��Ա����
		/// </summary>
		/// <param name="strApproveFlowID">��������ID</param>
		/// <returns>û�ж�Ӧ��Ա�Ĳ��ż�ְλ</returns>
		/// <remarks>�����ִ���:1.��Ӧ���ż�ְλû�ж�Ӧ��Ա,��":"��β.2,"NoApproveFlowMember":û���������̳�Ա</remarks>
		public string ValidateData ( string strApproveFlowID )
		{
			// ������Ϣ
			string strErrorMsg = string.Empty ;

			// ����ͨ����������ID�������������
			string SelectSql = "SELECT TI_ApproveFlowMember.ApproeDepartmentID ,  TI_ApproveFlowMember.PositionID FROM TI_ApproveFlowMember WHERE IDKey = '"+strApproveFlowID+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );

			if ( dt_Temp.Rows.Count > 0 )
			{
				foreach ( DataColumn dc in dt_Temp.Columns  )
				{
					dc.ColumnName = "TI_ApproveFlowMember." + dc.ColumnName; 
				}

				strErrorMsg = ValidateData( dt_Temp );

				if ( strErrorMsg != string.Empty )
				{
					strErrorMsg += ":";
				}
			}
			else
			{
				strErrorMsg = "NoApproveFlowMember";
			}

			return strErrorMsg ;
		}

		#endregion

		/// <summary>
		/// Add by Liujun at 0208 ��������������е�һ������û��ָ�����ŵ����
		/// </summary>
		/// <param name="strApproveFlowID">����ID</param>
		/// <param name="strAppDepartment">���벿��ID</param>
		/// <returns></returns>
		public string ValidateData ( string strApproveFlowID , string strAppDepartment )
		{
			// ������Ϣ
			string strErrorMsg = string.Empty ;

			// ����ͨ����������ID�������������
			string SelectSql = "SELECT TI_ApproveFlowMember.ApproeDepartmentID ,  TI_ApproveFlowMember.PositionID , TI_ApproveFlowMember.ApproeLevel FROM TI_ApproveFlowMember WHERE IDKey = '"+strApproveFlowID+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			if ( dt_Temp.Rows.Count > 0 )
			{
				// �����һ������û��ָ������,�����벿��ָ��
				foreach ( DataRow dr in dt_Temp.Rows )
				{
					if ( Convert.ToInt32( dr["ApproeLevel"] ) == 1 && dr["ApproeDepartmentID"] == DBNull.Value )
					{
						dr["ApproeDepartmentID"] = strAppDepartment;
					}
				}

				foreach ( DataColumn dc in dt_Temp.Columns  )
				{
					dc.ColumnName = "TI_ApproveFlowMember." + dc.ColumnName; 
				}

				SelectSql = "SELECT MAX(ApproeLevel) FROM TI_ApproveFlowMember WHERE IDKey = '"+strApproveFlowID+"'";
				

				strErrorMsg = ValidateData( dt_Temp , Convert.ToInt32( _da.GetDataTable ( SelectSql ).Rows[0][0] ));

				if ( strErrorMsg != string.Empty )
				{
					strErrorMsg += ":";
				}
			}
			else
			{
				strErrorMsg = "NoApproveFlowMember";
			}

			return strErrorMsg ;
		}

		#endregion
	}
}
