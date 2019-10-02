<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UCEditSearch" Src="../../UserControls/UCEditSearch.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Page language="c#" Codebehind="POSearch.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouseManagment.POSearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>POSearch</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/DatePicker.js"></SCRIPT>
		<SCRIPT language="javascript">
  
        function openWin(VoucherID,ModuleID,PKValue)   
		{   
			window.open("../../UserControls/View.aspx?ID="+VoucherID+"&ModuleID="+ModuleID+"&PKValue="+PKValue,"win","toolbar=no,location=no,directories=no,status=no,scrollbars=yes,menubar=no,resizable=yes,copyhistory=yes,width=800,height=500,top=120,left=100");	
		}   

		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD colSpan="4">
							<P><uc2:uceditsearch id="UCEditSearch" runat="server"></uc2:uceditsearch></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD>
							<DIV style="WIDTH: 100%; POSITION: relative; HEIGHT: 100px" ms_positioning="GridLayout">
								<TABLE class="TitleArea" width="100%" align="center" border="0">
									<TR>
										<TD class="TitleText1" align="center"><asp:label id="lbTitle" runat="server" Font-Size="Medium"></asp:label></TD>
									</TR>
								</TABLE>
								<table id="VoucherList_tbMain" width="100%" border="0">
									<tr>
										<td>
											<asp:datagrid id="DG_POSearch" runat="server" CssClass="TableGlobalOne" AllowSorting="True" AllowPaging="True"
												AutoGenerateColumns="False" PageSize="15" Width="100%">
												<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
												<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
												<ItemStyle CssClass="TableRow"></ItemStyle>
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<Columns>
													<asp:BoundColumn>
														<ItemStyle HorizontalAlign="Center" Height="23px" Width="5%" VerticalAlign="Middle"></ItemStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn>
														<ItemStyle Height="23px" Width="15%"></ItemStyle>
														<ItemTemplate>
															<a class="BlackLink" href="#" onclick='JavaScript:openWin("vch_PurchaseOrder","ProcurementManagement.ContractDatabase.PurchaseOrder","<%# DataBinder.Eval(Container.DataItem,"ThePOID")%>")'>
																<%# DataBinder.Eval(Container.DataItem,"ThePOID")%>
															</a>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="ThePOPurpose" SortExpression="ThePOPurpose">
														<ItemStyle HorizontalAlign="Left" Height="23px" Width="25%" VerticalAlign="Middle"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="TheItemCode" SortExpression="TheItemCode">
														<ItemStyle HorizontalAlign="Center" Height="23px" Width="15%" VerticalAlign="Middle"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="TheMaterialName" SortExpression="TheMaterialName">
														<ItemStyle HorizontalAlign="Left" Height="23px" Width="25%" VerticalAlign="Middle"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="ThePOQuantity" SortExpression="ThePOQuantity">
														<ItemStyle HorizontalAlign="Center" Height="23px" Width="8%" VerticalAlign="Middle"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="TheReceiveBaseQuantity" SortExpression="TheReceiveBaseQuantity">
														<ItemStyle HorizontalAlign="Center" Height="23px" Width="8%" VerticalAlign="Middle"></ItemStyle>
													</asp:BoundColumn>
												</Columns>
												<PagerStyle Visible="False"></PagerStyle>
											</asp:datagrid>
										</td>
									</tr>
									<tr>
										<td>
											<WEBDIYER:ASPNETPAGER id="pager" runat="server" CssClass="mypager" PageSize="15" Width="100%" ShowInputBox="Always"
												PagingButtonSpacing="4px" SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px"
												InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
												SubmitButtonText="Submit" NumericButtonTextFormatString="[{0}]" ShowCustomInfoSection="left" ImagePath="../../Images/aspnetpager/"
												ButtonImageNameExtension="n" CpiButtonImageNameExtension="r" DisabledButtonImageNameExtension="g"
												TextBeforeInputBox="Turn To " InvalidPageIndexErrorString="the page index is invalid" PageIndexOutOfRangeErrorString="page index out of range"
												NavigationToolTipTextFormatString="Turn To Page {0}" PagingButtonType="Image" Height="25px" HorizontalAlign="Right"></WEBDIYER:ASPNETPAGER>
										</td>
									</tr>
								</table>
							</DIV>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
