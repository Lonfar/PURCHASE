<%@ Page language="c#" Codebehind="StatisticsWareHouseChart.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.WareHouseReport.StatisticsWareHouseChart" %>

<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>StatisticsWareHouse</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<script type="text/javascript" src="../../myscripts/CoverEffect.js" ></script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
		<asp:HiddenField runat="server" ID="hidDocumentValue"  />
		<input type="button" runat="server" id="btnDownFiles" CausesValidation="False" style="display:none" />
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="TopToolBarLine">
				<TR>
					<td width="10"></td>
					<TD><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD>
							<P><uc1:UCList id="ucList_StatisticsWareHouse" runat="server"></uc1:UCList></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" valign="middle">
							<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			 <div style="display:none;" id="sample">
			    <div style="padding-top:10px;"> 
                    <div style="margin:0px 0px 0px 0px; padding:0px 20px 0px 0px; " >
                    <img  src="../../Images/ImgProgress/Progress.gif" border="0" />
                        </div>
                        
                </div>  
            </div>
		</form>
	</body>
</HTML>

