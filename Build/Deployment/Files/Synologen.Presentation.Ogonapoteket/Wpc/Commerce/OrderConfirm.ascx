<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderConfirm.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.Site.OrderConfirm" %>
<div id="tablecolumn" class="commercetable">
<h2>Orderbekräftelse</h2>
<h3>Dina uppgifter</h3>

<div class="float-right commercetable">
<h4>Stämmer dina uppgifter?</h4>
<p><a href="/Login/Dina-uppgifter/index.aspx">Ändra dina uppgifter här »</a></p>
</div>

<p>
	<asp:Label ID="lblName" AssociatedControlID="lblNameContent" runat="server" Text="Namn:" />
	<asp:Label ID="lblNameContent" runat="server" />
</p>
<p>
	<asp:Label ID="lblAddress" AssociatedControlID="lblAddressContent" runat="server" Text="Adress:" />
	<asp:Label ID="lblAddressContent" runat="server" />
</p>
<p>
	<asp:Label ID="lblDeliveryAddress" AssociatedControlID="lblDeliveryAddressContent" runat="server" Text="Leveransadress:" />
	<asp:Label ID="lblDeliveryAddressContent" runat="server" />
</p>
<p>
	<asp:Label ID="lblDeliveryType" AssociatedControlID="lblDeliveryTypeContent" runat="server" Text="Leveranstyp:" />
	<asp:Label ID="lblDeliveryTypeContent" runat="server" />
</p>
<h3>Din order</h3>
<p>
	<asp:Label ID="lblOrderId" AssociatedControlID="lblOrderIdContent" runat="server" Text="Ordernummer:" />
	<asp:Label ID="lblOrderIdContent" runat="server" />
</p>
<p>
	<asp:Label ID="lblPaymentType" AssociatedControlID="lblPaymentTypeContent" runat="server" Text="Betalningstyp:" />
	<asp:Label ID="lblPaymentTypeContent" runat="server" />
</p>
<p>
	<asp:Label ID="lblOrderStatus" AssociatedControlID="lblOrderStatusContent" runat="server" Text="Orderstatus:" />
	<asp:Label ID="lblOrderStatusContent" runat="server" />
</p>
<table>
    <tbody>
        <tr>
            <th colspan="4">Produkter</th>
        </tr>
<asp:Repeater ID="rptProducts" OnItemDataBound="rptProducts_ItemDataBound" runat="server">
<HeaderTemplate>    
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
            <th>Summa</th>
        </tr>
</HeaderTemplate>
<ItemTemplate>
<tr>
		<td><%# DataBinder.Eval(Container.DataItem, "ProductName")%></td>
		<td><%# ((decimal) DataBinder.Eval (Container.DataItem, "Price")).ToString ("#,##00")%></td>
		<td><%# DataBinder.Eval(Container.DataItem, "NoOfProducts")%></td>
		<td><%# ((decimal) DataBinder.Eval (Container.DataItem, "Sum")).ToString ("#,##00")%></td>
</tr>
</ItemTemplate>
<FooterTemplate>   
       <tr class="tablebottom">
            <th colspan="4">Varuv&auml;rde: <asp:Label ID="lblValue" runat="server" /></th>
        </tr>
 </tbody>
</table>
</div>
</FooterTemplate>
</asp:Repeater>
    </tbody>
</table>
</div>
