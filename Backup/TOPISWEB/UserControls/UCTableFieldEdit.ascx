<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UCTableFieldEdit.ascx.cs" Inherits="UserControls.UCTableFieldEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table1" runat="server">
	<tr>
		<td width = 100%><asp:TextBox Runat="server" ID="txtShowName" ReadOnly="True" Width = 100%></asp:TextBox></td>
		<td width = 0%><asp:TextBox Runat="server" ID="txtTableField" Width=0 ReadOnly="True" BorderStyle="None"></asp:TextBox></td>
		<td><asp:ImageButton Runat="server" ID="imgShowTableField" AlternateText="Sel" ImageAlign="AbsMiddle"
				CausesValidation="False"></asp:ImageButton></td>
		
	</tr>
</table>

