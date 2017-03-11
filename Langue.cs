using System.Globalization;
using System.Threading;

namespace Gfe
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
            CultureInfo langue = Thread.CurrentThread.CurrentUICulture;
            string affichage = Principale.affichage.ToLower();

            if (Texte.Titre == "Gestionnaire de Fond d'Écran")
            {
                if (affichage == "etirer" || affichage == "étirer" || affichage == "stretch")
                    Principale.affichage = "Étirer";
                else if (affichage == "ajuster" || affichage == "ajust")
                    Principale.affichage = "Ajuster";
                else if (affichage == "centrer" || affichage == "center")
                    Principale.affichage = "Centrer";
                else if (affichage == "mosaïque" || affichage == "mosaique")
                    Principale.affichage = "Mosaïque";
                else if (affichage == "remplir" || affichage == "fill")
                    Principale.affichage = "Remplir";
                else if (affichage == "etendre" || affichage == "étendre" || affichage == "extend")
                    Principale.affichage = "Étendre";
            }
            else
            {
                if (affichage == "etirer" || affichage == "étirer" || affichage == "stretch")
                    Principale.affichage = "Stretch";
                else if (affichage == "ajuster" || affichage == "ajust")
                    Principale.affichage = "Ajust";
                else if (affichage == "centrer" || affichage == "center")
                    Principale.affichage = "Center";
                else if (affichage == "mosaïque" || affichage == "mosaique")
                    Principale.affichage = "Mosaique";
                else if (affichage == "remplir" || affichage == "fill")
                    Principale.affichage = "Fill";
                else if (affichage == "etendre" || affichage == "étendre" || affichage == "extend")
                    Principale.affichage = "Extend";
            }
        }

        static private void ConversionCouleur()
        {
            CultureInfo langue = Thread.CurrentThread.CurrentUICulture;
            string couleur = Principale.couleur.ToLower();

            if (Texte.Titre == "Gestionnaire de Fond d'Écran")
            {
                if (couleur == "rouge" || couleur == "red")
                    Principale.couleur = "Rouge";
                else if (couleur == "noir" || couleur == "black")
                    Principale.couleur = "Noir";
                else if (couleur == "blanc" || couleur == "white")
                    Principale.couleur = "Blanc";
                else if (couleur == "gris" || couleur == "grey")
                    Principale.couleur = "Gris";
                else if (couleur == "jaune" || couleur == "yellow")
                    Principale.couleur = "Jaune";
                else if (couleur == "vert" || couleur == "green")
                    Principale.couleur = "Vert";
                else if (couleur == "bleu" || couleur == "blue")
                    Principale.couleur = "Bleu";
                else if (couleur == "violet" || couleur == "purple")
                    Principale.couleur = "Violet";
                else if (couleur == "rose" || couleur == "pink")
                    Principale.couleur = "Rose";
                else if (couleur == "marron" || couleur == "brown")
                    Principale.couleur = "Marron";
                else if (couleur == "orange")
                    Principale.couleur = "Orange";
                else if (couleur == "gris foncé" || couleur == "dark grey")
                    Principale.couleur = "Gris foncé";
                else if (couleur == "bleu clair" || couleur == "light blue")
                    Principale.couleur = "Bleu clair";
            }
            else
            {
                if (couleur == "rouge" || couleur == "red")
                    Principale.couleur = "Red";
                else if (couleur == "noir" || couleur == "black")
                    Principale.couleur = "Black";
                else if (couleur == "blanc" || couleur == "white")
                    Principale.couleur = "White";
                else if (couleur == "gris" || couleur == "grey")
                    Principale.couleur = "Grey";
                else if (couleur == "jaune" || couleur == "yellow")
                    Principale.couleur = "Yellow";
                else if (couleur == "vert" || couleur == "green")
                    Principale.couleur = "Green";
                else if (couleur == "bleu" || couleur == "blue")
                    Principale.couleur = "Blue";
                else if (couleur == "violet" || couleur == "purple")
                    Principale.couleur = "Purple";
                else if (couleur == "rose" || couleur == "pink")
                    Principale.couleur = "Pink";
                else if (couleur == "marron" || couleur == "brown")
                    Principale.couleur = "Brown";
                else if (couleur == "orange")
                    Principale.couleur = "Orange";
                else if (couleur == "gris foncé" || couleur == "dark grey")
                    Principale.couleur = "Dark grey";
                else if (couleur == "bleu clair" || couleur == "light blue")
                    Principale.couleur = "Light blue";
            }
        }
    }
}
