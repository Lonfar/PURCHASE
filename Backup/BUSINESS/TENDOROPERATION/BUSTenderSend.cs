using System;
using System.Data;
using DataEntity;

namespace Business
{
	/// <summary>
	/// ���������ʵ����
	/// </summary>
	public class BUSTenderSend : BUSBase
	{
		DataEntity.DAETenderSend dataEntity;	// ����ʵ��
		DataEntity.CEntityUitlity cEntity;				// ͨ������ʵ��

		string TenderSendDate = string.Empty;	// ��������
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
						// Add by ZZH on 2008-2-1 ����Ҫ�ѶϷ�������ر���
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
					// ����״̬,���ȱ���״̬����Ϊ2(�ر�׶�),Ȼ��SR״̬����Ϊ"BIDTake"
//					strErrorMsg = dataEntity.SetITBDocumentState ( Convert.ToString( dt.Rows[0]["ITBVendorList.ITBDocumentID"] ) , 2 );

					// ���� ITBDocument/TCStrategy/SR ��״̬ add by bincurd
					CEntityUitlity ceu = new CEntityUitlity();
					ceu.UpdateITBDocumentState (strTenderID,TenderState.State_ITBStart);
				}
				else
				{
					if  ( TenderSendDate.Length > 0 && strTenderID.Length > 0 )
					{
						// ��д�б��ķ���ʱ��
						strErrorMsg = dataEntity.SetTenderSendDate ( strTenderID , TenderSendDate );

						// ����SR��״̬Ϊ
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
