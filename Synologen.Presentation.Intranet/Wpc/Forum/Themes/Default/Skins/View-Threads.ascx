<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<!-- ********* View-Threads.ascx:Start ************* //-->	
<script language="Javascript">
  function OpenWindow (target) { 
    window.open(target, "test", "toolbar=no,scrollbars=yes,resizable=yes,width=500,height=400"); 
  }
</script>
<table width="100%" cellspacing="12" cellpadding="0" border="0">
<!-- View-Threads.Header.Start -->
	<tr>
		<td>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td valign="top">
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table class="tableBorder" width="100%" cellspacing="1" cellpadding="3">
										<tr>
											<td width="100%" class="column">&nbsp;</td>
										</tr>										
										<tr>
											<td class="fh">
												<table width="100%" cellspacing="0" border="0" cellpadding="0">
													<tr>    
														<td align="left" valign="middle">
															<table width="100%" cellpadding="4" cellspacing="0">
																<tr>
																	<td align="left">
																		<asp:Label CssClass="forumName" ID="ForumName" Runat="server" />												
																		<br /><br />
																		<asp:Label CssClass="forumThread" ID="ForumDescription" Runat="server" />
																	</td>
																</tr>
															</table>
														</td>
														<td width="1"><img height="85" width="1" src="<%# Globals.GetSkinPath() + "/images/spacer.gif"%>"></td>
													</tr>
												</table>    
											</td>    
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>  
<%-- 					
					<td valign="top" width="200">
						<Forums:DisplayLegendThread runat="server" />
					</td>					
--%>					
				</tr>
			</table>	
		</td>
	</tr>
<!-- View-Threads.Header.End -->	
<!-- View-Threads.MainCentent.Start -->
	<tr>
		<td>
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr>
					<td align="left" valign="bottom" colspan="2">
						<div style="padding-bottom: 6px;">
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td class="txt4Bold">
									&nbsp;<Forums:BreadCrumb ShowHome="true" runat="server" />
								</td>
								<td align="right" valign="top" class="txt4" nowrap>
									<Forums:SearchRedirect SkinFileName="Skin-SearchForum.ascx" ID="SearchRedirect" runat="server" />
								</td>
							</tr>
						</table>		
						</div>				
					</td>
				</tr>						
<!-- SUBFORUMS.START //-->	
<!-- ********* Repeater.Start ************* //-->	
<Forums:ForumRepeater runat="server" ID="Forumrepeater1">
	<HeaderTemplate>
	<!-- ********* HeaderTemplate.Start ************* //-->		
				<tr>
					<td colspan="2">				
						<table width="100%" class="tableBorder" cellpadding="4" cellspacing="1">
							<tr>
								<td class="column" align="center" width="*" colspan="2"><%= ResourceManager.GetString("Subforums") %></td>
								<td class="column" align="center" width="177" nowrap><%= ResourceManager.GetString("ForumGroupView_Inline4") %></td>							
								<td class="column" align="center" width="65" nowrap><%= ResourceManager.GetString("ForumGroupView_Inline2") %></td>
								<td class="column" align="center" width="65" nowrap><%= ResourceManager.GetString("ForumGroupView_Inline3") %></td>
							</tr>
						</table>
						<div style="padding-bottom: 6px;">
						<table width="100%" class="tableBorder" cellpadding="4" cellspacing="1">								
	<!-- ********* HeaderTemplate.End ************* //-->						
	</HeaderTemplate>
	<ItemTemplate>
	<!-- ********* ItemTemplate.Start ************* //-->	
							<tr>
								<td class="f" width="25">
									<%# Formatter.StatusIcon( (Forum) Container.DataItem ) %>
								</td>
								<td class="f" width="*">
									<a href="<%# Globals.GetSiteUrls().Forum( ((Forum) Container.DataItem).ForumID ) %>">
										<%# DataBinder.Eval(Container.DataItem, "Name") %>  <%# Formatter.FormatUsersViewingForum( (Forum) Container.DataItem ) %>
									</a>
									<br />
									<%# DataBinder.Eval(Container.DataItem, "Description") %>
									<%# Formatter.FormatSubForum( (Forum) Container.DataItem ) %>
								</td>
								<td width="175" class="fh3" align="center">
									<%# Formatter.FormatLastPost( (Forum) Container.DataItem, (bool) true ) %>
								</td>							
								<td width="64" class="fh3" align="center">
									<%# Formatter.FormatNumber( ((Forum) Container.DataItem).TotalThreads ) %>
								</td>
								<td width="65" class="fh3" align="center">
									<%# Formatter.FormatNumber( ((Forum) Container.DataItem).TotalPosts ) %>
								</td>
							</tr>
	<!-- ********* ItemTemplate.End ************* //-->							
	</ItemTemplate>
	<FooterTemplate>
	<!-- ********* FooterTemplate.Start ************* //-->						
						</table>
						</div>			
					</td>
				</tr>								
	<!-- ********* FooterTemplate.End ************* //-->						
	</FooterTemplate>
