using System;
using Common;
using DictionaryAccess;
using System.Collections;
using System.Data;
using Cnwit.Utility;
using System.Data.SqlClient;
namespace Business
{
	/// <summary>
	/// ҵ����Ļ���
	/// </summary>
	public class BUSBase:IBusiness
	{
		private IDataEntity _ida=null;	
		private const string BUSCHKERR="ERRBUS01";
		private const string BUSCALERR="ERRBUS02";
		private const string BUSDATERR="ERRBUS03";
		private const string BUSDATERR_SQL="ERRBUS04";
		private const string BUSDATERR_PK="ERRBUS05";
		private DataAcess _da = Common.GetProjectDataAcess.GetDataAcess() ; 
		public BUSBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			
		}
		#region IBusiness ��Ա

		public IDataEntity IEntity
		{
			set
			{
				_ida=value;
			}
			get
			{
				return _ida;
			}
		}

		#endregion

		#region IBusinessSaveData ��Ա
		/// <summary>
		///check business logic and business calculation and check data 
		/// </summary>
		/// <returns></returns>
		public string CheckBusinessDataAndLogic(DataTable dt,ArrayList fieldsList)
		{
			string errMsg1="",errMsg2="",errMsg3="",errMsg="";
			
			/*����ҵ�����ݣ�ҵ���߼�У��,����
			 * ���ҵ�������Ѿ���ҳ���У��ͨ���ˣ������ڴ�ֻ����ҵ�����У���ҵ���߼����� */
			errMsg1=CheckBusinessLogic_rule(dt,fieldsList);	
			if (errMsg1!=""){errMsg=errMsg1;}
			errMsg2=CheckBusinessLogic_calculate(dt,fieldsList);	
			if (errMsg2!=""){errMsg=errMsg2;}
			errMsg3=CheckBusinessData(dt,fieldsList);	
			if (errMsg3!=""){errMsg=errMsg3;}
			//return errMsg1+ errMsg2+ errMsg3;		//ֻ�������һ�δ����	
			return errMsg;
		}
		/// <summary>
		/// save data in business layer
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public string BusinessSaveData(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{	
			string errMsg="";
			string chkMsg="";
			chkMsg=CheckBusinessDataAndLogic(dt,fieldsList);
			if (chkMsg.Trim().Length==0)
			{
				_ida.CurDataTable=dt;					
				errMsg= _ida.Save();
			}
			else
				errMsg=chkMsg;
			return errMsg;
		}

		#endregion

		#region IBusinessDeleteData ��Ա
		/// <summary>
		/// delete Data in business layer
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="pkField"></param>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		public string BusinessDeleteData(ArrayList alDeleteData)
		{
			string errMsg="";				
			errMsg=_ida.Delete(alDeleteData);			
			return errMsg;			
		}

		#endregion
		#region IBusinessQueryData ��Ա
		public string BusinessQueryData ( string sql)
		{
			return "";
		}
		#endregion
		#region ������������õ��鷽������������ʹ�������鷽�����������֣�������Ժ��Ը÷���
		/*���ݲ�ͬҵ����Ҫ�ڴ��Զ���˽�з�������ҵ������У������߼������*/
		/*�������У������ҵ�����ɣ������ڽ���У�飬����Ժ�����Щ����*/
		
		/// <summary>
		/// the rule of business check;for example: the endtime must be >=start time  and so on.
		/// </summary>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public virtual string CheckBusinessLogic_rule(DataTable dt,ArrayList fieldsList)
		{
			return "";
		}


		

		/// <summary>
		/// logical of business calculate.for example :among of fields calculate and so on.
		/// </summary>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public virtual string CheckBusinessLogic_calculate(DataTable dt,ArrayList fieldsList)
		{

			return "";
		}


		/// <summary>
		/// check data's validity
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns>if sucess --"" else return error message</returns>
		public virtual string CheckBusinessData(DataTable dt,ArrayList fieldsList)
		{
			string pkName=_ida.TableName+"."+_ida.PKFieldName;
			if (pkName.IndexOf(",")>=0){return "";}
			try
			{
				foreach(DataRow dr in dt.Rows)
				{
					if(dr.RowState != DataRowState.Deleted)
					{
						if (dr[pkName]==System.DBNull.Value )
						{
							dr[pkName]=System.Guid.NewGuid().ToString() ; 
						}
					}
				}
			}
			catch
			{
				return BUSDATERR;
			}			
			return CheckBUSPKExistForInsert(dt.Copy(),_ida.TableName,_ida.BusPKFieldName);			
		}		
		#endregion
		#region The private Methods of  Business 's base class
		/// <summary>
		/// if business pk exists in DB then return the  prompt of this error,else return "";
		/// </summary>		
		/// <returns></returns>
		/// 
		private string CheckBUSPKExistForInsert(DataTable dt,string tableName,string busPKName)
		{
			//dt.AcceptChanges();
			string errMsg="";
			if (dt.Rows.Count>1)
			{
				errMsg = CheckBUSPKRepeatInDataTable(dt.Copy(),tableName,busPKName);
			}
			if (errMsg.Length>0)
			{
				return errMsg;
			}
			string pkName=_ida.TableName+"."+_ida.PKFieldName;
			bool bcheckDB = false ;
			string deletedParas = " and not exists(select 1 from "+tableName+" where "+_ida.PKFieldName+" = "+pkName+" and("; 
			foreach(DataRow dr in dt.Rows)
			{
				if(dr.RowState == DataRowState.Deleted)
				{
					dr.RejectChanges();
					deletedParas +=" "+_ida.PKFieldName+" = '"+dr[pkName].ToString() +"' or";
					bcheckDB = true ;

				}
			}
			deletedParas = deletedParas.TrimEnd("or".ToCharArray());
			if(bcheckDB == true)
			{
				deletedParas += "))";
			}
			else
			{
				deletedParas= "";
			}
			string a= deletedParas ;
			foreach(DataRow dr in dt.Rows)
			{
				if (dr["RowStatus"].ToString().ToUpper()=="EDIT" ){continue;}	
				if (dr.RowState==DataRowState.Deleted) 
				{
					continue;
				}	
				errMsg=GenerateSQLToCheckBUSPK(tableName,dr,busPKName,deletedParas);	//ֻ�׳����һ�δ������			
			}
			return errMsg;
		}
		/// <summary>
		/// check business pk repeat in the same DataTable
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="tableName"></param>
		/// <param name="busPKName"></param>
		/// <returns></returns>
		private string CheckBUSPKRepeatInDataTable(DataTable dt,string tableName,string busPKName)
		{
			dt.AcceptChanges();
			string[] busPKNames;
			bool isSame=true;
			if (busPKName==""){return "";}
			busPKNames=busPKName.Split(",".ToCharArray());
			for(int i=0;i<dt.Rows.Count;i++)
			{
				for(int j=i+1;j<dt.Rows.Count;j++)
				{
					isSame=true;
					for(int k=0;k<busPKNames.Length;k++)
					{
						if (dt.Rows[i][tableName+"."+busPKNames[k]]==DBNull.Value || dt.Rows[j][tableName+"."+busPKNames[k]]==DBNull.Value)
						{
							isSame=false;
							break;
						}
						if (dt.Rows[i][tableName+"."+busPKNames[k]].ToString()!=dt.Rows[j][tableName+"."+busPKNames[k]].ToString())
						{
							isSame=false;
							break;
						}
					}
					if (isSame){return BUSDATERR_PK;}						
				}
			}
			return "";

		}
		
		/// <summary>
		/// Generate sql sentence
		/// </summary>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		/// ҵ��������֤��sql
		private  string GenerateSQLToCheckBUSPK(string tableName,DataRow dr,string busPKName,string deletedParas)
		{
			string sql="";
			string errMsg="";	
			if (busPKName==""){return "";}
			sql=ConstructDMLSql.ConstructCheckBusPKSql(tableName,dr,busPKName);
			sql += deletedParas ;
			if (sql=="") return BUSDATERR_SQL;
			try
			{
				DataTable dt=_da.GetDataTable(sql);
				if ((int)dt.Rows[0][0]==0)			
					errMsg="";			
				else
				{
					errMsg=BUSDATERR_PK;				
				}	
			}
			catch(SqlException e)
			{
				errMsg=BUSDATERR_SQL;
			}
			return errMsg;
		}
		#endregion
	}
}
