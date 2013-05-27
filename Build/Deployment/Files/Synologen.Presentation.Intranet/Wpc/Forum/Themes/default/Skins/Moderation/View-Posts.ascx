<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>

<table cellPadding="2" cellspacing="0" width="100%">
  
    <tr>
        <td align="left" valign="top">
            <asp:Label class="forumName" ID="ForumName" Runat="server" />
            <br>
            <asp:Label class="forumThread" ID="ForumDescription" Runat="server" />
        </td>
        
        <td align="right" valign="top">
            <Forums:NavigationMenu DisplayTitle="false" id="Navigationmenu1" runat="server" />
        </td>
    </tr>

        <tr>
            <td>
                &nbsp;
            </td>
        </tr>

  <tr>
    <td align="left" valign="middle">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td valign="middle" class="txt4">
                    <Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb1"/>
                </td>
            </tr>
        </table>
         
    </td>    
    <td align="right" valign="bottom">
    </td>
  </tr>
  
  <tr>
    <td vAlign="top" colSpan="3">

        <table class="tableBorder" width="100%" cellpadding="3" cellspacing="1">
            <tr>

                <td class="column" align="left">
                    Posts awaiting moderation
                </td>
            </tr>

        <asp:Repeater EnableViewState="False" ID="PostRepeater" runat="server">
            <ItemTemplate>
                    <tr>
                        <td class="f" width="100%">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" class="txt3">
                                        <b>Subject:</b> <a class="lnk3" href="<%# Globals.GetSiteUrls().Post( (int) DataBinder.Eval(Container.DataItem, "ThreadID") ) %> "><%# DataBinder.Eval(Container.DataItem, "Subject") %></a>
                                    </td>
                                    
                                    <td align="right" class="txt3">
                                        <b>Post ID:</b> <%# DataBinder.Eval(Container.DataItem, "PostID") %>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="left" class="txt3">
                                        <b>Author:</b> <a href="<%# Globals.GetSiteUrls().UserProfile( (int) DataBinder.Eval(Container.DataItem, "User.UserID") ) %> "><%# DataBinder.Eval(Container.DataItem, "Username") %></a>
                                    </td>
                                    
                                    <td align="right" class="txt3">
                                        <b>Post Date:</b> <%# DataBinder.Eval(Container.DataItem, "PostDate") %>
                                    </td>
                                </tr>
                            </table>
                            <hr size="1">
                            <%# DataBinder.Eval(Container.DataItem, "Body" ) %>
                            <Forums:ModerationMenu Post='<%# Container.DataItem %>' runat="server" />
                        </td>

                    </tr>
            </ItemTemplate>
            
            <SeparatorTemplate>
                    <tr>
                        <td colspan="4" height="3" class="fh">
                        </td>
                    </tr>
            </SeparatorTemplate>
        </asp:Repeater>
        </table>

	</td>
  </tr>

  <tr>
    <td align="left">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td valign="middle" class="txt4">
                    <Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb3"/>
                </td>
            </tr>
        </table>
         
    </td>    
    <td align="right" valign="bottom" class="txt4">
        <Forums:Pager id="Pager" runat="server"/>
    </td>
  </tr>

  <tr>
    <td vAlign="bottom" align="left" class="txt4">
        <Forums:CurrentPage id="CurrentPage" runat="server"/>
    </td>
    <td align="right" class="txt4">
    </td>
  </tr>

</table>
