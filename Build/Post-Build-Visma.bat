set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\Spinit ADK\Adk.dll" 	"%rootPath%\Synologen Visma\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Spinit ADK\Adk.dll" 	"%rootPath%\Synologen Visma\bin\Release\" /R /K /Y /Q