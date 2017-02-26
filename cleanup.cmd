@ECHO OFF
CHCP 28591 > nul

REM Script pour nettoyer les sources du Gestionnaire de Fond d'écran
REM Par Penta - 2017

ECHO Nettoyage des sources...

DEL "%CD%\Resources\source.zip"
DEL "%CD%\Gestionnaire de Fond d'Écran.exe"

RMDIR "%CD%\.vs" /S /Q
RMDIR "%CD%\.vs" /S /Q

RMDIR "%CD%\bin" /S /Q
RMDIR "%CD%\obj" /S /Q
RMDIR "%CD%\tmp" /S /Q