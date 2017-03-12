using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using Gfe.Langues;

namespace Gfe
{
    static class Program
    {
        static public string dossierAppdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Gestionnaire de Fonds d'Écran\";
        static public bool nonXP = false;

        [STAThread]
        static void Main(string[] args)
        {
            ObtenirLangue();

            string var = "", chemin = "";
            bool erreur = false;

            CompatibilitéOS();

            if (args.Length > 0) // Si des arguments ont été fournis
            {
                var = args[0];

                if (var == "/?" || var == "/H")
                {
                    AfficherAide();
                }
                else if (var == "/U")
                {
                    if (args.Length == 2)
                        chemin = args[1];
                    else
                        chemin = "";

                    Maj.InstallerMaj(chemin);
                }
                else if (var == "/S")
                {
                    WebClient web = new WebClient();

                    try
                    {
                        web.DownloadFile("https://github.com/Penta/GFE/archive/" + Principale.VERSION + ".zip", Environment.CurrentDirectory + @"\source.zip");
                        MessageBox.Show(Texte.TéléchargementSources, Texte.SuccèsTitre, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch { MessageBox.Show(Texte.ErreurTéléchargementSources, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    web.Dispose();
                }
                else if (var == "/O")
                {
                    if (args.Length == 2)
                    {
                        chemin = @args[1];

                        foreach (string verif in chemin.Split('|'))
                            if (!Directory.Exists(verif.Trim()))
                                erreur = true;

                        if (!erreur)
                        {
                            VerifierInstance();
                            Registre.Initialisation();

                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new Principale(chemin));
                        }
                        else
                            MessageBox.Show(Texte.MauvaisCheminParamètre, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show(Texte.AucunCheminParamètre, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (var == "/R")
                {
                    ResetLogiciel();
                }
                else
                    MessageBox.Show(Texte.ArgumentInvalide, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                DemarrageNormal();
        }
        
        static private void ResetLogiciel()
        {
            Registre.ViderRegistre();

            if (Directory.Exists(dossierAppdata))
                Directory.Delete(dossierAppdata, true);

            if (File.Exists(Path.GetTempPath() + @"wallpaper1.bmp"))
                File.Delete(Path.GetTempPath() + @"wallpaper1.bmp");

            if (File.Exists(Path.GetTempPath() + @"wallpaper2.bmp"))
                File.Delete(Path.GetTempPath() + @"wallpaper2.bmp");

            MessageBox.Show(Texte.ToutSupprimé, Texte.SuccèsTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        static private void DemarrageNormal()
        {
            VerifierInstance();

            Registre.Initialisation();

            if (Registre.miseAJour)
                Maj.FinalisationMaj();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Selection());
        }



        static public void ObtenirLangue()
        {
            EmbeddedAssembly.Load(@"Gfe.lib.fr.resources.dll", @"Gfe.lib.fr.resources.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            string langue = Registre.RecupererLangue();

            if (langue == "fr")
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr");
            else if (langue == "en")
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        static private void CompatibilitéOS()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                nonXP = true;
        }

        static private void VerifierInstance()
        {
            Process[] proc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

            if (proc.Length > 1)
            {
                bool retour = false;

                IntPtr wdwIntPtr = NativeMethods.FindWindow(null, Texte.Titre);

                //get the hWnd of the process
                NativeMethods.Windowplacement placement = new NativeMethods.Windowplacement();
                NativeMethods.GetWindowPlacement(wdwIntPtr, ref placement);

                // Check if window is minimized
                if (placement.showCmd == 2)
                {
                    //the window is hidden so we restore it
                    retour = NativeMethods.ShowWindow(wdwIntPtr, NativeMethods.ShowWindowEnum.Restore);
                }

                //set user's focus to the window
                retour = NativeMethods.SetForegroundWindow(wdwIntPtr);

                if (!retour)
                    MessageBox.Show(Texte.SeulementUneInstance, Texte.Titre, MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(0);
            }
        }

        static private void AfficherAide()
        {
            MessageBox.Show(Texte.Aide, Texte.Titre, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
