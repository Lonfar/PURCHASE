using System;
using System.Data;


namespace DataEntity
{
	/// <summary>
	/// DAEMaterialRequest 的摘要说明。
	/// </summary>
	public class DAEBIDEvaluation : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAEBIDEvaluation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetEnauiryPriceId(String bidEvaluationId)
		{
			String strSql = "Select EnquiryPriceId From MR_BIDEvaluation Where BIDEvaluationID='" + bidEvaluationId + "'";
			DataTable dtEnquiryPriceId = _da.GetDataTable(strSql);
			return dtEnquiryPriceId;
		}
		public DataTable GetQuotationPrice(String enquiryPriceId)
		{
			String strSql = "Select QuotationPriceID From MR_QuotationPrice Where EnquiryPriceID = '" + enquiryPriceId + "'" ; 
			DataTable dtQuotationPrice = _da.GetDataTable(strSql);
			return dtQuotationPrice;
		}
		public int GetMRBIDEvaluationState(string strPkValue,string UserID,string popedomNew)
		{
			String strSql="SELECT * FROM MR_BIDEvaluation WHERE charindex(','+convert(varchar,MR_BIDEvaluation.Status)+',',',"+popedomNew+",')>0 AND  MR_BIDEvaluation.BIDEvaluationID='"+strPkValue+"' AND MR_BIDEvaluation.CreateBy= '"+ UserID +"'";
			DataTable dtMREnquiryPriceState=_da.GetDataTable(strSql);
			return dtMREnquiryPriceState.Rows.Count;

		}

		/// <summary>
		/// 得到所有查看人信息
		/// </summary>
		/// <returns></returns>
		public DataTable GetSeePerson( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName], 
										BI_PositionType.PositionName as [BI_PositionType.PositionName], 
										'Edit' RowStatus, 
										MR_Viewer.ViewerID as [MR_Viewer.ViewerID], 
										MR_Viewer.ViewerName as [MR_Viewer.ViewerName], 
										MR_Viewer.ObjectiveID as [MR_Viewer.ObjectiveID],
										MR_Viewer.ObjectiveType as [MR_Viewer.ObjectiveType], 
										MR_Viewer.ViewerPosition as [MR_Viewer.ViewerPosition],
										BI_DepartmentEmployee.DepartmentID as [MR_Viewer.ViewerDept] 
						from  MR_Viewer,BI_Employee,BI_DepartmentEmployee,BI_PositionType 
						where MR_Viewer.ViewerName = BI_Employee.IDKey 
						and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID 
						and MR_Viewer.ViewerDept = BI_DepartmentEmployee.DepartmentID 
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey 
						and MR_Viewer.ObjectiveID = '"+pkValue+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}

