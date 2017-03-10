namespace Gfe
{
    partial class Selection
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Selection));
            this.lbl_info_selection = new System.Windows.Forms.Label();
            this.btn_explorer = new System.Windows.Forms.Button();
            this.btn_valider = new System.Windows.Forms.Button();
            this.check_sousdossier = new System.Windows.Forms.CheckBox();
            this.lbl_info_multidossier = new System.Windows.Forms.Label();
            this.combo_chemin = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_info_selection
            // 
            resources.ApplyResources(this.lbl_info_selection, "lbl_info_selection");
            this.lbl_info_selection.Name = "lbl_info_selection";
            // 
            // btn_explorer
            // 
            resources.ApplyResources(this.btn_explorer, "btn_explorer");
            this.btn_explorer.Name = "btn_explorer";
            this.btn_explorer.UseVisualStyleBackColor = true;
            this.btn_explorer.Click += new System.EventHandler(this.BoutonExplorer_Clic);
            // 
            // btn_valider
            // 
            resources.ApplyResources(this.btn_valider, "btn_valider");
            this.btn_valider.Name = "btn_valider";
            this.btn_valider.UseVisualStyleBackColor = true;
            this.btn_valider.Click += new System.EventHandler(this.BoutonValider_Clic);
            // 
            // check_sousdossier
            // 
            resources.ApplyResources(this.check_sousdossier, "check_sousdossier");
            this.check_sousdossier.Name = "check_sousdossier";
            this.check_sousdossier.UseVisualStyleBackColor = true;
            // 
            // lbl_info_multidossier
            // 
            resources.ApplyResources(this.lbl_info_multidossier, "lbl_info_multidossier");
            this.lbl_info_multidossier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_info_multidossier.Name = "lbl_info_multidossier";
            this.lbl_info_multidossier.Click += new System.EventHandler(this.LabelInfoMultiDossier_Clic);
            // 
            // combo_chemin
            // 
            this.combo_chemin.FormattingEnabled = true;
            this.combo_chemin.Items.AddRange(new object[] {
            resources.GetString("combo_chemin.Items")});
            resources.ApplyResources(this.combo_chemin, "combo_chemin");
            this.combo_chemin.Name = "combo_chemin";
            // 
            // Selection
            // 
            this.AcceptButton = this.btn_valider;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.combo_chemin);
            this.Controls.Add(this.lbl_info_multidossier);
            this.Controls.Add(this.check_sousdossier);
            this.Controls.Add(this.btn_valider);
            this.Controls.Add(this.btn_explorer);
            this.Controls.Add(this.lbl_info_selection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_info_selection;
        private System.Windows.Forms.Button btn_explorer;
        private System.Windows.Forms.Button btn_valider;
        private System.Windows.Forms.CheckBox check_sousdossier;
        private System.Windows.Forms.Label lbl_info_multidossier;
        private System.Windows.Forms.ComboBox combo_chemin;
    }
}

