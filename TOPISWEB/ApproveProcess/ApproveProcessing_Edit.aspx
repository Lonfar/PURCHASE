<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Page language="c#" Codebehind="ApproveProcessing_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.ApproveProcess.ApproveProcessing_Edit" %>
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
						<P><FONT face="宋体"></FONT>&nbsp;</P>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 55px"></FONT>
						<msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px"
							Height="408px">
							<msp:TabPage ID="tabPage1">
								<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD colspan="2">
											<uc1:ucedit runat="server" ID="VoucherEdit_BaseInfo"></uc1:ucedit>
										</TD>
									</TR>
									<tr>
										<!-- 这里使用上传控件-->
										<td align="center" colspan="2">
											<uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager></td>
									</tr>
									<tr>
										<td width="10%"></td>
										<td height="80">
											<asp:LinkButton id="btnViewDetial" runat="server" Text="ViewDetial" CssClass="graybutton" CausesValidation="False"></asp:LinkButton>
										</td>
									</tr>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage2" Width="800px">
								<TABLE id="tb2" cellSpacing="1" cellPadding="1" border="0" align="center">
									<TR vAlign="top">
										<TD Width="800px">
											<uc1:uclist runat="server" ID="VoucherList_ApproveProcessing"></uc1:uclist>
											<uc1:ucedit runat="server" ID="VoucherEdit_ApproveProcessing"></uc1:ucedit>
										</TD>
									<tr>
										<td height="50" align="center" Width="800px">
											<asp:LinkButton id="btnPass" runat="server" Text="Pass" CssClass="graybutton"></asp:LinkButton>
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:LinkButton id="btnRefuse" runat="server" Text="Refuse" CssClass="graybutton"></asp:LinkButton>
										</td>
									</tr>
					</TD>
				</TR>
			</TABLE>
			</msp:TabPage> </msp:tabcontrol> </TD> </TR>
			<TR vAlign="top">
				<TD class="StatusLine" valign="middle">
					<asp:Label runat="server" Width="100%" ID="lblMSG"></asp:Label>
				</TD>
			</TR>
			</TABLE>
		</form>
	</body>
</HTML>
