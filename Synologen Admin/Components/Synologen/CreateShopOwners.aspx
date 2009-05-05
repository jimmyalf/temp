<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateShopOwners.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.CreateShopOwners" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Skapa nya medlemmar</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<label>Välj befintliga medlemmar med kategori:</label><br />
		<asp:DropDownList ID="drpSelectionMemberCategories" runat="server" DataValueField="cCategoryId" DataTextField="cName" />
    </div>
    <div>
		<label>Skapa nya medlemmar med kategori</label><br />
		<asp:DropDownList ID="drpnewMemberCategories" runat="server" DataValueField="cCategoryId" DataTextField="cName" />
    </div>       
    <div>
		<asp:Button ID="btnAction" runat="server" Text="Run" OnClick="btnAction_Click" />
    </div>
    </form>
</body>
</html>
