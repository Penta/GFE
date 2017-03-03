using Gulix.Wallpaper;
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

        static public string TraitementNom(string nom)
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

        static public string TraitementChemin(string chemin)
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

        static public FileInfo[] RechercheRecursive(string chemin, ref int erreur)
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
                    resultat = resultat.Concat(RechercheRecursive(dossiersInfo[i].FullName, ref erreur)).ToArray();
                    i++;
                }
            }
            catch { erreur++; }

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
    }
}
