using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
    /// DAESDT
    /// </summary>
	public class DAESDT : DAEBase
	{
        Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
        DAEPurchaseOrderEdit daePOEdit = new DAEPurchaseOrderEdit();
        DAEEnquiryPrice daeEnquiryPrice = new DAEEnquiryPrice();
        

        public DAESDT()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        public string InsertLogistics(DataTable dt)
        {
            string strSql = @"Insert into MR_SDT_Logistics_Tracking (IDKey,SDTNO,Recvby,RecvDate,PONO,TotalValue,SOContractNo,ServerSupplier)
                            Values('" + System.Guid.NewGuid().ToString() + "','" + dt.Rows[0]["MR_SDT_Logistics.SDTNo"].ToString() + "','" + dt.Rows[0]["MR_SDT_Logistics.receivedBy"].ToString() + "','" + Convert.ToDateTime(dt.Rows[0]["MR_SDT_Logistics.receivedDate"]).ToString("yyyy-MM-dd") + "','" + dt.Rows[0]["MR_SDT_Logistics.POID"].ToString()
                                      + "'," + Convert.ToDecimal(dt.Rows[0]["MR_SDT_Logistics.TotalValueStand"]).ToString("f8") + ",'" + dt.Rows[0]["MR_SDT_Logistics.SOContractNo"].ToString() + "','" + dt.Rows[0]["MR_SDT_Logistics.VendorID"].ToString() + "')";
            return _da.ExecuteDMLSQL(strSql);
        }

        public DataTable GetPOInformation(string strPKValue)
        {
            string strSelect = "SELECT * From MR_SDT_PO WHERE SDTID = '" + strPKValue + "' Order by OrderID";
            return this._da.GetDataTable(strSelect);
        }

        public DataTable GetPOMaterialInformation(string strPKValue)
        {
            string strSelect = "SELECT * From MR_SDT_Material WHERE SDTID = '" + strPKValue + "' Order by OrderID";
            return this._da.GetDataTable(strSelect);
        }

        private DataTable EmployeeInfo(string strPKvalue)
        {
            string strSelect = "SELECT * From BI_Employee WHERE IDKey = '" + strPKvalue + "'";
            return this._da.GetDataTable(strSelect);
        }


        public DataTable InsertPOInformation(DataTable dtPOList,DataTable dtPOInfo)
        {
            foreach (DataRow drPOInfo in dtPOInfo.Rows)
            {
                DataRow drPOList = dtPOList.NewRow();
                drPOList["BuyerName"] = drPOInfo["BuyerName"];
                drPOList["MR_SDT_PO_Logistics__BuyerName"] = this.EmployeeInfo(drPOInfo["BuyerName"].ToString()).Rows[0]["FullName"];
                drPOList["ContractTotalCost"] = drPOInfo["ContractTotalCost"];
                drPOList["ContractTotalCostCUR"] = drPOInfo["ContractTotalCostCUR"];
                drPOList["MR_SDT_PO_Logistics__ContractTotalCostCUR"] = drPOInfo["ContractTotalCostCUR"];
                drPOList["ContractTotalCostStandard"] = drPOInfo["ContractTotalCostStandard"];
                drPOList["ContractTotalCostStandardER"] = drPOInfo["ContractTotalCostStandardER"];
                drPOList["ETA"] = drPOInfo["ETA"];
                drPOList["MR_SDT_POIDKey"] = drPOInfo["IDKey"];
                drPOList["OrderID"] = drPOInfo["OrderID"];
                drPOList["POID"] = drPOInfo["POID"];
                drPOList["MR_SDT_PO_Logistics__POID"] = drPOInfo["POID"];
                drPOList["DescCol"] = drPOInfo["POID"];
                drPOList["IDKey"] = System.Guid.NewGuid().ToString();
                drPOList["VendorID"] = drPOInfo["VendorID"];
                drPOList["MR_SDT_PO_Logistics__VendorID"] = daeEnquiryPrice.GetEnquiryVendor(drPOInfo["VendorID"].ToString()).Rows[0]["Name"].ToString();


                drPOList["RowStatus"] = "NEW";
                drPOList["RowAttribute"] = "Ordinary";

                dtPOList.Rows.Add(drPOList);
            }

            return dtPOList;
        }

        public DataTable InsertPOMaterialInformation(DataTable dtMaterialList, DataTable dtMaterialInfo)
        {
            foreach (DataRow drMaterialInfo in dtMaterialInfo.Rows)
            {
                DataRow drMaterialList = dtMaterialList.NewRow();
                drMaterialList["Description"] = drMaterialInfo["Description"];
                drMaterialList["ItemCode"] = drMaterialInfo["ItemCode"];
                drMaterialList["POQuantity"] = drMaterialInfo["POQuantity"];
                drMaterialList["ActualReceiveQty"] = drMaterialInfo["ActualReceiveQty"];
                drMaterialList["ReceivedQty"] = drMaterialInfo["ReceivedQty"];
                drMaterialList["CanReceiveQty"] = drMaterialInfo["CanReceiveQty"];
                drMaterialList["MaterialUOMID"] = drMaterialInfo["MaterialUOMID"];
                drMaterialList["MR_SDT_Material_Logistics__MaterialUOMID"] = daePOEdit.GetMaterialUOM(drMaterialInfo["MaterialUOMID"].ToString()).Rows[0]["UOMID"].ToString();
                drMaterialList["POLine"] = drMaterialInfo["POLine"];
                drMaterialList["POID"] = drMaterialInfo["POID"];
                drMaterialList["MR_SDT_Material_MaterialID"] = drMaterialInfo["MaterialID"];
                drMaterialList["MaterialID"] = System.Guid.NewGuid().ToString();
                drMaterialList["UnitPrice"] = drMaterialInfo["UnitPrice"];
                drMaterialList["OrderID"] = drMaterialInfo["OrderID"];
                drMaterialList["POMaterialID"] = drMaterialInfo["POMaterialID"];
                drMaterialList["TotalCost"] = drMaterialInfo["TotalCost"];
                drMaterialList["UnitPriceStandard"] = drMaterialInfo["UnitPriceStandard"];
                drMaterialList["TotalCostStandard"] = drMaterialInfo["TotalCostStandard"];


                drMaterialList["RowStatus"] = "NEW";
                drMaterialList["RowAttribute"] = "Ordinary";

                dtMaterialList.Rows.Add(drMaterialList);
            }

            return dtMaterialList;
        }


        public DataTable GetPOMaterialInfo(DataTable dtPO, DataTable dtMaterial)
        {
            string strSelect = "";
            DataTable dtPOMaterial;

            if (dtPO.Rows.Count == 0)
            {
                for (int i = 0; i < dtMaterial.Rows.Count; i++)
                {
                    if (dtMaterial.Rows[i].RowState != DataRowState.Deleted)
                    {
                        dtMaterial.Rows[i].Delete();
                    }
                }
            }

            System.Collections.ArrayList strPOIDList = new System.Collections.ArrayList();

            foreach (DataRow dr in dtPO.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (!strPOIDList.Contains(dr["POID"].ToString()))
                    {
                        strPOIDList.Add(dr["POID"].ToString());
                    }
                }
            }

           
            bool Flag = false;

            foreach (DataRow dr in dtPO.Rows)
            {
                if (dr.RowState != DataRowState.Deleted && dr["RowStatus"].ToString() == "NEW")
                {
                    strSelect = "SELECT POLine,POMaterial.ItemCode,POMaterial.POMaterialID,POMaterial.POID,POMaterial.MaterialDescription,POQuantity,UnitPrice,ISNULL(SDTQuantity,0) AS ReceivedQty,(POQuantity-ISNULL(SDTQuantity,0)) As AvailableQty,POMaterial.MaterialUOMID,MaterialUOM.UOMID FROM POMaterial LEFT JOIN MaterialUOM ON MaterialUOM.MaterialUOMID = POMaterial.MaterialUOMID WHERE POID = '" + dr["POID"] + "' And POQuantity-ISNULL(SDTQuantity,0) >0 order by POLine asc";
                    dtPOMaterial = this._da.GetDataTable(strSelect);

                    
                    for (int i = 0; i < dtPOMaterial.Rows.Count; i++)
                    {
                        Flag = false;

                        foreach (DataRow drM in dtMaterial.Rows)
                        {
                            if (drM.RowState != DataRowState.Deleted)
                            {
                                if (dtPOMaterial.Rows[i]["POID"].ToString() == drM["POID"].ToString() && dtPOMaterial.Rows[i]["ItemCode"].ToString() == drM["ItemCode"].ToString())
                                {
                                    Flag = true;
                                }
                            }
                        }


                        if (Flag == false)
                        {

                            DataRow drFromMaterial = dtMaterial.NewRow();
                            drFromMaterial["Description"] = dtPOMaterial.Rows[i]["MaterialDescription"];
                            drFromMaterial["ItemCode"] = dtPOMaterial.Rows[i]["ItemCode"];
                            drFromMaterial["POQuantity"] = dtPOMaterial.Rows[i]["POQuantity"];
                            drFromMaterial["ActualReceiveQty"] = dtPOMaterial.Rows[i]["AvailableQty"];
                            drFromMaterial["ReceivedQty"] = dtPOMaterial.Rows[i]["ReceivedQty"];
                            drFromMaterial["CanReceiveQty"] = dtPOMaterial.Rows[i]["AvailableQty"];
                            drFromMaterial["MaterialUOMID"] = dtPOMaterial.Rows[i]["MaterialUOMID"];
                            drFromMaterial["MR_SDT_Material__MaterialUOMID"] = dtPOMaterial.Rows[i]["UOMID"];
                            drFromMaterial["POLine"] = dtPOMaterial.Rows[i]["POLine"];
                            drFromMaterial["POID"] = dr["POID"];
                            drFromMaterial["MaterialID"] = System.Guid.NewGuid().ToString();
                            drFromMaterial["UnitPrice"] = dtPOMaterial.Rows[i]["UnitPrice"];
                            drFromMaterial["POMaterialID"] = dtPOMaterial.Rows[i]["POMaterialID"];



                            drFromMaterial["RowStatus"] = "NEW";
                            drFromMaterial["RowAttribute"] = "Ordinary";

                            dtMaterial.Rows.Add(drFromMaterial);

                        }

                    }
                }

                else if (dr.RowState == DataRowState.Deleted)
                {

                    foreach (DataRow drMaterial in dtMaterial.Rows)
                    {
                        string strPOID = dr["POID", System.Data.DataRowVersion.Original].ToString();
                        string strPOMaterial = "";
                        if (drMaterial.RowState != DataRowState.Deleted)
                        {
                            strPOMaterial = drMaterial["POID"].ToString();

                            if (strPOMaterial == strPOID)
                            {
                                drMaterial.Delete();
                            }
                        }
                    }

                }
            }

            DataTable dtMCopy = dtMaterial.Copy();

            for (int i = 0; i < dtMaterial.Rows.Count; i++)
            {

                if (dtMaterial.Rows[i].RowState != DataRowState.Deleted && !strPOIDList.Contains(dtMaterial.Rows[i]["POID"].ToString()))
                {
                    for (int j = 0; j < dtMCopy.Rows.Count;j++ )
                    {
                        if (dtMCopy.Rows[j].RowState != DataRowState.Deleted && dtMCopy.Rows[j]["POID"].ToString() == dtMaterial.Rows[i]["POID"].ToString() && dtMCopy.Rows[j]["ItemCode"].ToString() == dtMaterial.Rows[i]["ItemCode"].ToString())
                        {
                            dtMCopy.Rows[j].Delete();
                        }
                    }
                }

            }


            dtMaterial = dtMCopy.Copy();

            return dtMaterial;
        }


        public DataTable GetMaterialInfo(DataTable dtMaterial)
        {
            string strSelect = "";
            DataTable dtPOMaterial;

            foreach (DataRow dr in dtMaterial.Rows)
            {
                if (dr.RowState != DataRowState.Deleted && dr["RowStatus"].ToString() == "NEW" && dr["POMaterialID"].ToString().Length >0)
                {
                    strSelect = "SELECT POLine,POID,POMaterial.ItemCode,POMaterial.MaterialDescription,POQuantity,UnitPrice,ISNULL(SDTQuantity,0) AS ReceivedQty,(POQuantity-ISNULL(SDTQuantity,0)) As AvailableQty,POMaterial.MaterialUOMID,MaterialUOM.UOMID FROM POMaterial LEFT JOIN MaterialUOM ON MaterialUOM.MaterialUOMID = POMaterial.MaterialUOMID WHERE POMaterialID = '" + dr["POMaterialID"] + "' And POQuantity-ISNULL(SDTQuantity,0) >0";
                    dtPOMaterial = this._da.GetDataTable(strSelect);

                    dr["Description"] = dtPOMaterial.Rows[0]["MaterialDescription"];
                    dr["ItemCode"] = dtPOMaterial.Rows[0]["ItemCode"];
                    dr["POQuantity"] = dtPOMaterial.Rows[0]["POQuantity"];
                    dr["ActualReceiveQty"] = dtPOMaterial.Rows[0]["AvailableQty"];
                    dr["ReceivedQty"] = dtPOMaterial.Rows[0]["ReceivedQty"];
                    dr["CanReceiveQty"] = dtPOMaterial.Rows[0]["AvailableQty"];
                    dr["MaterialUOMID"] = dtPOMaterial.Rows[0]["MaterialUOMID"];
                    dr["MR_SDT_Material__MaterialUOMID"] = dtPOMaterial.Rows[0]["UOMID"];
                    dr["POLine"] = dtPOMaterial.Rows[0]["POLine"];
                    dr["POID"] = dtPOMaterial.Rows[0]["POID"];
                    //dr["MaterialID"] = System.Guid.NewGuid().ToString();
                    dr["UnitPrice"] = dtPOMaterial.Rows[0]["UnitPrice"];

                }

            }

            return dtMaterial;
        }


        public DataTable GetPOInfo(DataTable dtPO)
        {

            foreach (DataRow dr in dtPO.Rows)
            {
                if (dr.RowState != DataRowState.Deleted && dr["RowStatus"].ToString() == "NEW")
                {
                    string strSelect = "SELECT *,BI_Employee.FullName,CON_BidderList.Name From PurchaseOrder left outer join CON_BidderList on CON_BidderList.BidderID = PurchaseOrder.VendorID left outer join BI_Currency on BI_Currency.IDKey = PurchaseOrder.ContractTotalCostCUR left outer join BI_Employee on BI_Employee.IDKey = PurchaseOrder.EmployeeID WHERE POID = '" + dr["POID"] + "'";
                    DataTable dtPOInfo = this._da.GetDataTable(strSelect);

                    dr["ContractTotalCostStandardER"] = dtPOInfo.Rows[0]["ContractTotalCostStandardER"];
                    dr["BuyerName"] = dtPOInfo.Rows[0]["EmployeeID"];
                    dr["ETA"] = dtPOInfo.Rows[0]["EstiamteArrivalDate"];
                    dr["MR_SDT_PO__POID"] = dtPOInfo.Rows[0]["POID"];
                    dr["MR_SDT_PO__BuyerName"] = dtPOInfo.Rows[0]["FullName"];
                    dr["ContractTotalCostCUR"] = dtPOInfo.Rows[0]["ContractTotalCostCUR"];
                    dr["MR_SDT_PO__ContractTotalCostCUR"] = dtPOInfo.Rows[0]["ContractTotalCostCUR"];
                    dr["VendorID"] = dtPOInfo.Rows[0]["VendorID"];
                    dr["MR_SDT_PO__VendorID"] = dtPOInfo.Rows[0]["Name"];
                }

            }

            return dtPO;
        }


        public DataTable GetStatusInfo(DataTable dt)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted && dr["RowStatus"].ToString() == "NEW")
                {
                    string strStatus = "SELECT * From BI_Logistics_TrackingStatus WHERE IDKey = " + Convert.ToInt32(dr["Status"]) ;
                    DataTable dtStatus = this._da.GetDataTable(strStatus);

                    dr["Status"] = dtStatus.Rows[0]["IDKey"];
                    dr["MR_SDT_Logistics_TrackingStatus__Status"] = dtStatus.Rows[0]["Description"];
                }

            }

            return dt;
        }


        /// <summary>
        /// 更新PO物资表中的SDT数量
        /// </summary>
        /// <param name="dtMaterial"></param>
        public void UpdateQuantity(DataTable dtMaterial)
        {
            string strSelect = string.Empty;
            DataTable dtTemp;

            foreach (DataRow dr in dtMaterial.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    strSelect = "SELECT SDTID,POMaterialID,SUM(ActualReceiveQty) AS ReceivedQty FROM MR_SDT_Material GROUP BY SDTID,POMaterialID HAVING SDTID <> '" + dr["MR_SDT_Material.SDTID"].ToString() + "' AND POMaterialID = '" + dr["MR_SDT_Material.POMaterialID"].ToString() + "'";

                    dtTemp = this._da.GetDataTable(strSelect);

                    if (dtTemp.Rows.Count > 0)
                    {
                        string strUpdate = string.Empty;

                        // 以前收过
                        if (!string.IsNullOrEmpty(dtTemp.Rows[0]["ReceivedQty"].ToString()))
                        {
                            decimal Qty = Convert.ToDecimal(dr["MR_SDT_Material.ActualReceiveQty"].ToString()) + Convert.ToDecimal(dtTemp.Rows[0]["ReceivedQty"].ToString());
                            strUpdate = "UPDATE POMaterial SET SDTQuantity = " + Qty.ToString() + " WHERE POMaterialID = '" + dr["MR_SDT_Material.POMaterialID"].ToString() + "'";

                            _da.ExecuteDMLSQL(strUpdate);
                        }
                    }
                    else
                    {
                        string strUpdate = string.Empty;

                        // 以前没收过
                        decimal Qty = Convert.ToDecimal(dr["MR_SDT_Material.ActualReceiveQty"].ToString());
                        strUpdate = "UPDATE POMaterial SET SDTQuantity = " + Qty.ToString() + " WHERE POMaterialID = '" + dr["MR_SDT_Material.POMaterialID"].ToString() + "'";

                        _da.ExecuteDMLSQL(strUpdate);
                    }
                }
                else if (dr.RowState == DataRowState.Deleted)//如果删除，需要修改SDTQuantity
                {
                    dr.RejectChanges();
                    if (dr["RowStatus"].ToString() == "EDIT")
                    {
                        //strSelect = "SELECT SDTID,POMaterialID,SUM(ActualReceiveQty) AS ReceivedQty FROM MR_SDT_Material GROUP BY SDTID,POMaterialID HAVING SDTID <> '" + dr["MR_SDT_Material.SDTID"].ToString() + "' AND POMaterialID = '" + dr["MR_SDT_Material.POMaterialID"].ToString() + "'";

                        //dtTemp = this._da.GetDataTable(strSelect);

                        //if (dtTemp.Rows.Count > 0)
                        //{
                        //    string strUpdate = string.Empty;

                            // 以前收过
                            //if (!string.IsNullOrEmpty(dtTemp.Rows[0]["ReceivedQty"].ToString()))
                            //{
                        decimal Qty = Convert.ToDecimal(dr["MR_SDT_Material.ActualReceiveQty"].ToString());
                        string strUpdate = "UPDATE POMaterial SET SDTQuantity = SDTQuantity - " + Qty.ToString() + " WHERE POMaterialID = '" + dr["MR_SDT_Material.POMaterialID"].ToString() + "'";

                        _da.ExecuteDMLSQL(strUpdate);
                            //}
                        //}
                        //else
                        //{
                        //    string strUpdate = string.Empty;

                        //    // 彻底删除
                        //    strUpdate = "UPDATE POMaterial SET SDTQuantity = 0 WHERE POMaterialID = '" + dr["MR_SDT_Material.POMaterialID"].ToString() + "'";

                        //    _da.ExecuteDMLSQL(strUpdate);
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// 在删除时，将POMaterial的数量相应减去
        /// </summary>
        /// <param name="strSDTID"></param>
        public void DeleteQuantity(string strSDTID)
        {
            string strSelect = @"UPDATE POMaterial SET SDTQuantity = SDTQuantity - a.ActualReceiveQty FROM POMaterial INNER JOIN
                            (SELECT SDTID,POMaterialID,ActualReceiveQty FROM MR_SDT_Material WHERE SDTID = '"+strSDTID+"') a ON a.POMaterialID = POMaterial.POMaterialID";

            _da.ExecuteDMLSQL(strSelect);
        }

        public DataTable GetMR_SDTInfo(string strPKValue)
        {
            string strSql = "Select * from MR_SDT Where SDTIDKey='" + strPKValue + "'";
            return _da.GetDataTable(strSql);
        }

        /// <summary>
        /// 获得报表打印数据
        /// </summary>
        /// <param name="strSDTID"></param>
        /// <returns></returns>
        public DataTable GetPrintData(string strSDTID)
        {
            string strSelect = "SELECT * FROM v_Report_SDT WHERE SDTIDKey = '"+strSDTID+"'";

            return _da.GetDataTable(strSelect);
        }

        public DataTable GetItem(string strItemCode)
        {
            string strSelect = @"select Material.ItemCode,Material.MaterialName,MaterialUOM.MaterialUomID,MaterialUOM.UOMID
									from  Material, MaterialUOM
									where MaterialUOM.ItemCode = Material.ItemCode and  MaterialUOM.UOMID = Material.UOMID
								    and Material.ItemCode = '" + strItemCode + "'";

            return _da.GetDataTable(strSelect);
        }

        /// <summary>
        /// 获得MRNO
        /// </summary>
        /// <param name="POMaterialID"></param>
        /// <returns></returns>
        public string GetMRNOData(string strPOMaterialID)
        {
            string strSelect = "SELECT MRNO FROM POMaterial WHERE POMaterialID = '" + strPOMaterialID + "'";
            if (_da.GetDataTable(strSelect).Rows.Count > 0)
            {
                return _da.GetDataTable(strSelect).Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public string GetMRNO(string strSDTID)
        {
            string strMRNO = string.Empty;
            string strSQL = "SELECT POID FROM MR_SDT WHERE SDTIDKey='" + strSDTID + "'";
            DataTable dtPOID = _da.GetDataTable(strSQL);
            if (dtPOID.Rows.Count > 0)
            {
                if (dtPOID.Rows[0]["POID"].ToString().Length > 0)
                {
                    string strSelect = "SELECT POMaterial.MRNO FROM MR_SDT_Material,POMaterial WHERE MR_SDT_Material.ItemCode = POMaterial.ItemCode AND MR_SDT_Material.SDTID = '" + strSDTID + "'  AND POMaterial.POID='" + dtPOID.Rows[0]["POID"].ToString() + "' GROUP BY POMaterial.MRNO";
                    DataTable dt = _da.GetDataTable(strSelect);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            strMRNO = dt.Rows[i]["MRNO"].ToString();
                        }
                        else
                        {
                            strMRNO = strMRNO + "," + dt.Rows[i]["MRNO"].ToString();
                        }
                    }
                }
            }
            
            return strMRNO;
        }
    }
}
