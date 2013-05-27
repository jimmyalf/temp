<%@ Page Title="" Language="C#" MasterPageFile="~/Components/Synologen/SynologenOpq.master" AutoEventWireup="true" CodeBehind="OpqShopRoutines.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.OpqShopRoutines" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phOpqMainContent" runat="server">
	<div class="Synologen-OpqShopRoutines-aspx">
		<div class="wrap">
			<asp:Repeater ID="rptShops" runat="server" OnItemDataBound="rptShops_ItemDataBound">
				<HeaderTemplate>
					<ol id="shop-routines">
				</HeaderTemplate>
				<ItemTemplate>
					<li>
						<div class="edit-shop-link"><asp:HyperLink ID="hlShopLink" runat="server" Text="Redigera rutin &raquo;"/></div>
						<h2><asp:Literal ID="ltShopName" runat="server" /></h2>
						<div class="routine">
							<asp:Literal ID="ltRoutine" runat="server" />
						</div>
						<div id="documents">
                            <opq:OpqFileGridView ID="gvOwnFiles" runat="server" HeaderText="Egna dokument" Visible="false" />
                            <opq:OpqFileGridView ID="gvFilledFiles" runat="server" HeaderText="Ifyllda dokument" Visible="false" />
						</div>
					</li>
				</ItemTemplate>
				<FooterTemplate>
					</ol></FooterTemplate>
			</asp:Repeater>
		</div>
	</div>
</asp:Content>
