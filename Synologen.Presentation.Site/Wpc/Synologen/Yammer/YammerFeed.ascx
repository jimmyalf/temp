<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YammerFeed.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.Yammer.YammerFeed" %>

<ul>
<asp:Repeater ID="yammerFeedRepeater" runat="server" DataSource='<%# Model.Messages %>'>
    <ItemTemplate>
        <li>
            <img src='<%# Eval("AuthorImageUrl") %>' alt='<%# Eval("AuthorName") %>' />
            <%# Eval("Content") %><br />
            av <%# Eval("AuthorName") %> <%# Eval("Created") %>
        </li>
    </ItemTemplate>
</asp:Repeater>
</ul>