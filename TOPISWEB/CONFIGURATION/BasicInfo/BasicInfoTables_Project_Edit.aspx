<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Page language="c#" Codebehind="BasicInfoTables_Project_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BasicInfoTables_Project_Edit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BasicInfoSex_Edit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<style type="text/css">
		img 
		{
			max-width: 200px; width:expression(this.width > 200 ? 200:this.width);
			max-height:150px; height:expression(this.height > 150 ? 150:this.height)
		}
		</style>		
		<script type="text/javascript" language="javascript">			
			function doShowPic()
			{				
				var imgLogo = document.getElementById('imgLogo');
				var imgPath = document.getElementById('uplTheFile');
				
				if( imgPath != null && imgPath.value != "" )
				{
					imgLogo.setAttribute("src",imgPath.value);
				}				
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<FORM id="Form1" method="post" runat="server">
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
							<P><uc1:ucedit id="VoucherEdit" runat="server"></uc1:ucedit></P>
						</TD>
					</TR>
					<tr valign="top">
						<td style="HEIGHT: 55px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;
							<asp:Label id="lblLogo" runat="server" CssClass="FormNormalTitle"></asp:Label>&nbsp;</FONT><INPUT id="uplTheFile" type="file" size="60" name="uplTheFile" onchange="doShowPic()" unselectable="on" runat="server">
							<P></P>
							<P>&nbsp;
								<asp:image id="imgLogo" Runat="server" BorderWidth="0"></asp:image></P>
						</td>
					</tr>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%">ÏîÄ¿</asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
			<P><FONT face="ËÎÌå"></FONT>&nbsp;</P>
		</FORM>
	</body>
</HTML>
