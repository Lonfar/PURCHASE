using System;
using System.Text;
using System.Data;

namespace DataEntity.WareHouseManagment
{
	/// <summary>
	/// 收料数据实体类 Liujun Add at 2007-6-22
	/// </summary>
	public class DAEReceive : DAEBase
	{
		public DAEReceive()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		#region 根据所选的PO物资带出对应值

        public string GetVenderAndDepName(string POID, string ReceiveID)
        {
            string strSql = "SELECT CON_BidderList.Name, BI_Department.DepartmentName, ReceiveNo,WHDescription,CurrencyID FROM PurchaseOrder LEFT OUTER JOIN " +
                "  CON_BidderList ON PurchaseOrder.VendorID = CON_BidderList.BidderID LEFT JOIN BI_Department ON " +
                "  PurchaseOrder.DepID = BI_Department.IDKey INNER JOIN WH_Receive ON WH_Receive.POID = PurchaseOrder.POID inner join WH_BI_WareHouse on WH_BI_WareHouse.WHID=WH_Receive.WHID WHERE (WH_Receive.ReceiveID = N'" + ReceiveID + "')";
            DataTable da = new DataTable();
            da = this.BaseDataAccess.GetDataTable(strSql);
            string strNO = da.Rows[0][0].ToString() + "$" + da.Rows[0][1].ToString() + "$" + da.Rows[0][2].ToString() + "$" + da.Rows[0][3].ToString() + "$" + da.Rows[0][4].ToString();
            return strNO;
        }
        public string GetVenderAndDepName(string POID)
        {
            string strSql = "SELECT CON_BidderList.Name, BI_Department.DepartmentName FROM PurchaseOrder LEFT OUTER JOIN " +
                "  CON_BidderList ON PurchaseOrder.VendorID = CON_BidderList.BidderID LEFT OUTER JOIN BI_Department ON " +
                "  PurchaseOrder.DepID = BI_Department.IDKey WHERE (PurchaseOrder.POID = N'"+ POID +"')";
            DataTable da = new DataTable();
            da = this.BaseDataAccess.GetDataTable(strSql);
            string strNO = da.Rows[0][0].ToString() + "$" + da.Rows[0][1].ToString() ;
            return strNO;
        }

