<%@ Control Language="C#" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Enumerations" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<!-- ********* View-PostFlatViewPrint.ascx:Start ************* //-->	
<!-- View-PostFlatViewPrint.Header.Start -->
<p>
<table cellpadding="2" cellspacing="0" width="100%">
  <tr>
    <td align="left">
      <asp:Label class="forumName" ID="ForumName" Runat="server" />
      <br />
      <span class="forumThread">Topic: <asp:Label ID="PostSubject" Runat="server" /></span>
    </td>

    <td align="right" valign="top" class="txt4Bold">
      <img src='<%= Globals.GetSkinPath() + "/images/title.gif" %>'>
      <br />
      <%= Globals.GetSiteSettings().SiteName %>
    </td>
  </tr>
</table>
</p>
<!-- View-PostFlatViewPrint.Header.End -->
<!-- View-PostFlatViewPrint.Pager.Start -->
<p>
<table cellpadding="3" cellspacing="0" width="100%" border="0">
  <tr>
    <td align="left" class="txt4" valign="bottom">
      <Forums:CurrentPage id="CurrentPage" runat="server" />
    </td>
    <td align="right" valign="bottom" class="txt4">
      <Forums:Pager id="Pager" runat="server" />
    </td>
  </tr>
</table>
</p>
<!-- View-PostFlatViewPrint.Pager.End -->
<hr size="1" />
<table cellpadding="3" cellspacing="0" width="100%" border="0">
<!-- ********* PostFlatViewPrint.Repeater.Start ************* //-->							    
<asp:Repeater ID="PostRepeater" EnableViewState="false" Runat="server">
  <ItemTemplate>
	<!-- ********* ItemTemplate.Start ************* //-->	
      <tr>
        <td>
          <table width="100%">
            <tr> 
              <td class="printDetails" align="left">
                Posted by <b><%# Formatter.FormatUsername ( ((User) DataBinder.Eval (Container.DataItem, "User" )).UserID, DataBinder.Eval (Container.DataItem, "Username").ToString() ) %></b> on <b><%# Formatter.FormatDate ( ((Post) Container.DataItem).PostDate, true ) %></b>
              </td>
            </tr>
            <tr>
              <td class="txt3" colspan="2">
                <%# DataBinder.Eval (Container.DataItem, "FormattedBody" ) %>
              </td>
            </tr>
          </table>
        </td>
      </tr>
	<!-- ********* ItemTemplate.End ************* //-->	
  </ItemTemplate>
  <SeparatorTemplate>
	<!-- ********* SeparatorTemplate.Start ************* //-->	
      <tr>
        <td colspan="2" align="left">
          <hr size="1" width="300">
        </td>
      </tr>
	<!-- ********* SeparatorTemplate.End ************* //-->	
  </SeparatorTemplate>
</asp:Repeater> 
<!-- ********* View-PostFlatViewPrint.Repeater.End ************* //-->							    
</table>
<hr size="1" />
<!-- ********* View-PostFlatViewPrint.ascx:End ************* //-->	
  