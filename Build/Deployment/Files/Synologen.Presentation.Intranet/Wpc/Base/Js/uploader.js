var imageUploader1 = null;
var uniqueId = 0;
var prevUploadFileCount = 0;
var dragAndDropEnabled = true;


var allowDrag = false;

function fullPageLoad(){
	imageUploader1 = getImageUploader("ImageUploader1");

	var UploadList=document.getElementById("UploadList");
	
	while (UploadList.childNodes.length > 0){
		UploadList.removeChild(UploadList.childNodes[0]);
	}

	//Fix Opera applet z-order bug
	if (__browser.isOpera){
		UploadList.style.height = "auto";
		UploadList.style.overflow = "visible";
	}


	//Handle drag & drop.
	if (__browser.isIE || __browser.isSafari){
		var target = __browser.isIE ? UploadList : document.body;
		target.ondragenter = function(){
			var e=window.event;
			var data = e.dataTransfer;
			if (data.getData('Text')==null){
				this.ondragover();
				data.dropEffect="copy";
				allowDrag=true;
			}
			else{
				allowDrag=false;
			}
		}
		
		target.ondragover=function(){
			var e = window.event;
			e.returnValue = !allowDrag;
		}

		target.ondrop = function(){
			var e = window.event;
			this.ondragover();
			e.dataTransfer.dropEffect = "none";
			processDragDrop();
		}
	}
	else {
		window.captureEvents(Event.DRAGDROP);
		window.addEventListener("dragdrop", function(){
				processDragDrop();
			}, true);
	}

}

//To identify items in upload list, GUID are used. However it would work 
//too slow if we use GUIDs directly. To increase performance, we will use 
//hash table which will map the guid to the index in upload list. 

//This function builds and returns the hash table which will be used for
//fast item search.
function getGuidIndexHash(){
	var uploadFileCount = imageUploader1.getUploadFileCount();
	var guidIndexHash = new Object();
	for (var i = 1; i <= uploadFileCount; i++){
		guidIndexHash["" + imageUploader1.getUploadFileGuid(i)] = i;
	}
	return guidIndexHash;
}

function getExtension(value) {
  return value.substring(value.lastIndexOf('.') + 1,value.length);
}

// ss
function addUploadFileHtml(index){
	var guid = "" + imageUploader1.getUploadFileGuid(index);
	var fileName = "" + imageUploader1.getUploadFileName(index);
	var height = "" + imageUploader1.getUploadFileHeight(index);
	var width = "" + imageUploader1.getUploadFileWidth(index);	
    var fileformatstring = getExtension(fileName);
    
    

	var h = "";

	//Add thumbnail control and link it with Image Uploader by its name and GUID.
	var tn = new ThumbnailWriter("Thumbnail" + uniqueId, 75, 75);
	//Copy codebase and version settings from ImageUploaderWriter instance.
	tn.activeXControlCodeBase = iu.activeXControlCodeBase;
	tn.activeXControlVersion = iu.activeXControlVersion;
	tn.javaAppletCodeBase = iu.javaAppletCodeBase;
	tn.javaAppletCached = iu.javaAppletCached;
	tn.javaAppletVersion = iu.javaAppletVersion;

	tn.addParam("ParentControlName", "ImageUploader1");
	tn.addParam("Guid", guid);
	tn.addParam("FileName", fileName);
	h += tn.getHtml();

	h += "<a class=\"remove\" href=\"#\" onclick=\"return Remove_click('" + guid + "');\">Ta bort</a>"
	h += "<div class=\"properties\">";

    
    
    //Add Filname element.	
	h += "<p><input id=\"Title" + uniqueId + "\"  type=\"text\" value=\"" + fileName + "\" />";
    h += "<label for=\"Title" + uniqueId + "\">Filnamn</label></p>";
    
    //Add Filtype element.
	h += "<p><input id=\"FileType" + uniqueId + "\"  type=\"text\" value=\"" + fileformatstring + "\" disabled=\"disabled\" />";
    h += "<label for=\"FileType" + uniqueId + "\">Filtyp</label></p>";
    
    //Add Width element.
	h += "<p><input id=\"Width" + uniqueId + "\"  type=\"text\" value=\"" + width + "\" onKeyUp=\"AdjustHeight("+ uniqueId + ", '" + guid + "');\" ";
	if(fileformatstring != "jpg" && fileformatstring != "png" && fileformatstring != "gif")
	{
	    h += "disabled=\"disabled\"  />";
	}
    h += "<label for=\"Width" + uniqueId + "\">Bredd (px)</label></p>";
    
	
	//Add Height element.
	h += "<p><input id=\"Height" + uniqueId + "\"  type=\"text\" value=\"" + height + "\"  onKeyUp=\"AdjustWidth("+ uniqueId + ", '" + guid + "');\" ";
    if(fileformatstring != "jpg" && fileformatstring != "png" && fileformatstring != "gif")
    {
        h += "disabled=\"disabled\"  />";
    }
    h += "<label for=\"Height" + uniqueId + "\">H&ouml;jd (px)</label></p>";
	
	
	h += "</div>";
	
	
//	var h = "<table cellspacing=\"5\"><tbody>";
//	h += "<tr>";
//	h += "<td class=\"Thumbnail\" align=\"center\" valign=\"middle\">";

//	//Add thumbnail control and link it with Image Uploader by its name and GUID.
//	var tn = new ThumbnailWriter("Thumbnail" + uniqueId, 96, 96);
//	//Copy codebase and version settings from ImageUploaderWriter instance.
//	tn.activeXControlCodeBase = iu.activeXControlCodeBase;
//	tn.activeXControlVersion = iu.activeXControlVersion;
//	tn.javaAppletCodeBase = iu.javaAppletCodeBase;
//	tn.javaAppletCached = iu.javaAppletCached;
//	tn.javaAppletVersion = iu.javaAppletVersion;

//	tn.addParam("ParentControlName", "ImageUploader1");
//	tn.addParam("Guid", guid);
//	tn.addParam("FileName", fileName);
//	h += tn.getHtml();

//	h += "</td>";
//	h += "<td valign=\"top\">";

//    //Add Title element.
//	h += "Width (px):<br />";
//	h += "<input id=\"Width" + uniqueId + "\" class=\"Width\" type=\"text\" onKeyUp=\"AdjustHeight("+ uniqueId + ")\" value=\"" + width + "\" /><br />";
//	
//	//Add Title element.
//	h += "Height (px):<br />";
//	h += "<input id=\"Height" + uniqueId + "\" class=\"Height\" type=\"text\" onKeyUp=\"AdjustWidth("+ uniqueId + ")\" value=\"" + height + "\" /><br />";
//	
//	//Add Title element.
//	h += "Title:<br />";
//	h += "<input id=\"Title" + uniqueId + "\" class=\"Title\" type=\"text\" value=\"" + fileName + "\" /><br />";

//	
//	h += "</td>";
//	h += "</tr>";
//	h += "<tr>";
//	h += "<td align=\"center\"><a href=\"#\" onclick=\"return Remove_click('" + guid + "');\">Remove</a></td>";
//	h += "<td></td>";
//	h += "</tr>";
//	h += "</tbody></table>";

	//Create DIV element which will represent the upload list item.
	var li = document.createElement("li");
	li.className = "UploadFile";
	li.innerHTML = h;
	li._guid = guid;
	//_uniqueId is used for fast access to the Title and Description form elements.
	li._uniqueId = uniqueId;

	//Append this upload list item to the custom upload pane.
	document.getElementById("UploadList").appendChild(li);

	//Increase the ID to guaranty uniqueness.
	uniqueId++;
}

