var MNoneText,MLangID,MImagePath;
var MblnDisableNone,MblnIsShown;
var MDateRef,MSelectorRef,CalendarIframe,MCalendarArea;
var MdtToday,MdtSeasonStart,MdtSeasonEnd;
var MintFormatMode=1;
var MintOneMinute=60*1000;
var MintOneHour=MintOneMinute*60;
var MintOneDay=MintOneHour*24;
var MSltDay=0;
var MSltMonth=0;
var MSltYear=0;
var MintCrtMonth=0;
var MintCrtYear=0;
var MSplictChar="-";
var MDoPostBack=false;

//document.write("<div id=meizzCalendarLayer style='border:1px;position: absolute; z-index: 9999; width: 188; height: 194; display:none;'>");
//document.write("<iframe ID=CalendarIframe scrolling=no frameborder=0 width=188 height=194 style='z-index: 10'></iframe>");

if(navigator.userAgent.indexOf("Gecko")>0)
{
	MBrowser="Gecko";
	document.onclick=MHideDateSelector
}
else
{
	MBrowser="IE";
	document.onclick=function()
	{ 
		MHideDateSelector(event)
	}
}

if((typeof(HTMLElement)!="undefined")&&(!HTMLElement.prototype.insertAdjacentHTML))
{
	HTMLElement.prototype.insertAdjacentHTML=function(where,htmlStr)
	{
		var r=this.ownerDocument.createRange();
		r.setStartBefore(this);
		var parsedHTML=r.createContextualFragment(htmlStr);
		this.appendChild(parsedHTML);
	}
}


function HTMLShowDateSelector(DateRef,EventRef,DisableNone,LangID,ImagePath)
{

	ShowDateSelector(DateRef,EventRef,DisableNone,LangID,ImagePath,false)
}

function MShowDateSelector(DateRef,EventRef,LangID,ImagePath,NeedPBack)
{
	ShowDateSelector(DateRef,EventRef,false,LangID,ImagePath,NeedPBack)
}

function ShowDateSelector(DateRef,EventRef,DisableNone,LangID,ImagePath,NeedPBack)
{

	
	if(MblnIsShown)
	{
		return
	}
	else
	{
		MblnIsShown=true
	}
	if(document.getElementById)
	{
		if(!MSelectorRef)
		{
			MdtToday=new Date();
			MdtToday.setHours(0,0,0,0);
			MWriteSelectorHTML();
			MSelectorRef=document.getElementById("MdateSelector");
			MCalendarArea=document.getElementById("McalendarArea");
		}
		if(!CalendarIframe)
		{
			CalendarIframe = document.getElementById("MCalendarIframe");
		}
		MDateRef=DateRef;
		MblnDisableNone=DisableNone;
		MLangID=LangID;
		MImagePath=ImagePath;
		MDoPostBack=NeedPBack;
		switch(MLangID)
		{
			case"FR":
				MNoneText='';
				break;
			case"CN":
				MNoneText='';
				break;
			case"US":
				MNoneText='';
				break;
			case"RU":
				MNoneText='';
				break;	
			default:
				MNoneText='';
		}
		//设置可选置的日期范围，有需要时可以启用下面被注销的两行
		//MdtSeasonStart=new Date(MdtToday.getTime()-(MintOneDay*184));
		//MdtSeasonEnd=new Date(MdtToday.getTime()+(MintOneDay*182));
		MSltDay=0;
		if(MDateRef.value==MNoneText)
		{
			if(MDateRef.defaultValue==MNoneText)
			{
				var arrCrtDate=MDateToString(MdtToday).split(MSplictChar);
				MSltMonth=arrCrtDate[1]-1;
			}
			else
			{
				var arrCrtDate=MDateRef.defaultValue.split(MSplictChar);
				MSltMonth=arrCrtDate[1]-1;
				MSltDay=arrCrtDate[2];
			}
		}
		else
		{
			var arrCrtDate=MDateRef.value.split(MSplictChar);
			MSltMonth=arrCrtDate[1]-1;
			MSltDay=arrCrtDate[2];
		}
		MSltYear=arrCrtDate[0];
		MintCrtMonth=parseInt(MSltMonth,10);
		MintCrtYear=parseInt(MSltYear,10);
		MCalendarArea.innerHTML=MCreateCalendarArea();
		if(MBrowser=="Gecko")
		{
		
			MSelectorRef.style.left=EventRef.clientX-90;
			MSelectorRef.style.top=EventRef.clientY+8;
		}
		else
		{
		        

			
			//MSelectorRef.style.left=EventRef.clientX-EventRef.offsetX-86+document.body.scrollLeft;
			//MSelectorRef.style.top=EventRef.clientY-EventRef.offsetY+16+document.body.scrollTop;						
			//MSelectorRef.style.left=event.clientX-190;
					//alert(TheEvent.screenY);
			//alert(screen.availHeight);
			//196为日历高度
			
			CalendarIframe.style.display = "block";
			CalendarIframe.style.zIndex   =   MSelectorRef.style.zIndex - 1;  
			MSelectorRef.style.left	=	EventRef.clientX-EventRef.offsetX-190+document.body.scrollLeft+7.5;
			CalendarIframe.style.left	=	MSelectorRef.style.left;
			CalendarIframe.style.width  = MSelectorRef.offsetWidth;
			CalendarIframe.style.height   =   MSelectorRef.offsetHeight;   
			
			if(EventRef.screenY+196<=screen.availHeight)
			{
				MSelectorRef.style.top	=	EventRef.clientY-EventRef.offsetY+19+document.body.scrollTop;
				CalendarIframe.style.top	=	MSelectorRef.style.top;
			}
			else
			{    
				MSelectorRef.style.top	=	EventRef.clientY-EventRef.offsetY+19+document.body.scrollTop-196;
				CalendarIframe.style.top	=	MSelectorRef.style.top;
			}
		}
		CalendarIframe.style.visibility = "visible";
		MSelectorRef.style.visibility="visible";
	}
}

