using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ComputerShutDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.FileName = "shutdown";
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.Arguments = "-s -t 10";
            pr.StartInfo.RedirectStandardOutput = true;
            pr.Start();
        }
    }
}
