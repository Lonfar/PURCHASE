using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Common;
using Business;
using DataEntity;
using Cnwit.Utility;

namespace TopisWeb.LogisticsManagement.Process
{
	/// <summary>
	/// PaymentRequest 的摘要说明。
	/// </summary>
	public partial class PaymentRequest : PageBase,IPageList
	{
		#region  私有变量及控件


		private BUSPaymentRequest _busPaymentRequest = new BUSPaymentRequest();
		private DAEPaymentRequest _daePaymentRequest = new DAEPaymentRequest();

		private Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		private CEntityUitlity pEntityUitlity = new CEntityUitlity();
		
		protected UserControls.UCList ucList_PaymentRequest;
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			SetModuleProperty();
			InitControlsCultureInfo();
			if ( ! IsPostBack )
			{
				ShowList();
			}
			CreateToolMenu(Toolbar1,TopisWeb.ToolMenuType.TYPE_MainList) ;	
			EventBind();
            CheckAuthorities(Toolbar1, this.ucList_PaymentRequest);
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.ucList_PaymentRequest.DataGridUserItemCommand +=new UserControls.DataGridUserCommandEventHandler(ucList_PaymentRequest_DataGridUserItemCommand);
			this.Toolbar1.MenuItemClick += new MSPlus.Web.UI.WebControls.MenuItemClickedEventHandler(this.Toolbar1_MenuItemClick);

		}
		#endregion

		#region Page_Load 加载函数区

		#region SetModuleProperty (Need Modify)
		private void SetModuleProperty()
		{
			this.ModuleID = Request.QueryString.Get("moduleID") ;
		}
		#endregion

		#region InitControlsCultureInfo
		private void InitControlsCultureInfo()
		{
			lblMSG.Text=this.GetPublicString("string","Status");
		}
		#endregion

		#region ShowList
		private void ShowList()
		{	
            //if(Request.QueryString.Get("PRType")!=null)
            //{
				
            //    ucList_PaymentRequest.FilterSqlWhere = " LOG_PR.PRTypeID='"+Request.QueryString.Get("PRType").ToString()+"'";
            //}
			
			ucList_PaymentRequest.ListID = "vch_PaymentRequest";
            ucList_PaymentRequest.ShowPrintCommand = true;

		}
		#endregion

		#region EventBind (Need Modify)
		/// <summary>
		/// Bind Event of Page
		/// </summary>
		private void EventBind()
		{
			ListObserve oo=new ListObserve(this);
			oo.Business = _busPaymentRequest ;
			oo.DataAcess = _daePaymentRequest;			
			oo=null;
		}
		#endregion

		#endregion

		#region IPageList接口实现区

		#region IAddData 成员

		public void AddNewData()
		{
			RedirectToEditPage("");
		}

		#endregion
	
		#region IEditData 成员

		public void EditData()
		{

		}

		#endregion

		#region IExport 成员

		public void ExportData()
		{
			this.ExportExcel( ucList_PaymentRequest.ExportData() , this );
		}

		#endregion

		#region IView 成员

		public void ViewData(string pkValue)
		{
			
		}

		#endregion

		#region IPrint 成员

		public void PrintData(string printUrl,string pkValue)
		{
			Response.Write("<script language='javascript'>window.open('"+printUrl+".aspx?pkValue="+Server.UrlEncode( pkValue ) +"');</script>");
		}

		#endregion

		#region IPageDeleteData 成员

		public event Common.OperateDataEventHandler DoDeleteData;

		#endregion

		#region IPageQueryData 成员

		public event Common.OperateDataEventHandler DoQueryData;

		#endregion

		#region IPageMessage 成员

		public void DisplayMessage(string sErrorMessage)
		{
			// Modified by Liujun at 20071218
			this.lblMSG.Text += this.GetDisplayMessage ( sErrorMessage );
		}

		#endregion

		#region IConfirm 成员

		public void ConfirmData(string pkValue)
		{
			// TODO:  添加 BasicInfoSex.ConfirmData 实现
		}

		#endregion

		#region IApprove 成员

		public void ApproveData(string pkValue)
		{
			// TODO:  添加 BasicInfoSex.ApproveData 实现
		}

		#endregion

		#region IUnApprove 成员

		public void UApproveData(string pkValue)
		{
			// TODO:  添加 BasicInfoSex.UApproveData 实现
		}

		#endregion

		#region ISubmit 成员

		public void SubmitData()
		{
			// TODO:  添加 BasicInfoSex.SubmitData 实现
		}

		#endregion

		#region IHelp 成员

		public void Help()
		{
			ShowHelp() ;
		}

		#endregion		
          
		#endregion

		#region 功能函数

		#region Redirect EditPage (Need Modify)
		private void RedirectToEditPage(string pkValue)
		{			
			Response.Redirect ( "PaymentRequest_Edit.aspx?moduleID="+ModuleID+"&pkValue=" + Server.UrlEncode(pkValue) +"&PRType="+Request.QueryString.Get("PRType") ) ;
		}	
		#endregion

		#region 查询过滤条件
		
		private void ShowFilterDetail(string strNewWhere)
		{
			string sqlWhere = string.Empty;
			// 将查询条件缓存
			ViewState["strFilter"] = strNewWhere;
		}
		#endregion
		#endregion

