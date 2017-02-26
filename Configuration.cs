using System;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Configuration : Form
    {
        static public bool changement = false;
        static public bool changementExt = false;

        public Configuration()
        {
            InitializeComponent();

            cb_dispo.Text = Principale.affichage;
            txt_externe.Text = Principale.logiciel;
            check_rappel.Checked = Principale.rappel;
            txt_extension.Text = Principale.extension;
        }

        private void btn_appliquer_Click(object sender, EventArgs e)
        {
            if (Principale.extension != txt_extension.Text)
                changementExt = true;

            changement = true;

            Principale.affichage = cb_dispo.Text;
            Principale.logiciel = txt_externe.Text;
            Principale.rappel = check_rappel.Checked;
            Principale.extension = txt_extension.Text;

            Registre.miseAjourConfig();

            this.DestroyHandle();
        }
    }
}
