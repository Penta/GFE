using System;
using System.Windows.Forms;

namespace Gfe
{
    public partial class Saut : Form
    {
        public Saut()
        {
            this.Icon = Properties.Resources.icone;

            InitializeComponent();

            lbl_max.Text = "/ " + Principale.nbFichier.ToString();
        }

        private void BoutonValider_Clic(object sender, EventArgs e)
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
                        MessageBox.Show(Texte.SautTropGrand, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                    MessageBox.Show(Texte.SautTropPetit, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch { MessageBox.Show(Texte.SautVide, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); }
        }
    }
}
