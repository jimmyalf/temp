<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Groups.GroupObject" Codebehind="GroupObject.ascx.cs" %>
<div class="Settings-GroupObject-ascx">
<asp:Repeater ID="rptObjects" runat="server">
<HeaderTemplate>
<dl class="striped">
</HeaderTemplate>
<ItemTemplate>
	<dt>
		<asp:Label ID="Id" Text='<%# DataBinder.Eval(Container.DataItem, "cId") %>' Visible="false" runat="server" />
		<asp:Label ID="Name" Text='<%# DataBinder.Eval(Container.DataItem, "cName") %>' runat="server" />
	</dt>
	<dd>    
		<asp:RadioButton ID="rdoAllow"  Enabled='<%# DataBinder.Eval(Container.DataItem, "cAllowEdit") %>' Checked='<%# DataBinder.Eval(Container.DataItem, "cAllow") %>' GroupName='<%# DataBinder.Eval(Container.DataItem, "cId") %>' runat="server" Text="Allow" CssClass="radioButtonItems" />
		<asp:RadioButton ID="rdoDeny" Enabled='<%# DataBinder.Eval(Container.DataItem, "cAllowEdit") %>' Checked='<%# DataBinder.Eval(Container.DataItem, "cDeny") %>' GroupName='<%# DataBinder.Eval(Container.DataItem, "cId") %>' runat="server" Text="Deny" CssClass="radioButtonItems" />
	</dd>
</ItemTemplate>
<SeparatorTemplate>

</SeparatorTemplate>
<FooterTemplate>
</dl>
</FooterTemplate>
</asp:Repeater>
</div>