using System;
using System.Data;
using DataEntity;

namespace Business
{
	/// <summary>
	/// 发标的数据实体类
	/// </summary>
	public class BUSTenderSend : BUSBase
	{
		DataEntity.DAETenderSend dataEntity;	// 数据实体
		DataEntity.CEntityUitlity cEntity;				// 通用数据实体

		string TenderSendDate = string.Empty;	// 发标日期
		string strTenderID = string.Empty;			// TenderID
		
		/// <summary>
		/// 
		/// </summary>
		public  BUSTenderSend()
		{
			dataEntity = new DataEntity.DAETenderSend();
			cEntity = new DataEntity.CEntityUitlity();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			string strErrorMsg = string.Empty;
			

			if ( this.IEntity.TableName == "ITBDocument" )
			{
				if ( dt.Rows.Count > 0 )
				{
					TenderSendDate = Convert.ToDateTime ( dt.Rows[0]["ITBDocument.SendDate"] ).ToString(@"yyyy-MM-dd");
					strTenderID = Convert.ToString ( dt.Rows[0]["ITBDocument.TenderID"] );

					if ( Convert.ToDateTime ( dt.Rows[0]["ITBDocument.SendDate"] ) > Convert.ToDateTime ( dt.Rows[0]["ITBDocument.EndDate"] ) )
					{
						strErrorMsg = "SendDateCannotBeLater";
					}
				}
			}

			if ( this.IEntity.TableName == "ITBVendorList" && strErrorMsg.Length == 0 )
			{
				bool IsOk = true;

				foreach ( DataRow dr in dt.Rows )
				{
					if( dr.RowState != DataRowState.Deleted )
					{
						// Add by ZZH on 2008-2-1 不需要叛断发标人与截标人
						if ( Convert.ToString( dr["ITBVendorList.TakeBidDate"]).Length == 0  )
						{
							IsOk = false;
							break;
						}
						//						if ( Convert.ToString( dr["ITBVendorList.TakeBidDate"]).Length == 0 || Convert.ToString( dr["ITBVendorList.SendMan"]).Length == 0 || Convert.ToString( dr["ITBVendorList.ReceivedMan"]).Length == 0 )
						//						{
						//							IsOk = false;
						//							break;
						//						}
						//*****************************************************
					}
				}

				if ( dt.Rows.Count == 0 )
				{
					IsOk = false;
				}

				if ( IsOk )
				{
					// 更新状态,首先标书状态设置为2(截标阶段),然后SR状态设置为"BIDTake"
//					strErrorMsg = dataEntity.SetITBDocumentState ( Convert.ToString( dt.Rows[0]["ITBVendorList.ITBDocumentID"] ) , 2 );

					// 设置 ITBDocument/TCStrategy/SR 的状态 add by bincurd
					CEntityUitlity ceu = new CEntityUitlity();
					ceu.UpdateITBDocumentState (strTenderID,TenderState.State_ITBStart);
				}
				else
				{
					if  ( TenderSendDate.Length > 0 && strTenderID.Length > 0 )
					{
						// 回写招标表的发标时间
						strErrorMsg = dataEntity.SetTenderSendDate ( strTenderID , TenderSendDate );

						// 更新SR的状态为
						//Modified by QSQ 1218 
						//strErrorMsg += cEntity.UpdateTenderState ( DataEntity.SRState.State_ITBStart ,  strSRID);
						//strErrorMsg += cEntity.UpdateTenderState (DataEntity.SRState.State_ITBStart ,  strSRID , 0);
					}
				}
			}

			strErrorMsg += base.CheckBusinessLogic_rule (dt, fieldsList);

			return strErrorMsg;
		}

	}
}
