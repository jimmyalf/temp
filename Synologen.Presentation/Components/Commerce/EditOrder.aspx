<%@ Page Language="C#" MasterPageFile="~/components/Commerce/ShopMain.Master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.EditOrder" Title="Untitled Page" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="cntCommerceMain" ContentPlaceHolderID="phCommerce" runat="server">
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-Commerce-EditOrder-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>
	
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
								
		<div class="formItem clearLeft">
                <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" SkinId="Long"/>
                <asp:TextBox ID="txtFirstName" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false"/>
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" SkinId="Long"/>
                <asp:TextBox ID="txtLastName" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false"/>
		</div>
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblCompany" runat="server" AssociatedControlID="txtCompany" SkinId="Long"/>
                <asp:TextBox ID="txtCompany" runat="server" TextMode="MultiLine" SkinID="Wide" Enabled="false" />
		</div>
		
	    <div class="formItem clearLeft">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" SkinId="Long"/>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false" />
		</div>		
	    
	    <div class="formItem clearLeft">
                <asp:Label ID="lblPhone" runat="server" AssociatedControlID="txtPhone" SkinId="Long"/>
                <asp:TextBox ID="txtPhone" runat="server" TextMode="SingleLine" SkinID="Wide"  Enabled="false"/>
		</div>		
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress" SkinId="Long"/>
                <asp:TextBox ID="txtAddress" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false"/>
		</div>
		
	    <div class="formItem clearLeft">
                <asp:Label ID="lblPostCode" runat="server" AssociatedControlID="txtPostCode" SkinId="Long"/>
                <asp:TextBox ID="txtPostCode" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false" />
		</div>		

	    <div class="formItem clearLeft">
                <asp:Label ID="lblCity" runat="server" AssociatedControlID="txtCity" SkinId="Long"/>
                <asp:TextBox ID="txtCity" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false" />
		</div>		
	    
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblDelAddress" runat="server" AssociatedControlID="txtDelAddress" SkinId="Long"/>
                <asp:TextBox ID="txtDelAddress" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false"/>
		</div>
		
	    <div class="formItem clearLeft">
                <asp:Label ID="lblDelPostCode" runat="server" AssociatedControlID="txtDelPostCode" SkinId="Long"/>
                <asp:TextBox ID="txtDelPostCode" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false" />
		</div>		

	    <div class="formItem clearLeft">
                <asp:Label ID="lblDelCity" runat="server" AssociatedControlID="txtDelCity" SkinId="Long"/>
                <asp:TextBox ID="txtDelCity" runat="server" TextMode="SingleLine" SkinID="Wide" Enabled="false" />
		</div>		
	</fieldset>
	
	<asp:PlaceHolder ID="phOrderStatus" runat="server">	
	<fieldset>
		<legend><asp:Label ID="lblHeaderStatus" runat="server"></asp:Label></legend>
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblOrderStatus" runat="server" AssociatedControlID="drpOrderStatus" SkinId="Long"/>
                <asp:DropDownList ID="drpOrderStatus" runat="server" SkinID="Wide"/>
		</div>
		
		<div class="formCommands">					    
		    <asp:button ID="btnChangeStatus" runat="server" CommandName="ChangeStatus" OnClick="btnChangeStatus_Click" Text="Change Status" SkinId="Big"/>
		</div>
	</fieldset>
	</asp:PlaceHolder>
	
	<asp:PlaceHolder ID="phOrderItems" runat="server">	
	<fieldset>
		<legend><asp:Label ID="lblHeaderItems" runat="server"></asp:Label></legend>

		<!--<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>-->

		<asp:GridView
			ID="gvOrderItems"
			runat="server"
			OnRowCreated="gvOrderItems_RowCreated"
			DataKeyNames="Id" 
			OnSorting="gvOrderItems_Sorting"
			OnPageIndexChanging="gvOrderItems_PageIndexChanging"
			SkinID="Striped"
			OnRowDataBound="gvOrderItems_RowDataBound"
			AllowSorting="true">
			<Columns>
				<asp:BoundField headerText="Id" DataField="Id" SortExpression="Id"/>
				<asp:TemplateField headertext="Product">
					<ItemTemplate>
						<asp:Label id="lblProduct" runat="server"/>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField headertext="Product Number">
					<ItemTemplate>
						<asp:Label id="lblProductNumber" runat="server"/>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField headerText="Number Of Products" DataField="NoOfProducts" SortExpression="NoOfProducts"/>
				<asp:BoundField headerText="Price" DataField="Price" SortExpression="Price" DataFormatString="{0:F2}"/>
				<asp:BoundField headerText="Sum" DataField="Sum" SortExpression="Sum" DataFormatString="{0:F2}"/>
				<asp:TemplateField headertext="Currency">
					<ItemTemplate>
						<asp:Label id="lblCurrency" runat="server"/>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</fieldset>	
	</asp:PlaceHolder>																						
</div>
</div>
</div>
</asp:Content>
