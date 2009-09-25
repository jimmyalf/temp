set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\Tools\Adk.dll" 	"%rootPath%\Synologen Visma\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Tools\Adk.dll" 	"%rootPath%\Synologen Visma\bin\Release\" /R /K /Y /Q