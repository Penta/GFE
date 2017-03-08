using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gfe
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string var = "", chemin = "";
            bool erreur = false;

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
                        MessageBox.Show("Téléchargement des sources via GitHub éffectué !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch { MessageBox.Show("Une erreur est survenue durant le téléchargement des sources !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    web.Dispose();
                }
                else if (var == "/O")
                {
                    if (args.Length == 2)
                    {
                        chemin = @args[1];

                        foreach (string verif in chemin.Split('|'))
                            if (!Directory.Exists(verif))
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
                            MessageBox.Show("Vous devez spécifier un ou des chemins valides séparés par un | (Alt Gr + 6).", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Vous devez spécifier un chemin !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (var == "/R")
                {
                    ResetLogiciel();
                }
                else
                    MessageBox.Show("Argument invalide.", "Erreur d'argument", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                DemarrageNormal();
        }
        
        static private void ResetLogiciel()
        {
            string dossierAppdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Gestionnaire de Fond d'Écran\";

            Registre.ViderRegistre();

            if (Directory.Exists(dossierAppdata))
            Directory.Delete(dossierAppdata, true);

            MessageBox.Show("Tous les fichiers et les données laissés par le Gestionnaire de Fond d'Écran on été supprimés, vous maintenant supprimer cet exécutable pour supprimer totalement le Gestionnaire de Fond d'Écran de votre ordinateur !", "Suppression effectuée", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        static private void VerifierInstance()
        {
            Process[] proc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

            if (proc.Length > 1)
            {
                bool retour = false;

                IntPtr wdwIntPtr = NativeMethods.FindWindow(null, "Gestionnaire de Fond d'Écran");

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
                    MessageBox.Show("Seulement une seule instance du logiciel peut être lancée à la fois !", "Gestionnaire de Fond d'Écran", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(0);
            }
        }

        static private void AfficherAide()
        {
            MessageBox.Show(
            "/H ou /?       : Affiche cette aide.\n" +
            "/S                  : Extrait les sources dans le dossier courant.\n" +
            "/U [<path>] : Télécharge la nouvelle version du logiciel.\n" +
            "/O <path>   : Ouvre le chemin sans demander à l'utilisateur.\n" +
            "/R                  : Supprime toutes les données stockées sur le PC.\n" +
            "\n" +
            "Pour plus d'aide, allez sur l'aide en ligne : http://github.com/Penta/GFE/wiki",
            "Aide du Gestionaire de Fond d'écran", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        internal enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        internal struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }
    }
}
