﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace Gestionnaire_de_Fond_d_Écran
{
    public partial class Attente : Form
    {
        Thread t = new Thread(new ThreadStart(ThreadProc));
        static public bool montrerIconeTaskbar = true;

        public new void Show(){ t.Start(); }
        public new void Close() { t.Abort(); }

        static public void ThreadProc()
        {
            Thread.Sleep(500);
            Application.Run(new Attente());
        }

        public Attente() {
            this.ShowInTaskbar = montrerIconeTaskbar;
            this.Icon = Properties.Resources.icone;

            InitializeComponent();
        }

        private void Attente_FormClosed(object sender, FormClosedEventArgs e) { Environment.Exit(0); }
    }
}
