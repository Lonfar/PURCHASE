using System;
using Common;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Cnwit.Utility;

namespace DataEntity
{
	/// <summary>
	/// 实体类的基类
	/// </summary>
	public class DAEBase : IDataEntity
	{
		#region 私有成员

		private DataTable _dt;
		private ArrayList _list;
		private string _pkFieldName=string.Empty;
		private string _tableName=string.Empty;
		private string _busPKFieldName=string.Empty;
		private const string ERR_DAE_SAVE="ERRDAE01";
		private const string ERR_DAE_DELETE="ERRDAE02";		
		private const string ERR_DAE_SAVE_PKCHECK="ERRDAE03";
		private DataAcess _da = GetProjectDataAcess.GetDataAcess();

		#endregion

		#region 公有属性

		/// <summary>
		/// 通过基类提供数据连接 Liujun Add at 2007-6-21
		/// </summary>
		public DataAcess BaseDataAccess
		{
			get { return this._da; }

		}
		#endregion

		#region 构在函数

		public DAEBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#endregion

		#region IDataEntity 成员
		
		public System.Data.DataTable CurDataTable
		{
			set
			{
				_dt=value;
			}
			get {return _dt;}
		}

		public System.Collections.ArrayList FieldList
		{
			set
			{
				_list=value;
			}
			get{return _list;}
		}
		public string PKFieldName
		{
			set {_pkFieldName=value;}
			get{return _pkFieldName;}
		}
		public string TableName
		{
			set{_tableName=value;}
			get{return _tableName;}
		}
		public string BusPKFieldName
		{
			set{_busPKFieldName=value;}	
			get{return _busPKFieldName;}		
		}
		public virtual string Delete(ArrayList alDeleteData)
		{
			string sqlDelete="SET   XACT_ABORT   ON begin TRANSACTION  Deletetran ";
			string errMsg="";
			for(int i =0;i<alDeleteData.Count;i++)
			{
				string _tbname = ((IDeleteEventDate)alDeleteData[i]).TableName;
				string _pkFieldName = ((IDeleteEventDate)alDeleteData[i]).PkName;
				string pkValue = ((IDeleteEventDate)alDeleteData[i]).PkValue;
				sqlDelete= sqlDelete + ConstructDMLSql.ConstructDeleteSql(_tbname,_pkFieldName,pkValue) +";";
				sqlDelete +="   IF @@ERROR<>0 begin  ROLLBACK TRANSACTION  Deletetran return end  ";
			}
			sqlDelete +=" else COMMIT TRANSACTION Deletetran ";
			if (sqlDelete.Trim().Length==0){return ERR_DAE_DELETE;}
			try
			{
				errMsg=_da.ExecuteDMLSQL(sqlDelete);
			}
			catch(System.Data.SqlClient.SqlException e)
			{
				return GetDataBaseErrorsMessage(e);
			}
			return errMsg;
		}		
		public virtual string Save()
		{					
			string errMsg="";
			string[] tables=GetStringsByStringName(_tableName);
			string[] pkNames=GetStringsByStringName(_pkFieldName);
			string[] busPKNames=GetStringsByStringName(_busPKFieldName);
			foreach(DataRow dr in _dt.Rows)
			{
				for(int i=0;i<tables.Length;i++)
				{
					errMsg+=SaveRowsData(tables[i],dr,tables[i]+"."+pkNames[i],tables[i]+"."+busPKNames[i]);
				}
			}

//			foreach(DataRow dr in _dt.Rows)
//			{
//				if(dr.RowState == DataRowState.Deleted)
//				{
//					for(int i=0;i<tables.Length;i++)
//					{
//						errMsg+=SaveRowsData(tables[i],dr,tables[i]+"."+pkNames[i],tables[i]+"."+busPKNames[i]);
//					}
//				}
//			}
//			foreach(DataRow dr in _dt.Rows)
//			{
//				if(dr.RowState != DataRowState.Deleted)
//				{
//					for(int i=0;i<tables.Length;i++)
//					{
//						errMsg+=SaveRowsData(tables[i],dr,tables[i]+"."+pkNames[i],tables[i]+"."+busPKNames[i]);
//					}
//				}
//			}

			return errMsg;
		}		
		private string SaveRowsData(string tableName,DataRow dr,string pkName,string busPKName)
		{
			string sql="";	
			string errMsg="";
			string pkValue="";	
			if(dr.RowState == DataRowState.Deleted)
			{
				dr.RejectChanges();
				sql=ConstructDMLSql.ConstructDeleteSql(tableName,pkName,dr[pkName].ToString()) ;
			}
			else
			{
				if (dr["RowStatus"].ToString().ToUpper()=="NEW")	
				{
					sql=ConstructDMLSql.ConstructInsertSql(tableName,dr);
				}
				else if (dr["RowStatus"].ToString().ToUpper()=="EDIT")
				{
					if (CheckTimeStampExistForUpdate("",""))
						sql=ConstructDMLSql.ConstructUpdateSql(tableName,dr,pkName,pkValue);				
				}
			}
			try
			{
				errMsg+=_da.ExecuteDMLSQL(sql);
				dr.AcceptChanges();
				if (dr["RowStatus"].ToString().ToUpper()=="NEW"&&errMsg.Trim().Length==0){dr["RowStatus"]="EDIT";}
			}
			catch(SqlException e)
			{
				errMsg+=GetDataBaseErrorsMessage(e);				
			}
			return errMsg;
		}
		private string[] GetStringsByStringName(string source)
		{
			return source.Split(",".ToCharArray());
		}
		public virtual string Query(string sql)
		{
			string errMsg="";
			try
			{
				_dt=_da.GetDataTable(sql);
			}
			catch(SqlException e)
			{
				errMsg=GetDataBaseErrorsMessage(e);
			}
			return errMsg;
		}


