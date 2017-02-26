using System;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Saut : Form
    {
        public Saut()
        {
            InitializeComponent();

            lbl_max.Text = "/ " +Principale.nbFichier.ToString();
        }

        private void btn_valider_Click(object sender, EventArgs e)
        {
            int valeur = -1; 

            try
            {
                valeur = Convert.ToInt32(txt_numero.Text);

                if (valeur >= 1)
                {
                    if (valeur <= Principale.nbFichier)
                    {
                        Principale.id = valeur - 1;

                        this.DestroyHandle();
                    }
                    else
                        MessageBox.Show("Nombre trop grand !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                    MessageBox.Show("Veuillez choisir un nombre superieur à 0 !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch
            {
                MessageBox.Show("Veuillez rentrer un nombre !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
