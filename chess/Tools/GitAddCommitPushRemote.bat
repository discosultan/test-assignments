@ECHO OFF
pushd %~dp0

CD..
git add -A
FOR /F "usebackq tokens=1,2 delims==" %%i IN (`wmic os get LocalDateTime /VALUE 2^>NUL`) DO IF '.%%i.'=='.LocalDateTime.' SET timestamp=%%j
SET timestamp=%timestamp:~0,4%-%timestamp:~4,2%-%timestamp:~6,2% %timestamp:~8,2%:%timestamp:~10,2%:%timestamp:~12,2%
git commit -m "Update %timestamp%."
git push origin master

PAUSE
popd