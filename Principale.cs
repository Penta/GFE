using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Linq;
using System.Reflection;

// Classe sous licence GNU GPLv3
using Gulix.Wallpaper;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Principale : Form
    {
        // VERSION DU LOGICIEL
        static public string VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);

        // Variables globales
        static public int id = -1, nbFichier = 0, mid = 0;
        static public Color couleur = Color.Black;
        static public string affichage = "etirer", logiciel = @"C:\Windows\System32\mspaint.exe";
        static public string chemin = null, ancienAffichage = null;
        static public bool rappel = true, sousDossier = false, rechargementConstant = false;
        static public string[] mauvaisFichiers = new string[65536], AncienfondEcran = new string[3];
        static public string extension = "jpg;jpeg;png;bmp";
        static private bool premierChargement = true;
        static public bool selectionPremierLancement = true;
        FileInfo[] fichiers = new FileInfo[65536];

        // Fonction chargeant le fond lié à la variable id dans l'array fichiers
        private void chargerFond()
        {
            if (id >= 0 && id < fichiers.Length)
            {
                // Si le fichier actuel n'est pas listé dans les fichiers illisibles
                if (!mauvaisFichiers.Contains(fichiers[id].FullName))
                {
                    lbl_nom.ForeColor = Color.Black;
                    infobulle_nom.SetToolTip(this.lbl_nom, "");
                    lbl_nom.Text = "Chargement...";
                    this.Refresh();

                    // On prépare l'application du fond
                    Wallpaper fond = new Wallpaper(fichiers[id].FullName, func.convertirAffichage(affichage), couleur);

                    // On compte le nombre de fond affichés
                    Registre.compterFond();

                    try { fond.Afficher(); } // On tente d'afficher le fond
                    catch // Si il y a eu une erreur
                    {
                        supprimerFichier(true); // On demande à l'utilisateur si il veut supprimer le fichier (et on le liste en tant que fichier illisible

                        lbl_nom.ForeColor = Color.Red;
                        this.Refresh();

                        // On compte l'erreur
                        Registre.compterErreur();
                    }

                    // On recharge les infos
                    rechargerInfo();
                }
                else // Si le fichier est listé en tant que fichier illisible
                {
                    // On affiche son nom en rouge, et on ne le l'affiche pas
                    lbl_nom.ForeColor = Color.Red;
                    rechargerInfo();
                }
            }
        }

        // Met à jour les informations à l'écran
        private void rechargerInfo()
        {
            if (rechargementConstant)
                recupererFichiers(false);

            if (id >= 0)
            {
                lbl_nom.Cursor = Cursors.Hand;
                renommerLeFichierToolStripMenuItem.Enabled = true;

                lbl_chemin.Text = func.traitementChemin(fichiers[id].DirectoryName);
                infobulle_chemin.SetToolTip(this.lbl_chemin, fichiers[id].DirectoryName);

                btn_supprimer.Enabled = true;
                supprimerToolStripMenuItem.Enabled = true;
                mettreEnFondDécranToolStripMenuItem.Enabled = true;

                btn_modifier.Enabled = true;
                btn_modifier.Text = "Modifier";
                modifierToolStripMenuItem.Enabled = true;
                allezÀLimageNuméroToolStripMenuItem.Enabled = true;

                aléatoireToolStripMenuItem.Enabled = true;

                lbl_num.Text = (id + 1).ToString() + " sur " + nbFichier;

                infobulle_nom.SetToolTip(this.lbl_nom, fichiers[id].Name);
                lbl_nom.Text = func.traitementNom(fichiers[id].Name);

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
                lbl_nom.Cursor = Cursors.Default;
                renommerLeFichierToolStripMenuItem.Enabled = false;

                lbl_chemin.Text = func.traitementChemin(chemin);
                infobulle_chemin.SetToolTip(this.lbl_chemin, chemin);

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

            this.Refresh();
        }

        // Fonction liée au bouton suivant
        private void fichierSuivant()
        {
            DialogResult question = new DialogResult();

            if (btn_suivant.Text == "Terminer")
            {
                question = MessageBox.Show("Vous avez fini le dossier !\n\nVoulez-vous fermer le programme ?", "Dossier terminé", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (question == DialogResult.Yes)
                    fermerProgramme();
            }
                
            else
            {
                id++;

                chargerFond();
                rechargerInfo();
            }
        }

        // Fonction liée au bouton précédent
        private void fichierPrecedent()
        {
            id--;

            chargerFond();
            rechargerInfo();
        }

        private void supprimerFichier(bool mode)
        {
            // On créé une boite de dialogue de confirmation
            DialogResult reponse = new DialogResult();

            // Selon le cas, on affiche un message different
            if (!mode)
                reponse = MessageBox.Show("Voulez-vous vraiment supprimer ce fond d'écran ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            else
                reponse = MessageBox.Show("Le fichier '" + fichiers[id].Name + "' n'a pas pu être chargé, voulez-vous le supprimer ?", "Erreur", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (reponse == DialogResult.Yes) // Si la réponse est oui
            {
                File.Delete(fichiers[id].FullName); // On supprime le fichier en question
                nbFichier--;

                recupererFichiers(true); // On refait la liste des fichiers

                // Si il reste des fichiers dans le dossier, on affiche le précédent
                if (nbFichier != 0)
                {
                    if (id == nbFichier)
                        id--;

                    chargerFond();
                }
                else 
                    rechargerInfo();
            }
            else if (reponse == DialogResult.No & mode == true) // Si le fichier est illisible, mais que l'utilisateur ne veut pas le supprimer
            {
                // On le liste dans les fichiers illisibles
                mauvaisFichiers[mid] = fichiers[id].FullName;
                mid++;

                if(rechargementConstant)
                    recupererFichiers(false);
            }
        }

        // Fonction qui réaffiche le fond d'écran initial
        private void ancienFond()
        {
            try // On essaye de remettre le fond
            {
                Wallpaper fond = new Wallpaper(Path.GetTempPath() + @"\GFE_tmp.bmp", func.convertirAffichage(ancienAffichage), couleur);
                fond.Afficher();
            }
            catch (Exception e) // Si il y a eu une erreur, on l'affiche
            {
                MessageBox.Show("Une erreur est survenue durant la remise de votre ancien fond d'écran !\n\n" +
                                "DEBUG :\nFichier : " + Path.GetTempPath() + "GFE_tmp.bmp\nMode : " + ancienAffichage + "\n\nErreur :\n" + e
                                , "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        
        // Fonction de fermeture du programme
        private void fermerProgramme()
        {
            this.Visible = false;

            if (id != -1)
                ancienFond();

            File.Delete(Path.GetTempPath() + @"GFE_tmp.bmp");
            Registre.miseAjourConfig(); 

            this.DestroyHandle();
        }



        // Fonction qui liste les fichiers d'un dossier
        private void recupererFichiers(bool afficherTexte)
        {
            FileInfo[] fichiersInfo = new FileInfo[65535];
            DirectoryInfo[] dossiersInfo = new DirectoryInfo[2048];
            Attente message = new Attente();

            int erreur = 0;
            string ext = "";

            if(afficherTexte)
                message.Show();

            fichiers.Initialize();

            try { fichiersInfo = new DirectoryInfo(chemin).GetFiles(); }
            catch { MessageBox.Show("Impossible de le lire ce dossier !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (sousDossier)
            {
                try
                {
                    fichiersInfo = func.rechercheRecursive(chemin, ref erreur);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Une erreur est survenue à la récupération de la liste des fichiers !\n\nErreur :\n" + e.ToString(), "Erreur durant la récupération des fichiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    premierChargement = false;
                }
            }

            nbFichier = 0;

            foreach (FileInfo fichier in fichiersInfo)
            {
                if (fichier != null)
                {
                    // On récupère l'extension du fichier
                    ext = fichier.Extension.ToLower();

                    // On compare l'extension sans le point à celles autorisées
                    if (ext != "")
                    {
                        if (extension.Split(';').Contains(ext.Substring(1)))
                        {
                            // On ajoute le fichier dans la liste des fichiers autorisés
                            fichiers[nbFichier] = fichier;
                            nbFichier++;
                        }
                    }
                }
            }

            if (erreur > 0 && premierChargement) // Si il y a une erreur durant la lecture d'un sous-dossier
            {
                // MessageBox.Show(erreur.ToString() + " sous-dossiers n'ont pas été lus !\n\nCette erreur est non fatale.", "Erreur de lecture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                erreur = 0;
            }

            premierChargement = false;

            if(afficherTexte)
                message.Close();

            if(Attente.montrerIconeTaskbar)
                Attente.montrerIconeTaskbar = false;
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
            chemin = pChemin;
            this.Icon = Properties.Resources.icone;

            InitializeComponent();
        }

        private void modificationExterne()
        {
            // Création du processus du logiciel externe
            try
            {
                ProcessStartInfo logicielExterne = new ProcessStartInfo(logiciel, "\"" + fichiers[id].FullName + "\"");
                Process proc = new Process();

                proc.StartInfo = logicielExterne;
                proc.Start();

                btn_modifier.Text = "Recharger";
            }
            catch
            {
                MessageBox.Show("Une erreur est survenue durant le lancement du logiciel externe !\n\n" +
                                "Commande executée :\n\"" + logiciel + " " + fichiers[id].FullName + "\"", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void renommerFichier()
        {
            if (id >= 0 && id < nbFichier)
            {
                Renommer.fichier = fichiers[id].Name;
                Renommer fenetre = new Renommer();
                fenetre.ShowDialog();

                if (Renommer.resultat != "")
                {
                    if (Renommer.resultat != fichiers[id].Name)
                    {
                        try
                        {
                            File.Move(fichiers[id].FullName, fichiers[id].DirectoryName + @"\" + Renommer.resultat);
                            fichiers[id] = new FileInfo(fichiers[id].DirectoryName + @"\" + Renommer.resultat);

                            MessageBox.Show("Le fichier a bien été renommé !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        catch
                        {
                            MessageBox.Show("Une erreur est survenue durant la modification du nom !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                rechargerInfo();
            }
        }

        private void Principale_Load(object sender, EventArgs e)
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

            recupererFichiers(true);
            rechargerInfo();

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

        private void btn_modifier_Click(object sender, EventArgs e) { modifierFichier(); }

        private void rechargerLeFondToolStripMenuItem_Click(object sender, EventArgs e) { chargerFond(); }

        private void changerDeDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ancien = chemin;
            bool ancienSousDossier = sousDossier;

            if (id != -1)
                ancienFond();

            Selection fenetre = new Selection();
            fenetre.ShowDialog();

            if (ancien != chemin | ancienSousDossier != sousDossier)
            {
                lbl_nom.Text = "";
                lbl_num.Text = "Création de la liste des fichiers...";
                this.Refresh();

                premierChargement = true;
                mauvaisFichiers.Initialize();

                id = -1;

                recupererFichiers(true);
            }
            else
                chargerFond();

            rechargerInfo();
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

            reponse = MessageBox.Show("Voulez-vous vraiment appliquer cette image en tant que fond d'écran de votre bureau ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (reponse == DialogResult.Yes)
            {
                File.Delete(Path.GetTempPath() + @"GFE_tmp.bmp");

                if (!File.Exists(Path.GetTempPath() + @"GFE_tmp.bmp"))
                    File.Copy(fichiers[id].FullName, Path.GetTempPath() + @"GFE_tmp.bmp");

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

        private void rechargerLaListeDesFichiersToolStripMenuItem_Click(object sender, EventArgs e) { recupererFichiers(true); }

        private void statistiquesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lbl_nom_Click(object sender, EventArgs e) { renommerFichier(); }

        private void renommerLeFichierToolStripMenuItem_Click(object sender, EventArgs e) { renommerFichier(); }

        private void mettreÀJourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ancienFond();
            maj.verifierMaj();
            chargerFond();
        }

        private void précédentToolStripMenuItem_Click(object sender, EventArgs e) { fichierPrecedent(); }
        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e) { supprimerFichier(false); }
        private void aideEnLigneToolStripMenuItem_Click(object sender, EventArgs e) { Process.Start("https://github.com/Penta/GFE/wiki/Accueil"); }

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

                if (id != -1)
                    chargerFond();

                if (Configuration.changementFichier)
                {
                    id = -1;
                    Configuration.changementFichier = false;

                    recupererFichiers(true);
                    ancienFond();
                }
                else if (rechargementConstant)
                    recupererFichiers(true);

                rechargerInfo();
            }
        }
    }
}
