<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RefEditor.ascx.cs" Inherits="UserControls.RefEditor" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table1" runat="server"  cellSpacing=0  cellPadding=0>
	<tr>
		<TD><asp:textbox id="txtRefID" Runat="server" Width="0px"  style="display:none"   Visible="true"></asp:textbox>
		<asp:textbox id="txtRefName"   Runat="server" Width=99%></asp:textbox></TD>
		<td width="15"  id="selectTD" runat=server align="right">
			<asp:imagebutton id="imgShowSelector" Runat="server" CausesValidation="False" ImageAlign="AbsMiddle"
				 Width="100%" Height="19" ></asp:imagebutton>			
		</td>
	</tr>
</table>
<asp:requiredfieldvalidator id="RequiredFieldValidator1" Enabled="False" ControlToValidate="txtRefName" Display="Dynamic"
	runat="server"   ErrorMessage="Required" Visible="false" Width="100%"></asp:requiredfieldvalidator>