function MHideDateSelector(TheEvent)
{
	if(MSelectorRef)
	{
		if(MBrowser=="Gecko")
		{
			if(TheEvent)
			{
				var ThisIcon="MdsIcon_"+MDateRef.name;
				var rel=TheEvent.target;
				while(rel)
				{
					if((rel.id=="MdateSelector")||(rel.id==ThisIcon))
					{
						break
					}
					else
					{
						rel=rel.parentNode
					}
				}
			}
			if(!rel)
			{
				MSelectorRef.style.visibility="hidden";
				MblnIsShown=false
			}
			return;
		}
		else
		{
			if((TheEvent))
			{
				
				if((TheEvent.clientX+document.body.scrollLeft>MSelectorRef.style.posLeft+1)
				&&(TheEvent.clientX+document.body.scrollLeft<MSelectorRef.style.posLeft+MSelectorRef.style.posWidth+7.5)				
				&&(TheEvent.clientY+document.body.scrollTop>MSelectorRef.style.posTop-19)
				&&(TheEvent.clientY+document.body.scrollTop<MSelectorRef.style.posTop+MSelectorRef.offsetHeight+2))
				{
					return
				}
				/* 暂时注释掉，不明原因
				if((TheEvent.clientX+document.body.scrollLeft>MSelectorRef.style.posLeft+81)
				&&(TheEvent.clientX+document.body.scrollLeft<MSelectorRef.style.posLeft+99)
				&&(TheEvent.clientY+document.body.scrollTop>MSelectorRef.style.posTop-17)
				&&(TheEvent.clientY+document.body.scrollTop<MSelectorRef.style.posTop))
				{
					return
				}
				*/
			}			
			MSelectorRef.style.visibility="hidden";
			CalendarIframe.style.visibility="hidden";
			//alert(CalendarIframe.style.visibility);
			MblnIsShown=false;
			
		}
	}
	else
	{
		MSelectorRef=false
	}
	
}

