call  ..\tools\mgcb /incremental Content_fx.mgcb
pause

echo copying...
mkdir ..\Tests\bin\Debug\Content
mkdir ..\Tests\bin\Release\Content
xcopy /y /E /I bin\DesktopGL\Data\* ..\Tests\bin\Debug\Content\
xcopy /y /E /I bin\DesktopGL\Data\* ..\Tests\bin\Release\Content\
