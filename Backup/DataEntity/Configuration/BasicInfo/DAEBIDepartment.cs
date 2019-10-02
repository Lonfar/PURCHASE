using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEBIDepartment ��ժҪ˵����
	/// </summary>
	public class DAEBIDepartment:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEBIDepartment()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
		//�Ƿ����ϵͳ����
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
		//�Ƿ���ϵͳ����
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
		//ͬһ�����Ƿ����ͬ������
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
		///  ͬһ�����Ƿ����ͬ������ Added By Liujun
		/// </summary>
		/// <param name="ParentIDKey">��������ID</param>
		/// <param name="IDKey">������ID</param>
		/// <param name="departmentName">��������</param>
		/// <returns>true:����,false:������</returns>
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
