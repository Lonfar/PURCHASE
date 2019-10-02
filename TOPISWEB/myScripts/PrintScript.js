function Print() 
{
		window.print();
}

function PrintPreview(errorstring) 
{
	try{
		document.body.insertAdjacentHTML( "beforeEnd", "<object id='idWBPrint' width=0 height=0 classid='clsid:8856F961-340A-11D0-A96B-00C04FD705A2'>   </object>");
		idWBPrint.ExecWB( 7, 1);
		idWBPrint.outerHTML = "";
	}
	catch(error){
		window.alert(errorstring);
	}
}

function PrintSetting(errorstring) {
	try{

		document.body.insertAdjacentHTML("beforeEnd", "<object id='idWBPrint' width=0 height=0 classid='clsid:8856F961-340A-11D0-A96B-00C04FD705A2'>   </object>");
		idWBPrint.ExecWB (8, 1)
		idWBPrint.outerHTML = "";
	}		
	catch(error){
		window.alert(errorstring);
	}
}
