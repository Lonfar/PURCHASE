using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEVendorList_Edit 的摘要说明。
	/// </summary>
	public class DAEVendorList_Edit
	{
		CEntityUitlity CU = new CEntityUitlity();
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		public DAEVendorList_Edit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		public string InsertMaterialVendor(string sItemCode,string sMaterialName,string sVendorID,string sComment,string sVendorName)
		{
			string errorMessage = "";
			string sSql = "insert into WH_MaterialVendor(MaterialVendorID,ItemCode,VendorID,MaterialName,Comment,VendorName) values ('"+System.Guid.NewGuid().ToString() +"',"+
				"'"+sItemCode+"','"+sVendorID+"','"+sMaterialName+"','"+sComment+"','"+sVendorName+"')";
			errorMessage = _da.ExecuteDMLSQL(sSql);
			return errorMessage;
		}

		/// <summary>
		/// 删除物资
		/// </summary>
		/// <param name="strIDKey"></param>
		public string DeleteMaterialVendor(string sItemCode)
		{
			string errorMessage = "";
			
			string sSql;
			if (sItemCode != null && sItemCode.Length > 0)
			{
				sSql = "delete WH_MaterialVendor where ItemCode ='"+sItemCode+"'";
			}
			else
			{
				sSql = "delete WH_MaterialVendor where 1 > 2 ";
			}
			errorMessage = _da.ExecuteDMLSQL(sSql);
			return errorMessage;
		}

		public DataTable GetMaterial( string sIDKey )
		{
			string strSql = "";
			if (sIDKey != null && sIDKey.Length > 0)
			{
				strSql = @"select * from WH_MaterialVendor where WH_MaterialVendor.VendorID = '"+sIDKey+"'";
			}
			else
			{
				strSql = @"select * from WH_MaterialVendor where 1 > 2 ";
			}
			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// 取得供应商评价表中的打分评价Grete by wxc
		/// </summary>
		public System.Data.DataTable  GetdtVendorEvaluateStandard(string strIDKEY)
		{
			string strSql= @"SELECT StandardIDKey, "+
							" VendorEvaluate.IDKey as 'VendorEvaluate.IDKey', "+
							" VendorEvaluate.MainField as 'VendorEvaluate.MainField', "+
							" VendorEvaluateStandard.StandardName,VendorEvaluateStandard.StandardIDKey as 'VendorEvaluate.StandardName', "+
							" StandardDescription as 'VendorEvaluate.Proporation',StandarValue as 'VendorEvaluate.StandarScore',"+
							" VendorEvaluateStandard.OrderID,VendorEvaluate.Score as 'VendorEvaluate.Score', "+
							" '' as RowStatus "+
							" FROM VendorEvaluateStandard left join (select * from VendorEvaluate where IDKey='"+strIDKEY+"')VendorEvaluate "+
							" on  VendorEvaluate.StandardName = VendorEvaluateStandard.StandardIDKey WHERE IsUse = 0 AND [Module]=0 ";
			System.Data.DataTable dt =_da.GetDataTable (strSql);
			return dt ;
		}


		/// <summary>
		/// 取得供应商评价表中的打分评价Greate by wxc
		/// </summary>
		public System.Data.DataTable  GetdtVendorGradeEvaluateStandard(string strIDKEY)
		{

			string strSql =  @" SELECT DISTINCT VendorEvaluate.IDKey as 'VendorEvaluate.IDKey', "+
							" VendorEvaluate.MainField as 'VendorEvaluate.MainField', "+
							" VendorEvaluateStandard.StandardName,VendorEvaluateStandard.StandardIDKey as 'VendorEvaluate.StandardName', "+
							" VendorEvaluateStandard.StandardDescription as 'VendorEvaluate.Proporation',"+
							" VendorEvaluateStandard.StandarValue as 'VendorEvaluate.StandarScore', "+
							" VendorGradeEvaluateStandard.StandardIDKey, "+
							" VendorEvaluateStandard.OrderID, VendorEvaluate.Score as 'VendorEvaluate.Score',"+
							" '' as  RowStatus "+
							" FROM VendorGradeEvaluateStandard LEFT JOIN VendorEvaluateStandard  "+
							" ON VendorEvaluateStandard.StandardIDKey = VendorGradeEvaluateStandard.StandardIDKey "+
							" LEFT JOIN (select * from VendorEvaluate where IDKey='"+strIDKEY+"')VendorEvaluate "+
							" on  VendorEvaluate.StandardName = VendorEvaluateStandard.StandardIDKey WHERE  "+
							" VendorEvaluateStandard.IsUse = 0 AND [Module]=1";
			System.Data.DataTable dt =_da.GetDataTable (strSql);
			return dt ;
		}

		/// <summary>
		/// 取得供应商评价表中的Greate by wxc
		/// </summary>
		//GenerallyDDL("GradeIDKey","GradeName",strID,ddl_Grade);
		public void  GenerallyDDL(string strValue,string strName,string strID,System.Web.UI.WebControls.DropDownList ddl)
		{
			string strSql="SELECT GradeIDKey,GradeName FROM VendorGradeEvaluateStandard WHERE StandardIDKey = '"+strID+"'";
			CU.BindDropDownList(strValue,strName,strSql,ddl);
			System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("","");
			ddl.Items.Insert(0,li);
		}


		public string  GetScorebyDataBase(string strGradeIDKey)
		{
			string strSql = @"SELECT * FROM VendorEvaluate WHERE MainField = '"+strGradeIDKey.Trim()+"'";
			if(_da.GetDataTable(strSql).Rows.Count>0)
				return _da.GetDataTable(strSql).Rows[0]["Score"].ToString();
			else
				return "";

		}

		public string  GetSelectedValue(string IDKey,string sGrade)
		{
			string strSql = string.Empty;

			if ( sGrade.Length > 0  ) 
			{
				strSql = @"SELECT GradeIDKey FROM VendorGradeEvaluateStandard WHERE StandardIDKey = '"+IDKey.Trim()+"'  AND  GradeScore = "+sGrade.Trim()+"";
			}
			else
			{
				strSql = @"SELECT GradeIDKey FROM VendorGradeEvaluateStandard WHERE StandardIDKey = '"+IDKey.Trim()+"'";
			}

			if(_da.GetDataTable(strSql).Rows.Count>0)
				return _da.GetDataTable(strSql).Rows[0]["GradeIDKey"].ToString();
			else
				return "";

		}


		


		/// <summary>
		///   判断RowStatus
		/// </summary>

		public string GetRowStatus(string ParentPKey,string sChildID)
		{

			string strSql= "select * from VendorEvaluate where IDKey='"+ParentPKey+"' and  StandardName='"+sChildID+"'";
            DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			{
				return "EDIT";

			}
			else
			{
				return "NEW";

			}
		

		}

		/// <summary>
		/// 根据等级提供分数Greate by wxc
		/// </summary>

		public string GetScorebyGrade(string strValue)
		{
			string strSql="SELECT GradeScore FROM VendorGradeEvaluateStandard WHERE GradeIDKey = '"+strValue+"'";
			if(_da.GetDataTable(strSql).Rows.Count>0)
			return _da.GetDataTable(strSql).Rows[0][0].ToString();
			else
				return "";
			
		}


	}
}
