using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEApprovedBusinesProcedureView 的摘要说明。
	/// </summary>
	public class DAEApprovedBusinesProcedureView
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();

		public DAEApprovedBusinesProcedureView()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//

		}
		public DataTable GetTable()
		{
			//此处添加distinct 语句，过滤同一个审批步骤有多个审批人的情况
			//但是如果在同一审批步骤下，审批描述不同，还是会查出多条记录
//			string sql = @"select
//							max(TI_ApproveFlowMember.IDKey) as IDKey, 
//							max(ISNULL(Approved.CurrApproveLevel,1)) AS ApproeLevel,
//							max(Putin.ObjectiveTitle) as ObjectiveTitle,
//							max(TI_ApproveType.ApprovalTypeName) as ApprovalTypeName,
//							max(TI_ApproveFlowMember.ApproeDescription) as ApproeDescription
//							from Putin 
//							left join Approved on Putin.IDKey=Approved.PutInID
//							left join TI_ApproveFlowMember on Putin.ApproveFlowID=TI_ApproveFlowMember.IDKey
//							AND TI_ApproveFlowMember.ApproeLevel =
//							(select MAX(ISNULL(Approved.CurrApproveLevel,1)) AS CApproveLevel from Approved where Approved.PutInID=Putin.IDKey)
//							LEFT JOIN TI_ApproveType on Putin.ObjectiveType=TI_ApproveType.IDKey
//							where 
//							Putin.state in ('-1','0')
//							group by Putin.IDKey";


				
			string sql = @"select 
						TI_ApproveFlowMember.ApproeLevel,
						max(TI_ApproveFlowMember.IDKey) as IDKey, 
						max(Putin.ObjectiveTitle) as ObjectiveTitle,
						max(TI_ApproveType.ApprovalTypeName) as ApprovalTypeName,
						max(TI_ApproveFlowMember.ApproeDescription) as ApproeDescription
						from Putin 
						left join Approved on Putin.IDKey=Approved.PutInID
						left join TI_ApproveFlowMember on Putin.ApproveFlowID=TI_ApproveFlowMember.IDKey
							LEFT JOIN TI_ApproveType on Putin.ObjectiveType=TI_ApproveType.IDKey
						where 
						Putin.state in ('-1','0')
						group by Putin.IDKey,TI_ApproveFlowMember.ApproeLevel
						  having TI_ApproveFlowMember.ApproeLevel = max(ISNULL(Approved.CurrApproveLevel,1))";

			return _da.GetDataTable(sql);
		}
	}
}
