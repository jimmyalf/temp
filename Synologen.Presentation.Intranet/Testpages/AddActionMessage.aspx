<%@ Page Language="C#" AutoEventWireup="true"%>
<%@ Import Namespace="Spinit.Wpc.Synologen.Presentation.Intranet.Code" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server" language="c#">
	public string Message { get; set; }
	protected void Page_Load(object sender, EventArgs e)
	{
		var tempDataprovider = (ITempDataProvider) HttpContext.Current.Items["__TempDataProvider"];
		if (tempDataprovider == null) return;
		Message = Request.Params["message"] ?? "Testmeddelande";
		tempDataprovider.Set("ActionMessage", Server.UrlDecode(Message));
		HttpContext.Current.Items["__TempDataProvider"] = tempDataprovider;
	}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test message page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<p>A test message ("<%=Message %>") has been added.</p>
		<p>To add a custom message, call this page with parameter ?message=Your%20own%20message%20goes%20here.</p>
    </div>
    </form>
</body>
</html>
