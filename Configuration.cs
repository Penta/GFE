using System;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Configuration : Form
    {
        static public bool changement = false;
        static public bool changementFichier = false;

        private void chargerConfig()
        {
            cb_dispo.Text = Principale.affichage;
            txt_externe.Text = Principale.logiciel;
            txt_extension.Text = Principale.extension;
            check_rappel.Checked = Principale.rappel;
            check_constanteVerif.Checked = Principale.rechargementConstant;
            check_sousdossier.Checked = Principale.sousDossier;
        }

        public Configuration()
        {
            InitializeComponent();

            chargerConfig();
        }

        private void btn_appliquer_Click(object sender, EventArgs e)
        {
            if (Principale.extension != txt_extension.Text | Principale.sousDossier != check_sousdossier.Checked)
                changementFichier = true;

            changement = true;

            Principale.affichage = cb_dispo.Text;
            Principale.logiciel = txt_externe.Text;
            Principale.extension = txt_extension.Text;
            Principale.rappel = check_rappel.Checked;
            Principale.rechargementConstant = check_constanteVerif.Checked;
            Principale.sousDossier = check_sousdossier.Checked;

            Registre.miseAjourConfig();

            this.DestroyHandle();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Voulez-vous perdre votre configuration et remettre celle par défaut ?", "Réinitialisation de la configuration", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (question == DialogResult.Yes)
            {
                Principale.logiciel = @"C:\Windows\System32\mspaint.exe";
                Principale.affichage = "étirer";
                Principale.extension = "png;jpg;jpeg;bmp";
                Principale.rappel = true;
                Principale.sousDossier = false;
                Principale.rechargementConstant = false;

                Registre.miseAjourConfig();
                chargerConfig();

                MessageBox.Show("Votre configuration a été réinitialisée !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
                    
        }
    }
}
