<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Page language="c#" Codebehind="EmployeeInfo.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.EmployeeInfo" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<TOPIS:STYLESKIN id="StyleSkinEmployeeInfo" runat="server"></TOPIS:STYLESKIN>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">						
		<form id="Form1" method="post" runat="server">
		<table width="100%" border="0" cellspacing="1" cellpadding="1">
			<tr bgcolor="#C8C8D8">
				<td valign="top" align="left">					
					<msp:toolbar id="ToolBar1" runat="server" ImagePath="../../../../Images/Office2003">										
					</msp:toolbar>
				</td>
			</tr>
		</table>		
		<uc1:UCEdit id="VoucherEdit" runat="server"></uc1:UCEdit>
		<table width="100%" border="0" cellspacing="1" cellpadding="1">
			<tr>
				<td colspan = 4>
					<asp:CheckBox ID="ckbUser" Text ="ckbUser" Runat =server AutoPostBack =True oncheckedchanged="ckbUser_CheckedChanged"></asp:CheckBox> 
				</td>
			</tr>
			<tr>
				<td align=right>
					<asp:Literal  Runat = server id = "litUserID" Text="litUserID"></asp:Literal>					
				</td>
				<td>
					<asp:TextBox Runat =server ID = "txtUserID" ></asp:TextBox> 
				</td>
				<td align =right>
					<asp:Literal  Runat = server id = "litUserName" Text="litUserName"></asp:Literal>
				</td>
				<td>
					<asp:TextBox Runat =server ID = "txtUserName"></asp:TextBox> 
				</td>
			</tr>
		</table> 
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal>
	</body>
</HTML>
