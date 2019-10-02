<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemCodeIN.aspx.cs" Inherits="TopisWeb.MaterialManagement.ItemCodeIN" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>GetExcelData</title>
		<meta content="True" name="vs_snapToGrid">
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet" />
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet" />
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet" />
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet" />
		<base target="_self" />
		<script type="text/javascript" language="javascript">
		    function ShowImage()
		    {   
		       
		        document.getElementById('<%= btnInputExcel.ClientID %>').style.visibility = 'hidden';
		        document.getElementById('imgRefresh').style.visibility = 'visible';
		       
		    }    
		</script>
  </HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
		    
			<table cellSpacing="0" cellPadding="0" width="80%" align="center"
				border="0">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="TitleText1" align="center" style="HEIGHT: 30px"><asp:label id="lblTitle" runat="server">Input Excel</asp:label></td>
				</tr>
				<tr>
					<td style="HEIGHT: 14px" align="right"><a href="../Template/Itemcode_template.xls" target="_blank" >Itemcode Template</a></td>
				</tr>
				<tr>
					<td align="center"><input id="File1" type="file" runat="server" />
						<asp:button id="btnInputExcel" runat="server" onclick="btnInputExcel_Click" OnClientClick="ShowImage()"></asp:button>
                        &nbsp; &nbsp;
                        <img id="imgRefresh" src="../Images/ImgProgress/Progress.gif" style="visibility:hidden" alt="" /></td>
				</tr>
				<tr>
					<td align="center"></td></tr>
				<tr vAlign="top">
					<td class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
