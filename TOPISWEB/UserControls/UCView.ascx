<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UCView.ascx.cs" Inherits="UserControls.UCView" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc2" TagName="AttchmentViewer" Src="AttchmentViewer.ascx" %>
<div align="center"> 
<asp:Panel id="Panel1" runat="server"></asp:Panel>
<p><asp:table id="tblChildTable" runat="server"></asp:table></p>
<p><uc2:AttchmentViewer id="AttchmentViewer1" runat="server"></uc2:AttchmentViewer></p>
<p>
</div> 
<asp:Button id="btn_Export" class="Noprn" CssClass = "graybutton" runat="server" style="COLOR:blue" onclick="btn_Export_Click"></asp:Button>
<asp:Button id="btn_Close"  class="Noprn" CssClass = "graybutton" runat="server" style="COLOR:blue"></asp:Button>
<asp:Button id="btn_Print"  class="Noprn" CssClass = "graybutton" runat="server" style="COLOR:blue" ></asp:Button>
</p>
