set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\Spinit ADK\Adk.dll" 	"%rootPath%\Synologen Client\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit ADK\Adk.dll" 	"%rootPath%\Synologen Client\bin\Release\" /R /K /Y /Q