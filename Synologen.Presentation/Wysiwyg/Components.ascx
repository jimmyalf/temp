<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Component.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.Component" %>
<%@Import Namespace="System"%>
<%@Import Namespace="System.Collections"%>
<%@Import Namespace="System.ComponentModel"%>
<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Drawing"%>
<%@Import Namespace="System.Web"%>
<%@Import Namespace="System.Web.UI.WebControls"%>
<%@Import Namespace="System.Web.UI.HtmlControls"%>
<%@Import Namespace="System.Text"%>
<%@Import Namespace="Spinit.Wpc.Content.Data"%>
<%@Import Namespace="Spinit.Wpc.Base.Data"%>
<%@Import Namespace="Spinit.Wpc.Content.Business"%>
<%@Import Namespace="Spinit.Wpc.Utility.Business"%>
<%@Import Namespace="System.Collections.Generic"%>
<script language="C#" runat="server">
	void Page_Load(object sender, EventArgs e) 
	{
		Response.Redirect("~/Wysiwyg/Components");
	}
</script>
