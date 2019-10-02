<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<%@ Page language="c#" Codebehind="RefList.aspx.cs" AutoEventWireup="True" Inherits="UserControls.RefList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<script language="javascript">
			function SelectRef(RefCode)
			{
				window.returnValue=RefCode;
				window.parent.close();
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">
		<form id="Form1" method="post" runat="server">
			<TABLE class="RefHeadArea"  width="95%"	align="center" border=0>
				<tr>
					<td class="TitleText1"><asp:literal id=litTitle  Runat="server"></asp:literal></td>
				</tr>
			</TABLE>
			<table align="center" width="95%" border=0>
				<tr>
					<td align="right" height="20">
						<asp:LinkButton Runat="server" ID="btnSelectThese"  CssClass="graybutton">
						</asp:LinkButton>
						<asp:LinkButton Runat="server"  Text= "ClearItem" ID="btnClearItem"  CssClass="graybutton">
						</asp:LinkButton>
					</td>
				</tr>
			</table>
			<asp:datagrid id="dgrdList" runat="server" BorderWidth="1" Width="95%" HorizontalAlign="Center"
				AutoGenerateColumns="False" CssClass="RefTable" AllowSorting="False">
				<HeaderStyle CssClass="RefTableHeader"></HeaderStyle>
				<ItemStyle CssClass="RefTableRow"></ItemStyle>
				<Columns>				
				</Columns>
			</asp:datagrid>
			<table align="center" width="100%" border=0>
				<tr>
					<td><WEBDIYER:ASPNETPAGER id="pager" runat="server" Width="100%" HorizontalAlign="Right" CssClass="mypager"
					PageSize="15" PagingButtonType="Image" NavigationToolTipTextFormatString="Turn To Page {0}" PageIndexOutOfRangeErrorString="page index out of range"
					InvalidPageIndexErrorString="the page index is invalid" TextBeforeInputBox="Turn To " DisabledButtonImageNameExtension="g"
					CpiButtonImageNameExtension="r" ButtonImageNameExtension="n" ImagePath="../Images/aspnetpager/" ShowCustomInfoSection="left"
					NumericButtonTextFormatString="[{0}]" SubmitButtonText="Submit" InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
					SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px" Height="25px"
					PagingButtonSpacing="4px" ShowInputBox="Always"></WEBDIYER:ASPNETPAGER></td>
				</tr>
			</table>
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal>
	</body>
</HTML>
