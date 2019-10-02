<%@ Register TagPrefix="uc1" TagName="UCView" Src="UCView.ascx" %>
<%@ Page language="c#" Codebehind="FileSubmit.aspx.cs" AutoEventWireup="false" Inherits="UserControls.FileSubmit" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="UCEdit.ascx" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FileSubmit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<TOPIS:PAGEDESCRIPTION id="FileSubmitPageDescription" runat="server" Text=""></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
				<tr>
					<td align="center">
						<uc1:ucedit runat="server" ID="ucEdit_Putin"></uc1:ucedit>
					</td>
				</tr>
				<tr><td height="30"></td></tr>
				<TR>
					<TD align="center" height="25">
						<asp:linkbutton id="btnSave" Runat="server" cssclass="graybutton"></asp:linkbutton>
						<asp:linkbutton id="btnCancel" Runat="server" cssclass="graybutton" CausesValidation="False"></asp:linkbutton>
						<asp:linkbutton id="btnView" cssclass="graybutton" Runat="server" CausesValidation="False" Visible = "True"></asp:linkbutton>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD class="StatusLine11" valign="middle">
						<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
					</TD>
				</TR>
			</TABLE>
		</form>
		<asp:Literal id="Literal1" runat="server" Visible="False"></asp:Literal>
	</body>
</HTML>
