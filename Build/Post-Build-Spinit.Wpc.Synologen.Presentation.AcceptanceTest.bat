for /d %%G in ("%1\bin\*") do (	
	echo Copying dependencies to: %%G
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Base.Presentation.dll" 				"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Utility.Data.dll" 					"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Data.dll" 					"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Business.dll" 				"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Core.UI.Mvc.Admin.dll"				"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Core.Dependencies.NHibernate.dll" 	"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Spinit.Wpc\Spinit.Wpc.Core.Dependencies.AutoMapper.dll" 	"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\Tools\Telerik.Web.UI.dll" 									"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\NHibernate.ByteCode.Castle.dll" 			"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\log4net.dll" 								"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\Castle.DynamicProxy2.dll" 				"%%G\" /R /K /Y /Q
	xcopy "%1..\Dependancies\FluentNHibernate\Castle.Core.dll" 							"%%G\" /R /K /Y /Q
)