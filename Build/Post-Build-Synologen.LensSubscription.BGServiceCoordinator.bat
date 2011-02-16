xcopy "%1..\..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%1bin\Debug\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%1bin\Debug\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%1bin\Debug\" /R /K /Y /Q

xcopy "%1..\..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\BGServiceCoordinator.Task\Output\Synologen.LensSubscription.BGServiceCoordinator.Task.*.dll"	"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\BGServiceCoordinator.Task\Output\Synologen.LensSubscription.BGServiceCoordinator.Task.*.dll"	"%1bin\Debug\" /R /K /Y /Q