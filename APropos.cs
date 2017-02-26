using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class APropos : Form
    {
        public APropos()
        {
            InitializeComponent();

            lbl_version.Text += Principale.VERSION;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("http://penta.fr.cr/GFdE/"); }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("http://www.gulix.fr/blog/spip.php?article22"); }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("http://www.gnu.org/licenses/gpl.html"); }
        private void button1_Click(object sender, EventArgs e) { this.DestroyHandle(); }
    }
}
