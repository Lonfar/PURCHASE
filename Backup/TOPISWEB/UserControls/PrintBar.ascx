<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PrintBar.ascx.cs" Inherits="UserControls.PrintBar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE width="925px" align="center" class="NoPrint" height="40px" bgcolor="#97acce" >
	<TR>
		<TD align="center" style="font-weight:bold">
			<a href="javascript:PrintPreview('<%#GetPublicString("string","sCannotPreview")%>')">
				<asp:Literal Runat="server" Text='<%#GetString("litPrintPreview")%>' ID="litPrintPreview">
				</asp:Literal>
			</a>&nbsp;&nbsp; <a href="javascript:PrintSetting('<%#GetPublicString("string","sCannotPageSetup")%>')">
				<asp:Literal Runat="server" Text='<%#GetString("litPrintSetting")%>' ID="litPrintSetting" >
				</asp:Literal>
			</a>&nbsp;&nbsp; <a href="javascript:Print()">
				<asp:Literal Runat="server" Text='<%#GetString("litPrint")%>' ID="litPrint" >
				</asp:Literal>
			</a>&nbsp;&nbsp; <a href="javascript:window.close()">
				<asp:Literal Runat="server" Text='<%#GetString("litCloseWindow")%>' ID="litCloseWindow" >
				</asp:Literal></a>
		</TD>
	</TR>
</TABLE>
