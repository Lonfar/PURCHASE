<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiveBillFrame.aspx.cs" Inherits="TopisWeb.WareHouse.WareHouseSearch.ReceiveBillFrame" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<frameset rows="80px,*" framespacing="0" frameborder="0">
	<frame name="Search" id="Search" src="ReceiveBillSearch.aspx" width="100%" frameborder="0" scrolling="no" marginheight="0" marginwidth="0">
	<frame name="RefContent" id="RefContent" src="ReceiveBillReport.aspx?POID=<%=Server.UrlEncode(Request.QueryString["POID"])%>&VendorID=<%=Server.UrlEncode(Request.QueryString["VendorID"])%>&DepID=<%=Server.UrlEncode(Request.QueryString["DepID"])%>" frameborder="0">
</frameset>
</html>
