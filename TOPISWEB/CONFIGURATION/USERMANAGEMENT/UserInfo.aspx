<%@ Page language="c#" Codebehind="UserInfo.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Configuration.UserInfo" EnableViewState="false"%>
<%@ Register TagPrefix="Topis" Namespace="Topis.Web.Controls" Assembly="Topis.Web" %>
<%@ Register TagPrefix="uc1" TagName="InfoPageHeader" Src="../../UserControls/InfoPageHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InfoPageTailer" Src="../../UserControls/InfoPageTailer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="StyleSkinAFE" runat="server"></TOPIS:StyleSkin>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body id="thebody">
		<form id="Form1" method="post" runat="server">
			<uc1:InfoPageHeader id="Infopageheader2" runat="server"></uc1:InfoPageHeader>
			<table align="center" width="100%">
				<tr>
					<td width="40"></td>
					<td>
						<table id="tblInfo" cellspacing="0" cellpadding="3" align="center" class="InfoTable" width="100%"
							border="1" bordercolor="#1e84f0">
							<COLGROUP>
								<COL align="right" width="120" bgcolor="aliceblue">
								<COL align="left" width="*" style="COLOR:darkblue">
							</COLGROUP>
							<TR bgcolor="aliceblue">
								<TD>
									<asp:literal id="litUserID" Runat="server" Text='<%#GetString("litUserID")%>'>
									</asp:literal></TD>
								<TD>
									<asp:Label Runat="server" ID="lblUserID"></asp:Label>
								</TD>
							</TR>
							<TR>
								<TD>
									<asp:literal id="litUserName" Runat="server" Text='<%#GetString("litUserName")%>'>
									</asp:literal></TD>
								<TD>
									<asp:Label Runat="server" ID="lblUserName"></asp:Label></TD>
							</TR>
							<TR>
								<TD>
									<asp:literal id="litCanLogin" Runat="server" Text='<%#GetString("litCanLogin")%>'>
									</asp:literal></TD>
								<TD>
									<asp:Label Runat="server" ID="lblCanLogin"></asp:Label></TD>
							</TR>
							<TR>
								<TD>
									<asp:literal id="litLastLoginTime" Runat="server" Text='<%#GetString("litLastLoginTime")%>'>
									</asp:literal></TD>
								<TD>
									<asp:Label Runat="server" ID="lblLastLoginTime"></asp:Label></TD>
							</TR>
							<TR>
								<TD>
									<asp:literal id="litLastLoginIP" Runat="server" Text='<%#GetString("litLastLoginIP")%>'>
									</asp:literal></TD>
								<TD>
									<asp:Label Runat="server" ID="lblLastLoginIP"></asp:Label></TD>
							</TR>
							<TR>
								<TD vAlign="top">
									<asp:literal id="litAssignedRoles" Runat="server" Text='<%#GetString("litAssignedRoles")%>'>
									</asp:literal></TD>
								<TD>
									<table align="center" width="100%" runat="server" id="tblRoles" cellpadding="2" style="BORDER-COLLAPSE:collapse">
									</table>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top">
									<asp:literal id="litAssignedAuthories" Runat="server" Text='<%#GetString("litAssignedAuthorities")%>'>
									</asp:literal></TD>
								<TD>
									<table align="center" width="100%" runat="server" id="tblAuthorities" cellpadding="2" style="BORDER-COLLAPSE:collapse">
									</table>
									<asp:Table id="tblAuthority2" Runat="server" BorderColor="Gray" CellPadding="3" Width="100%"
										GridLines="Both" CellSpacing="0" BorderStyle="Solid" EnableViewState="True"></asp:Table>
								</TD>
							</TR>
						</table>
					</td>
					<td width="20"></td>
				</tr>
			</table>
			<uc1:InfoPageTailer id="InfoPageTailer1" runat="server"></uc1:InfoPageTailer>
		</form>
	</body>
</HTML>
