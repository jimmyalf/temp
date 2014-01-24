for /d %%G in ("%1\bin\*") do (
	echo Copying dependencies & task assemblies to: %%G
	xcopy "%1..\..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%%G\" /R /K /Y /Q
	xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%%G\" /R /K /Y /Q
	xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%%G\" /R /K /Y /Q
	xcopy "%1..\..\Synologen.LensSubscription\ServiceCoordinator.Task\Output\Synologen.LensSubscription.ServiceCoordinator.Task.*.dll"	"%%G\" /R /K /Y /Q
	xcopy "%1..\..\Synologen.LensSubscription\ServiceCoordinator.Task\Output\Synologen.LensSubscription.ServiceCoordinator.Task.*.pdb"	"%%G\" /R /K /Y /Q
)