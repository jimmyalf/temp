for /d %%G in ("%1\bin\*") do (
	echo Copying dependencies to: %%G
	xcopy "%1..\..\Dependancies\Spinit ADK\Adk.dll" 	"%%g\" /R /K /Y /Q
)