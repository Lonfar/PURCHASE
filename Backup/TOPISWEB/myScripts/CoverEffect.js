
var t_DiglogX,t_DiglogY,t_DiglogW,t_DiglogH;

function gid(id) {
  return document.getElementById?document.getElementById(id):null;
}

function gname(name) {
  return document.getElementsByTagName?document.getElementsByTagName(name):new Array()
}

function Browser() {
  var ua, s, i;
  this.isIE = false;
  this.isNS = false;
  this.isOP = false;
  this.isSF = false;
  ua = navigator.userAgent.toLowerCase();
  s = "opera";
  if ((i = ua.indexOf(s)) >= 0) {
    this.isOP = true;return;
  }
  s = "msie";
  if ((i = ua.indexOf(s)) >= 0) {
    this.isIE = true;return;
  }
  s = "netscape6/";
  if ((i = ua.indexOf(s)) >= 0) {
    this.isNS = true;return;
  }
  s = "gecko";
  if ((i = ua.indexOf(s)) >= 0) {
    this.isNS = true;return;
  }
  s = "safari";
  if ((i = ua.indexOf(s)) >= 0) {
    this.isSF = true;return;
  }
}

function DialogLoc() {
  var dde = document.documentElement;
  if (window.innerWidth) {
    var ww = window.innerWidth;
    var wh = window.innerHeight;
    var bgX = window.pageXOffset;
    var bgY = window.pageYOffset;
  } else {
    var ww = dde.offsetWidth;
    var wh = dde.offsetHeight;
    var bgX = dde.scrollLeft;
    var bgY = dde.scrollTop;
  }
  t_DiglogX = (bgX + ((ww - t_DiglogW)/2)) *2 - 60;
  t_DiglogY = 30 ;//(bgY + ((wh - t_DiglogH)/2));
}

function DialogShow(showdata,ow,oh,w,h) {
  var objDialog = document.getElementById("DialogMove");
  if (!objDialog) objDialog = document.createElement("div");
  t_DiglogW = ow;//ow
  t_DiglogH = oh;
  DialogLoc();
  objDialog.id = "DialogMove";
  var oS = objDialog.style;
  oS.display = "block";
  oS.top = t_DiglogY + "px";
  oS.left = t_DiglogX + "px";
  oS.margin = "0px";
  oS.padding = "0px";
  oS.width = w + "px";
  oS.height = h + "px";
  oS.position = "absolute";
  oS.zIndex = "5";
  oS.background = "#f2f2f2";//#FFcccc alert div's color
  
  oS.border = "solid #000 0px"; //"solid #000 1px";
  objDialog.innerHTML = showdata;
  document.body.appendChild(objDialog);
}

function DialogHide() {
  ScreenClean();
  var objDialog = document.getElementById("DialogMove");
  if (objDialog) objDialog.style.display = "none";
}

function ScreenConvert() {
  var browser = new Browser();
  var objScreen = gid("ScreenOver");
  if (!objScreen) var objScreen = document.createElement("div");
  var oS = objScreen.style;
  objScreen.id = "ScreenOver";
  oS.display = "block";
  oS.top = oS.left = oS.margin = oS.padding = "0px";
  if (document.body.clientHeight) {
    var wh = document.body.clientHeight + "px";
  } else if (window.innerHeight) {
    var wh = window.innerHeight + "px";
  } else {
    var wh = "100%";
  }
  oS.width = screen.width;//set
  oS.height = screen.height;//
  //alert(document.body.clientHeight);
  oS.position = "absolute";
  oS.zIndex = "3";
  //oS.innerText ="HELLOOLDSFSDFSDFF";
  if ((!browser.isSF) && (!browser.isOP)) {
    oS.background = "#0C0C0C";//#181818  Set here color!!
  } else {
    oS.background = "#FF0000";//#F0F0F0
  }
  oS.filter = "alpha(opacity=6)";//GB color is Set here
  oS.opacity = 40/100;
  oS.MozOpacity = 40/100;
  document.body.appendChild(objScreen);
}

function ScreenClean() {
  var objScreen = document.getElementById("ScreenOver");
  if (objScreen) objScreen.style.display = "none";
}

//举报
function Report()
{
        if(document.readyState!='complete')
        {
            setTimeout( function(){Report();},25 );
            return;
        }
        else
        {
           //var PostData = "do=" + Type + "&reportid=" + ID;
            //PostRequest(window.location.protocol + "//" + window.location.host +"/AJAX_Comm.aspx", PostData);
            var showData = sample.innerHTML;
            //ScreenConvert();DialogShow(" <div id=\"DialogLoading\">正在读取,请稍候... </div>",110,10,124,20);
            ScreenConvert();
            DialogShow(showData,1,1,1,1);
        }
}
