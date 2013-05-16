<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
               
<table width="100%" cellpadding="2" cellspacing="0">
    <tr>
        <td colspan="2">
            <asp:Repeater EnableViewState="false" runat="server" id="forumGroupRepeater">

	            <HeaderTemplate>
                    <table class="tableBorder" cellPadding="3" cellSpacing="1">
                        <tr> 
                            <td class="column" align="center" width="100%">Forum</td>
                            <td class="column" align="center" width="150" nowrap>Posts to Moderate</td>
                        </tr>
	            </HeaderTemplate>

	            <ItemTemplate>
            	
			            <tr>
				            <td class="h1a" colspan="2" valign="bottom">
				            <%# DataBinder.Eval(Container.DataItem, "Name") %>
				            </td>
			            </tr>
            			
			            <Forums:ForumRepeater ForumGroupID='<%# DataBinder.Eval(Container.DataItem, "ForumGroupID") %>' Mode="Moderator" HideForums='<%# DataBinder.Eval(Container.DataItem, "HideForums") %>' runat="server">
				            <ItemTemplate>
					            <tr>
						            <td class="f">
							            <a href="<%# Globals.GetSiteUrls().ModerateForum( ((Forum) Container.DataItem).ForumID ) %>"><%# DataBinder.Eval(Container.DataItem, "Name") %></a>
							            <br>
							            <%# DataBinder.Eval(Container.DataItem, "Description") %><%# Formatter.FormatSubForum( (Forum) Container.DataItem ) %>
						            </td>

						            <td class="fh" align="center">
							            <%# DataBinder.Eval(Container.DataItem, "PostsToModerate" ) %>
						            </td>
					            </tr>
				            </ItemTemplate>
			            </Forums:ForumRepeater>
            			
	            </ItemTemplate>
            	
	            <FooterTemplate>
            	
		            </table>
            		
	            </FooterTemplate>							
            </asp:Repeater>
        </td>
    </tr>
</table>            
