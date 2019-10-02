<%@ Page language="c#" Codebehind="OnlineUser.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Account.OnlineUser" Trace="false"%>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">
		<form id="Form1" method="post" runat="server">
			<TOPIS:PAGEDESCRIPTION id="PageDescriptionAFE" runat="server"></TOPIS:PAGEDESCRIPTION>
			<br>
			<TABLE width="95%" align="center" border="0" cellpadding="0" Class="TitleArea">
				<TR>
					<TD class="TitleText1">
						<asp:Literal Runat="server" ID="litOnlineUsers" Text='<%#GetString("litOnlineUsers")%>'>
						</asp:Literal>
					</TD>
				</TR>
				<tr align="right">
					<td align="right"><asp:Literal Runat="server" ID="litOnlineUserDesc"></asp:Literal></td>
				</tr>
			</TABLE>
			<asp:datagrid id="dgrdList" runat="server" EnableViewState="False" AutoGenerateColumns="False"
				Width="95%" HorizontalAlign="Center" CssClass="TableGlobalOne" GridLines="None" CellPadding="2"
				CellSpacing="1">
				<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
				<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
				<ItemStyle CssClass="TableRow"></ItemStyle>
				<HeaderStyle HorizontalAlign="Center" CssClass="TableHeader"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="SessionID" ReadOnly="True" HeaderText="gcSessionID">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="LoginTime" ReadOnly="True" HeaderText="gcLogTime" DataFormatString="{0:g}">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="UserID" ReadOnly="True"></asp:BoundColumn>
					<asp:HyperLinkColumn Target="_blank" DataNavigateUrlField="UserID" DataNavigateUrlFormatString="../Account/Profile.aspx?UserID={0}"
						DataTextField="UserID" HeaderText="gcUser">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:HyperLinkColumn>
					<asp:BoundColumn DataField="UserName" ReadOnly="True" HeaderText="gcUserName">
						<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="RefreshModule" ReadOnly="True" HeaderText="gcModuleID">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="LoginIP" ReadOnly="True" HeaderText="gcLogIP">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Platform" ReadOnly="True" HeaderText="gcPlatform">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="BrowserVersion" ReadOnly="True" HeaderText="gcBrowserVersion">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Language" ReadOnly="True" HeaderText="gcLanguage">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="RefreshTime" ReadOnly="True" HeaderText="gcRefreshTime" DataFormatString="{0:HH:mm:ss}">
						<ItemStyle HorizontalAlign="Left"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" CssClass="dgrdPager" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</form>
	</body>
</HTML>
