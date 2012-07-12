<%@ Page Language="C#" MasterPageFile="~/BaseMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.FileMain" MaintainScrollPositionOnPostback="True" Codebehind="FileMain.aspx.cs" %>
<asp:Content ID="MainComponent" ContentPlaceHolderID="ComponentContent" Runat="Server">

<script type='text/javascript' src='<%=Spinit.Wpc.Utility.Business.Globals.ResourceUrl %>CommonControls/Wysiwyg/Scripts/tiny_mce/plugins/filemanager/js/mcfilemanager.js'></script>
<script type="text/javascript">

    $(document).ready(function() {
        mcFileManager.browse({ fields: 'url', relative_urls: true, target_frame: 'filemanagerframe' });
    });

</script>

<iframe id="filemanagerframe" width="100%" height="100%"></iframe>

</asp:Content>
