<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YammerFeed.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.Yammer.YammerFeed" %>

<h3 class="yammerheader">Senaste inläggen från Yammer</h3>
<ul class="yammer-list">
<asp:Repeater ID="yammerFeedRepeater" runat="server" DataSource='<%# Model.Messages %>'>
    <ItemTemplate>
        <li>
            <img src='<%# Eval("AuthorImageUrl") %>' alt='<%# Eval("AuthorName") %>' /><p class="creator">av <span><%# Eval("AuthorName") %></span><br /><%# Eval("Created") %></p>
            <p class="yammer-content"><%# Eval("Content") %></p>

        </li>
    </ItemTemplate>
</asp:Repeater>
</ul>