<%@ Page Language="C#" MasterPageFile="~/BaseMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Logs.Logs" Title="Untitled Page" Codebehind="Logs.aspx.cs" %>
<%@ Register Src="~/Common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<asp:Content ID="LogsContent" ContentPlaceHolderID="ComponentContent" Runat="Server">
<div id="dCompMain" class="Log-Logs-aspx">
	<div class="fullBox">
	<div class="wrap">
		<h1>Logs</h1>
		
		<table class="displayAsNonTable">
			<tr>
				<td>
					<asp:Label ID="lblLogTypes" runat="server" Text="Log-types:"></asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="drpLogTypes" runat="server">
					</asp:DropDownList>
				</td>
				<td>
					<asp:Label ID="lblLocations" runat="server" Text="Locations:"></asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="drpLocations" runat="server">
					</asp:DropDownList>
				</td>
				<td>
					<asp:Label ID="lblComponents" runat="server" Text="Components:"></asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="drpComponents" runat="server">
					</asp:DropDownList></td>
			</tr>
			<tr>
				<td valign="top">
					<asp:Label ID="lblAdmType" runat="server" Text="Type:"></asp:Label>
				</td>
				<td valign="top">
					<asp:RadioButtonList ID="rdoAdmType" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Selected="True" Value="0">Both</asp:ListItem>
						<asp:ListItem Value="1">Admin</asp:ListItem>
						<asp:ListItem Value="2">Site</asp:ListItem>
					</asp:RadioButtonList>
				</td>
				<td valign="top">
					<asp:Label ID="lblMoreThan" runat="server" Text="More errors then:"></asp:Label>
				</td>
				<td valign="top">
					<asp:TextBox ID="txtMoreThan" runat="server" Width="70px">0</asp:TextBox>
					<asp:RangeValidator ID="vldRngMoreThan" runat="server" ControlToValidate="txtMoreThan"
						Display="Dynamic" ErrorMessage="Input must be integer!" MinimumValue="0" MaximumValue="10000">(Input must be integer!)</asp:RangeValidator>
				</td>
				<td valign="top">
					<asp:Label ID="lblException" runat="server" Text="Exception text:"></asp:Label>
				</td>
				<td valign="top">
					<asp:TextBox ID="txtException" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblMessage" runat="server" Text="Message text:"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtMessage" runat="server"></asp:TextBox></td>
				<td>
					<asp:Label ID="lblIpAddress" runat="server" Text="IP Address:"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtIpAddress" runat="server"></asp:TextBox></td>
				<td>
					<asp:Label ID="lblUserAgent" runat="server" Text="User agent text:"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtUserAgent" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblHttpReferrer" runat="server" Text="Http referrer text:"></asp:Label></td>
				<td>
					<asp:TextBox ID="txtHttpReferrer" runat="server"></asp:TextBox></td>
				<td>
					<asp:Label ID="lblStartDate" runat="server" Text="Start date:"></asp:Label></td>
				<td>
					<uc1:DateTimeCalendar ID="dtiStartDate" runat="server" />
				</td>
				<td>
					<asp:Label ID="lblEndDate" runat="server" Text="End date:"></asp:Label></td>
				<td>
					<uc1:DateTimeCalendar ID="dtiEndDate" runat="server" />
				</td>
			</tr>
			<tr>
				<td valign="top" style="height: 55px">
					<asp:Label ID="lblOrder" runat="server" Text="Order by:"></asp:Label>
				</td>
				<td valign="top" style="height: 55px">
					<asp:RadioButtonList ID="rdoOrder" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="0">Asc</asp:ListItem>
						<asp:ListItem Selected="True" Value="1">Desc</asp:ListItem>
					</asp:RadioButtonList>
				</td>
				<td valign="top" style="height: 55px">
				</td>
				<td valign="top" style="height: 55px">
				</td>
				<td valign="top" style="height: 55px">
				</td>
				<td valign="top" style="height: 55px">
				</td>
			</tr>
		</table>
	</div>
	</div>
		
	<div class="fullBox">
	<div class="wrap">
		<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" SkinID="Big" />
		<asp:Button ID="btnDelete" runat="server" SkinID="Big" OnClick="btnDelete_Click" Text="Delete Selected" />
		<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear Selected" SkinID="Big" />
	</div>
	</div>
	
	<div class="fullBox">
	<div class="wrap">
		  <asp:GridView ID="dteLogs" 
						 runat="server" 
						 DataKeyNames="cId" 
						 AutoGenerateColumns="False" 
						 AllowSorting="True" 
						 AllowPaging="True"
						 OnPageIndexChanging="dteLogs_PageIndexChanging" 
						 OnRowCommand="dteLogs_RowCommand" 
						 OnRowDeleting="dteLogs_RowDeleting"
						 OnSorting="dteLogs_Sorting" 
						 PageSize="50"
						 GridLines="None"
						 CssClass="displayAsNonTable">
				<HeaderStyle CssClass="header" />
			   <Columns>
					<asp:TemplateField>
						<HeaderTemplate>
							Select
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox ID="chkSelect" runat="server" />
						</ItemTemplate>
					</asp:TemplateField>
				   <asp:BoundField DataField="cId" HeaderText="Log Id" ReadOnly="True" SortExpression="cId" />
				   <asp:BoundField DataField="Logtype" HeaderText="Log type" ReadOnly="True" SortExpression="Logtype" />
