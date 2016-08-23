using System;
using System.Windows.Forms;

namespace PlasticNotifier
{
    public partial class NotificationForm : Form
    {
        public NotificationForm(string title, string text)
        {
            InitializeComponent();

            IconLabel.Image = Resources.plasticlogo;

            TitleLabel.Text = title;
            DetailsTextBox.Text = text;
        }

        void NotificationWindow_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        void NotificationForm_Click(object sender, EventArgs e)
        {
            // stop auto hide
            timer.Enabled = false;

            // show border
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

            this.Text = "Plastic SCM - geolocated checkin";

            // and relocate
            this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
        }
    }
}
