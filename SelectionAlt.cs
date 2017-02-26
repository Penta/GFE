using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Gulix.Wallpaper;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class SelectionAlt : Form
    {
        private string chemin = null;
        private bool valide = false;

        private bool desactivation = false;

        private void ouvrirPrincipale() { Application.Run(new Principale(chemin)); }

        private void changerDossier()
        {
            FolderBrowserDialog selectionDossier = new FolderBrowserDialog();
            Wallpaper fond = new Wallpaper(null, Affichage.etirer, Principale.couleur);

            selectionDossier.Description = "Veuillez choisir le dossier de fond d'écran.";
            selectionDossier.ShowNewFolderButton = false;

            if (chemin != "")
                selectionDossier.SelectedPath = chemin;
            
            DialogResult result = selectionDossier.ShowDialog();

            if (result == DialogResult.OK)
            {
                chemin = selectionDossier.SelectedPath;
                valide = true;
            }
            else
                valide = false;

            desactivation = false;
        }

        public SelectionAlt()
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
            this.Enabled = false;
            desactivation = true;

            Thread proc = new Thread(new ThreadStart(changerDossier));
            proc.SetApartmentState(ApartmentState.STA);
            proc.Start();

            while (desactivation == true) { Thread.Sleep(100); }

            if (valide)
                txt_chemin.Text = chemin;

            this.Enabled = true;
            this.Activate();
            this.Show();
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
                    Principale.chemin = txt_chemin.Text;

                    Registre.miseAjourConfig();

                    this.DestroyHandle();
                }
                else
                    MessageBox.Show("Le dossier n'existe pas ou n'est pas valide !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
                MessageBox.Show("Veuillez choisir un dossier !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
    }
}