		#endregion
		
		#region 保存方式验证(多用户操作）
		
		/// <summary>
		/// if business pk exists then return the  physical pk of this table,else return "";
		/// </summary>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		/// 业务主键验证，以确认新增记录应该是insert还是update
		private  string CheckBUSPKExistForInsert(string tableName,DataRow dr,string busPKName,string pkFieldName,out string pkValue)
		{
			string sql="";
			string errMsg="";
			pkValue="";
			sql=ConstructDMLSql.ConstructCheckBusPKSql(tableName,dr,busPKName,pkFieldName);
			if (sql=="") return ERR_DAE_SAVE_PKCHECK;
			try
			{
				DataTable dt=_da.GetDataTable(sql);
				if (dt.Rows.Count==0)			
					pkValue="";			
				else
				{
					pkValue=dt.Rows[0][0].ToString();				
				}	
			}
			catch(SqlException e)
			{
				errMsg=GetDataBaseErrorsMessage(e);
			}
			return errMsg;
		}
		/// <summary>
		/// record status is Update in DataTable
		/// </summary>
		/// <param name="pkValue"></param>
		/// <param name="timeStamp"></param>
		/// <returns>if timeStamp params<>timeStamp in DataBase--return false,otherwise return true </returns>
		private  bool CheckTimeStampExistForUpdate(string pkValue,string timeStamp)
		{
			return true;
		}
		public object GetPKValueFromDataRow(DataRow dr,string pkFieldName)
		{
			return dr[pkFieldName];
		}
		#endregion

		#region errors message
		private string GetDataBaseErrorsMessage(SqlException e)
		{
			return (e.Number.ToString());
		}
		#endregion

		#region 分页存储过程

		// Liujun Add at 2007-6-19 将分页存储过程提取到基类，供查询时使用

		/// <summary>
		/// 计算总页数
		/// </summary>
		/// <param name = "strFromStatement"></param>
		/// <param name = "strWhereStatement"></param>
		/// <returns></returns>
		public int SelectListPagedTotalCount( string strFromStatement,string strWhereStatement)
		{
				try
			{
				/// 传递存储过程参数
				string[] sParameter = new string[]{"SelectStatement","FromStatement","WhereStatement","OrderByExpression","AscOrDesc","RecordCount","PageIndex","PageSize","DoCount"};
			
				/// 传递存储过程参数值
				Object[] sParameterValue = new object[]{DBNull.Value,strFromStatement,strWhereStatement,DBNull.Value,DBNull.Value,DBNull.Value,DBNull.Value,DBNull.Value,true};
			
				SqlDbType[] sqlDbTypeValue = new SqlDbType[]{SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int,SqlDbType.Bit};
			
				DataSet DS = _da.ExecuteSPQuery("spSelectListDynamicPaged2",sParameter,sParameterValue,sqlDbTypeValue);
			
				//return the total count
				return Convert.ToInt32(DS.Tables[0].Rows[0][0].ToString());
			}
			catch(System.Exception err)
			{
				throw(err);
			}
		}

		/// <summary>
		/// 获得数据表
		/// </summary>
		/// <param name="strSelectStatement"></param>
		/// <param name="strFromStatement"></param>
		/// <param name="strWhereStatement"></param>
		/// <param name="strOrderByExpression"></param>
		/// <param name="ascOrDesc"></param>
		/// <param name="intRecordCount"></param>
		/// <param name="intPageIndex"></param>
		/// <param name="intPageSize"></param>
		/// <returns></returns>
		public DataTable SelectListPaged( string strSelectStatement,string strFromStatement,string strWhereStatement,string strOrderByExpression,string ascOrDesc,int intRecordCount,int intPageIndex,int intPageSize)
		{
			try
			{
				string[] sParameter = new string[]{"SelectStatement","FromStatement","WhereStatement","OrderByExpression","AscOrDesc","RecordCount","PageIndex","PageSize","DoCount"};
			
				Object[] sParameterValue = new object[]{strSelectStatement,strFromStatement,strWhereStatement,strOrderByExpression,ascOrDesc,intRecordCount,intPageIndex,intPageSize,false};
			
				SqlDbType[] sqlDbTypeValue = new SqlDbType[]{SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int,SqlDbType.Bit};
			
				return _da.ExecuteSPQueryDataTable("spSelectListDynamicPaged2",sParameter,sParameterValue,sqlDbTypeValue);

			}
			catch(System.Exception err)
			{
				throw(err);
			}	
	
		}

		#endregion

	}
}

