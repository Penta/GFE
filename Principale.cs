using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Drawing.Imaging;
using Gulix.Wallpaper;

namespace Gfe
{
    public partial class Principale : Form
    {
        // VERSION DU LOGICIEL
        public static string VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);

        // Variables globales
        static public int id = -1, nbFichier = 0, mid = 0;
        static public string couleur = "Noir";
        static public string affichage = "Étirer", logiciel = Environment.SystemDirectory + @"\mspaint.exe";
        static public string chemin = string.Empty, ancienAffichage = string.Empty;
        static public bool rappel = true, sousDossier = false, rechargementConstant = false;
        static public string[] mauvaisFichiers = new string[65536];
        static public string extension = "jpg;jpeg;png;bmp;tiff;tif";
        static public bool selectionPremierLancement = true;
        static public string historique = string.Empty;
        static public bool conversion = !Program.nonXP;

        FileInfo[] fichiers = new FileInfo[65536];

        static private bool premierChargement = true;
        static private string cheminAncienFond = string.Empty;
        static private Color ancienneCouleur = Color.Black;


        // Fonction chargeant le fond lié à la variable id dans l'array fichiers
        private void ChargerFond(bool retry)
        {
            if (id >= 0 && id < fichiers.Length)
            {
                // On prépare l'application du fond
                Wallpaper fond = new Wallpaper(fichiers[id].FullName, Func.ConvertirAffichage(affichage), Func.ConvertirCouleur(couleur));

                // Si le fichier actuel n'est pas listé dans les fichiers illisibles
                if (!mauvaisFichiers.Contains(fichiers[id].FullName))
                {
                    labelNom.ForeColor = Color.Black;
                    infobulleNom.SetToolTip(this.labelNom, string.Empty);
                    labelNom.Text = Texte.Chargement;
                    this.Refresh();

                    // On compte le nombre de fond affichés
                    Registre.CompterFond();

                    try { fond.Afficher(conversion); } // On tente d'afficher le fond
                    catch // Si il y a eu une erreur
                    {
                        SupprimerFichier(true); // On demande à l'utilisateur si il veut supprimer le fichier (et on le liste en tant que fichier illisible

                        labelNom.ForeColor = Color.Red;
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
                        labelNom.ForeColor = Color.Red;
                        RechargerInfo();
                    }
                    else
                    {
                        try
                        {
                            fond.Afficher(conversion);
                            labelNom.ForeColor = Color.Black;
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
                labelNom.Cursor = Cursors.Hand;
                sousmenuRenommer.Enabled = true;

                labelChemin.Text = Func.TraitementChemin(fichiers[id].DirectoryName);
                infobulleChemin.SetToolTip(this.labelChemin, fichiers[id].DirectoryName);
                labelChemin.Cursor = Cursors.Hand;

                btnSupprimer.Enabled = true;
                sousmenuSupprimer.Enabled = true;
                sousmenuAppliquer.Enabled = true;

                btnModifier.Enabled = true;
                btnModifier.Text = Texte.Modifier;
                sousmenuModifier.Enabled = true;
                sousmenuAllezA.Enabled = true;

                menuAléatoire.Enabled = true;

                labelNuméro.Text = (id + 1).ToString() + Texte.Fraction + nbFichier;

                infobulleNom.SetToolTip(this.labelNom, fichiers[id].Name);
                labelNom.Text = Func.TraitementNom(fichiers[id].Name);

                sousmenuRechargerFond.Enabled = true;

                if (id + 1 < nbFichier)
                {
                    btnSuivant.Text = Texte.Suivant;
                    sousmenuSuivant.Text = Texte.Suivant;
                }
                else
                {
                    btnSuivant.Text = Texte.Terminer;
                    sousmenuSuivant.Text = Texte.Terminer;
                }

                if (id == 0)
                {
                    btnPrécédent.Enabled = false;
                    sousmenuPrécédent.Enabled = false;
                }
                else
                {
                    btnPrécédent.Enabled = true;
                    sousmenuPrécédent.Enabled = true;
                }
            }
            else if (id == -1)
            {
                labelNom.ForeColor = Color.Black;
                labelNom.Cursor = Cursors.Default;
                sousmenuRenommer.Enabled = false;

                labelChemin.Text = Func.TraitementChemin(chemin);
                infobulleChemin.SetToolTip(this.labelChemin, chemin);
                labelChemin.Cursor = Cursors.Default;

                btnSupprimer.Enabled = false;
                sousmenuSupprimer.Enabled = false;
                sousmenuAppliquer.Enabled = false;

                btnModifier.Enabled = false;
                btnModifier.Text = Texte.Modifier;
                sousmenuModifier.Enabled = false;

                sousmenuRechargerFond.Enabled = false;

                labelNom.Text = Texte.AucunFichierAffiché;
                infobulleNom.SetToolTip(this.labelNom, Texte.InfobulleNomStart);

                if (nbFichier != 0)
                {
                    labelNuméro.Text = Texte.ZeroFraction + nbFichier;

                    btnSuivant.Text = Texte.Commencer;
                    sousmenuSuivant.Text = Texte.Commencer;

                    sousmenuAllezA.Enabled = true;
                    menuAléatoire.Enabled = true;
                }
                else
                {
                    labelNuméro.Text = Texte.AucunFichierDossier;
                    labelNom.Text = null;
                    menuAléatoire.Enabled = false;

                    btnSuivant.Text = Texte.Terminer;
                    sousmenuSuivant.Text = Texte.Terminer;

                    sousmenuAllezA.Enabled = false;
                }

                btnPrécédent.Enabled = false;
                sousmenuPrécédent.Enabled = false;
            }

            this.Refresh();
        }

        // Fonction liée au bouton suivant
        private void FichierSuivant()
        {
            DialogResult question = new DialogResult();

            if (btnSuivant.Text == "Terminer")
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
                reponse = MessageBox.Show(Texte.Suppression, Texte.ConfirmationTitre, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            else
                reponse = MessageBox.Show(fichiers[id].Name + Texte.ErreurChargementFichier, Texte.ErreurTitre, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

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
                if(File.Exists(cheminAncienFond))
                {
                    Wallpaper fond = new Wallpaper(cheminAncienFond, Func.ConvertirAffichage(ancienAffichage), ancienneCouleur);
                    fond.Afficher(false);
                }
                else
                {
                    Wallpaper fond = new Wallpaper(null, Func.ConvertirAffichage(ancienAffichage), ancienneCouleur);
                    fond.Afficher(false, true);

                    if (!string.IsNullOrEmpty(cheminAncienFond))
                        MessageBox.Show(Texte.AncienFondSupprimé, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e) // Si il y a eu une erreur, on l'affiche
            {
                MessageBox.Show(Texte.ErreurAncienFond + "\n\n" +
                                "DEBUG :\nFichier : " + cheminAncienFond + "\nMode : " + ancienAffichage + "\nCouleur :" + ancienneCouleur + "\n\n" + Texte.ErreurTitre + " :\n" + e
                                , Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        // Fonction de fermeture du programme
        private void FermerProgramme()
        {
            this.Visible = false;

            if (id != -1) AncienFond();

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
                catch { MessageBox.Show(Texte.ErreurLectureDossier, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error); }

                if (sousDossier)
                {
                    try
                    {
                        fichiersInfo = Func.RechercheRecursive(emplacement, ref erreur);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(Texte.ErreurLectureListe + emplacement + " !\n\n" + Texte.ErreurTitre + " :\n" + e.ToString(), Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(erreur.ToString() + Texte.ErreurSousDossier, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                erreur = 0;
            }

            premierChargement = false;

            if (afficherTexte)
            {
                try { message.Close(); } catch { /* Je sais pas comment corriger le problème autrement... */ }

                this.Visible = true;
            }
        }

        private void ModifierFichier()
        {
            if (btnModifier.Text == Texte.Modifier)
            {
                ModificationExterne();
            }
            else
            {
                if (labelNom.ForeColor == Color.Black)
                    ChargerFond(false);
                else
                    ChargerFond(true);

                btnModifier.Text = Texte.Modifier;
            }
        }

        public Principale(string pChemin)
        {
            Program.ObtenirLangue();

            chemin = pChemin;
            this.Icon = Properties.Resources.icone;

            Registre.MiseAjourConfig();
            InitializeComponent();
        }

        private void ModificationExterne()
        {
            // Création du processus du logiciel externe
            try
            {
                Process proc = new Process() { StartInfo = new ProcessStartInfo(logiciel, "\"" + fichiers[id].FullName + "\"") };

                proc.Start();
                btnModifier.Text = Texte.Recharger;
            }
            catch
            {
                MessageBox.Show(Texte.ErreurLogicielExterne + "\n\n" +
                                Texte.CommandeExecutée + "\n\"" + logiciel + " " + fichiers[id].FullName + "\"", Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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

                            MessageBox.Show(Texte.FichierRenommé, Texte.SuccèsTitre, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        catch
                        {
                            MessageBox.Show(Texte.ErreurModificationNom, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                RechargerInfo();
            }
        }

        private void VoirDossier()
        {
            string parametre = "";

            if (labelChemin.Text != Texte.UneErreurSurvenue)
            {
                if (nbFichier > 0 && id >= 0 && id < nbFichier)
                    parametre = fichiers[id].DirectoryName;
                else
                    parametre = chemin;

                if (Directory.Exists(parametre))
                {
                    try { Process.Start("explorer.exe", "\"" + parametre + "\""); }
                    catch (Exception err) { MessageBox.Show(Texte.UneErreurSurvenue + ": " + err, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void Principale_Load(object sender, EventArgs e)
        {
            string[] ancienFondEcran = new string[3];
            RegistryKey registre = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            int tmpId = 0;

            ancienneCouleur = ColorTranslator.FromWin32(Convert.ToInt32(NativeMethods.GetSysColor(1)));

            sousmenuContextuel.Checked = Registre.VerifierContextuel();

            ancienFondEcran[0] = registre.GetValue("Wallpaper").ToString();
            ancienFondEcran[1] = registre.GetValue(@"WallpaperStyle").ToString();
            ancienFondEcran[2] = registre.GetValue(@"TileWallpaper").ToString();

            if ((ancienFondEcran[1] == "2") & (ancienFondEcran[2] == "0"))
                ancienAffichage = "etirer";
            else if ((ancienFondEcran[1] == "1") & (ancienFondEcran[2] == "0"))
                ancienAffichage = "centrer";
            else if ((ancienFondEcran[1] == "1") & (ancienFondEcran[2] == "1"))
                ancienAffichage = "mosaique";
            else if ((ancienFondEcran[1] == "10") & (ancienFondEcran[2] == "0"))
                ancienAffichage = "remplir";
            else if ((ancienFondEcran[1] == "22") & (ancienFondEcran[2] == "0"))
                ancienAffichage = "etendre";
            else
                ancienAffichage = "etirer";

            cheminAncienFond = ancienFondEcran[0];

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
                if (!sousmenuRechargerFond.Enabled && id != -1)
                    sousmenuRechargerFond.Enabled = true;

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

        private void MenuAppliquerFond_Clic(object sender, EventArgs e)
        {
            DialogResult reponse = new DialogResult();
            string tempChemin = Program.dossierAppdata + "GFE_fond.bmp";
            Image image;

            reponse = MessageBox.Show(Texte.ConfirmationAppliquerTitre, Texte.ConfirmationTitre, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (reponse == DialogResult.Yes)
            {
                if (conversion)
                {
                    if (!Directory.Exists(Program.dossierAppdata))
                        Directory.CreateDirectory(Program.dossierAppdata);

                    if (File.Exists(tempChemin))
                        File.Delete(tempChemin);

                    image = Image.FromFile(fichiers[id].FullName);
                    image.Save(tempChemin, ImageFormat.Bmp);

                    cheminAncienFond = tempChemin;
                }
                else
                    cheminAncienFond = fichiers[id].FullName;

                ancienAffichage = affichage;
                ancienneCouleur = ColorTranslator.FromWin32(Convert.ToInt32(NativeMethods.GetSysColor(1)));

                MessageBox.Show(Texte.FondAppliqué, Texte.SuccèsTitre, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (sousmenuContextuel.Checked)
                {
                    Registre.RetirerContextuel();
                    sousmenuContextuel.Checked = false;
                    File.Delete(cheminIcone + "icone.ico");

                    MessageBox.Show(Texte.MenuContextuelSupprimé, Texte.SuccèsTitre, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Registre.AjouterContextuel();
                    sousmenuContextuel.Checked = true;

                    if (!Directory.Exists(cheminIcone))
                        Directory.CreateDirectory(cheminIcone);

                    if (!File.Exists(cheminIcone + "icone.ico"))
                    {
                        FileStream fs = new FileStream(cheminIcone + "icone.ico", FileMode.Create);
                        Properties.Resources.icone.Save(fs);
                        fs.Close();
                    }

                    MessageBox.Show(Texte.MenuContextuelAppliqué, Texte.SuccèsTitre, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err) { MessageBox.Show(Texte.ErreurMenuContextuel + err.ToString(), Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Hand); }
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
