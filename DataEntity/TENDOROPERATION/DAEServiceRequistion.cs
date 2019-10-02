using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEServiceRequistion ��ժҪ˵����
	/// </summary>
	public class DAEServiceRequistion:DAEBase
	{

		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;
		
		public DAEServiceRequistion()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public DataTable GetPrintData ( string sSRID )
		{
			string sSelectSql = "SELECT * FROM v_Report_SRPrint WHERE SRIDKey = '"+sSRID+"'";
			DataTable dtData = _da.GetDataTable ( sSelectSql );
			return dtData;
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
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
		/// ͨ��������IDȡ�������Ӳ��ŵ�EmployeeID,Create by WXC
		/// </summary>
		public DataSet GetChildEmployeeID(string pDepartmentIdkey)
		{
			DataSet dsDepartmentIDKey=new DataSet();
			//f_CDepartment_IDKey������������
			strSql="SELECT a.* FROM BI_Department a,f_CDepartment_IDKey(pDepartmentIdkey) b WHERE a.IDKey=b.[ID]";
			DataTable dtDepartmentId=_da.GetDataTable(strSql);

			//�Ӳ��ŵĸ���
			if(dtDepartmentId.Rows.Count>0)
			{
				DataTable[] dtDepartmentEmployeeId=new DataTable[dtDepartmentId.Rows.Count];
				for(int i=0;i<dtDepartmentId.Rows.Count;i++)
				{
					//���Ӳ�����ȥ��������������ŵ�Ա��
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
		/// ͨ��������IDȡ�������Ӳ��ŵ�DepartmentIDKey,ͨ�����ݿ���Զ��庯������ȵݹ���ң�Ч���Ը�����ȵݹ飩ʵ�ֲ��ң�Ч��ͦ�ߵġ� Create by WXC
		/// �������ƣ� f_CDepartment_IDKey   
		/// --��ѯָ���ڵ㼰�������ӽڵ�ĺ���
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
			//���ҳ������Ա�ܹ��м��ֽ�ɫ����ɫ����˼���п�����һ�����ŵ��쵼���������ŵ�Ա��
			int j = dtIsDepartmentManagement.Rows.Count;
			//�ֱ�Ϊ��Щ��ɫ����DataTable
			DataTable[] dt = new DataTable[j];
			string[] dTableName =new string[j];
			//ѭ����ʼ��DataTable
			for(int m=0;m<j;m++)
			{
				dt[m] = new DataTable();
				dTableName[m] = "dTableName"+m;
				dt[m].TableName = dTableName[m];
			}
			//һ��ֻ��һ������
			for(int i=0;i<j;i++)
			{
				//����ǲ��ŵĹ�����
				if(dtIsDepartmentManagement.Rows[i]["Principal"].ToString().Trim().ToUpper()=="TRUE")
				{
					//�����Կ�������������Ա���ļ�¼������GetChildDepartmentIDKey������ȡ�������Ӳ��ŵ�ID
				    DataTable dtTemp = GetChildDepartmentIDKey(dtIsDepartmentManagement.Rows[i]["DepartmentID"].ToString().Trim());
					dtTemp.TableName = "dtTemp"+i;
					dt[i]=dtTemp.Copy();
					//���ǲ��Ź���Ĳ���ID���뵽DataTable
					ds.Tables.Add(dt[i]);
				}
				//���Ǵ˲��ŵĹ�����
				else
				{
					//ֻ�ܿ����Լ��ļ�¼������һ��DataTable 
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

		
		#region ɾ���鿴�˼�¼ Added by QSQ 11.20
		/// <summary>
		/// ɾ��������¼
		/// </summary>
		/// <param name="strID">���</param>
		public void DelMultiViewer(string SRIDKey)
		{
			string strSql = "delete ServiceRequestViewer where SRIDKey ='"+SRIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion
	}
}
