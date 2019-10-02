using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSMaxMinMaterialEdit 的摘要说明。
	/// </summary>
	public class BUSMaxMinMaterialEdit : BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSMaxMinMaterialEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		public string CheckMaxMinCapacity(DataTable dtChild)
		{
			string errMessage = string.Empty;
			string  strMaxCapacity = dtChild.Rows[0]["MaxMinMaterial.MaxCapacity"].ToString(); 
			string strMinCapacity = dtChild.Rows[0]["MaxMinMaterial.MinCapacity"].ToString();

			if( strMaxCapacity != string.Empty && strMinCapacity != string.Empty )
			{
				decimal decMax = Convert.ToDecimal(strMaxCapacity.ToString());
				decimal decMin = Convert.ToDecimal(strMinCapacity.ToString());

				if( decMin > decMax )
				{
					errMessage = "MaxToMin";
				}
			}
			return errMessage;
		}



//Add by ZZH on 2008-1-11 此验证逻辑产生编辑时报错，修改了业务主键所以不需要此验证逻辑了
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
//		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
//		{
//			// 错误信息
//			string sErrorMsg = string.Empty;
//
//			if ( dt.Rows.Count > 0 )
//			{
//				// 首先对库容进行判断 最小库容<最大库容
//				if ( Convert.ToDecimal( dt.Rows[0]["MaxMinMaterial.MaxCapacity"] ) <= Convert.ToDecimal( dt.Rows[0]["MaxMinMaterial.MinCapacity"] ) )
//				{
//					sErrorMsg = "MaxToMin";
//				}
//
//				if ( sErrorMsg.Length == 0 )
//				{
//					// 同一库房下ItemCode不允许重复
//					DataEntity.DAEMaxMinMaterialEdit dataEntity = this.IEntity as DataEntity.DAEMaxMinMaterialEdit;
//
//					int iNum = dataEntity.ExistsItemCode ( dt.Rows[0]["MaxMinMaterial.ItemCode"].ToString() , dt.Rows[0]["MaxMinMaterial.WHID"].ToString() );
//					if ( iNum > 0)
//					{
//						sErrorMsg = "ItemCode";				
//					}
//				}
//			}
//
//			return sErrorMsg;
//		}
//*********************************************
	}
}
