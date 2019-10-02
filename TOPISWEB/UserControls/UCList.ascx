<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register TagPrefix="uc1" TagName="UCSearch" Src="UCSearch.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UCList.ascx.cs" Inherits="UserControls.UCList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div style="WIDTH: 100%; POSITION: relative; HEIGHT: 100px" ms_positioning="GridLayout">
	<table width="100%" align="center" border="0">
		<tr>
			<td>
				<uc1:UCSearch id="UCSearch1" runat="server"></uc1:UCSearch></td>
		</tr>
	</table>
	<table class="TitleArea" width="100%" align="center" border="0">
		<tr>
			<td class="TitleText1" align="center"><asp:literal id="litTitle" Runat="server"></asp:literal></td>
		</tr>
	</table>
	<table id="tbMain" width="100%" border="0" runat="server">
		<tr>
			<td><asp:datagrid id="dgrdList" runat="server" CssClass="TableGlobalOne" width="100%">
					<PagerStyle Visible="False"></PagerStyle>
				</asp:datagrid></td>
		</tr>
		<tr>
			<td><WEBDIYER:ASPNETPAGER id="pager" runat="server" CssClass="mypager" ShowInputBox="Always" PagingButtonSpacing="4px"
					Height="25px" SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px"
					InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
					SubmitButtonText="Submit" NumericButtonTextFormatString="[{0}]" ShowCustomInfoSection="left" ImagePath="../Images/aspnetpager/"
					ButtonImageNameExtension="n" CpiButtonImageNameExtension="r" DisabledButtonImageNameExtension="g"
					TextBeforeInputBox="Turn To " InvalidPageIndexErrorString="the page index is invalid" PageIndexOutOfRangeErrorString="page index out of range"
					NavigationToolTipTextFormatString="Turn To Page {0}" PagingButtonType="Image" PageSize="15" HorizontalAlign="Right"
					Width="100%"></WEBDIYER:ASPNETPAGER></td>
		</tr>
	</table>
</div>
