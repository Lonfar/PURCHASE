<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RefButton.ascx.cs" Inherits="UserControls.RefButton" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:TextBox Runat="server" ID="txtRefName" Visible="False"></asp:TextBox>
<asp:TextBox Runat="server" ID="txtRefID" ReadOnly="True" BorderStyle="None" Width="0px"></asp:TextBox>
<asp:LinkButton id="confirm" runat="server" CssClass="graybutton" CausesValidation="False" onclick="confirm_Click"></asp:LinkButton>
