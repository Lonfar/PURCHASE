using System;
using System.Data;
using System.Configuration;
using System.Web.Mail;	

namespace DataEntity
{
	/// <summary>
	/// TC日程的数据实体类
	/// </summary>
	public class DAETCSchedule : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETCSchedule()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		//循环的给审批通过的供应商发送邮件

		// Add by ZZH on 2008-1-15 添加删除记录时PutIn表中的记录状态为-1
		public String UpdateRecordStateOnPutIn(String pkValue)
		{
		
				String strSql = " Select PutINIDKey From TCMeetingReport Where ID='" + pkValue + "'";
				DataTable dt = _da.GetDataTable(strSql);
				if( dt != null && dt.Rows.Count > 0 )
				{
					foreach( DataRow row in dt.Rows )
					{
						UpDatePutInState ( -1 , Convert.ToString( row["PutINIDKey"] ) );
					}
				}
				return "" ;
	
		}
		//**************************************************************

		#region 获得最大的OrderID,然后新增的时候加1

		public void  SendMailToVendor(string pIDkey)
		{

//			Common.MailSender ms = new Common.MailSender();
//			ms.Server = "smtp.163.com";
//			ms.Subject = ConfigurationSettings.AppSettings["VendorMailSubject"];
//			ms.Body = ConfigurationSettings.AppSettings["VendorMailBody"];
//			ms.UserName = "cnodcatobe";
//			ms.From = "cnodcatobe";
//			ms.Password = "cnwit123456";
//			ms.To="wxc103@sohu.com";
//			ms.SendMail();






//			MailMessage MyMessage=new MailMessage();
//			string sVendorMailSubject = ConfigurationSettings.AppSettings["VendorMailSubject"];
//			string sVendorMailBody = ConfigurationSettings.AppSettings["VendorMailBody"];
//			MyMessage.Priority=MailPriority.High;
//			MyMessage.Subject=sVendorMailSubject;
//
//			SmtpMail.SmtpServer = "pop3.sohu.com";
//
//			MyMessage.From="cnodcatobe@sohu.com";
//
//
//                SmtpMail.SmtpServer = "smtp.gmail.com";
//
//
//			MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
//			//说明发送邮件需要用户验证，否则设置为0
//			MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername","cnodcatobe@sohu.com"); 
//			//邮箱的用户名
//			MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword","CNWIT123456");
//			//邮箱的密码			
//            MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "smtp服务器端口号");
//            MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "1");
//		
//			MyMessage.BodyFormat=MailFormat.Text;
//			String s=sVendorMailBody;
//			MyMessage.Body=s.ToString();
//
//			string strSql = "SELECT Vendor.IDKey,Email,VendorName FROM TCMeetingReport,PutIn,Vendor "+
//                            " where TCMeetingReport.PutINIDKey = PutIn.IDKey AND TCMeetingReport.IsPass = 1 "+
//							" AND PutIn.ObjectiveType = 4 AND TCMeetingReport.[ID] ='"+pIDkey+"' AND Vendor.IDKey = PutIn.ObjectiveID";
//			DataTable dtVendorMail =  _da.GetDataTable(strSql);
//			if(dtVendorMail.Rows.Count>0)
//			{
//				for(int  i = 0 ;i<dtVendorMail.Rows.Count;i++)
//				{
//					//if(dtVendorMail.Rows[i]["Email"]!=System.DBNull.Value)
//					//{
//						MyMessage.To="wxc103@sohu.com";//;/dtVendorMail.Rows[i]["Email"].ToString();
//						SmtpMail.Send(MyMessage);
//					//}
//				}
//			}

		}



		/// <summary>
		/// 获得最大的OrderID,然后新增的时候加1
		/// </summary>
		/// <returns></returns>
		public int GetMaxOrderID ()
		{
			int orderID = 0;

			string SelectSql = "SELECT dbo.f_GetMaxOrderID() AS ORDERID ";

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader(SelectSql))
			{
				while ( dr.Read() )
				{
					orderID = Convert.ToInt32( dr["ORDERID"] );
				}
			}

