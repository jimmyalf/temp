<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ import Namespace="Spinit.Wpc.Forum" %>
<!-- ********* View-SearchResults.ascx:Start ************* //-->	
<script language="JavaScript" type="text/javascript">
  function TogglePost(postID) {
    post = document.getElementById(postID + '-Post');

    if (post.style.visibility == "hidden") {
      // Show the post
      post.style.visibility = "visible";
      post.style.position = "relative";
      post.style.top = "0";
      post.style.left = "0";

    } else {
      // Hide the post
      post.style.visibility = "hidden";
      post.style.position = "absolute";
      post.style.top = "-10000";
      post.style.left = "-10000";
    }
  }
</script>
<script runat="server">
  /* Use this to set the display width (no. or percent) for search results. */
	string OverallTableWidth = "590"; 
</script>
<table width="100%" cellspacing="12" cellpadding="0" border="0">
  <tbody>
  <!-- View-SearchResults.Header.Start -->
	<tr>
		<td colspan="2">
			<table align="center" width="70%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td valign="top" width="*" style="padding-right: 12px;">
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
																	<td align="center">
																		<span class="forumName" ID="ForumName" Runat="server" ><%= ResourceManager.GetString("SearchResults_Title") %></span>												
																		<br /><br />
																		<span class="forumThread" ID="ForumDescription" Runat="server" ><%= ResourceManager.GetString("SearchResults_Description") %></span>
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
  <!-- View-SearchResults.Header.End -->	
  <!-- View-SearchResults.Body.Start -->	
  <!-- View-SearchResults.BreadCrumb.Start -->	
  <tr>
    <td class="txt4" align="left" colspan="2">
      &nbsp;<Forums:BreadCrumb id="Breadcrumb" runat="server" ShowHome="true" />
    </td>
  </tr>
  <!-- View-SearchResults.BreadCrumb.End -->	
  <tr>
    <td class="txt4" valign="center" align="left" colspan="2">
      <asp:TextBox id="SearchTextTop" runat="server" MaxLength="64" columns="55" />
      <asp:Button id="SearchButtonTop" runat="server"></asp:Button> <a href="<%=Globals.GetSiteUrls().SearchAdvanced%>"><%= ResourceManager.GetString("Search_MoreSearchOptions")%></a>
      <br />
      <asp:Literal id="TotalResults" runat="server"></asp:Literal>
      <asp:Literal id="SearchDuration" runat="server"></asp:Literal>      
      <br />
      <br />
    </td>
  </tr>
  <tr>
    <td colspan="2">
<!-- ********* View-SearchResults.DataList.Start ************* //-->							    
<asp:DataList id="SearchDataList" runat="Server" EnableViewState="false" Width="100%" >
<ItemTemplate>
<!-- ********* ItemTemplate.Start ************* //-->	
      <table width="<%= OverallTableWidth %>" cellpadding="0" cellspacing="1" border="0" >
        <tr>
          <td class="txt2Bold" colspan="2">
            <a href="<%# Globals.GetSiteUrls().Post( (int) DataBinder.Eval(Container.DataItem, "PostID") ) %>"><%# DataBinder.Eval(Container.DataItem, "Subject") %></a>
          </td>
        </tr>
        <tr>
          <td height="1" colspan="3" bgcolor="darkgray" colspan="2"></td>
        </tr>
        <tr>
          <td class="txt4">
            <%= ResourceManager.GetString("SearchResults_PostedBy") %>: <a target="_blank" href="<%# Globals.GetSiteUrls().UserProfile( ((Post) Container.DataItem).User.UserID ) %>"><%# DataBinder.Eval(Container.DataItem, "UserName") %></a><br />
            <%= ResourceManager.GetString("SearchResults_Posted") %> <%# ((DateTime) DataBinder.Eval(Container.DataItem, "PostDate")).ToString( Users.GetUser().DateFormat ) %><br />
            <%= ResourceManager.GetString("SearchResults_PostedBy_2") %> <a href="<%# Globals.GetSiteUrls().Forum( ((Post) Container.DataItem).ForumID ) %>"><%# DataBinder.Eval(Container.DataItem, "Forum.Name") %></a> <%= ResourceManager.GetString("SearchResults_PostedBy_3") %>
          </td>
        </tr>
        <tr>
          <td class="txt3" colspan="2">
            <%# ((Post) Container.DataItem).GetBodySummary(200, SearchTextTop.Text, System.Drawing.Color.Red) %>
          </td>
        </tr>
        <tr>
          <td class="txt4Bold" colspan="2">
            <a href="javascript:TogglePost(<%# DataBinder.Eval(Container.DataItem, "PostID") %>)"><%= ResourceManager.GetString("SearchResults_More") %></a>
          </td>
        </tr>
      </table>
      <table width="<%= OverallTableWidth %>" cellspacing="0" border="0" class="tableBorder" cellpading="3" style="visibility:hidden;position:absolute;top:-10000;left:-10000;" id="<%# DataBinder.Eval(Container.DataItem, "PostID") %>-Post">
        <tr>
          <td>
            <table width="100%">
              <tr>
                <td width="30">&nbsp;</td>
                <td class="txt3">
                  <span class="normalTextSmall"><%# DataBinder.Eval(Container.DataItem, "FormattedBody") %></span>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
<!-- ********* ItemTemplate.End ************* //-->	
</ItemTemplate>
<SeparatorTemplate>
<!-- ********* SeparatorTemplate.Start ************* //-->	
      <br />
<!-- ********* SeparatorTemplate.End ************* //-->	
</SeparatorTemplate>
</asp:DataList>
<!-- ********* View-SearchResults.DataList.Start ************* //-->							    
    </td>
  </tr>
  <!-- View-SearchResults.Pager.Start -->
  <tr>
    <td colspan="2">
      <table width="<%= OverallTableWidth %>" cellpadding="0" cellspacing="0" border="0" >
        <tr>
          <td class="txt4" valign="top" align="left">          
            <Forums:CurrentPage id="CurrentPage" runat="server" />
          </td>
          <td class="txt4" align="right">
            <Forums:Pager id="Pager" runat="server" />
          </td>
        </tr>
      </table>
      <br />
    </td>                        
  </tr>
  <!-- View-SearchResults.Pager.End -->
  <tr>
    <td class="txt4" valign="center" align="left" colspan="2">
      <asp:TextBox id="SearchTextBottom" runat="server" MaxLength="64" columns="55"></asp:TextBox>
      <asp:Button id="SearchButtonBottom" runat="server"></asp:Button> <a href="<%=Globals.GetSiteUrls().SearchAdvanced%>"><%= ResourceManager.GetString("Search_MoreSearchOptions")%></a>
    </td>
  </tr>
  <tr>
    <td class="txt4" valign="bottom" align="left" colspan="2">
      &nbsp;<Forums:BreadCrumb id="Breadcrumb2" runat="server" ShowHome="true" />
    </td>
  </tr>
  <!-- View-SearchResults.Body.End -->	  
  </tbody>
</table>
<!-- ********* View-SearchResults.ascx:End ************* //-->	
