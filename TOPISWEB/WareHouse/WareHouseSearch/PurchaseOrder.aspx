<%@ Page language="c#" Codebehind="PurchaseOrder.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Report.WareHouse.PurchaseOrder" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCSearch" Src="../../UserControls/UCSearch.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PurchaseOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/DatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD  colSpan="4"><uc1:UCSearch id="UCSearch1" runat="server"></uc1:UCSearch></TD>
						
					</TR>
					<TR>
						<TD style="HEIGHT: 28px" align="center" colSpan="4"><uc3:crystalreportbar id="CrystalReportBar1" runat="server" ReportViewer="CrystalReportViewer1"></uc3:crystalreportbar></TD>
					</TR>
					<tr vAlign="top">
						<td style="HEIGHT: 2px" align="center" colSpan="4" height="2"><CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" Width="350px" AutoDataBind="true" Height="50px"
								DisplayGroupTree="False" PrintMode="ActiveX" HasCrystalLogo="False" HasDrillUpButton="False" EnableDrillDown="False" HasSearchButton="False" DisplayToolBar="false"
								HasToggleGroupTreeButton="False" HasViewList="False" BestFitPage="True" CssFilename="../../Styles/cr.css"></CR:CRYSTALREPORTVIEWER></td>
					</tr>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle" colSpan="4"><FONT face="ËÎÌו"></FONT></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
