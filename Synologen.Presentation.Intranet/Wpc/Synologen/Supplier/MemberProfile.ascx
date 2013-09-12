<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberProfile" Codebehind="MemberProfile.ascx.cs" %>
<div class="Member-EditMember-aspx wrap">
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server" /></legend>
					
		<div class="formItem clearLeft">
                <asp:Label ID="lblOrgName" runat="server" AssociatedControlID="txtOrgName" SkinId="Long"/>
                <asp:TextBox ID="txtOrgName" runat="server" SkinID="Wide"/>
		</div>
					
		<div class="formItem clearLeft">
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" SkinId="Long"/>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="Wide"></asp:TextBox>
		</div>
    
    				
		<div class="formItem clearLeft">
                <asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" SkinId="Long"/>
                <asp:TextBox ID="txtFirstName" runat="server" SkinID="Wide" ReadOnly="True"/>
		</div>
    				
		<div class="formItem clearLeft">
                <asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" SkinId="Long"/>
                <asp:TextBox ID="txtLastName" runat="server" SkinID="Wide" ReadOnly="True"/>
		</div>
			    
    				
		<div class="formItem clearLeft">
                <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" SkinId="Long" Visible="False"/>
                <asp:TextBox ID="txtUserName" runat="server" SkinID="Wide" ReadOnly="True" Visible="False"/>
		</div>
    				
		<div class="formItem clearLeft">
                <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" SkinId="Long" Visible="False"/>
                <asp:TextBox ID="txtPassword" runat="server" SkinID="Wide" ReadOnly="True" Visible="False"/>
		</div>
				    
		<div class="formItem clearLeft">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" SkinId="Long"/>
                <asp:TextBox ID="txtEmail" runat="server" SkinID="Wide" ReadOnly="True"/>
            </div>
			    
								
		<div class="formItem clearLeft">
                <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress" SkinId="Long"/>
                <asp:TextBox ID="txtAddress" runat="server" SkinID="Wide"/>
            &nbsp;
		</div>
				    
		<div class="formItem clearLeft">
                <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="txtZipCode" SkinId="Long"/>
                <asp:TextBox ID="txtZipCode" runat="server" SkinID="Wide"/>
		</div>
    				
		<div class="formItem clearLeft">
                <asp:Label ID="lblCity" runat="server" AssociatedControlID="txtCity" SkinId="Long"/>
                <asp:TextBox ID="txtCity" runat="server" SkinID="Wide"/>
		</div>
				    
		<div class="formItem clearLeft">
                <asp:Label ID="lblPhone" runat="server" AssociatedControlID="txtPhone" SkinId="Long"/>
                <asp:TextBox ID="txtPhone" runat="server" SkinID="Wide"/>
		</div>
    				
		<div class="formItem clearLeft">
                <asp:Label ID="lblFax" runat="server" AssociatedControlID="txtFax" SkinId="Long"/>
                <asp:TextBox ID="txtFax" runat="server" SkinID="Wide"/>
		</div>
				    
		<div class="formItem clearLeft">
                <asp:Label ID="lblMobile" runat="server" AssociatedControlID="txtMobile" SkinId="Long"/>
                <asp:TextBox ID="txtMobile" runat="server" SkinID="Wide"/>
		</div>
    				
		<div class="formItem clearLeft">
                <asp:Label ID="lblPublicEmail" runat="server" AssociatedControlID="txtPublicEmail" SkinId="Long"/>
                <asp:TextBox ID="txtPublicEmail" runat="server" SkinID="Wide"/>
            </div>
				    
		<div class="formItem clearLeft">
                <asp:Label ID="lblWeb" runat="server" AssociatedControlID="txtWeb" SkinId="Long"/>
                <asp:TextBox ID="txtWeb" runat="server" SkinID="Wide"/>
		</div>
					
		<div class="formItem clearLeft" id="dLocations">
            &nbsp;
		</div>
					
		<div class="formItem clearLeft">
            &nbsp;
		</div>
																				
		<div class="formCommands">					    
			<asp:button ID="btnSave" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Spara profil" SkinId="Big"/>
		</div>
	</fieldset>
</div>
