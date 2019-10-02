<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UCSearch.ascx.cs" Inherits="UserControls.UCSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="skin" Namespace="Controls" Assembly="UserControls" %>
<skin:StyleSkin id="UCSearchSkin" runat="server"></skin:StyleSkin>
<table width="100%" align="center" border="0">
	<tr>
		<td>
			<asp:linkbutton id="lnkShowSearch" Runat="server" CssClass="BlackLink"></asp:linkbutton>
			<asp:linkbutton id="lnkHideSearch" Runat="server" CssClass="BlackLink"></asp:linkbutton>
		</td>
	</tr>
	<tr>
		<td><asp:panel id="Panel1" Runat="server" HorizontalAlign="Right">
				<table width="100%" align="center" border="0">
					<tr>
						<td align="right">
							<asp:button id="btnSearch" CssClass="graybutton" Runat="server" Text="Search" onclick="btnSearch_Click"></asp:button>
							<asp:button id="btnClear" CssClass="graybutton" Runat="server" Text="Clear" onclick="btnClear_Click"></asp:button>
						</td>
					</tr>
					<tr>
						<td>
							<asp:table id="tblListSearch" runat="server" CssClass="SearchTableArea" Width="100%"></asp:table>
						</td>
					</tr>
				</table>
			</asp:panel>
		</td>
	</tr>
</table>
<script  type="text/javascript" language="javascript">
		function document.onkeydown()
		{
			var e=event.srcElement;
			if(event.keyCode==13)
			{
				var searchID = '<%=btnSearch.ClientID%>';
				document.getElementById(searchID).click();
				return false;
			}
		}
</script>
