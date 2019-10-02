<%@ Page language="c#" Codebehind="Authority.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.Authority" EnableViewState="False"%>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="StyleSkin" runat="server"></TOPIS:StyleSkin>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">
		<TOPIS:PAGEDESCRIPTION id="PageDescription" runat="server"></TOPIS:PAGEDESCRIPTION>
		
		<TABLE width="95%" align="center" border="0" cellpadding="0" Class="TitleArea">
			<TR>
				<TD class="TitleText1">
					<asp:Literal Runat="server" ID="litViewAuthority"  Text='<%#GetString("litViewAuthority")%>'>
					</asp:Literal>
				</TD>
			</TR>
		</TABLE>
		
		<form id="Form1" method="post" runat="server">
		<table align="center" width="95%" border="1" cellpadding=0 cellspacing=0 class="TableWithBorder">
			<tr><td >
				<asp:Table id="tblAuthority" Runat="server" Width="100%"  align="center" ></asp:Table>
			</td></tr>
		</table>
		<br><br>
		</form>
	</body>
</HTML>
