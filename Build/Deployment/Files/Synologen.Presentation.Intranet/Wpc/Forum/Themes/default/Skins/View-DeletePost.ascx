<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Control Language="C#" %>
<!-- ********* View-DeletePost.ascx.ascx:Start ************* //-->	
<%-- <Forums:NavigationMenu runat="server" />--%>
<table width="100%" cellspacing="12" cellpadding="0" border="0">
<!-- View-DeletePost.ascx.Header.Start -->
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
																		<asp:Label class="forumName" ID="ForumName" Runat="server" />
																		<br /><br />
																		<asp:Label class="forumThread" ID="ForumDescription" Runat="server" />
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
        <!-- View-DeletePost.BreadCrumb.Start -->	
        <tr>
          <td class="txt4Bold">
            <br />
            &nbsp;<Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb" />
          </td>
        </tr> 				
        <!-- View-DeletePost.BreadCrumb.End -->	
			</table>	
		</td>
	</tr>	
  <!-- View-DeletePost.Header.End -->	
  <!-- View-DeletePost.Body.Start -->	
  <tr>
    <td>
      <table align=center cellspacing="0" cellpadding="0" width="400">
        <tr>
          <td class="column" align="left" height="25">
            &nbsp; <asp:Literal id="DeletePost_Title" runat="server" />
          </td>   
        </tr>
        <tr>          
          <td class="f">
            <table cellspacing="1" cellpadding="3" width="100%">
              <tr>
                <td valign="top" class="txt3" align="left"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("DeletePost_ReasonText")%></td>
              </tr>
              <tr>
                <td align="left" colspan="2">
                  <table>
                    <tr>
                      <td valign="top" class="txt3Bold" nowrap align="right"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("DeletePost_By")%></td>
                      <td valign="top" align="left" class="txt3"><asp:Label id="DeletedBy" runat="server" /></td>
                    </tr>

                    <tr> 
                      <td align="right" nowrap class="txt3Bold">
                        <% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("DeletePost_HasChildren")%>
                      </td>
                      <td align="left">
                        <asp:Label Cssclass="txt3" runat="server" id="HasReplies" />
                      </td>
                    </tr>
      <!--
                    <tr> 
                      <td align="right" nowrap class="txt3Bold">
                        <% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("DeletePost_DeleteChildren") %>
                      </td>
                      <td align="left" >
                        <forums:YesNoRadioButtonList Cssclass="txt3" runat="server" ID="DeleteChildren" RepeatColumns="2" />
                      </td>
                    </tr>

                    <tr> 
                      <td align="right" nowrap class="txt3Bold">
                        <% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("DeletePost_SendEmail") %>
                      </td>
                      <td align="left" >
                        <forums:YesNoRadioButtonList Cssclass="txt3" runat="server" ID="SendEmail" RepeatColumns="2" />
                      </td>
                    </tr>
      -->
                    <tr>
                      <td valign="top" class="txt3Bold" nowrap align="right"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("DeletePost_Reason")%></td>
                      <td valign="top" align="left"><asp:textbox id="DeleteReason" runat="server" columns="75" TextMode="MultiLine" rows="8"></asp:textbox></td>
                    </tr>
                    <tr>
                      <td valign="top" colspan="2" nowrap align="right"><asp:Button id="DeletePost" runat="server" /> &nbsp; <asp:Button id="CancelDelete" runat="server" /> </td>
                    </tr>
                    <tr>
                      <td valign="top" colspan="2" nowrap align="right">
                        <asp:requiredfieldvalidator id="ValidateReason" runat="server" Cssclass="validationWarningSmall" ControlToValidate="DeleteReason" EnableClientScript="False"></asp:requiredfieldvalidator>
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
  <!-- View-DeletePost.Body.End -->	
</table>
<!-- ********* View-DeletePost.ascx.ascx:End ************* //-->	
