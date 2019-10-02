using System;
using System.Data;
using DataEntity;
using Common;

namespace Business
{
	/// <summary>
	/// BUSTCStrategy_Edit ��ժҪ˵����
	/// </summary>
	public class BUSTCStrategy_Edit:BUSBase
	{
		/// <summary>
		/// ����ʵ��ͨ����
		/// </summary>
		CEntityUitlity cEntity;
        CXmlReader pXmlReader = new CXmlReader();
		/// <summary>
		/// ���캯��
		/// </summary>
		public BUSTCStrategy_Edit()
		{
			cEntity = new CEntityUitlity();
		}

		public string CheckChildRows(DataTable dtTCStrategy)
		{
			string sErrMsg = "";
			DataTable dtCheckRow = dtTCStrategy.Copy();
			dtCheckRow.AcceptChanges();
			if(dtCheckRow.Rows.Count == 0)
			{
				sErrMsg= "NoSRSelected" ;
			}
			return sErrMsg;
		}

		#region �Ƿ��ڱ�dtViewer�д���
		/// <summary>
		/// �Ƿ��ڱ�dtViewer�д���
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtViewer )
		{
			foreach(DataRow dr in dtViewer.Rows)
			{
				if ( IDKey == dr["TCStrategyViewer.GroupPeopleID"].ToString()) return true;
			}
			return false;
		}
		#endregion


		#region ��֤������Ŀ�Ľ���Ƿ���Ҫ���ز��ֿؼ�

		/// <summary>
		/// ��֤������Ŀ�Ľ���Ƿ���Ҫ���ز��ֿؼ�
		/// </summary>
		/// <param name="CurrencyIDFrom">��Ŀʹ�õĻ��ҵ�λ</param>
		/// <returns>1:��ʾ,:0����ʾ,-1�����ڶ�Ӧ����</returns>
		public int IsHidenControl ( string CurrencyIDFrom , decimal  CurrencyMoneyAmount )
		{
			DataEntity.CEntityUitlity cEntity = new DataEntity.CEntityUitlity();		// ����ʵ��ͨ����
			int iState = 0 ;	// ��ʾ״̬����
			string CurrencyIDTo = string.Empty;	// Ŀ�����
			decimal dAmountLevel = 0 ;				// SR���ļ���
			decimal dAmountByCompute = 0 ;		// ����Ŀ����ҵ�λ������Ľ��
            
			try 
			{
				//dAmountLevel = Convert.ToDecimal( System.Configuration.ConfigurationSettings.AppSettings["SRPlanAmountLevel"] );
                dAmountLevel = Convert.ToDecimal(pXmlReader.GetSingleNodeValue("System.xml", "configuration/SystemConfig/SRPlanAmountLevel", "value"));
                //CurrencyIDTo = System.Configuration.ConfigurationSettings.AppSettings["Currency"];
                CurrencyIDTo = pXmlReader.GetSingleNodeValue("System.xml", "configuration/SystemConfig/Currency", "value");

				dAmountByCompute = cEntity.GetCurrencyMoney ( CurrencyIDFrom , CurrencyIDTo , CurrencyMoneyAmount );

				if ( dAmountByCompute >= dAmountLevel )
				{
					iState = 1;
				}

				if ( dAmountByCompute == -1 )
				{
					iState = -1;
				}
			}
			catch
			{
				throw new Exception ();
			}

			return iState ; 
		}

		#endregion

		#region ����SR�мƻ����ĺϼ�ֵ(�������)�������ļ��еı�׼�ȽϺ�ȷ���Ƿ���ʾ���ֿؼ�

		/// <summary>
		/// ����SR�мƻ����ĺϼ�ֵ(�������)�������ļ��еı�׼�ȽϺ�ȷ���Ƿ���ʾ���ֿؼ� 
		/// </summary>
		/// <param name="dt">�������SR�ӱ�</param>
		/// <param name="dAmount">�ƻ����ϼ�ֵ</param>
		/// <returns></returns>
		public bool IsHidenControl ( DataTable dt , ref decimal dAmount )
		{
			try
			{
				decimal dLevel = 0;
				decimal dAmount_ByLevelCurrency = 0;
				string CurrencyIDTo = string.Empty;	// Ŀ����� 
				string CurrencyIDLevel = string.Empty;	// ���õıȽϻ���

				//dLevel = decimal.Parse( System.Configuration.ConfigurationSettings.AppSettings["SRPlanAmountLevel"] );
                dLevel = decimal.Parse(pXmlReader.GetSingleNodeValue("System.xml", "configuration/SystemConfig/SRPlanAmountLevel", "value"));
				// �Ƚϻ���ʹ�õ�ǰϵͳ�ĺ����
				CurrencyIDTo = CEntityUitlity.GetSysCurrency();
				//CurrencyIDLevel = System.Configuration.ConfigurationSettings.AppSettings["Currency"];
                CurrencyIDLevel = pXmlReader.GetSingleNodeValue("System.xml", "configuration/SystemConfig/Currency", "value");

				foreach ( DataRow dr in dt.Rows )
				{
					if(dr.RowState != DataRowState.Deleted)
					{
						if( dr["TCStrategySR.PlanAmount"]==System.DBNull.Value )
						{
							dAmount += 0 ;

						}
						else
						{
							if ( dr["TCStrategySR.PlanCurrency"] == System.DBNull.Value ) 
							{
								dAmount += decimal.Parse( dr["TCStrategySR.PlanAmount"].ToString()); 
							}
							else
							{
								dAmount += cEntity.GetCurrencyMoney( dr["TCStrategySR.PlanCurrency"].ToString() , CurrencyIDTo , decimal.Parse( dr["TCStrategySR.PlanAmount"].ToString()) ) ;
							}
						}
					}
				}

				// ��ͳ����Ļ����ܺ�(�����)����������ļ��еĻ���
				dAmount_ByLevelCurrency = cEntity.GetCurrencyMoney ( CurrencyIDTo , CurrencyIDLevel , dAmount );

				if ( dAmount_ByLevelCurrency >= dLevel ) return true;
				else return false;
			}
			catch 
			{
				throw new Exception();
			}
		}

		#endregion
	}
}
