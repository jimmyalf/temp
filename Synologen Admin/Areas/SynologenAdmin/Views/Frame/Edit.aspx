<%@ Page MasterPageFile="~/Components/Synologen/SynologenMain.master" Inherits="System.Web.Mvc.ViewPage<FrameEditView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Frame-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.RenderPartial("FrameForm", Model); %>
		</div>
	</div>
</div>	
</asp:Content>
