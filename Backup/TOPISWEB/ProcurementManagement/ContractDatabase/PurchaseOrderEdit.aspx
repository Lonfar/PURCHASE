<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Page language="c#" Codebehind="PurchaseOrderEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.ProcurementManagement.ContractDatabase.PurchaseOrderEdit" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PurchaseOrderEdit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<FONT face="ËÎÌו">
			<FORM id="Form1" method="post" runat="server">
				<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<td width="10"></td>
						<TD><cc1:toolbar id="ToolBar" runat="server"></cc1:toolbar></TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
					<TR vAlign="top">
						<TD style="HEIGHT: 55px">
							<cc2:TabControl id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
								<cc2:TabPage ID="tabPage1">
									<TABLE id="Tbl1" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
										<tr>
											<td align="right">
											    <asp:Button ID="btn_RelatingPurchaseOrder" Runat="server" CausesValidation="False" ></asp:Button>
												<asp:Button ID="btnPass" Runat="server" CausesValidation="False"></asp:Button>
												<asp:Button ID="btnSubmit" Runat="server" CausesValidation="False"></asp:Button>
												<asp:Button ID="btnCancel" Runat="server" CausesValidation="False"></asp:Button>
											</td>
										</tr>
										<TR vAlign="top">
											<TD>
												<uc1:ucedit runat="server" ID="ucEdit_PurchaseOrder"></uc1:ucedit>
											</TD>
										</TR>
									</TABLE>
								</cc2:TabPage>
								<cc2:TabPage ID="tabPage2">
									<TABLE id="Tbl2" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
										<TR vAlign="top">
											<TD>
												<uc1:ucedit runat="server" ID="ucEdit_VendorContract"></uc1:ucedit>
											</TD>
										</TR>
										<tr vAlign="bottom">
											<td align="left">
											 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label id="labelCountry" runat="server" CssClass="FormNormalTitle"></asp:label>
												<asp:TextBox Runat="server" ID="TextBoxCountry" Width="300px" CssClass="SingleLineTextBox"></asp:TextBox>
											</td>
										</tr>
										<TR vAlign="top">
											<TD>
												<uc1:ucedit runat="server" ID="ucEdit_POSponsionLetter"></uc1:ucedit>
											</TD>
										</TR>
										<TR vAlign="top" width="50%">
											<TD width="50%">
												
											</TD>
										</TR>
									</TABLE>
								</cc2:TabPage>
								<cc2:TabPage ID="tabPage3">
									<TABLE id="Tbl3" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
										<TR vAlign="top">
											<TD>
												<uc1:ChildEditControl runat="server" ID="child_POMaterial"></uc1:ChildEditControl>
											</TD>
										</TR>
									</TABLE>
								</cc2:TabPage>
								<cc2:TabPage ID="tabPage4">
									<TABLE id="Tbl4" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
										<TR vAlign="top">
											<TD align="center">
												<asp:Label ID="Label1" runat="server"></asp:Label>
											</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center" class="TitleText1">
												<asp:Label ID="lbAttachment" runat="server"></asp:Label>
											</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center">
												<uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager>
											</TD>
										</TR>
									</TABLE>
								</cc2:TabPage>
							</cc2:TabControl></TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle">
							<asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TABLE>
			</FORM>
		</FONT>
	</body>
</HTML>
