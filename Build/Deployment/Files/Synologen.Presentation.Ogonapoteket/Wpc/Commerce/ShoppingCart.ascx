<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.Site.ShoppingCart" %>
<spinit:MessageManager ID="msgShoppingCart" runat="server" UniqueClientID="Commerce-MessageManager-ShoppingCart" CaptionElement="h4" NegativeCaptionText="Ett fel har inträffat!"/>
<div id="tablecolumn">
<h2>Kundvagn</h2>
<asp:Repeater ID="rptProducts" OnItemDataBound="rptProducts_ItemDataBound" runat="server">
<HeaderTemplate>    
<table class="tablewhite">
    <tbody>
    </tbody>
    <colgroup>
    <col width="55%" />
    <col width="20%" />
    <col width="10%" />
    <col width="15%" />
    </colgroup>
    <tbody>
        <tr class="tablehead">
            <th>Vara</th>
            <th>Pris i poäng</th>
            <th>Antal</th>
            <th>Ta bort</th>
        </tr>
</HeaderTemplate>
<ItemTemplate>
<tr>
		<td><%# DataBinder.Eval(Container.DataItem, "ProductName")%></td>
		<td><%# ((decimal) DataBinder.Eval(Container.DataItem, "Price")).ToString ("#,##00")%></td>
		<td><asp:TextBox ID="txtNoOfProducts" runat="server" Width="20px"></asp:TextBox></td>
		<td><asp:CheckBox ID="chkRemove" runat="server" /></td>
</tr>
</ItemTemplate>
<FooterTemplate>   
   <tr class="tablebottom">
        <th colspan="4">Varuvärde: <asp:Label ID="lblValue" runat="server" /></th>
        </tr>
        <tr class="tablebottom">
        <th colspan="4">
        <p class="buttons"><asp:ImageButton ID="btnEmpty" ToolTip="Töm din kundvagn" AlternateText="Töm kundvagn" ImageUrl="/CommonResources/Files/www.getcuster.se/graphics/button-empty.png" CssClass="imagebutton longbutton float-right" runat="server" OnClick="btnEmpty_Click" /> <asp:ImageButton ID="btnChange" ToolTip="Uppdatera din kundvagn" AlternateText="Ändra" ImageUrl="/CommonResources/Files/www.getcuster.se/graphics/button-update.png" CssClass="imagebutton longbutton float-right" runat="server" OnClick="btnChange_Click" /></p>
        </th>
     
    </tr>
 </tbody>
</table>
</FooterTemplate>
</asp:Repeater>
<asp:PlaceHolder ID="phShoppingCartEmpty" runat="server" Visible="false">
<div id="CartEmpty">Kundvagnen är tom</div>
</asp:PlaceHolder>
<div id="confirmorder">
<asp:PlaceHolder ID="phConfirmOrder" runat="server">
	<p class="buttons"><asp:ImageButton ID="btnSend" ToolTip="Beställ för att slutföra din order" AlternateText="Beställ" ImageUrl="/CommonResources/Files/www.getcuster.se/graphics/button-order.png" CssClass="imagebutton smallbutton float-right" runat="server" OnClick="btnSend_Click" /></p>
</asp:PlaceHolder>
</div>
</div>


