<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../../UserControls/ApproveStateInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>

<%@ Page Language="c#" Codebehind="ReceiveEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.WareHouseOperation.ReceiveEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Receive</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/DatePicker.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../../MyScripts/DatePicker.js"></script>

    <script language="JavaScript" src="../../MyScripts/Menu.js"></script>

    <script language="JavaScript" src="../../MyScripts/Tab.js"></script>

</head>
<body ms_positioning="FlowLayout">
    <form id="Form1" method="post" runat="server">
        <table class="TopToolBarLine" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td width="10">
                </td>
                <td>
                    <cc1:ToolBar ID="ToolBar1" runat="server">
                    </cc1:ToolBar></td>
            </tr>
        </table>
        <table id="Table1" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
            <tbody>
                <tr valign="top">
                    <td>
                    </td>
                </tr>
                <tr valign="top">
                    <td style="height: 55px">
                        <msp:TabControl ID="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080"
                            Width="1000px">
                            <msp:TabPage ID="tabPage1">
                                <table id="Tblucvoucheredit" cellspacing="1" cellpadding="1" width="100%" border="0"
                                    align="center">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnSubmit" runat="server" CausesValidation="False"></asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="center">
                                            <uc1:UCEdit runat="server" ID="ucEdit_Receive"></uc1:UCEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager>
                                        </td>
                                    </tr>
                                </table>
                            </msp:TabPage>
                            <msp:TabPage ID="tabPage2">
                                <table id="Tblucvoucheredit" cellspacing="2" cellpadding="2" width="99%" border="0"
                                    align="center">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnRefresh" runat="server" CausesValidation="False"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr valign="top" align="center">
                                        <td align="center" width="95%" colspan="2">
                                            <uc1:ChildEditControl ID="CEdit_ReceiveMaterial" runat="server"></uc1:ChildEditControl>
                                        </td>
                                    </tr>
                                </table>
                            </msp:TabPage>
                            <msp:TabPage ID="tabPage3">
                                <table id="tb2" cellspacing="1" cellpadding="1" width="100%" border="0" align="center">
                                    <tr>
                                        <td>
                                            <uc1:ChildEditControl ID="CEdit_ReceiveOSD" runat="server"></uc1:ChildEditControl>
                                        </td>
                                    </tr>
                                </table>
                            </msp:TabPage>
                            <msp:TabPage ID="tabPage4">
                                <table id="Tbl3" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                    <tr valign="top">
                                        <td>
                                            <uc1:ApproveStateInfo ID="ApproveStateInfo1" runat="server"></uc1:ApproveStateInfo>
                                        </td>
                                    </tr>
                                </table>
                            </msp:TabPage>
                        </msp:TabControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="../../MaterialPurchase/POTracking.aspx?moduleID=MaterialPurchase.POTracking" target="_blank" style="text-decoration: underline; color:blue">
                            PO Tracking</a>&nbsp;&nbsp;&nbsp;<a href="../WareHouseManagment/InStoreMaterialDetail.aspx?moduleID=WareHouse.InStoreMaterialDetail"
                                target="_blank" style="text-decoration: underline; color:blue">Material Inventory Query</a>&nbsp;&nbsp;&nbsp;<a
                                    href="../WareHouseManagment/WH_Material_Receive_Query.aspx?moduleID=WareHouse.WareHouseManagment.MaterialReceivingQuery"
                                    target="_blank" style="text-decoration: underline; color:blue">Material Receiving Query</a>&nbsp;&nbsp;&nbsp;<a
                                        href="Issue.aspx?moduleID=WareHouse.Issue" target="_blank" style="text-decoration: underline; color:blue">Material
                                        Issue Query</a></td>
                </tr>
                <tr valign="top">
                    <td class="StatusLine" valign="middle">
                        <asp:Label ID="lblMSG" runat="server" Width="100%"></asp:Label></td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
