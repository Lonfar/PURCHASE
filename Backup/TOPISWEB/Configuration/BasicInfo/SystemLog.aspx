<%@ Page language="c#" Codebehind="SystemLog.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Configuration.SystemLog" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register TagPrefix="uc1" TagName="DateEditor" Src="../../UserControls/DateEditor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserSelector" Src="../../UserControls/DDLRefrence.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModuleSelector" Src="../../UserControls/DDLRefrence.ascx" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="StyleSkin" runat="server"></TOPIS:StyleSkin>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/DatePicker.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/MenuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/DatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">
		<TOPIS:PAGEDESCRIPTION id="PageDescription" runat="server"></TOPIS:PAGEDESCRIPTION>
		<TABLE width="95%" align="center" border="0" cellpadding="0" Class="TitleArea">
			<TR>
				<TD class="TitleText1" align="center">
					<asp:Literal Runat="server" ID="litUserSystemLog" Text='<%#GetString("SystemLog")%>'>
					</asp:Literal>
				</TD>
			</TR>
		</TABLE>
		<form id="Form1" method="post" runat="server">
			<asp:Panel Runat="server" ID="pnlQuery">
				<TABLE class="TableWithBorder" style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px" cellPadding="2"
					width="95%" align="center" border="0">
					<TR>
						<TD vAlign="middle" noWrap align="left">
							<TABLE style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px" cellPadding="2" width="100%" align="center"
								border="0">
								<TR>
									<TD width="120">
										<uc1:UserSelector id="UserSelector1" runat="server" width="120px"></uc1:UserSelector></TD>
									<TD align="right" width="80">
										<asp:Literal id=litDateFrom Text='<%#GetString("litDateFrom")%>' Runat="server">
										</asp:Literal>:</TD>
									<TD align="right" width="100">
										<cc2:DatePicker id="DateEditor_From" runat="server"></cc2:DatePicker></TD>
									<TD align="center" width="30">
										<asp:Literal id=litDateTo Text='<%#GetString("litDateTo")%>' Runat="server">
										</asp:Literal></TD>
									<TD width="100">
										<cc2:DatePicker id="DateEditor_To" runat="server"></cc2:DatePicker></TD>
									<TD>
										<asp:DropDownList id="ddlLanguage" Runat="server" Width="125px"></asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD noWrap>
							<uc1:ModuleSelector id="ModuleSelector1" runat="server" Width="250px" AutoPostBack="True"></uc1:ModuleSelector>
							<asp:DropDownList id="ddlLogType" Runat="server" Width="125px"></asp:DropDownList>
							<asp:DropDownList id="ddlPlatform" Runat="server" Width="125px"></asp:DropDownList>
							<asp:DropDownList id="ddlBrowserVersion" Runat="server" Width="122px"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD align="right" height="25">
							<asp:LinkButton id=btnSearch Text='<%#GetString("btnSearch")%>' Runat="server" cssclass="graybutton" Font-Bold="True">
							</asp:LinkButton>
							<asp:LinkButton id=btnDeleteSystemLogs Text='<%#GetString("btnDeleteSystemLogs")%>' Runat="server" cssclass="graybutton" Font-Bold="True">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
				<asp:datagrid id="dgrdList" runat="server" Width="95%" AutoGenerateColumns="False" DataKeyField="ID"
					HorizontalAlign="Center" CellPadding="2" BorderColor="gray" CssClass="TableGlobalOne">
					<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
					<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
					<ItemStyle CssClass="TableRow"></ItemStyle>
					<HeaderStyle CssClass="TableHeader"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="LogTime" ReadOnly="True" HeaderText="gcLogTime" ItemStyle-HorizontalAlign="Left"
							DataFormatString="{0:g}"></asp:BoundColumn>
						<asp:HyperLinkColumn DataNavigateUrlField="UserID" DataTextField="UserName" HeaderText="gcUser" Target="_blank"
							ItemStyle-HorizontalAlign="Left" DataNavigateUrlFormatString="~/UserControls/View.aspx?ID=vch_BI_Employee&ModuleID=Configuration.Employee&PKValue={0}"></asp:HyperLinkColumn>
						<asp:BoundColumn DataField="ModuleID" ReadOnly="True" HeaderText="gcModuleID" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="LogType" ReadOnly="True" HeaderText="gcLogType" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="LogDescription" ReadOnly="True" HeaderText="gcLogDescription" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="LogIP" ReadOnly="True" HeaderText="gcLogIP" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="Platform" ReadOnly="True" HeaderText="gcPlatform" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="BrowserVersion" ReadOnly="True" HeaderText="gcBrowserVersion" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="Language" ReadOnly="True" HeaderText="gcLanguage" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="UserID2" ReadOnly="True" Visible="False"></asp:BoundColumn>
					</Columns>
					<PagerStyle Mode="NumericPages" CssClass="dgrdPager" HorizontalAlign="Right"></PagerStyle>
				</asp:datagrid>
				<TABLE width="95%" align="center">
					<TR>
						<TD>
							<webdiyer:AspNetPager id="pager" runat="server" Width="100%" HorizontalAlign="Right" CssClass="mypager"
								PagingButtonType="Image" NavigationToolTipTextFormatString="Turn to page {0}" InvalidPageIndexErrorString="the page index is invalid"
								TextBeforeInputBox="Turn To Page" DisabledButtonImageNameExtension="g" CpiButtonImageNameExtension="r"
								ButtonImageNameExtension="n" ImagePath="../../Images/aspnetpager" ShowCustomInfoSection="left" NumericButtonTextFormatString="[{0}]"
								SubmitButtonText="OK" InputBoxStyle="border:1px #0000FF solid;text-align:center" SubmitButtonStyle="border:1px solid #000066;height:20px;width:30px"
								PageSize="15" Height="25px" PagingButtonSpacing="2px"></webdiyer:AspNetPager></TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</form>
		<asp:literal id="litScript" Runat="server"></asp:literal>
	</body>
</HTML>
