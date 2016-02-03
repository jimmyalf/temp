<%@ Control Language="C#" %>
<%@ Import Namespace="Spinit.Wpc.Forum" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Enumerations" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<!-- View-PostFlatView.Header.Start -->
<div style="padding-left: 12px; padding-right: 12px;">
<table width="100%" cellpadding="0" border="0">
	<tr>
		<td>
			<table align="center" width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td valign="top">
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table class="tableBorder" width="100%" cellspacing="1" cellpadding="3">
										<tr>
											<td width="100%" class="column"><asp:Label ID="ForumName" Runat="server" /></td>
										</tr>										
										<tr>
											<td class="fh">
												<table width="100%" cellspacing="0" border="0" cellpadding="0">
													<tr>    
														<td align="left" valign="middle">
															<table width="100%" cellpadding="4" cellspacing="0">
																<tr>
																	<td align="center" nowrap>
																		<asp:Label CssClass="forumName" ID="PostSubject" Runat="server" />										
																		<br /><br />
																		<asp:Label CssClass="forumThread" ID="ThreadStats" Runat="server" />
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
				</tr>
			</table>	
		</td>
	</tr>
</table>
</div>
<!-- View-PostFlatView.Header.End -->	
<!-- View-PostFlatView.MainCentent.Start -->
<div style="padding-left: 12px; padding-right: 12px;">
<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td>
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr>
					<td align="left" valign="bottom" colspan="2">
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td class="txt4Bold">
									&nbsp;<Forums:BreadCrumb ShowHome="true" runat="server" />
								</td>
								<td align="right" valign="bottom" class="txt4" nowrap>
									<Forums:SearchRedirect SkinFileName="Skin-SearchForum.ascx" ID="SearchRedirect" runat="server" />
								</td>
							</tr>
						</table>					
					</td>
				</tr>	
			</table>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td align="left" width="10px">
						<Forums:ForumImageButton ButtonType="NewPost" runat="server" ID="NewPostButtonUp" />
					</td>                 
					<td align="right" valign="middle" class="txt4">
						<forums:RatePost id="RatePost" runat="server" />
					</td>
				</tr>
			</table>
			<table class="tableBorder" height="25" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td colspan="2" class="column">
						<table cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td valign="middle" align="left">
									&nbsp;<a target="_blank" href="<%=Globals.GetSiteUrls().PrintPost( ForumContext.Current.PostID )%>" class="columnText"><img border="0" src='<%= Globals.GetSkinPath() + "/images/icon_print.gif"%>'>Printable Version</a>&nbsp;&nbsp;&nbsp;<forums:ThreadSubscribeLinkButton runat="server"/>
								</td>
								<td valign="middle" align="right">
									<asp:HyperLink Cssclass="columnText" runat="server" id="PrevThread" /> <asp:Literal id="PrevNextSpacer" runat="server" /> <asp:HyperLink runat="server" Cssclass="columnText" id="NextThread" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
</div>
<!-- ********* Repeater.Start ************* //-->							
<asp:Repeater ID="PostRepeater" EnableViewState="false" Runat="server">      
	<ItemTemplate>
	<!-- ********* ItemTemplate.Start ************* //-->	
