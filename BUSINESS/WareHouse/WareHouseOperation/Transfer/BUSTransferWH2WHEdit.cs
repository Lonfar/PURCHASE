/*
 * Create nongbin 2007-06-28
 * 
 * 关于库间转料的页面
 * */
using System;
using System.Data;
using DataEntity;


namespace Business
{
	/// <summary>
	/// BUSTransferWH2WHEdit 的摘要说明。
	/// </summary>
	public class BUSTransferWH2WHEdit:BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSTransferWH2WHEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

	    #region  业务逻辑运算
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtEdit"></param>
		/// <param name="dtChild"></param>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal decTotalAmountStandard = 0.0m ;
			decimal decTotalAmountNatural = 0.0m ;

			foreach(DataRow row in dtChild.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(row["WH_TransferWH2WHMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(row["WH_TransferWH2WHMaterial.UnitPriceNatural"].ToString());
					decimal decTransferQuantity =  Convert.ToDecimal(row["WH_TransferWH2WHMaterial.TransferQuantity"].ToString());
					decTotalAmountStandard += decUnitPriceStandard * decTransferQuantity;
					decTotalAmountNatural += decUnitPriceNatural * decTransferQuantity ;
				}
			}
			
			dtEdit.Rows[0]["WH_TransferWH2WH.TotalPriceStandard"] = decTotalAmountStandard;
			dtEdit.Rows[0]["WH_TransferWH2WH.TotalPriceNatural"] = decTotalAmountNatural; 
		}
		#endregion

		#region  业务规则验证
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dtChild)
		{
			string sErrMsg = "";
			if(dtChild.Rows.Count >0)
			{
				foreach(DataRow row in dtChild.Rows)
				{
					if(row.RowState != DataRowState.Deleted)
					{
						//库房数量
						decimal decQuantityInBin = Convert.ToDecimal(row["WH_TransferWH2WHMaterial.TransferQuantity"].ToString());
						//借料数量
						decimal decQuantityBorrow = Convert.ToDecimal(row["WH_TransferWH2WHMaterial.QuantityInOldBin"].ToString());
						//借料数量应小于库房数量
						if(decQuantityBorrow < decQuantityInBin)
						{
							sErrMsg ="ErrQuantity";
							break;
						}
					}
				}
			}
			return sErrMsg;
		}
		
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtChild)
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
		

		#region 获得库房ID的筛选条件
		/// <summary>
		/// 获得库房ID的筛选条件
		/// </summary>
		public string GetWHIDFilter ()
		{
			// 库位数据实体类
			DataEntity.DAEBIBIN dAEBIBIN = new DataEntity.DAEBIBIN();
			string sFilter = string.Empty;

			// 查询
			string sWHIDs = dAEBIBIN.GetWHHasBIN();
			if ( sWHIDs.Length > 0 )
			{
				sFilter = " WH_BI_WareHouse.WHID IN ( " + sWHIDs + ")";
			}

			return sFilter;
		}

		#endregion


		#region 获得库位的筛选条件

		/// <summary>
		/// 根据库房获得对应库位的筛选条件
		/// </summary>
		/// <param name="sWHID">WHID</param>
		/// <returns></returns>
		public string GetBINIDFilter ( string sWHID )
		{
			// 库位数据实体类
			DataEntity.DAEBIBIN dAEBIBIN = new DataEntity.DAEBIBIN();
			string sFilter = string.Empty;

			// 查询
			string sBINIDs = dAEBIBIN.GetAllBINIDByWHID ( sWHID );
			if ( sBINIDs.Length > 0 )
			{
				sFilter = "WH_BI_BIN.BINID IN ( "+sBINIDs+" )";
			}

			return sFilter;
		}

		#endregion

		
		#region 重写CheckBusinessData(),检查 转料数量不能大于库存数量
		
		/// <summary>
		/// 重写CheckBusinessData(),检查 转料数量不能大于库存数量
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessData(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			string sErrorMsg = string.Empty;

			switch ( this.IEntity.TableName )
			{
				case "WH_TransferWH2WH" :
				{
					// 校验主表数据
					break;
				}
				case "WH_TransferWH2WHMaterial" :
				{
					if(CheckIsTransferQuantityTooLarge(dt,fieldsList) == true)
					{
						sErrorMsg = fieldsList[0].ToString();	//没有多语
					}
					break;
				}
			}

			if ( sErrorMsg.Length == 0 )
			{
				sErrorMsg = base.CheckBusinessData ( dt , fieldsList );
			}

			return sErrorMsg;
		}


		private bool CheckIsTransferQuantityTooLarge(DataTable dt,System.Collections.ArrayList fieldsList)
		{
			//得到一个带有转料数量dt
			//开始循环
			DAETransferWH2WHEdit DAETransferWH2WHEdit = new DAETransferWH2WHEdit(); 
			bool flag = false;
			
			if(dt.Rows.Count < 1) return false;						//如果没有行，则返回
			
			foreach ( DataRow dr in dt.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					string  sql = @"select QuantityInBin 
								from WH_InStoreMaterialDetail 
				where InStockMaterialID = '"+dr["WH_TransferWH2WHMaterial.InStockMaterialID"].ToString()+
						"' and BINID = '"+dr["WH_TransferWH2WHMaterial.BINIDOld"].ToString()+
						"' and POID = '"+dr["WH_TransferWH2WHMaterial.POID"].ToString() +"'";

				
					DataTable dtQuantityInBin = DAETransferWH2WHEdit.CheckIsTransferQuantity(sql);
			
				
					if(dtQuantityInBin.Rows.Count < 1)	continue;		//如果返回的"库存物资明细表"里的记录为空

					Decimal TransferQuantity = Convert.ToDecimal(dr["WH_TransferWH2WHMaterial.TransferQuantity"]);
					Decimal QuantityInBin = Convert.ToDecimal(dtQuantityInBin.Rows[0]["QuantityInBin"]);

				
					//如果dataTable当前循环行>从"库存物资明细表"取得的 库位数量
					if(TransferQuantity > QuantityInBin)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		#endregion

	}
}
