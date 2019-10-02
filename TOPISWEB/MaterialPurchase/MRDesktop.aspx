<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Register Assembly="Cnwit.DatePicker" Namespace="Cnwit.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ Page language="c#" Codebehind="MRDesktop.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.MaterialPurchase.MRDesktop" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel">
	<HEAD>
		<TOPIS:STYLESKIN id="Desktop" runat="server"></TOPIS:STYLESKIN>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="Styles/style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Tab.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../MyScripts/DatePicker.js"></SCRIPT>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<style type="text/css">BODY { MARGIN: 0px; bgcolor: #FF0000 }
		</style>
	</HEAD>
	<body id="thebody" bgColor="white" MS_POSITIONING="FlowLayout">
		<table class="Desktop_Topline" height="26" cellSpacing="0" cellPadding="0" width="100%">
			<tr>
				<td width="10"></td>
				<td vAlign="middle"><IMG src="../images/Office2003/ToolBarHeadLine.gif" border="0">
				</td>
			</tr>
		</table>
		<BR>
		<table width="100%" align="center">
			<tr>
				<td align="center">
					<fieldset style="WIDTH: 85%" align="center"><legend style="BORDER-RIGHT: lightsteelblue 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: lightsteelblue 1px solid; PADDING-LEFT: 2px; FONT-WEIGHT: bold; FONT-SIZE: 12px; PADDING-BOTTOM: 2px; BORDER-LEFT: lightsteelblue 1px solid; COLOR: #2f4f4f; PADDING-TOP: 2px; BORDER-BOTTOM: lightsteelblue 1px solid; BACKGROUND-COLOR: #dce8f4"><asp:label id="lbFlow" runat="server">Flow</asp:label></legend>
						<IMG src="../Images/Page/MRDesktop.jpg" alt="" border="0" usemap="#Map" class="ImageBorder"
							id="imgArchitecture">
					</fieldset>
				</td>
			</tr>
		</table>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td align="center">
						<fieldset style="WIDTH: 85%" align="center"><legend style="BORDER-RIGHT: lightsteelblue 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: lightsteelblue 1px solid; PADDING-LEFT: 2px; FONT-WEIGHT: bold; FONT-SIZE: 12px; PADDING-BOTTOM: 2px; BORDER-LEFT: lightsteelblue 1px solid; COLOR: #2f4f4f; PADDING-TOP: 2px; BORDER-BOTTOM: lightsteelblue 1px solid; BACKGROUND-COLOR: #dce8f4"><asp:label id="lblPendingService" runat="server">Warning</asp:label></legend>
							<table width="100%" align="center" border="0">
								<tr vAlign="top">
									<td align="center" width="30%"><msp:tabcontrol id="TabControl1" runat="server" Width="700px" BackColor="#FF8080" BorderColor="Red"
											Height="140px">
											<MSP:TABPAGE id="tabPage2">
												<TABLE id="TABLE1" cellSpacing="1" cellPadding="1" width="60%" align="center"
													border="0">
													<TR vAlign="top" width ="60%">
															<TD align="center" align="right" >
                                                                From:	
															</TD>
															<TD align="center" >
															    <cc1:DatePicker ID="dtFrom" runat="server" />
															</TD>
															<TD align="center" align="right" >To:</TD>
															<TD align="center" >
															    <cc1:DatePicker ID="dtTo" runat="server" />
															</TD>
															<TD align="center" align="left" ><asp:Button ID="btnExport" runat="server" Text = "Export" /></TD>
														</TR>
												</TABLE>
											</MSP:TABPAGE>
											<MSP:TABPAGE id="tabPage1">
												<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" align="center"
													border="0">
													<TR vAlign="top">
														<TD align="center">
															<UC1:UCLIST id="ucList_MRDelay" runat="server"></UC1:UCLIST></TD>
													</TR>
												</TABLE>
											</MSP:TABPAGE>
											
										</msp:tabcontrol></td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
			</table>
			<br>
			<BR>
		</form> <!--		</CENTER>   --></body>
</HTML>