<div style="padding-left: 12px; padding-right: 12px; padding-bottom: 12px; ">
<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td>
			<table class="tableBorder" cellpadding="0" cellspacing="0" width="100%">
				<tr>			
					<td colspan="2">
						<table align="left" width="100%" cellpadding="0" cellspacing="0">	
		<!-- ********* ItemTemplate.PostHeader ************* //-->								
							<tr>
								<td class="threadSeparator" colspan="2" valign="middle">
									<a name='<%# DataBinder.Eval (Container.DataItem, "PostID" ) %>'></a>
									<span class="dateText">&nbsp;<img border="0" src="<%= Globals.GetSkinPath() + "/images/icon_post_show.gif"%>">&nbsp;<%# Formatter.FormatDatePost (((Post) Container.DataItem).PostDate) %></span>
								</td>
							</tr>                   
		<!-- ********* ItemTemplate.PostHeaderEnd ************* //-->
		<!-- ********* ItemTemplate.BodyRowStart ************* //-->
							<tr>
		<!-- ********* ItemTemplate.BodyColUserInfoStart (2 rows:start of rowspan) ************* //-->						
								<td class="fh" rowspan="2" align="center" valign="top" width="180" nowrap>
									<div style="padding-top: 4px;">
									<Forums:UserOnlineStatus runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
									<b><%# Formatter.FormatUsername ( ((User) DataBinder.Eval (Container.DataItem, "User" )).UserID, DataBinder.Eval (Container.DataItem, "Username").ToString() ) %></b>
									<Forums:UserAvatar runat="server" Border="1" PadImage="True" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
									<span class="txt2">
										<br />
										<Forums:PostIcons runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
										<Forums:UserAttribute FormatString="<br />{0}" Attribute="Joined" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
										<Forums:UserAttribute FormatString="<br />{0}" Attribute="Location" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
										<Forums:UserAttribute FormatString="<br />{0}" Attribute="Posts" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
										<Forums:RoleIcons runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
									</span>
									</span>
								</td>
		<!-- ********* ItemTemplate.BodyColUserInfoEnd (/2 rows:end of rowspan) ************* //-->
		<!-- ********* ItemTemplate.BodyColPostHeaderStart (1 row) ************* //-->	
								<td class="f" width="100%" valign="top">
									<table width="100%" cellpadding="2" cellspacing="0">
										<tr>
											<td align="left" valign="bottom" nowrap>
												<span class="txt5">
													<%# Formatter.PostEmoticon( (int) DataBinder.Eval (Container.DataItem, "EmoticonID" ) ) %><b><%# DataBinder.Eval (Container.DataItem, "Subject" ) %></b><br />
													<Forums:PostAttachment runat="server" Post='<%# Container.DataItem %>' />
												</span>
											</td>
											<td align="right" valign="top" class="txt4">
												<Forums:ForumImageButton ButtonType="Reply" Post='<%# Container.DataItem %>' runat="server" ID="ReplyButton" /> <Forums:ForumImageButton runat="server" Post='<%# Container.DataItem %>' ButtonType="Quote" id="QuoteButton"/> <Forums:ForumImageButton runat="server" Post='<%# Container.DataItem %>' ButtonType="Delete" id="DeleteButton"/> <Forums:ForumImageButton runat="server" Post='<%# Container.DataItem %>' ButtonType="Edit" id="EditButton"/> 
												<Forums:ModerationMenu Post='<%# (Post) Container.DataItem %>' runat="server" SkinFileName="Moderation\Skin-ModeratePost.ascx" />
											</td>
										</tr>
									</table>
								</td>						
							</tr>
		<!-- ********* ItemTemplate.BodyColPostHeaderEnd (/1 row) ************* //-->								
		<!-- ********* ItemTemplate.BodyColPostBodyStart (1 row) ************* //-->								
							<tr>						
								<td height="100" valign="top" class="fh3a">
									<table align="left" width="100%" cellpadding="0" cellspacing="2">
										<tr>
											<td rowspan="2" width="1">
												<img height="250" width="1" src="<%# Globals.GetSkinPath() + "/images/spacer.gif"%>">
											</td>	
											<td colspan="3" width="100%" valign="top" class="txt4" style="padding-top: 4px;">
												<table align="left" width="90%" cellpadding="0" cellspacing="0">
													<tr>
														<td class="txt4" align="left" valign="top">											
															<Forums:PostView Post='<%# (Post) Container.DataItem %>' runat="server" />
															<br />											
														</td>
													</tr>
												</table>
											</td>			
										</tr>				
										<tr>
											<td align="left" valign="bottom" width="100%">
		<!-- EAD: These buttons are to be removed soon and
			be replaced with small-icons that will appear
			under the user's avatar section to clean up
			the overall postview.  Same with the report/ip info. //-->																					
												<%-- <Forums:PostImageButtons runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' ID="Postimagebuttons1"/>--%>
											</td>
											<td width="*" valign="bottom" align="right" class="txt5" nowrap>
												<%-- <%# Formatter.PostIPAddress ( (Post) Container.DataItem ) %>&nbsp;&nbsp;--%>
											</td>
											<td width="10" valign="bottom" align="right" class="txt5" nowrap>
												<% if ( !Users.GetUser().IsAnonymous ) { %>
												<a href="<%=Globals.GetSiteUrls().Report%>&ReportPostID=<%# ((Post) Container.DataItem).PostID %>"><%# ResourceManager.GetString("Report")%></a>&nbsp;
												<% } %>
											</td>												
										</tr>
									</table>									
								</td>
							</tr>
		<!-- ********* ItemTemplate.BodyColPostBodyEnd (/1 row) ************* //-->
						</table>	
					</td>
				</tr>							
			</table>
		</td>
	</tr>
