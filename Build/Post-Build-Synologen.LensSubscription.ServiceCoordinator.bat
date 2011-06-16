xcopy "%1..\..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%1bin\Debug\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%1bin\Debug\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%1bin\Debug\" /R /K /Y /Q

xcopy "%1..\..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\ServiceCoordinator.Task\Output\Synologen.LensSubscription.ServiceCoordinator.Task.*.dll"	"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\ServiceCoordinator.Task\Output\Synologen.LensSubscription.ServiceCoordinator.Task.*.pdb"	"%1bin\Release\" /R /K /Y /Q
xcopy "%1..\ServiceCoordinator.Task\Output\Synologen.LensSubscription.ServiceCoordinator.Task.*.dll"	"%1bin\Debug\" /R /K /Y /Q
xcopy "%1..\ServiceCoordinator.Task\Output\Synologen.LensSubscription.ServiceCoordinator.Task.*.pdb"	"%1bin\Debug\" /R /K /Y /Q