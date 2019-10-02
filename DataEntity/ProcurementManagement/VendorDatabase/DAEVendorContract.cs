using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEVendorContract 的摘要说明。
	/// Added by QSQ 11.20
	/// </summary>
	public class DAEVendorContract:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEVendorContract()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	

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
		/// 得到所有查看人信息
		/// </summary>
		/// <returns></returns>
		public DataTable GetTable( string whereSql)
		{
			string Sql = string.Empty;

			Sql =@" Select 	Vendor.VendorName as VendorName,
						Contract.IDkey,
						Contract.ContractID as ContractID,
						Contract.Objective,
						BI_Employee.FullName as Executant,
						( str(Contract.ContractAmount2)+' ' + Contract.ContractCurrency2 ) as ContractAmount2,
						CONVERT(varchar(10),Contract.ExecuteTerm,120) as ExecuteTerm,
						case when Contract.IsAddProtocol =1 then 'true'
							when Contract.IsAddProtocol <>1 then 'false' end as IsAddProtocol,
						Contract.VendorID as VendorID,
						Contract.SubscribeDate as SubscribeDate
					from Contract inner join Vendor on Contract.VendorID = Vendor.IDKey
						left join BI_Employee on BI_Employee.IDKey = Contract.Executant
					where 1=1 ";

			Sql += whereSql;
			Sql += " order by Contract.vTP DESC";

			DataTable dt = _da.GetDataTable(Sql);

			return dt;
		}



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

	}
}
