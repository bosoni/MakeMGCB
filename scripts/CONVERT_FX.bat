call  ..\tools\mgcb /incremental Content_fx.mgcb
pause

rmdir /q /s animations
rmdir /q /s effects
rmdir /q /s fonts
rmdir /q /s images
rmdir /q /s models
rmdir /q /s music
rmdir /q /s sounds

echo copying...
mkdir ..\Tests\bin\Debug\Content
mkdir ..\Tests\bin\Release\Content
xcopy /y /E /I bin\DesktopGL\Data\* ..\Tests\bin\Debug\Content\
xcopy /y /E /I bin\DesktopGL\Data\* ..\Tests\bin\Release\Content\

rem  /compress
rem  http://www.monogame.net/documentation/?page=MGCB