        private string GetTotalValue()
        {

            DataTable dt = this.ucList_PaymentRequest.ExportData().Tables[0];
            decimal total = 0;
            foreach (DataRow dr in dt.Rows)
            {

                //if (dr["LOG_PR__PRTotalStandard"] != DBNull.Value)
                //{
                //    total = total + Decimal.Parse(dr["LOG_PR__PRTotalStandard"].ToString());
                //}

                if (dr["MR_PaymentRequest__InvoiceAmount"] != DBNull.Value)
                {
                    total = total + Decimal.Parse(dr["MR_PaymentRequest__InvoiceAmount"].ToString());
                }

            }
            return "Invoice Total Amount:" + total.ToString("N4");
        }

		#region 数据操作区
		#region  Delete Operate (Need Modify)

		private void DeleteSingleRow(string pkValue)
		{
			if (DoDeleteData!=null)
			{
				DeleteData(pkValue);
			}		
		}


		private void DeleteMultiRow()
		{
			if (DoDeleteData!=null)
			{
				string pkValues = ucList_PaymentRequest.GetPrimaryCheckColumnValues();
				string[] pkValueArray = System.Text.RegularExpressions.Regex.Split(pkValues, ",");
				for(int i=0;i<pkValueArray.Length;i++)
				{
					DeleteData(pkValueArray[i]);
				}				
			}		
		}		
		
		private void DeleteData(string pkValue)
		{
			OperateDataEventArgs ode=new OperateDataEventArgs();
            //CDeleteOperation.AddDeleteData(ode,"LOG_PR_ForPSW","PRID",pkValue);
            ////再删主表	
            //CDeleteOperation.AddDeleteData(ode,"LOG_PR","PRID",pkValue);

            CDeleteOperation.AddDeleteData(ode, "MR_PaymentRequest", "IDKey", pkValue);

			DoDeleteData(ode);
				
			ucList_PaymentRequest.RefreshGrid();
			

		}
		
		
		#endregion


		#endregion 

		#region 事件函数区
		#region Toolbar Event
		private void Toolbar1_MenuItemClick(object sender, MSPlus.Web.UI.WebControls.MenuItemClickEventArgs e)
		{
			switch(e.EventItem.ID.ToString().ToLower())
			{
				case "tbadd":	
					AddNewData();
					break ;
				case "tbdelete":
					DeleteMultiRow();				
					break ;
				case "tbexport":	
					ExportData();
					break ;
				case "tbhelp":
					Help() ;
					break;
			}
		}
		#endregion

		#region ucList_PaymentRequest Event
		private void ucList_PaymentRequest_DataGridUserItemCommand(object sender, DataGridCommandEventArgs e)
		{
			string pkValue="";

			DataGrid dg=(DataGrid)sender;
			pkValue=dg.DataKeys[e.Item.ItemIndex].ToString().Trim();

			switch ( e.CommandName.Trim().ToUpper() )
			{
				case "EDIT" :
				{
					RedirectToEditPage( pkValue );
					break;
				}
				case "DELETE" :
				{
					DeleteSingleRow( pkValue );
					break;
				}
				case "PRINT":
                    PrintData("PRPrint", pkValue);
					break;
				case "VIEW" :
				{
					base.ShowViewInfo("vch_PaymentRequest" , pkValue );
					break;	
				}
			}		
		}		
		#endregion
		#endregion

		#region OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			SetButtonState();
            this.lblMSG.Text = this.GetTotalValue();
			base.OnPreRender (e);
		}


		#endregion

		#region OnPreRender 加载函数区
		private void SetButtonState()
		{
			DataTable  dt = ucList_PaymentRequest.CurrentPageData;
			DataGrid dg =(DataGrid)ucList_PaymentRequest.FindControl("dgrdList") as DataGrid;
			int  i=0,j = 0 ;
            //while(i<dg.Columns.Count)
            //{
            //    BoundColumn bc = dg.Columns[i] as BoundColumn ;
            //    if(bc != null&&bc.DataField == "LOG_PR__Status")
            //    {
            //        j = i ;
            //        break ;
            //    }
            //    i++ ;
            //}
            //if(dg != null)
            //{
            //    foreach(DataGridItem dgi in dg.Items)
            //    {
            //        ImageButton pImageEditButton = dgi.FindControl("Delete") as ImageButton;
            //        if(pImageEditButton != null)
            //        {
            //            if (dt.Rows[dgi.ItemIndex]["LOG_PR.Status"] != DBNull.Value )
            //            {
            //                dgi.Cells[j].CssClass = "Status"+Convert.ToInt32(dt.Rows[dgi.ItemIndex]["LOG_PR.Status"].ToString());
            //            }
            //            //状态改变为生效状态，不能编辑和修改
            //            if(dt.Rows[dgi.ItemIndex]["LOG_PR.Status"]!=System.DBNull.Value )
            //            {
            //                if ( (Convert.ToInt32(dt.Rows[dgi.ItemIndex]["LOG_PR.Status"].ToString()) >= (int)PRState.Close ) )
            //                {
            //                    pImageEditButton.ImageUrl =Request.ApplicationPath + "//Images//Grid//GridDelete1.gif";
            //                    pImageEditButton.Enabled = false;

            //                    CheckBox ckb = dgi.FindControl ( "chkSel" ) as CheckBox ;
            //                    ckb.Enabled = false;
            //                }
            //            }													
            //        }
            //    }
            //}
		
		}
		#endregion
	}
}
