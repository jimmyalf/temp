<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Page Language="c#" MasterPageFile="~/Components/Forum/ForumMain.Master" CodeBehind="SiteSettings.aspx.cs" AutoEventWireup="false" Inherits="Spinit.Wpc.Forum.Presentation.Components.Forum.SiteSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Configuration<br />
    These settings allow you to control various options for the forums.<br />
    <fieldset>
	    <legend>Manage Domain</legend>		
	    <div class="formItem">
	        <asp:Label ID="lblApplication" runat="server" AssociatedControlID="Domain" SkinId="labelLong" Text="Application" /><br />
	        <asp:Label ID="lblApplicationDescr" runat="server" AssociatedControlID="Domain" SkinId="labelLong" Text="Choose the application you wish to administer." />
	        <forums:domaindropdownlist id="Domain" runat="server"></forums:domaindropdownlist>&nbsp;
						<asp:button id="SelectDomain" runat="server" Text='Select'>
						</asp:button>
	    </div>
    </fieldset>		
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2">Manage Domain</TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B>Application</B><BR>
						Choose the application you wish to administer.
					</TD>
					<TD class="fh" noWrap width="420"></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" height="95" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_DisableForum")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_DisableForum")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_DisableForum_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="Disabled" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Title")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Name")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Name_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<asp:textbox id="SiteName" runat="server" columns="55" MaxLength="512"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f" vAlign="top"><B><% = ResourceManager.GetString("Admin_SiteSettings_Description")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Description_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="SiteDescription" runat="server" columns="55" rows="3" TextMode="Multiline"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_TimeZone")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_TimeZone_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<forums:timezonedropdownlist id="Timezone" runat="server"></forums:timezonedropdownlist></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_ThreadsPerPage")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_ThreadsPerPage_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="ThreadsPerPage" runat="Server" MaxLength="4"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_PostPerPage")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_PostPerPage_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="PostsPerPage" runat="Server" MaxLength="4"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_EnablePostPreviewPopup")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_EnablePostPreviewPopup_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnablePostPreviewPopup" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
