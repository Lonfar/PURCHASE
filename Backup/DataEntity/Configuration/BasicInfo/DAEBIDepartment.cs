using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEBIDepartment 的摘要说明。
	/// </summary>
	public class DAEBIDepartment:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEBIDepartment()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		///  wether one node has children
		/// </summary>
		/// <param name="idKey">Bi_department's pk</param>
		/// <returns></returns>
		public bool HashChildren(string idKey)
		{
			string sql="";
			sql="select count(*) from BI_Department where PDepartmentID  in ('"+idKey+"')";
			DataTable dt=_da.GetDataTable(sql);
			if ((int)dt.Rows[0][0]>0)
				return true;
			else
				return false;
		}
		public string getPOrder(string idKey)
		{
			string sql="";
			sql="select NodeOrder  from BI_Department  where IDKey='"+idKey+"'";
			//sql="select IDKey  from BI_Department  where IDKey='"+idKey+"'";
			DataTable dt=_da.GetDataTable(sql);
			if(dt.Rows[0][0]!=System.DBNull.Value)
			{
				return (string)dt.Rows[0][0];
			}
			else
			{
			   return "0";
			}
		}
		//是否存在系统部门
		public bool isHasSystemDepartment()
		{
			string sql="";
			sql="select sum(Convert(int,IsMisDepartment))  from BI_Department ";
			DataTable dt=_da.GetDataTable(sql);
			if((int)dt.Rows[0][0]==0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		//是否是系统部门
		public bool isSystemDepartment(string idKey)
		{
			string sql="";
			sql="select   Convert(int,IsMisDepartment)  from BI_Department where    IDKey='"+idKey.Trim()+"'";
			DataTable dt=_da.GetDataTable(sql);
			if((int)dt.Rows[0][0]==0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		//同一级中是否存在同名部门
		public bool isHasSameDepartment(string idKey,string departmentName)
		{
			string sql="";
			sql="select  count(*)  from BI_Department where     PDepartmentID='"+idKey.Trim()+"'  and DepartmentName='"+departmentName+"'";
			DataTable dt=_da.GetDataTable(sql);
			if((int)dt.Rows[0][0]==0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		///  同一级中是否存在同名部门 Added By Liujun
		/// </summary>
		/// <param name="ParentIDKey">父级部门ID</param>
		/// <param name="IDKey">本部门ID</param>
		/// <param name="departmentName">部门名称</param>
		/// <returns>true:存在,false:不存在</returns>
		public bool isHasSameDepartment ( string ParentIDKey , string IDKey , string departmentName )
		{
			string sql="";
			sql="select  count(*)  from BI_Department where  PDepartmentID='"+ParentIDKey+"'  and DepartmentName='"+departmentName+"' AND IDKey <> '"+IDKey+"'";
			DataTable dt=_da.GetDataTable(sql);
			if((int)dt.Rows[0][0]==0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
