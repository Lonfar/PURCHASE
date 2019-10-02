<%@ Page language="c#" Codebehind="ReceivePrint.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.WareHouseOperation.ReceivePrint" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReceivePrint</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<OBJECT id="factory" style="DISPLAY: none" codeBase="http://localhost/ScriptX.cab#Version=6,1,431,2"
			classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814" viewastext>
		</OBJECT>
		<SCRIPT defer>
function printBase() {
window.alert("aaa");
factory.printing.footer = "&b ScriptX ¥Ú”° &b"
factory.printing.portrait =false
//factory.printing.Print(true)
factory.printing.leftMargin = 1.5
factory.printing.topMargin = 0.5
factory.printing.rightMargin = 0.5
factory.printing.bottomMargin = 1.0
}

function printReport(){
	printBase();
	//window.print();
	factory.printing.Preview();
} 
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR>
						<TD colspan="4" align="center"><uc3:CrystalReportBar id="CrystalReportBar1" ReportViewer="CrystalReportViewer1" runat="server"></uc3:CrystalReportBar></TD>
					</TR>
					<tr valign="top">
						<td align="center" colspan="4" height="100%"><CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" CssFilename="../../Styles/cr.css" BestFitPage="True"
								HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False" EnableDrillDown="False" HasDrillUpButton="False"
								HasCrystalLogo="False" PrintMode="ActiveX" DisplayGroupTree="False" Height="50px" AutoDataBind="true" Width="350px"></CR:CRYSTALREPORTVIEWER></td>
					</tr>
					<TR vAlign="top">
						<TD colSpan="4" class="StatusLine" valign="middle"><FONT face="ÀŒÃÂ"> </FONT>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		<asp:literal id="litScript" Runat="server"></asp:literal><asp:label id="lbltemp" runat="server" Visible="False"></asp:label>
	</body>
</HTML>
