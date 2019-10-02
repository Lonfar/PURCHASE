<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../UserControls/ApproveStateInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../UserControls/RefEditor.ascx" %>
<%@ Page language="c#" Codebehind="BIDEvaluation_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialPurchase.BIDEvaluation_Edit" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.0.3300.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="uc3" TagName="CrystalReportBar" Src="../UserControls/CrystalReportBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModuleViewer" Src="../UserControls/ModuleViewer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RefButton" Src="../UserControls/RefButton.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServiceRequistion_Edit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../MyScripts/Tab.js"></SCRIPT>
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
						<P><FONT face="ËÎÌו"></FONT>&nbsp;</P>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 55px"></FONT><msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
							<msp:TabPage ID="tabPage1">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<tr>
										<td align="right">
										    <uc1:RefButton id="RefButton1" runat="server"></uc1:RefButton>
											<asp:Button ID="btnApproved" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnSubmit" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnCancel" Runat="server" CausesValidation="False"></asp:Button>
										</td>
									</tr>
									<TR vAlign="top">
										<TD align="center">
											<uc1:ucedit runat="server" ID="ucEdit_BIDEvaluation"></uc1:ucedit>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage2">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD>
											<uc1:ChildEditControl id="child_BIDSummary" runat="server"></uc1:ChildEditControl>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage3">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR>
										<TD align="center" colspan="1" width="50%"><uc3:CrystalReportBar id="CrystalReportBar1" ReportViewer="CrystalReportViewer1" runat="server"></uc3:CrystalReportBar></TD>
										<td width="50%" align="right"><asp:Button ID="btnByVendor" Runat="server" Text="Print" CausesValidation="False"></asp:Button></td>
									</TR>
									<TR vAlign="top">
										<TD colspan="2">
											<div align="center" style="BORDER-RIGHT:black 1px solid; BORDER-TOP:black 1px solid; OVERFLOW-Y:auto; OVERFLOW-X:auto; BORDER-LEFT:black 1px solid; WIDTH:100%; BORDER-BOTTOM:black 1px solid; HEIGHT:350px; TEXT-ALIGN:center"id="divReport">
												<CR:CRYSTALREPORTVIEWER id="CrystalReportViewer1" runat="server" CssFilename="../Styles/cr.css" BestFitPage="True" HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False" EnableDrillDown="False" HasDrillUpButton="False"
												HasCrystalLogo="False" PrintMode="ActiveX" DisplayGroupTree="False" Height="50px" AutoDataBind="true" Width="350px"></CR:CRYSTALREPORTVIEWER>
											</div>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage4">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR>
										<TD align="center"><uc3:CrystalReportBar id="Crystalreportbar2" ReportViewer="CrystalReportViewer2" runat="server"></uc3:CrystalReportBar></TD>
										<td width="50%" align="right"><asp:Button ID="btnByMaterial" Runat="server" Text="Print" CausesValidation="False"></asp:Button></td>
									</TR>
									<TR vAlign="top">
										<TD colspan="2">
											<div align="center" style="BORDER-RIGHT:black 1px solid; BORDER-TOP:black 1px solid; OVERFLOW-Y:auto; OVERFLOW-X:auto; BORDER-LEFT:black 1px solid; WIDTH:100%; BORDER-BOTTOM:black 1px solid; HEIGHT:350px; TEXT-ALIGN:center"id="divReport">
												<CR:CRYSTALREPORTVIEWER id="CrystalReportViewer2" runat="server" CssFilename="../Styles/cr.css" BestFitPage="True" HasViewList="False" HasToggleGroupTreeButton="False" DisplayToolBar="false" HasSearchButton="False" EnableDrillDown="False" HasDrillUpButton="False"
												HasCrystalLogo="False" PrintMode="ActiveX" DisplayGroupTree="False" Height="50px" AutoDataBind="true" Width="350px"></CR:CRYSTALREPORTVIEWER>
											</div>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage5">
								<TABLE id="tblViewer" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
										<TR vAlign="top">
											<TD>
											<uc1:ModuleViewer id="ModuleViewer1" runat="server"></uc1:ModuleViewer></TD>
										</TR>
									</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage6">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD>
											<uc1:ApproveStateInfo id="ApproveStateInfo1" runat="server"></uc1:ApproveStateInfo>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage7">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD>
											<uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager>
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
