<%@ Control Language="c#" AutoEventWireup="false" Codebehind="UCEditSearch.ascx.cs" Inherits="UserControls.UCEditSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<TOPIS:STYLESKIN id="StyleSkinUCVouchorEdit" runat="server"></TOPIS:STYLESKIN>
<TABLE width="100%" align="center" border="0">
	<TR>
		<TD>
			<asp:linkbutton id="lnkShowSearch" CssClass="BlackLink" Runat="server"></asp:linkbutton>
			<asp:linkbutton id="lnkHideSearch" CssClass="BlackLink" Runat="server"></asp:linkbutton>
		</TD>
	</TR>
	<tr>
		<TD><asp:panel id="Panel1" Runat="server" HorizontalAlign="Right">
<asp:table id=tblVoucher runat="server" CssClass="SearchTableArea" Width="100%"></asp:table>
			</asp:panel>
		</TD>
	</tr>
</TABLE>
