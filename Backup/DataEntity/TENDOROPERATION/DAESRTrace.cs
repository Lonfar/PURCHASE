using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAESRTrace 的摘要说明。
	/// </summary>
	public class DAESRTrace:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();

		public DAESRTrace()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 服务申请跟踪
		/// added by wanglijie on 2007-04-17
		/// </summary>
		/// <returns></returns>
		public DataTable GetTable_Trace( string whereSql1,string whereSql2)
		{
			string Sql = string.Empty;

			Sql =@"SELECT ServiceRequistion.SRID,ProRegisterDate,BI_Employee.FullName,ProUndertakerDate,TCStrategySR.TenderID, 
						TCStrategy.CreateDate,a.ContractID,a.ApprovedBy , a.AcceptTenderDate,a.CreateDate as ContractCreateDate
						FROM ServiceRequistion 
						left join BI_Employee ON ServiceRequistion.ProUndertaker = BI_Employee.idkey 
						LEFT JOIN TCStrategySR ON ServiceRequistion.idkey = TCStrategySR.SRID  
						left join TCStrategy ON TCStrategySR.TenderID =  TCStrategy.TenderID  
						lEFT JOIN (SELECT Contract.TenderNumber,PutIn.ApprovedBy,Contract.ContractID,Contract.AcceptTenderDate, Contract.CreateDate FROM Contract  
									left join PutIn on PutIn.ObjectiveID =Contract.idkey 
									WHERE TenderNumber IS NOT  NULL AND Contract.State IS NULL)a 
						ON TCStrategy.TenderID = a.TenderNumber WHERE ServiceRequistion.SRFlow =1  ";
			Sql += whereSql1;
			Sql += @"  UNION ALL 
						SELECT ServiceRequistion.SRID,ProRegisterDate,BI_Employee.FullName,ProUndertakerDate, 
							NULL as TenderID,NULL as CreateDate,Contract.ContractID,PutIn.ApprovedBy ,Contract.AcceptTenderDate,Contract.CreateDate as ContractCreateDate
						FROM ServiceRequistion 
						left join BI_Employee ON ServiceRequistion.ProUndertaker = BI_Employee.idkey 
						LEFT JOIN ContractRequistion on ContractRequistion.SRID = ServiceRequistion.SRID 
						left join Contract on  Contract.idkey = ContractRequistion.ContractID 
						left join PutIn on PutIn.ObjectiveID =Contract.idkey 
						WHERE ServiceRequistion.SRFlow =0";
									Sql += whereSql2;
			//			Sql += " order by Contract.vTP DESC" ; //VendorName,SubscribeDate,IsAddProtocol";

			DataTable dt = _da.GetDataTable(Sql);

			return dt;
		}
	}
}
