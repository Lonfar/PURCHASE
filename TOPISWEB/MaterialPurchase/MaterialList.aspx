<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MaterialList.aspx.cs"  Inherits="TopisWeb.MaterialPurchase.MaterialList" %>

<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>MaterialRequest</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Styles/Default.CSS" type="text/css" rel="stylesheet" />
    <link href="../Styles/Main.CSS" type="text/css" rel="stylesheet" />
    <base target="_self" />

    <script type="text/javascript" language="javascript">

		function searchEnter()
		{
			
			if(event.keyCode==13)
			{
			    debugger;
//				__doPostBack('skey','Sort$Button');
                document.getElementById("hidSub").click();
			}						
		}	
		function search()
		{		    
			//__doPostBack('imgSearch','Sort$Img');
			debugger;
			document.getElementById("hidSub").click();
		}

        function SelectAllCheckboxes(spanChk){

           // Added as ASPX uses SPAN for checkbox
           var oItem = spanChk.children;
           var theBox= (spanChk.type=="checkbox") ?spanChk : spanChk.children.item[0];
           xState=theBox.checked;
           elm=theBox.form.elements;

           for(i=0;i<elm.length;i++)
             if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
             {
               //elm[i].click();
               if(elm[i].checked!=xState)
                 elm[i].click();
               //elm[i].checked=xState;          
             }
         }
         
    </script>

</head>
<body ms_positioning="FlowLayout">
    <form id="Form1" method="post" runat="server">
        <asp:Literal runat="server" ID="litSearch"></asp:Literal>
        <table id="Table1" cellspacing="0" cellpadding="0" width="95%" border="0">
            <tr>
                <td align="right">
                    <b>PO NO:</b></td>
                <td>
                    <input type="text" size="20" maxlength="15" id="txtPO" name="skey" onkeyup="searchEnter()"
                        runat="server" />
                </td>
                <td align="right">
                    <b>BIN NO:</b></td>
                <td>
                    <input type="text" size="20" id="txtBin" name="skey" onkeyup="searchEnter()" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>ItemCode:</b>
                </td>
                <td>
                    <input type="text" size="20" id="txtItemCode" name="skey" onkeyup="searchEnter()"
                        runat="server" /></td>
                <td align="right">
                    <b>Description:</b>
                </td>
                <td>
                    <input type="text" size="40" maxlength="50" id="skey" name="skey" onkeyup="searchEnter()"
                        runat="server" />
                    <img id="imgSearch" src="../Images/Page/find.gif" alt="Search" title="Search" border="0"
                        onclick="search()" align="absMiddle" style="cursor: hand" /></td>
            </tr>
        </table>
        <table id="Table2" cellspacing="1" cellpadding="1" width="95%" border="0" align="center">
            <tbody>
                <tr>
                    <td align="right" height="20">
                        <asp:LinkButton runat="server" ID="btnSelectThese" Text="Select These" CssClass="graybutton"
                            OnClick="btnSelectThese_Click">
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <p>
                            <asp:GridView ID="gv_MaterialaList" CssClass="TableGlobalOne" runat="server" Width="95%"
                                AllowSorting="true" AutoGenerateColumns="false">
                                <Columns>
                                    
                                    <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
                                    <asp:BoundField DataField="WHID" HeaderText="WHID" />
                                    <asp:BoundField DataField="POID" HeaderText="POID" />
                                    <asp:BoundField DataField="BINID" HeaderText="BINID" />
                                    <asp:BoundField DataField="QuantityInBin" HeaderText="Quantity" HtmlEncode="false" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="UnitPricePOStandard" HeaderText="UnitPrice(S)" HtmlEncode="false" DataFormatString="{0:F2}" />
                                    <asp:BoundField DataField="MaterialName" HeaderText="MaterialName" />
                                    <asp:BoundField DataField="UOMID" HeaderText="UOMID" />
                                    <asp:BoundField DataField="ReceiveDate" HeaderText="ReceiveDate" HtmlEncode="False"
                                        DataFormatString="{0:d}" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                type="checkbox" /></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox_Changed"/></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </p>
                    </td>
                </tr>
            </tbody>
        </table>
        <table id="Table5" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
            <tbody>
                <tr>
                    <td colspan="4">
                        <br>
                        <webdiyer:AspNetPager ID="pager" runat="server" CssClass="mypager" Width="100%" PageSize="15"
                            PagingButtonType="Image" NavigationToolTipTextFormatString="Turn To Page {0}"
                            PageIndexOutOfRangeErrorString="page index out of range" InvalidPageIndexErrorString="the page index is invalid"
                            TextBeforeInputBox="Turn To " DisabledButtonImageNameExtension="g" CpiButtonImageNameExtension="r"
                            ButtonImageNameExtension="n" ImagePath="../Images/aspnetpager/" ShowCustomInfoSection="left"
                            NumericButtonTextFormatString="[{0}]" SubmitButtonText="Submit" InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
                            SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px"
                            PagingButtonSpacing="4px" ShowInputBox="Always" Height="25px" HorizontalAlign="Right">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </tbody>
        </table>
        <input type="button" runat="server" id="hidSub" style="display:none;" onserverclick="hidSubClick" />
        <%--<iframe name="targetIFrame" style="width:0px" src="about:blank"></iframe>--%>
    </form>
</body>
</html>