		/// <summary>
		/// 根据所选的PO物资带出对应值
		/// </summary>
		/// <param name="dtReceiveMaterial"></param>
		public void GetPOMaterialInfo ( DataTable dtReceiveMaterial,string sWHID)
		{
			string sSelectSql = string.Empty;
			DataTable dtPOMaterial;
			CEntityUitlity cEntity = new CEntityUitlity();
			string sItemCode = string.Empty;
			string BINID = GetBINIDFromWHID(sWHID);

            //dtReceiveMaterial.DefaultView.Sort = "POLine,ItemCode";
			// 需要计算可收数量
			foreach ( DataRow dr in dtReceiveMaterial.Rows )
			{
				//-------------add by wudi---------2007-8-8-------------
				if(dr.RowState != DataRowState.Deleted)
				{
				
					// 查询基本
					sSelectSql = @" SELECT POMaterial.MaterialUomID ,POMaterial.ItemCode,POMaterial.ReceiveBaseQuantity,MaterialUom.UOMID, 
                                    POMaterial.PartNo , POMaterial.UnitPrice , POMaterial.POQuantity ,Material.MaterialName,POMaterial.MRNO,POMaterial.POLine,POMaterial.Remark
					FROM POMaterial 
					INNER JOIN MaterialUOM ON POMaterial.MaterialUomID = MaterialUOM.MaterialUomID  
					INNER JOIN Material ON Material.ItemCode = POMaterial.ItemCode WHERE  POMaterialID = '" + dr["POMaterialID"].ToString() + "' ORDER BY POMaterial.POLine,POMaterial.ItemCode";

					dtPOMaterial = this.BaseDataAccess.GetDataTable ( sSelectSql );
                    if (dtPOMaterial.Rows.Count > 0)
                    {

                        dr["MaterialUomID"] = dtPOMaterial.Rows[0]["MaterialUomID"];
                        dr["WH_ReceiveMaterial__MaterialUomID"] = dtPOMaterial.Rows[0]["UOMID"];
                        dr["PartNO"] = dtPOMaterial.Rows[0]["PartNo"];
                        dr["UnitPrice"] = dtPOMaterial.Rows[0]["UnitPrice"];
                        dr["POQuantity"] = dtPOMaterial.Rows[0]["POQuantity"];
                        dr["ItemCode"] = dtPOMaterial.Rows[0]["ItemCode"].ToString();
                        dr["MaterialName"] = dtPOMaterial.Rows[0]["MaterialName"].ToString();
                        //dr["WH_ReceiveMaterial__BINID"] = BINID;
                        //dr["BINID"] = BINID;
                        dr["Comment"] = dtPOMaterial.Rows[0]["Remark"].ToString();
                        dr["POLine"] = dtPOMaterial.Rows[0]["POLine"].ToString();
                        if (dtPOMaterial.Rows[0]["ReceiveBaseQuantity"] == DBNull.Value || Convert.ToDecimal(dtPOMaterial.Rows[0]["ReceiveBaseQuantity"]) == 0)
                        {
                            dr["ReceivedQuantity"] = 0;
                            dr["CanReceivedQuantity"] = dr["POQuantity"];
                        }
                        else
                        {
                            // 将已收数量转换为当前的单位
                            decimal fReceivedQuattity = cEntity.ChangeFromBaseUON(dtPOMaterial.Rows[0]["ItemCode"].ToString(), dtPOMaterial.Rows[0]["MaterialUomID"].ToString(), Convert.ToDecimal(dtPOMaterial.Rows[0]["ReceiveBaseQuantity"]));

                            dr["ReceivedQuantity"] = fReceivedQuattity;
                            dr["CanReceivedQuantity"] = Convert.ToDecimal(dr["POQuantity"]) - fReceivedQuattity;
                        }
                    }
				}
			
			}
		}

		private string GetBINIDFromWHID(string sWHID)
		{
			string sSql = "SELECT BINID FROM WH_BI_BIN WHERE Status='1' AND WHID = '"+sWHID+"'";
			return this.BaseDataAccess.GetDataTable ( sSql ).Rows[0][0].ToString();			
		}
        /// <summary>
        /// 得到ReceiveNO 
        /// </summary>
        /// <param name="strTime">拼装后的时间字符串</param>
        /// <returns></returns>
        public string GetReceiveNO(string strTime)
        {
            //string strTime = DateTime.Now.Year.ToString().Substring(2, 2);
            //if (DateTime.Now.Month < 10)
            //{
            //    strTime = strTime + "0" + DateTime.Now.Month.ToString();
            //}
            //else
            //{
            //    strTime = strTime + DateTime.Now.Day.ToString();
            //}
            //if (DateTime.Now.Day > 10)
            //{
            //    strTime = strTime + DateTime.Now.Day.ToString();
            //}
            //else
            //{
            //    strTime = strTime + DateTime.Now.Day.ToString();
            //}

            string strSql = "SELECT TOP 1 ReceiveNO FROM WH_Receive WHERE (ReceiveNO LIKE 'R" + strTime + "%') ORDER BY ReceiveNO DESC";

            string strNO = "";
            DataTable dt = this.BaseDataAccess.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                strNO = this.BaseDataAccess.GetDataTable(strSql).Rows[0][0].ToString();
            }
            if (strNO == "")
            {
                strNO = "001";
                return "R" + strTime + strNO;
            }
            else
            {
                strNO = strNO.Replace("R", "");
                Int64 no = Int64.Parse(strNO) + 1;

                strNO = no.ToString();
                return "R" + strNO;

            }

