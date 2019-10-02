<%@ Page language="c#" Codebehind="Profile.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Account.Profile" EnableViewState="False"%>
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
	<body id="thebody">
		<TOPIS:PAGEDESCRIPTION id="PageDescriptionAFE" runat="server"></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE width="95%" align="center" border="0" cellpadding="0" Class="TitleArea">
				<TR>
					<TD class="TitleText1">
						<asp:Literal id="litModifyInfo" Runat="server" Text='<%#GetString("litUserProfile")%>'>
						</asp:Literal>
					</TD>
				</TR>
			</TABLE>
			<TABLE width="95%" align="center" border="1" bordercolor="gray" cellpadding="2" style="BORDER-COLLAPSE: collapse">
				<COLGROUP>
					<COL align="right" width="120">
					<COL align="left" width="*" style="COLOR:darkblue">
				</COLGROUP>
				<TR>
					<TD class="FormNormalTitle">
						<asp:literal id="litUserID" Runat="server" Text='<%#GetString("litUserID")%>'>
						</asp:literal></TD>
					<TD>
						<asp:Label Runat="server" ID="lblUserID"></asp:Label>
					</TD>
				</TR>
				<TR>
					<TD class="FormNormalTitle">
						<asp:literal id="litUserName" Runat="server" Text='<%#GetString("litUserName")%>'>
						</asp:literal></TD>
					<TD>
						<asp:Label Runat="server" ID="lblUserName"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="FormNormalTitle">
						<asp:literal id="litCanLogin" Runat="server" Text='<%#GetString("litCanLogin")%>'>
						</asp:literal></TD>
					<TD>
						<asp:Label Runat="server" ID="lblCanLogin"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="FormNormalTitle">
						<asp:literal id="litLastLoginTime" Runat="server" Text='<%#GetString("litLastLoginTime")%>'>
						</asp:literal></TD>
					<TD>
						<asp:Label Runat="server" ID="lblLastLoginTime"></asp:Label></TD>
				</TR>
				<TR>
					<TD class="FormNormalTitle">
						<asp:literal id="litLastLoginIP" Runat="server" Text='<%#GetString("litLastLoginIP")%>'>
						</asp:literal></TD>
					<TD>
						<asp:Label Runat="server" ID="lblLastLoginIP"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" class="FormNormalTitle">
						<asp:literal id="litAssignedRoles" Runat="server" Text='<%#GetString("litAssignedRoles")%>'>
						</asp:literal></TD>
					<TD>
						<table align="center" width="100%" runat="server" id="tblRoles" cellpadding="2" style="BORDER-COLLAPSE:collapse">
						</table>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" class="FormNormalTitle">
						<asp:literal id="litAssignedAuthories" Runat="server" Text='<%#GetString("litAssignedAuthorities")%>'>
						</asp:literal></TD>
					<TD>
						<asp:Table id="tblAuthority" runat="server" Width="100%" BorderWidth="1" BorderStyle="Solid"></asp:Table>

					</TD>
				</TR>
			</TABLE>
			<br><br>
		</form>
	</body>
</HTML>
