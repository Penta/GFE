namespace Gfe.Fenetres
{
    partial class Saut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Saut));
            this.lbl_aller_a_image = new System.Windows.Forms.Label();
            this.txt_numero = new System.Windows.Forms.TextBox();
            this.lbl_max = new System.Windows.Forms.Label();
            this.btn_valider = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_aller_a_image
            // 
            resources.ApplyResources(this.lbl_aller_a_image, "lbl_aller_a_image");
            this.lbl_aller_a_image.Name = "lbl_aller_a_image";
            // 
            // txt_numero
            // 
            resources.ApplyResources(this.txt_numero, "txt_numero");
            this.txt_numero.Name = "txt_numero";
            // 
            // lbl_max
            // 
            resources.ApplyResources(this.lbl_max, "lbl_max");
            this.lbl_max.Name = "lbl_max";
            // 
            // btn_valider
            // 
            resources.ApplyResources(this.btn_valider, "btn_valider");
            this.btn_valider.Name = "btn_valider";
            this.btn_valider.UseVisualStyleBackColor = true;
            this.btn_valider.Click += new System.EventHandler(this.BoutonValider_Clic);
            // 
            // Saut
            // 
            this.AcceptButton = this.btn_valider;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_valider);
            this.Controls.Add(this.lbl_max);
            this.Controls.Add(this.txt_numero);
            this.Controls.Add(this.lbl_aller_a_image);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Saut";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_aller_a_image;
        private System.Windows.Forms.TextBox txt_numero;
        private System.Windows.Forms.Label lbl_max;
        private System.Windows.Forms.Button btn_valider;
    }
}