function MCreateCalendarArea()
{
	switch(MLangID)
	{
		case"FR":
			var MDefWeek=new Array("Lun","Mar","Mer","Jeu","Ven","Sam","Dim");
			var MDefMonth=new Array("\112\141\156\166\151\145\162","\106\046\145\141\143\165\164\145\073\166\162\151\145\162","\115\141\162\143\150\145","\101\166\162\151\154","\115\141\151","\112\165\151\156","\112\165\151\154\154\145\164","\101\157\046\165\143\151\162\143\073\164","\123\145\160\164\145\155\142\162\145","\117\143\164\157\142\162\145","\116\157\166\145\155\142\162\145","\104\046\145\141\143\165\164\145\073\143\145\155\142\162\145");
			var MStrToday="Today";
			var MNoneValue="";
			break;
		case"CN":
			var MDefWeek=new Array("一","二","三","四","五","六","日");
			var MDefMonth=new Array("一月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","十二月");
			var MStrToday="Today";
			var MNoneValue="";
			break;
		case"RU":
			var MDefWeek=new Array("Пон","Вто","Сре","Чет","Пят","Суб","Вос");
			var MDefMonth=new Array("Январь","Февраль","Март","Апрель","Май","Июнь","Июль","Август","Сентябрь","Октябрь","Ноябрь","Декабрь");
			var MStrToday="Today";
			var MNoneValue="";
			break;
		default:
			var MDefWeek=new Array("Mon","Tue","Wed","Thu","Fir","Sat","Sun");
			var MDefMonth=new Array("January","February","March","April","May","June","July","August","September","October","November","December");
			var MStrToday="Today";
			var MNoneValue="";
	}
	var dtFirstOfMonth=new Date(MintCrtYear,MintCrtMonth,1);
	switch(dtFirstOfMonth.getDay())
	{
		case 0:
			var OffsetDays=6;
			break;
		case 1:
			var OffsetDays=7;
			break;
		default:
			var OffsetDays=dtFirstOfMonth.getDay()-1;
	}
	var dtCalendarStart=new Date(dtFirstOfMonth.getTime()-(MintOneDay*OffsetDays));
	var dtCalendarEnd=new Date(dtCalendarStart.getTime()+(MintOneDay*41));

	MHtmlCode="<TABLE BORDER='0' CELLPADDING='2' CELLSPACING='0' CLASS='Mcalendar'>";
	MHtmlCode+="<TR CLASS='McalendarTitles' ALIGN='center'>";

	for(var i=0;i<=6;i++)
	{
		MHtmlCode+="<TD WIDTH='22'>"+MDefWeek[i]+"</TD>"
	}
	MHtmlCode+="</TR>";
	for(var i=0;i<=41;i++)
	{
		if(i%7==0)
		{
			MHtmlCode+="<TR ALIGN='center'>"
		}
		var StyleString="";
		var dtTheDay=new Date(dtCalendarStart.getTime()+(MintOneDay*i));
		if(dtTheDay.getTime()==MdtToday.getTime())
		{
			if((dtTheDay.getMonth()==MSltMonth)&&(dtTheDay.getDate()==MSltDay)&&(dtTheDay.getFullYear()==MSltYear))
			{
				StyleString+="background-image: url("+MImagePath+"today_selected.gif); background-repeat:no-repeat;"
			}
			else
			{
				StyleString+="background-image: url("+MImagePath+"today.gif); background-repeat:no-repeat;"
			}
		}
		else if((dtTheDay.getMonth()==MSltMonth)&&(dtTheDay.getDate()==MSltDay)&&(dtTheDay.getFullYear()==MSltYear))
		{
			StyleString+="background-image: url("+MImagePath+"selected.gif); background-repeat:no-repeat;"
		}
		if(dtTheDay.getMonth()!=MintCrtMonth)
		{
			var LinkClass="MnotInMonth"
		}
		else
		{
			var LinkClass="MinMonth"
		}
		if(((MdtSeasonStart)&&(MdtSeasonEnd))&&((dtTheDay<MdtSeasonStart)||(dtTheDay>MdtSeasonEnd)))
		{
			MHtmlCode+="<TD CLASS='MoutOfRange' STYLE='"+StyleString+"'>"+dtTheDay.getDate()+"</TD>"
		}
		else
		{
			//alert(LinkClass);
			MHtmlCode+="<TD STYLE='"+StyleString+"'><A HREF='Javascript: void MSetDate(\""+MDateToString(dtTheDay)+"\")' CLASS='"+LinkClass+"'>"+dtTheDay.getDate()+"</A></TD>"
			//alert("<TD STYLE='"+StyleString+"'><A HREF='Javascript: void MSetDate(\""+MDateToString(dtTheDay)+"\")' CLASS='"+LinkClass+"'>"+dtTheDay.getDate()+"</A></TD>");
		}
		if(i%7==6)
		{
			MHtmlCode+="</TR>"
		}
	}

	MHtmlCode+="</TABLE>";

	var MSelectOption="";
	var Selected="";
	for(var i=0;i<=11;i++)
	{
		if(i==MintCrtMonth)
		{
			Selected=" SELECTED"
		}
		else
		{
			Selected=""
		}
		MSelectOption+='<OPTION'+Selected+'>'+MDefMonth[i]+'</OPTION>';
	}
	MHtmlCode='<TABLE BORDER="0" CELLPADDING="1" CELLSPACING="0" WIDTH="100%" style="z-index:500" HEIGHT="100%">'+'<TR HEIGHT="22" CLASS="MbuttonsRow">'+'<TD WIDTH="30" ALIGN="left"><INPUT TYPE="image" SRC="'+MImagePath+'but_prev.gif" WIDTH="18" HEIGHT="18" HSPACE="1" onClick="MAdvanceDate(-1)"></TD>'+'<TD><SELECT NAME="McurrentMonth" CLASS="MmonthRolldown" onChange="MChangeMonth(this.selectedIndex + 1)">'+MSelectOption+'</SELECT></TD>'+'<TD><INPUT TYPE="text" NAME="McurrentYear" VALUE="'+MintCrtYear+'" READONLY CLASS="MyearInput"></TD>'+'<TD><INPUT TYPE="image" SRC="'+MImagePath+'but_yeard.gif" WIDTH="18" HEIGHT="9" onClick="MAdvanceDate(12)"><BR><INPUT TYPE="image" SRC="'+MImagePath+'but_yearu.gif" WIDTH="18" HEIGHT="9" onClick="MAdvanceDate(-12)"></TD>'+'<TD WIDTH="30" ALIGN="right"><INPUT TYPE="image" SRC="'+MImagePath+'but_next.gif" WIDTH="18" HEIGHT="18" HSPACE="1" onClick="MAdvanceDate(1)"></TD>'+'</TR>'+'<TR HEIGHT="133" BGCOLOR="#FFFFFF"><TD COLSPAN="5" ALIGN="center">'+MHtmlCode+'</TD></TR>'+'<TR HEIGHT="22" CLASS="MbuttonsRow">'+'<TD COLSPAN="5">'+'<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0" WIDTH="100%">'+'<TR>'+'<TD ALIGN="left"><INPUT TYPE="image" SRC="'+MImagePath+MLangID+'/but_today.gif" WIDTH="44" HEIGHT="18" HSPACE="1" onClick="MSetDate(MDateToString(MdtToday))"></TD>'+'<TD ALIGN="center" ID="MdateToday"> '+MDateToString(MdtToday)+'</TD>'+'<TD ALIGN="right"><INPUT TYPE="image" SRC="'+MImagePath+MLangID+'/but_none.gif" WIDTH="44" HEIGHT="18" HSPACE="1" onClick="MSetDate(\''+MNoneValue+'\')"></TD>'+'</TR>'+'</TABLE>'+'</TD>'+'</TR>';
	return MHtmlCode;
}


