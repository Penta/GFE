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

                if (Convert.ToInt32(nouvelleVersion) > Convert.ToInt32(Principale.VERSION.Replace(".", "").Substring(0, 3)))
                {
                    Registre.registre.SetValue("MiseAJour", true);

                    MessageBox.Show(Texte.MajDispo, Texte.InfoTitre, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    Principale.AncienFond();
                    Maj fonc = new Maj();
                    fonc.MiseEnPlace();
                }
                else
                    MessageBox.Show(Texte.LogicielAJour, Texte.InfoTitre, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch
            {
                MessageBox.Show(Texte.ErreurMaj, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            web.Dispose();
        }

        private void MiseEnPlace()
        {
            if (File.Exists(Path.GetTempPath() + "GFE_updt.exe"))
                File.Delete(Path.GetTempPath() + "GFE_updt.exe");

            File.Copy(Path.GetFullPath(this.GetType().Assembly.Location), Path.GetTempPath() + "GFE_updt.exe");
            Process.Start(Path.GetTempPath() + "GFE_updt.exe", "/U \"" + Path.GetFullPath(this.GetType().Assembly.Location) + "\"");

            Environment.Exit(0);
        }

        static public void InstallerMaj(string chemin)
        {
            WebClient web = new WebClient();
            string nouvelleVersion = web.DownloadString("http://penta.fr.cr/GFE/ver").Substring(0, 3); ;

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

                MessageBox.Show(Texte.ErreurInstMaj + e, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            web.Dispose();
        }

        static public void FinalisationMaj()
        {
            Thread.Sleep(250);

            File.Delete(Path.GetTempPath() + "GFE_updt.exe");
            Registre.registre.SetValue("MiseAJour", false);

            MessageBox.Show(Texte.MajInstallée + Principale.VERSION + ".", Texte.MajInstalléeTitre, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
