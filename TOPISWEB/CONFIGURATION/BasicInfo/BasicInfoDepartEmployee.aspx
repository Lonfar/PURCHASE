<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Page language="c#" Codebehind="BasicInfoDepartEmployee.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BasicInfoDepartEmployee" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BasicInfoEmployee</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<script language="javascript">
		function refreshTree(sNodeID){
			window.parent.frames("BasicTableTree").location="BasicInfoDepartEmployee_Tree.aspx?NodeID=" + sNodeID;
		}	
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE id="Table0" cellSpacing="0" cellPadding="0" width="100%" border="0" class="TopToolBarLine">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD style="HEIGHT: 21px">
							<P>
								<uc1:UCList id="VoucherList" runat="server"></uc1:UCList></P>
						</TD>
					</TR>
					<TR vAlign="top">
					<TD class="StatusLine" valign="middle">
						<asp:label id="lblMSG" runat="server" Width="100%">Status:</asp:Label>
					</TD>
				</TR>
				</TBODY>
			</TABLE>
			<P>&nbsp;</P>
		</FORM>
	</body>
</HTML>
