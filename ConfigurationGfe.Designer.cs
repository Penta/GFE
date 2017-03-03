namespace Gfe
{
    partial class ConfigurationGfe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationGfe));
            this.lbl_dispo_info = new System.Windows.Forms.Label();
            this.cb_dispo = new System.Windows.Forms.ComboBox();
            this.btn_appliquer = new System.Windows.Forms.Button();
            this.lbl_externe = new System.Windows.Forms.Label();
            this.txt_externe = new System.Windows.Forms.TextBox();
            this.check_rappel = new System.Windows.Forms.CheckBox();
            this.lbl_info_extension = new System.Windows.Forms.Label();
            this.txt_extension = new System.Windows.Forms.TextBox();
            this.check_sousdossier = new System.Windows.Forms.CheckBox();
            this.check_constanteVerif = new System.Windows.Forms.CheckBox();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_explorer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_dispo_info
            // 
            resources.ApplyResources(this.lbl_dispo_info, "lbl_dispo_info");
            this.lbl_dispo_info.Name = "lbl_dispo_info";
            // 
            // cb_dispo
            // 
            this.cb_dispo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_dispo.Items.AddRange(new object[] {
            resources.GetString("cb_dispo.Items"),
            resources.GetString("cb_dispo.Items1"),
            resources.GetString("cb_dispo.Items2"),
            resources.GetString("cb_dispo.Items3"),
            resources.GetString("cb_dispo.Items4")});
            resources.ApplyResources(this.cb_dispo, "cb_dispo");
            this.cb_dispo.Name = "cb_dispo";
            // 
            // btn_appliquer
            // 
            resources.ApplyResources(this.btn_appliquer, "btn_appliquer");
            this.btn_appliquer.Name = "btn_appliquer";
            this.btn_appliquer.UseVisualStyleBackColor = true;
            this.btn_appliquer.Click += new System.EventHandler(this.BoutonAppliquer_Clic);
            // 
            // lbl_externe
            // 
            resources.ApplyResources(this.lbl_externe, "lbl_externe");
            this.lbl_externe.Name = "lbl_externe";
            // 
            // txt_externe
            // 
            resources.ApplyResources(this.txt_externe, "txt_externe");
            this.txt_externe.Name = "txt_externe";
            // 
            // check_rappel
            // 
            resources.ApplyResources(this.check_rappel, "check_rappel");
            this.check_rappel.Checked = true;
            this.check_rappel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_rappel.Name = "check_rappel";
            this.check_rappel.UseVisualStyleBackColor = true;
            // 
            // lbl_info_extension
            // 
            resources.ApplyResources(this.lbl_info_extension, "lbl_info_extension");
            this.lbl_info_extension.Name = "lbl_info_extension";
            // 
            // txt_extension
            // 
            resources.ApplyResources(this.txt_extension, "txt_extension");
            this.txt_extension.Name = "txt_extension";
            // 
            // check_sousdossier
            // 
            resources.ApplyResources(this.check_sousdossier, "check_sousdossier");
            this.check_sousdossier.Name = "check_sousdossier";
            this.check_sousdossier.UseVisualStyleBackColor = true;
            // 
            // check_constanteVerif
            // 
            resources.ApplyResources(this.check_constanteVerif, "check_constanteVerif");
            this.check_constanteVerif.Name = "check_constanteVerif";
            this.check_constanteVerif.UseVisualStyleBackColor = true;
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_reset.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.btn_reset, "btn_reset");
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.BoutonReset_Clic);
            // 
            // btn_explorer
            // 
            resources.ApplyResources(this.btn_explorer, "btn_explorer");
            this.btn_explorer.Name = "btn_explorer";
            this.btn_explorer.UseVisualStyleBackColor = true;
            this.btn_explorer.Click += new System.EventHandler(this.BoutonExplorer_Clic);
            // 
            // ConfigurationGfe
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_explorer);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.check_constanteVerif);
            this.Controls.Add(this.check_sousdossier);
            this.Controls.Add(this.txt_extension);
            this.Controls.Add(this.lbl_info_extension);
            this.Controls.Add(this.check_rappel);
            this.Controls.Add(this.txt_externe);
            this.Controls.Add(this.btn_appliquer);
            this.Controls.Add(this.lbl_externe);
            this.Controls.Add(this.cb_dispo);
            this.Controls.Add(this.lbl_dispo_info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationGfe";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_dispo_info;
        private System.Windows.Forms.ComboBox cb_dispo;
        private System.Windows.Forms.Button btn_appliquer;
        private System.Windows.Forms.Label lbl_externe;
        private System.Windows.Forms.TextBox txt_externe;
        private System.Windows.Forms.CheckBox check_rappel;
        private System.Windows.Forms.Label lbl_info_extension;
        private System.Windows.Forms.TextBox txt_extension;
        private System.Windows.Forms.CheckBox check_sousdossier;
        private System.Windows.Forms.CheckBox check_constanteVerif;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_explorer;
    }
}