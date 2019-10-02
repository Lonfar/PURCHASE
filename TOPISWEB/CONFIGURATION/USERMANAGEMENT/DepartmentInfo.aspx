<%@ Page language="c#" Codebehind="DepartmentInfo.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.DepartmentInfo" EnableViewState="false" %>
<%@ Register TagPrefix="uc1" TagName="InfoPageHeader" Src="../UserControls/InfoPageHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InfoPageTailer" Src="../UserControls/InfoPageTailer.ascx" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
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
			<uc1:InfoPageHeader id="InfoPageHeader1" runat="server"></uc1:InfoPageHeader>
			<table align="center" width="100%">
				<tr>
					<td width="40px"></td>
					<td><table runat="server" id="tblInfo" cellspacing="0" cellpadding="3" align="center" class="InfoTable"
							width="100%" border="1" bordercolor="#1e84f0">
							<tr>
								<td colspan="2" class="InfoHeaderInInfoPage"></td>
							</tr>
							<tr>
								<td width="120"  bgcolor="aliceblue" ><asp:literal id="litDepartrmentID" Text='<%#GetString("litDepartmentID")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue" ><asp:literal id="litDepartmentName" Text='<%#GetString("litDepartmentName")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue" ><asp:literal id="litDepartmentDescription" Text='<%#GetString("litDepartmentDescription")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue" ><asp:literal id="litPrincipal" Text='<%#GetString("litPrincipal")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue" ><asp:literal id="litFax" Text='<%#GetString("litFax")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue" ><asp:literal id="litTel" Text='<%#GetString("litTel")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue" ><asp:literal id="litEmail" Text='<%#GetString("litEmail")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
							<tr>
								<td bgcolor="aliceblue" ><asp:literal id="litContact" Text='<%#GetString("litContact")%>' Runat="server">
									</asp:literal></td>
								<td></td>
							</tr>
						</table>
					</td>
					<td width="20px"></td>
				</tr>
			</table>
			<uc1:InfoPageTailer id="InfoPageTailer1" runat="server"></uc1:InfoPageTailer>
		</form>
	</body>
</HTML>
