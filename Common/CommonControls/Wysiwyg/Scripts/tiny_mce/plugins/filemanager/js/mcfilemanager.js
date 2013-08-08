(function(){window.mcFileManager={settings:{document_base_url:"",relative_urls:false,remove_script_host:false,use_url_path:true,remember_last_path:"auto",target_elements:"",target_form:"",handle:"file"},setup:function(){var b=this,f,e=document,c=[];f=e.location.href;if(f.indexOf("?")!=-1){f=f.substring(0,f.indexOf("?"))}f=f.substring(0,f.lastIndexOf("/")+1);b.settings.default_base_url=unescape(f);function a(d){var g,h;for(g=0;g<d.length;g++){h=d[g];c.push(h);if(h.src&&/mcfilemanager\.js/g.test(h.src)){return h.src.substring(0,h.src.lastIndexOf("/"))}}}f=e.documentElement;if(f&&(f=a(f.getElementsByTagName("script")))){return b.baseURL=f}f=e.getElementsByTagName("script");if(f&&(f=a(f))){return b.baseURL=f}f=e.getElementsByTagName("head")[0];if(f&&(f=a(f.getElementsByTagName("script")))){return b.baseURL=f}},relaxDomain:function(){var a=this,b=/(http|https):\/\/([^\/:]+)\/?/.exec(a.baseURL);if(window.tinymce&&tinymce.relaxedDomain&&tinymce.relaxedDomain!=document.location.hostname){a.relaxedDomain=tinymce.relaxedDomain;return}if(b&&b[2]!=document.location.hostname){document.domain=a.relaxedDomain=b[2].replace(/.*\.(.+\..+)$/,"$1")}},init:function(a){this.extend(this.settings,a)},browse:function(b){var a=this;b=b||{};if(b.fields){b.oninsert=function(c){a.each(b.fields.replace(/\s+/g,"").split(/,/),function(d){var e;if(e=document.getElementById(d)){a._setVal(e,c.focusedFile.url)}})}}this.openWin({page:"index.html"},b)},edit:function(a){this.openWin({page:"edit.html",width:800,height:500},a)},upload:function(a){this.openWin({page:"upload.html",width:550,height:350},a)},createDoc:function(a){this.openWin({page:"createdoc.html",width:450,height:280},a)},createDir:function(a){this.openWin({page:"createdir.html",width:450,height:280},a)},createZip:function(a){this.openWin({page:"createzip.html",width:450,height:280},a)},openWin:function(h,c){var g=this,d,b;g.windowArgs=c=g.extend({},g.settings,c);h=g.extend({x:-1,y:-1,width:800,height:500,inline:1},h);if(h.x==-1){h.x=parseInt(screen.width/2)-(h.width/2)}if(h.y==-1){h.y=parseInt(screen.height/2)-(h.height/2)}if(h.page){h.url=g.baseURL+"/../default.aspx?type=fm&page="+h.page}if(c.session_id){h.url+="&sessionid="+c.session_id}if(c.custom_data){h.url+="&custom_data="+escape(c.custom_data)}if(g.relaxedDomain){h.url+="&domain="+escape(g.relaxedDomain)}if(c.custom_query){h.url+=c.custom_query}if(c.target_frame){if(d=frames[c.target_frame]){d.document.location=h.url}if(d=document.getElementById(c.target_frame)){d.src=h.url}return}if(window.tinymce&&tinyMCE.activeEditor){return tinyMCE.activeEditor.windowManager.open(h,c)}if(window.jQuery&&jQuery.WindowManager){return jQuery.WindowManager.open(h,c)}b=window.open(h.url,"mcFileManagerWin","left="+h.x+",top="+h.y+",width="+h.width+",height="+h.height+",scrollbars="+(h.scrollbars?"yes":"no")+",resizable="+(h.resizable?"yes":"no")+",statusbar="+(h.statusbar?"yes":"no"));try{b.focus()}catch(e){}},each:function(d,c,b){var e,a;if(d){b=b||d;if(d.length!==undefined){for(e=0,a=d.length;e<a;e++){c.call(b,d[e],e,d)}}else{for(e in d){if(d.hasOwnProperty(e)){c.call(b,d[e],e,d)}}}}},extend:function(){var d,b=arguments,f=b[0],e,c;for(e=1;e<b.length;e++){if(c=b[e]){for(d in c){f[d]=c[d]}}}return f},open:function(g,c,b,a,f){var d=this,e;f=f||{};if(!f.url&&document.forms[g]&&(e=document.forms[g].elements[c.split(",")[0]])){f.url=e.value}if(!a){f.oninsert=function(m){var l,j,h,k=m.focusedFile;h=c.replace(/\s+/g,"").split(",");for(j=0;j<h.length;j++){if(l=document.forms[g][h[j]]){d._setVal(l,k.url)}}}}else{if(typeof(a)=="string"){a=window[a]}f.oninsert=function(h){a(h.focusedFile.url,h)}}d.browse(f)},filebrowserCallBack:function(f,h,b,g,c){var j=mcFileManager,d,a,e,k={};if(window.mcImageManager&&!c){a=mcImageManager.settings.handle;a=a.split(",");for(d=0;d<a.length;d++){if(b==a[d]){e=1}}if(e&&mcImageManager.filebrowserCallBack(f,h,b,g,1)){return}}j.each(tinyMCE.activeEditor?tinyMCE.activeEditor.settings:tinyMCE.settings,function(l,i){if(i.indexOf("filemanager_")===0){k[i.substring(12)]=l}});j.browse(j.extend(k,{url:g.document.forms[0][f].value,relative_urls:0,oninsert:function(n){var m,l,i;m=g.document.forms[0];l=n.focusedFile.url;inf=n.focusedFile.custom;if(typeof(TinyMCE_convertURL)!="undefined"){l=TinyMCE_convertURL(l,null,true)}else{if(tinyMCE.convertURL){l=tinyMCE.convertURL(l,null,true)}else{l=tinyMCE.activeEditor.convertURL(l,null,true)}}if(inf.custom&&inf.custom.description){i=["alt","title","linktitle"];for(d=0;d<i.length;d++){if(m.elements[i[d]]){m.elements[i[d]].value=inf.custom.description}}}j._setVal(m[f],l);g=null}}));return true},_setVal:function(c,a){c.value=a;try{c.onchange()}catch(b){}}};mcFileManager.setup();mcFileManager.relaxDomain()})();