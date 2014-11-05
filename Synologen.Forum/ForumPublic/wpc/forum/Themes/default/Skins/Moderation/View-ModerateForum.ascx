<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>

<table cellPadding="2" cellspacing="0" width="100%" border="0">
  
    <tr>
        <td align="left" valign="top">
            <asp:Label class="forumName" ID="ForumTitle" Runat="server" />
            <br>
            <asp:Label class="forumThread" ID="ForumDescription" Runat="server" />
        </td>
    </tr>

        <tr>
            <td>
                &nbsp;
            </td>
        </tr>

  
  <tr>
    <td vAlign="top">

        <asp:Repeater EnableViewState="False" ID="PostRepeater" runat="server">
            <headerTemplate>
              <table width="100%" cellpadding="3" cellspacing="1">
            </headerTemplate>

            <ItemTemplate>
              <tr>
                <td class="column">
                   <%# DataBinder.Eval(Container.DataItem, "Subject") %> (<a target="_Preview" href="<%# Globals.GetSiteUrls().ModerateViewPost( (int) DataBinder.Eval(Container.DataItem, "ThreadID") ) %> ">View Thread</a>)
                <td>
              </tr>
              
              <tr>
                <td class="f">
                <table width="100%" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="f" width="100%">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" class="f">
                                        <b>Post ID:</b> <%# DataBinder.Eval(Container.DataItem, "PostID") %>
                                        <br>
                                        <b>Author:</b> <a target="_Preview" href="<%# Globals.GetSiteUrls().ModerateViewUserProfile( (int) DataBinder.Eval(Container.DataItem, "User.UserID") ) %> "><%# DataBinder.Eval(Container.DataItem, "Username") %></a>
                                        <br>
                                        <b>Post Date:</b> <%# Users.GetUser().GetTimezone( DateTime.Parse(DataBinder.Eval(Container.DataItem, "PostDate").ToString())).ToString() %>
                                    </td>
                                </tr>
                            </table>
                            <hr size="1">
                            <%# DataBinder.Eval(Container.DataItem, "Body" ) %>
                            <Forums:ModerationMenu Post='<%# Container.DataItem %>' runat="server" />
                        </td>
                    </tr>
                </table>
                </td>
              </tr>
            </ItemTemplate>
            
            <SeparatorTemplate>
              <tr>
                <td class="moderateSeparator">
                </td>
              </tr>
            </SeparatorTemplate>

            <FooterTemplate>
              </table>
            </FooterTemplate>
        </asp:Repeater>

	</td>
  </tr>
</table>
