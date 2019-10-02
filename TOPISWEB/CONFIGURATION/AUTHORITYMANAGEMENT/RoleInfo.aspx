<%@ Register TagPrefix="uc1" TagName="InfoPageTailer" Src="../../UserControls/InfoPageTailer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InfoPageHeader" Src="../../UserControls/InfoPageHeader.ascx" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Page language="c#" Codebehind="RoleInfo.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.RoleInfo" EnableViewState="false"%>
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
					<td><table runat="server" id="tblInfo" cellspacing="0" cellpadding="3" align="center" class="InfoTable"
							width="100%" border="1" bordercolor="#1e84f0">
							<tr>
								<td colspan="2" class="InfoHeaderInInfoPage">
								</td>
							</tr>
							<tr>
								<td width="120" bgcolor="aliceblue"><asp:literal id="litRoleID" Runat="server" Text='<%#GetString("litRoleID")%>'>
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue"><asp:literal id="litRoleName" Runat="server" Text='<%#GetString("litRoleName")%>'>
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue"><asp:literal id="litRoleDesc" Runat="server" Text='<%#GetString("litRoleDescription")%>'>
									</asp:literal></td>
								<td></td>
							</tr>
							<TR>
								<TD vAlign="top" bgcolor="aliceblue">
									<asp:literal id="litAssignAuthority" Runat="server" Text='<%#GetString("litAssignAuthority")%>'>
									</asp:literal></TD>
								<TD align="left">
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
