using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Gfe
{
    public partial class Selection : Form
    {
        private string chemin;
        private bool desactivation = false;

        private void OuvrirPrincipale() { Application.Run(new Principale(chemin)); }

        private void ChangerDossier()
        {
            FolderBrowserDialog selectionDossier = new FolderBrowserDialog()
            {
                Description = "Veuillez choisir le dossier de vos fonds d'écran.",
                ShowNewFolderButton = false
            };

            if (!string.IsNullOrEmpty(txt_chemin.Text))
                selectionDossier.SelectedPath = txt_chemin.Text.Split('|')[txt_chemin.Text.Split('|').Length - 1].Trim();

            DialogResult result = selectionDossier.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(txt_chemin.Text))
                {
                    DialogResult question = MessageBox.Show("Voulez-vous ajoutez ce chemin à celui ou ceux déjà existants ?\n\nAppuyer sur Non remplacera les valeurs déjà existantes !", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (question == DialogResult.Yes)
                        txt_chemin.Text += " | " + selectionDossier.SelectedPath;
                    else if (question == DialogResult.No)
                        txt_chemin.Text = selectionDossier.SelectedPath;
                }
                else
                    txt_chemin.Text = selectionDossier.SelectedPath;
            }

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
                check_sousdossier.Checked = Principale.sousDossier;
            }
        }

        private void BoutonExplorer_Clic (object sender, EventArgs e)
        {
            if (!Principale.selectionPremierLancement)
            {
                this.Enabled = false;
                desactivation = true;

                Thread proc = new Thread(new ThreadStart(ChangerDossier));
                proc.SetApartmentState(ApartmentState.STA);
                proc.Start();

                while (desactivation == true) { Thread.Sleep(100); }

                this.Enabled = true;
                this.Activate();
                this.Show();
            }
            else { ChangerDossier(); }
        }

        private void BoutonValider_Clic(object sender, EventArgs e) { Validation(); }

        private void Validation ()
        {
            bool erreur = false;

            if (!string.IsNullOrEmpty(txt_chemin.Text))
            {
                foreach (string var in txt_chemin.Text.Split('|'))
                    if (!Directory.Exists(var))
                        erreur = true;

                if(!erreur)
                {
                    Principale.sousDossier = check_sousdossier.Checked;
                    Registre.MiseAjourConfig();

                    if(Principale.selectionPremierLancement)
                    {
                        chemin = txt_chemin.Text;
                        Thread principale = new Thread(new ThreadStart(OuvrirPrincipale));
                        principale.Start();

                        Principale.selectionPremierLancement = false;
                    }
                    else
                        Principale.chemin = txt_chemin.Text;

                    this.DestroyHandle();
                }
                else
                    MessageBox.Show("Au moins un des dossiers rentrés n'est pas valide !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
                MessageBox.Show("Veuillez choisir un dossier !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
    }
}
