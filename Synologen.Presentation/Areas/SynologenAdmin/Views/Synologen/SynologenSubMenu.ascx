<%@ Control Language="C#" AutoEventWireup="true" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li>
			<a href="/Components/Synologen/Index.aspx">Medlemmar</a>
			&nbsp;>&nbsp;
			<a href="/Components/Synologen/EditMember.aspx">Ny</a>
		</li>
		<li>|</li>	
		<li>
			<a href="/Components/Synologen/Shops.aspx">Butiker</a>
			&nbsp;>&nbsp;
			<a href="/Components/Synologen/EditShop.aspx">Ny</a>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Butikgrupper","ShopGroups","Synologen") %>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","ShopGroupForm","Synologen") %>
		</li>
		<li>|</li>
		<li>
			<a href="/Components/Synologen/FileCategories.aspx">Filkategorier</a>
		</li>
		<li>|</li>
		<li>
			<a href="/Components/Synologen/Category.aspx">Medlemskategorier</a>
		</li>
	</ul>
</div>