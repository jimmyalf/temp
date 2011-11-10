<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YammerFeed.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.Yammer.YammerFeed" %>

<ul class="yammer-list">
<asp:Repeater ID="yammerFeedRepeater" runat="server" DataSource='<%# Model.Messages %>'>
    <ItemTemplate>
        <li>
            
            
            <div class="clear"></div>
            
            <div class='yammer-images'>
                <asp:Repeater ID="yammerImageRepeater" runat="server" DataSource='<%# Eval("Images") %>'>
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target='_blank' class='fancybox'><img src='<%# Eval("Thumbnail") %>' alt='<%# Eval("Name") %>' /></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            
            <p class="yammer-content"><%# Eval("Content") %></p>
            
            <p class="creator"><img src="<%# Eval("AuthorImageUrl") %>" /> av<span> <%# Eval("AuthorName") %></span> <br /><%# Eval("Created") %></p>
        </li>
    </ItemTemplate>
</asp:Repeater>
</ul>