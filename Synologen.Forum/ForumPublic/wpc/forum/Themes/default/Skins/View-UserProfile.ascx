<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<!-- ********* View-UserProfile.ascx:Start ************* //-->	
<table align="center" width="75%" cellspacing="12" cellpadding="0" border="0">
<!-- View-UserProfile.Header.Start -->
	<tr>
		<td>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td valign="top" width="*" style="padding-right: 12px;">
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table class="tableBorder" width="100%" cellspacing="1" cellpadding="3">
										<tr>
											<td width="100%" class="column"><%= ResourceManager.GetString("ViewUserProfile_Title")%></td>
										</tr>										
										<tr>
											<td class="fh">
												<table width="100%" cellspacing="0" border="0" cellpadding="0">
													<tr>    
														<td align="left" valign="middle">
															<table width="100%" cellpadding="4" cellspacing="0">
																<tr>
																	<td align="center">
																		<span class="forumName"><asp:Literal ID="UserName" Runat="server" /></span>
																		<br /><br />
																		&nbsp;&nbsp;&nbsp;<asp:HyperLink Runat="server" ID="PrivateMessageButton" Visible="False" />
																		&nbsp;&nbsp;&nbsp;<asp:HyperLink Runat="server" ID="EmailUserButton" Visible="False" />
																		&nbsp;&nbsp;&nbsp;<asp:HyperLink Runat="server" ID="SearchForPostsByUserButton" Visible="False" />
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
<!-- View-UserProfile.Header.End -->	
<!-- View-UserProfile.Body.Start -->	
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
									<Forums:SearchRedirect SkinFileName="Skin-SearchForum.ascx"  />
								</td>
							</tr>
						</table>		
						</div>				
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<table class="tableBorder" width="100%" cellpadding="3" cellspacing="1">
				<tr>
					<td class="column" colspan="2">
						<%= ResourceManager.GetString("ViewUserProfile_About")%>
					</td>
				</tr>
				<tr id="ManageUser" visible="false" runat="server">
					<td class="f" width="35%">
						<b><%= ResourceManager.GetString("Admin_CommonTerms_Manage")%></b>
					</td>
					<td class="fh" width="65%">
						<a href='<%=Globals.GetSiteUrls().AdminUserEdit( ForumContext.Current.UserID ) %>'><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("Admin_CommonTerms_Manage")%></a>            
					</td>
				</tr>
				<tr>
					<td class="f" width="35%">
						<b><%= ResourceManager.GetString("ViewUserProfile_Joined")%></b>
						<br />
						<%= ResourceManager.GetString("ViewUserProfile_JoinedDescription")%>
					</td>
					<td class="fh" width="65%">
						<asp:Literal id="JoinedDate" runat="server" />
					</td>
				</tr>
			    
				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_LastLogin")%></b>
						<br />
						<%= ResourceManager.GetString("ViewUserProfile_LastLoginDescription")%>
					</td>
					<td class="fh">
						<asp:Literal id="LastLoginDate" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_LastVisit")%></b>
						<br />
						<%= ResourceManager.GetString("ViewUserProfile_LastVisitDescription")%>
					</td>
					<td class="fh">
						<asp:Literal id="LastActivityDate" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_Timezone")%></b>
						<br />
						<%= ResourceManager.GetString("ViewUserProfile_TimezoneDescription")%>
					</td>
					<td class="fh">
						<asp:Literal id="TimeZone" runat="server" /> GMT
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_Location")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="Location" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_Occupation")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="Occupation" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_Interests")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="Interests" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_WebAddress")%></b>
					</td>
					<td class="fh">
						<asp:HyperLink id="WebURL" Target="_blank" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_BlogAddress")%></b>
					</td>
					<td class="fh">
						<asp:HyperLink id="BlogURL" Target="_blank" runat="server" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<table class="tableBorder" width="100%" cellpadding="3" cellspacing="1">
				<tr>
					<td class="column" colspan="2">
						<%= ResourceManager.GetString("ViewUserProfile_AvatarPostSignature")%>
					</td>
				</tr>
			    
				<tr>
					<td class="f" width="35%">
						<b><%= ResourceManager.GetString("ViewUserProfile_Avatar")%></b>
					</td>
					<td class="fh" width="65%">
						<forums:UserAvatar Visible="False" PadImage="False" Border="1" id="Avatar" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_Theme")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="Skin" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_Signature")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="Signature" runat="server" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<table class="tableBorder" width="100%" cellpadding="3" cellspacing="1">
				<tr>
					<td class="column" colspan="2">
						<%= ResourceManager.GetString("ViewUserProfile_Signature")%>
					</td>
				</tr>
				<tr>
					<td class="f" width="35%">
						<b><%= ResourceManager.GetString("ViewUserProfile_Email")%></b>
					</td>
					<td class="fh" width="65%">
						<asp:HyperLink id="Email" Target="_blank" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_MsnIm")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="MSNIM" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_AolIm")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="AOLIM" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_YahooIm")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="YahooIM" runat="server" />
					</td>
				</tr>

				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_IcqIm")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="ICQ" runat="server" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<table class="tableBorder" width="100%" cellpadding="3" cellspacing="1">
				<tr>
					<td class="column" colspan="2">
						<%= ResourceManager.GetString("ViewUserProfile_PostStats")%>
					</td>
				</tr>
				<tr>
					<td class="f" width="35%">
						<b><%= ResourceManager.GetString("ViewUserProfile_TotalPosts")%></b>
					</td>
					<td class="fh" width="65%">
						<asp:Literal id="TotalPosts" runat="server" />
					</td>
				</tr>
			    
				<tr>
					<td class="f">
						<b><%= ResourceManager.GetString("ViewUserProfile_PostRank")%></b>
					</td>
					<td class="fh">
						<asp:Literal id="Rank" runat="server" />
					</td>
				</tr>
			</table>
			</table>
		</td>
	</tr>
</table>
<!-- View-UserProfile.Body.End -->	