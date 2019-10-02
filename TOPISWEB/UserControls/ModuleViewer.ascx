<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../UserControls/RefEditor.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ModuleViewer.ascx.cs" Inherits="UserControls.ModuleViewer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="tabSelect" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
	<TR>
		<TD width="20%" align="right"><asp:label id="labUser" runat="server" Width="100%"></asp:label></TD>
		<TD width="30%"><uc1:RefEditor id="refUser" runat="server"></uc1:RefEditor></TD>
		<TD width="20%" align="right"><asp:label id="labDepartment" runat="server" Width="100%"></asp:label></TD>
		<TD width="30%"><uc1:RefEditor id="refDepartment" runat="server"></uc1:RefEditor></TD>
		<TD width="0" align="right"><asp:label id="labGroup" runat="server" Width="100%" Visible="false"></asp:label></TD>
		<TD width="0"><uc1:RefEditor id="refGroup" runat="server" Visible="false"></uc1:RefEditor></TD>
	</TR>
	<tr>
		<td colspan="6" height="20"></td>
	</tr>
</TABLE>
<TABLE id="tabSeePerson" cellSpacing="1" cellPadding="1" width="100%" border="0" class="TitleArea">
	<tr>
		<td class="TitleText1">
			<asp:label id="labSeePerson" runat="server" Width="100%"></asp:label>
		</td>
	</tr>
</TABLE>
<TABLE id="Tbl5" cellSpacing="1" cellPadding="1" width="80%" border="0" align="center">
	<TR vAlign="top">
		<TD align="center" Class="ChildTableBgColor">
			<asp:datagrid id="dgViewer" runat="server" width="100%" HeaderStyle-HorizontalAlign="Center" CssClass="TableGlobalOne"
				AutoGenerateColumns="False">
				<ItemStyle CssClass="TableRow"></ItemStyle>
				<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
				<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<Columns>
					<asp:BoundColumn HeaderStyle-CssClass = "ChildTableHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Center></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass = "ChildTableHeader" DataField="FullName" ItemStyle-Width="35%"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass = "ChildTableHeader" DataField="DepartmentName" ItemStyle-Width="35%"></asp:BoundColumn>
					<asp:BoundColumn HeaderStyle-CssClass = "ChildTableHeader" DataField="PositionName" ItemStyle-Width="25%"></asp:BoundColumn>
					<asp:TemplateColumn HeaderStyle-CssClass = "ChildTableHeader">
						<ItemTemplate >
							<asp:LinkButton id="cmdDel" runat="server" CausesValidation="false" Text="Delete"></asp:LinkButton>
						</ItemTemplate>
						<FooterTemplate>
							<FONT face="ËÎÌו"></FONT>
						</FooterTemplate>
						<EditItemTemplate>
							<FONT face="ËÎÌו"></FONT>
						</EditItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
		</TD>
	</TR>
</TABLE>
