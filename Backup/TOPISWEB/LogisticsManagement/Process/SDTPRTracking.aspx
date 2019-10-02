<%@ Page language="c#" Codebehind="SDTPRTracking.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.LogisticsManagement.Process.SDTPRTracking" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SDTPRTracking</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/UserControls.js"></SCRIPT>
	</HEAD>
	<body onload="InitSearch()" MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<td width="10"></td>
					<TD><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD>
							<P><uc1:uclist id="ucList_SDTTracking" runat="server"></uc1:uclist></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
