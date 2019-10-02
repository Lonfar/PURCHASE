<%@ Page language="c#" Codebehind="POInWareHouse.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Report.WareHouse.POInWareHouse" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCSearch" Src="../../UserControls/UCSearch.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>POInWareHouse</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/DatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<P><FONT face="宋体"><FONT face="宋体"><FONT face="宋体">
						<FORM id="Form2" method="post" runat="server">
							<TABLE class="TopToolBarLine" id="Table2" cellSpacing="0" cellPadding="0" width="100%"
								border="0">
								<TR>
									<TD width="10"></TD>
									<TD>
										<cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
								</TR>
							</TABLE>
							<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
								<tr>
									<td vAlign="top" align="center" colSpan="4">
										<P align="center">
											<uc1:UCSearch id="UCSearch1" runat="server"></uc1:UCSearch></P>
									</td>
								</tr>
								<TR vAlign="top">
									<TD colSpan="4">
										<P align="center"><uc3:crystalreportbar id="CrystalReportBar" runat="server"></uc3:crystalreportbar></P>
									</TD>
								</TR>
								<TR vAlign="top" align="center">
									<TD align="center" colSpan="4" height="100%">
										<CR:CRYSTALREPORTVIEWER id="CrystalReportViewer" runat="server" Width="350px" AutoDataBind="true" Height="50px"
											DisplayGroupTree="False" PrintMode="ActiveX" HasCrystalLogo="False" HasDrillUpButton="False" EnableDrillDown="False"
											HasSearchButton="False" DisplayToolBar="false" HasToggleGroupTreeButton="False" HasViewList="False"
											BestFitPage="True" CssFilename="../../Styles/cr.css"></CR:CRYSTALREPORTVIEWER></TD>
								</TR>
								<TR vAlign="top">
									<TD class="StatusLine" vAlign="middle" colSpan="4"><FONT face="宋体"></FONT></TD>
								</TR>
							</TABLE>
						</FORM>
						<asp:literal id="litScript" Runat="server"></asp:literal></FONT></FONT></FONT></P>
	</body>
</HTML>
