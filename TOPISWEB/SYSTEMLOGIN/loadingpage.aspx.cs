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
using Cnwit.Utility;

namespace TopisWeb
{
	/// <summary>
	/// loadingpage add by wxc at 2008/03/14��
	/// </summary>
	public partial class loadingpage : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		public string getHref()
		{
			return Server.UrlDecode(Request.QueryString["ModuleUrl"].ToString().Trim());
		}

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