<!--				<TR>
					<TD class="f" width="100%"><B>< % = ResourceManager.GetString("Admin_SiteSettings_AllowAutoRegistration")% ></B><BR>
						< % = ResourceManager.GetString("Admin_SiteSettings_AllowAutoRegistration_Descr")% >
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="AllowAutoRegistration" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
-->				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_StripDomainName")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_StripDomainName_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="StripDomainName" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Contact")%></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Contact_AdminEmail")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Contact_AdminEmail_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="AdminEmailAddress" runat="server" MaxLength="128"></asp:textbox>
						<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" CssClass="validationWarning" ControlToValidate="AdminEmailAddress"
							ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
							<% = ResourceManager.GetString("Admin_SiteSettings_Contact_InvalidEmail")%>
						</asp:regularexpressionvalidator></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Contact_CompanyName")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Contact_CompanyName_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<asp:textbox id="CompanyName" runat="server" MaxLength="128"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Contact_Company_Email")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Contact_Company_Email_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="CompanyEmailAddress" runat="server" MaxLength="128"></asp:textbox>
						<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" CssClass="validationWarning" ControlToValidate="CompanyEmailAddress"
							ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
							<% = ResourceManager.GetString("Admin_SiteSettings_Contact_InvalidEmail")%>
						</asp:regularexpressionvalidator></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Contact_Fax")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Contact_Fax_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="CompanyFaxNumber" runat="server" MaxLength="128"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Contact_Address")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Contact_Address_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="CompanyAddress" runat="server" MaxLength="256"></asp:textbox></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Menu")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Menu_Forum")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Menu_Forum_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="DisplayForumDescription" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR> <!--				<TR>
					<TD class="f"><B>< % = ResourceManager.GetString("Admin_SiteSettings_Menu_Birthdays")% ><FONT color="red">(NOT 
								ENABLED IN BETA)</FONT></B>
						<BR>
						< % = ResourceManager.GetString("Admin_SiteSettings_Menu_Birthdays_Descr")% >
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="DisplayBirthdays" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
-->
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Menu_CurrentTime")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Menu_CurrentTime_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="DisplayCurrentTime" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Menu_WhosOnline")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Menu_WhosOnline_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="DisplayWhoIsOnline" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Menu_Statistics")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Menu_Statistics_Descr")%>
					</TD>
					<TD class="fh">
						<forums:yesnoradiobuttonlist id="DisplayStatistics" runat="server" CssClass="txt1" RepeatColumns="2"></forums:yesnoradiobuttonlist></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_RSS")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_RSS_Feeds")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_RSS_Feeds_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableForumRSS" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_RSS_Threads")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_RSS_Threads_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<asp:textbox id="RSSDefaultThreadsPerFeed" runat="server" MaxLength="3"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_RSS_MaxThreads")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_RSS_MaxThreads_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<asp:textbox id="RSSMaxThreadsPerFeed" runat="server" MaxLength="3"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_RSS_Cache")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_RSS_Cache_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="RssCacheWindowInMinutes" runat="server" MaxLength="256"></asp:textbox></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_List")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_List_Display")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_List_Display_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="PublicMemberList" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_List_Searching")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_List_Searching_Descr")%>
					</TD>
					<TD class="fh">
						<forums:yesnoradiobuttonlist id="MemberListAdvancedSearch" runat="server" CssClass="txt1" RepeatColumns="2"></forums:yesnoradiobuttonlist></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_List_MembersPerPage")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_List_MembersPerPage_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="MemberListPageSize" runat="server" MaxLength="4"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_List_TopPosters")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_List_TopPosters_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="MemberListTopPostersToDisplay" runat="server" MaxLength="3"></asp:textbox></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Posting")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_Dups")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_Dups_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="AllowDuplicatePosts" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_DupsInterval")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_DupsInterval_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<asp:textbox id="DuplicatePostIntervalInMinutes" runat="Server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_Emoticons")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_Emoticons_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="EnableEmoticons" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_Popular")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_Popular_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="PopularPostThresholdPosts" runat="Server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_Popular_View")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_Popular_View_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="PopularPostThresholdViews" runat="Server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_Popular_Age")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_Popular_Age_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="PopularPostThresholdDays" runat="Server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_Pruning")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_Pruning_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="AutoPostDelete" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Posting_UserPruning")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Posting_UserPruning_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="AutoUserDelete" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Attachments")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Attachments_Allow")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Attachments_Allow_Desc")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableAttachments" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Attachments_Types")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Attachments_Types_Desc")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="AllowedAttachmentTypes" runat="server"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Attachments_MaxSize")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Attachments_MaxSize_Desc")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="MaxAttachmentSize" runat="server"></asp:textbox></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Editing")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Editing_Notes")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Editing_Notes_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<forums:yesnoradiobuttonlist id="DisplayEditNotes" runat="server" CssClass="txt1" RepeatColumns="2"></forums:yesnoradiobuttonlist></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Editing_Age")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Editing_Age_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="PostEditBodyAgeInMinutes" runat="server"></asp:textbox></TD>
				</TR> <!--
			<tr>
				<td class="f">
					<b>Edit Post Title Age Limit (in minutes)<font color="red"> (NOT ENABLED IN BETA)</font></b>
					<br>
					When enabled enables a post title to be edited until the post has aged a 
					specified number of minutes (set to 0 to allow infinite editing).
				</td>
				<td class="fh">
					<asp:TextBox id="PostEditTitleAgeInMinutes" runat="server" />
				</td>
			</tr>
	--></TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Flooding")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Flooding_Check")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Flooding_Check_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableFloodInterval" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Flooding_Time")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Flooding_Time_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="PostInterval" runat="Server"></asp:textbox></TD>
				</TR>
			</TABLE> <!--				
			<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_PostReporting")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B>< % = ResourceManager.GetString("Admin_SiteSettings_PostReporting_Allow")% ><FONT color="red">(NOT 
								ENABLED IN BETA)</FONT></B><BR>
						< % = ResourceManager.GetString("Admin_SiteSettings_PostReporting_Desc")% >
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnablePostReporting" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B>< % = ResourceManager.GetString("Admin_SiteSettings_PostReporting_AllowAnon")% ><FONT color="red">(NOT 
								ENABLED IN BETA)</FONT></B><BR>
						< % = ResourceManager.GetString("Admin_SiteSettings_PostReporting_AllowAnon_Descr")% >
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableAnonymousReporting" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>				
				<TR>
					<TD class="f" width="100%"><B>< % = ResourceManager.GetString("Admin_SiteSettings_PostReporting_ToForum")% ><FONT color="red">(NOT 
								ENABLED IN BETA)</FONT></B><BR>
						< % = ResourceManager.GetString("Admin_SiteSettings_PostReporting_ToForum_Descr")% >
					</TD>
					<TD class="fh">
						<asp:textbox id="ReportingForum" runat="server"></asp:textbox></TD>
				</TR>
			</TABLE>			
