using System;

namespace DataEntity
{
	/// <summary>
	/// DAEDocumentModelCategory 的摘要说明。
	/// </summary>
	public class DAEDocumentModelCategory : DAEBase
	{
		public DAEDocumentModelCategory()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public bool HashChildren(string strConID)
		{
			string sql="select sum(a) as RelateRow from(select count(*) a from  BT_DocumentModel where ParentID = '" + strConID + "'union all select count(*) a from DocumentModel where TypeID= '" + strConID + "')tb";
			System.Data.DataTable dt = BaseDataAccess.GetDataTable(sql);
			if ((int)dt.Rows[0][0]>0)
				return true;
			else
				return false;
		}
	}
}
