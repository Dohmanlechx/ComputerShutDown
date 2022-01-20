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
            int seconds = CalculateSeconds();
            RunShutdownCommand("-s -t " + (seconds).ToString());
        }

        private int CalculateSeconds()
        {
            int h = 0; 
            int m = 0;
            int totalSeconds = 0;

            if (int.TryParse(boxHours.Text, out _))
            {
                h = int.Parse(boxHours.Text);
            }

            if (int.TryParse(boxMinutes.Text, out _))
            {
                m = int.Parse(boxMinutes.Text);
            }

            totalSeconds += (h * 3600);
            totalSeconds += (m * 60);

            return totalSeconds;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (IsNotNumber(boxHours.Text))
            {
                ShowMessageNumbersOnly();
                DeleteInvalidInput(boxHours);
            }
        }

        private void lblHeader_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (IsNotNumber(boxMinutes.Text))
            {
                ShowMessageNumbersOnly();
                DeleteInvalidInput(boxMinutes);
            }
        }

        bool IsNotNumber(String txt)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(txt, "[^0-9]");
        }

        void ShowMessageNumbersOnly()
        {
            MessageBox.Show("Please enter only numbers.");
        }

        void DeleteInvalidInput(TextBox txtBox) 
        {
            txtBox.Text = txtBox.Text.Remove(0, txtBox.Text.Length);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}