-->
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_IP")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_IP_Enable")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_IP_Enable_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableTrackPostsByIP" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_IP_Display")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_IP_Display_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="DisplayPostIP" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_IP_AdminOnly")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_IP_AdminOnly_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="DisplayPostIPAdminsModeratorsOnly" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_DateTime")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_DateTime_Date")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_DateTime_Date_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<forums:dateformatdropdownlist id="DateFormat" runat="server"></forums:dateformatdropdownlist></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_DateTime_Time")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_DateTime_Time_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="TimeFormat" runat="server"></asp:textbox></TD>
				</TR> <!--				<TR>
					<TD class="f"><B>< % = ResourceManager.GetString("Admin_SiteSettings_DateTime_Joined")% ><FONT color="red">(NOT 
								ENABLED IN BETA)</FONT></B>
						<BR>
						< % = ResourceManager.GetString("Admin_SiteSettings_DateTime_Joined_Descr")% >
					</TD>
					<TD class="fh">
						<asp:textbox id="TimeFormatUserJoined" runat="server"></asp:textbox></TD>
				</TR>
--></TABLE>
<!--		
			<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2">< % = ResourceManager.GetString("Admin_SiteSettings_URL")% ></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B>< % = ResourceManager.GetString("Admin_SiteSettings_URL_Friendly")% ></B><BR>
						< % = ResourceManager.GetString("Admin_SiteSettings_URL_Friendly_Descr")% >
						<P>http://localhost/Forums/5/ShowForum.aspx
							<BR>
							vs.
							<BR>
							http://localhost/Forums/ShowForums.aspx?ForumID=5</P>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableSearchFriendlyURLs" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
			</TABLE>
-->
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Cookie")%></TD>
				</TR>
				<TR>
					<TD class="f" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_SubTitle")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Enabled")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Enabled_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableRolesCookie" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Track")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Track_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="EnableAnonymousTracking" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Name")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Name_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="RolesCookieName" runat="server" MaxLength="64"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Expire")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Expire_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="RolesCookieExpiration" runat="server" MaxLength="6"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Anon_Name")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Anon_Name_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="AnonymousCookieName" runat="server" MaxLength="64"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Anon_Expire")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Anon_Expire_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="AnonymousCookieExpiration" runat="server" MaxLength="6"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Domain")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Cookie_Domain_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="CookieDomain" runat="server" MaxLength="100"></asp:textbox></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_Anon")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_Anon_Allow")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Anon_Allow_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="AnonymousPosting" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_Anon_Window")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_Anon_Window_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="AnonymousUserOnlineTimeWindow" runat="server" MaxLength="64"></asp:textbox></TD>
				</TR>
			</TABLE>
		<P>
			<TABLE class="tableBorder" cellSpacing="1" cellPadding="4" width="100%">
				<TR>
					<TD class="column" colSpan="2"><% = ResourceManager.GetString("Admin_SiteSettings_SMTP")%></TD>
				</TR>
				<TR>
					<TD class="f" width="100%"><B><% = ResourceManager.GetString("Admin_SiteSettings_SMTP_Enable")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_SMTP_Enable_Descr")%>
					</TD>
					<TD class="fh" noWrap width="420">
						<Forums:YesNoRadioButtonList id="EnableEmail" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_SMTP_Server")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_SMTP_Server_Descr")%>
					</TD>
					<TD class="fh">
						<asp:textbox id="SmtpServer" runat="server" MaxLength="64"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_SMTP_NeedsLogin")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_SMTP_NeedsLogin_Descr")%>
					</TD>
					<TD class="fh">
						<Forums:YesNoRadioButtonList id="SmtpServerRequiredLogin" runat="server" CssClass="txt1" RepeatColumns="2"></Forums:YesNoRadioButtonList></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_SMTP_UserName")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_SMTP_UserName_Descr")%>
					</TD>
					<TD class="fh">
						<asp:TextBox id="SmtpServerUserName" runat="server" MaxLength="64"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD class="f"><B><% = ResourceManager.GetString("Admin_SiteSettings_SMTP_Password")%></B><BR>
						<% = ResourceManager.GetString("Admin_SiteSettings_SMTP_Password_Descr")%>
					</TD>
					<TD class="fh">
						<asp:TextBox id="SmtpServerPassword" runat="server" MaxLength="64"></asp:TextBox></TD>
				</TR>
			</TABLE>
			<TABLE width="100%">
				<TR>
					<TD class="txt3" align="right">
						<asp:button id="Save" runat="server" width="60px"></asp:button>&nbsp;
						<asp:button id="Reset" runat="server" width="60px"></asp:button></TD>
				</TR>
			</TABLE>
		</P>
</asp:Content>
