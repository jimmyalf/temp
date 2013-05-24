/**********************************************************************************   
FoldoutMenu2 
*   Copyright (C) 2001 <a href="/dhtmlcentral/thomas_brattli.asp">Thomas Brattli</a>
*   This script was released at DHTMLCentral.com
*   Visit for more great scripts!
*   This may be used and changed freely as long as this msg is intact!
*   We will also appreciate any links you could give us.
*
*   Made by <a href="/dhtmlcentral/thomas_brattli.asp">Thomas Brattli</a> 
*********************************************************************************/

function lib_bwcheck(){ //Browsercheck (needed)
	this.ver=navigator.appVersion
	this.agent=navigator.userAgent
	this.dom=document.getElementById?1:0
	this.opera5=this.agent.indexOf("Opera 5")>-1
	this.ie5=(this.ver.indexOf("MSIE 5")>-1 && this.dom && !this.opera5)?1:0; 
	this.ie6=(this.ver.indexOf("MSIE 6")>-1 && this.dom && !this.opera5)?1:0;
	this.ie4=(document.all && !this.dom && !this.opera5)?1:0;
	this.ie=this.ie4||this.ie5||this.ie6
	this.mac=this.agent.indexOf("Mac")>-1
	this.ns6=(this.dom && parseInt(this.ver) >= 5) ?1:0; 
	this.ns4=(document.layers && !this.dom)?1:0;
	this.bw=(this.ie6 || this.ie5 || this.ie4 || this.ns4 || this.ns6 || this.opera5)
	return this
}
var bw=new lib_bwcheck()




if(document.layers){ //NS4 resize fix...
	scrX= innerWidth; scrY= innerHeight;
	onresize= function(){if(scrX!= innerWidth || scrY!= innerHeight){history.go(0)} }
}

if(navigator.userAgent.indexOf('Opera')>-1 && document.getElementById){ //Opera 5 resize fix.
	scrX= innerWidth; scrY= innerHeight;
	document.onmousemove= function(){
		if(scrX<innerWidth-10 || scrY<innerHeight-10 || scrX>innerWidth+10 || scrY>innerHeight+10){
			scrX = innerWidth;
			scrY = innerHeight;
			foldInit();
		}
	};
}
/************************************************************************************
This is the new foldoutmenu, with more levels!
This is in some ways a rather advanced menu so please read every
note in this script and in the comments in the page to understand how
it works.

NOTE: if you change the position of divFoldCont from absolute to relative, 
you can put the menu in a table. HOWEVER it will no longer work in netscape 4.
If you wish to support netscape 4, you have to use absolute positioning.
************************************************************************************/

//Do you want images (if not set to 0 and remove the images from the body)?
foldImg=1

//Here are the images for the top links.
foldinImg=new Image(); foldinImg.src="foldoutmenu2_arrow.gif"		//The image for the closed state.
foldoutImg=new Image(); foldoutImg.src="foldoutmenu2_arrow_open.gif"	//The image for the open state.
//Here are the images for the sub links.
foldsubinImg=new Image(); foldsubinImg.src="foldoutmenu2_arrow.gif"		//The image for the "in" state.
foldsuboutImg=new Image(); foldsuboutImg.src="foldoutmenu2_arrow_open.gif"	//The image for the "out" state.
//If you change the images above please remember to change the images in the actual page as well.

mainmenus=4 //How many main menus do you have?

//How many submenus do you have beneath each main level.
submenus=new Array(3,3,2,3) 
//In this example I have 3 submenus in the first top menu,
//3, in the second, 2 submenus in the third top menu and 3 in the forth.
//It's really important that these numbers are correct.

pxbetweenmain = 10 //How much space should there be between the main menus
pxbetweensub = 5 //How much space should there be between the sub menus

mainstayfolded = false //Should the main menus stay folded?
substayfolded = false  //Should the sub menus stay folded?  Play with these 2 variables.


/************************************************************************************
Making cross-browser objects
************************************************************************************/
function makeMenu(obj,nest){
	nest=(!nest) ? "":'document.'+nest+'.';
   	this.css= bw.dom? document.getElementById(obj).style:bw.ie4?document.all[obj].style:bw.ns4?eval(nest+"document.layers." +obj):0;
	this.elm= bw.dom?document.getElementById(obj):bw.ie4?document.all[obj]:bw.ns4?eval(nest+'document.'+obj):0;
	this.ref= bw.dom || bw.ie4? document:bw.ns4?eval(nest+"document.layers." +obj+".document"):0;
	this.x= bw.ns4?this.css.left:bw.opera5?this.css.pixelLeft:this.elm.offsetLeft;
	this.y= bw.ns4?this.css.top:bw.opera5?this.css.pixelTop:this.elm.offsetTop;
	this.height= bw.ns4?this.ref.height:bw.opera5?this.css.pixelHeight:this.elm.offsetHeight;
	this.hideIt= b_hideIt;
	this.showIt= b_showIt;
	this.moveIt= b_moveIt;
	this.status= 0;
	return this;
}

// A unit of measure that will be added when setting the position of a layer.
var px = bw.ns4||window.opera?"":"px";

