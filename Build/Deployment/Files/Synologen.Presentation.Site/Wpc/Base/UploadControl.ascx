<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadControl.ascx.cs" Inherits="Spinit.Wpc.Upload.UploadControl" %>
<div>
        
    <div id="ImageUploader">
    <script type="text/javascript">
//<![CDATA[
//Create JavaScript object that will embed Image Uploader to the page.
var iu = new ImageUploaderWriter("ImageUploader1", 650, 400);

//For ActiveX control full path to CAB file (including file name) should be specified.
iu.activeXControlCodeBase = "/wpc/Base/Client/ImageUploader5.cab";
iu.activeXControlVersion = "5,0,40,0";

//For Java applet only path to directory with JAR files should be specified (without file name).
iu.javaAppletJarFileName = "ImageUploader5.jar";
iu.javaAppletCodeBase = "/wpc/Base/client/";
iu.javaAppletCached = true;
iu.javaAppletVersion = "5.0.40.0";

iu.showNonemptyResponse = "off";

//Configure appearance.
iu.addParam("PaneLayout", "ThreePanes");
iu.addParam("ShowDebugWindow", "true");
iu.addParam("AllowRotate", "false");
iu.addParam("BackgroundColor", "#eeeeee");
iu.addParam("FolderPaneBorderStyle", "FixedSingle");
iu.addParam("TreePaneBorderStyle", "FixedSingle");
iu.addParam("UploadPaneBorderStyle", "FixedSingle");
iu.addParam("ShowButtons", "false");

iu.addParam("ShowUploadListButtons", "true");
iu.addParam("ButtonRemoveFromUploadListText", "");
iu.addParam("ButtonRemoveAllFromUploadListText", "");

//Configure License Keys
iu.addParam("LicenseKey", "71050-4620A-00000-092A8-E12C6;72050-4620A-00000-0F10B-988AB");

//Hide standard upload pane.
iu.addParam("FolderPaneHeight", "-1");

//Configure thumbnail settings.
iu.addParam("UploadThumbnail1FitMode", "Fit");
iu.addParam("UploadThumbnail1Width", "120");
iu.addParam("UploadThumbnail1Height", "120");
iu.addParam("UploadThumbnail1JpegQuality", "60");

//Configure URL files are uploaded to.
iu.addParam("Action", "http://intranet.falkenbergskonstskola.se/wpc/Base/Upload.aspx");

//Configure URL where to redirect after upload.
iu.addParam("RedirectUrl", "")

//Add event handlers.
iu.addEventListener("UploadFileCountChange", "ImageUploader_UploadFileCountChange");
iu.fullPageLoadListenerName = "fullPageLoad";
iu.addEventListener("BeforeUpload", "ImageUploader_BeforeUpload");

sv_resources.addParams(iu);

//Tell Image Uploader writer object to generate all necessary HTML code to embed 
//Image Uploader to the page.
iu.writeHtml();
//]]>
	</script>
	</div>
	<div id="file-settings" class="clear-fix">
		<p><em>Observera att bilder automatiskt skalas proportionerligt</em></p>
		<ul id="UploadList" class="clear-fix"></ul>
	</div>
	<div>
	    <input id="UploadButton" type="button" value="&nbsp;&nbsp;Slutf&ouml;r uppladdning&nbsp;&nbsp;" disabled="disabled" onclick="UploadButton_click();" />
    </div>
</div>