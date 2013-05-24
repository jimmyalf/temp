<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetMemberList.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Intranet.Wpc.Member.IntranetMemberList" %>
<asp:Repeater ID="rptMembers" runat="server">
<HeaderTemplate><ol id="recently-edited-profiles">
</HeaderTemplate>
<ItemTemplate>
<li>
<div class="people"><a href='<%# GetMainLink(int.Parse(DataBinder.Eval(Container.DataItem, "cMemberId").ToString()),int.Parse(DataBinder.Eval(Container.DataItem, "UserId").ToString())) %>'><img src='/wpc/Member/ViewProfilePicture.aspx?memberId=<%# DataBinder.Eval(Container.DataItem, "cMemberId") %>&amp;width=35&amp;height=35' alt="" /> <%# DataBinder.Eval(Container.DataItem, "cFirstName") %> <%# DataBinder.Eval(Container.DataItem, "cLastName") %></a>
<span class="email"> (<a href='mailto:<%# DataBinder.Eval(Container.DataItem, "cEmail") %>'><%# DataBinder.Eval(Container.DataItem, "cEmail") %></a>) </span>
<ul class="options">
    <li id="mypage-btn"><a href='<%=ProfilePage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cUserId") %>' title="Min sida">Min sida</a></li>

    <li id="studio-btn"><a href='<%=StudioPage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cUserId") %>'  title="Ateljé">Ateljé</a></li>
    <li id="gallery-btn"><a href='<%=GalleryPage %>?owner=<%# DataBinder.Eval(Container.DataItem, "UserId") %>' title="Galleri">Galleri</a></li>
    <li id="blog-btn"><a href='<%=BlogPage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cUserId") %>' title="Blogg">Blogg</a></li>
</ul></div>
</li>
</ItemTemplate>
<FooterTemplate></ol></FooterTemplate>
</asp:Repeater>
<asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
<ItemTemplate>
    <asp:PlaceHolder ID="phCategoryHeader" runat="server">
        <h3><%# DataBinder.Eval(Container.DataItem, "Name") %></h3>
    </asp:PlaceHolder>
    
    <asp:Repeater ID="rptCategoryMembers" runat="server">
        <HeaderTemplate><ol>
        </HeaderTemplate>
        <ItemTemplate>
        <li>
        <div class="people"><a href='<%# GetMainLink(int.Parse(DataBinder.Eval(Container.DataItem, "cMemberId").ToString()),int.Parse(DataBinder.Eval(Container.DataItem, "UserId").ToString())) %>'><img src='/wpc/Member/ViewProfilePicture.aspx?memberId=<%# DataBinder.Eval(Container.DataItem, "cMemberId") %>&amp;width=35&amp;height=35' alt="" /> <%# DataBinder.Eval(Container.DataItem, "cFirstName") %> <%# DataBinder.Eval(Container.DataItem, "cLastName") %></a>
        <span class="email"> (<a href='mailto:<%# DataBinder.Eval(Container.DataItem, "cEmail") %>'><%# DataBinder.Eval(Container.DataItem, "cEmail") %></a>) </span>
        <ul class="options">
            <li id="mypage-btn"><a href='<%=ProfilePage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cUserId") %>' title="Min sida">Min sida</a></li>

            <li id="studio-btn"><a href='<%=StudioPage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cUserId") %>'  title="Ateljé">Ateljé</a></li>
            <li id="gallery-btn"><a href='<%=GalleryPage %>?owner=<%# DataBinder.Eval(Container.DataItem, "UserId") %>' title="Galleri">Galleri</a></li>
            <li id="blog-btn"><a href='<%=BlogPage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cUserId") %>' title="Blogg">Blogg</a></li>
        </ul></div>
        </li>
        </ItemTemplate>
        <FooterTemplate></ol></FooterTemplate>
    </asp:Repeater>
</ItemTemplate>
</asp:Repeater>

