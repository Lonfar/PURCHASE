using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEDocumentModel 的摘要说明。
	/// </summary>
	public class DAEDocumentModel : DAEBase
	{
		public DAEDocumentModel()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public void GetDownloadPath ( string sDocumentModelID ,ref string sAttachName,ref string sFileName,ref string sAttachAddr)
		{
			//string sDownloadPath = string.Empty ;

			string sError = AddDownLoadCount ( sDocumentModelID ) ;

			if ( sError.Length == 0 )
			{
				string strSql = @"
				SELECT
					IDKey , AttachAddr , AttachName
				FROM
					Attachments
				WHERE
					ObjectiveID = '" + sDocumentModelID + "'" ;

				DataTable dt = BaseDataAccess.GetDataTable ( strSql ) ;

				if ( dt.Rows.Count > 0 )
				{
					sAttachName = dt.Rows[0]["AttachName"].ToString() ;
					sFileName = dt.Rows[0]["IDKey"].ToString() ;
					sAttachAddr = dt.Rows[0]["AttachAddr"].ToString() ;
					//sDownloadPath = "/TopisWeb1/Public/DownloadAttachment.aspx?ModuleID=Topis.DocumentManagement.DocumentModel&AttName=" + dt.Rows[0]["AttachName"].ToString() + "&FileName=" + dt.Rows[0]["IDKey"].ToString() + "&AttAddr=" + dt.Rows[0]["AttachAddr"].ToString() ;
				}
			}

			//return sDownloadPath ;
		}


		public string AddDownLoadCount ( string sDocumentModelID )
		{
			string strSql = @"
				UPDATE
					DocumentModel
				SET
					DownLoadCount = DownLoadCount + 1
				WHERE
					IDkey = '" + sDocumentModelID + "'" ;

			return BaseDataAccess.ExecuteDMLSQL ( strSql ) ;
		}
	}
}
