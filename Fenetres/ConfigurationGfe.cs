using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Gfe.Langues;
using Gfe.Core;

namespace Gfe.Fenetres
{
    public partial class ConfigurationGfe : Form
    {
        static public bool changement = false;
        static public bool changementFichier = false;

        private string logiciel = null;
        private bool desactivation = false;
        private int ancienneLangue;


        private void ChargerConfig()
        {
            Langue.TraduireValeur();

            cb_couleur.Text = Principale.couleur;
            cb_dispo.Text = Principale.affichage;
            txt_externe.Text = Principale.logiciel;
            txt_extension.Text = Principale.extension;
            check_rappel.Checked = Principale.rappel;
            check_constanteVerif.Checked = Principale.rechargementConstant;
            check_sousdossier.Checked = Principale.sousDossier;
            check_conversion.Checked = Principale.conversion;
            comboLangue.SelectedIndex = ancienneLangue = ConvertionLangueEnID(Registre.RecupererLangue());


            if(!Program.nonXP)
                check_conversion.Enabled = false;
        }

        private int ConvertionLangueEnID(string valeur)
        {
            int resultat = 0;

            if (valeur == "fr")
                resultat = 1;
            else if (valeur == "en" || valeur == "gb" || valeur == "au")
                resultat = 2;
            else
                resultat = 0;

            return resultat;
        }

        private void ChangerDossier()
        {
            logiciel = null;

            OpenFileDialog selectionFichier = new OpenFileDialog()
            {
                InitialDirectory = Path.GetPathRoot(Environment.SystemDirectory),
                Filter = Texte.ExtensionsExterne,
                Title = Texte.ChoixExterneTitre,
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (selectionFichier.ShowDialog() == DialogResult.OK)
            {
                logiciel = selectionFichier.FileName;
            }

            desactivation = false;
        }

        public ConfigurationGfe()
        {
            this.Icon = Properties.Resources.icone;

            InitializeComponent();
            ChargerConfig();
        }

        private void BoutonAppliquer_Clic(object sender, EventArgs e)
        {
            string valeurRegistreLangue = string.Empty;

            if (Principale.extension != txt_extension.Text | Principale.sousDossier != check_sousdossier.Checked)
                changementFichier = true;

            changement = true;

            Principale.affichage = cb_dispo.Text;
            Principale.logiciel = txt_externe.Text;
            Principale.couleur = cb_couleur.Text;
            Principale.extension = txt_extension.Text.Replace(" ", string.Empty).Replace(".", string.Empty);
            Principale.rappel = check_rappel.Checked;
            Principale.rechargementConstant = check_constanteVerif.Checked;
            Principale.sousDossier = check_sousdossier.Checked;
            Principale.conversion = check_conversion.Checked;

            Registre.MiseAjourConfig();

            if(ancienneLangue != comboLangue.SelectedIndex)
            {
                if (comboLangue.SelectedIndex == 1)
                    valeurRegistreLangue = "fr";
                else if (comboLangue.SelectedIndex == 2)
                    valeurRegistreLangue = "en";

                Registre.registre.SetValue("Langue", valeurRegistreLangue);
                Program.ObtenirLangue();

                MessageBox.Show(Texte.RedémarrageLangue, Texte.ConfirmationTitre, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }

            this.DestroyHandle();
        }

        private void BoutonReset_Clic(object sender, EventArgs e)
        {
            DialogResult question = MessageBox.Show(Texte.MessageReinitialisation, Texte.ConfirmationTitre, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (question == DialogResult.Yes)
            {
                changement = true;
                changementFichier = true;

                Principale.logiciel = Environment.SystemDirectory + @"\mspaint.exe";
                Principale.affichage = Texte.ValeurEtirer;
                Principale.couleur = "Noir";
                Principale.extension = "png;jpg;jpeg;bmp;tiff;tif";
                Principale.rappel = true;
                Principale.sousDossier = false;
                Principale.rechargementConstant = false;
                Principale.conversion = !Program.nonXP;

                Registre.MiseAjourConfig();
                ChargerConfig();

                MessageBox.Show(Texte.ReinitialisationFaite, Texte.SuccèsTitre, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
                    
        }

        private void BoutonExplorer_Clic(object sender, EventArgs e)
        {
            this.Enabled = false;
            desactivation = true;

            Thread proc = new Thread(new ThreadStart(ChangerDossier));
            proc.SetApartmentState(ApartmentState.STA);
            proc.Start();

            while (desactivation == true) { Thread.Sleep(100); }

            if(!string.IsNullOrEmpty(logiciel))
                txt_externe.Text = logiciel;

            this.Enabled = true;
            this.Activate();
            this.Show();
        }
    }
}
