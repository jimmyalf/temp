<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.Test" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script type="text/JavaScript" src="js/cloud-carousel.1.0.5.min.js"></script>
<script>
    $(document).ready(function() {
        $("#carousel1").CloudCarousel(
		{
		    xPos: 323,
		    yPos: 50,
		    buttonLeft: $("#left-but"),
		    buttonRight: $("#right-but"),
		    bringToFront: true
		}
	);
    });
 
</script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
	<div id = "carousel1" style="position:absolute; top:0; left:0; width:646px; height:250;background:#fff;overflow:scroll;"> 
        <a href="http://www.frenil.se" target="_blank"><img class = "cloudcarousel" src="images/icon1.png" alt=" " title=" " /></a>
        <a href="http://www.frenil.se" target="_blank"><img class = "cloudcarousel" src="images/icon2.png" alt=" " title=" " /></a>
        <a href="http://www.frenil.se" target="_blank"><img class = "cloudcarousel" src="images/icon3.png" alt=" " title=" " /></a>
        <a href="http://www.frenil.se" target="_blank"><img class = "cloudcarousel" src="images/icon4.png" alt=" " title=" " /></a>
        <a href="http://www.frenil.se" target="_blank"><img class = "cloudcarousel" src="images/icon5.png" alt=" " title=" " /></a>
        <a href="http://www.frenil.se" target="_blank"><img class = "cloudcarousel" src="images/icon6.png" alt=" " title=" " /></a>
        <a href="http://www.frenil.se" target="_blank"><img class = "cloudcarousel" src="images/icon7.png" alt=" " title=" " /></a>
        
        <div  style="z-index:200; position:absolute; cursor:pointer; top:0; left:0; border:0; background:url('images/spacer.png');width:200px;height:200px;"  id="left-but">&nbsp;</div>
    	<div  style="z-index:200; position:absolute; cursor:pointer; top:0; right:0; border:0; background:url('images/spacer.png')100% 0%;width:200px;height:200px;" id="right-but">&nbsp;</div>
    </div>   
    </form>
</body>
</html>

 
