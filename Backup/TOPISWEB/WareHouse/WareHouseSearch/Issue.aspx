<%@ Register TagPrefix="uc1" TagName="UCSearch" Src="../../UserControls/UCSearch.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page language="c#" Codebehind="Issue.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Report.WareHouse.Issue" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Issue</title>
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
			<TABLE Class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD colSpan="4">
							<P><uc1:UCSearch id="UCSearch1" runat="server"></uc1:UCSearch></P>
						</TD>
					</tr>
					<tr>
						<TD colspan="4" align="center"><uc3:CrystalReportBar id="CrystalReportBar1" ReportViewer="CrystalReportViewer1" runat="server"></uc3:CrystalReportBar></TD>
					</TR>
					<tr valign="top">
						<td align="center" colspan="4" height="100%"><CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" CssFilename="../../Styles/cr.css" BestFitPage="True"
								HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False" EnableDrillDown="False" HasDrillUpButton="False"
								HasCrystalLogo="False" PrintMode="ActiveX" DisplayGroupTree="False" Height="50px" AutoDataBind="true" Width="350px"></CR:CRYSTALREPORTVIEWER></td>
					</tr>
					<tr vAlign="top">
						<TD colSpan="4" class="StatusLine" valign="middle">
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		<asp:literal id="litScript" Runat="server"></asp:literal>
	</body>
</HTML>
