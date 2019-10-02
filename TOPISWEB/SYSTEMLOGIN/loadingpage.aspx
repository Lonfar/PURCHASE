<%@ Page language="c#" Codebehind="loadingpage.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.loadingpage" %>
<HTML>
	<HEAD>
		<title>loadingpage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<script>
			function BeginPageLoad() 
			{
				location.href = "<%= getHref()%>";
			}			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="BeginPageLoad()">
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="Panel1" style="Z-INDEX: 101; LEFT: 260px; POSITION: absolute; TOP: 192px" runat="server">
				<div style="BORDER-RIGHT: #707888 1px solid; BORDER-TOP: #707888 1px solid; OVERFLOW: hidden; BORDER-LEFT: #707888 1px solid; WIDTH: 322px; BORDER-BOTTOM: #707888 1px solid; POSITION: absolute; HEIGHT: 14px">
					<div id="pimg" style="LEFT: 0px; POSITION: absolute; TOP: -1px"></DIV>
					<font face="ËÎÌו"></FONT><FONT face="ËÎÌו"></FONT>
				</div>
				<div id="abc" style="FONT-SIZE: 9pt; LEFT: 120px; COLOR: #f4f4f4; POSITION: absolute; TOP: 30px">Loading.............
				</div>
				<script>
			
			
s=new Array();
s[0]="#e1e9e7"
s[1]="#d2dfdc";
s[2]="#bdd2cc";
s[3]="#9ebcb3";
s[4]="#78a095";
s[5]="#518274";
s[6]="#1f5647";
function ls(){
		pimg.innerHTML="";
		for(i=0;i<9;i++){
		pimg.innerHTML+="<input style=\"width:15;height:10;border:0;background:"+s[i]+";margin:1\">";
		}
	}
	
function rs(){
		pimg.innerHTML="";
		for(i=9;i>-1;i--){
		pimg.innerHTML+="<input style=\"width:15;height:10;border:0;background:"+s[i]+";margin:1\">";
		}
	}
	
ls();
var g=0;sped=0;
function str(){
	if(pimg.style.pixelLeft<350&&g==0){
	if(sped==0){
		ls();
		sped=1;
		}
	pimg.style.pixelLeft+=2;
	setTimeout("str()",1);
	return;
	}
	g=1;
	if(pimg.style.pixelLeft>-200&&g==1){
	if(sped==1){
		rs();
		sped=0;
		}
	pimg.style.pixelLeft-=2;
	setTimeout("str()",1);
	return;
	}
	g=0;
	str();
}

function flashs(){
if(abc.style.color=="#f4f4f4"){
	abc.style.color="#1f5647";
	setTimeout('flashs()',500);
	}
else{
	abc.style.color="#f4f4f4";
	setTimeout('flashs()',500);
	}
}
flashs();
str();
				</script>
			</asp:Panel><font face="Arial"></font><font face="Arial"></FONT>
		</form>
	</body>
</HTML>
