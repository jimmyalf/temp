<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<!-- ********* View-ForumGroupView.ascx:Start ************* //-->	
<table width="100%" cellspacing="12" cellpadding="0" border="0">
<!-- ForumGroupView.Header.Start -->
	<%	if ( Spinit.Wpc.Forum.Users.GetUser().IsAnonymous ) { %>
	<tr>
		<td>
			<!-- ForumGroupView.Header.End -->	
			<Forums:Login SkinFilename="Skin-LoginSmall.ascx" runat="server" />
			<!-- ForumGroupView.MainCentent.Start -->
		</td>
	</tr>
	<%	}	%>
	<tr>
		<td valign="top" width="100%">
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="bottom" colspan="2">
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td class="txt4" colspan="2" align="left" nowrap>
									<Forums:CurrentTime runat="server" />
									<br />
									<Forums:ForumAnchor class="lnk3" AnchorType="PostsActive" runat="server" />
									|
									<Forums:ForumAnchor class="lnk3" AnchorType="PostsUnanswered" runat="server" />
								</td>
								<td class="txt4" align="right" valign="top" nowrap>
									<Forums:SearchRedirect ID="SearchRedirect" runat="server" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2">
                        <!-- ********* Repeater.Start ************* //-->							
                        <asp:Repeater EnableViewState="false" runat="server" id="forumGroupRepeater">						
	                        <HeaderTemplate>
	                        <!-- ********* Repeater.Header.Start ************* //-->									
					                        <table width="100%" class="tableBorder" cellpadding="4" cellspacing="1">
						                        <tr> 
							                        <td colspan="2" class="column" align="center" width="*"><% = ResourceManager.GetString("ForumGroupView_Inline1") %></td>
							                        <td class="column" align="center" width="177" nowrap><%= ResourceManager.GetString("ForumGroupView_Inline4") %></td>                                    
							                        <td class="column" align="center" width="65" nowrap><%= ResourceManager.GetString("ForumGroupView_Inline2") %></td>                                    
							                        <td class="column" align="center" width="65" nowrap><%= ResourceManager.GetString("ForumGroupView_Inline3") %></td>
                    	                        </tr>
					                        </table>
	                        <!-- ********* Repeater.HeaderTemplate.End ************* //-->										
	                        </HeaderTemplate>
	                        <ItemTemplate>
	                        <!-- ********* Repeater.ItemTemplate.Start ************* //-->									
					                        <div style="padding-bottom: 6px;">
					                        <table width="100%" class="tableBorder" cellpadding="4" cellspacing="1">
						                        <tr>
							                        <td class="fh" colspan="5" valign="bottom">
								                        <asp:ImageButton ID="ExpandCollapse" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ForumGroupID") %>' ImageUrl='<%# Formatter.ExplandCollapseIcon( (ForumGroup) Container.DataItem ) %>' ToolTip='<%# ResourceManager.GetString("ForumGroupView_ExpandCollapse")%>' Runat="server"/>
								                        &nbsp;<a href="<%# Globals.GetSiteUrls().ForumGroup ( (int) DataBinder.Eval(Container.DataItem, "ForumGroupID")) %>"><%# DataBinder.Eval(Container.DataItem, "Name") %></a>
							                        </td>
						                        </tr>
		                        <!-- ********* Repeater.ItemTemplate.ForumsRepeater.Start ************* //-->				                    	
		                        <Forums:ForumRepeater ForumGroupID='<%# DataBinder.Eval(Container.DataItem, "ForumGroupID") %>' HideForums='<%# DataBinder.Eval(Container.DataItem, "HideForums") %>' runat="server">										
			                        <ItemTemplate>
			                        <!-- ********* Repeater.ItemTemplate.ForumsRepeater.ItemTemplate.Start ************* //-->								            
						                        <tr>
							                        <td class="f" width="20">
								                        <%# Formatter.StatusIcon( (Forum) Container.DataItem ) %>
							                        </td>
							                        <td class="f" width="*">
								                        <b><a href="<%# Globals.GetSiteUrls().Forum( ((Forum) Container.DataItem).ForumID ) %>"><%# DataBinder.Eval(Container.DataItem, "Name") %></a></b> <%# Formatter.FormatUsersViewingForum( (Forum) Container.DataItem ) %>
								                        <br />
								                        <span class="txt5"></span><%# DataBinder.Eval(Container.DataItem, "Description") %><%# Formatter.FormatSubForum( (Forum) Container.DataItem ) %></span>
							                        </td>
							                        <td class="fh4" align="center" width="175">
								                        <%# Formatter.FormatLastPost( (Forum) Container.DataItem, (bool) true ) %>
							                        </td>
							                        <td class="fh3" align="center" width="64">
								                        <%# Formatter.FormatNumber( ((Forum) Container.DataItem).TotalThreads ) %>
							                        </td>                   						
							                        <td class="fh3" align="center" width="65">
								                        <%# Formatter.FormatNumber( ((Forum) Container.DataItem).TotalPosts ) %>
							                        </td>
						                        </tr>
			                        <!-- ********* Repeater.ItemTemplate.ForumsRepeater.ItemTemplate.End ************* //-->	
			                        </ItemTemplate>
		                        </Forums:ForumRepeater>							
		                        <!-- ********* Repeater.ItemTemplate.ForumsRepeater.End ************* //-->										
					                        </table>
					                        </div>		
	                        <!-- ********* Repeater.ItemTemplate.End ************* //-->											                    
	                        </ItemTemplate>
                        </asp:Repeater>
                        <!-- ********* Repeater.End ************* //-->								
					</td>
				</tr>
			    
				<tr>
					<td class="txt4" align="left">
						<Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb1" /><br />
						<Forums:MarkAllRead runat="server" />
					</td>
			        
					<td align="right" class="txt4">
						<Forums:JumpDropDownList runat="server" />
					</td>
				</tr>

				<tr>
					<td class="txt4" align="left">
					</td>
			        
					<td align="right" class="txt4">
					</td>
				</tr>
			</table> 
		</td>
	</tr>
<!-- ForumGroupView.MainCentent.End -->	
</table>
<!-- ********* View-ForumGroupView.ascx:End ************* //-->	