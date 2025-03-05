using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bridge
{
    public partial class Form1 : Form
    {
        private string savedText;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            savedText = textBox1.Text;
            if (!button2.Enabled && !button2.Visible)
            {
                button2.Enabled = true;
                button2.Visible = true;
                button3.Enabled = true;
                button3.Visible = true;
                button4.Enabled = true;
                button4.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            INotificationImplementation notif = new PushNotification();
            NotificationCreator pushNotif = new PushNotificationCreator(notif);
            pushNotif.SendNotification(savedText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            INotificationImplementation notif = new TelegramNotification();
            NotificationCreator pushNotif = new TelegramNotificationCreator(notif);
            pushNotif.SendNotification(savedText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            INotificationImplementation notif = new EmailNotification();
            NotificationCreator pushNotif = new EmailNotificationCreator(notif);
            pushNotif.SendNotification(savedText);
        }
    }
}
