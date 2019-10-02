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
using System.Reflection;
using Common;
using Business;
using DataEntity;
using System.IO;
using DictionaryAccess;
using UserControls;

namespace TopisWeb.LogisticsManagement.Process
{
	/// <summary>
	/// PaymentRequest_Edit ��ժҪ˵����
	/// </summary>
	public partial class PaymentRequest_Edit :PageBase,IPageEdit
	{
		/// <summary>
		/// ���ݹ������ͻ���
		/// </summary>
		
		#region ˽�б������ؼ�

		protected MSPlus.Web.UI.WebControls.ToolBar ToolBar1;
		protected UserControls.UCEdit ucEdit_PaymentRequest ;
		protected UserControls.AttachmentManager AttachmentManager1;
		private ArrayList _list=null;
		private DataEntity.DAEPaymentRequest _daePaymentRequest = new DAEPaymentRequest();
		string strSql=string.Empty;
		private BUSPaymentRequest _busPaymentRequest = new BUSPaymentRequest();
		private DataEntity.CEntityUitlity pEntityUitlity =new CEntityUitlity();
		//protected UserControls.ChildEditControl child_PRForPSW ;
		protected UserControls.UCEdit ucEdit_PaymentRequest1 ;
        protected UserControls.UCList ucList_PaymentRequest;
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
			this.ToolBar1.MenuItemClick += new MSPlus.Web.UI.WebControls.MenuItemClickedEventHandler(this.ToolBar1_MenuItemClick);
			this.ucEdit_PaymentRequest.VoucherItemSelected+=new VoucherItemSelectedHandler(ucEdit_PaymentRequest_VoucherItemSelected);
			//this.child_PRForPSW.UpdateData +=new UpdateDataToDataTable(child_PRForPSW_UpdateData);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetModuleProperty();
			InitControlsCultureInfo();
			if(!IsPostBack)
			{
				ShowEdit();
			}
			CreateToolMenu(ToolBar1,TopisWeb.ToolMenuType.TYPE_Edit) ;
			EventBind();
			SetLabelStatus();
			
		}


		#region ҳ��������躯��

		#region Set module property 

