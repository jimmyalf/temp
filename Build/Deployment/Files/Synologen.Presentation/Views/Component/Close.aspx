<%@ Page Inherits="System.Web.Mvc.ViewPage<string>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
	<title>Insert component and close</title>
	<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
	<script src="http://ajax.microsoft.com/ajax/jquery.validate/1.6/jquery.validate.min.js" type="text/javascript"></script>
	<script src="/Common/Js/WPC-Wysiwyg-Component.js" type="text/javascript"></script>
</head>
<body class="close">
	<div><input type="hidden" id="returnvalue" name="returnvalue" value="<%= Model %>" /></div>
</body>
</html>