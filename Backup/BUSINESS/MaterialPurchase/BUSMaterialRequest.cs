using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSMaterialRequest ��ժҪ˵����
	/// </summary>
	public class BUSMaterialRequest : BUSBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		DAEServiceRequistion daeMR = new DAEServiceRequistion();
		DAEMaterialRequest _daeMaterialRequest = new DAEMaterialRequest();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtEdit"></param>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public Decimal CalculateEstTotalCost(DataTable dtEdit,DataTable dtChild)
		{
			Decimal dEstTotalCost = 0 ;
			Decimal dEstUnitPrice = 0;
			Decimal dMRQuantity = 0;

			if ( dtEdit != null && dtChild != null )
			{
				foreach (DataRow dr in dtChild.Rows)
				{
					if ( dr.RowState != DataRowState.Deleted )
					{						
						dEstUnitPrice = Convert.ToDecimal( dr["MR_Material.EstUnitPrice"] == System.DBNull.Value ? "0" : dr["MR_Material.EstUnitPrice"].ToString() );
						dMRQuantity = Convert.ToDecimal( dr["MR_Material.MRQuantity"] == System.DBNull.Value ? "0" : dr["MR_Material.MRQuantity"].ToString() );
						
						dEstTotalCost += dEstUnitPrice * dMRQuantity;
					}
				}
			}
			return dEstTotalCost;
		}

		#region ��Excel�е����ݵ��뵽DataTable��

		/// <summary>
		/// ��Excel�е����ݵ��뵽DataTable��
		/// </summary>
		/// <param name="dtMaterialList"></param>
		/// <param name="dtExcel"></param>
		public string UpdateMaterialListFromExecl(DataTable dtMaterialList,DataTable dtExcel)
		{
			string itemcodes = "";
			foreach ( DataRow drExcel in dtExcel.Rows )
			{
				bool bRepert = JudgeRepert(dtMaterialList,drExcel) ;
				if( bRepert == false)
				{
					string returnValue = AddMaterialRowFromExecl(dtMaterialList,drExcel);
					if(returnValue.Length > 0)
					{
						itemcodes = returnValue +",";
					}
				}
			}
			if(itemcodes.Length >0)
			{
				return itemcodes.Substring(0,itemcodes.Length -1);
			}
			return "";
		}


		/// <summary>
		/// �ж�Excel�е�ItemCode�Ƿ���ҳ�����Ѿ�����
		/// </summary>
		/// <param name="dtMaterialList"></param>
		/// <param name="drExcel"></param>
		/// <returns></returns>
		private bool JudgeRepert(DataTable dtMaterialList,DataRow drExcel)
		{
			foreach(DataRow dr in dtMaterialList.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					if(dr["ItemCode"].ToString().Trim() == drExcel[0].ToString().Trim())
					{
						return true;
					}
				}
			}
			return false ;
		}


		/// <summary>
		/// ��Excel�������ݵ������ݱ���
		/// </summary>
		/// <param name="dtMaterialList"></param>
		/// <param name="drExcel"></param>
		private string AddMaterialRowFromExecl(DataTable dtMaterialList,DataRow drExcel)
		{
			if( !CheckItemCode(drExcel[0].ToString()) ) return drExcel[0].ToString(); 
			DataRow drNew = dtMaterialList.NewRow();
			drNew["MRMaterialID"] = System.Guid.NewGuid().ToString() ; 
			//���ʱ���
			drNew["ItemCode"] = drExcel[0] ;
			drNew["DescCol"] = drExcel[0] ; 
			//��������
			drNew["MaterialName"] = drExcel[1] ; 
			//��Ʒ���
			drNew["ProductStandard"] = drExcel[2] ;
			//����	
			drNew["MRQuantity"] = drExcel[3] ;
			//��λ
			String materialUomId = GetMaterialUomIDByItemCodeAndUomID(drExcel[0].ToString(), drExcel[4].ToString()) ; 
			if(materialUomId == String.Empty)
			{
				drNew["MaterialUomID"] =  DBNull.Value ;
			}
			else
			{
				drNew["MaterialUomID"] = materialUomId ; 
			}
			drNew["MR_Material__MaterialUomID"] = drNew["MaterialUomID"] == DBNull.Value ? DBNull.Value : drExcel[4] ;
			//���Ƶ���
			drNew["EstUnitPrice"] = drExcel[5] ;
			//��ע	
			drNew["Comment"] = drExcel[6] ;	
			drNew["RowAttribute"] = "Ordinary" ;	
			drNew["ROWSTATUS"] = "NEW" ;
			dtMaterialList.Rows.Add(drNew);
			return "";
			
		}


		#endregion

		#region GetMaterialUomIDByItemCodeAndUomID

		private String  GetMaterialUomIDByItemCodeAndUomID(String itemCode , String strUomID)
		{
			String materialUomId = String.Empty ; 
			materialUomId = _daeMaterialRequest.GetMaterialUomIDByItemCodeAndUomID( itemCode ,  strUomID);
			return materialUomId ; 
		}

		#endregion

		#region CheckItemCode
		private bool CheckItemCode(String strItemCode)
		{
			return _daeMaterialRequest.CheckItemCode(strItemCode);
		}
		#endregion


		#region �Ƿ�����������
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strPkValue"></param>
		/// <param name="UserID"></param>
		/// <param name="popedomNew"></param>
		/// <returns></returns>
		public int GetMRState(string strPkValue,string UserID,string popedomNew)
		{
			string strSql="SELECT * FROM MR_MaterialRequisition WHERE charindex(','+convert(varchar,MR_MaterialRequisition.Status)+',',',"+popedomNew+",')>0 AND  MR_MaterialRequisition.MRID='"+strPkValue+"' AND MR_MaterialRequisition.CreateBy= '"+ UserID +"'";
			DataTable dtMRState=_da.GetDataTable(strSql);
			return dtMRState.Rows.Count;

		}
		#endregion

		#region ����������Ϣ
		/// <summary>
		/// ����������Ϣ
		/// </summary>
		/// <param name="dtMaterialList"></param>
		public void UpdateMaterialList(DataTable dtMaterialList)
		{
			foreach ( DataRow drMaterial in dtMaterialList.Rows )
			{
				if(drMaterial.RowState != DataRowState.Deleted)
				{
					string sSql = @"select Material.ItemCode,Material.MaterialName,Material.ProductStandard,
									MaterialUOM.MaterialUomID,MaterialUOM.UOMID,Material.Comment 
									from  Material, MaterialUOM
									where MaterialUOM.ItemCode = Material.ItemCode and  MaterialUOM.IsBaseUOM = 1
								    and Material.ItemCode = '"+drMaterial["ItemCode"].ToString()+"'";
					DataTable  dtTempInfo = _da.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{					
						//���ʱ���	
						drMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ; 
						//��������
						drMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ; 
						//��Ʒ���
						drMaterial["ProductStandard"] = dtTempInfo.Rows[0]["ProductStandard"] ;
						//��ע	
						drMaterial["Comment"] = dtTempInfo.Rows[0]["Comment"] ;
						//��λ
						drMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drMaterial["MR_Material__MaterialUomID"] = dtTempInfo.Rows[0]["UomID"] ;						
					}
				}
			}
		}
		#endregion

		#region �ɰ�����

		/// <summary>
		/// �õ���½�ߺ����ز���֮��Ĺ�ϵ
		/// </summary>
		/// <param name="sCurrentUserID">�û�ID</param>
		/// <param name="nType">�ж�����</param>
		/// <returns></returns>
		public bool IsMainDepLeader(string sCurrentUserID,int nType)
		{
			string strSql = string.Empty;
			if(nType == 1)
			{
				//�����ز��ŵ��쵼
				strSql="SELECT COUNT(1) FROM BI_DepartmentEmployee a,BI_Department b WHERE a.DepartmentID = b.idkey AND  b.IsMisDepartment=1  AND  a.Principal =1 AND a.EmployeeID ='"+sCurrentUserID+"'";
			}
			else
			{
				//�����ز��ŵ���Ա
				strSql="SELECT COUNT(1) FROM BI_DepartmentEmployee a,BI_Department b WHERE a.DepartmentID = b.idkey AND  b.IsMisDepartment=1   AND a.EmployeeID ='"+sCurrentUserID+"'";

			}
			if(int.Parse(_da.GetDataTable(strSql).Rows[0][0].ToString())>0)
				return true;
			else
				return false;
		}


		/// <summary>
		/// �õ���ǰMR״̬
		/// </summary>
		/// <param name="strPkValue"></param>
		/// <param name="popedomNew"></param>
		/// <returns></returns>
		public int GetCurrentMRState(string strPkValue,string popedomNew)
		{
			string strSql = "SELECT * FROM MR_MaterialRequisition WHERE charindex(','+convert(varchar,MR_MaterialRequisition.Status)+',',',"+popedomNew+",')>0 AND  MR_MaterialRequisition.MRID='"+strPkValue+"' ";
			DataTable dtMRState=_da.GetDataTable(strSql);
			return dtMRState.Rows.Count;
		}

		/// <summary>
		/// �Ƿ������
		/// </summary>
		/// <param name="sCurrentUserID"></param>
		/// <param name="sPkValue"></param>
		/// <returns></returns>
		public bool IsUndertaker(string sCurrentUserID,string  sPkValue)
		{
			string strSql = "SELECT COUNT(1) FROM MR_MaterialRequisition WHERE ReceiveBy ='"+sCurrentUserID+"' AND MRID = '"+sPkValue+"'";
			if(int.Parse(_da.GetDataTable(strSql).Rows[0][0].ToString())>0)
				return true;
			else
				return false;

		}

		/// <summary>
		/// �ж�����״̬
		/// </summary>
		/// <param name="strPkValue"></param>
		/// <param name="popedomNew"></param>
		/// <returns></returns>
		public int GetMtMRState(string strPkValue,string popedomNew)
		{
			string strSql="SELECT * FROM MR_MaterialRequisition WHERE MR_MaterialRequisition.Status >= "+int.Parse(popedomNew)+" AND  MR_MaterialRequisition.MRID='"+strPkValue+"' ";
			DataTable dtTenderState=_da.GetDataTable(strSql);
			return dtTenderState.Rows.Count;

		}	


		#endregion

		#region ͨ����������������ӿ���
		/// <summary>
		/// ͨ����������������ӿ���
		/// </summary>
		/// <param name="DepID"></param>
		/// <returns></returns>
		public string GetDepsByDepID(string DepID)
		{
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

			string SelectSql ="SELECT a.IDKey FROM BI_Department a,f_CDepartment_IDKey('"+DepID+"') b WHERE a.IDKey=b.[ID]";

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					strBuilder.Append( "'"+Convert.ToString( dr["IDKey"] ) + "'," );
				}
			}

			string strDepartmentIDKey = string.Empty;

			if ( strBuilder.Length > 0 )
			{
				if ( strBuilder[strBuilder.Length-1] == ',' )
				{
					strDepartmentIDKey = strBuilder.Remove ( strBuilder.Length - 1 , 1 ).ToString();
				}
			}

			return strDepartmentIDKey ;
		}
		#endregion

		#region �Բ鿴�˲���

		/// <summary>
		/// �õ����в鿴����Ϣ
		/// </summary>
		/// <returns></returns>
		public DataTable GetSeePerson( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName], BI_PositionType.PositionName as [BI_PositionType.PositionName], 
						'Edit' RowStatus, MR_Viewer.ViewerID as [MR_Viewer.ViewerID], MR_Viewer.ViewerName as [MR_Viewer.ViewerName], 
						MR_Viewer.ObjectiveID as [MR_Viewer.ObjectiveID],MR_Viewer.ObjectiveType as [MR_Viewer.ObjectiveType], 
						MR_Viewer.ViewerPosition as [MR_Viewer.ViewerPosition],BI_DepartmentEmployee.DepartmentID as [MR_Viewer.ViewerDept] 
						from  MR_Viewer,BI_Employee,BI_DepartmentEmployee,BI_PositionType 
						where MR_Viewer.ViewerName = BI_Employee.IDKey 
						and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID 
						and MR_Viewer.ViewerDept = BI_DepartmentEmployee.DepartmentID 
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey 
						and MR_Viewer.ObjectiveID = '"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}


		/// <summary>
		/// �Ƿ���SeePerson���д���
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtSeePerson )
		{
			foreach(DataRow dr in dtSeePerson.Rows)
			{
				if ( IDKey == dr["MR_Viewer.ViewerName"].ToString()) return true;
			}
			return false;
		}

		/// <summary>
		/// ɾ���鿴��
		/// </summary>
		/// <param name="strIDKey"></param>
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
		/// 
		/// </summary>
		/// <param name="GroupID"></param>
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
		

		#endregion

		#region ����������Ϣ
		/// <summary>
		/// У���ӱ��Ƿ�������
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// ������Ϣ
			string sErrorMsg = string.Empty;
			// У���ӱ��Ƿ�������
			sErrorMsg = CheckChildRows(dt);
			if ( sErrorMsg.Trim().Length == 0 )
			{
				// У��ҵ������           
				sErrorMsg = CheckMaterial ( dt );
				if ( sErrorMsg.Trim().Length > 0 )
				{ 
					return sErrorMsg;
				}
			}
			return sErrorMsg;
		}

		/// <summary>
		/// �ж������Ƿ�С��0
		/// </summary>
		/// <param name="dtMaterial"></param>
		/// <returns></returns>
		public string CheckMaterial ( DataTable dtMaterial )
		{
			foreach(DataRow drMaterial in dtMaterial.Rows)
			{
				if (drMaterial.RowState != DataRowState.Deleted)
				{
					decimal iMRQuantity = Decimal.Parse(drMaterial["MR_Material.MRQuantity"].ToString()); 
					if (iMRQuantity <= 0)
					{	
						return "Error01" ;
					}
				}
			}

			return string.Empty ;
		}

		private string CheckChildRows(DataTable dtChild)
		{
			string sErrMsg = "";
			if(dtChild.Rows.Count <= 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}

		#endregion

		#region ͨ��Ա�����������Ӧ��ID

		/// <summary>
		/// ͨ��Ա�����������Ӧ��ID ( Added By wxc at 12.4 )
		/// </summary>
		/// <param name="EmployeeID"></param>
		/// <returns></returns>
		public DataTable GetDepartmentByEmployeeID(string EmployeeID)
		{
			DataTable dt = new DataTable(); 
			string rDepartmentID = string.Empty;
			
			string sSql = "SELECT  BI_Department.DepartmentDepth , e.[DepartmentID] as [DepartmentID], BI_Department.DepartmentName as   [DepartmentName],e.[PositionID] as [PositionID],"+    
				" p.[PositionName] as [PositionType] FROM  BI_DepartmentEmployee AS e  left JOIN BI_PositionType p ON p.IDKey=e.PositionID  "+
				" left join BI_Department on e.DepartmentID =BI_Department.IDKey  WHERE e.EmployeeID = '"+EmployeeID+"'";

			dt = _da.GetDataTable(sSql);

			/*
			//�жϲ��ż����Ƿ�Ϊ  2
			if(dt.Rows.Count > 0)
			{
				if(dt.Rows[0]["DepartmentDepth"] != System.DBNull.Value && dt.Rows[0]["DepartmentDepth"].ToString().Length > 0)
				{
					if(dt.Rows[0]["DepartmentDepth"].ToString().Trim() =="2")
					{
						rDepartmentID=dt.Rows[0]["DepartmentID"].ToString().Trim();
					}
					else
					{
						//��һ�µݹ����
						int nDepartmentDepth = int.Parse(dt.Rows[0]["DepartmentDepth"].ToString().Trim());
						string sDepartmentID = dt.Rows[0]["DepartmentID"].ToString().Trim();
						FindDepartment(nDepartmentDepth,sDepartmentID,ref rDepartmentID);
					}
				}
			}

			sSql = "SELECT   BI_Department.[IDKey] as [DepartmentID], BI_Department.DepartmentName as   [DepartmentName] "+
				" FROM BI_Department  WHERE BI_Department.IDKey = '"+rDepartmentID+"'";

			dt = _da.GetDataTable(sSql);
			*/

			return dt;

		}

		private void FindDepartment(int nDepartmentDepth,string sDepartmentID,ref string rDepID)
		{
			if(nDepartmentDepth.ToString().Trim() == "2")
			{

				rDepID = sDepartmentID;
			}
			else
			{
				DataTable dt_Temp=new DataTable();
				string sSql= "SELECT PDepartmentID from BI_Department WHERE IDKey = '"+sDepartmentID.Trim()+"'";
				dt_Temp = _da.GetDataTable(sSql);
				if(dt_Temp.Rows.Count>0)			
				{
					nDepartmentDepth  = nDepartmentDepth-1;
					FindDepartment(nDepartmentDepth,dt_Temp.Rows[0]["PDepartmentID"].ToString(),ref rDepID);
				}
			}

		}
		#endregion
		
		#region ȥ���ݿ���ɸѡ��½���û����ƿصĲ��Ż��߿���
		/// <param name="sCurrentUserID">��½���</param>
		/// <returns>popedomDepID</returns>
		public string GetUserDepartmentID(string sCurrentUserID)
		{
			string popedomDepID=string.Empty;
			//����Ȩ�޵Ŀ���
			if(daeMR.GetAllDepartmentID(sCurrentUserID) != null)
			{
				//ȡ�õ�½�ߵ����ڲ��ŵ�ID�����ܰ������֣�һ�����ǲ��ŵ��쵼��һ�������ǲ��ŵ��쵼��
				DataSet ds = daeMR.GetAllDepartmentID(sCurrentUserID);
				
				int nCount = ds.Tables.Count;
				//����Щ�����н���ѭ��
				for(int i=0;i<nCount;i++ )
				{
					int k = ds.Tables[i].Rows.Count;
					for(int j = 0; j<k ;j++)
					{
						//ѭ����ʱ����ds.Tables[i].Rows[j][1].ToString()=="1" �����Ǵ˲��ŵ�����
						if(ds.Tables[i].Rows[j][1] != System.DBNull.Value && ds.Tables[i].Rows[j][1].ToString() == "1")
						{
							//ѭ������DepID
							popedomDepID += ","+ds.Tables[i].Rows[j][0].ToString();
						}
					}
				}
			}
			return popedomDepID;
		}
		#endregion




		public bool IsModifyMRNo(string strMRKey,string strMRNo)
		{
			bool isExist = false;
			DataTable dt = _daeMaterialRequest.IsModifyMRNo(strMRKey);

			if( dt != null && dt.Rows.Count > 0 )
			{
				if( dt.Rows[0]["MRNO"].ToString() != strMRNo )
				{
					isExist = true;
				}
			}
			return isExist;
		
		}


		// Add by ZZH on 2008-1-18 �����֤�Ƿ����ɾ���ķ���
		public bool CheckState(String strMRID , MRState state)
		{
			DataTable dt = _daeMaterialRequest.CheckState(strMRID);
			int strState = -1 ; 
			int intState = (int)state ; 
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["CheckState"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CheckState"]);
			}
			return  strState >= intState ;

		}

		//  Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ������ڵ㱻��һ�ڵ�����ʱ����Ӧ��ɾ��
		public bool CheckDeleteRecord(String strPKValue , MRState state )
		{
			int strState = -1 ; 
			int intState = (int)state ; 
			DataTable dt = _daeMaterialRequest.GetRecord(strPKValue) ;
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["State"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["State"]);
			}
			return  strState <= intState ;
		}
		//*********************************************************


	}
}