<%--				   <asp:BoundField DataField="Location" HeaderText="Location" ReadOnly="True" SortExpression="Location" />
				   <asp:BoundField DataField="Component" HeaderText="Component" ReadOnly="True" SortExpression="Component" />
				   <asp:BoundField DataField="Background" HeaderText="Background" ReadOnly="True" SortExpression="Background" />
				   <asp:BoundField DataField="Admin" HeaderText="Admin/Site" ReadOnly="True" SortExpression="Admin" />
				   <asp:BoundField DataField="cHash" HeaderText="Hash" ReadOnly="True" SortExpression="cHash" />
				   <asp:BoundField DataField="cCount" HeaderText="Count" ReadOnly="True" SortExpression="cCount" />
--%>				   
					<asp:BoundField DataField="cException" HeaderText="Exception" ReadOnly="True" SortExpression="cException" />
                    <asp:BoundField DataField="cMessage" HeaderText="Message" ReadOnly="True" SortExpression="cMessage" />
<%--				   <asp:BoundField DataField="cIpAddress" HeaderText="IP Address" ReadOnly="True" SortExpression="cIpAddress" />
				   <asp:BoundField DataField="cUserAgent" HeaderText="User agent" ReadOnly="True" SortExpression="cUserAgent" />
				   <asp:BoundField DataField="cHttpReferrer" HeaderText="Http referrer" ReadOnly="True" SortExpression="cHttpReferrer" />
--%>
				   <asp:BoundField DataField="cCreatedBy" HeaderText="Created by" ReadOnly="True" SortExpression="CreatedBy" />
				   <asp:BoundField DataField="cCreatedDate" HeaderText="Created date" ReadOnly="True" SortExpression="cCreatedDate" />
				   <asp:BoundField DataField="cChangedBy" HeaderText="Changed by" ReadOnly="True" SortExpression="ChangedBy" />
				   <asp:BoundField DataField="cChangedDate" HeaderText="Changed date" ReadOnly="True" SortExpression="cChangedDate" />
<%--				   
                    <asp:BoundField DataField="cClearedBy" HeaderText="Cleared by" ReadOnly="True" SortExpression="ClearedBy" />
				   <asp:BoundField DataField="cClearedDate" HeaderText="Cleared date" ReadOnly="True" SortExpression="cClearedDate" />
--%>				   <asp:ButtonField HeaderText="Clear" CommandName="Clear" Text="Clear" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
				   <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
			   </Columns>
			  <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="50" Position="TopAndBottom" />
			</asp:GridView>
	</div>
	</div>
</div>
</asp:Content>