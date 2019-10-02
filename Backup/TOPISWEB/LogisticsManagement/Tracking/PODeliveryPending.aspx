<%@ Page language="c#" Codebehind="PODeliveryPending.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.LogisticsManagement.Tracking.PODeliveryPending" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEditSearch" Src="../../UserControls/UCEditSearch.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PODeliveryPending</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/UserControls.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout" onload="InitSearch()">
		<FORM id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD width="10"></TD>
					<TD><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TR vAlign="top">
					<TD>
						<P>
							<uc1:UCList id="ucList_PODeliveryPending" runat="server"></uc1:UCList></P>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD class="StatusLine" vAlign="middle">
						<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label></TD>
				</TR>
			</TABLE>
			<P><FONT face="宋体"></FONT>&nbsp;</P>
		</FORM>
	</body>
</HTML>
