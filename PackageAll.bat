@echo off

mkdir build
del /q build\*.*

echo Packing Testable.Events...
pushd Testable.Events
call Package.bat
if ERRORLEVEL 1 goto failed
popd




goto done


:failed

popd

echo.
echo.
echo An error occurred
pause

:done
