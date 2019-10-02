<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Page language="c#" Codebehind="UserAndRole_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.UserAndRole_Edit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
	</HEAD>
	<body id="thebody" MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<!--
			<asp:panel id="pnlQuery" Visible="False" Runat="server">
				
				<asp:datagrid id="dgrdList" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="IDKey"
					HorizontalAlign="Center" CellPadding="2" PageSize="20" BorderColor="Gray" CssClass="dgrdGlobal">
					<SelectedItemStyle CssClass="dgrdSelectedItem"></SelectedItemStyle>
					<AlternatingItemStyle CssClass="dgrdAlterItem"></AlternatingItemStyle>
					<ItemStyle CssClass="dgrdItem"></ItemStyle>
					<HeaderStyle CssClass="dgrdHeader"></HeaderStyle>
					<Columns>
						<asp:HyperLinkColumn Target="_blank" DataTextField="IDKey" HeaderText="UserID">
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:HyperLinkColumn>
						<asp:BoundColumn DataField="FullName" ReadOnly="True" HeaderText="UserName">
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
						</asp:BoundColumn>
						<asp:ButtonColumn Text="Edit" CommandName="Select">
							<ItemStyle Width="40px"></ItemStyle>
						</asp:ButtonColumn>
						<asp:ButtonColumn Text="Delete" CommandName="Delete">
							<ItemStyle Width="40px"></ItemStyle>
						</asp:ButtonColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" CssClass="dgrdPager" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
				<TABLE width="100%" align="center">
					<TR>
						<TD>
							<webdiyer:AspNetPager id="pager" runat="server" Width="100%" HorizontalAlign="Right" PageSize="15" CssClass="mypager"
								PagingButtonSpacing="2px" Height="25px" SubmitButtonStyle="border:1px solid #000066;height:20px;width:30px"
								InputBoxStyle="border:1px #0000FF solid;text-align:center" SubmitButtonText="OK" NumericButtonTextFormatString="[{0}]"
								ShowCustomInfoSection="left" ImagePath="../../Images/aspnetpager" ButtonImageNameExtension="n" CpiButtonImageNameExtension="r"
								DisabledButtonImageNameExtension="g" TextBeforeInputBox="Turn To Page" InvalidPageIndexErrorString="the page index is invalid"
								NavigationToolTipTextFormatString="Turn to page {0}" PagingButtonType="Image"></webdiyer:AspNetPager></TD>
					</TR>
				</TABLE>
			</asp:panel>-->
			<asp:panel id="pnlDetails" Runat="server">
				<TABLE class="TableBlueBorderWhiteBg" width="100%" align="center" border="0">
					<COLGROUP>
						<COL align="right" width="100">
						<COL align="left" width="*">
						<COL align="left" width="100">
					</COLGROUP>
					<TR>
						<TD class="TitleText1" align="center" colSpan="3" >
							<asp:Literal id=litModifyInfo Runat="server"  Text='<%#GetString("litInfoDetails")%>'>
							</asp:Literal></TD>
					</TR>
					<TR>
						<TD class="Seperator" colSpan="3"><FONT face="ËÎÌו"></FONT></TD>
					</TR>
					<TR>
					</TR>
					<TR>
						<TD vAlign="top">
							<asp:literal id=litDepartment Runat="server" Text='<%#GetString("litDepartment")%>'>
							</asp:literal></TD>
						<TD>
							<asp:DropDownList id="ddlDepartmentList" runat="server" Width="100%" AutoPostBack="True" onselectedindexchanged="ddlDepartmentList_SelectedIndexChanged"></asp:DropDownList></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD vAlign="top">
							<asp:literal id=litUserID Runat="server" Text='<%#GetString("litUserID")%>'>
							</asp:literal></TD>
						<TD><FONT face="ËÎÌו">
								<asp:DropDownList id="ddlUserList" runat="server" Width="100%"></asp:DropDownList></FONT></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD vAlign="top">
							<asp:literal id=litAssignRoles Runat="server" Text='<%#GetString("litAssignRoles")%>'>
							</asp:literal></TD>
						<TD>
							<asp:CheckBoxList id="chklstRoles" Runat="server" Width="100%" BorderColor="Gray" RepeatLayout="Table"
								RepeatDirection="Horizontal" RepeatColumns="3" BorderStyle="Solid" BorderWidth="1" BackColor="white"></asp:CheckBoxList></TD>
						<TD><FONT face="ËÎÌו"></FONT></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
		<asp:literal id="litScript" Runat="server"></asp:literal>
		<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
			<TR vAlign="top">
				<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
			</TR>
		</TABLE>
	</body>
</HTML>
