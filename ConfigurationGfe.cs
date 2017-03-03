using System;
using System.Windows.Forms;

namespace Gfe
{
    public partial class ConfigurationGfe : Form
    {
        static public bool changement = false;
        static public bool changementFichier = false;

        private void ChargerConfig()
        {
            cb_dispo.Text = Principale.affichage;
            txt_externe.Text = Principale.logiciel;
            txt_extension.Text = Principale.extension;
            check_rappel.Checked = Principale.rappel;
            check_constanteVerif.Checked = Principale.rechargementConstant;
            check_sousdossier.Checked = Principale.sousDossier;
        }

        public ConfigurationGfe()
        {
            InitializeComponent();

            ChargerConfig();
        }

        private void BoutonAppliquer_Clic(object sender, EventArgs e)
        {
            if (Principale.extension != txt_extension.Text | Principale.sousDossier != check_sousdossier.Checked)
                changementFichier = true;

            changement = true;

            Principale.affichage = cb_dispo.Text;
            Principale.logiciel = txt_externe.Text;
            Principale.extension = txt_extension.Text.Replace(" ", string.Empty).Replace(".", string.Empty);
            Principale.rappel = check_rappel.Checked;
            Principale.rechargementConstant = check_constanteVerif.Checked;
            Principale.sousDossier = check_sousdossier.Checked;

            Registre.MiseAjourConfig();

            this.DestroyHandle();
        }

        private void BoutonReset_Clic(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show("Voulez-vous perdre votre configuration et remettre celle par défaut ?", "Réinitialisation de la configuration", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (question == DialogResult.Yes)
            {
                changement = true;
                changementFichier = true;

                Principale.logiciel = @"C:\Windows\System32\mspaint.exe";
                Principale.affichage = "étirer";
                Principale.extension = "png;jpg;jpeg;bmp;tiff;tif";
                Principale.rappel = true;
                Principale.sousDossier = false;
                Principale.rechargementConstant = false;

                Registre.MiseAjourConfig();
                ChargerConfig();

                MessageBox.Show("Votre configuration a été réinitialisée !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
                    
        }
    }
}
