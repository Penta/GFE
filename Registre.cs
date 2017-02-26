using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    class Registre
    {
        public const string emplacement = @"SOFTWARE\Gestionnaire de Fond d'Écran";
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

                    // Clef pour l'activation du Rappel du fond d'écran au démarrage du logiciel
                    if (registre.GetValue("Rappel") is bool)
                        Principale.rappel = Convert.ToBoolean(registre.GetValue("Rappel"));
                    else
                        registre.SetValue("Rappel", true);

                    // Clef pour vérifier si il y a eu une mise à jour
                    if (registre.GetValue("MiseAJour") is bool)
                        miseAJour = Convert.ToBoolean(registre.GetValue("MiseAJour"));
                    else
                        registre.SetValue("MiseAJour", false);
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
            registre.SetValue("Rappel", true);
            registre.SetValue("AncienChemin", "");
            registre.SetValue("AncienID", 0);
            registre.SetValue("MiseAJour", false);
            registre.SetValue("Extensions", Principale.extension);

        }

        static public void miseAjourConfig()
        {
            registre.SetValue("LogicielExterne", Principale.logiciel);
            registre.SetValue("Disposition", Principale.affichage);
            registre.SetValue("Rappel", Principale.rappel);
            registre.SetValue("AncienChemin", Principale.chemin);
            registre.SetValue("AncienID", Principale.id);
            registre.SetValue("Extensions", Principale.extension);
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
    }
}
