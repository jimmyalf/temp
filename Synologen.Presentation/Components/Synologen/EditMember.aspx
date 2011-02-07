<%@ Page Language="C#" MasterPageFile="~/components/Synologen/SynologenMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.EditMember" Title="Untitled Page" Codebehind="EditMember.aspx.cs" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" Runat="Server">
    <div id="dCompMain" class="Components-Synologen-EditMember-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Member</h1>
                <h2><asp:Literal ID="ltHeader" runat="server" /></h2>
				<fieldset>
					<legend>Organization</legend>					
					<div class="formItem clearLeft">
                        <asp:Label ID="lblOrgName" runat="server" AssociatedControlID="txtOrgName" SkinId="Long"/>
                        <asp:TextBox ID="txtOrgName" runat="server" SkinID="Wide"/>
					</div>				
					<div class="formItem clearLeft">
						<asp:Label ID="lblCategories" runat="server" AssociatedControlID="drpMemberCategories" SkinId="Long"></asp:Label>                            
						<asp:DropDownList ID="drpMemberCategories" DataValueField="cCategoryId" DataTextField="cName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpMemberCategories_OnSelectedIndexChanged" />
					</div>
					<div class="formItem clearLeft">
                        <label class="labelLong">Butik *</label>
                        <asp:DropDownList id="drpShops" runat="server" DataValueField="cId" DataTextField="cDetailName" Enabled="false" />
					</div>
					<div class="formItem clearLeft">
                        <label class="labelLong">Website tillhörigheter *</label>					
						<asp:CheckBoxList ID="chkLocations" DataValueField="Id" DataTextField="Name" runat="server" RepeatLayout="Flow"/>
						<%--<asp:RequiredFieldValidator id="reqLocation" InitialValue="0" runat="server" errormessage="Location saknas" controltovalidate="drpLocations" Display="Dynamic" ValidationGroup="Error">*</asp:RequiredFieldValidator>--%>
					</div>						
					<div class="formItem infoItem" style="display:none;visibility:hidden;">
						<span>*</span><p>Kopplingen används ej för Leverantörer.</p>
					</div>							
                </fieldset>
                <br />
                <div id="dAcountDetails" runat="server" visible="true">
					<fieldset>
						<legend>Account Details</legend>
						<div class="formItem">
								<asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" SkinId="Long"></asp:Label>
								<ASP:REQUIREDFIELDVALIDATOR id="rfvUserName" runat="server" errormessage="Username missing" controltovalidate="txtUserName" Display="Dynamic">(Username missing!)</ASP:REQUIREDFIELDVALIDATOR>
								<asp:Label ID="lblUsernameExists" runat="server" ForeColor="red" Text="Username exist, choose another one." Visible="False" />
								<asp:TextBox ID="txtUserName" runat="server" SkinID="Wide"/>
	                            
						</div>    				
						<div class="formItem clearLeft">
								<asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" SkinId="Long"></asp:Label>
								<ASP:REQUIREDFIELDVALIDATOR id="rfvPassword" runat="server" errormessage="Password missing" controltovalidate="txtPassword" Display="Dynamic">(Password missing!)</ASP:REQUIREDFIELDVALIDATOR>
								<asp:TextBox ID="txtPassword" runat="server" SkinID="Wide" TextMode="Password"/>
						</div>			    
						<div class="formItem">
								<asp:Label ID="lblVerifyPassword" runat="server" AssociatedControlID="txtVerifyPassword" SkinId="Long"></asp:Label>
                				<ASP:COMPAREVALIDATOR id="CompareValidator1" runat="server" controltovalidate="txtPassword" controltocompare="txtVerifyPassword" Display="Dynamic" EnableClientScript="false"></ASP:COMPAREVALIDATOR>
								<asp:TextBox ID="txtVerifyPassword" runat="server" SkinID="Wide" TextMode="Password"/>
						</div>
						<div class="formItem clearLeft">
								<asp:Label ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName" SkinId="Long"></asp:Label>
								<ASP:REQUIREDFIELDVALIDATOR id="rfvFirstName" runat="server" errormessage="Firstname missing" controltovalidate="txtFirstName" Display="Dynamic">(Firstname missing!)</ASP:REQUIREDFIELDVALIDATOR>
								<asp:TextBox ID="txtFirstName" runat="server" SkinID="Wide"/>
						</div>    				
						<div class="formItem">
								<asp:Label ID="lblLastName" runat="server" AssociatedControlID="txtLastName" SkinId="Long"></asp:Label>
								<ASP:REQUIREDFIELDVALIDATOR id="rfvLastName" runat="server" errormessage="Lastname missing" controltovalidate="txtLastName" Display="Dynamic">(Lastname missing!)</ASP:REQUIREDFIELDVALIDATOR>
								<asp:TextBox ID="txtLastName" runat="server" SkinID="Wide"/>
						</div>	        				
						<div class="formItem clearLeft">
								<asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" SkinId="Long"></asp:Label>
								<ASP:REQUIREDFIELDVALIDATOR id="rfvEmail" runat="server" errormessage="Email missing" controltovalidate="txtEmail" Display="Dynamic">(Email missing!)</ASP:REQUIREDFIELDVALIDATOR>
								<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
								Display="Dynamic" ErrorMessage="Email invalid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">(Email invalid!)</asp:RegularExpressionValidator>
								<asp:TextBox ID="txtEmail" runat="server" SkinID="Wide"/>
						</div>
						<div class="formItem clearLeft">
								<asp:CheckBox ID="chkActive" runat="server" />
						</div>
					</fieldset>
                </div>
                <br />
                <div id="dPublicDetails" runat="server">
					<fieldset>
						<legend><asp:Literal ID="ltPublicHeader" runat="server" /></legend>
						<div class="formItem">
								<asp:Label ID="lblPublicEmail" runat="server" AssociatedControlID="txtPublicEmail" SkinId="Long"></asp:Label>
								<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPublicEmail"
								Display="Dynamic" ErrorMessage="Email invalid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">(Email invalid!)</asp:RegularExpressionValidator>
								<asp:TextBox ID="txtPublicEmail" runat="server" SkinID="Wide"/>
						</div>
						<div class="formItem clearLeft">
								<asp:Label ID="lblContactFirstName" runat="server" AssociatedControlID="txtContactFirstName" SkinId="Long"></asp:Label>
	                            
								<asp:TextBox ID="txtContactFirstName" runat="server" SkinID="Wide"/>
						</div>
						<div class="formItem">
								<asp:Label ID="lblContactLastName" runat="server" AssociatedControlID="txtContactLastName" SkinId="Long"></asp:Label>
	                            
								<asp:TextBox ID="txtContactLastName" runat="server" SkinID="Wide"/>
						</div>
						<div class="formItem clearLeft" id="dActiveAccount" runat="server" visible="true">
								<asp:CheckBox ID="chkActivePublic" runat="server" />
						</div>
					</fieldset>	
				</div>		
				<br />		
				<fieldset>
					<legend>Adress Details</legend>
					
					<div class="formItem clearLeft">
                            <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress" SkinId="Long"/>
                            <asp:TextBox ID="txtAddress" runat="server" SkinID="Wide"/>
				    </div>
    				
				    <div class="formItem">
                            <asp:Label ID="lblAddress2" runat="server" AssociatedControlID="txtAddress2" SkinId="Long"/>
                            <asp:TextBox ID="txtAddress2" runat="server" SkinID="Wide"/>
				    </div>
				    
				    <div class="formItem clearLeft">
                            <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="txtZipCode" SkinId="Long"/>
                            <asp:TextBox ID="txtZipCode" runat="server" SkinID="Wide"/>
				    </div>
    				
				    <div class="formItem">
                            <asp:Label ID="lblCity" runat="server" AssociatedControlID="txtCity" SkinId="Long"/>
                            <asp:TextBox ID="txtCity" runat="server" SkinID="Wide"/>
				    </div>					
				</fieldset>					
				<fieldset>
					<legend>Numbers</legend>
				    <div class="formItem">
                            <asp:Label ID="lblMobile" runat="server" AssociatedControlID="txtMobile" SkinId="Long"/>
                            <asp:TextBox ID="txtMobile" runat="server" SkinID="Wide"/>
				    </div>

				    <div class="formItem clearLeft">
                            <asp:Label ID="lblPhone" runat="server" AssociatedControlID="txtPhone" SkinId="Long"/>
                            <asp:TextBox ID="txtPhone" runat="server" SkinID="Wide"/>
				    </div>
    				
				    <div class="formItem clearLeft">
                            <asp:Label ID="lblFax" runat="server" AssociatedControlID="txtFax" SkinId="Long"/>
                            <asp:TextBox ID="txtFax" runat="server" SkinID="Wide"/>
				    </div>
				</fieldset>					
				<fieldset>
					<legend>Member info</legend>
					<div class="formItem clearLeft">
                            <asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle" SkinId="Long"/>
                            <asp:TextBox ID="txtTitle" runat="server" SkinID="Wide"></asp:TextBox>
					</div>    				
					<div class="formItem clearLeft">
                            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" SkinId="Long"/>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="Wide"></asp:TextBox>
					</div>												    				        								    				    
				    <div class="formItem clearLeft">
                            <asp:Label ID="lblWeb" runat="server" AssociatedControlID="txtWeb" SkinId="Long"/>
                            <asp:TextBox ID="txtWeb" runat="server" SkinID="Wide"/>
				    </div>
				    				    
				    <div id="dWysiwyg" runat="server" class="dWysiwyg clearLeft">
				        <asp:Label ID="lblBody" runat="server" SkinId="Long">Memberpage</asp:Label>
			            <WPC:WpcWysiwyg ID="txtBody" Mode="WpcInternalAdvanced" runat="server" />
		            </div>
				</fieldset>					
				<fieldset style="visibility:hidden; display:none;">
					<legend>Location connection</legend>
					
					<div class="formItem clearLeft" id="dLocations">
                            <%--<asp:Label ID="lblLocations" runat="server" AssociatedControlID="drpLocations" SkinId="Long"></asp:Label>
                            <asp:CheckBoxList ID="chklLocations" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="5" RepeatLayout="Table"/>--%>
					</div>
				</fieldset>					
				<fieldset>
					<div class="formCommands">					    
					    <asp:button ID="btnSaveAndPublish" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Save & Publish" SkinId="Big" CausesValidation="true" ValidationGroup="Error"/>
					    <asp:button ID="btnSaveForLater" runat="server" CommandName="SaveForLater" OnClick="btnSave_Click" Text="Save For Later" SkinId="Big"  CausesValidation="true" ValidationGroup="Error"/>
					    <asp:button ID="btnSaveForApproval" runat="server" CommandName="SaveForApproval" OnClick="btnSave_Click" Text="Send For Approval" SkinId="Big"  CausesValidation="true" ValidationGroup="Error"/>
					</div>
					
					<asp:ValidationSummary ID="vldsUser" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Error" />
				</fieldset>
			</div>
        </div>
    </div>
</asp:Content>