</table>
</div>
	<!-- ********* ItemTemplate.End ************* //-->										
	</ItemTemplate>
	<AlternatingItemTemplate>
	<!-- ********* AlternatingItemTemplate.Start ************* //-->	
<div style="padding-left: 12px; padding-right: 12px; padding-bottom: 12px; ">
<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td>
			<table class="tableBorder" cellpadding="0" cellspacing="0" width="100%">
				<tr>			
					<td colspan="2">
						<table align="left" width="100%" cellpadding="0" cellspacing="0">	
		<!-- ********* AlternatingItemTemplate.PostHeader ************* //-->								
							<tr>
								<td class="threadSeparator" colspan="2" valign="middle">
									<a name='<%# DataBinder.Eval (Container.DataItem, "PostID" ) %>'></a>
									<span class="dateText">&nbsp;<img border="0" src="<%= Globals.GetSkinPath() + "/images/icon_post_show.gif"%>">&nbsp;<%# Formatter.FormatDatePost (((Post) Container.DataItem).PostDate) %></span>
								</td>
							</tr>                   
		<!-- ********* AlternatingItemTemplate.PostHeaderEnd ************* //-->
		<!-- ********* AlternatingItemTemplate.BodyRowStart ************* //-->
							<tr>
		<!-- ********* AlternatingItemTemplate.BodyColUserInfoStart (2 rows:start of rowspan) ************* //-->						
								<td class="fh" rowspan="2" align="center" valign="top" width="180" nowrap>
									<div style="padding-top: 4px;">
									<Forums:UserOnlineStatus runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
									<b><%# Formatter.FormatUsername ( ((User) DataBinder.Eval (Container.DataItem, "User" )).UserID, DataBinder.Eval (Container.DataItem, "Username").ToString() ) %></b>
									<Forums:UserAvatar runat="server" Border="1" PadImage="True" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
									<span class="txt2">
										<br />
										<Forums:PostIcons runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />									
										<Forums:UserAttribute FormatString="<br />{0}" Attribute="Joined" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
										<Forums:UserAttribute FormatString="<br />{0}" Attribute="Location" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
										<Forums:UserAttribute FormatString="<br />{0}" Attribute="Posts" runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
										<Forums:RoleIcons runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' />
									</span>
									</div>
								</td>
		<!-- ********* AlternatingItemTemplate.BodyColUserInfoEnd (/2 rows:end of rowspan) ************* //-->
		<!-- ********* AlternatingItemTemplate.BodyColPostHeaderStart (1 row) ************* //-->	
								<td class="f" width="100%" valign="top">
									<table width="100%" cellpadding="2" cellspacing="0">
										<tr>
											<td align="left" valign="bottom" nowrap>
												<span class="txt5">
													<%# Formatter.PostEmoticon( (int) DataBinder.Eval (Container.DataItem, "EmoticonID" ) ) %><b><%# DataBinder.Eval (Container.DataItem, "Subject" ) %></b><br />
													<Forums:PostAttachment runat="server" Post='<%# Container.DataItem %>' />
												</span>
											</td>
											<td align="right" valign="top" class="txt4">
												<Forums:ForumImageButton ButtonType="Reply" Post='<%# Container.DataItem %>' runat="server" ID="ReplyButton" /> <Forums:ForumImageButton runat="server" Post='<%# Container.DataItem %>' ButtonType="Quote" id="QuoteButton"/> <Forums:ForumImageButton runat="server" Post='<%# Container.DataItem %>' ButtonType="Delete" id="DeleteButton"/> <Forums:ForumImageButton runat="server" Post='<%# Container.DataItem %>' ButtonType="Edit" id="EditButton"/> 
												<Forums:ModerationMenu Post='<%# (Post) Container.DataItem %>' runat="server" SkinFileName="Moderation\Skin-ModeratePost.ascx" />
											</td>
										</tr>
									</table>
								</td>						
							</tr>
		<!-- ********* AlternatingItemTemplate.BodyColPostHeaderEnd (/1 row) ************* //-->								
		<!-- ********* AlternatingItemTemplate.BodyColPostBodyStart (1 row) ************* //-->								
							<tr>						
								<td height="100" valign="top" class="fh3">
									<table align="left" width="100%" cellpadding="0" cellspacing="2">
										<tr>
											<td rowspan="2" width="1">
												<img height="250" width="1" src="<%# Globals.GetSkinPath() + "/images/spacer.gif"%>">
											</td>	
											<td colspan="3" width="100%" valign="top" style="padding-top: 4px;">
												<table align="left" width="90%" cellpadding="0" cellspacing="0">
													<tr>
														<td class="txt4" align="left" valign="top">
															<Forums:PostView Post='<%# (Post) Container.DataItem %>' runat="server" ID="Postview1"/>
															<br />	
														</td>
													</tr>
												</table>								
											</td>			
										</tr>				
										<tr>
											<td align="left" valign="bottom" width="100%">
		<!-- EAD: These buttons are to be removed soon and
			be replaced with small-icons that will appear
			under the user's avatar section to clean up
			the overall postview.  Same with the report/ip info. //-->																					
												<%-- <Forums:PostImageButtons runat="server" User='<%# (User) DataBinder.Eval (Container.DataItem, "User" ) %>' ID="Postimagebuttons1"/>--%>
											</td>
											<td width="*" valign="bottom" align="right" class="txt5" nowrap>
												<%-- <%# Formatter.PostIPAddress ( (Post) Container.DataItem ) %>&nbsp;&nbsp; --%>
											</td>
											<td width="10" valign="bottom" align="right" class="txt5" nowrap>
												<% if ( !Users.GetUser().IsAnonymous ) { %>
												<a href="<%=Globals.GetSiteUrls().Report%>&ReportPostID=<%# ((Post) Container.DataItem).PostID %>"><%# ResourceManager.GetString("Report")%></a>&nbsp;
												<% } %>
											</td>												
										</tr>
									</table>									
								</td>
							</tr>
		<!-- ********* AlternatingItemTemplate.BodyColPostBodyEnd (/1 row) ************* //-->
						</table>	
					</td>
				</tr>							
			</table>
		</td>
	</tr>