			return orderID;
		}

		#endregion

		#region 更新提交表中记录的状态

		/// <summary>
		/// 更新提交表中记录的状态
		/// </summary>
		/// <param name="iState">目标状态</param>
		/// <param name="strPutInIDKey">需要更新的记录ID</param>
		/// <returns></returns>
		public string UpDatePutInState ( int iState , string strPutInIDKey )
		{
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE PutIn Set state = "+iState+" WHERE IDKey = '"+strPutInIDKey+"'";

			strErrorMsg = _da.ExecuteDMLSQL ( UpdateSql );

			return strErrorMsg;
		}

		#endregion

		#region 通过提交表的IDKey来获得提交人名称

		/// <summary>
		/// 通过提交表的IDKey来获得提交人名称
		/// </summary>
		/// <param name="strPutIDKey">提交表的IDKey</param>
		/// <returns>提交人名称</returns>
		public string GetPutInPeople ( string strPutIDKey )
		{
			string strApproveBy = string.Empty;

			string SelectSql = @"Select BI_Employee.FullName , PutIn.PutInPeople From BI_DepartmentEmployee , PutIn , BI_Employee
										Where BI_Employee.IDkey = PutIn.PutInPeople AND PutIn.IDKey = '"+strPutIDKey+"'" ;

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					strApproveBy = Convert.ToString( dr["FullName"] ) + "," + Convert.ToString( dr["PutInPeople"] );
				}
			}

			return strApproveBy;
		}

		#endregion

		#region 删除TCMeetingMember记录 Added by QSQ 11.9
		public void DelViewer(string strIDKey)
		{
			string strSql = "delete TCMeetingMember where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		/// <summary>
		/// 删除多条记录
		/// </summary>
		/// <param name="strID">外键</param>
		public void DelMultiViewer(string strID)
		{
			string strSql = "delete TCMeetingMember where ID ='"+strID+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region 通过外键找到子表主键
		/// <summary>
		/// 通过外键(主表的主键)来获得子表的主键*****解决了VoucherEdit控件不能作为子控件的Bug
		/// <param name="ParentKey"></param>
		/// <returns></returns>
		public string GetIDKey ( string ParentKeyFieldName , string ParentValue , string ChildTableName ,string ChildPKFieldName )
		{
			string IDKey = string.Empty;

			string SelectSql = " SELECT "+ ChildPKFieldName +" FROM "+ChildTableName+" WHERE "+ParentKeyFieldName +" = '"+ ParentValue+"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				IDKey = Convert.ToString ( dataTable.Rows[0][0] );
			}

			return IDKey ;
		}
		#endregion

		#region 得到所有招标委员会成员的信息

		/// <summary>
		/// 得到所有招标委员会成员的信息
		/// Added by QSQ 11.8
		/// </summary>
		/// <returns></returns>
		public DataTable GetTCGroup( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName],
							BI_PositionType.PositionName as [BI_PositionType.PositionName],
							TCMeetingMember.UserID as [TCMeetingMember.UserID],
							'Edit' RowStatus,
							TCMeetingMember.IDKey as [TCMeetingMember.IDKey],
							TCMeetingMember.ID as [TCMeetingMember.ID],
							BI_DepartmentEmployee.DepartmentID as [TCMeetingMember.DepartmentID]
						from TCMeetingMember inner join BI_Employee on TCMeetingMember.UserID = BI_Employee.IDKey
							left outer join BI_DepartmentEmployee on BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
							left outer join BI_PositionType on BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						where TCMeetingMember.ID ='"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}
		#endregion

		#region 得到BI_Employee.IDKey Added by QSQ 11.9
		/// <summary>
		/// 根据BI_DepartmentEmployee.IDKey 得到BI_Employee.IDKey
		/// </summary>
		/// <returns></returns>
		public string GetBI_EmployeeID( string pkValue )
		{
			string strSql = "";
			strSql = @"select	BI_Employee.IDKey
							  
						from 
							BI_Employee,
							BI_DepartmentEmployee
							
						where 
							BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and BI_DepartmentEmployee.IDKey = '"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			if(dt.Rows.Count > 0)
			{
				return dt.Rows[0]["IDKey"].ToString();
			}
			else return "";
		}
		#endregion

		#region 得到单个用户信息 Added by QSQ 11.9
		/// <summary>
		/// 得到单个用户信息
		/// </summary>
		/// <param name="UserID">用户ID</param>
		/// <returns></returns>
		public DataTable GetTCGroup_User( string UserID )
		{
			string strSql = "";
			strSql = @"SELECT BI_Employee.FullName,                 
							  BI_PositionType.PositionName,
							  BI_Employee.IDKey,
							  BI_DepartmentEmployee.DepartmentID

						FROM  BI_Employee , 
							  BI_DepartmentEmployee,
							  BI_PositionType

						where BI_DepartmentEmployee.IDKey='"+UserID+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		#endregion

		#region 得到指定TC小组的人员信息 Added by QSQ 11.9
		/// <summary>
		/// 得到指定组的人员信息
		/// </summary>
		/// <param name="UserID">GroupID</param>
		/// <returns></returns>
		public DataTable GetTCGroup_Group( string GroupID )
		{
			string strSql = "";
			strSql = @"select BI_Employee.FullName,
							BI_PositionType.PositionName,
								BI_Employee.IDKey,
							BI_DepartmentEmployee.DepartmentID
						from (((BI_Employee left outer join BI_DepartmentEmployee on BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID)
							inner join TI_DefaultTCGroupMember on TI_DefaultTCGroupMember.TCUserID = BI_Employee.IDKey)
							left join BI_PositionType on BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey )
							where TI_DefaultTCGroupMember.IDKEY ='"+GroupID+"'";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		#endregion

	}
}
