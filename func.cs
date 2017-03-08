using Gulix.Wallpaper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Gfe
{
    internal class Func
    {
        static public Affichage ConvertirAffichage(string affichage)
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

        static public string TraitementNom(string nom, int longueur = 55)
        {
            string resultat = null;
            string[] tmp = null;

            if (nom.Length > longueur)
            {
                resultat = nom.Substring(0, longueur - 10);

                tmp = nom.Split('.');

                resultat += "[...]." + tmp[tmp.Length - 1];
            }
            else
                resultat = nom;

            return resultat;
        }

        static public string TraitementChemin(string chemin, int longueur = 55)
        {
            string resultat = null;
            string[] tmp = null;

            if (chemin.Length > longueur)
            {
                tmp = chemin.Split('\\');

                resultat = chemin.Substring(0, longueur - 15) + @"[...]\";

                if (tmp[tmp.Length - 1].Length > 10)
                    resultat += tmp[tmp.Length - 1].Substring(tmp[tmp.Length - 1].Length - 10, 10);
                else
                    resultat += tmp[tmp.Length - 1];
            }
            else
                resultat = chemin;

            return resultat;
        }

        static public FileInfo[] RechercheRecursive(string chemin, ref int erreur)
        {
            FileInfo[] resultat = new FileInfo[65535];
            int i = 0;

            try
            {
                // On ajoute les fichiers du dossier au résultat
                resultat = new DirectoryInfo(chemin).GetFiles();

                // Et on récupère la liste des dossiers dans le répertoire courant
                DirectoryInfo[] dossiersInfo = new DirectoryInfo(chemin).GetDirectories();

                // On appelle la même fonction pour les autres dossiers dans ce répertoire
                while (i < dossiersInfo.Length && dossiersInfo.Length > 0)
                {
                    resultat = resultat.Concat(RechercheRecursive(dossiersInfo[i].FullName, ref erreur)).ToArray();
                    i++;
                }
            } catch { erreur++; }

            // On renvoie la liste des fichiers de tout les sous-dossiers
            return resultat;
        }

        static public byte[] ImageVersBytes(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        static public List<string> ListeChemins(string chemins, bool verifSousDossier = false)
        {
            List<string> resultat = new List<string>();
            List<string> tempMinus = new List<string>();
            string[] temp = chemins.Split('|');
            bool present = false;

            foreach (string var in temp) // On vérifie que le path n'existe pas déjà, même sous une autre forme
            {

                if (!tempMinus.Contains(Path.GetFullPath(var.Trim().TrimEnd('\\')).ToLower()))
                {
                    tempMinus.Add(Path.GetFullPath(var.Trim().TrimEnd('\\')).ToLower());
                    resultat.Add(Path.GetFullPath(var.Trim()).TrimEnd('\\'));
                }
            }

            tempMinus.Clear();

            if (verifSousDossier) // On vérifie qu'il n'y a pas des sous-dossiers de dossiers que l'on va analyser dans la liste
            {
                // On tri la liste par ordre de taille et par ordre alphabetique et on le met dans la variable temporaire
                temp = resultat.OrderBy(q => q).ToArray();
                resultat.Clear();

                foreach (string varTemp in temp)
                {
                    present = false;

                    foreach (string varResultat in resultat)
                    {
                        if (varTemp.Length > varResultat.Length)
                            if (varTemp.Substring(0, varResultat.Length).ToLower() == varResultat.ToLower())
                                present = true;
                        else
                            if (varTemp.ToLower() == varResultat.Substring(0, varTemp.Length).ToLower())
                                present = true;

                    }

                    if (!present)
                        resultat.Add(varTemp);
                }
            }

            // On renvoie une liste des fichiers propres sans doublons
            return resultat;
        }

        // L'historique de l'application, on utilise le > car c'est un caractère interdit par Windows dans les URL
        static public string GenererHistorique(string nouveau, string ancien, int longueur = 15)
        {
            string resultat = null;
            List<string> temp = new List<String>();
            int i = 0;

            if (!string.IsNullOrEmpty(ancien))
                temp = ancien.Split('>').ToList();

            resultat = nouveau;

            foreach (string var in temp)
            {
                if (!string.IsNullOrEmpty(var) && i < longueur - 1)
                {
                    if (!resultat.Split('>').Contains(var))
                    {
                        resultat += ">" + var;
                        i++;
                    }
                }
            }

            return resultat;
        }
    }
}