</Forums:ForumRepeater> 
<!-- ********* Repeater.End ************* //-->				
<!-- SUBFORUMS.END //-->	
				<tr>
					<td align="left" valign="middle" colspan="2">
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td style="padding-top: 6px;">
									<Forums:ForumImageButton ButtonType="NewPost" runat="server" ID="NewPostButton" />
								</td>
							</tr>
						</table>
					</td>
				</tr>	
				<tr>
					<td valign="top" colspan="3">
						<table width="100%" class="tableBorder" cellpadding="2" cellspacing="1">
							<tr>
								<td class="column" colspan="2" height="20" align="center" width="*"><%= ResourceManager.GetString("ViewThreads_TitleThread") %></td>
								<td class="column" align="center" width="177" nowrap><%= ResourceManager.GetString("ViewThreads_TitleLastPost") %></td>					
								<td class="column" align="center" width="65" nowrap><%= ResourceManager.GetString("ViewThreads_TitleReplies") %></td>
								<td class="column" align="center" width="65" nowrap><%= ResourceManager.GetString("ViewThreads_TitleViews") %></td>
							</tr>
						</table>
<!-- ANNOUNCEMENTS.START //-->				
<!-- ********* Repeater.Start ************* //-->
<asp:Repeater EnableViewState="False" ID="Announcements" runat="server">
	<HeaderTemplate>
	<!-- ********* HeaderTemplate.Start ************* //-->
						<div style="padding-bottom: 10px;">
						<table width="100%" class="tableBorder" cellpadding="2" cellspacing="1">
							<tr>
								<td colspan="5" class="fh">
									&nbsp;<b><%= ResourceManager.GetString("ViewThreads_FaqsAnnouncements")%></b>
								</td>
							</tr>
	<!-- ********* HeaderTemplate.End ************* //-->
	</HeaderTemplate>
	<ItemTemplate>
	<!-- ********* ItemTemplate.Start ************* //-->	
							<tr>
								<td width="25" valign="middle" align="right" class="fh3">
									<%# Formatter.StatusIcon( (Thread) Container.DataItem ) %>
								</td>
								<td width="*" class="fh3">
									<table width="100%" cellpadding="1" cellspacing="0">
										<tr>
											<td><%# Formatter.ThreadEmoticon( (Thread) Container.DataItem ) %></td>							
											<td width="100%" class="txt3Bold">
												<a class="lnk3" 
													title="<%# ( Globals.GetSiteSettings().EnablePostPreviewPopup && ForumContext.Current.User.EnablePostPreviewPopup  ? Formatter.CheckStringLength(Formatter.StripAllTags(DataBinder.Eval(Container.DataItem, "Body").ToString()), 300) : String.Empty )%>" 
													href="<%# Globals.GetSiteUrls().Post( (int) DataBinder.Eval(Container.DataItem, "PostID") ) %> "> 
													<%# Formatter.CheckStringLength(DataBinder.Eval(Container.DataItem, "Subject").ToString(), 65) %>
												</a>
											</td>
											<td align="right"><forums:RatingImageButton Thread='<%# (Thread) Container.DataItem %>' runat="server" /></td>
										</tr>
									</table>
									<table width="100%" cellpadding="0" cellspacing="0">									
										<tr>
											<td width="*" class="txt4">
												&nbsp;<%= ResourceManager.GetString("ForumGroupView_Legend_LastPostBy")%><%# Formatter.FormatUsername( (int) DataBinder.Eval(Container.DataItem, "AuthorID"), DataBinder.Eval(Container.DataItem, "Username").ToString() ) %>
												<%# Formatter.ForumNameInThreadView( (Thread) Container.DataItem ) %>
											</td>
											<td width="200" align="right" class="txt4">
												<Forums:ThreadPager runat="server" ThreadID='<%# DataBinder.Eval(Container.DataItem, "PostID") %>' Replies='<%# DataBinder.Eval(Container.DataItem, "Replies") %>' ID="Threadpager3" NAME="Threadpager2"/>
											</td>										
										</tr>
									</table>
								</td>
								<td width="175" class="f" align="right">
									<%# Formatter.FormatLastPost( (Thread) Container.DataItem ) %>
									<Forums:ThreadPager runat="server" ThreadID='<%# DataBinder.Eval(Container.DataItem, "PostID") %>' Replies='<%# DataBinder.Eval(Container.DataItem, "Replies") %>' ID="Threadpager1" NAME="Threadpager1"/>
								</td>							
								<td width="65" class="f" align="center">
									<%# Formatter.FormatNumber( ((Thread) Container.DataItem).Replies ) %>
								</td>
								<td width="65" class="f" align="center">
									<%# Formatter.FormatNumber( ((Thread) Container.DataItem).Views ) %>
								</td>
							</tr>
	<!-- ********* ItemTemplate.End ************* //-->								
	</ItemTemplate>				
	<FooterTemplate>
	<!-- ********* FooterTemplate.End ************* //-->					
							</table>
							</div>				
	<!-- ********* FooterTemplate.End ************* //-->							
	</FooterTemplate>
