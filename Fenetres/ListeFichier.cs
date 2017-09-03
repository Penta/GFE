﻿using System;
using System.IO;
using System.Windows.Forms;
using Gfe.Langues;

namespace Gfe.Fenetres
{
    public partial class ListeFichier : Form
    {
        public ListeFichier(FileInfo[] fichiers)
        {
            string[] listeFichier = new string[65536];

            for (int i = 0; i < fichiers.Length; i++)
            {
                if(fichiers[i] != null)
                    listeFichier[i] = fichiers[i].Name;
            }

            InitializeComponent();

            lbl_nombre.Text = Texte.NombreFichierListe + " 0 / " + Principale.nbFichier;

            if (listeFichier.Length > 0)
            {
                liste.Items.Clear();

                for (int i = 0; i < listeFichier.Length; i++)
                {
                    if(listeFichier[i] != null)
                        liste.Items.Add(listeFichier[i].ToString());
                }
            }
        }

        private void btn_annuler_Click(object sender, EventArgs e) { this.DestroyHandle(); }

        private void liste_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_nombre.Text = Texte.NombreFichierListe + " " + (liste.SelectedIndex + 1) + " / " + Principale.nbFichier;
        }

        private void liste_DoubleClick(object sender, EventArgs e) { Goto(); }
        private void btn_goto_Click(object sender, EventArgs e) { Goto(); }

        void Goto()
        {
            if (liste.SelectedIndex < 0 || liste.SelectedIndex >= Principale.nbFichier)
            {
                MessageBox.Show(Texte.ErreurListeFichier);
            }
            else
            {
                Principale.id = liste.SelectedIndex;

                this.DestroyHandle();
            }
        }
    }
}