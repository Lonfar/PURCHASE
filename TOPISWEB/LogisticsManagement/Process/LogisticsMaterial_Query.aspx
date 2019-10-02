<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogisticsMaterial_Query.aspx.cs" Inherits="TopisWeb.LogisticsManagement.Process.LogisticsMaterial_Query" %>


<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEditSearch" Src="../../UserControls/UCEditSearch.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AdjustMaterialDetail</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../../MyScripts/Menu.js"></script>

    <script language="JavaScript" src="../../MyScripts/DatePicker.js"></script>

    <script language="JavaScript" src="../../MyScripts/Tab.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="TopToolBarLine" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td width="10">
                </td>
                <td>
                    <cc1:ToolBar ID="ToolBar1" runat="server">
                    </cc1:ToolBar></td>
            </tr>
        </table>
        <table id="Table3" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
            <tr valign="top">
                <td>
                    <uc2:UCList ID="Uclist1" runat="server"></uc2:UCList>
                </td>
            </tr>
            <tr valign="top">
                <td class="StatusLine" valign="middle">
                    <asp:Label ID="lblMSG" runat="server" Width="100%" Font-Bold="True"></asp:Label></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
