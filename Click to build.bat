SET currentdir=%CD%
"Dependancies\Tools\NAnt\NAnt.exe" "-buildfile:Build\Scripts\Build.build"  "-D:currentdir=%currentdir%" "-D:configuration=Build" -nologo
PAUSE