</table>
</div>
	<!-- ********* AlternatingItemTemplate.End ************* //-->			
	</AlternatingItemTemplate>
</asp:Repeater> 
<!-- ********* Repeater.End ************* //-->	
<div style="padding-left: 12px; padding-right: 12px; padding-bottom: 12px;">
<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td valign="top" align="left" width="10px">
			<Forums:ForumImageButton ButtonType="NewPost" runat="server" ID="NewPostButtonDown" />
		</td>	
		<td valign="top">
<!-- TODO (EAD): Write a <asp:Panel> with an id to turn off if no paging is needed.  //-->		
			<table align="right" class="tableBorder" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table width="100%" cellpadding="2" cellspacing="0">
							<tr>
								<td valign="middle" class="column" nowrap>
									&nbsp;<Forums:CurrentPage Cssclass="columnText" id="CurrentPage" runat="server" />
								</td>
								<td valign="middle" align="right" class="column" nowrap>
									<Forums:Pager id="Pager" runat="server" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
<!-- END TODO  //-->			
		</td> 
	</tr>
</table>
<%--
<p align="center" class="txt2Bold" runat="server" id="QuickReplyBlock">
Quick reply<br>
<asp:TextBox id="QuickReply" runat="server" TextMode="MultiLine" Width="548px" Height="104px"></asp:TextBox>
<asp:Button Runat="server" id="PostButton"></asp:Button>
<!--asp:RequiredFieldValidator id="postBodyValidator"  ErrorMessage="RequiredFieldValidator" ControlToValidate="QuickReply"></asp:RequiredFieldValidator-->
</p>
--%>
</div>
<div style="padding-left: 12px; padding-right: 12px; padding-bottom: 12px;">
<table cellpadding="0" cellspacing="2" width="100%">
	<tr>
		<td valign="top" align="left" class="txt4">
			<Forums:BreadCrumb ShowHome="true" runat="server" />
		</td>
		<td valign="top" align="right" class="txt5" nowrap>
			<Forums:JumpDropDownList CssClass="txt1" runat="server" />
			<br /><br />
			<Forums:UserPermissions runat="server" />
		</td>
	</tr>
</table>
</div>
