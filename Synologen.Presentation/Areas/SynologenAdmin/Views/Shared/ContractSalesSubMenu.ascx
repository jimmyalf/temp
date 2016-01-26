<%@ Control Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="dCompSubNavigation">
	<ul id="SubMenu">
		<li>
			<a href="/components/synologen/Orders.aspx">Fakturor</a>
		</li>
		<li>|</li>
		<li>
			<a href="/Components/Synologen/Articles.aspx">Artiklar</a>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddArticle","ContractSales") %>
		</li>
		<li>|</li>
		<li>
			<a href="/components/synologen/contracts.aspx">Avtal</a>
			&nbsp;>&nbsp;
			<a href="/components/synologen/EditContract.aspx">Nytt</a>
		</li>
		<li>|</li>
		<li>
			<a href="/components/synologen/FtpProfiles.aspx">Ftp-profiler</a>
			&nbsp;>&nbsp;
			<%=Html.ActionLink("Ny","AddFtpProfile","ContractSales") %>
		</li>
		<li>|</li>
		<li>
			<a href="/components/synologen/OrderStatus.aspx">Orderstatus</a>
		</li>
		<li>|</li>
		<li>
			<%=Html.ActionLink("Statistik", "Statistics", "ContractSales") %>
		</li>
	</ul>
</div>