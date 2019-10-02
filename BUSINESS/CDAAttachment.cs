/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Date Generated: 2005.7.19
-- Version List
--  Version 1.0 2005.7.19
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Configuration;
using System.Data;
using System.IO;
using Cnwit.Utility ;

namespace Business
{
	/// <summary>
	/// Use to manage database about Attachment
	/// </summary>
	public class CDAAttachment 
	{
	    string strSql=string.Empty;
		DataSet dsAttachment=new DataSet();
		DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 
		DataTable dtAttachment=new DataTable();

		public CDAAttachment()
		{
			
		}
		#region SelectAttachmentList Methods

		public DataTable SelectAttachmentList(string moduleID, string infoID)		
		{
			strSql="Select IDKey,ObjectiveType,ObjectiveID,AttachAddr,AttachName,UploadTime,AttachSize,DateOfExpire  FROM Attachments WHERE ObjectiveType='"+moduleID+"' AND ObjectiveID='"+infoID+"' ";
			dtAttachment=pDataAccess.GetDataTable(strSql);
			return dtAttachment;
	
		}
		#endregion


		#region SelectAttachmentListSchema Methods

		public DataTable SelectAttachmentListSchema()
		{
			strSql="Select IDKey,ObjectiveType,ObjectiveID,AttachAddr,AttachName,UploadTime,AttachSize,DateOfExpire FROM Attachments where 1>2";
			dtAttachment=pDataAccess.GetDataTable(strSql);
			return dtAttachment;
		}
		#endregion

		#region  InsertAttachment Methods

		public string InsertAttachment( string IDKey, string moduleID, string infoID, string ObjectiveType, string ObjectiveID, string  AttachAddr, string AttachName,string UploadTime,string AttachSize,string DateOfExpire)
		{
			strSql="Select  dbo.f_NextAttachmentOrderID('"+moduleID.Trim()+"','"+infoID.Trim()+"') as AttachmentOrderID";
			int iOrderID=int.Parse(pDataAccess.GetDataTable(strSql).Rows[0][0].ToString());

			System.DateTime loadTime=Convert.ToDateTime(UploadTime);
			string sfieldValue =string.Empty;
			//modify by wxc at 200/12/28  
			sfieldValue = loadTime.ToString("yyyy-MM-dd HH:mm:ss");
			if(DateOfExpire.Trim()=="")
			{
					strSql="INSERT INTO Attachments(IDKey,ObjectiveType,ObjectiveID,AttachAddr,AttachName,OrderID,UploadTime,AttachSize) Values('"+IDKey+"','"+ObjectiveType+"','"+infoID+"','"+AttachAddr+"','"+AttachName+"',"+iOrderID+",'"+sfieldValue+"',"+double.Parse(AttachSize.Trim())+")";
			}
			else
			{
				System.DateTime strDateOfExpire=Convert.ToDateTime(DateOfExpire);	
				strSql="INSERT INTO Attachments(IDKey,ObjectiveType,ObjectiveID,AttachAddr,AttachName,OrderID,UploadTime,AttachSize,DateOfExpire) Values('"+IDKey+"','"+ObjectiveType+"','"+infoID+"','"+AttachAddr+"','"+AttachName+"',"+iOrderID+",'"+sfieldValue+"',"+double.Parse(AttachSize.Trim())+",'"+strDateOfExpire+"')";
			}
			
			return pDataAccess.ExecuteDMLSQL(strSql);
	
		}
		#endregion

		#region  DeleteAttachment Methods

		public string DeleteAttachment(string attachmentID)
		{
			strSql="DELETE FROM Attachments WHERE IDKey='"+attachmentID.Trim()+"'";
			return pDataAccess.ExecuteDMLSQL(strSql);	
		}
		#endregion

		#region  DeleteAttachmentsByModuleIDAndInfoID Methods
		public string DeleteAttachmentsByModuleIDAndInfoID( string moduleID, string infoID)
		{
			strSql="DELETE FROM Attachments WHERE ObjectiveID='"+infoID.Trim()+"' and ObjectiveType='"+moduleID+"'";
			return pDataAccess.ExecuteDMLSQL(strSql);
		}
		#endregion


		/// <summary>
		/// update the data in datatable to database
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="infoID"></param>
		/// <param name="tblAttachment"></param>
		/// <returns></returns>
		public bool UpdateAttachment(string moduleID,string infoID,DataTable tblAttachment)
		{
			bool datasuc=true;
			foreach(DataRow drdata in tblAttachment.Rows)
			{
				//add only added items
				if(drdata.RowState==DataRowState.Deleted )
				{
					drdata.RejectChanges ();
					datasuc=datasuc & (this.DeleteAttachment((string)drdata["IDKey"])=="");
					string pathConfig = ConfigurationSettings.AppSettings["SavefilePath"];
					string path =Path.Combine(pathConfig,drdata["AttachAddr"].ToString ());
					try
					{
						string oldFileName=drdata["AttachName"].ToString (); 
						int nLastO = oldFileName.LastIndexOf(".");
						string extension= oldFileName.Substring(nLastO);
						string fileName=drdata["IDKey"].ToString ()+extension;
						File.Delete (Path.Combine(path,fileName));
					}
					catch
					{
						//删除文件失败，不作处理
					}

				}
				else if (drdata.RowState==DataRowState.Added )
				{

					datasuc=datasuc& (this.InsertAttachment((string)drdata["IDKey"],
						moduleID,infoID,drdata["ObjectiveType"].ToString (),drdata["ObjectiveID"].ToString(),drdata["AttachAddr"].ToString(),
						(string)drdata["AttachName"],drdata["UploadTime"].ToString(),drdata["AttachSize"].ToString(),drdata["DateOfExpire"].ToString()) =="");
				}
			}
			return datasuc; 
		}

		#region  DeleteAttachmentRecordAndFilesByModuleIDAndInfoID Methods

		public void DeleteAttachmentRecordAndFilesByModuleIDAndInfoID(string moduleID, string infoID)
		{
		
			DataTable tbl= SelectAttachmentList(moduleID,infoID);
			foreach(DataRow dr in tbl.Rows)
			{
				this.DeleteAttachment((string)dr["IDKey"]);
				string path =dr["AttachAddr"].ToString (); //Path.Combine(ConfigurationSettings.AppSettings["BusinessDataFilePath"],moduleID+"/"+drdata["IDKey"].ToString ().Substring(0,6));
				try
				{
					File.Delete (Path.Combine(path,dr["AttachName"].ToString ()));
				}
				catch
				{
					//删除文件失败，不作处理
				}
			}
		}
		#endregion
	}
}
