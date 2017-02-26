using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string var = "";

            if (args.Length > 0) // Si des arguments ont été fournis
            {
                var = args[0];

                if (var == "/?" || var == "/H")
                {
                    afficherAide();
                }
                else if (var == "/U")
                {
                    string chemin = "";

                    if (args.Length == 2)
                        chemin = args[1];
                    else
                        chemin = "";

                    maj.installerMaj(chemin);
                }
                else if (var == "/S")
                {
                    File.WriteAllBytes(Environment.CurrentDirectory + @"\source.zip", Properties.Resources.source);

                    MessageBox.Show("Les sources du logiciel ont été extraites dans le dossier courant.", "Extraction des sources", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Argument invalide.", "Erreur d'argument", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                demarrageNormal();
        }

        static private void demarrageNormal()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Une seule instance du logiciel n'est autorisée !", "Erreur de lancement", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(0);
            }

            Registre.Initialisation();

            if (Registre.miseAJour)
                maj.finalisationMaj();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Selection());
        }

        static private void afficherAide()
        {
            MessageBox.Show(
            "/H ou /?     : Affiche cette aide.\n" +
            "/S                : Extrait les sources dans le dossier courant.\n" +
            "/U <path> : Télécharge la nouvelle version du logiciel.\n" +
            "\n" +
            "Pour plus d'aide, allez sur l'aide en ligne : http://penta.fr.cr/GFE/aide.html",
            "Aide du Gestionaire de Fond d'écran", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
