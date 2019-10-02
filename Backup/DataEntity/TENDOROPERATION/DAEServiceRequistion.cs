using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEServiceRequistion 的摘要说明。
	/// </summary>
	public class DAEServiceRequistion:DAEBase
	{

		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;
		
		public DAEServiceRequistion()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetPrintData ( string sSRID )
		{
			string sSelectSql = "SELECT * FROM v_Report_SRPrint WHERE SRIDKey = '"+sSRID+"'";
			DataTable dtData = _da.GetDataTable ( sSelectSql );
			return dtData;
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
		public DataTable CheckState(String strTenderID )
		{
			String strSql = " Select Max(Contract.State) as CheckState From ServiceRequistion  Inner Join ContractRequistion On ServiceRequistion.SRID = ContractRequistion.SRID Inner Join Contract On Contract.IDkey = ContractRequistion.ContractID Where ServiceRequistion.IDKey ='" + strTenderID + "'" ;
			DataTable dt = _da.GetDataTable(strSql) ; 
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select ServiceRequistion.SRState As State  From ServiceRequistion Where ServiceRequistion.IDKey='" + strPKValue + "'" ;
			DataTable dt = _da.GetDataTable(strSql) ; 
			return dt ; 
		}

		//****************************************************


		/// <summary>
		/// 通过父部门ID取得所有子部门的EmployeeID,Create by WXC
		/// </summary>
		public DataSet GetChildEmployeeID(string pDepartmentIdkey)
		{
			DataSet dsDepartmentIDKey=new DataSet();
			//f_CDepartment_IDKey函数介绍如下
			strSql="SELECT a.* FROM BI_Department a,f_CDepartment_IDKey(pDepartmentIdkey) b WHERE a.IDKey=b.[ID]";
			DataTable dtDepartmentId=_da.GetDataTable(strSql);

			//子部门的个数
			if(dtDepartmentId.Rows.Count>0)
			{
				DataTable[] dtDepartmentEmployeeId=new DataTable[dtDepartmentId.Rows.Count];
				for(int i=0;i<dtDepartmentId.Rows.Count;i++)
				{
					//从子部门中去遍历查找这个部门的员工
					string sql="SELECT * FROM BI_DepartmentEmployee WHERE DepartmentID='"+dtDepartmentId.Rows[i]["IDKey"].ToString()+"'";
					dtDepartmentEmployeeId[i]=_da.GetDataTable(sql);
					if(dtDepartmentEmployeeId[i].Rows.Count>0)
					{
						dsDepartmentIDKey.Tables.Add(dtDepartmentEmployeeId[i]);
						dtDepartmentEmployeeId[i].Clear();
					}
					
				}
				if(dsDepartmentIDKey.Tables.Count>0)
				{
					return dsDepartmentIDKey;
				}
				else
				{
					return null;
				}


			}
			else
			{

				return null;

			}

		}

		/// <summary>
		/// 通过父部门ID取得所有子部门的DepartmentIDKey,通过数据库的自定义函数（广度递归查找，效率稍高于深度递归）实现查找，效率挺高的。 Create by WXC
		/// 函数名称： f_CDepartment_IDKey   
		/// --查询指定节点及其所有子节点的函数
						/*--Create by WXC
						CREATE FUNCTION f_CDepartment_IDKey(@ID varchar(64))
							RETURNS @t_Level TABLE([ID] varchar(64),[Level] int)
						AS
							BEGIN
								DECLARE @Level int
								SET @Level=1
								INSERT @t_Level SELECT @ID,@Level
									WHILE @@ROWCOUNT>0
											BEGIN
										SET @Level=@Level+1
										INSERT @t_Level SELECT a.IDKey,@Level
										FROM BI_Department a,@t_Level b
										WHERE a.PDepartmentID=b.[ID]
										AND b.[Level]=@Level-1
								END
							RETURN
							END*/
		/// </summary>
		public DataTable GetChildDepartmentIDKey(string pDepartmentIdkey)
		{	
			DataSet dsDepartmentIDKey=new DataSet();
			strSql="SELECT a.IDKey,'1' AS IScDepartmentID FROM BI_Department a,f_CDepartment_IDKey('"+pDepartmentIdkey+"') b WHERE a.IDKey=b.[ID]";
			DataTable dtDepartmentId=_da.GetDataTable(strSql);
			return dtDepartmentId;

		}

		public DataSet GetAllDepartmentID(string strEmployeeID)
		{
			strSql="SELECT * FROM BI_DepartmentEmployee WHERE EmployeeID ='"+ strEmployeeID +"'";
			DataTable dtIsDepartmentManagement=_da.GetDataTable(strSql);
			DataSet ds=new DataSet();
			//查找出这个雇员总共有几种角色：角色的意思是有可能是一个部门的领导和另几个部门的员工
			int j = dtIsDepartmentManagement.Rows.Count;
			//分别为这些角色构造DataTable
			DataTable[] dt = new DataTable[j];
			string[] dTableName =new string[j];
			//循环初始化DataTable
			for(int m=0;m<j;m++)
			{
				dt[m] = new DataTable();
				dTableName[m] = "dTableName"+m;
				dt[m].TableName = dTableName[m];
			}
			//一般只有一个部门
			for(int i=0;i<j;i++)
			{
				//如果是部门的管理者
				if(dtIsDepartmentManagement.Rows[i]["Principal"].ToString().Trim().ToUpper()=="TRUE")
				{
					//他可以看见本部门所以员工的记录，调用GetChildDepartmentIDKey函数，取得所有子部门的ID
				    DataTable dtTemp = GetChildDepartmentIDKey(dtIsDepartmentManagement.Rows[i]["DepartmentID"].ToString().Trim());
					dtTemp.TableName = "dtTemp"+i;
					dt[i]=dtTemp.Copy();
					//把是部门管理的部门ID插入到DataTable
					ds.Tables.Add(dt[i]);
				}
				//不是此部门的管理者
				else
				{
					//只能看见自己的记录，构造一个DataTable 
					DataColumn colDepartmentID=new DataColumn("IDKey",Type.GetType("System.String"));
					dt[i].Columns.Add(colDepartmentID);
					DataColumn colIScDepartmentID=new DataColumn("IScDepartmentID",Type.GetType("System.String"));
					dt[i].Columns.Add(colIScDepartmentID);

                    DataRow dr = dt[i].NewRow();
					dr[0]=dtIsDepartmentManagement.Rows[i]["DepartmentID"].ToString().Trim();
					dr[1]="0";
					dt[i].Rows.Add(dr);
					ds.Tables.Add(dt[i]);
					
				}
			}
			return ds;
		}

		
		#region 删除查看人记录 Added by QSQ 11.20
		/// <summary>
		/// 删除多条记录
		/// </summary>
		/// <param name="strID">外键</param>
		public void DelMultiViewer(string SRIDKey)
		{
			string strSql = "delete ServiceRequestViewer where SRIDKey ='"+SRIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion
	}
}
