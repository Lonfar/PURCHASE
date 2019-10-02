/*add by wxc 逐步为控件加入一些javascript脚本功能,尽量减少回传  // Copyright (c) Cnwit.  All rights reserved.*/

function ListCheck(chkb) 
{ 
    var tbl = FindParentElement(chkb, 'TABLE') 
    var checks = tbl.all.tags('INPUT'); 
    for ( var i=0 ; i < checks.length ; ++i ) 
    { 
        var check = checks[i]; 
        if ( check.type == 'checkbox' ) 
        { 
			if ( check.disabled == false )
			{
				check.checked = chkb.checked; 
				SetTRColor(check) 
            }
        } 
    } 
}

function ChildCheck(chkb) 
{ /*
	var checkall=document.getElementById("chkSel");
	alert(checkall);
	var checks=document.getElementsByName("chkCol");
	var ischecked=checkall.checked;
	for(var i=0;i<checks.length;i++)
	{
		checks[i].checked=ischecked;
	}
	
 */
    var tbl = FindParentElement(chkb, 'TABLE') 
    var checks = tbl.all.tags('INPUT'); 
    for ( var i=0 ; i < checks.length ; ++i ) 
    { 
        var check = checks[i]; 
        if ( check.type == 'checkbox' ) 
        { 
			
				check.checked = chkb.checked; 
            
        } 
    } 
   
}



function FindParentElement(element, tagName)
{
    while(element != null && element.tagName != tagName )
    {
        element = element.parentElement;
    }
    if ( element != null && element.tagName == tagName )
    {
        return element;
    }
    return null;
}


function FindChildElement(element, tagName)
{
    var isFounded = false;
    var elements = element;
    var result = element;
    if ( element.tagName == tagName )
    {
        return element;
    }
    while(!isFounded && elements != null && result != null && result.tagName != tagName)
    {
        elements = elements.childNodes;
        for( var i=0 ; elements != null && i < elements.length ; i++ )
        {
            result = elements.item(i);
            var result2 = FindChildElement(result, tagName);
            if ( result == null || result2 == null )
            {
                continue;
            }
            if ( result.tagName == tagName || result2.tagName == tagName )
            {
                if ( result2.tagName == tagName )
                {
                    result = result2;
                }
                isFounded = true;
                break;
            }
        }
    }
    if ( isFounded )
    {
        return result;
    }
    else
    {
        return null;
    }
}

function SetTDColor(TD) 
{ 

	var tbl = FindParentElement(TD, 'TABLE') 
    var checks = tbl.all.tags('INPUT'); 
    //先取消所有行的选择
    for ( var i=0 ; i < checks.length ; ++i ) 
    { 
        var check = checks[i]; 
        if ( check.type == 'checkbox' ) 
        { 
            check.checked = false; 
            SetTRColor(check) 
        } 
    }
	var trs = tbl.all.tags('TR'); 
	for(var i=0 ; i < trs.length ; ++i)
	{
		var TR = trs[i] ;
 		SetUnTRColor(TR)
	}
	TD.parentElement.style.backgroundColor='lightgoldenrodyellow'
	TD.parentElement.style.height = '23px';
	TD.parentElement.style.fontSize = '19pt'
}

function SetUnTRColor(TR)
{
		 var tbl = FindParentElement(TR, 'TABLE') 
		 var TRs = tbl.all.tags('TR'); 	  
			 for(i=0;i<TRs.length;i++)//循环行 
			{ 
				if(TR.style.backgroundColor=='lightgoldenrodyellow')//如果该单元格为开始时设置的颜色,即bColor 
				{ 	

					var j = i%2 ; 
					if(j==0) 
					{ 
						TR.style.cssText = 'TableRow';
						break; 
					} 
					else
					{ 
						TR.style.cssText = 'TableAlterRow';
						break; 
					} 
				} 
			} 
	
}


function SetUncheckColor(chkb)
{
	var tbl = FindParentElement(chkb, 'TABLE') 
    var checks = tbl.all.tags('INPUT'); 
    for ( var i=0 ; i < checks.length ; ++i ) 
    { 
        var check = checks[i]; 
        if ( check.type == 'checkbox' ) 
        { 
            if( check.checked == false)
            { 
			 SetTRColor(check) ;
            }
        } 
    } 

}

