using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Selection : Form
    {
        private FolderBrowserDialog selectionDossier;
        private string chemin;

        private void ouvrirPrincipale() { Application.Run(new Principale(chemin)); }


        public Selection()
        {
            InitializeComponent();

            if (Principale.chemin != null)
            {
                txt_chemin.Text = Principale.chemin;
                chemin = Principale.chemin;
            }
        }

        private void btn_explorer_Click(object sender, EventArgs e)
        {
            this.selectionDossier = new FolderBrowserDialog();
            this.selectionDossier.Description = "Veuillez choisir le dossier de fond d'écran.";
            this.selectionDossier.ShowNewFolderButton = false;

            if (chemin != "")
                selectionDossier.SelectedPath = chemin;

            DialogResult result = selectionDossier.ShowDialog();

            if (result == DialogResult.OK)
            {
                chemin = selectionDossier.SelectedPath;
                txt_chemin.Text = chemin; 
            }
        }

        private void btn_valider_Click(object sender, EventArgs e)
        {
            validation();
        }

        private void validation()
        {
            if (txt_chemin.Text != "")
            {
                if (Directory.Exists(txt_chemin.Text))
                {
                    chemin = txt_chemin.Text;

                    this.DestroyHandle();

                    Thread principale = new Thread(new ThreadStart(ouvrirPrincipale));
                    principale.Start();
                }
                else
                    MessageBox.Show("Le dossier n'existe pas ou n'est pas valide !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
                MessageBox.Show("Veuillez choisir un dossier !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
    }
}
