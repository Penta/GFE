using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Gfe.Langues;
using Gfe.Core;

namespace Gfe.Fenetres
{
    public partial class Selection : Form
    {
        private string chemin;
        private bool desactivation = false;
        private string tempChemin = null;

        private void OuvrirPrincipale() { Application.Run(new Principale(chemin)); }

        private void ChangerDossier()
        {
            FolderBrowserDialog selectionDossier = new FolderBrowserDialog()
            {
                Description = Texte.DescriptionSelectionDossier,
                ShowNewFolderButton = false
            };

            if (!string.IsNullOrEmpty(chemin))
                selectionDossier.SelectedPath = chemin.Split('|')[chemin.Split('|').Length - 1].Trim();

            DialogResult result = selectionDossier.ShowDialog();

            if (result == DialogResult.OK)
            {
                tempChemin = selectionDossier.SelectedPath;
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
                combo_chemin.Text = Principale.chemin;
                check_sousdossier.Checked = Principale.sousDossier;
            }

            if (!string.IsNullOrEmpty(Principale.historique))
            {
                combo_chemin.Items.Clear();
                combo_chemin.Items.AddRange(Principale.historique.Split('>'));
            }
        }

        private void BoutonExplorer_Clic (object sender, EventArgs e)
        {
            chemin = combo_chemin.Text;

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

            if (!string.IsNullOrEmpty(tempChemin))
            {
                DialogResult question = MessageBox.Show(Texte.QuestionSelection, Texte.ConfirmationTitre, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (question == DialogResult.Yes)
                    chemin = chemin.TrimEnd('|').TrimEnd(' ') + " | " + tempChemin;
                else if (question == DialogResult.No)
                    chemin = tempChemin;

                tempChemin = null;
            }

            combo_chemin.Text = chemin;
        }

        private void BoutonValider_Clic(object sender, EventArgs e) { Validation(); }

        private void Validation()
        {
            bool erreur = false;

            if (!string.IsNullOrEmpty(combo_chemin.Text))
            {
                foreach (string var in combo_chemin.Text.Split('|'))
                    if (!Directory.Exists(var.Trim()))
                        erreur = true;

                if(!erreur)
                {
                    Principale.sousDossier = check_sousdossier.Checked;
                    Registre.MiseAjourConfig();

                    if(Principale.selectionPremierLancement)
                    {
                        chemin = combo_chemin.Text;
                        Thread principale = new Thread(new ThreadStart(OuvrirPrincipale));
                        principale.Start();

                        Principale.selectionPremierLancement = false;
                    }
                    else
                        Principale.chemin = combo_chemin.Text;

                    this.DestroyHandle();
                }
                else
                    MessageBox.Show(Texte.DossierInvalide, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
                MessageBox.Show(Texte.AucunDossier, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        private void LabelInfoMultiDossier_Clic(object sender, EventArgs e) { combo_chemin.Text += " | "; }
    }
}
