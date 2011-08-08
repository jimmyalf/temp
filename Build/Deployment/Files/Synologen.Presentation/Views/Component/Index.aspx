<%@ Page Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Utility.Core.ControlPropertyMapping.ControlPropertyMap>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
	<title>Insert component</title>
	<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
	<link rel="stylesheet" type="text/css" href="/Common/Css/Base.css" media="screen" />
	<link rel="stylesheet" type="text/css" href="/Common/Css/WYSIWYG-Dialogs.css" media="screen" />
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
	<script src="http://ajax.microsoft.com/ajax/jquery.validate/1.6/jquery.validate.min.js" type="text/javascript"></script>
	<script src="/Common/Js/WPC-Wysiwyg-Component.js" type="text/javascript"></script>
</head>
<body id="components-index">
	<%= Html.DisplayForModel() %>
</body>
</html>