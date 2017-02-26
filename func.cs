﻿using Gulix.Wallpaper;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    class func
    {
        static public Affichage convertirAffichage(string affichage)
        {
            Affichage resultat = Affichage.etirer;
            affichage = affichage.ToLower();

            if (affichage == "etirer" || affichage == "étirer")
                resultat = Affichage.etirer;
            else if (affichage == "ajuster")
                resultat = Affichage.ajuster;
            else if (affichage == "centrer")
                resultat = Affichage.centrer;
            else if (affichage == "mosaique")
                resultat = Affichage.mosaique;
            else if (affichage == "remplir")
                resultat = Affichage.remplir;
            else if (affichage == "etendre" || affichage == "étendre")
                resultat = Affichage.etendre;
            else
                resultat = Affichage.etirer;

            return resultat;
        }

        static public string traitementNom(string nom)
        {
            string resultat = null;
            string[] tmp = null;

            if (nom.Length > 55)
            {
                resultat = nom.Substring(0, 45);

                tmp = nom.Split('.');

                resultat += "[...]." + tmp[tmp.Length - 1];
            }
            else
                resultat = nom;

            return resultat;
        }

        static public string traitementChemin(string chemin)
        {
            string resultat = null;
            string[] tmp = null;

            if (chemin.Length > 55)
            {
                tmp = chemin.Split('\\');

                resultat = chemin.Substring(0, 40) + @"[...]\";

                if (tmp[tmp.Length - 1].Length > 10)
                    resultat += tmp[tmp.Length - 1].Substring(tmp[tmp.Length - 1].Length - 10, 10);
                else
                    resultat += tmp[tmp.Length - 1];
            }
            else
                resultat = chemin;

            return resultat;
        }

        static public FileInfo[] rechercheRecursive(string chemin, ref int erreur)
        {
            FileInfo[] resultat = new FileInfo[65535];
            FileInfo[] temp = new FileInfo[65535];
            int i = 0;

            try
            {
                resultat = new DirectoryInfo(chemin).GetFiles();
                DirectoryInfo[] dossiersInfo = new DirectoryInfo(chemin).GetDirectories();

                while (i < dossiersInfo.Length && dossiersInfo.Length > 0)
                {
                    resultat = resultat.Concat(rechercheRecursive(dossiersInfo[i].FullName, ref erreur)).ToArray();
                    i++;
                }
            }
            catch { erreur++; }

            return resultat;
        }
    }
}
