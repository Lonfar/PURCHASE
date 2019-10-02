<%@ Register TagPrefix="uc1" TagName="UCSearch" Src="../../UserControls/UCSearch.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page language="c#" Codebehind="Preserve.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Report.WareHouse.Preserve" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Preserve</title>
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
		<FONT face="ËÎÌå"><FONT face="ËÎÌå">
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
						<TR vAlign="top">
							<TD>
								<P>
									<uc1:UCSearch id="UCSearch1" runat="server"></uc1:UCSearch></P>
							</TD>
						</TR>
						<TR>
							<TD></TD>
						</TR>
						<TR vAlign="top">
							<TD align="center" colSpan="4">
								<uc3:crystalreportbar id="PrintBar" runat="server" width="80%" ReportViewer="CrystalReportViewer1"></uc3:crystalreportbar></TD>
						</TR>
						<TR vAlign="top">
							<TD align="center" colSpan="4" height="100%">
								<CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" Width="350px" CssFilename="../../Styles/cr.css"
									BestFitPage="True" HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False"
									EnableDrillDown="False" HasDrillUpButton="False" HasCrystalLogo="False" PrintMode="ActiveX" DisplayGroupTree="False"
									Height="50px" AutoDataBind="true"></CR:CRYSTALREPORTVIEWER></TD>
						</TR>
						<TR vAlign="top">
							<TD class="StatusLine" vAlign="middle"><FONT face="ËÎÌå"></FONT></TD>
						</TR>
					</TABLE>
				</FORM>
				<asp:literal id="litScript" Runat="server"></asp:literal></FONT></FONT>
	</body>
</HTML>
