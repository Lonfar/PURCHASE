using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEIssueEdit 的摘要说明。
	/// </summary>
	public class DAEIssueEdit : DAEBase
	{

		CEntityUitlity  cen = new CEntityUitlity();

		public DAEIssueEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑  
			//
		}


		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal decTotalAmountStandard = 0;
			decimal decTotalAmountNatural = 0;
			foreach(DataRow drTransferDEP2DEPMaterial in dtChild.Rows)
			{
				if(drTransferDEP2DEPMaterial.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_IssueMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_IssueMaterial.UnitPriceNatural"].ToString());
					decimal decFactIssuedQuantity =  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_IssueMaterial.FactIssuedQuantity"].ToString());
					//decimal decDepreciationRate=  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.depreciationRate"].ToString());
					decTotalAmountStandard += decUnitPriceStandard*decFactIssuedQuantity ;
					decTotalAmountNatural += decUnitPriceNatural*decFactIssuedQuantity;
				}
			}
			dtEdit.Rows[0]["WH_Issue.TotalPriceStandard"] = decTotalAmountStandard;
			dtEdit.Rows[0]["WH_Issue.TotalPriceNatural"] = decTotalAmountNatural; 

		}

		public DataTable GetCurrency()
		{
			string strSql=@"SELECT * FROM BI_SysCurrency";
			DataTable dtCurrency = BaseDataAccess.GetDataTable(strSql);
			return dtCurrency;		
		}

		public DataTable  GetIssueMaterialBaseDetial(string strInStockMaterialID,string sDepID)
		{
			string sSql = "	SELECT WH_InStoreMaterialDetail.*,MaterialUOM.MaterialUomID,Material.MaterialName AS MaterialName,"+
				" ISNULL(WH_InStoreMaterialDetail.QuantityInBin,0) - ISNULL(WH_InStoreMaterialDetail.PreserveQuantity,0) + ISNULL(t.CanIssueQuantityPreserve,0) as CanIssueQuantityInBin,"+
				" ISNULL(t.CanIssueQuantityPreserve,0) AS CanIssueQuantityPreserve "+
				"	From WH_InStoreMaterialDetail inner JOIN  MaterialUOM "+
				"	on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID "+  
				"	AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1 "+
				"	inner JOIN Material on Material.ItemCode = WH_InStoreMaterialDetail.ItemCode "+
				"	left join  "+
				"	( "+
				"	SELECT a.InStockMaterialID,(ISNULL(QuantityPreserve,0)-Isnull(IssueFactQuantityPreserve,0)) "+
				"	as  CanIssueQuantityPreserve "+
				"	from "+
				"	(SELECT InStockMaterialID,SUM(CASE WH_Preserve.IsPreserve WHEN 1 THEN QuantityPreserve ELSE -ISNULL(QuantityByCanceled,0) END) as QuantityPreserve FROM  "+
				"	WH_PreserveMaterial "+
				"	JOIN  "+
				"	WH_Preserve  "+
				"	ON WH_PreserveMaterial.PreserveID = WH_Preserve.PreserveID "+
				"	where  WH_Preserve.DepID ='"+sDepID+"' AND WH_Preserve.Status =  "+(int)ApproveState.State_Approved+" "+
				"	group  by InStockMaterialID "+
				"	) a "+
				"	LEFT JOIN "+
				"	( "+
				"	SELECT InStockMaterialID,Sum(PreserveQuantityInFact) as IssueFactQuantityPreserve FROM  "+
				"	WH_IssueMaterial "+
				"	JOIN  "+
				"	WH_Issue  "+
				"	ON WH_IssueMaterial.IssueID = WH_Issue.IssueID "+
				"	where WH_Issue.DepID ='"+sDepID+"' AND WH_Issue.Status =  "+(int)ApproveState.State_Approved+" "+
				"	group  by InStockMaterialID "+
				"	) b "+
				"	ON  a.InStockMaterialID = b.InStockMaterialID  "+
				"	) t on WH_InStoreMaterialDetail.InStockMaterialID = t.InStockMaterialID "+
				"	WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+strInStockMaterialID+"'";
			DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
			return dtTempInfo;
		}

		public void UpdateIssueMaterial(DataTable dtIssueMaterial,string sDepID,PriceType enWHPriceType)
		{
            decimal dFactIssuedQuantity = 0;
            decimal dUnitPriceStandard = 0;

			foreach ( DataRow drIssueMaterial in dtIssueMaterial.Rows )
			{
				if(drIssueMaterial.RowState != DataRowState.Deleted)
				{

					DataTable  dtTempInfo = GetIssueMaterialBaseDetial (drIssueMaterial["InStockMaterialID"].ToString(),sDepID);
					if (dtTempInfo.Rows.Count > 0 )
					{
                        //订单编号	
                        drIssueMaterial["POID"] = dtTempInfo.Rows[0]["POID"];
                        drIssueMaterial["WH_IssueMaterial__POID"] = dtTempInfo.Rows[0]["POID"];

                        //库位	
                        drIssueMaterial["BINID"] = dtTempInfo.Rows[0]["BINID"];
                        drIssueMaterial["WH_IssueMaterial__BINID"] = dtTempInfo.Rows[0]["BINID"];

                        drIssueMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"];
                        //单位	
                        drIssueMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"];
                        drIssueMaterial["WH_IssueMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UomID"];

                        decimal decQuantityInBin = Convert.ToDecimal(dtTempInfo.Rows[0]["QuantityInBin"]);

                        //库存数量	
                        drIssueMaterial["QuantityInBin"] = decQuantityInBin;


                        //预留数量	
                        drIssueMaterial["PreserveQuantity"] = dtTempInfo.Rows[0]["CanIssueQuantityPreserve"];

                        //
                        //							drIssueMaterial["PreserveQuantityInFact"] = dtTempInfo.Rows[0]["CanIssueQuantityPreserve"] ;

                        //可发数量	
                        drIssueMaterial["CanIssuedQuantity"] = dtTempInfo.Rows[0]["CanIssueQuantityInBin"];

                        if (enWHPriceType == PriceType.TYPE_PO)
                        {
                            //基本单位单价(核)	
                            drIssueMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"];

                            drIssueMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"];
                        }
                        else if (enWHPriceType == PriceType.TYPE_Average)
                        {
                            //基本单位单价(核)	
                            drIssueMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["AveragePriceStandard"];

                            drIssueMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"];

                        }

                        //MaterialName
                        drIssueMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"];

                        //SumPrice
                        try
                        {
                            dFactIssuedQuantity = drIssueMaterial["FactIssuedQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(drIssueMaterial["FactIssuedQuantity"]);
                            dUnitPriceStandard = drIssueMaterial["UnitPriceStandard"] == DBNull.Value ? 0 : Convert.ToDecimal(drIssueMaterial["UnitPriceStandard"]);
                            drIssueMaterial["SumPrice"] = dFactIssuedQuantity * dUnitPriceStandard;

                        }
                        catch
                        {

                        }
                    }

				}

			}

		}

		
		public bool GetIssueNo(string sIssueNo)
		{
			string strSql =@"SELECT IssueNo FROM WH_Issue WHERE IssueNo = '"+sIssueNo+"'";
			DataTable dtIssue =  BaseDataAccess.GetDataTable(strSql);
			if (dtIssue.Rows.Count > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//获得物料单位 
		public DataTable GetMaterialUOM(string strMaterialUomID)
		{
			string strSql =@"SELECT * FROM MaterialUOM WHERE MaterialUomID = '"+strMaterialUomID+"'";
			DataTable dtMaterialUOM = dtMaterialUOM = BaseDataAccess.GetDataTable(strSql);
			if(dtMaterialUOM != null)
			{
				if(dtMaterialUOM.Rows.Count != 0)
				{
					return dtMaterialUOM;
				}
			}
			return dtMaterialUOM;
		  
		}

	    //获得物资单位系数信息
		public decimal GetMultipleOfBaseUOM(string sItemCode,string sMaterialUomID)
		{
			DataTable  dtMaterialUOM = new DataTable();
			decimal decMultipleOfBaseUOM = 0;

			if(sItemCode.Length != 0 && sMaterialUomID.Length != 0)
			{
				string sSelectSql = "SELECT MultipleOfBaseUOM FROM MaterialUOM WHERE ItemCode = '"+sItemCode+"' AND MaterialUomID = '"+sMaterialUomID+"'";
				dtMaterialUOM = BaseDataAccess.GetDataTable (sSelectSql);
			}
			if(dtMaterialUOM != null)
			{
				if(dtMaterialUOM.Rows.Count != 0)
				{
				  decMultipleOfBaseUOM =  Convert.ToDecimal(dtMaterialUOM.Rows[0]["MultipleOfBaseUOM"]);
				}
			
			}
			return decMultipleOfBaseUOM;
		}

		public void SetChildrenNew(DataTable dtChildren, int nRowindex,string PkValue,string sDepID,PriceType enWHPriceType)
		{

			//单位库房ID
			string strInStockMaterialID = dtChildren.Rows[nRowindex]["InStockMaterialID"].ToString() ;

			#region  单位信息
			string strItemCode ="";
			string strMaterialUomID ="";

			if(dtChildren.Rows.Count != 0)
			{
				strItemCode = dtChildren.Rows[nRowindex]["ItemCode"].ToString() ;
				strMaterialUomID = dtChildren.Rows[nRowindex]["MaterialUomID"].ToString() ;
			}

			//目标系数
		    decimal decMultipleOfBaseUOM = GetMultipleOfBaseUOM(strItemCode,strMaterialUomID);

			#endregion

			#region  基本单位数量信息
			
			DataTable dtMaterialBasicDetial = GetIssueMaterialBaseDetial(strInStockMaterialID,sDepID);
	
			//库存数量	
			decimal  decbasicQuantityInBin = Convert.ToDecimal(dtMaterialBasicDetial.Rows[0]["QuantityInBin"]);
			//预留数量	
			decimal  decbasicPreserveQuantity =  Convert.ToDecimal(dtMaterialBasicDetial.Rows[0]["CanIssueQuantityPreserve"]);
			//可发数量	
			decimal  decbasicCanIssuedQuantity =  Convert.ToDecimal(dtMaterialBasicDetial.Rows[0]["CanIssueQuantityInBin"]);// Convert.ToDecimal(DetIssueMaterial(dtMaterialBasicDetial.Rows[0]["QuantityInBin"].ToString(),dtMaterialBasicDetial.Rows[0]["PreserveQuantity"].ToString(),dtMaterialBasicDetial.Rows[0]["CanIssueQuantityPreserve"].ToString()));		
			//基本单位单价(核)	

			decimal  decbasicUnitPriceStandard = 0m;
			decimal  decbasicUnitPriceNatural = 0m;
			if(enWHPriceType == PriceType.TYPE_PO)
			{
				//基本单位单价(核)	
				decbasicUnitPriceStandard =  Convert.ToDecimal(dtMaterialBasicDetial.Rows[0]["UnitPricePOStandard"]) ;
						
				decbasicUnitPriceNatural = Convert.ToDecimal(dtMaterialBasicDetial.Rows[0]["UnitPricePONatural"]) ; 
			}
			else if(enWHPriceType == PriceType.TYPE_Average)
			{
				//基本单位单价(核)	
				decbasicUnitPriceStandard =  Convert.ToDecimal(dtMaterialBasicDetial.Rows[0]["AveragePriceStandard"]) ;
						
				decbasicUnitPriceNatural = Convert.ToDecimal(dtMaterialBasicDetial.Rows[0]["AveragePriceNatural"]) ;

			}
			#endregion

			  
			//基本单位ID
			string  strBasicMateriaUD = dtMaterialBasicDetial.Rows[0]["MaterialUomID"].ToString();
			//当前物资标识 IssueMaterialID
			string strIssueMaterialID = dtMaterialBasicDetial.Rows[0]["InStockMaterialID"].ToString();
			//库存数量	
			decimal decQuantityInBin = 0;
			decQuantityInBin = decimal.Divide(decbasicQuantityInBin,decMultipleOfBaseUOM);
			dtChildren.Rows[nRowindex]["QuantityInBin"] =  decQuantityInBin;
			//预留数量
			decimal decPreserveQuantity = 0;
			decPreserveQuantity= decimal.Divide(decbasicPreserveQuantity,decMultipleOfBaseUOM);
			dtChildren.Rows[nRowindex]["PreserveQuantity"] = decPreserveQuantity.ToString();
			dtChildren.Rows[nRowindex]["PreserveQuantityInFact"] = decPreserveQuantity.ToString();
			//可发数量	
			dtChildren.Rows[nRowindex]["CanIssuedQuantity"] =  decQuantityInBin -  decPreserveQuantity;
			dtChildren.Rows[nRowindex]["UnitPriceNatural"] = decbasicUnitPriceStandard * decMultipleOfBaseUOM;
			//基本单位单价(核)	
			dtChildren.Rows[nRowindex]["UnitPriceStandard"] = decbasicUnitPriceNatural * decMultipleOfBaseUOM ;
			
		
		}
		


		//返回人员所属部门信息
		public DataTable GetSingleEmDepart(string EmpID)
	    {
			string strSql =" select IDKey,DepartmentName  from BI_Department where IDKey in (select DepartmentID from BI_DepartmentEmployee where EmployeeID = '"+EmpID+"')";

			DataTable dtDepartmentName = BaseDataAccess.GetDataTable(strSql);

			return dtDepartmentName;
	    }

		//员工选部门
		public string  GetEmDepart(string EmpID)
		{
			//归属部门IDkey
		    string strDepartmentName = GetDepartFromEm(EmpID);
			
			//获取所属部门下属depth1的DataTable
			string strDepID = "SELECT * FROM BI_Department WHERE PDepartmentID ='"+strDepartmentName+"'" ;	
			DataTable dtDepartmentName = BaseDataAccess.GetDataTable(strDepID);
			string strTurn = "'";

			if(dtDepartmentName != null && dtDepartmentName.Rows.Count != 0)
			{
				
				DataTable dt = 	GetDepartTreeStr(strDepartmentName);

				//                    strDepartmentName += DepartmentSubIN(dt);
				strTurn ="'"+ strDepartmentName+"'";

				strTurn += DepartmentSubIN(dt);
				
			}
			else
			{
			  strTurn ="'"+strDepartmentName+"'";
			}
			
		    return strTurn;
		}

		//人员归属部门IDKey
		public string  GetDepartFromEm(string EmpID)
		{
			string strSql =" select IDKey from BI_Department where IDKey in (select DepartmentID from BI_DepartmentEmployee where EmployeeID = '"+EmpID+"')";

			DataTable dtDepartmentName = BaseDataAccess.GetDataTable(strSql);

            string strDepartmentName = "";

			if(dtDepartmentName.Rows.Count >0)
			{
			   strDepartmentName =  dtDepartmentName.Rows[0]["IDKey"].ToString();
			}

		    return strDepartmentName;
		}

		
		public string DepartmentSubIN(DataTable dt)
		{
	       string strSub="";
			foreach(DataRow row in dt.Rows)
			{
			 strSub +=",'"+row["IDKey"].ToString()+"'";
			}
			
		   return strSub;
		}

		//返回子Tree部门字符串
		public DataTable  GetDepartTreeStr(string DepID)
		{
		    string  strSql ="SELECT * FROM BI_Department where PDepartmentID = '"+DepID+"'";
		   
			DataTable dtDepartmentName = BaseDataAccess.GetDataTable(strSql);

			DataTable dtDepartmentNameClone = dtDepartmentName.Clone();

			 GetDepartment(dtDepartmentName,ref dtDepartmentNameClone);

			return dtDepartmentNameClone;
		
		}


		public void GetDepartment(DataTable dt,ref DataTable Clone)
		{

			foreach(DataRow row in dt.Rows)
			{
				if(row["IDKey"] != null && row["IDKey"].ToString().Length >0)
				{
				    Clone.ImportRow(row);

					if(CheckhaveSub(row["IDKey"].ToString()))
					{
					   DataTable dtsub =  GetDepartTreeStr(row["IDKey"].ToString());
					  
					  GetDepartment(dtsub,ref Clone);

					}
					
				}

			}		
		}


		//判断是否还有子节点
		public  bool CheckhaveSub(string DepID)
		{
		  string  strSql ="SELECT * FROM BI_Department where PDepartmentID = '"+DepID+"'";
		
		  DataTable dtDepartmentName = BaseDataAccess.GetDataTable(strSql);
		  
			if(dtDepartmentName.Rows.Count > 0)
			{
				return true;
			}
			else
			{
				return false;
			
			}

		}


        //--------------------------------
		//部门选员工
		public string GetDepartEm(string DepID)
		{

			string strDepID = "SELECT * FROM BI_Department WHERE PDepartmentID ='"+DepID+"'" ;	
			DataTable dtDepartmentName = BaseDataAccess.GetDataTable(strDepID);
			
			//部门字符串
			string strTurn = "";

			string strEmpSql = string.Empty;
			//返回字符串
			string strEmply ="";

			
			//有子部门状态
			if(dtDepartmentName != null && dtDepartmentName.Rows.Count != 0 )
			{
				strTurn = "'";
				if(dtDepartmentName.Rows.Count != 0)
				{
					DataTable dt = 	GetDepartTreeStr(DepID);
					strTurn ="'"+ DepID+"'";
					strTurn += DepartmentSubIN(dt);

				}
				
			}
			else
			{
				//无子目录
			   strTurn = "'"+DepID+"'";
			}
			
			if(strTurn.Length >0)
			{
				strEmpSql = "SELECT * From BI_DepartmentEmployee WHERE DepartmentID in ("+strTurn+")";
			
				DataTable dtEmploy = BaseDataAccess.GetDataTable(strEmpSql);
             
				foreach(DataRow row in dtEmploy.Rows)
				{
					string strTemp = "";
					if(strEmply.Length == 0)
					{
				
						strTemp = "'"+row["EmployeeID"].ToString()+"'";
					}
					else
					{
						strTemp = ",'"+row["EmployeeID"].ToString()+"'";
					}

					strEmply += strTemp;
				}
			}
		   return strEmply;
	 }
	}
}
