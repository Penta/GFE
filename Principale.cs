using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Linq;
using System.Reflection;
using System.Threading;

// Classe sous licence GNU GPLv3
using Gulix.Wallpaper;

namespace Gfe
{
    public partial class Principale : Form
    {
        // VERSION DU LOGICIEL
        public static string VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);

        // Variables globales
        static public int id = -1, nbFichier = 0, mid = 0;
        static public Color couleur = Color.Black;
        static public string affichage = "Étirer", logiciel = Environment.SystemDirectory + @"\mspaint.exe";
        static public string chemin = null, ancienAffichage = null;
        static public bool rappel = true, sousDossier = false, rechargementConstant = false;
        static public string[] mauvaisFichiers = new string[65536], AncienfondEcran = new string[3];
        static public string extension = "jpg;jpeg;png;bmp;tiff;tif";
        static public bool selectionPremierLancement = true;
        FileInfo[] fichiers = new FileInfo[65536];

        static private bool premierChargement = true;

        // Fonction chargeant le fond lié à la variable id dans l'array fichiers
        private void ChargerFond(bool retry)
        {
            if (id >= 0 && id < fichiers.Length)
            {
                // On prépare l'application du fond
                Wallpaper fond = new Wallpaper(fichiers[id].FullName, Func.ConvertirAffichage(affichage), couleur);

                // Si le fichier actuel n'est pas listé dans les fichiers illisibles
                if (!mauvaisFichiers.Contains(fichiers[id].FullName))
                {
                    lbl_nom.ForeColor = Color.Black;
                    infobulle_nom.SetToolTip(this.lbl_nom, "");
                    lbl_nom.Text = "Chargement...";
                    this.Refresh();

                    // On compte le nombre de fond affichés
                    Registre.CompterFond();

                    try { fond.Afficher(); } // On tente d'afficher le fond
                    catch // Si il y a eu une erreur
                    {
                        SupprimerFichier(true); // On demande à l'utilisateur si il veut supprimer le fichier (et on le liste en tant que fichier illisible

                        lbl_nom.ForeColor = Color.Red;
                        this.Refresh();

                        // On compte l'erreur
                        Registre.CompterErreur();
                    }

                    // On recharge les infos
                    RechargerInfo();
                }
                else // Si le fichier est listé en tant que fichier illisible
                {
                    if (!retry)
                    {
                        // On affiche son nom en rouge, et on ne le l'affiche pas
                        lbl_nom.ForeColor = Color.Red;
                        RechargerInfo();
                    }
                    else
                    {
                        try
                        {
                            fond.Afficher();
                            lbl_nom.ForeColor = Color.Black;
                            mauvaisFichiers = mauvaisFichiers.Where(val => val != fichiers[id].FullName).ToArray();
                            RechargerInfo();
                            Registre.CompterFond();
                        }
                        catch {}
                    }
                }
            }
        }

