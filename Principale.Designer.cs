namespace Gestionnaire_de_Fond_d_Écran
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
            this.btn_suivant = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suivantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.précédentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rechargerLeFondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mettreEnFondDécranToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.changerDeDossierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allezÀLimageNuméroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimiserToutesLesFenêtresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mettreÀJourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.statistiquesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aléatoireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aideEnLigneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_info_chemin = new System.Windows.Forms.Label();
            this.lbl_info_nom = new System.Windows.Forms.Label();
            this.btn_precedent = new System.Windows.Forms.Button();
            this.lbl_chemin = new System.Windows.Forms.Label();
            this.lbl_nom = new System.Windows.Forms.Label();
            this.btn_supprimer = new System.Windows.Forms.Button();
            this.lbl_info_num = new System.Windows.Forms.Label();
            this.lbl_num = new System.Windows.Forms.Label();
            this.infobulle_nom = new System.Windows.Forms.ToolTip(this.components);
            this.btn_modifier = new System.Windows.Forms.Button();
            this.infobulle_chemin = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_suivant
            // 
            resources.ApplyResources(this.btn_suivant, "btn_suivant");
            this.btn_suivant.Name = "btn_suivant";
            this.btn_suivant.UseVisualStyleBackColor = true;
            this.btn_suivant.Click += new System.EventHandler(this.btn_suivant_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aléatoireToolStripMenuItem,
            this.toolStripMenuItem1});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.suivantToolStripMenuItem,
            this.précédentToolStripMenuItem,
            this.supprimerToolStripMenuItem,
            this.modifierToolStripMenuItem,
            this.toolStripSeparator4,
            this.rechargerLeFondToolStripMenuItem,
            this.mettreEnFondDécranToolStripMenuItem,
            this.toolStripSeparator1,
            this.changerDeDossierToolStripMenuItem,
            this.allezÀLimageNuméroToolStripMenuItem,
            this.minimiserToutesLesFenêtresToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitterToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            resources.ApplyResources(this.actionsToolStripMenuItem, "actionsToolStripMenuItem");
            // 
            // suivantToolStripMenuItem
            // 
            this.suivantToolStripMenuItem.Name = "suivantToolStripMenuItem";
            resources.ApplyResources(this.suivantToolStripMenuItem, "suivantToolStripMenuItem");
            this.suivantToolStripMenuItem.Click += new System.EventHandler(this.suivantToolStripMenuItem_Click);
            // 
            // précédentToolStripMenuItem
            // 
            resources.ApplyResources(this.précédentToolStripMenuItem, "précédentToolStripMenuItem");
            this.précédentToolStripMenuItem.Name = "précédentToolStripMenuItem";
            this.précédentToolStripMenuItem.Click += new System.EventHandler(this.précédentToolStripMenuItem_Click);
            // 
            // supprimerToolStripMenuItem
            // 
            resources.ApplyResources(this.supprimerToolStripMenuItem, "supprimerToolStripMenuItem");
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // modifierToolStripMenuItem
            // 
            resources.ApplyResources(this.modifierToolStripMenuItem, "modifierToolStripMenuItem");
            this.modifierToolStripMenuItem.Name = "modifierToolStripMenuItem";
            this.modifierToolStripMenuItem.Click += new System.EventHandler(this.modifierToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // rechargerLeFondToolStripMenuItem
            // 
            resources.ApplyResources(this.rechargerLeFondToolStripMenuItem, "rechargerLeFondToolStripMenuItem");
            this.rechargerLeFondToolStripMenuItem.Name = "rechargerLeFondToolStripMenuItem";
            this.rechargerLeFondToolStripMenuItem.Click += new System.EventHandler(this.rechargerLeFondToolStripMenuItem_Click);
            // 
            // mettreEnFondDécranToolStripMenuItem
            // 
            this.mettreEnFondDécranToolStripMenuItem.Name = "mettreEnFondDécranToolStripMenuItem";
            resources.ApplyResources(this.mettreEnFondDécranToolStripMenuItem, "mettreEnFondDécranToolStripMenuItem");
            this.mettreEnFondDécranToolStripMenuItem.Click += new System.EventHandler(this.mettreEnFondDécranToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // changerDeDossierToolStripMenuItem
            // 
            this.changerDeDossierToolStripMenuItem.Name = "changerDeDossierToolStripMenuItem";
            resources.ApplyResources(this.changerDeDossierToolStripMenuItem, "changerDeDossierToolStripMenuItem");
            this.changerDeDossierToolStripMenuItem.Click += new System.EventHandler(this.changerDeDossierToolStripMenuItem_Click);
            // 
            // allezÀLimageNuméroToolStripMenuItem
            // 
            this.allezÀLimageNuméroToolStripMenuItem.Name = "allezÀLimageNuméroToolStripMenuItem";
            resources.ApplyResources(this.allezÀLimageNuméroToolStripMenuItem, "allezÀLimageNuméroToolStripMenuItem");
            this.allezÀLimageNuméroToolStripMenuItem.Click += new System.EventHandler(this.allezÀLimageNuméroToolStripMenuItem_Click);
            // 
            // minimiserToutesLesFenêtresToolStripMenuItem
            // 
            this.minimiserToutesLesFenêtresToolStripMenuItem.Name = "minimiserToutesLesFenêtresToolStripMenuItem";
            resources.ApplyResources(this.minimiserToutesLesFenêtresToolStripMenuItem, "minimiserToutesLesFenêtresToolStripMenuItem");
            this.minimiserToutesLesFenêtresToolStripMenuItem.Click += new System.EventHandler(this.minimiserToutesLesFenêtresToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            resources.ApplyResources(this.quitterToolStripMenuItem, "quitterToolStripMenuItem");
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.mettreÀJourToolStripMenuItem,
            this.toolStripSeparator3,
            this.statistiquesToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            resources.ApplyResources(this.configurationToolStripMenuItem, "configurationToolStripMenuItem");
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // mettreÀJourToolStripMenuItem
            // 
            this.mettreÀJourToolStripMenuItem.Name = "mettreÀJourToolStripMenuItem";
            resources.ApplyResources(this.mettreÀJourToolStripMenuItem, "mettreÀJourToolStripMenuItem");
            this.mettreÀJourToolStripMenuItem.Click += new System.EventHandler(this.mettreÀJourToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // statistiquesToolStripMenuItem
            // 
            this.statistiquesToolStripMenuItem.Name = "statistiquesToolStripMenuItem";
            resources.ApplyResources(this.statistiquesToolStripMenuItem, "statistiquesToolStripMenuItem");
            // 
            // aléatoireToolStripMenuItem
            // 
            resources.ApplyResources(this.aléatoireToolStripMenuItem, "aléatoireToolStripMenuItem");
            this.aléatoireToolStripMenuItem.Name = "aléatoireToolStripMenuItem";
            this.aléatoireToolStripMenuItem.Click += new System.EventHandler(this.aléatoireToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aideEnLigneToolStripMenuItem,
            this.aProposToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // aideEnLigneToolStripMenuItem
            // 
            this.aideEnLigneToolStripMenuItem.Name = "aideEnLigneToolStripMenuItem";
            resources.ApplyResources(this.aideEnLigneToolStripMenuItem, "aideEnLigneToolStripMenuItem");
            this.aideEnLigneToolStripMenuItem.Click += new System.EventHandler(this.aideEnLigneToolStripMenuItem_Click);
            // 
            // aProposToolStripMenuItem
            // 
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            resources.ApplyResources(this.aProposToolStripMenuItem, "aProposToolStripMenuItem");
            this.aProposToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // lbl_info_chemin
            // 
            resources.ApplyResources(this.lbl_info_chemin, "lbl_info_chemin");
            this.lbl_info_chemin.Name = "lbl_info_chemin";
            // 
            // lbl_info_nom
            // 
            resources.ApplyResources(this.lbl_info_nom, "lbl_info_nom");
            this.lbl_info_nom.Name = "lbl_info_nom";
            // 
            // btn_precedent
            // 
            resources.ApplyResources(this.btn_precedent, "btn_precedent");
            this.btn_precedent.Name = "btn_precedent";
            this.btn_precedent.UseVisualStyleBackColor = true;
            this.btn_precedent.Click += new System.EventHandler(this.btn_precedent_Click);
            // 
            // lbl_chemin
            // 
            resources.ApplyResources(this.lbl_chemin, "lbl_chemin");
            this.lbl_chemin.Name = "lbl_chemin";
            // 
            // lbl_nom
            // 
            resources.ApplyResources(this.lbl_nom, "lbl_nom");
            this.lbl_nom.Name = "lbl_nom";
            // 
            // btn_supprimer
            // 
            resources.ApplyResources(this.btn_supprimer, "btn_supprimer");
            this.btn_supprimer.Name = "btn_supprimer";
            this.btn_supprimer.UseVisualStyleBackColor = true;
            this.btn_supprimer.Click += new System.EventHandler(this.btn_supprimer_Click);
            // 
            // lbl_info_num
            // 
            resources.ApplyResources(this.lbl_info_num, "lbl_info_num");
            this.lbl_info_num.Name = "lbl_info_num";
            // 
            // lbl_num
            // 
            resources.ApplyResources(this.lbl_num, "lbl_num");
            this.lbl_num.Name = "lbl_num";
            // 
            // btn_modifier
            // 
            resources.ApplyResources(this.btn_modifier, "btn_modifier");
            this.btn_modifier.Name = "btn_modifier";
            this.btn_modifier.UseVisualStyleBackColor = true;
            this.btn_modifier.Click += new System.EventHandler(this.btn_modifier_Click);
            // 
            // Principale
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_modifier);
            this.Controls.Add(this.lbl_num);
            this.Controls.Add(this.lbl_info_num);
            this.Controls.Add(this.btn_supprimer);
            this.Controls.Add(this.lbl_nom);
            this.Controls.Add(this.lbl_chemin);
            this.Controls.Add(this.btn_precedent);
            this.Controls.Add(this.lbl_info_nom);
            this.Controls.Add(this.lbl_info_chemin);
            this.Controls.Add(this.btn_suivant);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Principale";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Principale_FormClosed);
            this.Load += new System.EventHandler(this.Principale_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_suivant;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suivantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem précédentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mettreÀJourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aideEnLigneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.Label lbl_info_chemin;
        private System.Windows.Forms.Label lbl_info_nom;
        private System.Windows.Forms.Button btn_precedent;
        private System.Windows.Forms.Label lbl_chemin;
        private System.Windows.Forms.Label lbl_nom;
        private System.Windows.Forms.Button btn_supprimer;
        private System.Windows.Forms.Label lbl_info_num;
        private System.Windows.Forms.Label lbl_num;
        private System.Windows.Forms.ToolTip infobulle_nom;
        private System.Windows.Forms.Button btn_modifier;
        private System.Windows.Forms.ToolStripMenuItem modifierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changerDeDossierToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem allezÀLimageNuméroToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem statistiquesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem rechargerLeFondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mettreEnFondDécranToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimiserToutesLesFenêtresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aléatoireToolStripMenuItem;
        private System.Windows.Forms.ToolTip infobulle_chemin;
    }
}