using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Gfe
{
    public partial class Maj
    {
        static public void VerifierMaj()
        {
            WebClient web = new WebClient();

            string nouvelleVersion = "";

            try
            {
                nouvelleVersion = web.DownloadString("http://penta.fr.cr/GFE/ver").Substring(0, 3);

                if (Convert.ToInt32(nouvelleVersion) > Convert.ToInt32(Principale.VERSION.Replace(".", "")))
                {
                    Registre.registre.SetValue("MiseAJour", true);

                    MessageBox.Show("Une mise à jour est disponible !\n\nLe logiciel doit redémarrer pour se mettre à jour.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    Maj fonc = new Maj();
                    fonc.MiseEnPlace();
                }
                else
                {
                    File.Delete("ver.txt");
                    MessageBox.Show("Votre logiciel est déjà à jour !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch
            {
                MessageBox.Show("Impossible de faire la mise à jour ! Avez-vous encore Internet ? Le serveur de mise à jour est-il encore en ligne ?", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void MiseEnPlace()
        {
            if (File.Exists(Path.GetTempPath() + "GFE_updt.exe"))
                File.Delete(Path.GetTempPath() + "GFE_updt.exe");

            File.Copy(Path.GetFullPath(this.GetType().Assembly.Lo‌​cation), Path.GetTempPath() + "GFE_updt.exe");
            Process.Start(Path.GetTempPath() + "GFE_updt.exe", "/U \"" + Path.GetFullPath(this.GetType().Assembly.Lo‌​cation) + "\"");

            Environment.Exit(0);
        }

        static public void InstallerMaj(string chemin)
        {
            WebClient web = new WebClient();
            string nouvelleVersion = nouvelleVersion = web.DownloadString("http://penta.fr.cr/GFE/ver").Substring(0, 3); ;

            Thread.Sleep(250);
            File.Delete(chemin);

            try
            {
                web.DownloadFile("http://penta.fr.cr/GFE/depot/" + nouvelleVersion + "/gfe.exe", chemin);
                Process.Start(chemin);

                Environment.Exit(0);
            }
            catch (Exception e)
            {
                File.Copy(Path.GetTempPath() + "GFE_updt.exe", chemin);
                Registre.registre.SetValue("MiseAJour", false);

                MessageBox.Show("Une erreur est survenue durant la mise à jour !\n\nErreur :\n" + e, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static public void FinalisationMaj()
        {
            Thread.Sleep(250);
            File.Delete(Path.GetTempPath() + "GFE_updt.exe");
            Registre.registre.SetValue("MiseAJour", false);

            MessageBox.Show("Votre logiciel a bien été mis à jour vers la version " + Principale.VERSION + " !", "Mise à jour réussite !", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
