using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ComputerShutDown
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RunShutdownCommand("-s -t " + (60).ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RunShutdownCommand("-a");
        }

        private void RunShutdownCommand(String arguments)
        {
            Process pr = new Process();
            pr.StartInfo.FileName = "shutdown";
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.Arguments = arguments;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblHeader_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(boxHours.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                boxHours.Text = boxHours.Text.Remove(boxHours.Text.Length - 1);
            }
        }

        private void lblHeader_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}