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
        private List<NotificationCreator> notificationCreators = new List<NotificationCreator>();
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
            if (!notificationCreators.Any() || !notificationCreators.Any(nc => nc is PushNotificationCreator))
                notificationCreators.Add(new PushNotificationCreator());
            foreach(var nc in notificationCreators)
                if(nc is PushNotificationCreator pnc)
                {
                    Notification notif = pnc.GenerateNotification(savedText);
                    notif.SendNotification();
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!notificationCreators.Any() || !notificationCreators.Any(nc => nc is TelegramNotificationCreator))
                notificationCreators.Add(new TelegramNotificationCreator());
            foreach (var nc in notificationCreators)
                if (nc is TelegramNotificationCreator pnc)
                {
                    Notification notif = pnc.GenerateNotification(savedText);
                    notif.SendNotification();
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!notificationCreators.Any() || !notificationCreators.Any(nc => nc is EmailNotificationCreator))
                notificationCreators.Add(new EmailNotificationCreator());
            foreach (var nc in notificationCreators)
                if (nc is EmailNotificationCreator pnc)
                {
                    Notification notif = pnc.GenerateNotification(savedText);
                    notif.SendNotification();
                }
        }
    }
}
