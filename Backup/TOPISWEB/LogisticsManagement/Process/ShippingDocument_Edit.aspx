<%@ Register TagPrefix="uc1" TagName="ModuleViewer" Src="../../UserControls/ModuleViewer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Page language="c#" Codebehind="ShippingDocument_Edit.aspx.cs" AutoEventWireup="true" Inherits="TopisWeb.LogisticsManagement.Process.ShippingDocument_Edit" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../../UserControls/ApproveStateInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<HTML>
	<HEAD>
		<title>ShipDoctransfer_Edit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table class="TopToolBarLine" cellspacing="0" cellpadding="0" width="100%" border="0" >
				<tr>
					<td width="10"></td>
					<td><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></td>
				</tr>
			</table>
			<table id="table1" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
				<tr valign="top">
					<td>
						<P>&nbsp;</P>
					</td>
				</tr>
				<tr valign="top">
					<td style="HEIGHT: 55px"><msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
							<msp:TabPage ID="tabPage1">
								<table id="1" cellspacing="1" cellpadding="1" width="100%" border="0" >
									<tr valign="top">
										<td align="center">
											<uc1:ucedit runat="server" ID="ucEdit_ShipDoc"></uc1:ucedit>
										</td>
									</tr>
								</table>
								<div style="BORDER-TOP: sandybrown thin solid " ></div>
								<table id="2" cellspacing="1" cellpadding="1" width="80%" border="0">
									<tr valign="top">
										<td align="center" width="760px">
											<uc1:ucedit runat="server" ID="ucEdit_ShipDoc1"></uc1:ucedit>
										</td>
									</tr>
								</table>
							</msp:TabPage>
							<msp:TabPage ID="tabPage2">
								<table id="3" cellspacing="1" cellpadding="1" width="100%" border="0">
									<tr valign="top">
										<td>
											<uc1:ChildEditControl runat="server" ID="child_RelateSDT"></uc1:ChildEditControl>
										</td>
									</tr>
								</table>
							</msp:TabPage>
						</msp:tabcontrol></td>
				</tr>
				<tr valign="top">
					<td class="StatusLine" valign="middle">
						<asp:label id="lblMSG" runat="server" Width="100%"></asp:label><asp:label id="lbltemp" runat="server" Visible="False"></asp:label></td>
				</tr>
			</table>
			<asp:Literal id="lbError" runat="server"></asp:Literal>
		</form>
	</body>
</HTML>
