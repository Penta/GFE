﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

[assembly: CLSCompliant(false)]
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
                    File.WriteAllBytes(Environment.CurrentDirectory + @"\source.zip", Properties.Resources.source);

                    MessageBox.Show("Les sources du logiciel ont été extraites dans le dossier courant.", "Extraction des sources", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (var == "/O")
                {
                    if (args.Length == 2)
                    {
                        chemin = @args[1];

                        if(Directory.Exists(chemin))
                        {
                            VerifierInstance();
                            Registre.Initialisation();

                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new Principale(chemin));
                        }
                        else
                            MessageBox.Show("Vous devez spécifier un chemin valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Vous devez spécifier un chemin !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Argument invalide.", "Erreur d'argument", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                DemarrageNormal();
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
                IntPtr wdwIntPtr = NativeMethods.FindWindow(null, "Gestionnaire de Fond d'Écran");

                //get the hWnd of the process
                NativeMethods.Windowplacement placement = new NativeMethods.Windowplacement();
                NativeMethods.GetWindowPlacement(wdwIntPtr, ref placement);

                // Check if window is minimized
                if (placement.showCmd == 2)
                {
                    //the window is hidden so we restore it
                    NativeMethods.ShowWindow(wdwIntPtr, NativeMethods.ShowWindowEnum.Restore);
                }

                //set user's focus to the window
                NativeMethods.SetForegroundWindow(wdwIntPtr);

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
