@ECHO OFF
CHCP 28591 > nul

REM Script pour nettoyer les sources du Gestionnaire de Fond d'Écran
REM Par Andy Esnard - Février 2017
REM Sous Licence GNU GPLv3

ECHO Nettoyage des sources...

IF EXIST "%CD%\Resources\source.zip"                     DEL "%CD%\Resources\source.zip"
IF EXIST "%CD%\Gestionnaire de Fond d'Écran.exe"         DEL "%CD%\Gestionnaire de Fond d'Écran.exe"
IF EXIST "%CD%\Gestionnaire de Fond d'Écran.csproj.user" DEL "%CD%\Gestionnaire de Fond d'Écran.csproj.user"

IF EXIST "%CD%\.vs" RMDIR "%CD%\.vs" /S /Q
IF EXIST "%CD%\bin" RMDIR "%CD%\bin" /S /Q
IF EXIST "%CD%\obj" RMDIR "%CD%\obj" /S /Q
IF EXIST "%CD%\tmp" RMDIR "%CD%\tmp" /S /Q