</asp:Repeater>
<!-- ********* Repeater.End ************* //-->	
<!-- ANNOUNCEMENTS.END //-->						
<!-- THREADSandSTICKIES.START //-->
<!-- ********* Repeater.Start ************* //-->	
<asp:Repeater EnableViewState="False" ID="Threads" runat="server">
	<HeaderTemplate>
	<!-- ********* HeaderTemplate.Start ************* //-->	
						<div style="padding-bottom: 6px;">
						<table width="100%" class="tableBorder" cellpadding="2" cellspacing="1">				
							<tr>
								<td colspan="5" class="fh">
									&nbsp;<b><%= ResourceManager.GetString("ViewThreads_Posts")%></b>
								</td>
							</tr>
	<!-- ********* HeaderTemplate.End ************* //-->					
	</HeaderTemplate>
	<ItemTemplate>
	<!-- ********* ItemTemplate.Start ************* //-->	
							<tr>
								<td width="25" class="fh3" valign="middle" align="right">
									<%# Formatter.StatusIcon( (Thread) Container.DataItem ) %>
								</td>
								<td width="*" class="fh3">
									<table width="100%" cellpadding="1" cellspacing="0">
										<tr>
											<td><%# Formatter.ThreadEmoticon( (Thread) Container.DataItem ) %></td>
											<td width="100%" class="txt3Bold">										
												<a class="lnk3" 
													title="<%# ( Globals.GetSiteSettings().EnablePostPreviewPopup && ForumContext.Current.User.EnablePostPreviewPopup  ? Formatter.CheckStringLength(Formatter.StripAllTags(DataBinder.Eval(Container.DataItem, "Body").ToString()), 300) : String.Empty )%>" 
													href="<%# Globals.GetSiteUrls().Post( (int) DataBinder.Eval(Container.DataItem, "PostID") ) %> "> 
													<%# Formatter.CheckStringLength(DataBinder.Eval(Container.DataItem, "Subject").ToString(), 65) %>
												</a>
											</td>
											<td class="txt4" align="right"><forums:RatingImageButton Thread='<%# (Thread) Container.DataItem %>' runat="server" /><forums:ThreadStatusImage Thread='<%# (Thread) Container.DataItem %>' runat="server" /></td>
										</tr>
									</table>
									<table width="100%" cellpadding="0" cellspacing="0">
										<tr>
											<td width="*" class="txt4">
												&nbsp;<%= ResourceManager.GetString("ForumGroupView_Legend_LastPostBy")%><%# Formatter.FormatUsername( (int) DataBinder.Eval(Container.DataItem, "AuthorID"), DataBinder.Eval(Container.DataItem, "Username").ToString() ) %>
												<%# Formatter.ForumNameInThreadView( (Thread) Container.DataItem ) %>
											</td>
											<!--
											<td width="200" align="right" class="txt4">
												<Forums:ThreadPager runat="server" ThreadID='<%# DataBinder.Eval(Container.DataItem, "PostID") %>' Replies='<%# DataBinder.Eval(Container.DataItem, "Replies") %>' ID="Threadpager2" NAME="Threadpager2"/>
											</td>
											-->
										</tr>
										<tr>
											<td width="200" align="right" class="txt4">
												<Forums:ThreadPager runat="server" ThreadID='<%# DataBinder.Eval(Container.DataItem, "PostID") %>' Replies='<%# DataBinder.Eval(Container.DataItem, "Replies") %>' ID="Threadpager4" NAME="Threadpager2"/>
											</td>
										</tr>										
									</table>
								</td>
								<!--
								<td width="175" class="f" align="right"><span class="txt3">
									<%# Formatter.FormatLastPost( (Thread) Container.DataItem ) %>
									</span>
								</td>							
								-->
								<td width="175" class="f" align="right"><span class="txt3">
									<%# Formatter.FormatLastPost( (Thread) Container.DataItem, true ) %>
									</span>
								</td>									
								<td width="65" class="f" align="center">
									<%# Formatter.FormatNumber( ((Thread) Container.DataItem).Replies ) %>
								</td>
								<td width="65" class="f" align="center">
									<%# Formatter.FormatNumber( ((Thread) Container.DataItem).Views ) %>
								</td>
							</tr>				
	<!-- ********* ItemTemplate.End ************* //-->	
	</ItemTemplate>
	<FooterTemplate>
	<!-- ********* FooterTemplate.Start ************* //-->					
						</table>
						</div>				
	<!-- ********* FooterTempalte.End ************* //-->							
	</FooterTemplate>
