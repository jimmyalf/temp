<%@ Page language="c#" MasterPageFile="~/Content/ContentMain.master" Inherits="Spinit.Wpc.Content.Presentation.Content.Main" Codebehind="Main.aspx.cs" %>
<asp:Content ID="Tree" ContentPlaceHolderID="ComponentPage" Runat="Server">
<div class="Content-Main-aspx fullBox">
<div class="wrap">
<h1>Content</h1>
<div id="dLatestChanges" runat="server" class="box">
    <h2>Latest changes</h2>
    <ASP:DATALIST id="dltLatestChanges" OnItemCreated="dltLatestChanges_ItemCreated" runat="server" SkinID="Striped">
	    <ITEMTEMPLATE>
		    <ASP:HYPERLINK runat="server" id="hplLatestChanges"><%# DataBinder.Eval(Container.DataItem, "ItemName") %></ASP:HYPERLINK>
	        <asp:Label runat="server" id="lblChangedBy">&nbsp;Changed by&nbsp;<%# DataBinder.Eval(Container.DataItem, "EditedByName") %></asp:Label>
            <ASP:Label runat="server" id="lblChangedDate">&nbsp;at&nbsp;<%# DataBinder.Eval (Container.DataItem, "EditedDate")%></ASP:Label>	    
        </ITEMTEMPLATE>
   </ASP:DATALIST>
</div>

<div id="dForApproval" runat="server" class="box">
    <h2>For approval</h2>
    <ASP:DATALIST id="dltForApproval" OnItemCreated="dltForApproval_ItemCreated" runat="server" SkinID="Striped">
	    <ITEMTEMPLATE>
		    <ASP:HYPERLINK runat="server" id="hplForApproval"><%# DataBinder.Eval(Container.DataItem, "ItemName") %></ASP:HYPERLINK>
	        <asp:Label runat="server" id="lblChangedBy">&nbsp;Changed by&nbsp;<%# DataBinder.Eval(Container.DataItem, "EditedByName") %></asp:Label>
            <ASP:Label runat="server" id="lblChangedDate">&nbsp;at&nbsp;<%# DataBinder.Eval (Container.DataItem, "EditedDate")%></ASP:Label>	    
	    </ITEMTEMPLATE>
    </ASP:DATALIST>
</div>

<div id="dInWork" runat="server" class="box">
    <h2>In work</h2>
    <ASP:DATALIST id="dltInWork" OnItemCreated="dltInWork_ItemCreated" runat="server" SkinID="Striped">
	    <ITEMTEMPLATE>
		    <ASP:HYPERLINK runat="server" id="hplInWork"><%# DataBinder.Eval(Container.DataItem, "ItemName") %></ASP:HYPERLINK>
	        <asp:Label runat="server" id="lblChangedBy">&nbsp;Checked out by&nbsp;<%# DataBinder.Eval(Container.DataItem, "EditedByName") %></asp:Label>
            <ASP:Label runat="server" id="lblChangedDate">&nbsp;at&nbsp;<%# DataBinder.Eval (Container.DataItem, "EditedDate")%></ASP:Label>	    
	    </ITEMTEMPLATE>
    </ASP:DATALIST>
</div>
</div>
</div>
</asp:Content>
