<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BigDepartmentEdit.aspx.cs" Inherits="TopisWeb.Configuration.BasicInfo.BigDepartmentEdit" %>
<%@ Register TagPrefix="uc1" TagName="RefButton" Src="../../UserControls/RefButton.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BigDepartmentEdit</title>
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
		
</head>
<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD>
							<P>&nbsp;</P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 55px"><msp:tabcontrol id="TabControl1" runat="server" Width="800px" BackColor="#FF8080" BorderColor="Red">
								<msp:TabPage ID="tabPage1">
									<TABLE id="Tbl1" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
								
										<TR vAlign="top">
											<TD>
												<uc1:ucedit runat="server" ID="ucEdit_BigDepartment"></uc1:ucedit>
											</TD>
										</TR>
									</TABLE>
									<br /><br /><TABLE id="Tbl2" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
										<TR vAlign="top">
											<TD>
												<uc2:ChildEditControl id="child_BigDepartment" runat="server"></uc2:ChildEditControl>
											</TD>
										</TR>
									</TABLE>
									
								</msp:TabPage>
							</msp:tabcontrol></TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		<asp:literal id="lblErrorMsg" Runat="server" Visible="False"></asp:literal>
	</body>
</html>
