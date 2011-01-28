set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%rootPath%\Synologen.LensSubscriptionServiceCoordinator\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%rootPath%\Synologen.LensSubscriptionServiceCoordinator\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%rootPath%\Synologen.LensSubscriptionServiceCoordinator\bin\Debug\" /R /K /Y /Q

xcopy "%rootPath%\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%rootPath%\Synologen.LensSubscriptionServiceCoordinator\bin\Release\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%rootPath%\Synologen.LensSubscriptionServiceCoordinator\bin\Release\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%rootPath%\Synologen.LensSubscriptionServiceCoordinator\bin\Release\" /R /K /Y /Q