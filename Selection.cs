using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Gfe
{
    public partial class Selection : Form
    {
        private FolderBrowserDialog selectionDossier;
        private string chemin;
        private bool valide = false, desactivation = false;

        private void OuvrirPrincipale() { Application.Run(new Principale(chemin)); }

        private void ChangerDossier()
        {
            FolderBrowserDialog selectionDossier = new FolderBrowserDialog()
            {
                Description = "Veuillez choisir le dossier de fond d'écran.",
                ShowNewFolderButton = false
            };

            if (!string.IsNullOrEmpty(chemin))
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

        public Selection()
        {
            this.Icon = Properties.Resources.icone;

            if (!Principale.selectionPremierLancement)
            {
                this.ShowInTaskbar = false;
                this.ShowIcon = false;
            }

            InitializeComponent();

            if (Principale.chemin != null)
            {
                txt_chemin.Text = Principale.chemin;
                chemin = Principale.chemin;
                check_sousdossier.Checked = Principale.sousDossier;
            }
        }

        private void BoutonExplorer_Clic (object sender, EventArgs e)
        {
            if (Principale.selectionPremierLancement)
            {
                this.selectionDossier = new FolderBrowserDialog()
                {
                    Description = "Veuillez choisir le dossier de fond d'écran.",
                    ShowNewFolderButton = false
                };

                if (!string.IsNullOrEmpty(chemin))
                    selectionDossier.SelectedPath = chemin;

                DialogResult result = selectionDossier.ShowDialog();

                if (result == DialogResult.OK)
                {
                    chemin = selectionDossier.SelectedPath;
                    txt_chemin.Text = chemin;
                }
            }
            else
            {
                this.Enabled = false;
                desactivation = true;

                Thread proc = new Thread(new ThreadStart(ChangerDossier));
                proc.SetApartmentState(ApartmentState.STA);
                proc.Start();

                while (desactivation == true) { Thread.Sleep(100); }

                if (valide)
                    txt_chemin.Text = chemin;

                this.Enabled = true;
                this.Activate();
                this.Show();
            }
        }

        private void BoutonValider_Clic(object sender, EventArgs e) { Validation(); }

        private void Validation ()
        {
            if (!string.IsNullOrEmpty(txt_chemin.Text))
            {
                if (Directory.Exists(txt_chemin.Text))
                {
                    chemin = txt_chemin.Text;

                    Principale.sousDossier = check_sousdossier.Checked;
                    Registre.MiseAjourConfig();

                    if(Principale.selectionPremierLancement)
                    {
                        Thread principale = new Thread(new ThreadStart(OuvrirPrincipale));
                        principale.Start();

                        Principale.selectionPremierLancement = false;
                    }
                    else
                        Principale.chemin = txt_chemin.Text;

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
