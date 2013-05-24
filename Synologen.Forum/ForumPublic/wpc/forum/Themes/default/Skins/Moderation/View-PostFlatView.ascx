<%@ Control Language="C#" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Enumerations" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<p>
    <table cellPadding="2" cellspacing="0" width="100%">

        <tr>
            <td align="left" colspan="2">
                <asp:Label Class="forumName" ID="ForumName" Runat="server" />
                <br>
                <span class="forumThread">Topic: <asp:Label ID="PostSubject" Runat="server" /></span>
                <%--
                TODO: Next version
                <P>
                Moderators: [TODO]
                <br>
                Users browsing this forum: [TODO]
                --%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colSpan="2">
                    <table class="tableBorder" cellpadding="3" cellspacing="1">

                <asp:Repeater ID="PostRepeater" EnableViewState="false" Runat="server">
                    <ItemTemplate>
                        <tr>
                            <td class="fh" rowspan="2" valign="top" nowrap>
                                <a name='<%# DataBinder.Eval (Container.DataItem, "PostID" ) %>'></a>
                                <Forums:UserOnlineStatus runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
                                <b><a href='<%# Globals.GetSiteUrls().UserProfile( ((User) DataBinder.Eval (Container.DataItem, "User" )).UserID ) %>' runat="server">
                                        <%# DataBinder.Eval (Container.DataItem, "Username" ) %>
                                    </a></b>
                                <br>
                                <Forums:UserAvatar runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
                                <span class="txt2">
                                <Forums:UserAttribute FormatString="<br>{0}" Attribute="Joined" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
                                <Forums:UserAttribute FormatString="<br>{0}" Attribute="Location" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
                                <Forums:UserAttribute FormatString="<br>{0}" Attribute="Posts" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
                                <br>
                                <Forums:RoleIcons runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
                                </span>
                            </td>
                            <td class="f" width="100%">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <span class="txt4">
                                                <b><%# DataBinder.Eval (Container.DataItem, "Subject" ) %></b>
                                                <br>
                                                Posted <%# Formatter.FormatDate ( ((Post) Container.DataItem).PostDate, true ) %>
                                                <Forums:PostAttachment runat="server" Post='<%# Container.DataItem %>' />
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100" valign="top" class="fh">
                                <Forums:PostView Post='<%# (Post) Container.DataItem %>' runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="fh" align="left">
                                <a class="txt2" href="#top">Back to top</a>
                            </td>
                            <td class="fh" align="center">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" width="100%">
                                        </td>
                                        <td width="250" align="right" class="txt4" nowrap>
                                           <%# Formatter.PostIPAddress ( (Post) Container.DataItem ) %> | Report
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </ItemTemplate>

                    <SeparatorTemplate>
                        <tr>
                            <td colspan="2" class="threadSeparator" >
                            </td>
                        </tr>
                    </SeparatorTemplate>

                    <FooterTemplate>
                        <tr>
                            <td>
                            </td>

                            <td>
                            </td>
                        </tr>
                    </FooterTemplate> 

                  </asp:Repeater> 
             </table>

        </td> 
    </tr>

    <tr>
        <td align="left" class="txt4" valign="bottom">
          <Forums:CurrentPage id="CurrentPage" runat="server" />
        </td>
        <td align="right" valign="bottom" class="txt4">
            <Forums:Pager id="Pager" runat="server" />
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
</table>