function MSetDate(TheDate)
{
	if((TheDate==MNoneText)&&(MblnDisableNone==true))
	{
		switch(MLangID)
		{
			case"FR":
				alert("Cette d鈚e ne peut pas 阾re 'Aucune'");
				break;
			case"CN":
				alert("日期不能设置为空");
				break;
			case"RU":
				alert("Дата неразрешена пустой!");
				break;	
			default:
				alert("This date cannot be set to 'None'");
		}
		return false;
	}
	var tempArray=TheDate.split(MSplictChar);
	var resultingDate=new Date(tempArray[0],tempArray[1]-1,tempArray[2]);
	if(((MdtSeasonStart)&&(MdtSeasonEnd))&&((resultingDate<MdtSeasonStart)||(resultingDate>MdtSeasonEnd)))
	{
		switch(MLangID)
		{
			case"FR":
				alert("Veuillez choisir une d鈚e dans la gamme specifi閑");
				break;
			case"CN":
				alert("请选择设定范围内的日期");
				break;
			case"RU":
				alert("Выберите дату из интервала определения!");
				break;	
			default:
				alert("Please select a date in the range specified");
		}
	return false;
	}
	MSltDay=0;
	MDateRef.value=TheDate;
	MHideDateSelector();
	if(MDoPostBack)
	{
		document.forms[0].submit();
	}
}