function b_showIt(){this.css.visibility="visible"; this.status=1}
function b_hideIt(){this.css.visibility="hidden"; this.status=0}
function b_moveIt(x,y){this.x=x; this.y=y; /*this.css.left=this.x+px;*/ this.css.top=this.y+px;}
/************************************************************************************
Initiating the page and making the menu
************************************************************************************/
function foldInit(){
	//Fixing the browsercheck for opera... this can be removed if the browsercheck has been updated!!
	bw.opera5 = (navigator.userAgent.indexOf("Opera")>-1 && document.getElementById)?true:false
	if (bw.opera5) bw.ns6 = 0
	
	//Creating the foldoutmenu
	oFold = new Array();
	y = 0;
	for (var i=0; i<mainmenus; i++){
		oFold[i] = new makeMenu('divFold'+i,'divFoldCont');
		oFold[i].moveIt(0,y);
		oFold[i].size = oFold[i].height;
		y += oFold[i].height+pxbetweenmain;
		if (bw.ns4) oFold[i].css.clip.bottom = 500;
		oFold[i].sub = new Array();
		oFold[i].subs = top_subs;
		suby = oFold[i].height+pxbetweensub;
		for (var j=0; j<submenus[i]; j++){
			oFold[i].sub[j] = new makeMenu('divFoldSub'+i+'_'+j,'divFoldCont.document.divFold'+i);
			oFold[i].sub[j].sub = new makeMenu('divFoldSub'+i+'_'+j+'_0','divFoldCont.document.divFold'+i+'.document.divFoldSub'+i+'_'+j);
			oFold[i].sub[j].hideIt();
			oFold[i].sub[j].sub.hideIt();
			oFold[i].sub[j].moveIt(10,suby);
			suby += oFold[i].sub[j].height+pxbetweensub;
		}
	}
  	oFoldCont = new makeMenu('divFoldCont');
	if (bw.ns4) oFoldCont.css.clip.bottom = 800;
  	oFoldCont.showIt(); //Showing the menu when the menu have been "created"
}
/************************************************************************************
Object function to the top of the menus.
************************************************************************************/
function top_subs(show, num){
	for (var j=0; j<this.sub.length; j++){
		if (show){
			this.sub[j].showIt()
			if (foldImg) this.ref["imgFold"+num].src = foldoutImg.src
			if (substayfolded){if(this.sub[j].sub.status) this.sub[j].sub.showIt()
			}else{
				if (foldImg) this.sub[j].ref["imgFold"+num+"Sub"+j].src = foldsubinImg.src
				this.sub[j].sub.hideIt()
			}
		}else{
			this.sub[j].hideIt()
			if (foldImg) this.ref["imgFold"+num].src = foldinImg.src
			if (!substayfolded) this.sub[j].sub.hideIt()
			else if (substayfolded) this.sub[j].sub.css.visibility="hidden"
		}
	}
}
/************************************************************************************
Checking height of the menus, and moves them to the correct position.
************************************************************************************/
function checkheight(org){
	if(mainstayfolded) istart=org; else istart=0
	for(i=istart;i<oFold.length;i++){
		oFold[i].size=oFold[i].height
		for(j=0;j<oFold[i].sub.length;j++){
			oFold[i].sub[j].size=oFold[i].sub[j].height+pxbetweensub
			if(oFold[i].sub[j].status){
				oFold[i].size+=oFold[i].sub[j].size
				if(oFold[i].sub[j].sub.status){
					oFold[i].size+=oFold[i].sub[j].sub.height+pxbetweensub
					oFold[i].sub[j].size+=oFold[i].sub[j].sub.height+pxbetweensub
				}
			}
			if(j!=0) oFold[i].sub[j].moveIt(10,oFold[i].sub[j-1].y+oFold[i].sub[j-1].size)
		}
		if(i!=0) oFold[i].moveIt(0,oFold[i-1].y+oFold[i-1].size+pxbetweenmain)
	}
}
/************************************************************************************
Fold in and out the submenus.
************************************************************************************/
function subfoldmenu(main,sub){
	if (!substayfolded){
		for (var i=0; i<oFold[main].sub.length; i++){
			if (i!=sub){
				oFold[main].sub[i].sub.hideIt();
				if (foldImg) oFold[main].sub[i].ref["imgFold"+main+"Sub"+i].src = foldsubinImg.src
			}
		}
	}
	if (!oFold[main].sub[sub].sub.status){
		oFold[main].sub[sub].sub.showIt()
		if(foldImg) oFold[main].sub[sub].ref["imgFold"+main+"Sub"+sub].src = foldsuboutImg.src
	}
	else{
		oFold[main].sub[sub].sub.hideIt()
		if(foldImg) oFold[main].sub[sub].ref["imgFold"+main+"Sub"+sub].src = foldsubinImg.src
	}
	checkheight(main)
}
/************************************************************************************
Fold in and out the mainmenus
************************************************************************************/
function foldmenu(num){
	if (!mainstayfolded){
		for(var i=0; i<oFold.length; i++){
			if(i!=num) oFold[i].subs(0,i)
		}
	}
	if (!oFold[num].sub[0].status) oFold[num].subs(1,num)
	else oFold[num].subs(0,num)
	checkheight(num)
}
//Initiating the menus onload, if the browser is ok.
if(bw.bw) onload = foldInit;