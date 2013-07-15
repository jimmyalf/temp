<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" CodeBehind="~/Testpages/TestButtonPage.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Testpages.TestButtonPage" %>
<%@ Register Src="~/Wpc/Synologen/ValidationButton.ascx" TagPrefix="WpcSynologen" TagName="ValidationButton" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">

	<style type="text/css">
		.user-validation-form 
		{
			background-color: white;
			position:relative; 
			padding:20px;
			border: solid 1px #98A399;
			width: 300px;
			margin: 20% auto;
		}
		.user-validation-form-container
		{
			width:100%;
			height:100%;
			position:absolute; 
			left:0px; 
			top:0px;
			background-image:url(http://www.ogonapoteket.se/CommonResources/Files/www.ogonapoteket.se/Images/Bilder/Mallelement/pop-up-box-bg.png);
			z-index:15;
		}
	</style>
	<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
	<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse vitae lorem ante, hendrerit iaculis massa. Phasellus non tellus nulla. Morbi hendrerit tortor sed massa elementum a interdum lorem ornare. Mauris enim ipsum, suscipit nec condimentum ut, egestas et diam. Vestibulum scelerisque dui gravida eros posuere porttitor. Curabitur nibh risus, pretium at accumsan nec, adipiscing nec lorem. Duis pulvinar, enim sit amet egestas ullamcorper, est tortor vulputate dolor, sed suscipit mauris justo vel sapien. Pellentesque porttitor egestas auctor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec eu massa a mi pellentesque euismod. Cras vestibulum nisi ac mauris pharetra ut tristique libero sodales. Ut a eros nisi. Nulla placerat lacus in urna sollicitudin dapibus. Proin imperdiet, nisl sed porttitor iaculis, ipsum odio tempor libero, sed sagittis est libero ut lacus. Quisque euismod, nibh nec tincidunt consequat, elit nibh tincidunt justo, posuere blandit ante odio ac tellus.</p>
	<p>Morbi cursus, ipsum sed ultricies egestas, quam nunc semper magna, vitae luctus diam leo id sapien. Nulla quam tellus, semper sit amet semper ac, ornare ac elit. Etiam nibh mi, adipiscing sed gravida sit amet, mattis quis enim. Nunc sed lacus at nibh auctor ornare ac at odio. Donec ut volutpat lacus. In hac habitasse platea dictumst. Praesent sem tellus, tincidunt id imperdiet sed, varius non nulla. Cras hendrerit, diam posuere volutpat pretium, leo urna varius sem, vel egestas mi purus eu tellus. Integer non ipsum dui, vitae semper felis. Suspendisse potenti. Vestibulum leo nulla, cursus quis consectetur eu, posuere non urna. Ut malesuada urna faucibus massa pharetra ut lacinia mauris pulvinar. Donec non diam ac metus pellentesque accumsan. Aliquam nec dolor ipsum, quis congue nulla. Proin consectetur vehicula nisl eget auctor. Suspendisse potenti. Sed erat arcu, mattis sit amet tristique eu, tincidunt ut eros. Mauris molestie sapien vitae lectus varius imperdiet lacinia nibh placerat. Morbi arcu sem, congue sit amet faucibus in, ultrices eu purus. Sed commodo mollis porta.</p>
	<p>Nulla convallis feugiat sem id scelerisque. Sed non nisi id nisi ornare dignissim ut eu eros. Vivamus est lectus, fermentum dictum posuere eu, ultricies non nunc. Fusce massa tortor, commodo eget imperdiet in, placerat in diam. Sed tellus eros, consequat vel dictum eget, pretium interdum tortor. Cras facilisis semper diam. Praesent malesuada adipiscing risus id dapibus. Proin consectetur consectetur ipsum, non faucibus odio auctor eget. Sed in dolor nec enim lacinia pharetra eget eu ligula. Phasellus suscipit pulvinar viverra. Vestibulum nec nunc mauris. Duis rhoncus laoreet euismod. Proin a blandit eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;</p>
	<p>Phasellus a nibh et nisl auctor vehicula. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed sit amet est vitae massa aliquam suscipit molestie ornare nisi. Nunc elementum, ipsum in pretium ultrices, libero elit ultrices lectus, sit amet posuere libero mauris vehicula nulla. Donec facilisis facilisis iaculis. Nullam tempor sagittis dolor. Pellentesque sed tempus ipsum. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Ut ut arcu est. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer et lectus nec ligula adipiscing tincidunt. Morbi laoreet lacinia pretium. Nulla mollis condimentum sem, quis luctus felis luctus id. Etiam felis nisl, congue nec convallis pellentesque, luctus a augue. Cras eget purus vel massa fringilla consectetur a eget odio.</p>
	<p>Curabitur volutpat, lectus quis semper molestie, lorem magna pharetra risus, et rutrum nulla nisi quis urna. Praesent interdum urna sit amet justo convallis ullamcorper euismod justo consectetur. Aenean ut eros nibh. Pellentesque ac ante mi, id vehicula velit. Vivamus elementum eros nec purus pulvinar et bibendum diam sollicitudin. Nunc et diam tempor augue dictum consequat eget in est. Suspendisse gravida nisl sed justo imperdiet tempus. Etiam faucibus felis sit amet lacus viverra pharetra. Proin vitae leo purus. Maecenas id felis enim, sed hendrerit nulla. Duis turpis mi, vehicula at euismod et, rhoncus rutrum metus. Duis iaculis est et odio facilisis lacinia. Duis nec odio ante, et varius mi. Morbi arcu ante, tincidunt in commodo nec, tempus non velit. Nunc a ipsum sapien. Fusce in mattis felis. Nunc pretium rhoncus interdum.</p>
	<WpcSynologen:ValidationButton 
		runat="server" 
		ButtonText="Spara" 
		ButtonSubmitText="Verifiera" 
		OnOnValidatedPassword="OnValidatedPassword_Event" 
		CloseButtonText="Avbryt" >
		<MessageTemplate>
			<p>Åtgärden ni försöker slutföra kräver förhöjd säkerhet. Var vänlig verifiera behörighet genom att ange ert lösenord nedan.</p>
		</MessageTemplate>
		<ErrorMessageTemplate>
			<p class="error">Lösenordsverifiering misslyckades</p>
		</ErrorMessageTemplate>
	</WpcSynologen:ValidationButton>
</asp:Content>