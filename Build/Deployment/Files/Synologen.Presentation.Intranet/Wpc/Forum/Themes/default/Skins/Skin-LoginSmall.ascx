<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Control Language="C#" %>
<!-- ********* Skin-LoginSmall.ascx:Start ************* //-->
<table width="100%" cellspacing="0" cellpadding="0">
	<tr>
		<td width="60%">
			<table class="tableBorder" cellspacing="1" cellpadding="3" width="100%">
				<tr>
					<td class="column"><%= ResourceManager.GetString("LoginSmall_Title") %></td>
				</tr>
				<tr>
					<td class="fh" align="left" valign="top">
						<table width="100%" cellspacing="0" border="0" cellpadding="0">
							<tr>
								<td align="left" class="txt4" nowrap>
									<%= ResourceManager.GetString("LoginSmall_Username") %>
									<asp:TextBox id="username" runat="server" size="14" maxlength="64" /><asp:RequiredFieldValidator EnableClientScript="false" runat="server" ControlToValidate="username" ErrorMessage="(*)" />						
									<%= ResourceManager.GetString("LoginSmall_Password") %>
									<asp:TextBox TextMode="Password" id="password" runat="server" size="14" maxlength="64" /><asp:RequiredFieldValidator EnableClientScript="false" runat="server" ControlToValidate="password" ErrorMessage="(*)" />
									&nbsp;&nbsp;&nbsp;<asp:CheckBox id="autoLogin" TextAlign="Left" type="checkbox" Checked="true" runat="server" />
									<asp:Button CssClass="txt2" id="loginButton" runat="server" />
								</td>
								<td width="1"><img height="25" width="1" src="<%# Globals.GetSkinPath() + "/images/spacer.gif"%>"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>

<!-- ********* Skin-LoginSmall.ascx:End ************* //-->
