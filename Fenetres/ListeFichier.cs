using System;
using System.IO;
using System.Windows.Forms;
using Gfe.Langues;
using System.Drawing;

namespace Gfe.Fenetres
{
    public partial class ListeFichier : Form
    {
        bool doubleClic = true;

        public ListeFichier(FileInfo[] fichiers)
        {
            string[] listeFichier = new string[65536];

            this.Icon = Properties.Resources.icone;

            for (int i = 0; i < Principale.nbFichier; i++)
            {
                if(fichiers[i] != null)
                    listeFichier[i] = fichiers[i].Name;
            }

            InitializeComponent();

            lbl_nombre.Text = Texte.NombreFichierListe + " 0 / " + Principale.nbFichier;
            lbl_nombre.Location = new Point((this.Size.Width / 2) - (lbl_nombre.Size.Width / 2) - 4, this.Size.Height - 63);

            liste.Items.Clear();

            if (Principale.nbFichier > 1)
            {
                for (int i = 0; i < Principale.nbFichier; i++)
                {
                    if (listeFichier[i] != null)
                        liste.Items.Add(listeFichier[i].ToString());
                }
            }
            else
            {
                liste.Items.Add(Texte.ErreurDossierVide);

                doubleClic = false;
                btn_goto.Enabled = false;
            }
        }

        private void btn_annuler_Click(object sender, EventArgs e) { this.DestroyHandle(); }

        private void liste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Principale.nbFichier > 0)
            {
                lbl_nombre.Text = Texte.NombreFichierListe + " " + (liste.SelectedIndex + 1) + " / " + Principale.nbFichier;
                lbl_nombre.Location = new Point((this.Size.Width / 2) - (lbl_nombre.Size.Width / 2) - 4, this.Size.Height - 63);

                btn_goto.Enabled = true;
            }
        }

        private void liste_DoubleClick(object sender, EventArgs e) { Goto(); }
        private void btn_goto_Click(object sender, EventArgs e) { Goto(); }

        void Goto()
        {
            if (doubleClic)
            {
                if (liste.SelectedIndex < 0 || liste.SelectedIndex >= Principale.nbFichier)
                {
                    MessageBox.Show(Texte.ErreurListeFichier, Texte.ErreurTitre, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    Principale.id = liste.SelectedIndex;

                    this.DestroyHandle();
                }
            }
        }

        private void ListeFichier_SizeChanged(object sender, EventArgs e)
        {
            btn_annuler.Location = new Point(13, this.Size.Height - 68);
            btn_goto.Location = new Point(this.Size.Width - 110, this.Size.Height - 68);
            lbl_nombre.Location = new Point((this.Size.Width/2) - (lbl_nombre.Size.Width / 2) - 4, this.Size.Height - 63);

            liste.Size = new Size(this.Size.Width - 42, this.Size.Height - 89);
        }
    }
}
