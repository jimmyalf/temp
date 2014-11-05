<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<a name="Top" />
<!-- ********* View-UserProfile.ascx:Start ************* //-->	
<Forums:NavigationMenu DisplayTitle="false" id="Navigationmenu" runat="server" />
<table align="center" width="95%" cellspacing="12" cellpadding="0" border="0">
<!-- View-UserProfile.Header.Start -->
	<tr>
		<td>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td valign="top" width="*" style="padding-right: 12px;">
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table class="tableBorder" align="center" width="85%" cellspacing="1" cellpadding="3">
										<tr>
											<td width="100%" class="column"><%= ResourceManager.GetString("Faq_Title")%></td>
										</tr>										
										<tr>
											<td class="fh">
												<table width="100%" cellspacing="0" border="0" cellpadding="0">
													<tr>    
														<td align="left" valign="middle">
															<table width="100%" cellpadding="4" cellspacing="0">
																<tr>
																	<td align="center">
																		<span class="forumName"><%= ResourceManager.GetString("Faq_Description")%></span>
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
    <tr>
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
    <td colspan="2">

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Frequently Asked Questions
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <b>Registration & Login</b>
      <br>
      <a href="#1">Why do I need to Register?</a>
      <br>
      <a href="#2">How do I Register?</a>
      <br>
      <a href="#3">I have a Username and Password, How do I Login?</a>
      <br>
      <a href="#4">I already logged in, why do I get logged off automatically?</a>
      <br>
      <a href="#5">I forgot my username and/or password.</a>
      <br>
      <a href="#6"> What if I’ve registered but still cannot login?</a>
      <br> 
      <a href="#7">I’ve logged in before, but now can’t login?</a>

      <P>

      <b>User Profile & Settings </b>
      <br>
      <a href="#8">What is a Profile?</a>
      <br> 
      <a href="#9">Why do I want to set my time zone?</a> 
      <br> 
      <a href="#10">How do I add Signature to my Post? 
      <br> 
      <a href="#11">What is an avatar?</a> 
      <br> 
      <a href="#12">How do I set my avatar? 
      <br> 
      <a href="#13">How do I change the language used by the forums?</a> 
      <br> 
      <a href="#14">How do I set the date format?</a>  
      <br> 
      <a href="#15">How do I turn off email-tracking?</a> 
      <br> 
      <a href="#16">What are the other icons/avatars that show up next to users?</a> 
      <br> 
      <a href="#17">Why am I required to login to post, view members, or email other users?</a> 

      <P>

      <b>Privacy & Security</b>
      <br>
      <a href="#18">How do I change my Password?</a> 
      <br>
      <a href="#19">How do I change my Username?</a> 
      <br>
      <a href="#20">How do I change my email address?</a>  
      <br>
      <a href="#21">What Profile settings are required?</a>  
      <br>
      <a href="#22">What if I don’t want my name displayed in the member lists?</a>  

      <P>
      <b>Navigation</b>
      <br>
      <a href="#23">What is a Forum Group?</a> 
      <br>
      <a href="#24">What is a Forum?</a> 
      <br>
      <a href="#25">What is a Thread?</a> 
      <br>
      <a href="#58">What do the Thread icons mean?</a> 
      <br>
      <a href="#26">When I view a Forum I don’t see any Threads/Posts?</a> 
      <br>
      <a href="#27">I just posted a message, how come I don’t see it?</a> 
      <br>
      <a href="#28">What are the different icons next to threads?</a> 
      <br>
      <a href="#29">What is an Announcement Thread?</a> 
      <br>
      <a href="#30">What is a sticky Thread?</a> 
      <br>
      <a href="#31">What is a Locked Thread?</a> 
      <br>
      <a href="#32">Can I sort Threads when viewing a forum?</a> 
      <br>
      <a href="#33">What is the ‘XML’ icon at the bottom of a forum?</a> 
      <br>
      <a href="#34">What is the red/green icon next to a user’s name when viewing a Post?</a> 
      <br>
      <a href="#35">I can’t access a forum I know exists.</a> 

      <P>
      <b>Posting</b>
      <br>
      <a href="#36">Can I use HTML?</a> 
      <br>
      <a href="#37">What is BBCode?</a> 
      <br>
      <a href="#38">Can I add attachments to my posts?</a> 
      <br>
      <a href="#39">What are Emoticons?</a> 
      <br>
      <a href="#40">How do I post a new message to a forum?</a> 
      <br>
      <a href="#41">How do I reply to an existing post?</a> 
      <br>
      <a href="#42">How do I edit my posts?</a> 
      <br>
      <a href="#43">How do I delete my posts?</a> 
      <br>
      <a href="#44">My Post has words replaced with ***?</a> 
      <br>
      <a href="#45">How do I add a signature to my posts?</a> 
      <br>
      <a href="#46">How do I add an avatar to my posts?</a> 

      <P>
      <b>User Groups & Permissions</b>
      <br>
      <a href="#47">What are Permissions?</a> 
      <br>
      <a href="#48">What is an Administrator?</a> 
      <br>
      <a href="#49">What is a Moderator?</a> 
      <br>
      <a href="#50">What is a Forum Role?</a> 
      <br>
      <a href="#51">How do I join a Forum Role or Group?</a> 

      <P>
      <b>Private Messages</b>

      <P>
      <b>Searching</b>

      <P>
      <b>About ASP.NET Forums</b>
      <br>
      <a href="#52">What is the ASP.NET Forums?</a> 
      <br>
      <a href="#53">Who is using the ASP.NET Forums?</a> 
      <br>
      <a href="#54">Who builds the ASP.NET Forums?</a> 
      <br>
      <a href="#55">Where can I get a copy of the ASP.NET Forums?</a> 
      <br>
      <a href="#56">What about the license to use the forums?</a> 
      <br>
      <a href="#57">What do I do with new features I’ve created or bugs I’ve fixed?</a> 

    </td>
  </tr>
