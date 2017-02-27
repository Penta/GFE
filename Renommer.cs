using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Renommer : Form
    {
        static public string fichier;
        static public string resultat = "";

        public Renommer()
        {
            resultat = "";

            InitializeComponent();

            txt_nom.Text = Path.GetFileNameWithoutExtension(fichier);
            lbl_extension.Text = Path.GetExtension(fichier);
        }

        private void btn_valider_Click(object sender, EventArgs e)
        {
            if (txt_nom.Text != "")
            {
                resultat = txt_nom.Text + Path.GetExtension(fichier);
                this.DestroyHandle();
            }
            else
                MessageBox.Show("Le nom du fichier ne peut pas être vide !", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