function MAdvanceDate(Adjuster)
{
	if((Adjuster==12)||(Adjuster==-12))
	{
		MintCrtYear=MintCrtYear+(Adjuster/12)
	}
	else
	{
		MintCrtMonth=MintCrtMonth+Adjuster;
		if(MintCrtMonth==-1)
		{
			MintCrtMonth=11;
			MintCrtYear--
		}
		if(MintCrtMonth==12)
		{
			MintCrtMonth=0;
			MintCrtYear++
		}
	}
	MCalendarArea.innerHTML=MCreateCalendarArea();
}

function MChangeMonth(Adjuster)
{
	MintCrtMonth=Adjuster-1;
	MCalendarArea.innerHTML=MCreateCalendarArea();
}

function MDateToString(TheDate)
{
	if(!TheDate)
	{
		return""
	}
	else
	{
		mYear	= TheDate.getFullYear();
		mMonth	= TheDate.getMonth()<9?"0"+(TheDate.getMonth()+1):(TheDate.getMonth()+1);
		mDate	= TheDate.getDate()<10?"0"+TheDate.getDate():TheDate.getDate();
		return(mYear+MSplictChar+mMonth+MSplictChar+mDate);
	}
}


function MMakeDate(TheDay,TheMonth,TheYear)
{
	return new Date(TheYear,TheMonth-1,TheDay)
}


function MCheckDate(thisDateField,LangID)
{
	if(!LangID)
	{
		LangID=MLangID
	}
	switch(LangID)
	{
		case"FR":
			MNoneText='';
			var FailText="Cette date n'est pas valable";
			break;
		case"CN":
			MNoneText='';
			var FailText="无效的日期";
			break;
		case"EN":
			MNoneText='';
			var FailText="Date is not valid";
			break;
		case"RU":
			MNoneText='';
			var FailText="Бездейственная дата!";
			break;	
		default:
			MNoneText='';
			var FailText="Date is not valid";
	}
	if(thisDateField.value=="")
	{
		thisDateField.value=MNoneText
	}
	if((thisDateField.value!=MNoneText)&&(!MCheckDateFormat(thisDateField.value)))
	{
		alert(FailText);
		thisDateField.value=thisDateField.defaultValue
	}
}

function MCheckDateFormat(thisDate)
{
	if(thisDate.indexOf(MSplictChar)==-1)
	{
		return false
	}
	var ArrayDate=thisDate.split(MSplictChar);
	if(ArrayDate.length!=3)
	{
		return false
	}
	if((isNaN(ArrayDate[0]))||(ArrayDate[0]==""))
	{
		return false
	}
	if((isNaN(ArrayDate[1]))||(ArrayDate[1]==""))
	{
		return false
	}
	if((isNaN(ArrayDate[2]))||(ArrayDate[2]==""))
	{
		return false
	}
	var daysInMonth=new Array(0,31,29,31,30,31,30,31,31,30,31,30,31);
	if((parseInt(ArrayDate[2],10)<1)||(parseInt(ArrayDate[2],10)>daysInMonth[parseInt(ArrayDate[1],10)]))
	{
		return false
	}
	if((parseInt(ArrayDate[1],10)==2)&&(parseInt(ArrayDate[2],10)>MDaysInFebruary(parseInt(ArrayDate[2],10))))
	{
		return false
	}
	if((parseInt(ArrayDate[1],10)<1)||(parseInt(ArrayDate[1],10)>12))
	{
		return false
	}
	return true;
}


