#########################################
#                                       #
#               zipsrc.ps1              #
#               ==========              #
#                                       #
#   Script de l'archivage des sources   #
#    fournisent dans l'executable.      #
#                                       #
#    Par Andy Esnard - Février 2017     #
#        Sous Licence GNU GPLv3         #
#                                       #
#########################################

$tmp = ".\tmp\"
$output = ".\Resources\source.zip"

echo "Copie des sources..."

# On supprime les fichiers/dossiers anciens
Remove-Item $tmp -Recurse
Remove-Item $output

# On créé le dossier temporaire
New-Item -ItemType Directory -Force -Path $tmp

Copy-Item "*.csproj" $tmp
Copy-Item "*sln" $tmp
Copy-Item "zipsrc.ps1" $tmp
Copy-Item "app.config" $tmp
Copy-Item "cleanup.cmd" $tmp
Copy-Item "icone.ico" $tmp
Copy-Item "*sln" $tmp
Copy-Item "*.cs" $tmp
Copy-Item "*.resx" $tmp
Copy-Item "LICENSE" $tmp

Copy-Item "Properties/" $tmp -Recurse
Copy-Item "Resources/" $tmp -Recurse

# Nettoyage des sources
cd ".\tmp\"
d
cmd /c ".\cleanup.cmd"
cd ..

# On zip le tout
Add-Type -assembly "system.io.compression.filesystem"
[io.compression.zipfile]::CreateFromDirectory($tmp, $output)

# On supprime le dossier temporaire
Remove-Item $tmp -Recurse
