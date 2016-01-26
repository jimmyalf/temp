<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Control Language="C#" %>
<script runat="server">
  int i = 1;
</script>
<table border="0" cellPadding="0" cellSpacing="0">
    <tr>
        <td><img src="<%= Globals.GetSkinPath() %>/images/forumHeader-Curve-Left.gif"></td>
        <td width="100%" class="h1"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Moderator_Statistics_Title")%></td>
        <td><img src="<%= Globals.GetSkinPath() %>/images/forumHeader-Curve-Right.gif"></td>
    </tr>
</table>
<table class="tableBorder" cellSpacing="1" cellPadding="3" width="100%">
    <tr>
        <td class="f" vAlign="top">
            <table CellPadding="2" CellSpacing="2">
                <tr>
                    <td>
                        <span class="normalTextSmaller">
            <b>
                                <asp:label id="TotalModerators" runat="server" />&nbsp;</b>moderators 
            have moderated <b><asp:label id="TotalModeratedPosts" runat="server" />&nbsp;</b>threads 
            and posts.
            </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="normalTextSmaller">
              10 most active moderators:<br> 
              <asp:Repeater id="TopModerators" runat="server">
                                <ItemTemplate>
                                    <%# (i++).ToString() %>
                                    . <a href='<%# Globals.GetSiteUrls().UserProfile( (int) DataBinder.Eval(Container.DataItem, "UserID") ) %>'>
                                        <%# DataBinder.Eval(Container.DataItem, "Username") %>
                                    </a>(<%# ((Int32)DataBinder.Eval(Container.DataItem, "TotalPostsModerated")).ToString("n0") %>)<br>
                                </ItemTemplate>
                            </asp:Repeater>
            </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="normalTextSmaller">
              Moderator actions:<br> 
              <asp:Repeater id="ModerationAction" runat="server">
                                <ItemTemplate>
                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "Action") %>
                                    (<%# ((Int32)DataBinder.Eval(Container.DataItem, "ActionSummary")).ToString("n0") %>)<br>
                                </ItemTemplate>
                            </asp:Repeater>
            </span>
                    </td>
                </tr>
            </table>
    </tr>
</table>
