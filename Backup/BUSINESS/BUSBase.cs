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
	/// 业务类的基类
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
			// TODO: 在此处添加构造函数逻辑
			//
			
		}
		#region IBusiness 成员

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

		#region IBusinessSaveData 成员
		/// <summary>
		///check business logic and business calculation and check data 
		/// </summary>
		/// <returns></returns>
		public string CheckBusinessDataAndLogic(DataTable dt,ArrayList fieldsList)
		{
			string errMsg1="",errMsg2="",errMsg3="",errMsg="";
			
			/*进行业务数据，业务逻辑校验,运算
			 * 如果业务数据已经在页面层校验通过了，可以在此只进行业务规则校验和业务逻辑运算 */
			errMsg1=CheckBusinessLogic_rule(dt,fieldsList);	
			if (errMsg1!=""){errMsg=errMsg1;}
			errMsg2=CheckBusinessLogic_calculate(dt,fieldsList);	
			if (errMsg2!=""){errMsg=errMsg2;}
			errMsg3=CheckBusinessData(dt,fieldsList);	
			if (errMsg3!=""){errMsg=errMsg3;}
			//return errMsg1+ errMsg2+ errMsg3;		//只返回最后一次错误号	
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

		#region IBusinessDeleteData 成员
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
		#region IBusinessQueryData 成员
		public string BusinessQueryData ( string sql)
		{
			return "";
		}
		#endregion
		#region 定义下面可能用到虚方法，在子类中使用哪种虚方法，重载哪种，否则可以忽略该方法
		/*根据不同业务需要在此自定义私有方法进行业务、数据校验或者逻辑运算等*/
		/*如果数据校验已在业务层完成，不需在进行校验，则可以忽略这些操作*/
		
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
				errMsg=GenerateSQLToCheckBUSPK(tableName,dr,busPKName,deletedParas);	//只抛出最后一次错误号码			
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
		/// 业务主键验证，sql
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
