using System;
using System.Data;
using DataEntity;
using Common;

namespace Business
{
	/// <summary>
	/// BUSTCStrategy_Edit 的摘要说明。
	/// </summary>
	public class BUSTCStrategy_Edit:BUSBase
	{
		/// <summary>
		/// 数据实体通用类
		/// </summary>
		CEntityUitlity cEntity;
        CXmlReader pXmlReader = new CXmlReader();
		/// <summary>
		/// 构造函数
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

		#region 是否在表dtViewer中存在
		/// <summary>
		/// 是否在表dtViewer中存在
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


		#region 验证根据项目的金额是否需要隐藏部分控件

		/// <summary>
		/// 验证根据项目的金额是否需要隐藏部分控件
		/// </summary>
		/// <param name="CurrencyIDFrom">项目使用的货币单位</param>
		/// <returns>1:显示,:0不显示,-1不存在对应汇率</returns>
		public int IsHidenControl ( string CurrencyIDFrom , decimal  CurrencyMoneyAmount )
		{
			DataEntity.CEntityUitlity cEntity = new DataEntity.CEntityUitlity();		// 数据实体通用类
			int iState = 0 ;	// 显示状态变量
			string CurrencyIDTo = string.Empty;	// 目标货币
			decimal dAmountLevel = 0 ;				// SR金额的级别
			decimal dAmountByCompute = 0 ;		// 根据目标货币单位计算出的金额
            
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

		#region 计算SR中计划金额的合计值(计算汇率)与配置文件中的标准比较后确定是否显示部分控件

		/// <summary>
		/// 计算SR中计划金额的合计值(计算汇率)与配置文件中的标准比较后确定是否显示部分控件 
		/// </summary>
		/// <param name="dt">策略相关SR子表</param>
		/// <param name="dAmount">计划金额合计值</param>
		/// <returns></returns>
		public bool IsHidenControl ( DataTable dt , ref decimal dAmount )
		{
			try
			{
				decimal dLevel = 0;
				decimal dAmount_ByLevelCurrency = 0;
				string CurrencyIDTo = string.Empty;	// 目标货币 
				string CurrencyIDLevel = string.Empty;	// 配置的比较货币

				//dLevel = decimal.Parse( System.Configuration.ConfigurationSettings.AppSettings["SRPlanAmountLevel"] );
                dLevel = decimal.Parse(pXmlReader.GetSingleNodeValue("System.xml", "configuration/SystemConfig/SRPlanAmountLevel", "value"));
				// 比较货币使用当前系统的核算币
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

				// 将统计完的货币总和(核算币)换算成配置文件中的货币
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
