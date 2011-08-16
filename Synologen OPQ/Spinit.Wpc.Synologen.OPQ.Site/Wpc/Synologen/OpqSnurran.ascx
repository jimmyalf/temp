<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqSnurran.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqSnurran" %>

<script type="text/javascript" src="/wpc/synologen/js/cloud-carousel.1.0.5.min.js"></script>
<script type="text/javascript">
$(document).ready(function(){
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
<div id = "carousel1" style="position:absolute; top:0; left:0; width:646px; height:250px;background:#fff;overflow:scroll;"> 
    <a href="<%=OpqSubPageUrl %>?nodeId=11"><img class = "cloudcarousel" src="/wpc/synologen/images/icon1.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=44"><img class = "cloudcarousel" src="/wpc/synologen/images/icon7.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=42"><img class = "cloudcarousel" src="/wpc/synologen/images/icon6.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=36"><img class = "cloudcarousel" src="/wpc/synologen/images/icon5.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=30"><img class = "cloudcarousel" src="/wpc/synologen/images/icon4.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=26"><img class = "cloudcarousel" src="/wpc/synologen/images/icon3.png" alt=" " title=" " /></a>
    <a href="<%=OpqSubPageUrl %>?nodeId=18"><img class = "cloudcarousel" src="/wpc/synologen/images/icon2.png" alt=" " title=" " /></a>
    
    <div  style="z-index:200; position:absolute; cursor:pointer; top:0; left:0; border:0; background:url('/wpc/synologen/images/spacer.png');width:200px;height:200px;"  id="left-but">&nbsp;</div>
	<div  style="z-index:200; position:absolute; cursor:pointer; top:0; right:0; border:0; background:url('/wpc/synologen/images/spacer.png')100% 0%;width:200px;height:200px;" id="right-but">&nbsp;</div>
</div>

