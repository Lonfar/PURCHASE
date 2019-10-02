<%@ Page language="c#" Codebehind="ApprovedBusinesProcedureView.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.ApproveProcess.ApprovedBusinesProcedureView" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServiceRequistion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
  
        function openWin(VoucherID,ModuleID,PKValue)   
		{   
			window.open("../UserControls/View.aspx?ID="+VoucherID+"&ModuleID="+ModuleID+"&PKValue="+PKValue,"win","toolbar=no,location=no,directories=no,status=no,scrollbars=yes,menubar=no,resizable=yes,copyhistory=yes,width=800,height=500,top=120,left=100");	
		}   


		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<td width="10"></td>
					<TD><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar>
					</TD>
				</TR>
			</TABLE>
			<TABLE class="TitleArea" id="Table4" cellSpacing="1" cellPadding="1" width="95%" align="center"
				border="0">
				<tr>
					<td class="TitleText1"><asp:label id="lbTitle" runat="server"></asp:label>
					</td>
				</tr>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD align="center"><asp:datagrid id="DataGrid1" runat="server" CssClass="TableGlobalOne" AllowPaging="True" CellPadding="1"
								AutoGenerateColumns="False">
								<ItemStyle CssClass="TableRow"></ItemStyle>
								<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
								<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
								<HeaderStyle CssClass=""></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="ApprovalTypeName" ItemStyle-Width="100px"></asp:BoundColumn>
									<asp:BoundColumn DataField="ObjectiveTitle" ItemStyle-Width="250px"></asp:BoundColumn>
									<asp:BoundColumn DataField="ApproeLevel" ItemStyle-Width="100px"></asp:BoundColumn>
									<asp:BoundColumn DataField="ApproeDescription" ItemStyle-Width="280px"></asp:BoundColumn>
									<asp:TemplateColumn>
										<ItemStyle Height="20px" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a href="#" onclick='JavaScript:openWin("vch_TI_ApproveFlow","TenderConfig.ApproveFlow","<%# DataBinder.Eval(Container.DataItem,"IDKey")%>")'>
												<img border="0" src='../Images/Grid/book1_open.gif'> </a>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" Mode="NumericPages"
									Height="25"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<tr>
						<td height="20"></td>
					</tr>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
