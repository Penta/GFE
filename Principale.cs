using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Linq;

// Classe sous licence GNU GPL
using Gulix.Wallpaper;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Principale : Form
    {

        // VERSION DU LOGICIEL
        public const string VERSION = "0.7.2";

        static public int id = -1, nbFichier = 0, mid = 0;
        static public Color couleur = Color.Black;

        static public string affichage = "etirer", logiciel = @"C:\Windows\System32\mspaint.exe";
        static public string chemin = null, ancienAffichage = null;
        static public bool rappel = true;
        static public string[] fichiers = new string[65536], mauvaisFichiers = new string[65536], AncienfondEcran = new string[3];
        static public string extension = "jpg;jpeg;png;bmp";


        private void chargerFond()
        {
            if (!mauvaisFichiers.Contains(fichiers[id]))
            {
                lbl_nom.ForeColor = Color.Black;
                infobulle_nom.SetToolTip(this.lbl_nom, "");
                lbl_nom.Text = "Chargement...";
                this.Refresh();

                Wallpaper fond = new Wallpaper(null, Affichage.etirer, Color.Black);

                fond = new Wallpaper(chemin + "\\" + fichiers[id], func.convertirAffichage(affichage), couleur);

                rechargerInfo();

                Registre.compterFond();

                try
                {
                    fond.Afficher();
                }
                catch
                {
                    lbl_nom.ForeColor = Color.Red;
                    supprimerFichier(true);

                    Registre.compterErreur();
                }
            }
            else
            {
                lbl_nom.ForeColor = Color.Red;
                rechargerInfo();
            }
        }

        private void rechargerInfo()
        {
            recupererFichiers();

            lbl_chemin.Text = func.traitementChemin(chemin);
            infobulle_chemin.SetToolTip(this.lbl_chemin, chemin);

            if (id >= 0)
            {
                btn_supprimer.Enabled = true;
                supprimerToolStripMenuItem.Enabled = true;
                mettreEnFondDécranToolStripMenuItem.Enabled = true;

                btn_modifier.Enabled = true;
                btn_modifier.Text = "Modifier";
                modifierToolStripMenuItem.Enabled = true;
                allezÀLimageNuméroToolStripMenuItem.Enabled = true;

                aléatoireToolStripMenuItem.Enabled = true;

                lbl_num.Text = (id + 1).ToString() + " sur " + nbFichier;

                infobulle_nom.SetToolTip(this.lbl_nom, fichiers[id]);
                lbl_nom.Text = func.traitementNom(fichiers[id]);

                rechargerLeFondToolStripMenuItem.Enabled = true;

                if (id + 1 < nbFichier)
                {
                    btn_suivant.Text = "Suivant";
                    suivantToolStripMenuItem.Text = "Suivant";
                }
                else
                {
                    btn_suivant.Text = "Terminer";
                    suivantToolStripMenuItem.Text = "Terminer";
                }

                if (id == 0)
                {
                    btn_precedent.Enabled = false;
                    précédentToolStripMenuItem.Enabled = false;
                }
                else
                {
                    btn_precedent.Enabled = true;
                    précédentToolStripMenuItem.Enabled = true;
                }
            }
            else if (id == -1)
            {
                btn_supprimer.Enabled = false;
                supprimerToolStripMenuItem.Enabled = false;
                mettreEnFondDécranToolStripMenuItem.Enabled = false;

                btn_modifier.Enabled = false;
                btn_modifier.Text = "Modifier";
                modifierToolStripMenuItem.Enabled = false;

                rechargerLeFondToolStripMenuItem.Enabled = false;

                lbl_nom.Text = "Aucun fichier n'est selectionné.";
                infobulle_nom.SetToolTip(this.lbl_nom, "Cliquez sur commencer pour selectionner un fichier.");

                if (nbFichier != 0)
                {
                    lbl_num.Text = "0 sur " + nbFichier;

                    btn_suivant.Text = "Commencer";
                    suivantToolStripMenuItem.Text = "Commencer";

                    allezÀLimageNuméroToolStripMenuItem.Enabled = true;
                    aléatoireToolStripMenuItem.Enabled = true;
                }
                else
                {
                    lbl_num.Text = "Aucun fichier d'image dans ce dossier !";
                    lbl_nom.Text = null;
                    aléatoireToolStripMenuItem.Enabled = false;

                    btn_suivant.Text = "Terminer";
                    suivantToolStripMenuItem.Text = "Terminer";

                    allezÀLimageNuméroToolStripMenuItem.Enabled = false;
                }

                btn_precedent.Enabled = false;
                précédentToolStripMenuItem.Enabled = false;
            }
        }

        private void fichierSuivant()
        {
            if (btn_suivant.Text == "Terminer")
                fermerProgramme();
            else
            {
                id++;

                chargerFond();
                rechargerInfo();
            }
        }

        private void fichierPrecedent()
        {
            id--;

            chargerFond();
            rechargerInfo();
        }

        private void supprimerFichier(bool mode)
        {
            DialogResult reponse = new DialogResult();

            if (!mode)
                reponse = MessageBox.Show("Voulez-vous vraiment supprimer ce fond d'écran ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            else
                reponse = MessageBox.Show("Le fichier '" + fichiers[id] + "' n'a pas pu être chargé, voulez-vous le supprimer ?", "Erreur", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (reponse == DialogResult.Yes)
            {
                File.Delete(chemin + @"\" + fichiers[id]);
                nbFichier--;

                recupererFichiers();

                if (nbFichier != 0)
                {
                    if (id == nbFichier)
                        id--;

                    chargerFond();
                }
                else
                    rechargerInfo();
            }
            else if (reponse == DialogResult.No & mode == true)
            {
                mauvaisFichiers[mid] = fichiers[id];
                mid++;

                recupererFichiers();
            }
        }

        private void ancienFond()
        {
            try
            {
                Wallpaper fond = new Wallpaper(Path.GetTempPath() + @"\GFE_tmp.bmp", func.convertirAffichage(ancienAffichage), couleur);
                fond.Afficher();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur est survenue durant la remise de votre ancien fond d'écran !\n\n" +
                                "DEBUG :\nFichier : " + Path.GetTempPath() + "GFE_tmp.bmp\nMode : " + ancienAffichage + "\n\nErreur :\n" + e
                                , "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        
        private void fermerProgramme()
        {
            this.Visible = false;

            if (id != -1)
                ancienFond();

            File.Delete(Path.GetTempPath() + @"GFE_tmp.bmp");
            Registre.miseAjourConfig(); 

            this.DestroyHandle();
        }

        private void recupererFichiers()
        {
            string ext = null;

            DirectoryInfo dossier = new DirectoryInfo(chemin);
            FileInfo[] fichiersInfo = dossier.GetFiles();
            nbFichier = 0;

            foreach (FileInfo fichier in fichiersInfo)
            {
                // On récupère l'extension du fichier
                ext = fichier.Extension.ToLower();

                // On compare l'extension sans le point à celles autorisées
                if (extension.Split(';').Contains(ext.Substring(1)))
                {
                    // On ajoute le fichier dans la liste des fichiers autorisés
                    fichiers[nbFichier] = fichier.Name;
                    nbFichier++;
                }
             }
        }

        private void modifierFichier()
        {
            if (btn_modifier.Text == "Modifier")
            {
                modificationExterne();
            }
            else
            {
                chargerFond();
                btn_modifier.Text = "Modifier";
            }
        }

        public Principale(string pChemin)
        {
            RegistryKey registre = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            int tmpId = 0;

            AncienfondEcran[0] = registre.GetValue("Wallpaper").ToString();
            AncienfondEcran[1] = registre.GetValue(@"WallpaperStyle").ToString();
            AncienfondEcran[2] = registre.GetValue(@"TileWallpaper").ToString();

            if ((AncienfondEcran[1] == "2") & (AncienfondEcran[2] == "0"))
                ancienAffichage = "etirer";
            else if ((AncienfondEcran[1] == "1") & (AncienfondEcran[2] == "0"))
                ancienAffichage = "centrer";
            else if ((AncienfondEcran[1] == "1") & (AncienfondEcran[2] == "1"))
                ancienAffichage = "mosaique";
            else if ((AncienfondEcran[1] == "10") & (AncienfondEcran[2] == "0"))
                ancienAffichage = "remplir";
            else if ((AncienfondEcran[1] == "22") & (AncienfondEcran[2] == "0"))
                ancienAffichage = "etendre";
            else
                ancienAffichage = "etirer";

            // On crée le fichier temporaire où sera stocké le le fond d'écran initial
            File.Delete(Path.GetTempPath() + @"GFE_tmp.bmp");

            if (!File.Exists(Path.GetTempPath() + @"GFE_tmp.bmp"))
                File.Copy(@AncienfondEcran[0], Path.GetTempPath() + @"GFE_tmp.bmp");

            chemin = pChemin;
            recupererFichiers();

            InitializeComponent();

            if (rappel && Registre.ancienChemin == chemin)
            {
                tmpId = Registre.ancienId;

                if (nbFichier > 0 && tmpId < nbFichier - 1 && tmpId >= 0)
                {
                    id = tmpId;

                    rechargerInfo();
                    chargerFond();
                }
            }
        }

        private void modificationExterne()
        {
            // Création du processus du logiciel externe
            try
            {
                ProcessStartInfo logicielExterne = new ProcessStartInfo(logiciel, "\"" + chemin + "\\" + fichiers[id] + "\"");
                Process proc = new Process();

                proc.StartInfo = logicielExterne;
                proc.Start();

                btn_modifier.Text = "Recharger";
            }
            catch
            {
                MessageBox.Show("Une erreur est survenue durant le lancement du logiciel externe !\n\n" +
                                "Commande executée :\n\"" + logiciel + " " + chemin + "\\" + fichiers[id] + "\"", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void Principale_Load(object sender, EventArgs e)
        {
            rechargerInfo();
        }

        private void btn_suivant_Click(object sender, EventArgs e) { fichierSuivant(); }
        private void btn_precedent_Click(object sender, EventArgs e) { fichierPrecedent(); }
        private void btn_supprimer_Click(object sender, EventArgs e) { supprimerFichier(false); }

        private void suivantToolStripMenuItem_Click(object sender, EventArgs e) { fichierSuivant(); }
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e) { fermerProgramme(); }

        private void allezÀLimageNuméroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ancientId = id;

            Saut fenetre = new Saut();
            fenetre.ShowDialog();

            if (ancientId != id)
            {
                if (!rechargerLeFondToolStripMenuItem.Enabled && id != -1)
                    rechargerLeFondToolStripMenuItem.Enabled = true;

                chargerFond();
            }
        }

        private void btn_modifier_Click(object sender, EventArgs e)
        {
            modifierFichier();
        }

        private void rechargerLeFondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chargerFond();
        }

        private void changerDeDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ancien = chemin;

            if (id != -1)
                ancienFond();

            SelectionAlt fenetre = new SelectionAlt();
            fenetre.ShowDialog();

            if (ancien != chemin)
            {
                mauvaisFichiers.Initialize();

                id = -1;

                recupererFichiers();
                rechargerInfo();
            }
            else
                chargerFond();
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e) { modificationExterne(); }

        private void aléatoireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rndId = new Random();
            int tmpId = id;

            id = rndId.Next(nbFichier);

            while(tmpId == id && nbFichier >= 2)
                id = rndId.Next(nbFichier);

            chargerFond();
            rechargerInfo();
        }

        private void mettreEnFondDécranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult reponse = new DialogResult();

            reponse = MessageBox.Show("Voulez-vous vraiment appliquer cette image en tant que fond d'écran ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (reponse == DialogResult.Yes)
            {
                File.Delete(Path.GetTempPath() + @"GFE_tmp.bmp");

                if (!File.Exists(Path.GetTempPath() + @"GFE_tmp.bmp"))
                    File.Copy(chemin + @"\" + fichiers[id], Path.GetTempPath() + @"GFE_tmp.bmp");

                ancienAffichage = affichage;

                MessageBox.Show("Le fond d'écran sera actif une fois le logiciel fermé !", "Fond d'écran appliqué.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void minimiserToutesLesFenêtresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            // On réduit toutes les fenêtres
            Type typeShell = Type.GetTypeFromProgID("Shell.Application");
            object objShell = Activator.CreateInstance(typeShell);
            typeShell.InvokeMember("MinimizeAll", System.Reflection.BindingFlags.InvokeMethod, null, objShell, null);

            // Et on réaffiche celle là
            this.Show();
    }

        private void mettreÀJourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ancienFond();
            maj.verifierMaj();
            chargerFond();
        }

        private void précédentToolStripMenuItem_Click(object sender, EventArgs e) { fichierPrecedent(); }
        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e) { supprimerFichier(false); }
        private void aideEnLigneToolStripMenuItem_Click(object sender, EventArgs e) { Process.Start("http://penta.fr.cr/GFE/aide.html"); }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APropos fenetre = new APropos();
            fenetre.ShowDialog();
        }

        private void Principale_FormClosed(object sender, FormClosedEventArgs e) { fermerProgramme(); }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration fenetre = new Configuration();
            fenetre.ShowDialog();

            if (Configuration.changement)
            {
                Configuration.changement = false;

                recupererFichiers();

                if (id != -1)
                    chargerFond();

                if (Configuration.changementExt)
                {
                    id = -1;
                    Configuration.changementExt = false;

                    ancienFond();
                }

                rechargerInfo();
            }
        }
    }
}
