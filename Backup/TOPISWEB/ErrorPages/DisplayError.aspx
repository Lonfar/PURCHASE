<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Page language="c#" Codebehind="DisplayError.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.ErrorPages.DisplayError" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="ProjectStyleSkin" runat="server"></TOPIS:StyleSkin>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body id="thebody">
		<form id="Form1" method="post" runat="server">
			<table align="center" width="100%" height="100%">
				<tr>
					<td>
						<table align="center" width="80%" border="0" bordercolor="#425d8c" cellspacing="0" height="200px"
							style="border-collapse:collapse;border:1px solid #425d8c">
							<tr height="25" bgcolor="#425d8c">
								<td style="COLOR:white" colspan="2">
									<asp:Literal Runat="server" ID="litCaption" Text='<%#GetString("litCaption")%>'>
									</asp:Literal>
								</td>
							</tr>
							<tr>
								<td width="60px" valign="middle" align="right" >
									<img src="../Images/Page/UnKnownError.gif" border="0">
								</td>
								<td align="left" >
									<table width="100%">
										<colgroup>
											<col width="15%">
											<col width="*">
										</colgroup>
										<tr>
											<td valign="middle" style="PADDING-LEFT:20px" nowrap>
												<asp:Literal Runat="server" ID="litErrorCode" Text='<%#GetString("litErrorCode")%>'>
												</asp:Literal>
											</td>
											<td>
												<asp:Label Runat="server" ID="lblErrorCode"></asp:Label>
											</td>
										</tr>
										<tr>
											<td valign="middle" style="PADDING-LEFT:20px" nowrap>
												<asp:Literal Runat="server" ID="litDescription" Text='<%#GetString("litDescription")%>'>
												</asp:Literal>
											</td>
											<td>
												<asp:Label Runat="server" ID="lblDescription"></asp:Label>
											</td>
										</tr>
										<tr>
											<td valign="middle" style="PADDING-LEFT:20px" nowrap>
												<asp:Literal Runat="server" ID="litModuleID" Text='<%#GetString("litModuleID")%>'>
												</asp:Literal>
											</td>
											<td>
												<asp:Label Runat="server" ID="lblModuleID"></asp:Label>
											</td>
										</tr>
										
										<tr>
											<td valign="middle" style="PADDING-LEFT:20px" nowrap>
												<asp:Literal Runat="server" ID="litAddedInfo" Text='<%#GetString("litAddedInfo")%>'>
												</asp:Literal>
											</td>
											<td>
												<asp:Label Runat="server" ID="lblAddedInfo"></asp:Label>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
