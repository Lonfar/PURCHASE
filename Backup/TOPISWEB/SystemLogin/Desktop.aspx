<%@ Page language="c#" Codebehind="Desktop.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Desktop" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel">
	<HEAD>
		<TOPIS:STYLESKIN id="Desktop1" runat="server"></TOPIS:STYLESKIN>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="Styles/style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Tab.js"></SCRIPT>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<style type="text/css">BODY { MARGIN: 0px; bgcolor: #FF0000 }
		</style>
		<script language="javascript">
		    function HrefClick()
		    {
		        event.returnValue = false;
		    }
		</script>
	</HEAD>
	<body id="thebody" bgColor="white" MS_POSITIONING="FlowLayout">
		<table class="Desktop_Topline" height="26" cellSpacing="0" cellPadding="0" width="100%">
			<tr>
				<td width="10"></td>
				<td vAlign="middle">
					<IMG src="../images/Office2003/ToolBarHeadLine.gif" border="0">
				</td>
			</tr>
		</table>
		<BR>
		<map name="Map">
		     <area shape="rect" coords="37,23,333,108" href="" onclick="HrefClick();" target="_self" alt="Material Purchase">
             <area shape="rect" coords="377,27,654,108" href="" onclick="HrefClick();" target="_self" alt="Tender Operation">
             <area shape="rect" coords="36,153,329,239" href="" onclick="HrefClick();" target="_self" alt="Clearance & Transporation">
             <area shape="rect" coords="379,156,654,239" href="" onclick="HrefClick();" target="_self" alt="Wharehouse">
        </map>

		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="85%" align="center">
				<tr>
					<td align="center">
						<fieldset style="WIDTH: 100%" align="center"><legend style="BORDER-RIGHT: lightsteelblue 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: lightsteelblue 1px solid; PADDING-LEFT: 2px; FONT-WEIGHT: bold; FONT-SIZE: 12px; PADDING-BOTTOM: 2px; BORDER-LEFT: lightsteelblue 1px solid; COLOR: #2f4f4f; PADDING-TOP: 2px; BORDER-BOTTOM: lightsteelblue 1px solid; BACKGROUND-COLOR: #dce8f4"><asp:label id="lblPendingService" runat="server">Warning</asp:label></legend>
							<table width="100%" align="center" border="0">
								<tr vAlign="top">
									<IMG src="../Images/Page/Desktop.jpg" border="0"  usemap="#Map">


								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
			</table>
			<BR>
			
			<table cellSpacing="0" cellPadding="0" width="85%" align="center">
				<tr>
					<td align="center">
						<fieldset style="WIDTH: 100%" align="center"><legend style="BORDER-RIGHT: lightsteelblue 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: lightsteelblue 1px solid; PADDING-LEFT: 2px; FONT-WEIGHT: bold; FONT-SIZE: 12px; PADDING-BOTTOM: 2px; BORDER-LEFT: lightsteelblue 1px solid; COLOR: #2f4f4f; PADDING-TOP: 2px; BORDER-BOTTOM: lightsteelblue 1px solid; BACKGROUND-COLOR: #dce8f4"><asp:label id="lbNote" runat="server">Note</asp:label></legend>
							<table width="100%" align="center" border="0">
								<tr vAlign="top">
									
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
			</table>
			<br>
		</form> <!--		</CENTER>   -->

		</body>
</HTML>
