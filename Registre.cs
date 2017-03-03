using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Linq;

namespace Gestionnaire_de_Fond_d_Écran
{
    class Registre
    {
        public const string emplacement = @"SOFTWARE\Gestionnaire de Fond d'Écran";
        private const string nomContextuel = "Gestionnaire de Fond d'Écran";

        static public RegistryKey registre = Registry.CurrentUser.OpenSubKey(emplacement, RegistryKeyPermissionCheck.ReadWriteSubTree);

        public static string ancienChemin = null;
        public static int ancienId = 0;

        public static bool miseAJour = false;

        static public void Initialisation()
        {
            if (registre == null)
            {
                registre = Registry.CurrentUser.CreateSubKey(emplacement);

                resetRegistre();
            }
            else
            {
                try
                {
                    // Clef pour la disposition du fond d'écran
                    if (registre.GetValue("Disposition").ToString() != null)
                        Principale.affichage = registre.GetValue("Disposition").ToString();
                    else
                        registre.SetValue("Disposition", "etirer");

                    // Clef pour le logiciel externe
                    if (registre.GetValue("LogicielExterne").ToString() != null)
                        Principale.logiciel = registre.GetValue("LogicielExterne").ToString();
                    else
                        registre.SetValue("LogicielExterne", Principale.logiciel);

                    // Clef pour l'ancien chemin utilisé la dernière fois dans le logiciel
                    if (registre.GetValue("AncienChemin").ToString() != null)
                        ancienChemin = registre.GetValue("AncienChemin").ToString();

                    // Clef pour les extensions utilisées par le logiciel
                    if (registre.GetValue("Extensions").ToString() != null)
                        Principale.extension = registre.GetValue("Extensions").ToString();
                    else
                        registre.SetValue("Extensions", Principale.extension);

                    // Clef pour l'ancien ID de fond utilisé dans le logiciel
                    if (registre.GetValue("AncienID") is int)
                        ancienId = Convert.ToInt32(registre.GetValue("AncienID"));
                    else
                        registre.SetValue("AncienID", 0);

                    // Quelques clefs booléennes
                    Principale.rappel = Convert.ToBoolean(registre.GetValue("Rappel"));
                    Principale.sousDossier = Convert.ToBoolean(registre.GetValue("SousDossier"));
                    Principale.rechargementConstant = Convert.ToBoolean(registre.GetValue("VerifConstante"));
                    miseAJour = Convert.ToBoolean(registre.GetValue("MiseAJour"));
                }
                catch
                {
                    MessageBox.Show("Une erreur est survenue durant la lecture du registre !\n\nVotre configuration va être réinitialisée.", "Erreur du registre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    resetRegistre();
                }

                Principale.chemin = ancienChemin;
            }
        }

        static public void resetRegistre()
        {
            registre.SetValue("Disposition", Principale.affichage);
            registre.SetValue("LogicielExterne", Principale.logiciel);
            registre.SetValue("nbErreur", 0);
            registre.SetValue("nbFond", 0);
            registre.SetValue("Rappel", Principale.rappel);
            registre.SetValue("AncienChemin", "");
            registre.SetValue("AncienID", 0);
            registre.SetValue("MiseAJour", false);
            registre.SetValue("Extensions", Principale.extension);
            registre.SetValue("SousDossier", Principale.sousDossier);
            registre.SetValue("VerifConstante", Principale.rechargementConstant);
        }

        static public void miseAjourConfig()
        {
            registre.SetValue("LogicielExterne", Principale.logiciel);
            registre.SetValue("Disposition", Principale.affichage);
            registre.SetValue("Rappel", Principale.rappel);
            registre.SetValue("AncienChemin", Principale.chemin);
            registre.SetValue("AncienID", Principale.id);
            registre.SetValue("Extensions", Principale.extension);
            registre.SetValue("SousDossier", Principale.sousDossier);
            registre.SetValue("VerifConstante", Principale.rechargementConstant);
        }

        static public void compterErreur()
        {
            int var = 0;

            try { var = Convert.ToInt32(registre.GetValue("nbErreur")) + 2; }
            catch { var = 1; }

            registre.SetValue("nbErreur", var);
        }

        static public void compterFond()
        {
            int var = 0;

            try { var = Convert.ToInt32(registre.GetValue("nbFond")) + 1; }
            catch { var = 1; }

            registre.SetValue("nbFond", var);
        }

        static public void ajouterContextuel()
        {
            RegistryKey clef = Registry.CurrentUser.CreateSubKey(@"Software\Classes\Directory\shell\" + nomContextuel, true);
            RegistryKey sousClef = clef.CreateSubKey("command", true);

            clef.SetValue("", "Visualiser les fonds d'écran du dossier");
            clef.SetValue("Icon", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Gestionnaire de Fond d'Écran\icone.ico");
            sousClef.SetValue("", "\"" + System.Reflection.Assembly.GetEntryAssembly().Location + "\" /O \"%1\"");
            sousClef.Close();
            clef.Close();
        }

        static public void retirerContextuel()
        {
            RegistryKey clef = Registry.CurrentUser.OpenSubKey(@"Software\Classes\Directory\shell", true);

            clef.DeleteSubKeyTree(nomContextuel);
            clef.Close();
        }

        static public bool verifierContextuel()
        {
            bool resultat = false;

            RegistryKey clef = Registry.CurrentUser.OpenSubKey(@"Software\Classes\Directory\shell\" + nomContextuel + @"\command", false);

            try
            {
                if (clef.GetValue("") != null)
                    resultat = true;
            }
            catch { resultat = false; }

            return resultat;
        }
    }
}
