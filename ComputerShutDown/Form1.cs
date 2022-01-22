using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ComputerShutDown
{
    public partial class Form : System.Windows.Forms.Form
    {
        DateTime dateTimeOfStartedTimer;
        DateTime dateTimeOfShutDown;
        bool isRunning = false;

        public Form()
        {
            InitializeComponent();
            BuildMenu();

            timer1.Interval = 1000;
            timer1.Enabled = true;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void BuildMenu()
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Index = 0;
            menuItem.Text = "Exit";
            menuItem.Click += new EventHandler(menuItem_Click);

            ContextMenu contextMenu;
            contextMenu = new ContextMenu();

            contextMenu.MenuItems.AddRange(new MenuItem[] { menuItem });
            notifyIcon.ContextMenu = contextMenu;
        }

        private void menuItem_Click(object Sender, EventArgs e)
        {
            RunShutdownCommand("-a");
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isRunning)
            {
                RunShutdownCommand("-a");
                dateTimeOfStartedTimer = DateTime.MinValue;
                dateTimeOfShutDown = DateTime.MinValue;
                isRunning = false;
            } 
            else
            {
                int seconds = CalculateSeconds();
                if (seconds != -1)
                {
                    dateTimeOfStartedTimer = DateTime.Now;
                    dateTimeOfShutDown = dateTimeOfStartedTimer.AddSeconds(seconds);
                    RunShutdownCommand("-s -t " + (seconds).ToString());
                    isRunning = true;
                }
            }

            ToggleButtonUI();
        }

        private void ToggleButtonUI()
        {
            if (isRunning)
            {
                btnShutdown.Text = "Abort";
                btnShutdown.BackColor = Color.Red;
            }
            else
            {
                btnShutdown.Text = "Apply";
                btnShutdown.BackColor = Color.SeaGreen;
            }
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

            totalSeconds += (h * 3600) + (m * 60);

            if (totalSeconds == 0)
            {
                ShowMessageMustEnterAnyNumber();
                return -1;
            }

            return totalSeconds;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (IsNotNumber(boxHours.Text))
            {
                ShowMessageNumbersOnly();
                DeleteInvalidInput(boxHours);
            }
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

        void ShowMessageMustEnterAnyNumber()
        {
            MessageBox.Show("Please enter any value first.");
        }

        void ShowMessageNumbersOnly()
        {
            MessageBox.Show("Please enter only numbers.");
        }

        void DeleteInvalidInput(TextBox txtBox) 
        {
            txtBox.Text = txtBox.Text.Remove(0, txtBox.Text.Length);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                notifyIcon.Visible = true;
                notifyIcon.BalloonTipText = "The application is minimized and still running.";
                notifyIcon.ShowBalloonTip(1000);

                this.Hide();
                e.Cancel = true;
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dateTimeOfStartedTimer == DateTime.MinValue || dateTimeOfShutDown == DateTime.MinValue)
            {
                progressBar1.Value = 0;
                lblTimer.Visible = false;
                return;
            }
            double secondsFromStartToEnd = dateTimeOfShutDown.Subtract(dateTimeOfStartedTimer).TotalSeconds;
            DateTime now = DateTime.Now;
            double secondsFromNowToStart = now.Subtract(dateTimeOfStartedTimer).TotalSeconds;
            double progress = secondsFromNowToStart / secondsFromStartToEnd;
            progressBar1.Value = (int) (progress * 100);
            int minutesLeft = (int) Math.Ceiling(((secondsFromStartToEnd - secondsFromNowToStart) / 60));
            lblTimer.Text = minutesLeft.ToString() + " min left";
            lblTimer.Visible = true;
        }
    }
}