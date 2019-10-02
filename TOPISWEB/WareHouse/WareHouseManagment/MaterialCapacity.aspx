<%@ Register TagPrefix="cc3" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page language="c#" Codebehind="MaterialCapacity.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialCapacity" %>
<%@ Register TagPrefix="cc2" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="cc1" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MaterialCapacity</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/DatePicker.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE Class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD>
						<cc2:ToolBar id="ToolBar1" runat="server"></cc2:ToolBar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<tr vAlign="bottom">
						<td align="left" colSpan="4">
							<asp:LinkButton ID="lbHideVoucher" Runat="server" CssClass="blueunderline" Visable="true" onclick="lbHideVoucher_Click">cccccccc</asp:LinkButton>
							<asp:LinkButton ID="lbShowVoucher" Runat="server" CssClass="blueunderline" Visable="true" onclick="lbShowVoucher_Click">cccccccccbbb</asp:LinkButton>
						</td>
					</tr>
					<TR vAlign="top">
						<TD colSpan="4">
							<P>
								<asp:Panel id="pnlSearch" runat="server">
									<TABLE cellSpacing="1" cellPadding="1" align="center" border="0">
										<TR>
											<TD class="FormNormalTitle" style="WIDTH: 10%" align="right">
												<asp:Label id="lblWHID" runat="server"></asp:Label></TD>
											<TD>
												<uc1:RefEditor id="Ref_WHID" runat="server"></uc1:RefEditor></TD>
											<TD>&nbsp;</TD>
											<TD class="FormNormalTitle" style="WIDTH: 10%" align="right">
												<asp:Label id="lblItemCode" runat="server"></asp:Label></TD>
											<TD>
												<asp:TextBox class="SingleLineTextBox" id="txtItemCode" runat="server"></asp:TextBox></TD>
										</TR>
									</TABLE>
								</asp:Panel>
							</P>
							<P align="center">
								<cc1:TabControl id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
									<cc1:TabPage Caption="" ID="tabPage1">
										<TABLE cellSpacing="1" cellPadding="1" width="90%" border="0" align="center">
											<tr valign="top">
												<td align="center" colspan="4" height="100%">
													<CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" CssFilename="../../Styles/cr.css" BestFitPage="True"
														HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False"
														EnableDrillDown="False" HasDrillUpButton="False" HasCrystalLogo="False" PrintMode="ActiveX" DisplayGroupTree="False"
														Height="50px" AutoDataBind="true" Width="350px"></CR:CRYSTALREPORTVIEWER></td>
											</tr>
										</TABLE>
									</cc1:TabPage>
									<cc1:TabPage Caption="" ID="tabPage2">
										<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center" >
											<TBODY>
												<tr valign ="bottom" >
													<td align = "center" colspan = "4" class="TitleText1" >
														<asp:Label ID = "lbTitle" Runat = "server"></asp:Label>
													</td>
												</tr>
												<tr vAlign="bottom">
													<td align="left" colSpan="4">
														<asp:datagrid id="DG_MaterialCapacity" runat="server" CssClass="TableGlobalOne" AllowSorting="True"
															AllowPaging="True" AutoGenerateColumns="False" PageSize="15" Width="100%">
															<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
															<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
															<ItemStyle CssClass="TableRow"></ItemStyle>
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<Columns>
																<asp:BoundColumn>
																	<ItemStyle HorizontalAlign="Center" Height="23px" Width="5%" VerticalAlign="Middle"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="WHID" SortExpression="WHID">
																	<ItemStyle HorizontalAlign="Left" Height="23px" Width="20%" VerticalAlign="Middle"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="ItemCode" SortExpression="ItemCode">
																	<ItemStyle HorizontalAlign="Center" Height="23px" Width="15%" VerticalAlign="Middle"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="MaterialName" SortExpression="MaterialName">
																	<ItemStyle HorizontalAlign="Left" Height="23px" Width="30%" VerticalAlign="Middle"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="MinNum" SortExpression="MinNum">
																	<ItemStyle HorizontalAlign="Center" Height="23px" Width="8%" VerticalAlign="Middle"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="CurrentNum" SortExpression="CurrentNum">
																	<ItemStyle HorizontalAlign="Center" Height="23px" Width="8%" VerticalAlign="Middle"></ItemStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="MaxNum" SortExpression="MaxNum">
																	<ItemStyle HorizontalAlign="Center" Height="23px" Width="8%" VerticalAlign="Middle"></ItemStyle>
																</asp:BoundColumn>
															</Columns>
															<PagerStyle Visible="False"></PagerStyle>
														</asp:datagrid>
													</td>
												</tr>
												<tr>
													<td>
														<WEBDIYER:ASPNETPAGER id="pager" runat="server" CssClass="mypager" PageSize="15" Width="100%" ShowInputBox="Always"
															PagingButtonSpacing="4px" SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px"
															InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
															SubmitButtonText="Submit" NumericButtonTextFormatString="[{0}]" ShowCustomInfoSection="left" ImagePath="../../Images/aspnetpager/"
															ButtonImageNameExtension="n" CpiButtonImageNameExtension="r" DisabledButtonImageNameExtension="g"
															TextBeforeInputBox="Turn To " InvalidPageIndexErrorString="the page index is invalid" PageIndexOutOfRangeErrorString="page index out of range"
															NavigationToolTipTextFormatString="Turn To Page {0}" PagingButtonType="Image" Height="25px" HorizontalAlign="Right"></WEBDIYER:ASPNETPAGER>
													</td>
												</tr>
										</TABLE>
									</cc1:TabPage>
								</cc1:TabControl></P>
							<P align="center"><asp:Label class="TitleText1" id="lblTitle" runat="server"></asp:Label></P>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		<asp:literal id="litScript" Runat="server"></asp:literal><asp:label id="lbltemp" runat="server" Visible="False"></asp:label>
	</body>
</HTML>