</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Registration & Login
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="1"></a><b>Why do I need to Register?</b>
      <br>
      Depending upon how the administrator has configured his/her forums installation you may be required to create an account before posting, and in some cases reading other user’s posts; most configurations will allow for reading messages without requiring registration. The forums also supports anonymous posting and the administrator may configure some forums to allow anonymous/guest posting. To take advantage of all the features the forums offers, such as setting your own avatar, tracking post counts, emailing users, private messages, access private forums, and many other you will need to have an account. It only takes a few seconds to register, and it is recommended you do so.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="2"></a><b>How do I Register?</b>
      <br>
      To create an account you will need to visit the Registration page and complete the form for creating a new account. Here you will specify details such as your login name and email address – depending upon how the forums are configured you may also be asked to specify a password. If you are not asked to specify a password, one will be emailed to you after successfully registering.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="3"></a><b>I have a Username and Password, How do I Login?</b>
      <br>
      After successfully registering you should have a username and password. You can then visit the login page and enter your username and password to login to the forums.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="4"></a><b>I already logged in, why do I get logged off automatically?</b>
      <Br>
      When logging in if you do not check the ‘Remember Me’ option you will be automatically logged off after a administrator-defined length of inactivity, usually 20 minutes. If you would like the forums to always log you in automatically, please check the ‘Remember Me’ checkbox.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="5"></a><b>I forgot my username and/or password.</b>
      <Br>
      If you forgot your username and/or password you can visit the Forget Your Password page and have both your username and a new password emailed to you by entering the email account your registered with. You will be sent a new password since we store your password encrypted and have no way of retrieving the original value. Once you receive your username and new password you can login and change your password.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="6"></a><b>What if I’ve registered but still cannot login?</b>
      <br>
      If you’ve registered and can’t login, check to ensure you have a valid username and password. If you are sure the username and password are valid, but still can’t login you may either require account activation or your account may be on hold. In this case it is best to contact the board administrator(s) or moderator(s).
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="7"></a><b>I’ve logged in before, but now can’t login?</b>
      <br>
      First check to ensure your username and password are correct. If you still can’t login your account has either been put on hold or deleted due to inactivity. Please contact the board administrator(s) or moderator(s).
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td class="f">
    </td>
  </tr>
