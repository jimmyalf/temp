<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<% //<WpcSynologen:YammerFeed ID="testYammerFeed" runat="server" NumberOfMessages="10" ExcludeJoins="true" Threaded="true" NewerThan="111864279" />%>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
<script type="text/javascript" src="http://www.synologen.nu/wpc/synologen/js/jquery.fancybox-1.3.4/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
<script type="text/javascript" src="http://www.synologen.nu/wpc/synologen/js/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
<script type="text/javascript" src="http://www.synologen.nu/wpc/synologen/js/Synologen.js"></script>
<link rel="stylesheet" type="text/css" href="http://www.synologen.nu/wpc/synologen/js/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.css" media="screen" /><h3 class="yammerheader">GO WITH THE FLOW</h3>

<WpcSynologen:MiniYammerFeed ID="testYammerFeed" runat="server" NumberOfMessages="50" ExcludeJoins="true" Threaded="true" NewerThan="1" />

<h3 class="yammerheader"><a href="/TestPages/YammerFeed.aspx">Visa alla</a></h3>
</asp:Content>
