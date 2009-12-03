<%@ Page Title="" Language="C#" MasterPageFile="~/Components/Synologen/SynologenOpq.master"
	AutoEventWireup="true" CodeBehind="OpqShopRoutines.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.OpqShopRoutines" %>
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
							<asp:GridView ID="gvFiles" 
								runat="server" 
								OnRowCreated="gvFiles_RowCreated" 
								DataKeyNames="Id" 
								SkinID="Striped" 
								OnRowEditing="gvFiles_Editing" 
								OnRowDeleting="gvFiles_Deleting" 
								OnRowCommand="gvFiles_RowCommand">
								<Columns>
									<asp:TemplateField headertext="Namn"  ItemStyle-HorizontalAlign="Center" >
										<ItemTemplate>
											<asp:Literal ID="ltFile" runat="server" />
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField headertext="Datum"  ItemStyle-HorizontalAlign="Center" >
										<ItemTemplate>
											<asp:Literal ID="ltFileDate" runat="server" />
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
							</asp:GridView>          
						</div>
					</li>
				</ItemTemplate>
				<FooterTemplate>
					</ol></FooterTemplate>
			</asp:Repeater>
		</div>
	</div>
</asp:Content>
