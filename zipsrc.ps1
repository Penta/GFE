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



# Quelques variables utilisées dans le script
$dossierCourant = (Get-Item -Path ".\" -Verbose).FullName
$tmp            = "$dossierCourant\tmp\"
$output         = "$dossierCourant\Resources\source.zip"

echo "Copie des sources..."

# On supprime les fichiers/dossiers anciens
If (Test-Path $tmp   ) { Remove-Item $tmp    -Recurse }
If (Test-Path $output) { Remove-Item $output -Recurse }

# On créé le dossier temporaire
New-Item -ItemType Directory -Force -Path $tmp >> $null

# On teste si les fichiers existent, et on les copie
If ( Test-Path "*.csproj"    ) { Copy-Item "*.csproj"    $tmp }
If ( Test-Path "*.sln"       ) { Copy-Item "*.sln"       $tmp }
If ( Test-Path "*.cs"        ) { Copy-Item "*.cs"        $tmp }
If ( Test-Path "*.resx"      ) { Copy-Item "*.resx"      $tmp }
If ( Test-Path "*.md"        ) { Copy-Item "*.md"        $tmp }
If ( Test-Path "zipsrc.ps1"  ) { Copy-Item "zipsrc.ps1"  $tmp }
If ( Test-Path "app.config"  ) { Copy-Item "app.config"  $tmp }
If ( Test-Path "cleanup.cmd" ) { Copy-Item "cleanup.cmd" $tmp }
If ( Test-Path "LICENSE"     ) { Copy-Item "LICENSE"     $tmp }

# On fait pareil, mais pour les dossiers
If ( Test-Path "Properties/" ) { Copy-Item "Properties/" $tmp -Recurse }
If ( Test-Path "Resources/"  ) { Copy-Item "Resources/"  $tmp -Recurse }

# Nettoyage des sources
echo "Nettoyage des sources..."

# Suppression des fichiers inutiles
If ( Test-Path "$tmp\Resources\source.zip"                     ) { Remove-Item "$tmp\Resources\source.zip"                     }
If ( Test-Path "$tmp\Gestionnaire de Fond d'Écran.exe"         ) { Remove-Item "$tmp\Gestionnaire de Fond d'Écran.exe"         }
If ( Test-Path "$tmp\Gestionnaire de Fond d'Écran.csproj.user" ) { Remove-Item "$tmp\Gestionnaire de Fond d'Écran.csproj.user" }

# Suppression des dossiers inutiles
If ( Test-Path "$tmp\.vs"  ) { Remove-Item "$tmp\.vs" -Recurse }
If ( Test-Path "$tmp\bin"  ) { Remove-Item "$tmp\bin" -Recurse }
If ( Test-Path "$tmp\obj"  ) { Remove-Item "$tmp\obj" -Recurse }
If ( Test-Path "$tmp\tmp"  ) { Remove-Item "$tmp\tmp" -Recurse }

echo "Archivage des sources..."

# On zip les sources dans le dossier de sortie
Add-Type -assembly "System.IO.Compression.Filesystem"
[IO.Compression.ZipFile]::CreateFromDirectory($tmp, $output)

echo "L'archive des sources a été créée à l'emplacement : $output !"

# On supprime le dossier temporaire
Remove-Item $tmp -Recurse
