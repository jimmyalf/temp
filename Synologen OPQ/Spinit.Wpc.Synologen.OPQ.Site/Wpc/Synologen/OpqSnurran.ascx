<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqSnurran.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqSnurran" %>

<script type="text/javascript" src="/wpc/synologen/js/cloud-carousel.1.0.5.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#carousel1").CloudCarousel(
		{
		    xPos: 323,
		    yPos: 60,
		    buttonLeft: $("#left-but"),
		    buttonRight: $("#right-but"),
		    mouseWheel: true,
		    bringToFront: true
		}
	);
    });
 </script>
<div id="opq-carusel-container-subpage">
<div id = "carousel1"> 
    <a href="<%=OpqSubPageUrl %>?nodeId=11"><img class = "cloudcarousel" src="/wpc/synologen/images/icon1.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=44"><img class = "cloudcarousel" src="/wpc/synologen/images/icon7.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=42"><img class = "cloudcarousel" src="/wpc/synologen/images/icon6.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=36"><img class = "cloudcarousel" src="/wpc/synologen/images/icon5.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=30"><img class = "cloudcarousel" src="/wpc/synologen/images/icon4.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=26"><img class = "cloudcarousel" src="/wpc/synologen/images/icon3.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=18"><img class = "cloudcarousel" src="/wpc/synologen/images/icon2.png" alt=" " title=" " /></a>
    
    <div  id="left-but"></div>
	<div  id="right-but"></div>
</div>
</div>

