using System;
using System.Data;
using Cnwit.Utility;
using Common;
using System.Collections;

namespace DataEntity
{
	/// <summary>
	/// ���������ʵ����
	/// </summary>
	public class DAEDesktop : DAEBase
	{
		DataAcess _da ; 

		public DAEDesktop()
		{
			_da = GetProjectDataAcess.GetDataAcess();
		}

		/// <summary>
		/// ���ݵ�ǰ��½�������
		/// 1.�½���δ�ύ��				State_NEW
		/// 2.��Ҫ��ǰ��½����������	State_SRMisDepApproved 
		/// 3.��Ҫ��ǰ��½�����Ǽǵ� State_SRRegister
		/// 4.��Ҫ��ǰ��½�����а�� State_SRDispatch
		/// SR
		/// </summary>
		/// <param name="strEmployeeID">Ա��ID</param>
		/// <param name="HasRecordAuthority">�Ƿ��еǼ�Ȩ��</param>
		/// <returns></returns>
		public DataTable GetSRInfo ( string strEmployeeID , bool HasRecordAuthority )
		{
			DataTable dtSRInfo = new DataTable();
			
			string SelectSql = @"SELECT IDKey,ISNULL(SRID,'               ') AS SRID,SRName,AppDate FROM ServiceRequistion WHERE 
									( CreateBy = '"+strEmployeeID+@"' AND SRState = '"+((int)TenderState.State_NEW).ToString()+@"' ) 
									OR ( (SELECT COUNT(1) FROM BI_DepartmentEmployee a,BI_Department b WHERE a.DepartmentID = b.idkey AND  b.IsMisDepartment=1  AND  a.Principal =1 AND a.EmployeeID ='"+strEmployeeID+@"') > 0 AND SRState = '"+((int)TenderState.State_Approved).ToString()+@"' )  
									OR ( ProUndertaker = '"+strEmployeeID+@"' AND SRState = '"+((int)TenderState.State_Register).ToString()+@"' )";
			
			// �鿴�Ƿ��еǼ�Ȩ��
			if ( HasRecordAuthority )
			{
				SelectSql += " OR ( SRState = '"+((int)TenderState.State_MisDepApproved).ToString()+"' )";
			}

			SelectSql += " ORDER BY AppDate DESC";

			dtSRInfo = _da.GetDataTable ( SelectSql );

			return dtSRInfo;
		}

		public DataTable GetMRInfo ( string strEmployeeID , bool HasRecordAuthority )
		{			
			string SelectSql = @"SELECT MRID,ISNULL(MRNO,'               ') AS MRNO,MRDescription,RequestDate FROM MR_MaterialRequisition WHERE 
									( CreateBy = '"+strEmployeeID+@"' AND Status = '"+((int)MRState.State_New).ToString()+@"' ) 
									OR ( (SELECT COUNT(1) FROM BI_DepartmentEmployee a,BI_Department b WHERE a.DepartmentID = b.idkey AND  b.IsMisDepartment=1  AND  a.Principal =1 AND a.EmployeeID ='"+strEmployeeID+@"') > 0 AND Status = '"+((int)MRState.State_MRApproved).ToString()+@"' )  
									OR ( ReceiveBy = '"+strEmployeeID+@"' AND Status = '"+((int)MRState.State_MRRegister).ToString()+@"' )";
			
			// �鿴�Ƿ��еǼ�Ȩ��
			if ( HasRecordAuthority )
			{
				SelectSql += " OR ( Status = '"+((int)MRState.State_MRReply).ToString()+"' )";
			}

			SelectSql += " ORDER BY RequestDate DESC";

			DataTable dtMRInfo = _da.GetDataTable ( SelectSql );

			return dtMRInfo;
		}

		/// <summary>
		/// ������TC�ճ̵���Ϣ
		/// </summary>
		/// <param name="strEmployeeID"></param>
		/// <returns></returns>
		public DataTable GetTCInfo ( string strEmployeeID )
		{
			DataTable dtTCInfo = new DataTable();
			string SelectSql = @"SELECT TCMeeting.Title,TCMeeting.MeetingDate,RoomDescription FROM TCMeeting 
										INNER JOIN TCMeetingMember ON TCMeeting.ID = TCMeetingMember.ID 
										INNER JOIN TCMeetingRoom ON TCMeetingRoom.RoomIDKey = TCMeeting.MeetingAddr
										WHERE TCMeeting.State = 0 AND TCMeetingMember.UserID = '"+strEmployeeID+"' ORDER BY TCMeeting.MeetingDate DESC";

			dtTCInfo = _da.GetDataTable ( SelectSql );

			return dtTCInfo;
		}

		/// <summary>
		/// �����ص���������Ϣ
		/// </summary>
		/// <param name="strEmployeeID"></param>
		/// <returns></returns>
		public DataTable GetApproveInfo ( string strEmployeeID )
		{
			DataTable dtApproveInfo = new DataTable();
            string SelectSql = @"SELECT PutIn.IDKey,ObjectiveID,ObjectiveTitle,ObjectiveType,TypeDescription AS State , BT_PutInState.IDKey As StateIDKey ,TI_ApproveType.ApprovalTypeName,PutIn.ApprovedBy FROM PutIn 
								INNER JOIN BT_PutInState ON PutIn.State=BT_PutInState.IDKey 
								INNER JOIN TI_ApproveType ON PutIn.ObjectiveType = TI_ApproveType.IDKey WHERE ";

			DAEApproveProcessing daeAP = new DAEApproveProcessing();
			string strPutInString = daeAP.GetPutInIDKey ( strEmployeeID );
			
			if ( strPutInString.Length > 0 )
			{
				SelectSql += "PutIn.IDKey In ("+strPutInString+") ORDER BY PutIn.ApprovedBy DESC";
			}
			else
			{
				SelectSql += "1=2 ORDER BY PutIn.ApprovedBy DESC";
			}

			dtApproveInfo = _da.GetDataTable ( SelectSql );
			
			return dtApproveInfo;
		}
	}
}
