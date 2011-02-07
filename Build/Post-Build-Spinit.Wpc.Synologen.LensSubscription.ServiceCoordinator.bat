set rootPath=%~dp0..\..
xcopy "%rootPath%\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%rootPath%\Synologen.LensSubscription\Synologen.LensSubscription.ServiceCoordinator\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%rootPath%\Synologen.LensSubscription\Synologen.LensSubscription.ServiceCoordinator\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%rootPath%\Synologen.LensSubscription\Synologen.LensSubscription.ServiceCoordinator\bin\Debug\" /R /K /Y /Q

xcopy "%rootPath%\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%rootPath%\Synologen.LensSubscription\Synologen.LensSubscription.ServiceCoordinator\bin\Release\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%rootPath%\Synologen.LensSubscription\Synologen.LensSubscription.ServiceCoordinator\bin\Release\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%rootPath%\Synologen.LensSubscription\Synologen.LensSubscription.ServiceCoordinator\bin\Release\" /R /K /Y /Q