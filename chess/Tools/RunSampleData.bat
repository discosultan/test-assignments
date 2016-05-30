@ECHO OFF
REM Assumes the project is built in Debug build.

pushd %~dp0

..\Source\ChessSample.CommandLine\bin\Debug\ChessSample.CommandLine.exe /I SampleInput.txt /O SampleOutput.txt

popd