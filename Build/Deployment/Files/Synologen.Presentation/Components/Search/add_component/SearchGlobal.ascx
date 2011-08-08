<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchGlobal.ascx.cs" Inherits="Spinit.Wpc.Search.Presentation.Components.Search.add_component.SearchGlobal" %>
<div class="Component-Search-AddComponent-SearchGlobal-ascx fullBox">
<div class="wrap">
	<fieldset>
        <legend>Insert Component search control</legend>   	    
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblHeading" AssociatedControlID="txtHeading" runat="server" SkinID="Long">Heading text</asp:Label>
	        <asp:TextBox ID="txtHeading" runat="server" />
	    </div>	
	    <div class="formItem clearLeft">	    	        
	        <asp:Label ID="lblButtonText" AssociatedControlID="txtButtonText" runat="server" SkinID="Long">Button text</asp:Label>
	        <asp:TextBox ID="txtButtonText" runat="server" />	   
	    </div>	
	    <div class="formItem clearLeft">		        
	        <asp:Label ID="lblComponents" AssociatedControlID="drpComponents" runat="server" SkinID="Long">Components</asp:Label>
	        <asp:DropDownList ID="drpComponents" runat="server" />	 
	    </div>	
	    <div class="formItem clearLeft">		         	 
	        <asp:Label ID="lblCategories" AssociatedControlID="txtCategory" runat="server" SkinID="Long" >Category Id</asp:Label>
	        <asp:TextBox ID="txtCategory" runat="server" />
	    </div>		        
    	    
    </fieldset>
</div>
</div>