//Synchronize custom upload pane with Image Uploader upload list when 
//some files are added or removed.
function ImageUploader_UploadFileCountChange(){
	if (imageUploader1){
		var uploadFileCount  = imageUploader1.getUploadFileCount();

		//Files are being added.
		if (prevUploadFileCount <= uploadFileCount){
			for (var i = prevUploadFileCount + 1; i <= uploadFileCount; i++){
				addUploadFileHtml(i);
			}
		}
		//Files are being removed.
		else{
			var guidIndexHash = getGuidIndexHash();
			var UploadList = document.getElementById("UploadList");
			var i = UploadList.childNodes.length - 1;
			while (i >= 0){
				if (guidIndexHash["" + UploadList.childNodes[i]._guid] == undefined){
					UploadList.removeChild(UploadList.childNodes[i]);
				}
				i--;
			}
		}

		prevUploadFileCount = uploadFileCount;

		document.getElementById("UploadButton").disabled = (uploadFileCount == 0);
	}
}

//This function is used to handle Remove link click. It removes an item 
//from the custom upload pane by specified GUID.
function Remove_click(guid){
	var guidIndexHash = getGuidIndexHash();
	imageUploader1.UploadFileRemove(guidIndexHash[guid]);
}

//This function posts data on server.
function UploadButton_click(){
	imageUploader1.Send();
}

//Adjust height when width is changed to keep ratio 
function AdjustHeight(id,guid){
    var guidIndexHash = getGuidIndexHash();
    var newWidth = document.getElementById("Width" + id).value;   
    
    var index = guidIndexHash[guid];
     
    var oldHeight = imageUploader1.getUploadFileHeight(index);
	var oldWidth = imageUploader1.getUploadFileWidth(index);
	
    document.getElementById("Height" + id).value = (newWidth / oldWidth) * oldHeight;
}

//Adjust width when hight is changed to keep ratio
function AdjustWidth(id,guid){
     var guidIndexHash = getGuidIndexHash();
     var newHeight = document.getElementById("Height" + id).value;
     
     var index = guidIndexHash[guid];
     
     var oldHeight = imageUploader1.getUploadFileHeight(index);
	 var oldWidth = imageUploader1.getUploadFileWidth(index);
     document.getElementById("Width" + id).value = (newHeight / oldHeight) * oldWidth;
}

//This function posts data on server.
function UploadButton_click(){
	imageUploader1.Send();
}

//Append the additional data entered by the user (title and description)
//to the upload. If you add more fields, do not forget to modify this event 
//handler to call AddField for these fields.
function ImageUploader_BeforeUpload(){
	var guidIndexHash = getGuidIndexHash();

	var UploadList = document.getElementById("UploadList");

	for (var i = 0; i < UploadList.childNodes.length; i++){
		var li = UploadList.childNodes[i];

		var index = guidIndexHash[li._guid];

		//Description will be sent as a native Description POST field 
		//provided by Image Uploader.
//		imageUploader1.setUploadFileDescription(index,
//			document.getElementById("Description" + div._uniqueId).value);


        imageUploader1.setUploadThumbnailHeight(index, document.getElementById("Height" + li._uniqueId).value);
        imageUploader1.setUploadThumbnailWidth(index, document.getElementById("Width" + li._uniqueId).value);


		//Title will be sent as a custom Title_N POST field, where N is an 
		//index of the file.
		imageUploader1.AddField("Title_" + index, document.getElementById("Title" + li._uniqueId).value);
	}
}