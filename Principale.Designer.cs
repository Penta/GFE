namespace Gfe
{
    partial class Principale
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principale));
            this.btnSuivant = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuSuivant = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuPrécédent = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuSupprimer = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuModifier = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sousmenuRenommer = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuRechargerFond = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuAppliquer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sousmenuVoirDossier = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuChangerDossier = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuAllezA = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuMinimiser = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuRechargerListe = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sousmenuQuitter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.sousmenuMaj = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuContextuel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.sousmenuStatistiques = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAléatoire = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuestion = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuAide = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.sousmenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.labelInfoChemin = new System.Windows.Forms.Label();
            this.labelInfoNom = new System.Windows.Forms.Label();
            this.btnPrécédent = new System.Windows.Forms.Button();
            this.labelChemin = new System.Windows.Forms.Label();
            this.labelNom = new System.Windows.Forms.Label();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.labelInfoFichier = new System.Windows.Forms.Label();
            this.labelNuméro = new System.Windows.Forms.Label();
            this.infobulleNom = new System.Windows.Forms.ToolTip(this.components);
            this.btnModifier = new System.Windows.Forms.Button();
            this.infobulleChemin = new System.Windows.Forms.ToolTip(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSuivant
            // 
            resources.ApplyResources(this.btnSuivant, "btnSuivant");
            this.btnSuivant.Name = "btnSuivant";
            this.infobulleNom.SetToolTip(this.btnSuivant, resources.GetString("btnSuivant.ToolTip"));
            this.infobulleChemin.SetToolTip(this.btnSuivant, resources.GetString("btnSuivant.ToolTip1"));
            this.btnSuivant.UseVisualStyleBackColor = true;
            this.btnSuivant.Click += new System.EventHandler(this.BoutonSuivant_Clic);
            // 
            // menu
            // 
            resources.ApplyResources(this.menu, "menu");
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAction,
            this.menuOption,
            this.menuAléatoire,
            this.menuQuestion});
            this.menu.Name = "menu";
            this.infobulleChemin.SetToolTip(this.menu, resources.GetString("menu.ToolTip"));
            this.infobulleNom.SetToolTip(this.menu, resources.GetString("menu.ToolTip1"));
            // 
            // menuAction
            // 
            resources.ApplyResources(this.menuAction, "menuAction");
            this.menuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sousmenuSuivant,
            this.sousmenuPrécédent,
            this.sousmenuSupprimer,
            this.sousmenuModifier,
            this.toolStripSeparator4,
            this.sousmenuRenommer,
            this.sousmenuRechargerFond,
            this.sousmenuAppliquer,
            this.toolStripSeparator1,
            this.sousmenuVoirDossier,
            this.sousmenuChangerDossier,
            this.sousmenuAllezA,
            this.sousmenuMinimiser,
            this.sousmenuRechargerListe,
            this.toolStripSeparator2,
            this.sousmenuQuitter});
            this.menuAction.Name = "menuAction";
            // 
            // sousmenuSuivant
            // 
            resources.ApplyResources(this.sousmenuSuivant, "sousmenuSuivant");
            this.sousmenuSuivant.Name = "sousmenuSuivant";
            this.sousmenuSuivant.Click += new System.EventHandler(this.MenuSuivant_Clic);
            // 
            // sousmenuPrécédent
            // 
            resources.ApplyResources(this.sousmenuPrécédent, "sousmenuPrécédent");
            this.sousmenuPrécédent.Name = "sousmenuPrécédent";
            this.sousmenuPrécédent.Click += new System.EventHandler(this.MenuPrécédent_Clic);
            // 
            // sousmenuSupprimer
            // 
            resources.ApplyResources(this.sousmenuSupprimer, "sousmenuSupprimer");
            this.sousmenuSupprimer.Name = "sousmenuSupprimer";
            this.sousmenuSupprimer.Click += new System.EventHandler(this.MenuSupprimer_Clic);
            // 
            // sousmenuModifier
            // 
            resources.ApplyResources(this.sousmenuModifier, "sousmenuModifier");
            this.sousmenuModifier.Name = "sousmenuModifier";
            this.sousmenuModifier.Click += new System.EventHandler(this.MenuModifier_Clic);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // sousmenuRenommer
            // 
            resources.ApplyResources(this.sousmenuRenommer, "sousmenuRenommer");
            this.sousmenuRenommer.Name = "sousmenuRenommer";
            this.sousmenuRenommer.Click += new System.EventHandler(this.MenuRenommer_Clic);
            // 
            // sousmenuRechargerFond
            // 
            resources.ApplyResources(this.sousmenuRechargerFond, "sousmenuRechargerFond");
            this.sousmenuRechargerFond.Name = "sousmenuRechargerFond";
            this.sousmenuRechargerFond.Click += new System.EventHandler(this.MenuRecharger_Clic);
            // 
            // sousmenuAppliquer
            // 
            resources.ApplyResources(this.sousmenuAppliquer, "sousmenuAppliquer");
            this.sousmenuAppliquer.Name = "sousmenuAppliquer";
            this.sousmenuAppliquer.Click += new System.EventHandler(this.MenuAppliquerFond_Clic);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // sousmenuVoirDossier
            // 
            resources.ApplyResources(this.sousmenuVoirDossier, "sousmenuVoirDossier");
            this.sousmenuVoirDossier.Name = "sousmenuVoirDossier";
            this.sousmenuVoirDossier.Click += new System.EventHandler(this.MenuVoirLeContenu_Clic);
            // 
            // sousmenuChangerDossier
            // 
            resources.ApplyResources(this.sousmenuChangerDossier, "sousmenuChangerDossier");
            this.sousmenuChangerDossier.Name = "sousmenuChangerDossier";
            this.sousmenuChangerDossier.Click += new System.EventHandler(this.MenuChangerDossier_Clic);
            // 
            // sousmenuAllezA
            // 
            resources.ApplyResources(this.sousmenuAllezA, "sousmenuAllezA");
            this.sousmenuAllezA.Name = "sousmenuAllezA";
            this.sousmenuAllezA.Click += new System.EventHandler(this.MenuAllezÀ_Clic);
            // 
            // sousmenuMinimiser
            // 
            resources.ApplyResources(this.sousmenuMinimiser, "sousmenuMinimiser");
            this.sousmenuMinimiser.Name = "sousmenuMinimiser";
            this.sousmenuMinimiser.Click += new System.EventHandler(this.MenuMinimiser_Clic);
            // 
            // sousmenuRechargerListe
            // 
            resources.ApplyResources(this.sousmenuRechargerListe, "sousmenuRechargerListe");
            this.sousmenuRechargerListe.Name = "sousmenuRechargerListe";
            this.sousmenuRechargerListe.Click += new System.EventHandler(this.MenuRechargerListe_Clic);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // sousmenuQuitter
            // 
            resources.ApplyResources(this.sousmenuQuitter, "sousmenuQuitter");
            this.sousmenuQuitter.Name = "sousmenuQuitter";
            this.sousmenuQuitter.Click += new System.EventHandler(this.MenuQuitter_Clic);
            // 
            // menuOption
            // 
            resources.ApplyResources(this.menuOption, "menuOption");
            this.menuOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sousmenuConfiguration,
            this.toolStripSeparator3,
            this.sousmenuMaj,
            this.sousmenuContextuel,
            this.toolStripSeparator5,
            this.sousmenuStatistiques});
            this.menuOption.Name = "menuOption";
            // 
            // sousmenuConfiguration
            // 
            resources.ApplyResources(this.sousmenuConfiguration, "sousmenuConfiguration");
            this.sousmenuConfiguration.Name = "sousmenuConfiguration";
            this.sousmenuConfiguration.Click += new System.EventHandler(this.MenuConfiguration_Clic);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // sousmenuMaj
            // 
            resources.ApplyResources(this.sousmenuMaj, "sousmenuMaj");
            this.sousmenuMaj.Name = "sousmenuMaj";
            this.sousmenuMaj.Click += new System.EventHandler(this.MenuMettreAJour_Clic);
            // 
            // sousmenuContextuel
            // 
            resources.ApplyResources(this.sousmenuContextuel, "sousmenuContextuel");
            this.sousmenuContextuel.Name = "sousmenuContextuel";
            this.sousmenuContextuel.Click += new System.EventHandler(this.MenuAjouterContextuel_Clic);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // sousmenuStatistiques
            // 
            resources.ApplyResources(this.sousmenuStatistiques, "sousmenuStatistiques");
            this.sousmenuStatistiques.Name = "sousmenuStatistiques";
            this.sousmenuStatistiques.Click += new System.EventHandler(this.MenuStatistique_Clic);
            // 
            // menuAléatoire
            // 
            resources.ApplyResources(this.menuAléatoire, "menuAléatoire");
            this.menuAléatoire.Name = "menuAléatoire";
            this.menuAléatoire.Click += new System.EventHandler(this.MenuAléatoire_Clic);
            // 
            // menuQuestion
            // 
            resources.ApplyResources(this.menuQuestion, "menuQuestion");
            this.menuQuestion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sousmenuAide,
            this.sousmenuVersion,
            this.sousmenuAbout});
            this.menuQuestion.Name = "menuQuestion";
            // 
            // sousmenuAide
            // 
            resources.ApplyResources(this.sousmenuAide, "sousmenuAide");
            this.sousmenuAide.Name = "sousmenuAide";
            this.sousmenuAide.Click += new System.EventHandler(this.MenuAide_Clic);
            // 
            // sousmenuVersion
            // 
            resources.ApplyResources(this.sousmenuVersion, "sousmenuVersion");
            this.sousmenuVersion.Name = "sousmenuVersion";
            this.sousmenuVersion.Click += new System.EventHandler(this.MenuNoteDeVersion_Clic);
            // 
            // sousmenuAbout
            // 
            resources.ApplyResources(this.sousmenuAbout, "sousmenuAbout");
            this.sousmenuAbout.Name = "sousmenuAbout";
            this.sousmenuAbout.Click += new System.EventHandler(this.MenuAPropos_Clic);
            // 
            // labelInfoChemin
            // 
            resources.ApplyResources(this.labelInfoChemin, "labelInfoChemin");
            this.labelInfoChemin.Name = "labelInfoChemin";
            this.infobulleNom.SetToolTip(this.labelInfoChemin, resources.GetString("labelInfoChemin.ToolTip"));
            this.infobulleChemin.SetToolTip(this.labelInfoChemin, resources.GetString("labelInfoChemin.ToolTip1"));
            // 
            // labelInfoNom
            // 
            resources.ApplyResources(this.labelInfoNom, "labelInfoNom");
            this.labelInfoNom.Name = "labelInfoNom";
            this.infobulleNom.SetToolTip(this.labelInfoNom, resources.GetString("labelInfoNom.ToolTip"));
            this.infobulleChemin.SetToolTip(this.labelInfoNom, resources.GetString("labelInfoNom.ToolTip1"));
            // 
            // btnPrécédent
            // 
            resources.ApplyResources(this.btnPrécédent, "btnPrécédent");
            this.btnPrécédent.Name = "btnPrécédent";
            this.infobulleNom.SetToolTip(this.btnPrécédent, resources.GetString("btnPrécédent.ToolTip"));
            this.infobulleChemin.SetToolTip(this.btnPrécédent, resources.GetString("btnPrécédent.ToolTip1"));
            this.btnPrécédent.UseVisualStyleBackColor = true;
            this.btnPrécédent.Click += new System.EventHandler(this.BoutonPrécédent_Clic);
            // 
            // labelChemin
            // 
            resources.ApplyResources(this.labelChemin, "labelChemin");
            this.labelChemin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelChemin.Name = "labelChemin";
            this.infobulleNom.SetToolTip(this.labelChemin, resources.GetString("labelChemin.ToolTip"));
            this.infobulleChemin.SetToolTip(this.labelChemin, resources.GetString("labelChemin.ToolTip1"));
            this.labelChemin.Click += new System.EventHandler(this.LabelCheminClic);
            // 
            // labelNom
            // 
            resources.ApplyResources(this.labelNom, "labelNom");
            this.labelNom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNom.Name = "labelNom";
            this.infobulleNom.SetToolTip(this.labelNom, resources.GetString("labelNom.ToolTip"));
            this.infobulleChemin.SetToolTip(this.labelNom, resources.GetString("labelNom.ToolTip1"));
            this.labelNom.Click += new System.EventHandler(this.LabelNomClic);
            // 
            // btnSupprimer
            // 
            resources.ApplyResources(this.btnSupprimer, "btnSupprimer");
            this.btnSupprimer.Name = "btnSupprimer";
            this.infobulleNom.SetToolTip(this.btnSupprimer, resources.GetString("btnSupprimer.ToolTip"));
            this.infobulleChemin.SetToolTip(this.btnSupprimer, resources.GetString("btnSupprimer.ToolTip1"));
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.BoutonSupprimer_Clic);
            // 
            // labelInfoFichier
            // 
            resources.ApplyResources(this.labelInfoFichier, "labelInfoFichier");
            this.labelInfoFichier.Name = "labelInfoFichier";
            this.infobulleNom.SetToolTip(this.labelInfoFichier, resources.GetString("labelInfoFichier.ToolTip"));
            this.infobulleChemin.SetToolTip(this.labelInfoFichier, resources.GetString("labelInfoFichier.ToolTip1"));
            // 
            // labelNuméro
            // 
            resources.ApplyResources(this.labelNuméro, "labelNuméro");
            this.labelNuméro.Name = "labelNuméro";
            this.infobulleNom.SetToolTip(this.labelNuméro, resources.GetString("labelNuméro.ToolTip"));
            this.infobulleChemin.SetToolTip(this.labelNuméro, resources.GetString("labelNuméro.ToolTip1"));
            // 
            // btnModifier
            // 
            resources.ApplyResources(this.btnModifier, "btnModifier");
            this.btnModifier.Name = "btnModifier";
            this.infobulleNom.SetToolTip(this.btnModifier, resources.GetString("btnModifier.ToolTip"));
            this.infobulleChemin.SetToolTip(this.btnModifier, resources.GetString("btnModifier.ToolTip1"));
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.BoutonModifier_Clic);
            // 
            // Principale
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.labelNuméro);
            this.Controls.Add(this.labelInfoFichier);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.labelNom);
            this.Controls.Add(this.labelChemin);
            this.Controls.Add(this.btnPrécédent);
            this.Controls.Add(this.labelInfoNom);
            this.Controls.Add(this.labelInfoChemin);
            this.Controls.Add(this.btnSuivant);
            this.Controls.Add(this.menu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "Principale";
            this.infobulleChemin.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.infobulleNom.SetToolTip(this, resources.GetString("$this.ToolTip1"));
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Principale_FormClosed);
            this.Load += new System.EventHandler(this.Principale_Load);
            this.Resize += new System.EventHandler(this.Principale_Resize);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSuivant;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuAction;
        private System.Windows.Forms.ToolStripMenuItem sousmenuSuivant;
        private System.Windows.Forms.ToolStripMenuItem sousmenuPrécédent;
        private System.Windows.Forms.ToolStripMenuItem sousmenuSupprimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sousmenuQuitter;
        private System.Windows.Forms.ToolStripMenuItem menuOption;
        private System.Windows.Forms.ToolStripMenuItem sousmenuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem sousmenuMaj;
        private System.Windows.Forms.ToolStripMenuItem menuQuestion;
        private System.Windows.Forms.ToolStripMenuItem sousmenuAide;
        private System.Windows.Forms.ToolStripMenuItem sousmenuAbout;
        private System.Windows.Forms.Label labelInfoChemin;
        private System.Windows.Forms.Label labelInfoNom;
        private System.Windows.Forms.Button btnPrécédent;
        private System.Windows.Forms.Label labelChemin;
        private System.Windows.Forms.Label labelNom;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Label labelInfoFichier;
        private System.Windows.Forms.Label labelNuméro;
        private System.Windows.Forms.ToolTip infobulleNom;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.ToolStripMenuItem sousmenuModifier;
        private System.Windows.Forms.ToolStripMenuItem sousmenuChangerDossier;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem sousmenuAllezA;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem sousmenuStatistiques;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem sousmenuRechargerFond;
        private System.Windows.Forms.ToolStripMenuItem sousmenuAppliquer;
        private System.Windows.Forms.ToolStripMenuItem sousmenuMinimiser;
        private System.Windows.Forms.ToolStripMenuItem menuAléatoire;
        private System.Windows.Forms.ToolTip infobulleChemin;
        private System.Windows.Forms.ToolStripMenuItem sousmenuRechargerListe;
        private System.Windows.Forms.ToolStripMenuItem sousmenuRenommer;
        private System.Windows.Forms.ToolStripMenuItem sousmenuContextuel;
        private System.Windows.Forms.ToolStripMenuItem sousmenuVoirDossier;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem sousmenuVersion;
    }
}