</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      User Profile & Settings
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="8"></a><b>What is a Profile?</b>
      <br>
      A profile is information about your account that controls how you view information in the forums. This includes details about posts you’ve contributed to, personal information you wish to share such as your web address or weblog address, as well as setting that control how you interact with the forums such as: themes, time zone, and many other settings.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="9"></a><b>Why do I want to set my time zone?</b>
      <br>
      Once the forums knows what time zone you are in it will display all dates and time relative to your time zone.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="10"></a><b>How do I add Signature to my Post?</b>
      <br>
      A signature is a message that is appended to the end of any posts you make in the forums. You can edit your signature from the profile page. This signature will then appear at the bottom of any messages posted by you.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="11"></a><b>What is an avatar?</b>
      <br>
      An avatar is a feature of the forums which allows for an image to be displayed along with your posts. Avatars may be enabled or disabled by your forums administrator.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="12"></a><b>How do I set my avatar?</b>
      <br>
      If avatars are enabled by the administrator you will see and avatar section when viewing your profile. From here you can complete the forum to name the avatar you wish to use, either uploading an avatar or specifying a URL to your avatar. You will also need to enable your avatar for it to be displayed with your posts.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="13"></a><b>How do I change the language used by the forums?</b>
      <br>
      The forums is designed to be multi-language friendly. Currently the only available language is English, but additional language packs can be installed to add support for other languages. Check http://forums.asp.net for language packs. Within your profile you will see a listing of the available languages.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="14"></a><b>How do I set the date format?</b>
      <br>
      The date format used to display any date information can be configured from your profile.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="15"></a><b>How do I turn off email-tracking?</b>
      <br>
      Email tracking is a feature which will send emails to you when messages that you are subscribed to change. You can turn off all email tracking globally from your profile.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="16"></a><b>What are the other icons/avatars that show up next to users?</b>
      <br>
      There are many different icons that can show up next to usernames in the forums. Common examples are administrators, moderators, or top posters. Other images may be displayed based on groups the user belongs to. 
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="17"></a><b>Why am I required to login to post, view members, or email other users?</b>
      <br>
      Depending upon how the administrator has configured the forums you may be required to be logged in before viewing/using these areas of the forums. This is primarily to protect the privacy of users who have shared their information or to prevent unwanted/unsolicited emails.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Privacy & Security
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="18"></a><b>How do I change my Password?</b>
      <br>
      Once logged in you can change your password from your Profile page.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="19"></a><b>How do I change my Username?</b>
      <br>
      Unless the administrator has configured the forums to allow username changes you cannot change you username. Otherwise you can change your username from the Profile page.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="20"></a><b>How do I change my email address?</b>
      <br>
      Once logged in, you can change your private email address from your Profile page.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="21"></a><b>What Profile settings are required?</b>
      <br>
      The only profile setting that is required is your private email address. This is the email address that is used when you subscribe to the forum threads or emailing you a forgotten username/password. The private email address is never shared or displayed publicly. If you wish to share an email address publicly, use the public email address field. The remainder of the profile settings are optional.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="22"></a><b>What if I don’t want my name displayed in the member lists?</b>
      <br>
      You can set the option in your profile and your name will not appear in any member listings, including the listing of who is online.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Navigation
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="23"></a><b>What is a Forum Group?</b>
      <br>
      A Forum Group is a top level grouping of related forums. A forum group contains 1 or more forums.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="24"></a><b>What is a Forum?</b>
      <br>
      A Forum is a grouping of related threads of discussion. A Forum contains 0 or more threads and 0 or more sub-forums.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="25"></a><b>What is a Thread?</b>
      <br>
      A Thread is a grouping of related posts. A Thread contains 1 or more Posts. The first post becomes the Thread and replies to the original post increment details on the Thread, such as the reply count or last post.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="58"></a><b>What do the Thread icons mean?</b>
      <br>
      <table cellpadding="2" class="tableBorder" cellspacing="1">
	<tr>
		<td align="left" valign="top" class="column" colspan="2">
			Thread Icon Legend
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic_notread.gif"%>'>
		</td>
		<td width="100%" class="fh3">Topic with posts you have not read.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic.gif"%>'>
		</td>
		<td width="100%" class="fh3">Topic with posts you have read.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-popular_notread.gif"%>'>
		</td>
		<td width="100%" class="fh3">Popular topic with posts you have not read. A topic 
			becomes popular after after a certain number of views and posts (administrator 
			defined).
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-popular.gif"%>'>
		</td>
		<td width="100%" class="fh3">Popular topic with posts you have read. A topic becomes 
			popular after after a certain number of views and posts (administrator 
			defined).
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-announce_notread.gif"%>'>
		</td>
		<td class="fh3">Announcement you have not read
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-announce.gif"%>'>
		</td>
		<td class="fh3">Announcement you have read
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned_notread.gif"%>'>
		</td>
		<td class="fh3">A pinned topic with posts you have not read. Pinned topics are 
			displayed before other topics until they become unpinned.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned.gif"%>'>
		</td>
		<td class="fh3">A pinned topic with posts you have read. Pinned topics are displayed 
			before other topics until they become unpinned.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned&popular_notread.gif"%>'>
		</td>
		<td class="fh3">A pinned popular topic with posts you have not read. A pinned topic 
			with enough views or replies to become popular.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned&popular.gif"%>'>
		</td>
		<td class="fh3">A pinned popular topic with posts you have read. A pinned topic with 
			enough views or replies to become popular.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-locked_notread.gif"%>'>
		</td>
		<td class="fh3">A locked topic with posts you have not read. Locked topics do not 
			allow replies.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-locked.gif"%>'>
		</td>
		<td class="fh3">A locked topic with posts you have read. Locked topics do not allow 
			replies.
		</td>
	</tr>
      </table>
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="26"></a><b>When I view a Forum I don’t see any Threads/Posts?</b>
      <br>
      A forum may not display any threads if there are no threads in the forum or if filters on the forms have been applied and no threads match the filter. An example of a filter is filtering to display threads newer than a certain date, such as threads new in the past 2 weeks.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="27"></a><b>I just posted a message, how come I don’t see it?</b>
      <br>
      A forum may or may not be moderated depending upon how the forum has been configured. After posting a message in a moderated forum you may receive a message stating that the post is awaiting moderation. Once the moderator(s) approve your post you post will become visible. The moderators may choose to move, edit, or delete your post to ensure that the post is topical to the current forum.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="28"></a><b>What are the different icons next to threads?</b>
      <br>
      The icons next to threads when viewing a forum indicate different status. You can move your mouse cursor over these icons to see what the different status / types of threads are.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="29"></a><b>What is an Announcement Thread?</b>
      <br>
      An announcement is a special post type that is always displayed at the top of a forum for a configured amount of time. The purpose of an announcement is to increase the visibility of certain topics.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="30"></a><b>What is a sticky Thread?</b>
      <br>
      A sticky topic is a special post that causes a post to sort to the top of a forum for a specified amount of time. A sticky topic is similar to an announcement, whereas an announcement is displayed separate from other threads and usually does not allow replies.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="31"></a><b>What is a Locked Thread?</b>
      <br>
      A locked thread is a special post that does not allow replies. Once a user locks a post or an administrator/moderator locks a thread no more posts are allowed.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="32"></a><b>Can I sort Threads when viewing a forum?</b>
      <br>
      Yes, you can sort threads when viewing a forum by Author, Replies, Views, and Last Post. The default sort for a forum is to display the newest threads first (Last Post descending). To sort simply click on the column title, e.g. Last Post.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="33"></a><b>What is the ‘XML’ icon at the bottom of a forum?</b>
      <br>
      The XML icon is linked to the RSS feed for the forum. RSS is used to allow other applications to subscribe a forums posts.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="34"></a><b>What is the red/green icon next to a user’s name when viewing a Post?</b>
      <br>
      This icon indicates the user’s online status. A green icon means the user has been active recently (usually within the last 15 minutes). A red icon means the user has not recently been active. You can hover your mouse over this icon to see details about the user’s past activity.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="35"></a><b>I can’t access a forum I know exists.</b>
      <br>
      If you are attempting to access a forum that you have visited before, but now receive an ‘unknown forum’ error there are two likely causes. The first cause is that the forum you are attempting to access is private and you are not signed in. The second cause is that the forum has been removed.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Posting
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="36"></a><b>Can I use HTML?</b>
      <br>
      Yes and no. You cannot type HTML directly into the editor. If you are using Internet Explorer the default editor for creating new posts will be a Rich Text Editor that will automatically format posts using HTML. If you post with a browser other than Internet Explorer a standard HTML textbox is used and BBCode can be used to mark-up posts.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="37"></a><b>What is BBCode?</b>
      <br>
      BBCode is a special syntax for formatting plaintext posts.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="38"></a><b>Can I add attachments to my posts?</b>
      <br>
      Yes, however, this requires the moderator(s) or administrator(s) to enable this permission for user’s on a forum-by-forum basis.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="39"></a><b>What are Emoticons?</b>
      <br>
      Emoticons are graphical elements that can be added within the body of a post to add emotions to the post. Common examples are the use of similes within the contents of a post. The forums come with a pre-defined set of emoticons, however the administrator can add additional ones.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="40"></a><b>How do I post a new message to a forum?</b>
      <br>
      You can post a new message to a forum in several ways depending upon how the administrator has configured the site. When viewing a forum you should see an image button reading New Topic. Clicking on this image button will take you to a form for posting a message or ask you to login first. Depending upon how the administrator has configured the forums you may be able to post anonymously in some forums, i.e. no login required. If you do not see the New Topic image button you may not have enough permissions – even after logging in – to post a message to the forum even though you are allowed to view the forum.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="41"></a><b>How do I reply to an existing post?</b>
      <br>
      You can reply to an existing post using either the Reply or Quote image buttons displayed with the post. If you do not see the Reply or Quote image buttons when viewing a post you either do not have permissions to reply or the post may not allow replies.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="42"></a><b>How do I edit my posts?</b>
      <br>
      If the administrator or moderator has configured the forum or your role to allow editing of posts you will see an Edit image button next to posts you have made. Clicking on this image button will allow you to edit your post.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="43"></a><b>How do I delete my posts?</b>
      <br>
      If the administrator or moderator has configured the forum or your role to allow deleting posts you will see a Delete image button next to new posts you have made. If a post you have made has one or more replies you will no longer be able to delete the post.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="44"></a><b>My Post has words replaced with ***?</b>
      <br>
      The administrator may have specified a word filter for posts. When word filters are enabled certain words that are deemed to be offensive are filtered and replaced with the ‘*’ character.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="45"></a><b>How do I add a signature to my posts?</b>
      <br>
      See How do I add Signature to my Post? in the User Profile and Settings section.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="46"></a><b>How do I add an avatar to my posts?</b>
      <br>
      See What is an avatar? And How do I Set my Avatar in the User Profile and Settings section.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      User Groups & Permissions
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="47"></a><b>What are Permissions?</b>
      <br>
      Permissions control what you are or are not allowed to do while browsing the forums. When using the default views for the forums, the permissions are displayed in the lower-right corner of ever page (where permissions are applicable). The permissions you are granted control all aspects of your view within the forums.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="48"></a><b>What is an Administrator?</b>
      <br>
      An administrator is the highest permission level within the forums. By default, an administrator has full permissions in the forums to perform any action, e.g. moderating posts, approving users, creating new forums, and so on. Administrators belong to the Forum-Administrator group.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="49"></a><b>What is a Moderator?</b>
      <br>
      A moderator is the second highest permission level within the forums. By default a moderator can perform any number of tasks within a particular forum or set of forums. This includes approving posts, moving posts, deleting posts, editing posts, or banning users. If you have a problem with a particular forum the best place to start is with a moderator. Moderators belong to varying groups configured by the Administrator.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="50"></a><b>What is a Forum Role?</b>
      <br>
      A forum role is grouping of common users for the purpose of assigning permissions. In addition to common permission assignment a forum group can also be used to display an image for a user in that group. Forum roles make the job of administering and moderating the forums easier since users can be assigned to roles and then permission applied to forums based on those roles.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="51"></a><b>How do I join a Forum Role or Group?</b>
      <br>
      Users are assigned to Forum Roles by the administrator or moderator. If there is a particular forum role you wish to join, please send a private message or email to one of its members for more information.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Private Messages
    </td>
  <tr>
