<%@ Register TagPrefix="uc1" TagName="ModuleViewer" Src="../../UserControls/ModuleViewer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Page language="c#" Codebehind="ShipDocTransfer_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.LogisticsManagement.Process.ShipDocTransfer_Edit" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../../UserControls/ApproveStateInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ShipDocTransfer_Edit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
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
					<TD>
						<P>&nbsp;</P>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 55px"></FONT><msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
							<msp:TabPage ID="tabPage1">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD align="center">
											<uc1:ucedit runat="server" ID="ucEdit_ShipDocTransfer"></uc1:ucedit>
										</TD>
									</TR>
								</TABLE>
								<div style="BORDER-TOP: sandybrown thin solid " ></div>
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="80%" border="0" align="center">
									<TR vAlign="top">
										<TD align="center" width="760px">
											<uc1:ucedit runat="server" ID="ucEdit_ShipDocTransfer1"></uc1:ucedit>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage2">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD>
											<uc1:ChildEditControl id="child_RelatedPO" runat="server"></uc1:ChildEditControl>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage3">
								<TABLE id="TABLE2" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD>
											<uc1:ChildEditControl id="child_RelatedOthers" runat="server"></uc1:ChildEditControl>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage4">
								<TABLE id="TABLE3" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD>
											<uc1:ChildEditControl id="child_RelatedWO" runat="server"></uc1:ChildEditControl>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
						</msp:tabcontrol></TD>
				</TR>
				<TR vAlign="top">
					<TD class="StatusLine" vAlign="middle">
						<asp:label id="lblMSG" runat="server" Width="100%"></asp:label><asp:label id="lbltemp" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
			<asp:Literal id="lbError" runat="server"></asp:Literal>
		</form>
	</body>
</HTML>
