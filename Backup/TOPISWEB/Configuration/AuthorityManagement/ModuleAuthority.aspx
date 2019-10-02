<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Page language="c#" Codebehind="ModuleAuthority.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.ModuleAuthority" EnableViewState="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="StyleSkin" runat="server"></TOPIS:StyleSkin>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">
		<TOPIS:PAGEDESCRIPTION id="PageDescription" runat="server"></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<table runat="server" id="tblAuthority" cellspacing="0" cellpadding="3" align="center"
				bordercolor="gray" border="1" style="BORDER-COLLAPSE:collapse" width="100%">
			</table>
			<table align="center" width="100%">
				<tr height="30">
					<td align="center">
						<a href="javascript:window.history.back()" class="blueunderline">
							<asp:Literal Runat="server" id="litBack" Text='<%#GetString("btnBack")%>'>
							</asp:Literal></a>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