		public void DelViewer(string strIDKey)
		{
			string strSql = "delete MR_Viewer where ViewerID ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}

		/// <summary>
		/// 根据BI_DepartmentEmployee.IDKey 得到BI_Employee.IDKey
		/// </summary>
		/// <returns></returns>
		public string GetBI_EmployeeID( string pkValue )
		{
			string strSql = "";
			strSql = @"select	BI_Employee.IDKey							  
						from 	BI_Employee, BI_DepartmentEmployee
						where 	BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						        and BI_DepartmentEmployee.IDKey = '"+pkValue+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			if(dt.Rows.Count > 0)
			{
				return dt.Rows[0]["IDKey"].ToString();
			}
			else return "";
		}

		/// <summary>
		/// 得到单个用户信息
		/// </summary>
		/// <param name="UserID">用户ID(暂时保存为部门员工的IDKey，以后可能会有变动)</param>
		/// <returns></returns>
		public DataTable GetSeePerson_User( string UserID )
		{
			string strSql = "";
			strSql = @"SELECT BI_Employee.FullName,                 
							  BI_PositionType.PositionName,
							  BI_Employee.IDKey,
							  BI_DepartmentEmployee.DepartmentID

						FROM  BI_Employee , 
							  BI_DepartmentEmployee,
							  BI_PositionType

						where BI_DepartmentEmployee.IDKey='"+UserID+"'";
			strSql += @"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// 得到指定部门的人员信息
		/// </summary>
		/// <param name="DepartmentID">部门ID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Department( string DepartmentID )
		{
			string strSql = "";
			strSql = @"select  BI_Employee.FullName,
							   BI_PositionType.PositionName,
							   BI_Employee.IDKey,
							   BI_DepartmentEmployee.DepartmentID

						from BI_Employee,
							 BI_Department,
							 BI_DepartmentEmployee,
							 BI_PositionType
						where BI_DepartmentEmployee.DepartmentID = '"+DepartmentID+"'";
			strSql += @"and BI_Department.IDKey = BI_DepartmentEmployee.DepartmentID
					   and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
					   and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						";

			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// 得到指定组的人员信息
		/// </summary>
		/// <param name="UserID">GroupID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Group( string GroupID )
		{
			string strSql = "";
			strSql = @"select  
							BI_Employee.FullName,
							BI_PositionType.PositionName,
  							BI_Employee.IDKey,
							BI_DepartmentEmployee.DepartmentID

						from TI_DefaultGroupMember,
							 BI_Employee,
							 BI_PositionType,
							 BI_DepartmentEmployee

						where TI_DefaultGroupMember.IDKEY = '"+GroupID+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		
		public  void GetVendorList(DataTable dtTemp,string enquiryPriceID)
		{
			
			string strSQL = "SELECT MR_QuotationPrice.VendorNo,MR_QuotationPrice.VendorName,MR_QuotationPrice.CurrencyID , MR_QuotationPrice.TotalPriceNoTax,MR_QuotationPrice.TotalPriceTax,MR_QuotationPrice.ETA,MR_QuotationPrice.PaymentTypeID , MR_QuotationPrice.Comment , Vendor.IDKey as VendorID  FROM MR_QuotationPrice Inner Join Vendor ON Vendor.VendorNo = MR_QuotationPrice.VendorNO Where MR_QuotationPrice.EnquiryPriceID='" + enquiryPriceID + "' And MR_QuotationPrice.Status = " + (int)MRState.State_QuoFinished ;
			DataTable dt = _da.GetDataTable(strSQL);
			dtTemp.Rows.Clear();
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
				DataRow dr = dtTemp.NewRow();
				dr["BIDSummaryID"] = System.Guid.NewGuid().ToString();
				dr["VendorNo"] = dt.Rows[i]["VendorNo"] == DBNull.Value ? "" : dt.Rows[i]["VendorNo"].ToString();
				dr["VendorName"]  = dt.Rows[i]["VendorName"] == DBNull.Value  ? "" : dt.Rows[i]["VendorName"].ToString();
				dr["CurrencyID"] =  dt.Rows[i]["CurrencyID"] == DBNull.Value ? "" : dt.Rows[i]["CurrencyID"].ToString();
				dr["MR_BIDSummary__CurrencyID"] =  dt.Rows[i]["CurrencyID"] == DBNull.Value ? "" : dt.Rows[i]["CurrencyID"].ToString();
				//dr["TotalPriceNoTax"] = Convert.ToDecimal(dt.Rows[i]["TotalPriceNoTax"] == DBNull.Value ? "0" : dt.Rows[i]["TotalPriceNoTax"].ToString());
				
				dr["TotalPriceTax"] = Convert.ToDecimal(dt.Rows[i]["TotalPriceTax"] == DBNull.Value ? "0" : dt.Rows[i]["TotalPriceTax"].ToString());
				
				if(dt.Rows[i]["ETA"] == DBNull.Value)
				{
					dr["ETA"] = DBNull.Value ; 
				}
				else
				{
					dr["ETA"] = Convert.ToDateTime(dt.Rows[i]["ETA"].ToString());
				}
				
				dr["PamentTypeID"] = dt.Rows[i]["PaymentTypeID"] == DBNull.Value ? "" : dt.Rows[i]["PaymentTypeID"].ToString();
				
				dr["Comment"] = dt.Rows[i]["PaymentTypeID"] == DBNull.Value ? "" : dt.Rows[i]["Comment"].ToString();
				dr["VendorID"] = dt.Rows[i]["VendorID"] == DBNull.Value ? "" : dt.Rows[i]["VendorID"].ToString();

				dr["RowStatus"] = "NEW" ;
				dtTemp.Rows.Add(dr);
			}

		}
		public  DataTable GetEnquiryPrice(string enquiryPriceID)
		{
			string strSQL = "SELECT MR_EnquiryPrice.EnquiryPriceDate,MR_EnquiryPrice.CreateBy FROM MR_EnquiryPrice Where MR_EnquiryPrice.EnquiryPriceID='" + enquiryPriceID + "'";
			DataTable dt = _da.GetDataTable(strSQL);
			return dt;
		}

		#region 获得报表数据BidByVendor

		/// <summary>
		/// 获得报表数据BidByVendor
		/// </summary>
		/// <param name="sEnquiryID">询价ID</param>
		/// <returns></returns>
		public DataTable GetPrintData_Vendor( string sEnquiryID )
		{
			string sSelectSql = "SELECT * FROM v_Report_BidByVendor WHERE EnquiryPriceID = '"+sEnquiryID+"' AND Status >= "+(int)MRState.State_QuoFinished+"";
			string sErrorMsg = string.Empty;

			DataTable dtData = this.BaseDataAccess.GetDataTable ( sSelectSql );

			HandleDataTable ( dtData );

			return dtData;
		}

		public void HandleDataTable(DataTable dtData)
		{
			DataTable dtTempEnum = dtData.Copy();
			foreach ( DataRow drEnquiryMaterial in dtTempEnum.Rows )
			{
				AddMergerRow( dtData,drEnquiryMaterial);
			}	

			for ( int i = dtData.Rows.Count-1;i >= 0 ;i-- )
			{
				if ( dtData.Rows[i]["RowAttribute"].ToString() == "Hide" )
				{
					dtData.Rows.RemoveAt(i);
				}
			}
		}

		public void AddMergerRow(DataTable dtEnquiryMaterial,DataRow drSearch)
		{			
			decimal decMRQuantity = System.Convert.ToDecimal(drSearch["MRQuantity"].ToString()); ;
			//string MRNO =drSearch["MRNO"].ToString();
			bool hasMerger = false ;
			decimal decTotalTax = Convert.ToDecimal(drSearch["TotalTax"].ToString());
			
			foreach(DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows)
			{									
				if( 
					drEnquiryMaterial["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //保证是同种物资
					drEnquiryMaterial["VendorNo"].ToString() == drSearch["VendorNo"].ToString() &&
					drEnquiryMaterial["ID"].ToString() != drSearch["ID"].ToString() && //保证不是同一行
					drEnquiryMaterial["RowAttribute"].ToString() == "Ordinary" //保证是没有合并过的
					)
				{
					hasMerger = true ;
					decMRQuantity += System.Convert.ToDecimal(drEnquiryMaterial["MRQuantity"].ToString());
					decTotalTax += Convert.ToDecimal (drEnquiryMaterial["TotalTax"].ToString()); 
					

					//MRNO += "," + drEnquiryMaterial["MRNO"].ToString();
					drEnquiryMaterial["RowAttribute"] = "Hide";
				}
			}
			if(hasMerger == true)
			{
				UpdateRowStatus(dtEnquiryMaterial,drSearch);
				DataRow dr = dtEnquiryMaterial.NewRow();
				dr["ID"] = System.Guid.NewGuid().ToString() ; 
				//dr["MRNO"] = MRNO.Trim(",".ToCharArray());
				dr["VendorNo"] = drSearch["VendorNo"];
				dr["VendorName"] = drSearch["VendorName"];
				dr["ItemCode"] = drSearch["ItemCode"];
				dr["MaterialName"] = drSearch["MaterialName"];
				dr["UOMID"] = drSearch["UOMID"];
				dr["MRQuantity"] = decMRQuantity;
				//				dr["UnitPriceNoTax"] = drSearch["UnitPriceNoTax"];
				dr["TotalNoTax"] = drSearch["TotalNoTax"];
				dr["UnitPriceTax"] = drSearch["UnitPriceTax"];
				
				dr["TotalTax"] = decTotalTax;
				//				dr["TotalTax"] = drSearch["TotalTax"];
				dr["TotalNoTax1"] = drSearch["TotalNoTax1"];
				dr["TotalTax2"] = drSearch["TotalTax2"];
				dr["Status"] = drSearch["Status"];				
				dr["RowAttribute"] ="Merger";
				dtEnquiryMaterial.Rows.Add(dr);
			}			
		}

		public void UpdateRowStatus(DataTable dtEnquiryMaterial,DataRow drSearch)
		{
			foreach(DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows)
			{
				if( 
					drEnquiryMaterial["ID"].ToString() == drSearch["ID"].ToString() && //保证不是同一行
					drEnquiryMaterial["RowAttribute"].ToString() == "Ordinary" //保证是没有合并过的
					)
				{
					drEnquiryMaterial["RowAttribute"] = "Hide";
					break;
				}
			}	
		}

		#endregion

		// Add by ZZH on 2008-1-18 添加验证是否可以删除的方法
		public DataTable CheckState(String strMREnquiryPrice )
		{
			String strSql = " Select Max(PurchaseOrder.ApproveStatus) as CheckState From MR_EnquiryMaterial Left Join POMaterial On MR_EnquiryMaterial.MRMaterialID = POMaterial.MRMaterialID " +
				" Left Join PurchaseOrder On PurchaseOrder.POID = POMaterial.POID  Where MR_EnquiryMaterial.EnquiryPriceID = '" + strMREnquiryPrice + "'" ;
			DataTable dt = this.BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select Max(MR_Material.Status) as State From MR_BIDEvaluation Inner Join MR_EnquiryPrice On MR_BIDEvaluation.EnquiryPriceID = MR_EnquiryPrice.EnquiryPriceID Inner Join MR_EnquiryMaterial On MR_EnquiryPrice.EnquiryPriceID=MR_EnquiryMaterial.EnquiryPriceID Inner Join MR_Material On MR_EnquiryMaterial.MRMaterialID=MR_Material.MRMaterialID Where MR_BIDEvaluation.BIDEvaluationID='" + strPKValue + "'" ;
			DataTable dt = BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}
		//******************************************************
	}
}
