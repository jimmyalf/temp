<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ import Namespace="Spinit.Wpc.Forum.Components" %>
<!-- ********* View-SearchAdvanced.ascx:Start ************* //-->	
<table align=center width="100%" cellspacing="12" cellpadding="0" border="0">
<!-- View-SearchAdvanced.Header.Start -->
	<tr>
		<td>
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
																		<span class="forumName" ID="ForumName" Runat="server" ><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Title") %></span>												
																		<br /><br />
																		<span class="forumThread" ID="ForumDescription" Runat="server" ><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Description") %></span>
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
        <!-- View-SearchAdvanced.BreadCrumb.Start -->	
        <tr>
          <td align="left" colspan="2" class="txt4Bold">
            <br />
            &nbsp;<Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb2" />
          </td>
        </tr>
        <!-- View-SearchAdvanced.BreadCrumb.End -->					
			</table>	
		</td>
	</tr>	
  <!-- View-SearchAdvanced.Header.End -->	
  <!-- View-SearchAdvanced.Body.Start -->	
  <tr>
    <td align="center">
      <table width="60%" cellpadding=2 cellspacing=1 class=tableBorder>
        <tr>
          <td class="column">
            <%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Options") %>
          </td>
        </tr>
        <tr>
          <td class="fh">
            <table cellpadding="3" cellspacing="2" width="100%">
              <tr> 
                <td>
                  <span class="txt4Bold"><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Keywords") %> </span> <span class="txt4"><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Keywords_Info") %></span>
                  <br />
                  <asp:textbox id="searchTextKeywords" runat="server" columns=84 />
                </td>
              </tr>
              <tr> 
                <td>
                  <span class="txt4Bold"><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Users") %> </span> <span class="txt4"><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Users_Info") %></span>
                  <br />
                  <asp:textbox id="searchTextByUsers" runat="server" columns=84 />
                </td>
              </tr>
              <tr> 
                <td>
                  <span class="txt4Bold"><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Forums") %> </span> <span class="txt4"><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchAdvanced_Forums_Info") %></span>
                  <br />
                  <forums:SearchForumsRadioButtonList AutoPostBack="true" cellpadding="0" cellspacing="0" Cssclass="txt3" id="SearchForums" RepeatColumns="2" runat="server" />
                  <forums:ForumListBox Enabled="false" SelectionMode="Multiple" Rows="15" class="txt4" id="SearchForumList" runat="server" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td align="right">
            <asp:Button id="SearchKeyWordsButton" runat="server" />
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <!-- View-SearchAdvanced.Body.End -->	
</table>
<!-- ********* View-SearchAdvanced.ascx:End ************* //-->	
