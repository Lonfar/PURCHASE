<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Page language="c#" Codebehind="ShowMaterialList.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialPurchase.ShowMaterialList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MaterialRequest</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<base target="_self"></base>
		<script type="text/javascript" language="javascript">

		function searchEnter()
		{
			if(event.keyCode==13)
			{
				if(document.getElementById("skey").value != "")
				{
					__doPostBack('skey','Sort$Button');
				}
			}						
		}	
		function search()
		{		    
			if(document.getElementById("skey").value !="")
			{				
				__doPostBack('imgSearch','Sort$Img');
			}
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
	</HEAD>
	
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
		    
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="95%" border="0" >
				<TR>
					<td width="10">
					     
					</td>
					<TD width="95%" align="right">		    
					    <asp:Literal Runat="server" ID="litSearch" Text="Search Condition" ></asp:Literal>
					    <input type="text" size="40"  maxlength="50" id="skey" name="skey" OnKeyDown="searchEnter()" runat="server">
					    <img id="imgSearch" src="../Images/Page/find.gif" alt="Search" title="Search" border="0" onclick="search()" align="absMiddle" style="cursor:hand">
					</TD>
				</TR>
			</TABLE>
			
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
				    <tr>
					    <td align="right" height="20">
						    <asp:LinkButton Runat="server" ID="btnSelectThese" Text="Select These"  CssClass="graybutton" OnClick="btnSelectThese_Click">
						    </asp:LinkButton>
					    </td>
				    </tr>
			    
					<TR vAlign="top">
						<TD>
							<P>
							    <asp:GridView ID="gv_MaterialaList"  CssClass="TableGlobalOne" runat="server" Width="95%" AllowSorting="true" AutoGenerateColumns="false">
    							<Columns>
    							    <asp:BoundField DataField="ItemCode" HeaderText="ItemCode"  />
    							    <asp:BoundField DataField="MaterialName" HeaderText="MaterialName"   />
    							    <asp:BoundField DataField="ProductStandard" HeaderText="ProductStandard"   />
    							    <asp:BoundField DataField="UOMID" HeaderText="UOMID"   />
    							    <asp:BoundField DataField="MFG" HeaderText="MFG"  />
    							     <asp:TemplateField>
                                           <HeaderTemplate><input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" /></HeaderTemplate>
                                           <ItemTemplate><asp:CheckBox ID="chk" runat="server" /></ItemTemplate>
                                     </asp:TemplateField>
    							</Columns>
							    </asp:GridView>							
							</P>
						</TD>
					</TR>

				</TBODY>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<tr>
						<td colSpan="4"><br>
							<WEBDIYER:ASPNETPAGER id="pager" runat="server" CssClass="mypager" Width="100%" PageSize="15" PagingButtonType="Image"
								NavigationToolTipTextFormatString="Turn To Page {0}" PageIndexOutOfRangeErrorString="page index out of range"
								InvalidPageIndexErrorString="the page index is invalid" TextBeforeInputBox="Turn To " DisabledButtonImageNameExtension="g"
								CpiButtonImageNameExtension="r" ButtonImageNameExtension="n" ImagePath="../Images/aspnetpager/" ShowCustomInfoSection="left"
								NumericButtonTextFormatString="[{0}]" SubmitButtonText="Submit" InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
								SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px" PagingButtonSpacing="4px"
								ShowInputBox="Always" Height="25px" HorizontalAlign="Right"></WEBDIYER:ASPNETPAGER></td>
					</tr>
				</TBODY>
			</TABLE>
			<%--<iframe name="targetIFrame" style="width:0px" src="about:blank"></iframe>--%>
		</form>
	</body>
</HTML>