function MDaysInFebruary(year)
{
	return(((year%4==0)&&((!(year%100==0))||(year%400==0)))?29:28)
}

function MWriteSelectorHTML()
{
	var selectorHTML='<iframe id="MCalendarIframe" src="javascript:false;" scrolling="no"  frameborder="0" style="position:absolute;top:0px;left:0px;display:none;filter=progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0);"></iframe> '+'<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0"  WIDTH="188" HEIGHT="194" ID="MdateSelector" STYLE="Z-Index:1000;width:190px; height:196px">'+'<TR><TD ID="McalendarArea"></TD></TR>'+'</TABLE>';
	document.body.insertAdjacentHTML("BeforeEnd",selectorHTML)
}

function MWriteFieldHTML(FormName,FieldName,FieldValue,FieldWidth,ImagePath,LangID,DisableNone,UseOnClick)
{
	if(!LangID)
	{
		LangID="EN"
	}
	if(!DisableNone)
	{
		DisableNone=false
	}
	if(ImagePath.charAt(ImagePath.length-1)!=MSplictChar)
	{
		ImagePath=ImagePath+MSplictChar
	}
	if(document.getElementById)
	{
		var Mimg1=new Image();Mimg1.src=ImagePath+"today_selected.gif";
		var Mimg2=new Image();Mimg2.src=ImagePath+"today.gif";
		var Mimg3=new Image();Mimg3.src=ImagePath+"selected.gif";
		var Mimg4=new Image();Mimg4.src=ImagePath+"but_prev.gif";
		var Mimg5=new Image();Mimg5.src=ImagePath+"but_yearu.gif";
		var Mimg6=new Image();Mimg6.src=ImagePath+"but_yeard.gif";
		var Mimg7=new Image();Mimg7.src=ImagePath+"but_next.gif";
		var Mimg8=new Image();Mimg8.src=ImagePath+LangID+"/but_today.gif";
		var Mimg9=new Image();Mimg9.src=ImagePath+LangID+"/but_none.gif";
		var ActionString='HTMLShowDateSelector(document.'+FormName+'.'+FieldName+',event,'+DisableNone+',\''+LangID+'\',\''+ImagePath+'\')';
		if(UseOnClick==true)
		{
			var ActionEvent="onMouseDown="+ActionString;
			switch(LangID)
			{
				case"FR":
					var IconAltText="Cliquez ici pour choisir une date";
					break;
				case"CN":
					var IconAltText="单击这里选择日期";
					break;
				case"RU":
					var IconAltText="Сюда щёлкните, выбрать дату!";
					break;	
				default:
					var IconAltText="Click here to select a date";
			}
		}
		else
		{
			var ActionEvent="onMouseOver="+ActionString+" onMouseDown="+ActionString;
			var IconAltText="";
		}
		var formFieldHTML=''+'<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0" BGCOLOR="#FFFFFF" CLASS="MdateSelect" ID="'+FieldName+'Mtable" WIDTH="'+FieldWidth+'" HEIGHT="22" STYLE="width:'+FieldWidth+'px">'+'<TR>'+'<TD><INPUT TYPE="text" NAME="'+FieldName+'" VALUE="'+FieldValue+'" CLASS="MdateField" SIZE="9" MAXLENGTH="10" onChange="MCheckDate(this,\''+LangID+'\')" READONLY></TD>'+'<TD ALIGN="right"><A HREF="JavaScript: void 0" '+ActionEvent+'><IMG SRC="'+ImagePath+'calendar.gif" HEIGHT="16" WIDTH="16" HSPACE="3" BORDER="0" ALT="'+IconAltText+'" ID="MdsIcon_'+FieldName+'"></A></TD>'+'</TR>'+'</TABLE>';
		document.write(formFieldHTML);
	}
	else
	{
		var formFieldHTML='<INPUT TYPE="text" NAME="'+FieldName+'" VALUE="'+FieldValue+'" SIZE="10" MAXLENGTH="10" onChange="MCheckDate(this,\''+LangID+'\')" STYLE="width:'+FieldWidth+'px; height:22px">';
		document.write(formFieldHTML);
	}
}
