<%@ Register TagPrefix="uc1" TagName="UCEditSearch" Src="../UserControls/UCEditSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CrystalReportBar" Src="../UserControls/CrystalReportBar.ascx"%>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>
<%@ Page language="c#" Codebehind="MaterialUseAnalysis.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Statistic.MaterialUseAnalysis" %>
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
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
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
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">			
				<TR vAlign="top">
					<TD align="left">
					<TABLE id="Table10" cellSpacing="1" cellPadding="1" width="50%" align="left" border="0">
							<tr>
							<td colspan="5"><uc1:uceditsearch id="UCEditSearch1" runat="server" width="50%"></uc1:uceditsearch>	</td>
							</tr>
							<tr>
							<td class="FormNormalTitle" align="right" style="WIDTH:15%">
								<asp:literal id="Literal_From" runat="server"></asp:literal>
							</td>
							<td align="left" style="WIDTH:17%">
									<cc2:DatePicker id="DateEditor_From" runat="server" width="100%"></cc2:DatePicker>
							</td>
							<td class="FormNormalTitle" align="right" style="WIDTH:5%"><asp:literal id="Literal_To" runat="server"></asp:literal>
							</td>
							<td align="left" style="WIDTH:17%">
									<cc2:DatePicker id="DateEditor_To" runat="server" ></cc2:DatePicker>
							</td><td WIDTH="45%"></td>
						</tr>
					</table>
					
					</TD>
				</TR>
				
				<tr vAlign="top">
					<td align="center" colSpan="2" style="WIDTH: 800px"><uc1:crystalreportbar id="PrintBar" runat="server" width="80%" ReportViewer="CrystalReportViewer1"></uc1:crystalreportbar></td>
				</tr>
				<tr valign="top">
					<td align="left" colspan="2" height="100%" width="80%"><CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" CssFilename="../../Styles/cr.css" BestFitPage="True"
							HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False" EnableDrillDown="False" HasDrillUpButton="False" HasCrystalLogo="False"
							PrintMode="ActiveX" DisplayGroupTree="False" Height="50px" AutoDataBind="true" Width="350px"></CR:CRYSTALREPORTVIEWER></td>
				</tr>
			</TABLE>
		</form>
		<asp:literal id="litScript" Runat="server"></asp:literal><asp:label id="lbltemp" runat="server" Visible="False"></asp:label>
	</body>
</HTML>
