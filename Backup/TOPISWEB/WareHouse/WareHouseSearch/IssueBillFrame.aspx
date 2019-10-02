<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueBillFrame.aspx.cs" Inherits="TopisWeb.WareHouse.WareHouseSearch.IssueBillFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<frameset rows="80px,*" framespacing="0" frameborder="0">
	<frame name="Search" id="Search" src="IssueBillSearch.aspx" width="100%" frameborder="0" scrolling="no" marginheight="0" marginwidth="0">
	<frame name="RefContent" id="RefContent" src="IssueBillReport.aspx?WHID=<%=Server.UrlEncode(Request.QueryString["WHID"])%>&AFE=<%=Server.UrlEncode(Request.QueryString["AFE"])%>" frameborder="0">
</frameset>
</html>
