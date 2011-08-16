<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqMenu.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqMenu" %>
<script type="text/javascript">
    $(document).ready(function(){            
        $("ul.topnav li").click(function() {
            $(this).parent().find("ul.subnav").slideUp('slow');
            $(this).find("ul.subnav").slideDown('fast').show(); 
        });        
    });  
</script>    
<div id="sub-nav"><asp:Literal ID="ltMenu" runat="server" /></div>
