<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ApproveStateInfo.ascx.cs" Inherits="UserControls.ApproveStateInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="1" cellPadding="1" width="100%" border="0" align="center" class="TitleArea">
	<TR>
		<TD class="TitleText1">
			<asp:Label id="Label1" runat="server">Label</asp:Label>
		</TD>
	</TR>
</table>
<TABLE id="Tbl5" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
	<TR vAlign="top">
		<TD align="center" Class="ChildTableBgColor">
			<asp:DataGrid id="dgApproveProcess" runat="server" AutoGenerateColumns="False" CssClass="TableGlobalOne"
				Width="100%" HeaderStyle-HorizontalAlign="Center">
				<ItemStyle CssClass="TableRow"></ItemStyle>
				<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
				<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
				<Columns>
					<asp:BoundColumn HeaderStyle-CssClass="ChildTableHeader" DataField="Num" HeaderText="Num"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="ChildTableHeader" DataField="FullName" HeaderText="Name"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="ChildTableHeader" DataField="Contents" HeaderText="ApproveContents"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="ChildTableHeader" DataField="ApprovedDate" HeaderText="ApprovedDate"
						DataFormatString="{0:d}"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass="ChildTableHeader" DataField="State" HeaderText="State"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
		</TD>
	</TR>
</TABLE>
