<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveBillReport.aspx.cs" Inherits="TopisWeb.WareHouse.WareHouseSearch.ReceiveBillReport" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
	<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
	<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
	<script language="javascript">
		function refreshSearch(){
					
			window.parent.frames("Search").document.getElementById("imgRefresh").style.visibility = "hidden";
		}	
	</script>
</head>
<body>
    <form id="form1" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<tr>
					    <td>
					        <TABLE cellSpacing="1" cellPadding="1" width="90%" border="0" align="left">
									<TR vAlign="top">
						                <TD align="left" colSpan="4"><uc3:crystalreportbar id="PrintBar" runat="server" width="80%" ReportViewer="CrystalReportViewer1"></uc3:crystalreportbar></TD>
					                </TR>
									<tr valign="top">
										<td align="center" colspan="4" height="100%">
											<CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" CssFilename="../../Styles/cr.css" BestFitPage="True"
												HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False"
												EnableDrillDown="False" HasDrillUpButton="False" HasCrystalLogo="False" PrintMode="ActiveX" DisplayGroupTree="False"
												Height="50px" AutoDataBind="true" Width="350px"></CR:CRYSTALREPORTVIEWER></td>
									</tr>
								</TABLE>
					    </td>
					</tr>
					<TR vAlign="top">
					<TD class="StatusLine" valign="middle">
						<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
					</TD>
				</TR>
				</TBODY>
			</TABLE>
    </form>
</body>
</html>
