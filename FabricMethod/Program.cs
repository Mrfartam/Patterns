using Microsoft.Toolkit.Uwp.Notifications;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using static System.Net.Mime.MediaTypeNames;

namespace FabricMethod
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
        }
    }
    abstract class NotificationCreator
    {
        public Notification notification;
        public NotificationCreator()
        {
            notification = null;
        }
        public abstract Notification GenerateNotification(string txt);
    }

    abstract class Notification
    {
        public string text;
        public Notification(string txt)
        {
            text = txt;
        }
        public abstract void SendNotification();
    }
    class PushNotificationCreator: NotificationCreator
    {
        public override Notification GenerateNotification(string txt)
        {
            if(notification == null)
                notification = new PushNotification(txt);
            else
                notification.text = txt;
            return notification;
        }
    }

    class TelegramNotificationCreator: NotificationCreator
    {
        private string nickname;
        private string bot_token;

        public TelegramNotificationCreator(string nick = "1280753537",
            string bot_tok = "7746664717:AAEOqZgxkvMPZv_7Xl5Btgkhv5chebRsr2g")
        {
            this.nickname = nick;
            this.bot_token = bot_tok;
        }

        public override Notification GenerateNotification(string txt)
        {
            if (notification == null)
                notification = new TelegramNotification(txt, nickname, bot_token);
            else
                notification.text = txt;
            return notification;
        }
    }

    class EmailNotificationCreator: NotificationCreator
    {
        private string sender_email;
        private string receiver_email;

        public EmailNotificationCreator(string sender = "notification.ooad@gmail.com",
            string receiver = "parfenovivan55@gmail.com")
        {
            this.sender_email = sender;
            this.receiver_email = receiver;
        }

        public override Notification GenerateNotification(string txt)
        {
            if (notification == null)
                notification = new EmailNotification(txt, sender_email, receiver_email);
            else
                notification.text = txt;
            return notification;
        }
    }

    class PushNotification: Notification
    {
        public PushNotification(string txt) : base(txt) { }
        public override void SendNotification()
        {
            new ToastContentBuilder().
                AddText("Новое push-уведомление!").
                AddText(text).
                SetToastDuration(ToastDuration.Short).
                Show();
        }
    }

    class TelegramNotification: Notification
    {
        private string nickname;
        private TelegramBotClient bot;

        public TelegramNotification(string txt, string nick, string bot_token) : base(txt)
        {
            nickname = nick;
            bot = new TelegramBotClient(bot_token);
        }
        public override async void SendNotification()
        {
            try
            {
                await bot.SendMessage(nickname, $"Новое уведомление в телеграме!\n{text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    class EmailNotification: Notification
    {
        private string sender_email;
        private string receiver_email;

        public EmailNotification(string txt, string send_email, string receive_email): base(txt)
        {
            sender_email = send_email;
            receiver_email = receive_email;
        }
        public override async void SendNotification()
        {
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 465;
            bool useSsl = true;

            string sender_pass = "xkae rird aewv nzzu";

            string subject = "Новое уведомление на почту!";
            string body = text;

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Notification", sender_email));
                message.To.Add(new MailboxAddress("Получатель", receiver_email));
                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = body
                };
                
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, useSsl);
                    await client.AuthenticateAsync(sender_email, sender_pass);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
