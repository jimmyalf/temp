<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ import Namespace="Spinit.Wpc.Forum.Components" %>
<!-- ********* View-Search.ascx:Start ************* //-->	   
<table width="100%" cellspacing="12" cellpadding="0" border="0">
<!-- View-Search.Header.Start -->
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
																		<span class="forumName" ID="ForumName" Runat="server" ><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchViewSimple_Title") %></span>												
																		<br /><br />
																		<span class="forumThread" ID="ForumDescription" Runat="server" ><%= Spinit.Wpc.Forum.Components.ResourceManager.GetString("SearchViewSimple_Title") %></span>
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
				<tr>
				  <td>&nbsp;</td>
        </tr>				  
        <!-- View-Search.BreadCrumb.Start -->	
        <tr>
          <td align="left" colspan="2" class="txt4" valign="middle">
            <Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb" />
          </td>
        </tr>
        <!-- View-Search.BreadCrumb.End -->	
			</table>	
		</td>
	</tr>	
  <!-- View-Search.Header.End -->	
  <!-- View-Search.Body.Start -->	
  <tr>
    <td align="center">
      <asp:textbox id="searchTextKeywords" runat="server" columns=44 /> 
      <asp:button id="SearchKeyWordsButton" runat="server" />
      <a class="txt4" href="<%= Globals.GetSiteUrls().SearchAdvanced %>"><%= ResourceManager.GetString("Search_MoreSearchOptions") %></a>
    </td>
  </tr>
  <!-- View-Search.Body.End -->	
</table>
<!-- ********* View-Search.ascx:End ************* //-->	   