		/// <summary>
		/// ����ģ�������
		/// </summary>
		private void SetModuleProperty()
		{						
			this.ModuleID = Request.QueryString.Get("moduleID") ;
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

		private void ShowEdit()
		{

			//����༭
			ucEdit_PaymentRequest.EditID = "vch_PaymentRequest";			
			ucEdit_PaymentRequest.PKValue = PKValue;
            //if(Request.QueryString.Get("PRType") != null)
            //{
            //    switch (Request.QueryString.Get("PRType").ToString())
            //    {
            //        case "1":
            //            child_PRForPSW.ChildEditID = "Child_PRForPSW_PO" ; 
            //            child_PRForPSW.ParentKey = PKValue ;
            //            break ;
            //        case "2":
            //            child_PRForPSW.ChildEditID = "Child_PRForPSW" ; 
            //            child_PRForPSW.ParentKey = PKValue ;
            //            break ; 
            //        case "3":
            //            child_PRForPSW.ChildEditID = "Child_PRForPSW_WO" ; 
            //            child_PRForPSW.ParentKey = PKValue ;
            //            break;

            //    }

            //}

			

            //ucEdit_PaymentRequest1.EditID = "vch_LOG_PRDate" ;
            //ucEdit_PaymentRequest1.PKValue = PKValue ; 
            //ucEdit_PaymentRequest1.ShowTitle = false ;
            ucList_PaymentRequest.ListID = "vch_PaymentRequest";
            ucList_PaymentRequest.ShowApproveCommand = false;
            ucList_PaymentRequest.ShowDeleteCommand = false;
            ucList_PaymentRequest.ShowEditCommand = false;
            ucList_PaymentRequest.ShowListCheck = false;
            ucList_PaymentRequest.NeedSearch = false;
            ucList_PaymentRequest.ShowViewCommand = false;
			
		}
		private void SetLabelStatus()
		{
			this.lbError.Visible = false;
		}

		#region �¼�����
		private void EventBind()
		{
			EditObserve oo=new EditObserve(this);
			oo.Business = _busPaymentRequest;
			oo.DataAcess = _daePaymentRequest;
		}
		#endregion

		#region InitControlsCultureInfo

		private void InitControlsCultureInfo()
		{
			lblMSG.Text=this.GetPublicString("string","Status");
			SetTabText();
		}

		#endregion

		
		#region SetTabText
		private void SetTabText()
		{
			this.TabControl1.Items[0].Caption = this.GetString("Payment");
            //if(Request.QueryString.Get("PRType") != null)
            //{
            //    switch (Request.QueryString.Get("PRType").ToString())
            //    {
            //        case "1":
            //        this.TabControl1.Items[1].Caption = this.GetString("PRForPO");
            //            break;
            //        case "2":
            //            this.TabControl1.Items[1].Caption = this.GetString("PRForSDT");
            //            break;
            //        case "3":
            //            this.TabControl1.Items[1].Caption = this.GetString("PRForWO");
            //            break;
            //    }
            //}
		}
		#endregion



		#endregion

		#region �ӿڳ�Ա

		#region IPageSaveDate ��Ա

		public event Common.OperateDataEventHandler DoSaveData;

		#endregion

		#region ICancel ��Ա

		public void CancelOperate()
		{
			ReturnListPage();
		}
		private void ReturnListPage()
		{			
			this.Response.Redirect("PaymentRequest.aspx?moduleID="+ModuleID+"&PRType="+Request.QueryString.Get("PRType"));
		}
		#endregion

		#region IPageDeleteData ��Ա

		public event Common.OperateDataEventHandler DoDeleteData;

		#endregion

		#region IPageMessage ��Ա

		public void DisplayMessage(string message)
		{
			// Modified by Liujun at 20071218
			this.lblMSG.Text += this.GetDisplayMessage ( message );
		}

		#endregion

		#region IPageQueryData ��Ա

		public event Common.OperateDataEventHandler DoQueryData;

		#endregion

		#region IHelp ��Ա

		public void Help()
		{
			ShowHelp() ;// TODO:  ��� BasicInfoSex_Edit.Help ʵ��
		}

		#endregion

		#endregion

		#region ���ݲ�����

		/// <summary>
		/// ����༭�ؼ�����������(һ��Ҫ���,ͨ��ҵ������ʵ��㷽��������)
		/// </summary>
		/// <param name="dtEdit">�༭����Edit��</param>
		/// <returns></returns>
		private void HandleMainTable( DataTable dtEdit )
		{
            if (this.CurrentPageState == PageState.State_New)
            {
                dtEdit.Rows[0]["MR_PaymentRequest.PRNo"] = this._daePaymentRequest.GetPRNO(this.TimeNO());
            }
		}

		private int GetLastState(DataTable dtEdit)
		{
            //Cnwit.Web.UI.WebControls.DatePicker datePicker1 = ucEdit_PaymentRequest1.FindControl("date_LOG_PR__PaymentDate") as Cnwit.Web.UI.WebControls.DatePicker ; 
            //DataTable dt_PRForPSW = child_PRForPSW.CurrentDataTable ;
			
            //if(CheckPRState(dt_PRForPSW))
            //{
			
            //    if(datePicker1 != null && datePicker1.Value.Length >0 )
            //    {
            //        return (int)PRState.Close;
            //    }
            //}
            return -1;
		}

		private bool ChangeState(DataTable dtEdit)
		{
			int state = GetLastState(dtEdit) ; 
			if(state != -1)
			{
				dtEdit.Rows[0]["LOG_PR.Status"]  = state.ToString() ;
			}
			return true;

		}

		private bool CheckPRState(DataTable dt_PRForPSW)
		{
			
			if(dt_PRForPSW != null && dt_PRForPSW.Rows.Count > 0)
			{
				switch (Request.QueryString.Get("PRType").ToString())
				{
					case "1":
					 
						return LoopCheck(dt_PRForPSW , "LOG_PR_ForPSW.PaymentPO");
					case "2":
						return LoopCheck(dt_PRForPSW , "LOG_PR_ForPSW.PaymentSDT");
					case "3":
						return LoopCheck(dt_PRForPSW , "LOG_PR_ForPSW.PaymentWO");
				}
				
			}
			return false ;
		}

		private bool LoopCheck(DataTable dt , String fieldName)
		{
			int flag = 0;
			ArrayList list = GetList(dt , fieldName);
			String rowFlag = String.Empty ;
			rowFlag = dt.Rows[0][fieldName].ToString();
			int flagInt = 0 ;
			for(int i=0 ; i< list.Count ; i++)
			{
				flag += LoopCheckTwo(dt ,list[i].ToString().Trim() , fieldName ) ; 
				flagInt ++ ;
			}
			return flag == flagInt ;  
		}

		private ArrayList GetList(DataTable dt , String fieldName)
		{
			ArrayList list = new ArrayList();
			String rowFlag = String.Empty ;
			rowFlag = dt.Rows[0][fieldName].ToString();
			list.Add(rowFlag);
			foreach( DataRow row in dt.Rows)
			{
				if( row.RowState != DataRowState.Deleted)
				{
					if( ! list.Contains(row[fieldName].ToString().Trim()))
					{
						list.Add(row[fieldName].ToString().Trim());
					}
				}
			}
			return list ;

		}

		private int LoopCheckTwo(DataTable dt , String strList , String fieldName)
		{
			foreach( DataRow row in  dt.Rows)
			{
				if( row.RowState != DataRowState.Deleted)
				{
					if( row[fieldName].ToString().Trim() == strList && (bool)row["LOG_PR_ForPSW.IsCompleted"]  )
					{	
							return 1 ; 
					}
				}
			}
			return 0 ;

		}
		/// <summary>
		/// �����ӱ�ؼ�����������
		/// </summary>
		/// <param name="dtChild">�ӱ�ؼ�Edit��</param>
		/// <returns></returns>
		private void HandlePRForPSW(DataTable dtChild)
		{
			/*
			 * ������Դ����ӱ��ֵ
			 * */
		}
			



		/// <summary>
		/// ����ǰ��һ��ҵ�����У��
		/// </summary>
		/// <returns></returns>
		private string CheckBusinessData()
		{
			string sErrMsg = string.Empty ;
			/*
			 * ���Լ�������һЩ����ǰ���߼�ҵ��У�飬��������
			 *	sErrMsg = this.busBorrowEdit.������(����ֵ) ;
				if(sErrMsg.Length > 0)
				{
					return sErrMsg ;
				}
			 * 
			 * 
			 * 
			 * */
			return "";
		}

		#region ����

		/// <summary>
		/// ���ݱ���
		/// </summary>
		private void OnSaveData()
		{
			string errMessage="";
			OperateDataEventArgs ode = new OperateDataEventArgs();
			if (DoSaveData!=null)
			{
				//��ȡ��������
				DataTable dt= ucEdit_PaymentRequest.dtEditDataCollection;
                //DataTable dt1= ucEdit_PaymentRequest1.dtEditDataCollection;
                ////������������
                HandleMainTable(dt);
				//������������
				errMessage = SaveDataToTables(ode,dt,"MR_PaymentRequest","IDKey","PRNo");

				if (errMessage.Trim().Length == 0 )
				{		
					//��ȡ��������(�̻�)
					PKValue = ode.CurDataTable.Rows[0][ode.TableName+"."+ode.PKField].ToString() ;
					if (this.CurrentPageState == PageState.State_New)
					{
						//���ÿؼ�״̬��ҳ��״̬
						this.CurrentPageState = PageState.State_Edit ;
					}	
                    ////�����һ���ӱ�
                    ////��ȡ�ӱ�����
                    //DataTable dt_PRForPSW = child_PRForPSW.CurrentDataTable ;
                    ////���ӱ������
                    //child_PRForPSW.SetFKValue(dt_PRForPSW,PKValue); 
                    ////�����ӱ�����
                    //HandlePRForPSW(dt_PRForPSW);
                    ////�����ӱ�����
                    //String busFields = GetCheckFields();
                    //errMessage += SaveDataToTables(ode,dt_PRForPSW , "LOG_PR_ForPSW" , "PRPSWIDKey" , busFields);
				}

				//���δ��벻��Ҫ�޸�
				if ( errMessage.Trim().Length == 0 )															
				{
					ReturnListPage();			
				}
				else
				{
					DisplayMessage(errMessage);
				}				
								
			}		
		}				

		/// <summary>
		/// ���渽��
		/// </summary>
		/// <param name="PKValue"></param>
		void OnSaveAttachments(string PKValue)
		{
			AttachmentManager1.InfoID = PKValue;
			if(AttachmentManager1.UpdateAttachmentListToDatabase())
			{

			}
		}

		/// <summary>
		/// save parent and children  tables
		/// </summary>
		/// <param name="ode"></param>
		/// <returns></returns>
		private string SaveDataToTables(OperateDataEventArgs ode,DataTable dt,
			string tbName,string pkName,string busPKName )
		{
			ode.CurDataTable = dt;
			ode.TableName = tbName;
			ode.PKField = pkName;
			ode.BusPKFieldName = busPKName;
			return DoSaveData(ode);							
		}

		#endregion
		private String GetCheckFields()
		{
			switch (Request.QueryString.Get("PRType").ToString())
			{
				case "1":
					return "PRID,PaymentPO,PayBatch";
					
				case "2":
					return "PRID,PaymentSDT,PayBatch";
			
				case "3":
					return "PRID,PaymentWO,PayBatch";
				
			}
			return "";
		}
		#endregion 

		#region �¼�������

		#region toolbar event

		private void ToolBar1_MenuItemClick(object sender, MSPlus.Web.UI.WebControls.MenuItemClickEventArgs e)
		{
			
			string sErrMsg = string.Empty ;

			switch(e.EventItem.ID.ToString().ToLower())
			{
				case "tbsave":	
					this.Validate();
					if (this.IsValid)
					{
						sErrMsg = CheckBusinessData();
						if(sErrMsg.Trim() == "")
						{
							OnSaveData();
						}
						else
						{
							DisplayMessage(sErrMsg) ; 
						}
					}	
					break ;
				case "tbcancel":
					CancelOperate();		
					break ;				
				case "tbhelp":
					Help();
					break;
			}
		}
		#endregion

		public void ucEdit_PaymentRequest_VoucherItemSelected(object sender, EventArgs e)
		{
            if (sender is RefEditor)
            {
                RefEditor refSender = (RefEditor)sender;

                string POID = refSender.RefCodeValue;

                // Get the PO Amount & PO Currency
                DAEPOBidFlow POdataEntity = new DAEPOBidFlow();

                DataTable dt = POdataEntity.GetPrintData(POID);

                TextBox txtPOAmount = this.ucEdit_PaymentRequest.FindControl("txt_MR_PaymentRequest__POAmount") as TextBox;
                if (txtPOAmount != null && dt.Rows.Count > 0 )
                {
                    Decimal amount = Decimal.Parse(dt.Rows[0]["Amount"].ToString());

                    txtPOAmount.Text = amount.ToString("N4");
                }

                DDLRefrence refTemp = this.ucEdit_PaymentRequest.FindControl("cboref_MR_PaymentRequest__POCur") as DDLRefrence;
                if (refTemp != null && dt.Rows.Count > 0 )
                {
                    DropDownList ddlTemp = refTemp.FindControl("Refdrop") as DropDownList;
                    if (ddlTemp != null)
                    {
                        ddlTemp.SelectedValue = dt.Rows[0]["ContractTotalCostCUR"].ToString();
                    }
                }

                refTemp = this.ucEdit_PaymentRequest.FindControl("cboref_MR_PaymentRequest__InvoiceCur") as DDLRefrence;
                if (refTemp != null && dt.Rows.Count > 0)
                {
                    DropDownList ddlTemp = refTemp.FindControl("Refdrop") as DropDownList;
                    if (ddlTemp != null)
                    {
                        ddlTemp.SelectedValue = dt.Rows[0]["ContractTotalCostCUR"].ToString();
                    }
                }

                RefEditor refVendorID = ucEdit_PaymentRequest.FindControl("ref_MR_PaymentRequest__VendorID") as RefEditor;
                if (refVendorID != null && dt.Rows.Count > 0)
                {
                    refVendorID.RefCodeValue = dt.Rows[0]["BidderID"].ToString();
                    refVendorID.RefNameValue = dt.Rows[0]["Name"].ToString();
                }

            }
		}

		private void child_PRForPSW_UpdateData(object sender, EventArgs e)
		{
//			DataTable dt_PRForPSW = this.child_PRForPSW.OutPutDataTable;
//			if (dt_PRForPSW != null )
//			{
//				_busPaymentRequest.UpdatePRForPSW(dt_PRForPSW);
//				child_PRForPSW.Refresh(ref dt_PRForPSW);			    
//			}
		}

		#endregion

		#region OnPreRender
		protected override void OnPreRender(EventArgs e)
		{
			SetControlProperty();
			SetPSWFilter();
			InitEditControlValue();
			ApproveControl();
			SetControlEnable();
			base.OnPreRender (e);
		}
		#endregion

		#region OnPreRender ���غ�����

		private void SetControlProperty()
		{
            RefEditor refPOID = ucEdit_PaymentRequest.FindControl("ref_MR_PaymentRequest__POID") as RefEditor;

            if (refPOID != null)
            {
                this.ucList_PaymentRequest.Title = "PO Related Payment";
                this.ucList_PaymentRequest.FilterSqlWhere = "POID = '" + refPOID.RefCodeValue + "'";
                this.ucList_PaymentRequest.RefreshGrid();
            }
            //DDLRefrence drpState = ucEdit_PaymentRequest.FindControl("cboref_LOG_PR__PRTypeID") as  DDLRefrence ; 
            //if(drpState != null)
            //{
            //    DropDownList ddlList = drpState.FindControl("Refdrop") as DropDownList ;
            //    if(ddlList != null)
            //    {
            //        ddlList.SelectedValue = Request.QueryString.Get("PRType").ToString();
            //    }
            //}

            //DDLRefrence cborefStatus = ucEdit_PaymentRequest.FindControl("cboref_LOG_PR__Status") as  DDLRefrence ; 
            //DataTable dt = ucEdit_PaymentRequest.dtEditDataCollection ; 
//			if(cborefStatus != null)
//			{
//				DropDownList ddlList = cborefStatus.FindControl("Refdrop") as DropDownList ;
//				if(ddlList != null)
//				{
//					for( int i = 0 ; i < ddlList.Items.Count ; i++)
//					{
//						if( ddlList.Items[i].Value == Convert.ToString(((int)PRState.Close)))
//						{
//							ddlList.Items.RemoveAt(i) ; 
//							break ;
//						}
//					}
//				}
//			}
            //if(cborefStatus != null)
            //{
            //    if( dt != null && dt.Rows.Count > 0)
            //    {
            //        if( !dt.Rows[0]["LOG_PR.Status"].ToString().Equals(Convert.ToString((int)PRState.Close)) )
            //        {
					
            //            cborefStatus.WhereSql = "LOG_PR_Status.PRIDKey <> '"+((int)PRState.Close).ToString()+"'";
            //            cborefStatus.Refresh();
            //        }
            //    }
				
            //}


		}
		
		
		private void SetPSWFilter()
		{
			string controlID = String.Empty ; 
			string strSql = String.Empty ; 
			if(Request.QueryString.Get("PRType") != null)
			{
				switch (Request.QueryString.Get("PRType").ToString())
				{
					case "1":
                        //controlID = "ref_PaymentPO";
                        //strSql = " PurchaseOrder.ApproveStatus="+(int)MRState.State_POSinged;
                        //SetPSWFilter(controlID ,strSql );
						break ;
					case "2":
//						controlID = "ref_LOG_PR_ForPSW__PaymentSDT";
//						strSql = " LOG_SDT.SDTIDKey ="+(int)SDTState.CompletedDate;
//						SetPSWFilter(controlID ,strSql );
						break ; 
					case "3":
//						controlID = "ref_LOG_PR_ForPSW__PaymentWO";
//						strSql = " LOG_WO.Status <> '"+(int)WOStatus.Closed+"'";
//						SetPSWFilter(controlID ,strSql );
						break;

				}

			}
			
		}

		private void SetPSWFilter(String controlID , String strSql)
		{
            //foreach( DataGridItem item in child_PRForPSW.DataGridItems)
            //{
            //    RefEditor ref_Control =  item.FindControl(controlID) as RefEditor;
            //    if (ref_Control != null)
            //    {
            //        ref_Control.WhereSql = strSql ; 
            //        ref_Control.Refresh();
            //    }
            //}
		}
		private void InitEditControlValue()
		{
			//����״̬�µĿؼ�����
			
                if (this.CurrentPageState == PageState.State_New)
                {
                    //��ʼ��������
                    RefEditor ref_CreateBy = ucEdit_PaymentRequest.GetControl("ref_MR_PaymentRequest__CreateBy") as RefEditor;
                    if (ref_CreateBy != null)
                    {
                        ref_CreateBy.RefCodeValue = CurrentUser.UserID;
                        ref_CreateBy.RefNameValue = CurrentUser.UserName;
                    }

                }
			
		}
		protected void ApproveControl()
		{
			if ( this.CurrentPageState == PageState.State_Edit )
			{
				
                //DataTable dtEnquiryPrice = ucEdit_PaymentRequest.dtEditDataCollection;
                //if(dtEnquiryPrice.Rows[0]["LOG_PR.Status"] == DBNull.Value) return ;
                //switch ( Convert.ToInt32(dtEnquiryPrice.Rows[0]["LOG_PR.Status"].ToString()))
                //{
                //    case (int)PRState.Close:
                //    {
                //        this.ToolBar1.Items[0].Visable = false;
                //        break;
                //    }
                //}
			}
		}
		private void CalculationTotal(DataTable dtEdit)
		{
            //if(dtEdit != null)
            //{
            //    decimal total = 0;
            //    DataTable dt = child_PRForPSW.CurrentDataTable ; 
            //    if (dt != null)
            //    {
            //        foreach (DataRow row in dt.Rows)
            //        {
            //            if ( row.RowState != DataRowState.Deleted )
            //            {
            //                total += row["LOG_PR_ForPSW.PaidValue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LOG_PR_ForPSW.PaidValue"]);
            //            }
            //        }
            //    }
            //    dtEdit.Rows[0]["LOG_PR.DestinationID"] = total ;
            //}

		}
		private void SetControlEnable()
		{
			//�༭״̬�µĿؼ�����
			if(this.CurrentPageState == PageState.State_Edit)
			{
				//����ҵ������
				VoucherControl.SetEndableToControl(ucEdit_PaymentRequest,"txt","LOG_PR","PRNO");
			}
		}
		#endregion

	
	}
}