</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Searching
    </td>
  <tr>
</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      About ASP.NET Forums
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="52"></a><b>What is the ASP.NET Forums?</b>
      <br>
      The ASP.NET Forums is a scalable, extensible, and free web based discussion system that you can use to add discussion capabilities to any web application.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="53"></a><b>Who is using the ASP.NET Forums?</b>
      <br>
      This same discussion system is used by many public and private organizations, including Microsoft. Both the ASP.NET Web Site and the Xbox Web site use the forums system to support web based discussions – these two sites support thousands of users daily and run across multiple servers, so if you’re wondering if the forums can scale – just check out those sites! You can search using Google to find other sites using the ASP.NET Forums.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="54"></a><b>Who builds the ASP.NET Forums?</b>
      <br>
      The ASP.NET Forums is a shared source project built by a community of developers and is released by the Microsoft ASP.NET team. You can learn more about the project at http://forums.asp.net.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="55"></a><b>Where can I get a copy of the ASP.NET Forums?</b>
      <br>
      Visit http://forums.asp.net/builds/ to download the latest version of the ASP.NET Forums. Please see the builds.txt file at that location for more details on the available builds.
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="56"></a><b>What about the license to use the forums?</b>
      <br>
      
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="57"></a><b>What do I do with new features I’ve created or bugs I’ve fixed?</b>
      <br>
      
      <br>
      <a href="#top">Back to top</a>
    </td>
  </tr>

</table>


<P>

    </td>
  </tr>
</table>











 



