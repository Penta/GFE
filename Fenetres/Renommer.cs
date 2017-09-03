using System;
using System.IO;
using System.Windows.Forms;
using Gfe.Langues;

namespace Gfe.Fenetres
{
    public partial class Renommer : Form
    {
        public static string fichier;
        public static string resultat;

        public Renommer()
        {
            this.Icon = Properties.Resources.icone;
            resultat = string.Empty;

            InitializeComponent();

            txt_nom.Text = Path.GetFileNameWithoutExtension(fichier);
            lbl_extension.Text = Path.GetExtension(fichier);

            txt_nom.Focus();
        }

        private void BoutonValiderClic(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_nom.Text))
            {
                resultat = txt_nom.Text + Path.GetExtension(fichier);
                this.DestroyHandle();
            }
            else
                MessageBox.Show(Texte.RenommerFichierVide, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TexteNomTouchePresse(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
                btn_valider.PerformClick();
            }
        }
    }
}
