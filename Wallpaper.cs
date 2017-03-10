/*
    Copyright Nicolas Ronvel 2006,2007
	gulix33xp@yahoo.fr

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;


namespace Gulix.Wallpaper
{
	/// <summary>
	/// Type d'affichage du Fond d'écran
	/// </summary>
	public enum Affichage
	{
    	/// <summary>
    	/// Affichage Centrer standard de Windows
    	/// </summary>
		centrer,
		/// <summary>
    	/// Affichage Mosaïque standard de Windows
    	/// </summary>
        mosaique,
        /// <summary>
    	/// Affichage Etirer standard de Windows
    	/// </summary>
        etirer,
        /// <summary>
    	/// Si l'image est plus grande que le bureau, elle est étirée, tout en gardant ses proportions
    	/// </summary>
        ajuster,

        // Ajout perso
        remplir,
        etendre
	}
	
	/// <summary>
	/// Une classe permettant d'afficher les fonds d'écran sous Windows
	/// </summary>
	public class Wallpaper
    {
		#region Variables
        
        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        /// <summary>
        /// Permet de spécifier l'affichage du fond d'écran via la méthode ToString()
        /// </summary>
        protected string sToStringConfig = "%R%F";
        
        /// <summary>
        /// Nom complet du fichier image
        /// </summary>
        protected string nomfichier;
        
        /// <summary>
        /// Style d'affichage choisi pour le fond d'écran
        /// </summary>
        protected Affichage affichage;
        
        /// <summary>
        /// Couleur de fond du bureau à afficher
        /// </summary>
        protected Color couleurFond;
        #endregion

        private bool binaire = false;
        
		#region Proprietes
        
		/// <summary>
		/// La hauteur du fichier image, en pixels (-1 en cas d'erreur)
		/// </summary>
       	public int Hauteur
       	{
       		get
       		{
	            int retour = 0;

	            try
	            {
	                Image image = new Bitmap(this.nomfichier);
	                retour = image.Height;
	                image.Dispose();
	            }
	            catch { retour = -1; }

	            return retour;
       		}
        }

        /// <summary>
        /// La largeur du fichier image, en pixels (-1 en cas d'erreur)
        /// </summary>
        public int Largeur
        {
        	get
        	{
	            int retour = 0;

	            try
	            {
	                Image image = new Bitmap(this.nomfichier);
	                retour = image.Width;
	                image.Dispose();
	            }
	            catch { retour = -1; }

	            return retour;
        	}
        }
        
        /// <summary>
        /// Le style d'affichage
        /// </summary>
        public Affichage Affichage
        {
        	get { return affichage; }
        	set { this.affichage = value; }
        }
        
        /// <summary>
        /// La couleur de fond du bureau
        /// </summary>
        public Color CouleurFondBureau
        {
        	get { return couleurFond; }
        	set { couleurFond = value; }
        }
        
		/// <summary>
		/// Chaine de configuration de la sortie de la fonction ToString()
		/// </summary>
        public string ConfigurationToString
        {
        	get { return sToStringConfig; }
        	set { sToStringConfig = value; }
        }
        
        /// <summary>
        /// Largeur en pixels de l'écran
        /// </summary>
        public static int LargeurEcran { get { return SystemInformation.PrimaryMonitorSize.Width; } }

        /// <summary>
        /// Hauteur en pixels de l'écran
        /// </summary>
        public static int HauteurEcran { get { return SystemInformation.PrimaryMonitorSize.Height; } }
        
        #endregion

        #region Constructeurs
        
        /// <summary>
        /// Constructeur de Wallpaper
        /// </summary>
        /// <param name="fichier">Nom du fichier image</param>
        /// <param name="affich">Type d'affichage</param>
        /// <param name="fond">Couleur du fond</param>
        public Wallpaper(string fichier, Affichage affich, Color fond)
        {
            this.nomfichier = fichier;
            this.affichage = affich;
            this.couleurFond = fond;
        }
		#endregion
        
        /// <summary>
        /// Retourne le nom court du fichier image
        /// </summary>
        /// <returns>le nom court du fichier image</returns>
        public string GetNomCourt()
        {
            string[] split;
            char[] separateur ={ '\\' };
            int max;

            split = this.nomfichier.Split(separateur);
            max = split.GetUpperBound(0);

            return split[max];
        }

        /// <summary>
        /// Retourne le nom du répertoire dans lequel se trouve le fichier image
        /// </summary>
        /// <returns>le nom du répertoire du fichier image</returns>
        public string GetRepertoire()
        {
            string[] split;
            string[] join;
            char[] separateur = {'\\'};
            int max;

            split = this.nomfichier.Split(separateur);
            max = split.GetUpperBound(0);
            join = new string[max];

            for (int i = 0; i < (max); i++) { join[i] = split[i]; }

            return String.Join("\\", join) + "\\";
        }

        /// <summary>
        /// Affiche le fond d'écran avec les paramètres renseignés
        /// </summary>
        /// <exception cref="Exception">Renvoie une erreur survenue lors de l'affichage</exception>
        public void Afficher(bool conversion, bool cheminVide = false)
        {
            string fichierTemporaire = "";
            string paramChemin = "";

            Image image;

            if (conversion)
            {
                // On recopie l'image dans les fichiers temporaires au format bitmap
                if (this.affichage == Affichage.ajuster)
                    image = Image.FromFile(this.Ajustement());
                else
                    image = Image.FromFile(this.GetRepertoire() + this.GetNomCourt());

                if (binaire) // J'ai été obligé de faire comme ça pour éviter une erreur, il y a sans doute un moyen plus propre, mais ça me suffit. :c
                    fichierTemporaire = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                else
                    fichierTemporaire = Path.Combine(Path.GetTempPath(), "wallpaper2.bmp");

                image.Save(fichierTemporaire, ImageFormat.Bmp);
                image.Dispose();
            }

            // On modifie le style d'affichage dans la base de registre
            RegistryKey cle = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            if (this.affichage == Affichage.etirer)
            {
                cle.SetValue(@"WallpaperStyle", 2.ToString());
                cle.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (this.affichage == Affichage.centrer)
            {
                cle.SetValue(@"WallpaperStyle", 1.ToString());
                cle.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (this.affichage == Affichage.mosaique)
            {
                cle.SetValue(@"WallpaperStyle", 1.ToString());
                cle.SetValue(@"TileWallpaper", 1.ToString());
            }

            if (this.affichage == Affichage.ajuster)
            {
                cle.SetValue(@"WallpaperStyle", 1.ToString());
                cle.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (this.affichage == Affichage.remplir)
            {
                cle.SetValue(@"WallpaperStyle", 10.ToString());
                cle.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (this.affichage == Affichage.etendre)
            {
                cle.SetValue(@"WallpaperStyle", 22.ToString());
                cle.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (!cheminVide)
            {
                if (conversion)
                    paramChemin = fichierTemporaire;
                else
                    paramChemin = Path.GetFullPath(this.nomfichier);
            }
            else
                paramChemin = "";

            // On utilise les fonctions de la DLL user32 pour afficher le wallpaper

            NativeMethods.SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, paramChemin, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

            int[] elementArray = { 1 };
            int[] elementValues = { ColorTranslator.ToWin32(this.couleurFond) };

            // et modifier la couleur de fond du bureau
            NativeMethods.SetSysColors(1, elementArray, elementValues);

            if (conversion)
            {
                if (binaire && File.Exists(Path.GetTempPath() + @"wallpaper2.bmp"))
                    File.Delete(Path.GetTempPath() + @"wallpaper2.bmp");
                else if (!binaire && File.Exists(Path.GetTempPath() + @"wallpaper.bmp"))
                    File.Delete(Path.GetTempPath() + @"wallpaper.bmp");
            }
        }
        
        /// <summary>
        /// Réalise l'ajustement du fond d'écran au bureau (avec le style d'Affichage Ajuster)
        /// </summary>
        /// <returns>Le nom du fichier créé, avec l'ancienne image ajustée, ou l'ancienne image si aucun ajustement n'est nécessaire</returns>
        protected string Ajustement()
        {
            string fichierRetour;

            if ((this.Hauteur <= HauteurEcran) && (this.Largeur <= LargeurEcran))
            {
                // Pas d'ajustement nécessaire, on retourne le nom du fichier original
                fichierRetour = this.nomfichier;
            }
            else
            {
                // On calcule les nouvelles dimensions
                double ratio = ((double) HauteurEcran) / ((double) this.Hauteur);

                if (ratio > (((double) LargeurEcran) / ((double) this.Largeur)))
                    ratio = ((double) LargeurEcran) / ((double) this.Largeur);

                int nouvelleLargeur = (int) (((double) this.Largeur) * ratio);
                int nouvelleHauteur = (int) (((double) this.Largeur) * ratio);

                // On crée le support de la nouvelle image
                Size tailleAjuster = new Size(nouvelleLargeur, nouvelleHauteur);
                Image imageAjuster = null;

                // On crée la nouvelle image à partir de l'original, et de la nouvelle taille
                imageAjuster = new Bitmap(Image.FromFile(this.nomfichier), tailleAjuster);
                imageAjuster.Save(Path.Combine(Path.GetTempPath(), "ajuster.bmp"), ImageFormat.Bmp);
                imageAjuster.Dispose();

                fichierRetour = Path.Combine(Path.GetTempPath(), "ajuster.bmp");
            }

            return fichierRetour;
        }
        
        /// <summary>
        /// Retourne une chaîne de caractères
        /// Cette chaîne de caractères peut être configurée au moyen de la string ConfigurationToString
        /// %F : nom complet du fichier (avec l'extension)
		/// %f : nom du fichier sans l'extension
        /// %R : chemin complet vers le fichier
		/// %r : répertoire du fichier (sans le chemin complet)
		/// On peut y ajouter n'importe quel autre caractère
        /// </summary>
        /// <returns>une string donnant des informations sur le Wallpaper</returns>
        public override string ToString()
        {
            // La chaîne retournée dépend de la variable sToStringConfig
            // et de la configuration indiquée
            string sRetour = sToStringConfig;

            if (sRetour.IndexOf("%F") != -1)
	            sRetour = sRetour.Replace("%F",this.GetNomCourt());
            
            if (sRetour.IndexOf("%f") != -1)
            {
            	string sNomCourt = this.GetNomCourt();
            	sNomCourt = sNomCourt.Remove(sNomCourt.LastIndexOf("."),sNomCourt.Length-sNomCourt.LastIndexOf("."));
            	sRetour = sRetour.Replace("%f",sNomCourt);
            }
            
            if (sRetour.IndexOf("%R") != -1)
	            sRetour = sRetour.Replace("%R",this.GetRepertoire());
            
            if (sRetour.IndexOf("%r") != -1)
            {
            	string sRepertoireCourt = this.GetRepertoire();
            	sRepertoireCourt = sRepertoireCourt.Remove(sRepertoireCourt.Length-1,1);
            	sRepertoireCourt = sRepertoireCourt.Remove(0,sRepertoireCourt.LastIndexOf("\\")+1);
            	sRepertoireCourt += "\\";

            	sRetour = sRetour.Replace("%r",sRepertoireCourt);
            }

            return sRetour;
        }
    }

    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean SetSysColors(int elementCount, int[] colorNames, int[] colorValues);
    }
}
