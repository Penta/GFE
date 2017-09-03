using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Gfe.Fenetres
{
    public partial class APropos : Form
    {
        public APropos()
        {
            this.Icon = Properties.Resources.icone;

            InitializeComponent();

            lbl_version.Text += Principale.VERSION;
        }

        private void LienGitHub_Clic (object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("https://github.com/Penta/GFE"); }
        private void LienClasse_Clic (object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("http://www.gulix.fr/blog/spip.php?article22"); }
        private void LienLicence_Clic (object sender, LinkLabelLinkClickedEventArgs e) { Process.Start("http://www.gnu.org/licenses/gpl.html"); }
        private void BoutonOK_Clic (object sender, EventArgs e) { this.DestroyHandle(); }
    }
}
