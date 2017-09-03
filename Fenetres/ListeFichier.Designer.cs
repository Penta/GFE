namespace Gfe.Fenetres
{
    partial class ListeFichier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListeFichier));
            this.liste = new System.Windows.Forms.ListBox();
            this.btn_goto = new System.Windows.Forms.Button();
            this.btn_annuler = new System.Windows.Forms.Button();
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // liste
            // 
            resources.ApplyResources(this.liste, "liste");
            this.liste.FormattingEnabled = true;
            this.liste.Items.AddRange(new object[] {
            resources.GetString("liste.Items")});
            this.liste.Name = "liste";
            this.liste.SelectedIndexChanged += new System.EventHandler(this.liste_SelectedIndexChanged);
            this.liste.DoubleClick += new System.EventHandler(this.liste_DoubleClick);
            // 
            // btn_goto
            // 
            resources.ApplyResources(this.btn_goto, "btn_goto");
            this.btn_goto.Name = "btn_goto";
            this.btn_goto.UseVisualStyleBackColor = true;
            this.btn_goto.Click += new System.EventHandler(this.btn_goto_Click);
            // 
            // btn_annuler
            // 
            resources.ApplyResources(this.btn_annuler, "btn_annuler");
            this.btn_annuler.Name = "btn_annuler";
            this.btn_annuler.UseVisualStyleBackColor = true;
            this.btn_annuler.Click += new System.EventHandler(this.btn_annuler_Click);
            // 
            // lbl_nombre
            // 
            resources.ApplyResources(this.lbl_nombre, "lbl_nombre");
            this.lbl_nombre.Name = "lbl_nombre";
            // 
            // ListeFichier
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_nombre);
            this.Controls.Add(this.btn_annuler);
            this.Controls.Add(this.btn_goto);
            this.Controls.Add(this.liste);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListeFichier";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox liste;
        private System.Windows.Forms.Button btn_goto;
        private System.Windows.Forms.Button btn_annuler;
        private System.Windows.Forms.Label lbl_nombre;
    }
}