            /*
            string strNO = this.BaseDataAccess.GetDataTable(strSql).Rows[0][0].ToString();
            if (strNO.Length == 2)
            {
                strNO = "0" + strNO;
            }
            if (strNO.Length == 1)
            {
                strNO = "00" + strNO;
            }
            return "R" + strTime + strNO;
            */

        }


		#endregion

		public void GetPOMaterialOSDInfo ( DataTable dtReceiveMaterial)
		{
			string sSelectSql = string.Empty;
			DataTable dtPOMaterial;

			foreach ( DataRow dr in dtReceiveMaterial.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					sSelectSql = @" SELECT ItemCode,POID,PartNo,MFG FROM POMaterial 
                                    WHERE POMaterialID = '"+dr["POMaterialID"].ToString()+"'";
					dtPOMaterial = this.BaseDataAccess.GetDataTable ( sSelectSql );				
					dr["ItemCode"] = dtPOMaterial.Rows[0]["ItemCode"];
					dr["PartNo"] = dtPOMaterial.Rows[0]["PartNo"];
					dr["MFG"] = dtPOMaterial.Rows[0]["MFG"].ToString();
				}
			}
		}

		#region 根据选择的PO来获得PO对应的详细信息

		/// <summary>
		/// 根据选择的PO来获得PO对应的详细信息
		/// </summary>
		/// <param name="sPOID">POID</param>
		/// <returns></returns>
		public DataTable GetPODetails ( string sPOID )
		{
			string sSelectSql = "SELECT * FROM PurchaseOrder WHERE POID = '"+sPOID+"'";

			DataTable dtPODetails = this.BaseDataAccess.GetDataTable ( sSelectSql );

            // ======================================================================== //
            // Modified By Liujun at 20081230
            // 如果此PO已经收料，把最近一次的收料汇率查找并带出来

            if (dtPODetails.Rows.Count > 0)
            {
                sSelectSql = "SELECT TotalPriceStandardlER FROM WH_Receive WHERE POID = '"+sPOID+"' ORDER BY ReceiveNO DESC ";

                DataTable dtER = this.BaseDataAccess.GetDataTable(sSelectSql);

                if (dtER.Rows.Count > 0 && !string.IsNullOrEmpty(dtER.Rows[0][0].ToString()))
                {
                    dtPODetails.Rows[0]["ContractTotalCostStandardER"] = dtER.Rows[0][0].ToString();
                }
            }
            // ======================================================================== //

			return dtPODetails;
		}

		#endregion

		#region 在提交审核时进行校验( 实收数量是否大于可收数量 )

		/// <summary>
		/// 在提交审核时进行校验
		/// </summary>
		/// <param name="sReceiveID">收料单编号</param>
		/// <returns></returns>
		public string CheckNum ( string sReceiveID )
		{
			string sErrorMsg = string.Empty;

			string sSelectReceiveMaterial = @"SELECT ItemCode FROM 
															(SELECT 
															WH_Receive.ReceiveID,
															WH_ReceiveMaterial.ItemCode,
															WH_Receive.POID ,
															WH_ReceiveMaterial.MaterialUomID,
															WH_ReceiveMaterial.FactReceivedQuantity ,
															POMaterial.BaseQuantity,
															POMaterial.ReceiveBaseQuantity,
															(POMaterial.BaseQuantity - POMaterial.ReceiveBaseQuantity ) as CanReceive,
															(WH_ReceiveMaterial.FactReceivedQuantity * MaterialUOM.MultipleOfBaseUOM ) as FactReceive

															FROM WH_ReceiveMaterial
															INNER JOIN WH_Receive ON WH_Receive.ReceiveID = WH_ReceiveMaterial.ReceiveID
															INNER JOIN PurchaseOrder ON PurchaseOrder.POID = WH_Receive.POID
															INNER JOIN POMaterial ON POMaterial.POID = PurchaseOrder.POID
															INNER JOIN MaterialUOM ON MaterialUOM.MaterialUOMID = WH_ReceiveMaterial.MaterialUOMID 

															WHERE POMaterial.ItemCode = WH_ReceiveMaterial.ItemCode 
															) AS a 
															WHERE a.CanReceive < a.FactReceive AND ReceiveID = '"+sReceiveID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectReceiveMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ sErrorMsg = dt.Rows[0]["ItemCode"].ToString();	}

			return sErrorMsg;
		}

		#endregion

		#region 更新收料单状态

		/// <summary>
		/// 更新收料单状态
		/// </summary>
		/// <param name="sReceiveID">主键</param>
		/// <param name="state">目标状态</param>
		/// <returns></returns>
		public string UpdateReceiveState ( string sReceiveID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_Receive SET Status = "+iState.ToString()+" WHERE ReceiveID = '"+sReceiveID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );

			if ( sErrorMsg .Length == 0 )
			{
				switch ( state )
				{
					case ApproveState.State_Approved :
					{
						string[] sParams = {"ReceiveID"};
						object[] objParamValues = {sReceiveID} ; 
						SqlDbType[] paramTypes = { SqlDbType.NVarChar} ;

						bool bRetVal =  this.BaseDataAccess.ExecuteSP("spUpdatePOMaterialANDOperateStoreMaterialDetail",sParams,objParamValues,paramTypes) ; 

						if ( bRetVal == false )
						{
							sErrorMsg = "OperateStoreFailed";
						}

						// 写入财务接口
						string sSelectSql = @"SELECT WHID,ItemCode,BINID,ReceiveNo,CreateBy,FactReceivedQuantity,UnitPriceStandard FROM WH_ReceiveMaterial 
											INNER JOIN WH_Receive ON WH_ReceiveMaterial.ReceiveID = WH_Receive.ReceiveID WHERE WH_Receive.ReceiveID = '"+sReceiveID+"'";

						DataTable dtReceiveMaterial = this.BaseDataAccess.GetDataTable( sSelectSql );

						foreach ( DataRow dr in dtReceiveMaterial.Rows )
						{
							// 财务接口处理类
							CInterfaceOfFinanceAccess pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess() ;

							// 财务接口实体
							CInterfaceOfFinance pInterfaceOfFinance = new CInterfaceOfFinance();
				
							pInterfaceOfFinance.Location = dr["WHID"].ToString();
							pInterfaceOfFinance.ItemCode = dr["ItemCode"].ToString();
							pInterfaceOfFinance.BinNo = dr["BINID"].ToString();
							pInterfaceOfFinance.BillNo = dr["ReceiveNo"].ToString();
							pInterfaceOfFinance.Operater = dr["CreateBy"].ToString();
							pInterfaceOfFinance.Quantity = decimal.Parse ( dr["FactReceivedQuantity"].ToString());
							pInterfaceOfFinance.UnitPriceStandard = decimal.Parse ( dr["UnitPriceStandard"].ToString());
							pInterfaceOfFinance.OperationDirection = DIRECTIONTYPE.TYPE_IN;
							pInterfaceOfFinance.OperationType = pInterfaceOfFinanceAccess.GetBillType( BILLTYPE.TYPE_Receive ) ;

							pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;
                        }

                        // =========================================== // 
                        // Modified By Liujun at 20081230
                        // 更新本次收料相关PO所有的 收料记录中的汇率，并且对更新后单价相同项进行合并

//                        #region 更新汇率

                        

//                        DAEInStoreMaterialDetail dataEntity = new DAEInStoreMaterialDetail();

//                        string UpdateSql = @"UPDATE WH_Receive SET TotalPriceStandardlER = (SELECT TotalPriceStandardlER FROM WH_Receive WHERE ReceiveID = '" + sReceiveID + "') WHERE POID IN (SELECT POID FROM WH_Receive WHERE ReceiveID = '" + sReceiveID + "')";

//                        sErrorMsg = dataEntity.BaseDataAccess.ExecuteDMLSQL(UpdateSql);

//                        if (sErrorMsg.Length == 0)
//                        {
//                            UpdateSql = "UPDATE WH_Receive SET TotalPriceStandarCUR = TotalPriceStandardlER * TotalPrice WHERE POID IN (SELECT POID FROM WH_Receive WHERE ReceiveID = '" + sReceiveID + "')";

//                            sErrorMsg = dataEntity.BaseDataAccess.ExecuteDMLSQL(UpdateSql);
//                        }

//                        if (sErrorMsg.Length == 0)
//                        {
//                            UpdateSql = @"UPDATE WH_ReceiveMaterial SET UnitPriceStandard = WH_Receive.TotalPriceStandardlER * UnitPrice 
//                            FROM WH_ReceiveMaterial INNER JOIN WH_Receive ON WH_Receive.ReceiveID = WH_ReceiveMaterial.ReceiveID 
//                            WHERE POID IN (SELECT POID FROM WH_Receive WHERE ReceiveID = '" + sReceiveID + "')";

//                            sErrorMsg = dataEntity.BaseDataAccess.ExecuteDMLSQL(UpdateSql);
//                        }

//                        if (sErrorMsg.Length == 0)
//                        {
//                            UpdateSql = @"UPDATE WH_InStoreMaterialDetail SET WH_InStoreMaterialDetail.UnitPricePOStandard = WH_ReceiveMaterial.UnitPriceStandard
//                            FROM WH_InStoreMaterialDetail INNER JOIN WH_ReceiveMaterial ON WH_ReceiveMaterial.ItemCode = WH_InStoreMaterialDetail.ItemCode 
//                            INNER JOIN WH_Receive ON WH_Receive.ReceiveID = WH_ReceiveMaterial.ReceiveID AND WH_Receive.POID = WH_InStoreMaterialDetail.POID
//                            WHERE WH_Receive.POID IN (SELECT POID FROM WH_Receive WHERE ReceiveID = '" + sReceiveID + "')";

//                            sErrorMsg = dataEntity.BaseDataAccess.ExecuteDMLSQL(UpdateSql);
//                        }

//                        // ============================================================= //
//                        // Modified By Liujun at 20081228
//                        // 根据 Warehouse+ItemCode+PO+BinNo+UnitPrice 进行数量合并

//                        // 根据PO选出所有库存物资
//                        string SelectSql = "SELECT * FROM WH_InStoreMaterialDetail WHERE POID IN (SELECT POID FROM WH_Receive WHERE ReceiveID = '" + sReceiveID + "') ORDER BY ReceiveDate";

//                        DataTable dtPOInWarehouse = dataEntity.BaseDataAccess.GetDataTable(SelectSql);

//                        // 待删除列表 放置 InStockMaterialID
//                        System.Collections.ArrayList list = new System.Collections.ArrayList();

//                        for (int i = dtPOInWarehouse.Rows.Count - 1; i >= 0; i--)
//                        {
//                            for (int j = i - 1; j >= 0; j--)
//                            {
//                                if (dtPOInWarehouse.Rows[i]["WHID"].ToString() == dtPOInWarehouse.Rows[j]["WHID"].ToString()
//                                    && dtPOInWarehouse.Rows[i]["ItemCode"].ToString() == dtPOInWarehouse.Rows[j]["ItemCode"].ToString()
//                                    && dtPOInWarehouse.Rows[i]["POID"].ToString() == dtPOInWarehouse.Rows[j]["POID"].ToString()
//                                    && dtPOInWarehouse.Rows[i]["BinID"].ToString() == dtPOInWarehouse.Rows[j]["BinID"].ToString()
//                                    && dtPOInWarehouse.Rows[i]["UnitPricePOStandard"].ToString() == dtPOInWarehouse.Rows[j]["UnitPricePOStandard"].ToString())
//                                {
//                                    dtPOInWarehouse.Rows[j]["QuantityInBin"] = decimal.Parse(dtPOInWarehouse.Rows[i]["QuantityInBin"].ToString()) + decimal.Parse(dtPOInWarehouse.Rows[j]["QuantityInBin"].ToString());

//                                    // 分别对比较行与被比较行修改临时状态
//                                    dtPOInWarehouse.Rows[j]["Status"] = 1;

//                                    // 放入删除列表中
//                                    list.Add(dtPOInWarehouse.Rows[i]["InStockMaterialID"].ToString());

//                                    // 在内存数据表中删除
//                                    dtPOInWarehouse.Rows.RemoveAt(i);

//                                    break;
//                                }
//                            }
//                        }



//                        // 更新数据
//                        foreach (DataRow dr in dtPOInWarehouse.Rows)
//                        {
//                            if (dr["Status"].ToString() == "1")
//                            {
//                                UpdateSql = "UPDATE WH_InStoreMaterialDetail SET QuantityInBin = " + dr["QuantityInBin"].ToString() + " WHERE InStockMaterialID ='" + dr["InStockMaterialID"].ToString() + "'";

//                                sErrorMsg = dataEntity.BaseDataAccess.ExecuteDMLSQL(UpdateSql);
//                            }
//                        }


//                        // 删除数据(数量变为0)
//                        string DeleteSql = string.Empty;

//                        foreach (string str in list)
//                        {
//                            //DeleteSql = "UPDATE WH_InStoreMaterialDetail SET QuantityInBin = 0 WHERE InStockMaterialID ='" + str + "'";
//                            DeleteSql = "DELETE FROM WH_InStoreMaterialDetail WHERE InStockMaterialID ='" + str + "'";

//                            sErrorMsg = dataEntity.BaseDataAccess.ExecuteDMLSQL(DeleteSql);
//                        }

//                        #endregion

//                        // =========================================== //

//                        // =========================================== //
//                        // Modified By Liujun at 20030315
//                        // 更新本次收料相关PO的物资Remark。
//                        UpdateSql = @"UPDATE POMaterial
//
//                                    SET POMaterial.Remark = WH_ReceiveMaterial.Comment
//
//                                    FROM POMaterial INNER JOIN WH_Receive ON POMaterial.POID = WH_Receive.POID 
//                                    INNER JOIN WH_ReceiveMaterial ON WH_Receive.ReceiveID = WH_ReceiveMaterial.ReceiveID
//                                    AND WH_ReceiveMaterial.ItemCode = POMaterial.ItemCode 
//                                    WHERE WH_Receive.ReceiveID = '" + sReceiveID + "' AND WH_ReceiveMaterial.Comment <>  ''";
//                        sErrorMsg = dataEntity.BaseDataAccess.ExecuteDMLSQL(UpdateSql);

//                        // =========================================== // 

                        break;
					}
				}
			}

			return sErrorMsg;
		}

		#endregion

        public DataTable GetExcelData(string ReceiveID)
        {
            string strSql = "SELECT ItemCode,MaterialName,ReceiveDate,UOMID,POQuantity,FactReceivedQuantity,UnitPriceStandard,SumStandard,CurrencyID,UnitPrice,SumPrice,BinID,PoLine,POID  FROM v_Report_Receives WHERE ReceiveID = '" + ReceiveID + "' ORDER BY v_Report_Receives.POLine ";

            DataTable dt = this.BaseDataAccess.GetDataTable(strSql);

            return dt;
        }

        public DataTable GetReportData(string filter)
        {
            if (filter.Length != 0)
            {
                filter = "WHERE " + filter;
            }

            string strSql = "SELECT POID,ItemCode,MaterialName,ReceiveDate,UOMID,POQuantity,FactReceivedQuantity,UnitPriceStandard,SumStandard,CurrencyID,UnitPrice,SumPrice,BinID,PoLine,Name FROM v_Report_Receives " + filter + " ORDER BY v_Report_Receives.POLine ";

            DataTable dt = this.BaseDataAccess.GetDataTable(strSql);

            return dt;
        }
    }
}
