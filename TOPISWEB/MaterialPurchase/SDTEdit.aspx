<%@ Page language="c#" Codebehind="SDTEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialPurchase.SDTEdit" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RefButton" Src="../UserControls/RefButton.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Receive</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/DatePicker.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/DatePicker.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../MyScripts/Tab.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
	<table  Class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<tr>
			<td width="10"></td>
			<td><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></td>
		</tr>
	</Table>
	<table id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
		<tr vAlign="top">
		    <td>
			    <p></p>
		    </td>
	    </tr>
	    <tr vAlign="top">
		    <td style="HEIGHT: 55px">
		        <msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="100%">
				    <msp:TabPage ID="tabPage1">
                        <table id="TABLE3" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
							<tr vAlign="top">
								<td align="center">
									<uc1:UCEdit runat="server" ID="VoucherEdit"></uc1:UCEdit>
								</td>
							</tr>
						</table>
                    </msp:TabPage>   
                    <msp:TabPage ID="tabPage2">
                    <table id="TABLE1" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
                            
							 <tr valign="top">
									<td>
										<uc1:ChildEditControl id="ChildEdit_POList" runat="server"></uc1:ChildEditControl>											
									</td>
								</tr>  
                            
					</table>
				</msp:TabPage> 
				<msp:TabPage ID="tabPage3">
                    <table id="TABLE4" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
                            
							
							 <tr valign="top">
									<td>
										<uc1:ChildEditControl id="ChildEdit_MaterialList" runat="server"></uc1:ChildEditControl>											
									</td>
								</tr>  
					</table>
				</msp:TabPage>                                 
			    </msp:tabcontrol>
		    </td>
	    </tr>
	    <tr vAlign="top">
		<td class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></td>
	</tr>
	</table>		
		</form>
	</body>
</HTML>