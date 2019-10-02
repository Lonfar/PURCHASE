<%@ Register TagPrefix="uc1" TagName="RefButton" Src="RefButton.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ChildEditControl.ascx.cs" Inherits="UserControls.ChildEditControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="RefEditor.ascx" %>
<P>
	<TABLE id="Table1" height="90%" cellSpacing="1" cellPadding="1" width="100%" border="0">
		<tr id="Tr1" runat="server">
			<td class="TitleText1" id="Td1" style="HEIGHT: 29px" align="center" runat="server">
				<asp:label id="lblTitle" runat="server" Width="415px" Font-Bold="True" Height="11px"></asp:label>
			</td>
		</tr>
		<TR>
			<TD style="HEIGHT: 28px">
				<asp:linkbutton id="lbAdd" runat="server" CausesValidation="False" CssClass="graybutton" onclick="lbAdd_Click">Ôö ¼Ó</asp:linkbutton>
				<uc1:refbutton id="RefButton1" runat="server"></uc1:refbutton>&nbsp;
				<asp:linkbutton id="lbDetete" runat="server" CausesValidation="False" CssClass="graybutton" onclick="lbDetete_Click">É¾ ³ý</asp:linkbutton>
			</TD>
		</TR>
		<TR vAlign="top">
			<TD bgcolor=#ffffff><asp:datagrid id="DataGrid1" runat="server" CssClass="ChildTable" width="100%">
					<PagerStyle Visible="False"></PagerStyle>
				</asp:datagrid></TD>
		</TR>
	</TABLE>
</P>
