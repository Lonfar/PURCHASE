using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSServiceRequistion 的摘要说明。
	/// </summary>
	public class BUSServiceRequistion:BUSBase
	{
		DAEServiceRequistion daesr = new DAEServiceRequistion();
		public BUSServiceRequistion()
		{


			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
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

		//  Add by ZZH on 2008-1-21 添加验证是否可以删除的方法当节点被下一节点引用时，不应被删除
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
		/// <param name="sCurrentUserID">登陆编号</param>
		/// <returns>popedomDepID</returns>

		public string GetUserDepartmentID(string sCurrentUserID)
		{
			string popedomDepID=string.Empty;
			//数据权限的控制
			if(daesr.GetAllDepartmentID(sCurrentUserID)!=null)
			{
				//取得登陆者的所在部门的ID（可能包含两种：一种他是部门的领导，一种他不是部门的领导）
				DataSet ds=daesr.GetAllDepartmentID(sCurrentUserID);
				
				int nCount = ds.Tables.Count;
				//在这些部门中进行循环
				for(int i=0;i<nCount;i++ )
				{
					int k = ds.Tables[i].Rows.Count;
					for(int j= 0; j<k ;j++)
					{
						//循环的时候发现ds.Tables[i].Rows[j][1].ToString()=="1" 表明是此部门的主管
						if(ds.Tables[i].Rows[j][1]!=System.DBNull.Value&&ds.Tables[i].Rows[j][1].ToString()=="1")
						{
							//循环加上DepID
							popedomDepID+=","+ds.Tables[i].Rows[j][0].ToString();

						}

					}
				}
			}
			return popedomDepID;

		}
		
	}
}
