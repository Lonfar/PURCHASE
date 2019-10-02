using System;
using System.Data;


namespace DataEntity
{
	/// <summary>
	/// DAEMaterialRequest ��ժҪ˵����
	/// </summary>
	public class DAEEnquiryPrice : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAEEnquiryPrice()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public bool DealingWithQuotations(String enquiryPriceId)
		{
			string[] sParams = {"enquiryPriceID"} ;
			object[] objParamValues = {enquiryPriceId} ; 
			SqlDbType[] paramTypes = { SqlDbType.VarChar} ;
			return _da.ExecuteSP("spDealingWithQuotations",sParams,objParamValues,paramTypes) ; 
		}
		public int GetMREnquiryPriceState(string strPkValue,string UserID,string popedomNew)
		{
			String strSql="SELECT * FROM MR_EnquiryPrice WHERE charindex(','+convert(varchar,MR_EnquiryPrice.Status)+',',',"+popedomNew+",')>0 AND  MR_EnquiryPrice.EnquiryPriceID='"+strPkValue+"' AND MR_EnquiryPrice.CreateBy= '"+ UserID +"'";
			DataTable dtMREnquiryPriceState=_da.GetDataTable(strSql);
			return dtMREnquiryPriceState.Rows.Count;

		}
		public DataTable GetEnquiryVendor(String vendorId)
		{
			string sSql = @"SELECT Vendor.* From Vendor WHERE Vendor.IDKey = '" + vendorId + "'";
			DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
			return dtTempInfo;
		}
		public DataTable GetEnauiryMaterial(String mRMaterialID , bool bolValue )
		{
			DataTable materialTable = new DataTable();
			if(bolValue)
			{
				String strSql = "Select MRNO , ItemCode From MR_EnquiryMaterial Where EnquiryPriceID='" + mRMaterialID + "'";
				materialTable = BaseDataAccess.GetDataTable(strSql);
				
			}
			return materialTable ;
		
		}
		public DataTable GetEnquiryMaterial(String mRMaterialID)
		{
			string sSql = @"SELECT MR_MaterialRequisition.MRNO,
						MR_Material.ItemCode,
						MR_Material.MaterialName,
						MR_Material.ProductStandard,
						MR_Material.MFG,
						MR_Material.PartNO,
						MR_Material.MaterialUomID,
						MaterialUOM.UOMID,
						MRQuantity - isnull(QuantityInEnquiry,0) as CanEnquiryQuantity
						 From MR_Material join MR_MaterialRequisition on MR_Material.MRID = MR_MaterialRequisition.MRID
						join MaterialUOM on MaterialUOM.ItemCode = MR_Material.ItemCode and IsBaseUOM = 1
						WHERE MR_Material.MRMaterialID = '" + mRMaterialID + "'";
			DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
			return dtTempInfo;
		}
//		public void UpdateMaterialList(DataTable dtMaterialList)
//		{
//			foreach ( DataRow drMaterial in dtMaterialList.Rows )
//			{
//				if(drMaterial.RowState != DataRowState.Deleted)
//				{
//					string sSql = @"select MR_Material.ItemCode,MR_Material.MaterialName,MR_Material.ProductStandard,MR_Material.MRQuantity,
//									MaterialUOM.UOMID,MR_Material.EstUnitPrice,MR_Material.Comment 
//									from  MR_Material,MaterialUOM 
//									where MaterialUOM.MaterialUomID = MR_Material.MaterialUomID
//								    and MR_Material.MRID = '"+drMaterial["MRID"].ToString()+"'";
//					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
//					if (dtTempInfo.Rows.Count > 0 )
//					{					
//						//���ʱ���	
//						drMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ; 
//						//��������
//						drMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ; 
//						//��Ʒ���
//						drMaterial["ProductStandard"] = dtTempInfo.Rows[0]["ProductStandard"] ;
//						//����	
//						drMaterial["MRQuantity"] = dtTempInfo.Rows[0]["MRQuantity"] ;
//						//���Ƶ���
//						drMaterial["EstUnitPrice"] = dtTempInfo.Rows[0]["EstUnitPrice"] ;
//						//��ע	
//						drMaterial["Comment"] = dtTempInfo.Rows[0]["Comment"] ;
//						//��λ
//						drMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["UomID"] ;
//						drMaterial["MR_Material__MaterialUomID"] = dtTempInfo.Rows[0]["UomID"] ;						
//					}
//				}
//			}
//		}

		/// <summary>
		/// �õ����в鿴����Ϣ
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
		/// ����BI_DepartmentEmployee.IDKey �õ�BI_Employee.IDKey
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
		/// �õ������û���Ϣ
		/// </summary>
		/// <param name="UserID">�û�ID(��ʱ����Ϊ����Ա����IDKey���Ժ���ܻ��б䶯)</param>
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
		/// �õ�ָ�����ŵ���Ա��Ϣ
		/// </summary>
		/// <param name="DepartmentID">����ID</param>
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
		/// �õ�ָ�������Ա��Ϣ
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

		#region ��ȡEnquiry��ӡ��������

		/// <summary>
		/// ��ȡMR��ӡ��������
		/// </summary>
		/// <param name="sMRID"></param>
		/// <returns></returns>
		public DataTable GetPrintData ( string sEnquiryPriceID )
		{
			string sSelectSql = "SELECT * FROM v_Report_EnquiryPrint WHERE EnquiryPriceID = '"+sEnquiryPriceID+"'";
			string sErrorMsg = string.Empty;

			DataTable dtData = this.BaseDataAccess.GetDataTable ( sSelectSql );

			return dtData;
		}

		#endregion

		// Add by ZZH on 2008-1-18 �����֤�Ƿ����ɾ���ķ���
		public DataTable CheckState(String strMREnquiryPrice )
		{
			String strSql = " Select Max(PurchaseOrder.ApproveStatus) as CheckState From MR_EnquiryMaterial Left Join POMaterial On MR_EnquiryMaterial.MRMaterialID = POMaterial.MRMaterialID " +
				" Left Join PurchaseOrder On PurchaseOrder.POID = POMaterial.POID  Where MR_EnquiryMaterial.EnquiryPriceID = '" + strMREnquiryPrice + "'" ;
			DataTable dt = this.BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}

		//����ѯ�۵�ID����ñ��۵�ID
		public DataTable GetQuotationPriceID(String strMREnquiryPrice)
		{
			String strSql = " Select QuotationPriceID From MR_QuotationPrice where EnquiryPriceID='" + strMREnquiryPrice +"'";
			DataTable dt =  this.BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}
		//****************************************************
		
	}
}
