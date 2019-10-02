<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Page language="c#" Codebehind="MisDepApprove.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.MaterialPurchase.MisDepApprove" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title> <%=GetString("txt_title")%>
		</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<TOPIS:PAGEDESCRIPTION id="MisDepApprovePageDescription" runat="server" Text=""></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
				<tr>
					<td align="center">
						<uc1:ucedit runat="server" ID="VoucherEdit"></uc1:ucedit>
					</td>
				</tr>
				<TR>
					<td align="left">
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<fieldset style="BORDER-RIGHT:#999999 1px solid; BORDER-TOP:#999999 1px solid; VISIBILITY:hidden; BORDER-LEFT:#999999 1px solid; WIDTH:45%; BORDER-BOTTOM:#999999 1px solid; HEIGHT:50px">
							<legend style="BORDER-RIGHT:1px;BORDER-TOP:1px;BORDER-LEFT:1px;BORDER-BOTTOM:1px;BACKGROUND-COLOR:white">
								<%=GetString("ContractSignType")%>
							</legend>
							<asp:RadioButtonList id="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Width="512px" Visible=False>
<asp:ListItem Value="0">0</asp:ListItem>
<asp:ListItem Value="2" Selected="True">2</asp:ListItem>
							</asp:RadioButtonList>
						</fieldset>
					</td>
				</TR>
				<tr>
					<td height="30"><FONT face="宋体"></FONT></td>
				</tr>
				<TR>
					<TD align="center" height="25">
						<asp:linkbutton id="btnSave" Runat="server" cssclass="graybutton" Visible="True"></asp:linkbutton>
						<asp:linkbutton id="btnCancel" Runat="server" cssclass="graybutton" CausesValidation="False" Visible="True"></asp:linkbutton>
						<asp:linkbutton id="btnView" cssclass="graybutton" Runat="server" CausesValidation="False" Visible="True"></asp:linkbutton>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD class="StatusLine11" valign="middle">
						<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
					</TD>
				</TR>
			</TABLE>
			<asp:Literal id="lbError" runat="server"></asp:Literal>
		</form>
	</body>
</HTML>
