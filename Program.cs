using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    static class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        private struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }


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
            Process[] proc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

            if (proc.Length > 1)
            {
                IntPtr wdwIntPtr = FindWindow(null, "Gestionnaire de Fond d'Écran");

                //get the hWnd of the process
                Windowplacement placement = new Windowplacement();
                GetWindowPlacement(wdwIntPtr, ref placement);

                // Check if window is minimized
                if (placement.showCmd == 2)
                {
                    //the window is hidden so we restore it
                    ShowWindow(wdwIntPtr, ShowWindowEnum.Restore);
                }

                //set user's focus to the window
                SetForegroundWindow(wdwIntPtr);

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
