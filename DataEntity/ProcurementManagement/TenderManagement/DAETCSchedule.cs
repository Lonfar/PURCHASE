using System;
using System.Data;
using System.Configuration;
using System.Web.Mail;	

namespace DataEntity
{
	/// <summary>
	/// TC�ճ̵�����ʵ����
	/// </summary>
	public class DAETCSchedule : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETCSchedule()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		//ѭ���ĸ�����ͨ���Ĺ�Ӧ�̷����ʼ�

		// Add by ZZH on 2008-1-15 ���ɾ����¼ʱPutIn���еļ�¼״̬Ϊ-1
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

		#region �������OrderID,Ȼ��������ʱ���1

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
//			//˵�������ʼ���Ҫ�û���֤����������Ϊ0
//			MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername","cnodcatobe@sohu.com"); 
//			//������û���
//			MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword","CNWIT123456");
//			//���������			
//            MyMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "smtp�������˿ں�");
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
		/// �������OrderID,Ȼ��������ʱ���1
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

		#region �����ύ���м�¼��״̬

		/// <summary>
		/// �����ύ���м�¼��״̬
		/// </summary>
		/// <param name="iState">Ŀ��״̬</param>
		/// <param name="strPutInIDKey">��Ҫ���µļ�¼ID</param>
		/// <returns></returns>
		public string UpDatePutInState ( int iState , string strPutInIDKey )
		{
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE PutIn Set state = "+iState+" WHERE IDKey = '"+strPutInIDKey+"'";

			strErrorMsg = _da.ExecuteDMLSQL ( UpdateSql );

			return strErrorMsg;
		}

		#endregion

		#region ͨ���ύ���IDKey������ύ������

		/// <summary>
		/// ͨ���ύ���IDKey������ύ������
		/// </summary>
		/// <param name="strPutIDKey">�ύ���IDKey</param>
		/// <returns>�ύ������</returns>
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

		#region ɾ��TCMeetingMember��¼ Added by QSQ 11.9
		public void DelViewer(string strIDKey)
		{
			string strSql = "delete TCMeetingMember where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		/// <summary>
		/// ɾ��������¼
		/// </summary>
		/// <param name="strID">���</param>
		public void DelMultiViewer(string strID)
		{
			string strSql = "delete TCMeetingMember where ID ='"+strID+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region ͨ������ҵ��ӱ�����
		/// <summary>
		/// ͨ�����(���������)������ӱ������*****�����VoucherEdit�ؼ�������Ϊ�ӿؼ���Bug
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

		#region �õ������б�ίԱ���Ա����Ϣ

		/// <summary>
		/// �õ������б�ίԱ���Ա����Ϣ
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

		#region �õ�BI_Employee.IDKey Added by QSQ 11.9
		/// <summary>
		/// ����BI_DepartmentEmployee.IDKey �õ�BI_Employee.IDKey
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

		#region �õ������û���Ϣ Added by QSQ 11.9
		/// <summary>
		/// �õ������û���Ϣ
		/// </summary>
		/// <param name="UserID">�û�ID</param>
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

		#region �õ�ָ��TCС�����Ա��Ϣ Added by QSQ 11.9
		/// <summary>
		/// �õ�ָ�������Ա��Ϣ
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
