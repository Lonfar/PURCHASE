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
	/// ҳ������: ��������
	/// ҳ������: ��ӡ����
	/// �༭����: 2007-10-22
	/// �༭״̬: ����
	/// ������Ա: ����
	/// </summary>
	public partial class PRPrint : PageBase
	{
		#region ˽�б������ؼ�

        private DataEntity.DAEPaymentRequest dataEntity = new DataEntity.DAEPaymentRequest();
		private ReportDocument rptDocument ;
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			SetModuleProperty();
			
			if(!IsPostBack)
			{
				InitControlsCultureInfo ();
			}			
			LoadReport();
		}

		#region PageLoad����

		#region set module property

		/// <summary>
		/// ����ģ������(�����ݿ��ж�Ӧ��Module��)
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

            // �鿴�Ƿ��л����Sql�������(ˮ��������Ҫ��PageLoad��)
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

		#region ����������ʼ���������
		/// <summary>
		/// ����������ʼ���������
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
						
		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
