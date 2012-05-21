<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberProfile" Codebehind="MemberProfile.ascx.cs" %>
<div id="contentlev">
<h3><asp:Label ID="lblHeader" runat="server" /></h3>
					<table cellpadding="10px" cellspacing="0" border="0" width="100%" style="padding:5px" >
					<tr>
					<td width="120" valign="top">
                            <b>
                            <asp:Label ID="lblOrgName" runat="server" AssociatedControlID="txtOrgName" SkinId="Long"/></b> </td>
							<td>
                            <asp:TextBox ID="txtOrgName" runat="server" SkinID="Wide" Width="100%"/></td></tr>
							
					<tr><td valign="top">
					
                            <b>
                            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" SkinId="Long"/>                                                                              </b></td>
					<td>
                      <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="Wide" Width="100%"></asp:TextBox>					</td></tr>
					
    <tr>
					<td valign="top">
                            <b>
                            <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" SkinId="Long" />                                                              </b> </td>
							
					<td>
                      <asp:TextBox ID="txtFirstName" runat="server" SkinID="Wide" ReadOnly="True" Width="100%"/>				    </td></tr>
    				
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtLastName" runat="server" SkinID="Wide" ReadOnly="True" Width="100%"/>				    </td></tr>
			    
    				
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" SkinId="Long" Visible="False"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtUserName" runat="server" SkinID="Wide" ReadOnly="True" Visible="False" Width="100%"/>				    </td></tr>
    				
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" SkinId="Long" Visible="False"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtPassword" runat="server" SkinID="Wide" ReadOnly="True" Visible="False" Width="100%"/>				    </td></tr>
				    
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtEmail" runat="server" SkinID="Wide" ReadOnly="True" Width="100%"/>                      </td></tr>
			    
								
					<tr><td valign="top">
                            <b>
                            <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress" SkinId="Long"/>                                                                              </b></td>
					<td>
                      <asp:TextBox ID="txtAddress" runat="server" SkinID="Wide" Width="100%"/>                      </td></tr>
				    
				    
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="txtZipCode" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtZipCode" runat="server" SkinID="Wide" Width="100%"/>				    </td></tr>
    				
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblCity" runat="server" AssociatedControlID="txtCity" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtCity" runat="server" SkinID="Wide" Width="100%"/>				    </td></tr>
				    
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblPhone" runat="server" AssociatedControlID="txtPhone" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtPhone" runat="server" SkinID="Wide" Width="100%"/>				    </td></tr>
    				
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblFax" runat="server" AssociatedControlID="txtFax" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtFax" runat="server" SkinID="Wide" Width="100%"/>				    </td></tr>
				    
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblMobile" runat="server" AssociatedControlID="txtMobile" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtMobile" runat="server" SkinID="Wide" Width="100%"/>				    </td></tr>
    				
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblPublicEmail" runat="server" AssociatedControlID="txtPublicEmail" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtPublicEmail" runat="server" SkinID="Wide" Width="100%"/>                      </td></tr>
				    
				    <tr><td valign="top">
                            <b>
                            <asp:Label ID="lblWeb" runat="server" AssociatedControlID="txtWeb" SkinId="Long"/>                                                                              </b></td>
				    <td>
                      <asp:TextBox ID="txtWeb" runat="server" SkinID="Wide" Width="100%"/>				    </td></tr>
  </table>
					
				
																				
					<div class="formCommands">					    
					    <asp:button ID="btnSave" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Spara profil" SkinId="Big" CssClass="button"/>
					</div>
</div>
