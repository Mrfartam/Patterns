using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabricMethod
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
            PushNotificationCreator creator = new PushNotificationCreator();
            PushNotification notif = creator.GenerateNotification(savedText);
            notif.SendNotification();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TelegramNotificationCreator creator = new TelegramNotificationCreator();
            TelegramNotification notif = creator.GenerateNotification(savedText);
            notif.SendNotification();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EmailNotificationCreator creator = new EmailNotificationCreator();
            EmailNotification notif = creator.GenerateNotification(savedText);
            notif.SendNotification();
        }
    }
}