function SetTRColor(chk) 
{ 
	if(chk.checked ==true)
	{
		SetUncheckColor(chk) ; 
		chk.parentElement.parentElement.style.backgroundColor='lightgoldenrodyellow';
		chk.parentElement.parentElement.style.height = '23px';
		chk.parentElement.parentElement.style.fontSize = '19pt';
	}
	else
	{
		 var tbl = FindParentElement(chk, 'TABLE') 
		 var TRs = tbl.all.tags('TR'); 	  

		  for(i=0;i<TRs.length;i++)//循环行 
			{ 
				if(chk.parentElement.parentElement.style.backgroundColor=='lightgoldenrodyellow')//如果该单元格为开始时设置的颜色,即bColor 
				{ 	

					var j = i%2 ; 
					if(j==0) 
					{ 
						chk.parentElement.parentElement.style.cssText = 'TableRow';
						break; 
					} 
					else
					{ 
						chk.parentElement.parentElement.style.cssText = 'TableAlterRow';
						break; 
					} 
				} 
			} 
		
	}
} 

function SetOnFocusStyle(input)
{
	input.style.borderColor = '#E78C2D'
}

function SetOnBlurStyle(input)
{
	input.style.borderColor = 'DimGray'
}
function SetNumOnFocusStyle(input)
{
	input.style.borderColor = '#E78C2D'
	input.value = input.value.replace(/,/g,"");
}

function SetNumOnBlurStyle(input,declen)
{
	input.style.borderColor = 'DimGray'
	input.value = formatCurrency(input.value,declen) ;
}
/**
 * 将数值四舍五入(保留2位小数)后格式化成金额形式
 *
 * @param num 数值(Number或者String)
 * @return 金额格式的字符串,如'1,234,567.45'
 * @type String
 */

function formatCurrency(num,declen) {

    num = num.replace(/(^\s*)|(\s*$)/g,"");
    if(num != "" || num == "0")
    {
        return num;
    }
     if(declen==null||isNaN(declen)||declen<0) declen=2;   
     dec = Math.pow(10,declen);
     num = num.toString().replace(/\$|\,/g,'');
       if(isNaN(num))
       num = "0";
       //取绝对值，在最后看是否加上 "-" 号
       sign = (num == (num = Math.abs(num))); 

       if(num.toString().indexOf(".") >0)
       {
          cents = num.toString().replace(/[\x20-\x7f]*\./,""); //小数部分
          cents = cents.substring(0,declen);
       }
       else
       {
          cents =  Math.pow(10,declen).toString().substring(1);
       }
       num = num.toString().replace(/\.[\x20-\x7f]*/,"");
       
       for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
       num = num.substring(0,num.length-(4*i+3))+','+
       num.substring(num.length-(4*i+3));
       return (((sign)?'':'-') + num + '.' + cents);
       /*
       if ( num == "0")
       {
            return "";
       }
       else
       {
            //return (((sign)?'':'-') + num + '.' + cents);
            //return (((sign)?'':'-') + num + '.' + cents);

       }*/
}


function ReplaceChar(oldString)
{
    return oldString.replace(/([\'\"\&\\\n\r\t\b\f])/g,"\$1")
}

function ShowOrHideSpan(elmt)
{
    var srcElmt = event.srcElement;
    if ( srcElmt && srcElmt.tagName == 'TD' && srcElmt.name == 'title' )
    {
         var span = FindChildElement(elmt, 'SPAN');
         if ( span.style.display == 'none' )
         {
              span.style.display = 'inline';
         }
         else
         {
              span.style.display = 'none';
         }       
    }   
}


function PrintMessages(b,c)
{
	var sUrl="PrintShell.aspx?type=message&cpids="+b+"&isSafe="+c;
	var oWindow=window.open(sUrl,"","width=600px,height=400px,menubar=no,location=no,toolbar=no,resizable=yes,scrollbars=yes");
	CancelPrint();
}

function CheckAndPrintOneMsg()
{
	var items=document.getElementsByName("SelectedMessages");
	var found=false;
	var idx=0;
	for(var i=0;i<items.length;i++)
	{
		if(items[i].checked)
		{
			if(found)
			return false;
			found=true;
			idx=i;
		}
	}
	PrintMessages(items[idx].value+",-1",false);
	return true;
}
function CancelPrint()
{
	var infoPane=document.getElementById("error");
	if(infoPane)
	infoPane.style.display="none";
}
function selectone(e)
{
	var srcElement=e.srcElement;var checkall=document.getElementById("checkall");
if(checkall.checked==true&&srcElement.checked==false)
	checkall.checked=false;
else
{
	if(checkall.checked==false&&srcElement.checked==true)
	{
		var checks=document.getElementsByName("SelectedMessages");
		var allchecked=true;
		for(var i=0;i<checks.length;i++)
		{
			if(checks[i].checked==false)
			{
				allchecked=false;break;
			}
		}
		if(allchecked==true)
		checkall.checked=true;
		}
	}
}

function selectall()
{
	var checkall=document.getElementById("checkall");
	var checks=document.getElementsByName("SelectedMessages");
	var ischecked=checkall.checked;
	for(var i=0;i<checks.length;i++)
	{
		checks[i].checked=ischecked;
	}
}
