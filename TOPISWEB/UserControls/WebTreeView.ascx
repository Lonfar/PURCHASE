<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebTreeView.ascx.cs" Inherits="UserControls.WebTreeView" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<iewc:treeview id="webTree" runat="server" SystemImagesPath="../images/tree/" 
DefaultStyle="font-size:11px;font-family:Arial;" 
SelectedStyle="Color:Black;background-color:#FDF4A2;" 
HoverStyle="Color:White;background-color:#333333;" Target="BasicTables"  ExpandLevel="1" SelectExpands="True" AutoSelect="True"
ShowPlus="True" EnableViewState="False"></iewc:treeview>