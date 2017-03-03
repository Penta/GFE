using System;
using System.Threading;
using System.Windows.Forms;

namespace Gfe
{
    public partial class Attente : Form
    {
        Thread t = new Thread(new ThreadStart(ThreadProc));

        public new void Show(){ t.Start(); }
        public new void Close() { t.Abort(); }

        static public void ThreadProc() { Application.Run(new Attente()); }

        public Attente()
        {
            this.Icon = Properties.Resources.icone;
            InitializeComponent();
        }

        private void Attente_FormClosed(object sender, FormClosedEventArgs e) { Environment.Exit(0); }
    }
}
