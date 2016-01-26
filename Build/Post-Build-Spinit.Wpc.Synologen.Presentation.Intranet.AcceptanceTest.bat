::@echo off
for /d %%G in ("%1\bin\*") do (
	echo Copying dependencies to: %%G
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Base.Presentation.Site.dll" 				"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Presentation.Site.dll" 			"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Data.dll" 						"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Business.dll" 					"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Member.Business.dll" 					"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Core.Dependencies.NHibernate.dll" 		"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit\Spinit.Data.dll" 										"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit\Spinit.Services.Client.dll" 							"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 				"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\log4net.dll" 									"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 					"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\Castle.Core.dll" 								"%%G\" /R /K /Y /Q
)