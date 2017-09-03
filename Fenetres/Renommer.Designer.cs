namespace Gfe.Fenetres
{
    partial class Renommer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Renommer));
            this.btn_valider = new System.Windows.Forms.Button();
            this.lbl_extension = new System.Windows.Forms.Label();
            this.txt_nom = new System.Windows.Forms.TextBox();
            this.lbl_description = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_valider
            // 
            resources.ApplyResources(this.btn_valider, "btn_valider");
            this.btn_valider.Name = "btn_valider";
            this.btn_valider.UseVisualStyleBackColor = true;
            this.btn_valider.Click += new System.EventHandler(this.BoutonValiderClic);
            // 
            // lbl_extension
            // 
            resources.ApplyResources(this.lbl_extension, "lbl_extension");
            this.lbl_extension.Name = "lbl_extension";
            // 
            // txt_nom
            // 
            resources.ApplyResources(this.txt_nom, "txt_nom");
            this.txt_nom.Name = "txt_nom";
            this.txt_nom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TexteNomTouchePresse);
            // 
            // lbl_description
            // 
            resources.ApplyResources(this.lbl_description, "lbl_description");
            this.lbl_description.Name = "lbl_description";
            // 
            // Renommer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_description);
            this.Controls.Add(this.txt_nom);
            this.Controls.Add(this.lbl_extension);
            this.Controls.Add(this.btn_valider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Renommer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_valider;
        private System.Windows.Forms.Label lbl_extension;
        private System.Windows.Forms.TextBox txt_nom;
        private System.Windows.Forms.Label lbl_description;
    }
}