        // Met à jour les informations à l'écran
        private void RechargerInfo()
        {
            if (rechargementConstant)
                RecupererFichiers(false);

            if (id >= 0)
            {
                lbl_nom.Cursor = Cursors.Hand;
                renommerLeFichierToolStripMenuItem.Enabled = true;

                lbl_chemin.Text = Func.TraitementChemin(fichiers[id].DirectoryName);
                infobulle_chemin.SetToolTip(this.lbl_chemin, fichiers[id].DirectoryName);
                lbl_chemin.Cursor = Cursors.Hand;

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
                lbl_nom.Text = Func.TraitementNom(fichiers[id].Name);

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
                lbl_nom.ForeColor = Color.Black;
                lbl_nom.Cursor = Cursors.Default;
                renommerLeFichierToolStripMenuItem.Enabled = false;

                lbl_chemin.Text = Func.TraitementChemin(chemin);
                infobulle_chemin.SetToolTip(this.lbl_chemin, chemin);
                lbl_chemin.Cursor = Cursors.Default;

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
        private void FichierSuivant()
        {
            DialogResult question = new DialogResult();

            if (btn_suivant.Text == "Terminer")
            {
                question = MessageBox.Show("Vous avez fini le dossier !\n\nVoulez-vous fermer le programme ?", "Dossier terminé", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (question == DialogResult.Yes)
                    FermerProgramme();
            }
                
            else
            {
                id++;

                ChargerFond(false);
                RechargerInfo();
            }
        }

        // Fonction liée au bouton précédent
        private void FichierPrecedent()
        {
            id--;

            ChargerFond(false);
            RechargerInfo();
        }

        private void SupprimerFichier(bool mode)
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
                if(File.Exists(fichiers[id].FullName)) // On vérifie que le fichier existe
                    File.Delete(fichiers[id].FullName); // On supprime le fichier en question

                nbFichier--;

                fichiers = fichiers.Where(val => val != fichiers[id]).ToArray();

                // Si il reste des fichiers dans le dossier, on affiche le précédent
                if (nbFichier != 0)
                {
                    if (id == nbFichier)
                        id--;

                    ChargerFond(false);
                }
                else
                {
                    id = -1;
                    RechargerInfo();
                }
            }
            else if (reponse == DialogResult.No & mode == true) // Si le fichier est illisible, mais que l'utilisateur ne veut pas le supprimer
            {
                // On le liste dans les fichiers illisibles
                mauvaisFichiers[mid] = fichiers[id].FullName;
                mid++;

                if(rechargementConstant)
                    RecupererFichiers(true);
            }
        }

        // Fonction qui réaffiche le fond d'écran initial
        public static void AncienFond()
        {
            try // On essaye de remettre le fond
            {
                Wallpaper fond = new Wallpaper(Path.GetTempPath() + @"\GFE_tmp.bmp", Func.ConvertirAffichage(ancienAffichage), couleur);
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
        private void FermerProgramme()
        {
            this.Visible = false;

            if (id != -1) AncienFond();

            File.Delete(Path.GetTempPath() + @"GFE_tmp.bmp");
            Registre.MiseAjourConfig();
            Registre.registre.Close();

            this.DestroyHandle();
        }

        // Fonction qui liste les fichiers d'un dossier
        private void RecupererFichiers(bool afficherTexte)
        {
            FileInfo[] fichiersInfo = new FileInfo[65535];
            Attente message = new Attente();

            int erreur = 0;
            string ext = null;

            if (afficherTexte)
            {
                this.Visible= false;
                message.Show();
                Thread.Sleep(333);
            }

            fichiers.Initialize();
            nbFichier = 0;

            foreach (string emplacement in Func.ListeChemins(chemin, sousDossier))
            {
                try { fichiersInfo = new DirectoryInfo(emplacement).GetFiles(); }
                catch { MessageBox.Show("Impossible de le lire ce dossier !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                if (sousDossier)
                {
                    try
                    {
                        fichiersInfo = Func.RechercheRecursive(emplacement, ref erreur);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Une erreur est survenue à la récupération de la liste des fichiers !\n\nErreur :\n" + e.ToString(), "Erreur durant la récupération des fichiers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        premierChargement = false;
                    }
                }

                foreach (FileInfo fichier in fichiersInfo)
                {
                    if (fichier != null)
                    {
                        // On récupère l'extension du fichier
                        ext = fichier.Extension.ToLower();

                        // On compare l'extension sans le point à celles autorisées
                        if (!string.IsNullOrEmpty(ext))
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
            }

            if (erreur > 0 && premierChargement) // Si il y a une erreur durant la lecture d'un sous-dossier
            {
                MessageBox.Show(erreur.ToString() + " sous-dossiers n'ont pas été lus !\n\nCette erreur est non fatale.", "Erreur de lecture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                erreur = 0;
            }

            premierChargement = false;

            if (afficherTexte)
            {
                message.Close();
                this.Visible = true;
            }
        }

        private void ModifierFichier()
        {
            if (btn_modifier.Text == "Modifier")
            {
                ModificationExterne();
            }
            else
            {
                if (lbl_nom.ForeColor == Color.Black)
                    ChargerFond(false);
                else
                    ChargerFond(true);

                btn_modifier.Text = "Modifier";
            }
        }

        public Principale(string pChemin)
        {
            chemin = pChemin;
            this.Icon = Properties.Resources.icone;

            InitializeComponent();
        }

        private void ModificationExterne()
        {
            // Création du processus du logiciel externe
            try
            {
                Process proc = new Process() { StartInfo = new ProcessStartInfo(logiciel, "\"" + fichiers[id].FullName + "\"") };

                proc.Start();
                btn_modifier.Text = "Recharger";
            }
            catch
            {
                MessageBox.Show("Une erreur est survenue durant le lancement du logiciel externe !\n\n" +
                                "Commande executée :\n\"" + logiciel + " " + fichiers[id].FullName + "\"", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void RenommerFichier()
        {
            int mId = -1;

            if (id >= 0 && id < nbFichier)
            {
                Renommer.fichier = fichiers[id].Name;
                Renommer fenetre = new Renommer();
                fenetre.ShowDialog();

                mId = Array.FindIndex(mauvaisFichiers, x => x == fichiers[id].FullName);

                if (!string.IsNullOrEmpty(Renommer.resultat))
                {
                    if (Renommer.resultat != fichiers[id].Name)
                    {
                        try
                        {
                            File.Move(fichiers[id].FullName, fichiers[id].DirectoryName + @"\" + Renommer.resultat);
                            fichiers[id] = new FileInfo(fichiers[id].DirectoryName + @"\" + Renommer.resultat);

                            if (mId >= 0)
                                mauvaisFichiers[mId] = fichiers[id].FullName;

                            MessageBox.Show("Le fichier a bien été renommé !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        catch
                        {
                            MessageBox.Show("Une erreur est survenue durant la modification du nom !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                RechargerInfo();
            }
        }

        private void VoirDossier()
        {
            string parametre = "";

            if (lbl_chemin.Text != "Une erreur est survenue.")
            {
                if (nbFichier > 0 && id >= 0 && id < nbFichier)
                    parametre = fichiers[id].DirectoryName;
                else
                    parametre = chemin;

                if (Directory.Exists(parametre))
                {
                    try { Process.Start("explorer.exe", "\"" + parametre + "\""); }
                    catch (Exception err) { MessageBox.Show("Une erreur est survenue à l'ouverture du dossier '" + parametre + "' dans l'explorateur de fichier.\n\nErreur :\n" + err, "Erreur durant l'ouverture de l'explorateur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void Principale_Load(object sender, EventArgs e)
        {
            RegistryKey registre = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            int tmpId = 0;

            ajouterAuMenuContextuelToolStripMenuItem.Checked = Registre.VerifierContextuel();

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

            RecupererFichiers(true);
            RechargerInfo();

            if (rappel && Registre.ancienChemin == chemin)
            {
                tmpId = Registre.ancienId;

                if (nbFichier > 0 && tmpId < nbFichier - 1 && tmpId >= 0)
                {
                    id = tmpId;

                    RechargerInfo();
                    ChargerFond(false);
                }
            }
        }

        private void MenuAllezÀ_Clic (object sender, EventArgs e)
        {
            int ancientId = id;

            Saut fenetre = new Saut();
            fenetre.ShowDialog();

            if (ancientId != id)
            {
                if (!rechargerLeFondToolStripMenuItem.Enabled && id != -1)
                    rechargerLeFondToolStripMenuItem.Enabled = true;

                ChargerFond(false);
            }
        }

        private void MenuChangerDossier_Clic (object sender, EventArgs e)
        {
            string ancien = chemin;
            bool ancienSousDossier = sousDossier;

            if (id != -1)
                AncienFond();

            Selection fenetre = new Selection();
            fenetre.ShowDialog();

            if (ancien != chemin | ancienSousDossier != sousDossier)
            {
                premierChargement = true;
                mauvaisFichiers.Initialize();

                id = -1;

                RecupererFichiers(true);
            }
            else
                ChargerFond(false);

            RechargerInfo();
        }

        private void MenuAléatoire_Clic(object sender, EventArgs e)
        {
            Random rndId = new Random();
            int tmpId = id;

            id = rndId.Next(nbFichier);

            // On vérifie que l'id n'est pas le même que précédent, qu'il y a plus de 1 fichier et que le fichier n'est pas dans la liste des mauvais fichiers
            while ((tmpId == id && nbFichier >= 2) | (Array.FindIndex(mauvaisFichiers, x => x == fichiers[id].FullName) >= 0 && mauvaisFichiers.Count(s => s != null) + 1 < nbFichier))
                id = rndId.Next(nbFichier);

            ChargerFond(false);
            RechargerInfo();
        }

        private void MenuMettreEnFond_Clic(object sender, EventArgs e)
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

        private void MenuMinimiser_Clic(object sender, EventArgs e)
        {
            this.Visible = false;

            // On réduit toutes les fenêtres
            Type typeShell = Type.GetTypeFromProgID("Shell.Application");
            object objShell = Activator.CreateInstance(typeShell);
            typeShell.InvokeMember("MinimizeAll", System.Reflection.BindingFlags.InvokeMethod, null, objShell, null);

            // Et on réaffiche celle là
            this.Show();
    }

        private void MenuRechargerListe_Clic(object sender, EventArgs e)
        {
            RecupererFichiers(true);
            RechargerInfo();
        }

        private void MenuStatistique_Clic(object sender, EventArgs e)
        {

        }

        private void LabelNomClic(object sender, EventArgs e) { RenommerFichier(); }

        private void MenuRenommer_Clic(object sender, EventArgs e) { RenommerFichier(); }

        // On raffraichi la fenêtre quand elle est restaurée pour éviter du noir tout moche partout
        private void Principale_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.Refresh();
        }

        private void MenuAjouterContextuel_Clic(object sender, EventArgs e)
        {
            string cheminIcone = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Gestionnaire de Fond d'Écran\";

            try
            {
                if (ajouterAuMenuContextuelToolStripMenuItem.Checked)
                {
                    Registre.RetirerContextuel();
                    ajouterAuMenuContextuelToolStripMenuItem.Checked = false;
                    File.Delete(cheminIcone + "icone.ico");

                    MessageBox.Show("L'entrée dans le menu contextuel a bien été supprimée.", "Désactivation de l'entrée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Registre.AjouterContextuel();
                    ajouterAuMenuContextuelToolStripMenuItem.Checked = true;

                    if (!Directory.Exists(cheminIcone))
                        Directory.CreateDirectory(cheminIcone);

                    if (!File.Exists(cheminIcone + "icone.ico"))
                    {
                        FileStream fs = new FileStream(cheminIcone + "icone.ico", FileMode.Create);
                        Properties.Resources.icone.Save(fs);
                        fs.Close();
                    }

                    MessageBox.Show("Vous pouvez maintenant lancer le Gestionnaire de Fond d'Écran dans le menu contextuel d'un dossier ! (clic droit)\n\nVeuillez désactiver cette option avant de supprimer ou déplacer cet exécutable.", "Bravo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err) { MessageBox.Show("Une erreur est surevenue durant la modification du registre !\n\nErreur :\n" + err.ToString(), "Erreur durant l'ajout au menu contextuel", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        private void MenuMettreAJour_Clic(object sender, EventArgs e)
        {
            Maj.VerifierMaj();
            ChargerFond(false);
        }

        private void MenuAPropos_Clic(object sender, EventArgs e)
        {
            APropos fenetre = new APropos();
            fenetre.ShowDialog();
        }

        private void MenuConfiguration_Clic(object sender, EventArgs e)
        {
            ConfigurationGfe fenetre = new ConfigurationGfe();
            fenetre.ShowDialog();

            if (ConfigurationGfe.changement)
            {
                ConfigurationGfe.changement = false;

                if (ConfigurationGfe.changementFichier)
                {
                    id = -1;
                    ConfigurationGfe.changementFichier = false;

                    AncienFond();

                    RecupererFichiers(true);
                }
                else
                {
                    if (id != -1)
                        ChargerFond(true);

                    if (rechargementConstant)
                        RecupererFichiers(false);
                }

                RechargerInfo();
            }
        }

        private void MenuPrécédent_Clic (object sender, EventArgs e) { FichierPrecedent(); }
        private void MenuSupprimer_Clic (object sender, EventArgs e) { SupprimerFichier(false); }
        private void MenuAide_Clic (object sender, EventArgs e) { Process.Start("https://github.com/Penta/GFE/wiki/Accueil"); }
        private void Principale_FormClosed (object sender, FormClosedEventArgs e) { FermerProgramme(); }
        private void MenuVoirLeContenu_Clic (object sender, EventArgs e) { VoirDossier(); }
        private void MenuNoteDeVersion_Clic (object sender, EventArgs e) { Process.Start("https://github.com/Penta/GFE/releases/tag/" + VERSION); }
        private void LabelCheminClic (object sender, EventArgs e) { VoirDossier(); }
        private void BoutonSuivant_Clic (object sender, EventArgs e) { FichierSuivant(); }
        private void BoutonPrécédent_Clic (object sender, EventArgs e) { FichierPrecedent(); }
        private void BoutonSupprimer_Clic (object sender, EventArgs e) { SupprimerFichier(false); }
        private void MenuSuivant_Clic (object sender, EventArgs e) { FichierSuivant(); }
        private void MenuQuitter_Clic (object sender, EventArgs e) { FermerProgramme(); }
        private void BoutonModifier_Clic (object sender, EventArgs e) { ModifierFichier(); }
        private void MenuRecharger_Clic (object sender, EventArgs e) { ChargerFond(true); }
        private void MenuModifier_Clic (object sender, EventArgs e) { ModificationExterne(); }
    }
}
