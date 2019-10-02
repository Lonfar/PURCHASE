<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueBillSearch.aspx.cs" Inherits="TopisWeb.WareHouse.WareHouseSearch.IssueBillSearch" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="cc3" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
	<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
	<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
	<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
	<script language="javascript">
		function search()
		{
		    document.getElementById("imgRefresh").style.visibility = "visible";
			window.parent.frames("RefContent").location='IssueBillReport.aspx?WHID='+document.getElementById("Ref_IssueFrom_txtRefID").value+'&AFE='+document.getElementById("Ref_AFENO_txtRefID").value;
		}	
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <TABLE Class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD>
						<cc3:ToolBar id="ToolBar1" runat="server"></cc3:ToolBar></TD>
				</TR>
			</TABLE>
			<br />
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD colSpan="4">
							<TABLE class="SearchTableArea"  width="90%" cellSpacing="1" cellPadding="1" align="left" border="0">
								<TR>
									<TD class="FormNormalTitle" style="WIDTH: 10%" align="right">
										<asp:Label id="lblIssueFrom" runat="server"></asp:Label></TD>
									<TD>
										<uc1:RefEditor id="Ref_IssueFrom" runat="server"></uc1:RefEditor></TD>
									<TD>&nbsp;</TD>
									<TD class="FormNormalTitle" style="WIDTH: 10%" align="right">
										<asp:Label id="lblAFENO" runat="server"></asp:Label></TD>
									<TD>
										<uc1:RefEditor id="Ref_AFENO" runat="server"></uc1:RefEditor></TD>	
									<TD><img id="imgRefresh" src="../../Images/ImgProgress/Progress.gif" style="visibility:hidden" alt="" /></TD>
								    <td>								    
								    <img src="../../Images/Page/find.gif" alt="Search" title="Search" border="0" onclick="search()" align="absMiddle" style="CURSOR:hand">	
								    </td>
								</TR>
							</TABLE>
						</TD>
					</TR>					
				</TBODY>
			</TABLE>
    </form>
</body>
</html>
