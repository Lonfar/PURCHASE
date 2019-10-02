<%@ Page language="c#" Codebehind="FinancingUseAnalysis.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Statistic.FinancingUseAnalysis" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="CrystalReportBar" Src="../UserControls/CrystalReportBar.ascx"%>
<%@ Register TagPrefix="uc1" TagName="UCEditSearch" Src="../UserControls/UCEditSearch.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>VendorReportByBasicInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/DatePicker.js"></SCRIPT>
</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="4" cellPadding="4" width="100%" align="center" border="0">
				<TR vAlign="top" width="80%">
					<TD style="WIDTH: 600px" noWrap align="left" colSpan="4"><uc1:uceditsearch id="UCEditSearch1" runat="server" width="50%"></uc1:uceditsearch></TD>
				</TR>
				<TR vAlign="top" width="80%">
					<TD style="WIDTH: 8%" noWrap align="right" colSpan="1">
						<asp:Label id="lbYear" runat="server">Year</asp:Label></TD>
					<TD style="WIDTH: 27%" noWrap align="left" colSpan="1">
						<asp:DropDownList id="ddlYear" runat="server" Width="71%"></asp:DropDownList></TD>
					<TD style="WIDTH: 9%" noWrap align="right" colSpan="1">
						<asp:Label id="lbMonth" runat="server" Visible ="False" >Month</asp:Label></TD>
					<TD style="WIDTH: 55%" noWrap align="left" colSpan="1">
						<asp:DropDownList id="ddlMonth" runat="server" Width="41%" Visible = "False"></asp:DropDownList></TD>
				</TR>
				<tr vAlign="top" width="80%">
					<td align="left" width="800" colSpan="4"><uc1:crystalreportbar id="PrintBar" runat="server" width="80%" ReportViewer="CrystalReportViewer1"></uc1:crystalreportbar></td>
				</tr>
				<tr vAlign="top" width="80%">
					<td align="left" width="800" colSpan="4"><CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" BorderStyle="Ridge" Width="80%" AutoDataBind="true"
							Height="50px" DisplayGroupTree="False" PrintMode="ActiveX" HasCrystalLogo="False" HasDrillUpButton="False" EnableDrillDown="False" HasSearchButton="False"
							DisplayToolBar="false" HasToggleGroupTreeButton="False" HasViewList="False" BestFitPage="True" CssFilename="../../Styles/cr.css"></CR:CRYSTALREPORTVIEWER></td>
				</tr>
			</TABLE>
		</form>
		<asp:literal id="litScript" Runat="server"></asp:literal><asp:label id="lbltemp" runat="server" Visible="False"></asp:label>
	</body>
</HTML>
