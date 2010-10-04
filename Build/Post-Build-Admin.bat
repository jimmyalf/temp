set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Base.Presentation.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Utility.Data.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Data.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Business.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Core.UI.Mvc.Admin.dll"	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Core.Dependencies.NHibernate.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Tools\Telerik.Web.UI.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\log4net.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.Core.dll" 	"%rootPath%\Synologen Admin\bin\" /R /K /Y /Q