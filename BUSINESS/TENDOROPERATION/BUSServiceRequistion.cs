using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSServiceRequistion ��ժҪ˵����
	/// </summary>
	public class BUSServiceRequistion:BUSBase
	{
		DAEServiceRequistion daesr = new DAEServiceRequistion();
		public BUSServiceRequistion()
		{


			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public bool CheckState(String strTenderID , TenderState state)
		{
			DataTable dt = daesr.CheckState(strTenderID);
			int strState = -1 ; 
			int intState = (int)state ; 
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["CheckState"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CheckState"]);
			}
			return  strState >= intState ;

		}

		//  Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ������ڵ㱻��һ�ڵ�����ʱ����Ӧ��ɾ��
		public bool CheckDeleteRecord(String strPKValue , TenderState state )
		{
			int strState = -1 ; 
			int intState = (int)state ; 
			DataTable dt = daesr.GetRecord(strPKValue) ;
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["State"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["State"]);
			}
			return  strState <= intState ;
		}
		//*********************************************************

		//add  by wxc  (2006/11/27)
		/// <param name="sCurrentUserID">��½���</param>
		/// <returns>popedomDepID</returns>

		public string GetUserDepartmentID(string sCurrentUserID)
		{
			string popedomDepID=string.Empty;
			//����Ȩ�޵Ŀ���
			if(daesr.GetAllDepartmentID(sCurrentUserID)!=null)
			{
				//ȡ�õ�½�ߵ����ڲ��ŵ�ID�����ܰ������֣�һ�����ǲ��ŵ��쵼��һ�������ǲ��ŵ��쵼��
				DataSet ds=daesr.GetAllDepartmentID(sCurrentUserID);
				
				int nCount = ds.Tables.Count;
				//����Щ�����н���ѭ��
				for(int i=0;i<nCount;i++ )
				{
					int k = ds.Tables[i].Rows.Count;
					for(int j= 0; j<k ;j++)
					{
						//ѭ����ʱ����ds.Tables[i].Rows[j][1].ToString()=="1" �����Ǵ˲��ŵ�����
						if(ds.Tables[i].Rows[j][1]!=System.DBNull.Value&&ds.Tables[i].Rows[j][1].ToString()=="1")
						{
							//ѭ������DepID
							popedomDepID+=","+ds.Tables[i].Rows[j][0].ToString();

						}

					}
				}
			}
			return popedomDepID;

		}
		
	}
}
