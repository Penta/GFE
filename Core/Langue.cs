using Gfe.Langues;
using Gfe.Fenetres;

namespace Gfe.Core
{
    class Langue
    {
        static public void TraduireValeur()
        {
            ConversionCouleur();
            ConversionDisposition();
        }

        static private void ConversionDisposition()
        {
            string affichage = Principale.affichage.ToLower();

            if (affichage == "etirer" || affichage == "étirer" || affichage == "stretch")
                Principale.affichage = Texte.ValeurEtirer;
            else if (affichage == "ajuster" || affichage == "ajust")
                Principale.affichage = Texte.ValeurAjuster;
            else if (affichage == "centrer" || affichage == "center")
                Principale.affichage = Texte.ValeurCentrer;
            else if (affichage == "mosaïque" || affichage == "mosaique")
                Principale.affichage = Texte.ValeurMosaique;
            else if (affichage == "remplir" || affichage == "fill")
                Principale.affichage = Texte.ValeurRemplir;
            else if (affichage == "etendre" || affichage == "étendre" || affichage == "extend")
                Principale.affichage = Texte.ValeurEtendre;
        }

        static private void ConversionCouleur()
        {
            string couleur = Principale.couleur.ToLower();

            if (couleur == "rouge" || couleur == "red")
                Principale.couleur = Texte.ValeurRouge;
            else if (couleur == "noir" || couleur == "black")
                Principale.couleur = Texte.ValeurNoir;
            else if (couleur == "blanc" || couleur == "white")
                Principale.couleur = Texte.ValeurBlanc;
            else if (couleur == "gris" || couleur == "grey")
                Principale.couleur = Texte.ValeurGris;
            else if (couleur == "jaune" || couleur == "yellow")
                Principale.couleur = Texte.ValeurJaune;
            else if (couleur == "vert" || couleur == "green")
                Principale.couleur = Texte.ValeurVert;
            else if (couleur == "bleu" || couleur == "blue")
                Principale.couleur = Texte.ValeurBleu;
            else if (couleur == "violet" || couleur == "purple")
                Principale.couleur = Texte.ValeurViolet;
            else if (couleur == "rose" || couleur == "pink")
                Principale.couleur = Texte.ValeurRose;
            else if (couleur == "marron" || couleur == "brown")
                Principale.couleur = Texte.ValeurMarron;
            else if (couleur == "orange")
                Principale.couleur = Texte.ValeurOrange;
            else if (couleur == "gris foncé" || couleur == "dark grey")
                Principale.couleur = Texte.ValeurGrisFoncé;
            else if (couleur == "bleu clair" || couleur == "light blue")
                Principale.couleur = Texte.ValeurBleuClair;
        }
    }
}
