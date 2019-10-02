<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DateEditor.ascx.cs" Inherits="UserControls.DateEditor" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Literal Runat="server" ID="litShowSelectScript" Visible="False"></asp:Literal>
<TABLE id="tb2" runat="server">
	<TR>
		<TD><asp:TextBox Runat="server" ID="txtDate" Width="100%"></asp:TextBox></TD>
		<TD align="right" width="15">
			<asp:ImageButton Runat="server" ID="imgShowSelector" AlternateText="Sel" ImageAlign="AbsMiddle" Height="19"
				Width="100%" CausesValidation="False"></asp:ImageButton>
		</TD>
	</TR>
</TABLE>
<asp:requiredfieldvalidator id="RequiredFieldValidator1" ControlToValidate="txtDate" Display="Dynamic" runat="server"
	ErrorMessage="Please select Datevalue " Enabled="False" Width="100%" Visible="false"></asp:requiredfieldvalidator>
<asp:RegularExpressionValidator ID="RegularExpress1" Enabled="False" ControlToValidate="txtDate" Display="Dynamic"
	Runat="server" ErrorMessage="" Width="100%" Visible="false"></asp:RegularExpressionValidator>
