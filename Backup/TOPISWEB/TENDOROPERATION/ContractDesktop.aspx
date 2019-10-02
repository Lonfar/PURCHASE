<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Page language="c#" Codebehind="ContractDesktop.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.TendorOperation.Desktop" %>

<%@ Register Assembly="Cnwit.DatePicker" Namespace="Cnwit.Web.UI.WebControls" TagPrefix="cc1" %>
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
		<LINK href="../Styles/DatePicker.CSS" type="text/css" rel="stylesheet">
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
		<table width="100%" align="center">
			<tr>
				<td align="center">
					<fieldset style="WIDTH: 85%" align="center"><legend style="BORDER-RIGHT: lightsteelblue 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: lightsteelblue 1px solid; PADDING-LEFT: 2px; FONT-WEIGHT: bold; FONT-SIZE: 12px; PADDING-BOTTOM: 2px; BORDER-LEFT: lightsteelblue 1px solid; COLOR: #2f4f4f; PADDING-TOP: 2px; BORDER-BOTTOM: lightsteelblue 1px solid; BACKGROUND-COLOR: #dce8f4">
						<asp:label id="lbFlow" runat="server">Flow</asp:label></legend>
						<IMG src="../Images/Page/ContractDesktop.jpg" alt="" border="0" usemap="#Map" class="ImageBorder"
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
								<TBODY>
									<tr vAlign="top">
										<td align="center" width="100%"><msp:tabcontrol id="TabControl1" runat="server" Width="700px" BackColor="#FF8080" BorderColor="Red"
												Height="240px">
												<MSP:TABPAGE id="tabPage5">
													<TABLE id="TABLE1" cellSpacing="1" cellPadding="1" width="400" align="center"
														border="0">
														<TR vAlign="top" width ="80%">
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
																<UC1:UCLIST id="ucList_ContractWithinThreeMonths" runat="server"></UC1:UCLIST></TD>
														</TR>
													</TABLE>
												</MSP:TABPAGE>
												<MSP:TABPAGE id="tabPage2">
													<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" align="center"
														border="0">
														<TR vAlign="top">
															<TD>
																<UC1:UCLIST id="ucList_ContractWithinSixMonths" runat="server"></UC1:UCLIST></TD>
														</TR>
													</TABLE>
												</MSP:TABPAGE>
												<MSP:TABPAGE id="tabPage3">
													<table width="100%" align="center" border="0" height="240px">
														<TBODY>
															<tr vAlign="top">
																<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
																</td>
																<td align="right" width="30%"><asp:label id="lbSR" runat="server">Service Requisition</asp:label></td>
																<td align="left" width="60%"><asp:label id="lbSR_Num" runat="server">100</asp:label></td>
										</td>
									</tr>
									<tr vAlign="top">
										<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
										</td>
										<td align="right" width="30%"><asp:label id="lbTCPaper" runat="server">TC Paper</asp:label></td>
										<td align="left" width="60%"><asp:label id="lbTCPaper_Num" runat="server">100</asp:label></td>
									</tr>
									<tr>
										<td colSpan="3">
											<table height="100%" width="100%">
												<tr width="100%">
													<td align="right"><asp:label id="lbStrategy" runat="server">Strategy</asp:label></td>
													<td align="left"><asp:label id="lbStrategy_Num" runat="server">100</asp:label></td>
													<td align="right"><asp:label id="lbTechnical" runat="server">Technical</asp:label></td>
													<td align="left"><asp:label id="lbTechnical_Num" runat="server">100</asp:label></td>
													<td align="right"><asp:label id="lbCommercial" runat="server">Commercial</asp:label></td>
													<td align="left"><asp:label id="lbCommercial_Num" runat="server">100</asp:label></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr vAlign="top">
										<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
										</td>
										<td align="right" width="30%"><asp:label id="lbTCAgenda" runat="server">TC Agenda</asp:label></td>
										<td align="left" width="60%"><asp:label id="lbTCAgenda_Num" runat="server">100</asp:label></td>
									</tr>
									<tr vAlign="top">
										<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
										</td>
										<td align="right" width="30%"><asp:label id="lbMinutes" runat="server">TC Minutes</asp:label></td>
										<td align="left" width="60%"><asp:label id="lbMinutes_Num" runat="server">100</asp:label></td>
									</tr>
									<tr vAlign="top">
										<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
										</td>
										<td align="right" width="30%"><asp:label id="lbITBDocument" runat="server">ITB Document</asp:label></td>
										<td align="left" width="60%"><asp:label id="lbITBDocument_Num" runat="server">100</asp:label></td>
									</tr>
									<tr vAlign="top">
										<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
										</td>
										<td align="right" width="30%"><asp:label id="lbLetter" runat="server">Letter</asp:label></td>
										<td align="left" width="60%"><asp:label id="lbLetter_Num" runat="server">100</asp:label><FONT face="鐎瑰缍?></FONT></td>
									</tr>
									<tr vAlign="top">
										<td colSpan="3">
											<table height="100%" width="100%">
												<tr width="100%">
													<td align="right"><asp:label id="lbLetterIn" runat="server">Letter In</asp:label></td>
													<td align="left"><asp:label id="lbLetterIn_Num" runat="server">100</asp:label></td>
													<td align="right"><asp:label id="lbLetterOut" runat="server">Letter Out</asp:label></td>
													<td align="left"><asp:label id="lbLetterOut_Num" runat="server">100</asp:label></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr vAlign="top">
										<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
										</td>
										<td align="right" width="30%"><asp:label id="lbProposal" runat="server">Proposal</asp:label></td>
										<td align="left" width="60%"><asp:label id="lbProposal_Num" runat="server">100</asp:label></td>
									</tr>
									<tr vAlign="top">
										<td align="center" width="10%"><IMG src="..\IMAGES\menew\icon_edit.gif">
										</td>
										<td align="right" width="30%"><asp:label id="lbContract" runat="server">Contract</asp:label></td>
										<td align="left" width="60%"><asp:label id="lbContract_Num" runat="server">100</asp:label></td>
									</tr>
							</table>
							</MSP:TABPAGE>
							<MSP:TABPAGE id="tabPage4">
								<table width="100%" height="240px">
									<TBODY>
										<tr>
											<td align="center" class="TitleText1">
												<asp:label id="lbNoticeTitle" runat="server">Notice</asp:label>
											</td>
										</tr>
										<tr valign="top">
											<td align="center"><asp:datagrid id="DataGrid1" runat="server" Width="100%" AutoGenerateColumns="False">
													<Columns>
														<asp:TemplateColumn>
															<ItemTemplate>
																<IMG src="..\IMAGES\Page\Info.gif">
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:BoundColumn DataField="Title" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="20px"></asp:BoundColumn>
														<asp:BoundColumn DataField="IssueDate" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:yyyy-MM-dd}"
															ItemStyle-Height="20px"></asp:BoundColumn>
													</Columns>
												</asp:datagrid></td>
										</tr>
										<TR>
											<TD align="right"><asp:imagebutton id="btnMore" runat="server" ImageUrl="..\IMAGES\Page\More.gif"></asp:imagebutton></TD>
										</TR>
									</TBODY>
								</table>
							</MSP:TABPAGE>
						</msp:tabcontrol></td>
				</tr>
			</table>
			</FIELDSET> </TD></TR></TBODY></TABLE>
			<br>
		</form> <!--		</CENTER>   --></body>
</HTML>
