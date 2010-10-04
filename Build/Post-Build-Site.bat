set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Base.Presentation.Site.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Presentation.Site.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Data.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Business.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Member.Business.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Core.Dependencies.NHibernate.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit\Spinit.Data.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit\Spinit.Services.Client.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\log4net.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.Core.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q