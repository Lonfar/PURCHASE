<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrameMain.aspx.cs" Inherits="TopisWeb.WareHouse.WareHouseReport.FrameMain" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RefButton" Src="../../UserControls/RefButton.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>  
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
		<script type="text/javascript" src="../../myscripts/CoverEffect.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
		<asp:HiddenField runat="server" ID="hidDocumentValue"  />
     <table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="TopToolBarLine">
				<TR>
					<td width="10"></td>
					<TD><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></TD>
					</TR>
	 </table>
	 <div align="center" style="border-right: black 1px solid; border-top: black 1px solid;
                                overflow-y: auto; overflow-x: auto; border-left: black 1px solid; width: 840px;
                                border-bottom: black 1px solid; height: 433px; text-align: center" id="divTable">
	 <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
			<TR vAlign="top" align="center" >
						<TD>
						<asp:Literal ID="litTitle" runat="server"></asp:Literal>	<input type="button" runat="server" id="btnDownFiles" CausesValidation="False" style="display:none" />
										
						</TD>
						</TR>
					<TR vAlign="top">
						<TD align="center">
							<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCreated="GridView1_RowCreated"  CssClass="TableGlobalOne" Width="80%" >
                                 <Columns>
                                     <asp:BoundField DataField="DepName" />
                                     <asp:BoundField DataField="TotalItem" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="TotalValue"  DataFormatString="{0:N2}"  HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="PercentageValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"/>
                                     
                                     <asp:BoundField DataField="NineItem"  ItemStyle-HorizontalAlign="Right"/>
                                     <asp:BoundField DataField="NineValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="EightItem"  ItemStyle-HorizontalAlign="Right"/>
                                     <asp:BoundField DataField="EightValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="SevenItem"  ItemStyle-HorizontalAlign="Right"/>
                                     <asp:BoundField DataField="SevenValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="SixItem"  ItemStyle-HorizontalAlign="Right"/>
                                     <asp:BoundField DataField="SixValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     
                                     <asp:BoundField DataField="FiveItem"  ItemStyle-HorizontalAlign="Right"/>
                                     <asp:BoundField DataField="FiveValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="FourItem" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="FourValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="ThreeItem" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="ThreeValue"  DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"/>
                                     <asp:BoundField DataField="TwoItem" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="TwoValue"  DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="OneItem"  ItemStyle-HorizontalAlign="Right" />
                                     <asp:BoundField DataField="OneValue" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right"/>
                                 </Columns>
                            </asp:GridView>
						</TD>
					</TR>
					
			
	</TABLE>
         <div style="display:none;" id="sample">
			    <div style="padding-top:10px;"> 
                    <div style="margin:0px 0px 0px 0px; padding:0px 20px 0px 0px; " >
                    <img  src="../../Images/ImgProgress/Progress.gif" border="0" />
                        </div>
                        
                </div>  
            </div>
	</div>
    </form>
</body>
</html>
