set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Base.Presentation.Site.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Presentation.Site.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Content.Data.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit.Wpc\Spinit.Wpc.Member.Business.dll" 	"%rootPath%\Synologen Site\bin\" /R /K /Y /Q