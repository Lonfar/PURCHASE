<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DateSearch.ascx.cs" Inherits="UserControls.DateSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table1" runat="server" cellSpacing=0 cellPadding=0>
	<tr>
		<TD vAlign="bottom" width="48%"><cc2:datepicker id="Datepicker1" runat="server" width="100%"></cc2:datepicker></TD>
		<TD vAlign="bottom" width="4%"><asp:literal id="litTO" Runat="server" ></asp:literal></TD>
		<td vAlign="bottom" align="right" width="48%"><cc2:datepicker id="Datepicker2" runat="server" width="100%"></cc2:datepicker></td>
	</tr>
</table>
