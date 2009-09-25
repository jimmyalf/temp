set rootPath=%~dp0..
xcopy "%rootPath%\Dependancies\Tools\DeployLX.Licensing.v3.dll" 	"%rootPath%\Synologen Invoice\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Tools\DeployLX.Licensing.v3.dll" 	"%rootPath%\Synologen Invoice\bin\Release\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Tools\SFTI-Helpers.dll"				"%rootPath%\Synologen Invoice\bin\Debug\" /R /K /Y /Q
xcopy "%rootPath%\Dependancies\Tools\SFTI-Helpers.dll"				"%rootPath%\Synologen Invoice\bin\Release\" /R /K /Y /Q