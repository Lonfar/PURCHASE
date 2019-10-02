using System;
using System.Data;
using Cnwit.Utility;
using Common;
using System.Collections;
namespace DataEntity
{
	/// <summary>
	/// DAETenderGroup 的摘要说明。
	/// </summary>
	public class DAEApproveFlow:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEApproveFlow()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 得到相应下拉列表中最大的金额上限
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
		/// 得到相应下拉列表中最小的金额下限
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
		/// 得到除了自己以外的所有金额段时间
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
		/// 转化DataTable中的LowerBudget，UpperBudget到2维数组中
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public decimal[,] GetArray(System.Data.DataTable dt)
		{
			//Num 记录dt的记录数（有多少个金额段）
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
		/// 检查流程是否完整(是否连续)
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public bool ValidateProcess ( DataTable dt , ref int iMaxStep )
		{
			ArrayList iStepList = new ArrayList();	// 步骤列表
			// int iMaxStep = 0;	// 最大步骤数
			int iTemp = 0;		// 临时变量
			bool IsOk = true;	// 最终状态

			// 找到最大步骤
			foreach ( DataRow dr in dt.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					iTemp = Convert.ToInt32( dr["TI_ApproveFlowMember.ApproeLevel"] );

					if ( iTemp > iMaxStep )
					{
						iMaxStep = iTemp;
					}


					// 将所有步骤数放入列表中
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
		/// 校验审批流程是否有一个以上的通路
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
				
				bool IsOk = false; // 为这一个级别(步骤)是否存在一个通路

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
		/// 校验审批流程是否有一个以上的通路(不检查第一级别)
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
				
				bool IsOk = false; // 为这一个级别(步骤)是否存在一个通路

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
		/// 检查每个流程步骤是否有人
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string ValidateData ( DataTable dt , DataRow dr , int index )
		{
			string strErrorMsg = string.Empty;	// 错误信息
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
						// 获得部门名称 
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
		/// 检查指定的步骤是否有人
		/// </summary>
		/// <param name="dr"></param>
		/// <returns>返回职位和部门的名称</returns>
		public string ValidateData ( DataRow dr )
		{
			string strErrorMsg = string.Empty;	// 错误信息
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
						// 获得部门名称 
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
		/// 检查每个流程步骤是否有人
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string ValidateData ( DataTable dt )
		{
			string strErrorMsg = string.Empty;	// 错误信息
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
							// 获得部门名称 
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
		/// 根据审批流ID来检查每个流程中是否有对应人员存在
		/// </summary>
		/// <param name="strApproveFlowID">审批流程ID</param>
		/// <returns>没有对应人员的部门及职位</returns>
		/// <remarks>有两种错误:1.相应部门及职位没有对应人员,以":"结尾.2,"NoApproveFlowMember":没有审批流程成员</remarks>
		public string ValidateData ( string strApproveFlowID )
		{
			// 错误信息
			string strErrorMsg = string.Empty ;

			// 首先通过审批流程ID来获得审批步骤
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
		/// Add by Liujun at 0208 解决的审批流程中第一个步骤没有指定部门的情况
		/// </summary>
		/// <param name="strApproveFlowID">流程ID</param>
		/// <param name="strAppDepartment">申请部门ID</param>
		/// <returns></returns>
		public string ValidateData ( string strApproveFlowID , string strAppDepartment )
		{
			// 错误信息
			string strErrorMsg = string.Empty ;

			// 首先通过审批流程ID来获得审批步骤
			string SelectSql = "SELECT TI_ApproveFlowMember.ApproeDepartmentID ,  TI_ApproveFlowMember.PositionID , TI_ApproveFlowMember.ApproeLevel FROM TI_ApproveFlowMember WHERE IDKey = '"+strApproveFlowID+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			if ( dt_Temp.Rows.Count > 0 )
			{
				// 如果第一个步骤没有指定部门,则将申请部门指定
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