</asp:Repeater>
<!-- ********* Repeater.End ************* //-->	
<!-- THREADSandSTICKIES.END //-->
						<span id="NoThreadsToDisplay" runat="server">	
						<div style="padding-bottom: 6px;">		
						<table class="tableBorder" width="100%" align="center">
							<tr>
								<td class="fh" align="center"><%=ResourceManager.GetString("ViewThreads_NoTopics") %></td>
							</tr>
						</table>
						</div>
						</span>			
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td valign="top" align="left" width="10px">
						<Forums:ForumImageButton ButtonType="NewPost" runat="server" ID="NewPostButtonDown" />
					</td>	
					<td valign="top" align="right">
						<asp:Panel ID="DisplayPager" Visible="True" Runat="server">					
						<table class="tableBorder" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table width="100%" cellpadding="2" cellspacing="0">
										<tr>
											<td valign="middle" class="column" nowrap>
												&nbsp;<Forums:CurrentPage Cssclass="columnText" id="Currentpage" runat="server" />
											</td>
											<td valign="middle" align="right" class="column" nowrap>
												<Forums:Pager id="Pager" runat="server" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						</asp:Panel>
					</td> 
				</tr>
			</table>
			<div style="padding-top: 12px; padding-bottom: 12px;">
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td align="left" valign="top">
						<table class="tableBorder" cellpadding="3" cellspacing="1" width="100%">
							<tr>
								<td class="column" colspan="2"><%= ResourceManager.GetString("ViewThreads_ForumOptions")%></td>
							</tr>
							<tr>
								<td class="fh">
									<table cellpadding="2" cellspacing="1" width="100%">
									<tr>
										<td align="left" class="fh2" nowrap>
											<%= ResourceManager.GetString("ViewThreads_SortedBy")%>
										</td>
										<td align="left" class="fh2" width="100%">
											<Forums:ThreadSortDropDownList id="SortThreads" runat="server" /><%= ResourceManager.GetString("ViewThreads_In")%><forums:SortOrderDropDownList id="SortOrder" runat="server" /><%= ResourceManager.GetString("ViewThreads_OrderFrom")%>
										</td>
									</tr>
									<tr>
										<td align="left" class="fh2" nowrap>
											<%= ResourceManager.GetString("ViewThreads_FilterByDate")%>
										</td>
										<td align="left" class="fh2" width="100%">
											<Forums:DateFilter id="DateFilter" runat="server" AddText="false" AppendLineBreak="false" />
										</td>
									</tr>
									<tr>
										<td align="left" class="fh2" nowrap>
											<%= ResourceManager.GetString("ViewThreads_FilterByTopic")%>
										</td>
										<td align="left" class="fh2" width="100%">	
											<Forums:HideReadPostsDropDownList runat="server" ID="HideReadPosts" />
											<Forums:FilterUsersDropDownList runat="server" ID="FilterUsers" />
										</td>
									</tr>
					<!-- Future version
									<tr>
										<td align="left" class="fh" nowrap>
											<%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewThreads_FilterByIgnore")%>
										</td>
										<td align="left" class="fh2" width="100%">&nbsp;	
										</td>
									</tr>
					-->
									<tr>
										<td align="left" class="fh2" nowrap>
											<%= ResourceManager.GetString("EmailNotificationDropDownList_When") %>
										</td>
										<td align="left" class="fh2" width="100%">
											<Forums:EmailNotificationDropDownList ID="ForumTrackingDDL" Runat="server" />
										</td>
									</tr>
									<tr>
										<td align="right" class="fh2" colspan="2" nowrap>
											<asp:Button id="SortThreadsButton" runat="server" />
											<asp:Button id="RememberSettingsButton" runat="server" />
											<Forums:MarkAllRead runat="server" ID="Markallread1" NAME="Markallread1" />
										</td>
									</tr>
									</table>
								</td>
							</tr>		
						</table>
					</td>
					<td align="right" valign="top" class="txt4" nowrap>
						<Forums:JumpDropDownList runat="server" />
						<br /><br />
						<Forums:UserPermissions runat="server" ID="Userpermissions1" />
					</td>
				</tr>
			</table>
			</div>
			<table cellpadding="2" class="tableBorder" cellspacing="1" width="100%">
				<tr>
					<td align="left" valign="top" class="column" colspan="2">
					<%= ResourceManager.GetString("ViewThreads_ForumStats") %>
					</td>
				</tr>
				<tr>
					<td class="fh">
						<table cellpadding="2" cellspacing="1" width="100%">
						<tr>
							<td class="fh2">
								<forums:ForumModerators runat="server" />
							</td>
						</tr>
						<tr>
							<td class="fh2">
								<forums:ActiveUsers GuestMode="true" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="fh2"><forums:ActiveUsers runat="server" />
							</td>
						</tr>
						</table>
					</td>
				</tr>
			</table>
			<div align="right"><Forums:RssLink runat="server" /></div>
		</td>
	</tr>
</table>
<!-- ********* View-Threads.ascx:End ************* //-->	