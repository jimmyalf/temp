<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSearch.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.Site.wpc.Commerce.ProductSearch" %>

<script Language="JavaScript">
<!--
	function ValidateInputLength(source, arguments)

{	
	var x = (document.getElementById('<%=txtSearch.ClientID%>').value);
	if (x.length < 3 && x.length > 0) {
		alert('Ange minst 3 tecken vid sökning.');
		arguments.IsValid = false;
	}
	else
		arguments.IsValid = true;

}
-->
</script>


<asp:Label ID="lblSearch" runat="server" Text="Sök produkt"></asp:Label>
<asp:TextBox ID="txtSearch" runat="server" Height="22px"></asp:TextBox>
<asp:Button ID="btnSearch" runat="server" Text="Sök" 
	onclick="btnSearch_Click" />
	
<asp:CustomValidator ID="vldcSearch" runat="server"
	ErrorMessage="" 
	ClientValidationFunction="ValidateInputLength" ControlToValidate="txtSearch" 
	EnableClientScript="true" ValidateEmptyText="True"></asp:CustomValidator>

