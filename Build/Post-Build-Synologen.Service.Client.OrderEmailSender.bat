for /d %%G in ("%1\bin\*") do (
	echo Copying dependencies to: %%G
	xcopy "%1..\..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 	"%%G\" /R /K /Y /Q
	xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 			"%%G\" /R /K /Y /Q
	xcopy "%1..\..\Dependancies\FluentNHibernate\Castle.Core.dll" 					"%%G\" /R /K /Y /Q
)