<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainList.ascx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.MainList" %>
<asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
    <HeaderTemplate><div></HeaderTemplate>
    <ItemTemplate>
        <div ID="divObjectInfoContainer" runat="server" visible=false>
            <asp:Label ID="lblCategoryId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cCategoryId") %>'/>
        </div>
        <asp:panel id="pnlCategoryObject" runat="server">
            <h2><asp:Label ID="lblCategoryName" runat="server" ><%# DataBinder.Eval(Container.DataItem, "cName") %></asp:Label></h2>
            <asp:Repeater ID="rptMain" runat="server" OnItemDataBound="rptMain_ItemDataBound">
                <HeaderTemplate><div></HeaderTemplate>
                <ItemTemplate>
                    <div ID="divObjectInfoContainerMain" runat="server" visible=false>
                        <asp:Label ID="lblMainId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cId") %>'/>
                    </div>
                    &nbsp;&nbsp;<asp:HyperLink runat="server" ID="hlMainLink">
                        <B>
					    <%# DataBinder.Eval (Container.DataItem, "cName") %>
					    -
					    <%# DataBinder.Eval (Container.DataItem, "cDescription") %>
					    </B>
					</asp:HyperLink>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
        </asp:panel>
    </ItemTemplate>
    <FooterTemplate></div></FooterTemplate>
</asp:Repeater>