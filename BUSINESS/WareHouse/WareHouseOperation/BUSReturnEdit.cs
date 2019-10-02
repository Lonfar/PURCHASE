using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSReturnEdit 的摘要说明。
	/// </summary>
	public class BUSReturnEdit: BUSBase
	{
		public BUSReturnEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

//		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
//		{
//			string sErrorMsg = string.Empty ;
//
//			if ( IEntity.TableName == "WH_ReturnMaterial" )
//			{
//				sErrorMsg = Check_WH_ReturnMaterial ( dt ) ;
//			}
//
//			return sErrorMsg ;
//		}

		/// <summary>
		/// 计算借料核算和本位总额
		/// </summary>
		/// <param name="dtBorrowEdit">Edit表</param>
		/// <param name="decTotalAmountStandard">核算总额</param>
		/// <param name="decTotalAmountNatural">本位总额</param>
		/// <returns></returns>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal dReturnSumPriceStandard = 0 ;
			decimal dReturnSumPriceNatural = 0 ;
			foreach(DataRow row in dtChild.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					decimal dFactReturnQuantity = Convert.ToDecimal ( row["WH_ReturnMaterial.FactReturnQuantity"].ToString() ) ;
					decimal dUnitPriceStandard = Convert.ToDecimal ( row["WH_ReturnMaterial.UnitPriceStandard"].ToString() ) ;
					decimal dUnitPriceNatural = Convert.ToDecimal ( row["WH_ReturnMaterial.UnitPriceNatural"].ToString() ) ;
					decimal dDepreciationRate = Convert.ToDecimal ( row["WH_ReturnMaterial.depreciationRate"].ToString() ) ;

					decimal dSumPriceStandard = dFactReturnQuantity * dUnitPriceStandard * dDepreciationRate ;
					dReturnSumPriceStandard += dSumPriceStandard ;

					decimal dSumPriceNatural = dFactReturnQuantity * dUnitPriceNatural * dDepreciationRate ;
					dReturnSumPriceNatural += dSumPriceNatural ;

					row["WH_ReturnMaterial.SumPrice"] = dSumPriceStandard ;
				}
			}
			dtEdit.Rows[0]["WH_Return.TotalPriceStandard"] = dReturnSumPriceStandard ;
			dtEdit.Rows[0]["WH_Return.TotalPriceNatural"] = dReturnSumPriceNatural ;
		}		

		#region Check WH_ReturnMaterial

		private string Check_WH_ReturnMaterial ( DataTable dt )
		{
			decimal nCanReturnQuantity = 0 ;
			decimal nFactReturnQuantity = 0 ;
			double dDepreciationRate = 0 ;

			foreach(DataRow row in dt.Rows)
			{
				if(row.RowState  != DataRowState.Deleted)
				{
					nCanReturnQuantity = ( row["WH_ReturnMaterial.CanReturnQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal( row["WH_ReturnMaterial.CanReturnQuantity"].ToString() )) ;
					nFactReturnQuantity = (row["WH_ReturnMaterial.FactReturnQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal( row["WH_ReturnMaterial.FactReturnQuantity"].ToString() ) );
					dDepreciationRate = ( row["WH_ReturnMaterial.depreciationRate"] == DBNull.Value ? 0 : Convert.ToDouble ( row["WH_ReturnMaterial.depreciationRate"].ToString() )) ;

					if ( nFactReturnQuantity > nCanReturnQuantity ) return "Error01" ;

					//折旧率范围在0到1之间的数值  2007-08-23 wanglijie
					if ( dDepreciationRate > 1 || dDepreciationRate < 0 ) return "Error02" ;
				}
			}
			return string.Empty ;
		}

		/// <summary>
		/// 校验数据逻辑
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// 错误信息
			string sErrorMsg = string.Empty;
			// 校验子表是否有数据
			sErrorMsg = CheckChildRows(dt);
			if ( sErrorMsg.Trim().Length == 0 )
			{
				// 校验业务主键           
				sErrorMsg = Check_WH_ReturnMaterial ( dt );
				if ( sErrorMsg.Trim().Length > 0 )
				{ 
					return sErrorMsg;
				}
			}
			return sErrorMsg;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		private string CheckChildRows(DataTable dtChild)
		{
			foreach(DataRow dr in dtChild.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoMaterialSelected" ;
		}

		#endregion

	}
}
