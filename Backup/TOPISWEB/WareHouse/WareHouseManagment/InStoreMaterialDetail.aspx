<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEditSearch" Src="../../UserControls/UCEditSearch.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Page language="c#" Codebehind="InStoreMaterialDetail.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouseManagment.InStoreMaterialDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Inventory</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<table class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td ></td>
					<td><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></td>
				</tr>
			</table>
			<table id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<tr vAlign="top">
						<td style="HEIGHT: 55px">
						    <msp:tabcontrol id="Tabcontrol2" runat="server" width="100%" BackColor="#FF8080" BorderColor="Red">
								<msp:TabPage id="tabPage1">
									<table id="Tbl1" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
										<tr vAlign="top">
											<td>
												<uc2:UCList id="VoucherList" runat="server"></uc2:UCList></td>
										</tr>
									</table>
								</msp:TabPage>
								<msp:TabPage id="tabPage2">									
									<table id="Tbl1" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
										<tr vAlign="top">
											<td>
												<uc2:UCList id="Uclist1" runat="server"></uc2:UCList></td>
										</tr>
									</table>
								</msp:TabPage>
							</msp:tabcontrol></td>
					</tr>
					<tr vAlign="top">
						<td class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%" Font-Bold="True"></asp:label></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
