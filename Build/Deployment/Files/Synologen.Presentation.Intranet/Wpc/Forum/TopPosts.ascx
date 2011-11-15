<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPosts.ascx.cs" Inherits="Spinit.Wpc.Forum.Presentation.Site.TopPosts" %>
<asp:DataList ID="dlPosts" runat="server">
<ItemTemplate>
<div>
						<b><%# DataBinder.Eval(Container.DataItem, "Subject")%></b><br />
						<%# DataBinder.Eval(Container.DataItem, "Body").ToString()%><br />
					<a href="/wpc/forum/ShowPost.aspx?PostID=<%# DataBinder.Eval(Container.DataItem, "PostId")%>">Läs mer &gt;</a><br /><br />
					</div>
</ItemTemplate>
</asp:DataList>
