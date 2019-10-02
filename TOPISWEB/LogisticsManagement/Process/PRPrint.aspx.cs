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
using CrystalDecisions.CrystalReports.Engine;
using TopisWeb.Report;

namespace TopisWeb.MaterialPurchase
{
	/// <summary>
	/// 页面名称: 物资申请
	/// 页面类型: 打印界面
	/// 编辑日期: 2007-10-22
	/// 编辑状态: 新增
	/// 操作人员: 刘俊
	/// </summary>
	public partial class PRPrint : PageBase
	{
		#region 私有变量及控件

        private DataEntity.DAEPaymentRequest dataEntity = new DataEntity.DAEPaymentRequest();
		private ReportDocument rptDocument ;
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			SetModuleProperty();
			
			if(!IsPostBack)
			{
				InitControlsCultureInfo ();
			}			
			LoadReport();
		}

		#region PageLoad函数

		#region set module property

		/// <summary>
		/// 设置模块名称(在数据库中对应的Module表)
		/// </summary>
		private void SetModuleProperty()
		{
			this.ModuleID = "WareHouse.Receive" ;		
			if(!IsPostBack)
			{
				if((Request["pkValue"] == null || Request["pkValue"].Trim().Length == 0))
				{
					CurrentPageState = PageState.State_New;
				}
				else
				{
					CurrentPageState = PageState.State_Edit;
					PKValue = Request.QueryString.Get("PKValue") ;
				}
			}
		}
		#endregion

		private void LoadReport()
		{
			rptDocument =new ReportDocument() ;

            // 查看是否有缓存的Sql语句来绑定(水晶报表需要在PageLoad绑定)
            string sPKValue = this.Request.QueryString.Get("pkValue");

            rptDocument.Load(Server.MapPath("Report_PRPrint.rpt"));	
     			
			if (CurrentPageState == PageState.State_Edit) 
			{
				BindReport(sPKValue);
			}
		}
		private void InitControlsCultureInfo ()
		{
			lbltemp.Text= "OneLoad";
		}

		#region 根据条件初始报表的数据
		/// <summary>
		/// 根据条件初始报表的数据
		/// </summary>
		/// <param name="condition"></param>
		private void BindReport( string strWhere )
		{
			PrintOptions option = rptDocument.PrintOptions;
			//option.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;


			DataTable tbl = dataEntity.GetPrintData( strWhere );

			rptDocument.SetDataSource(tbl) ;

			this.CrystalReportViewer1.ReportSource = rptDocument;
			this.CrystalReportViewer1.DataBind() ;
		}
		#endregion

		#endregion
						
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

		}
		#endregion
	}
}
