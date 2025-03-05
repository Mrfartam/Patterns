using Microsoft.Toolkit.Uwp.Notifications;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace Bridge
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

    public interface INotificationImplementation
    {
        void SendNotification(string text);
    }

    abstract class NotificationCreator
    {
        protected INotificationImplementation implementation;

        public NotificationCreator(INotificationImplementation implementation)
        {
            this.implementation = implementation;
        }
        
        public abstract void SendNotification(string txt);
    }

    class PushNotificationCreator : NotificationCreator
    {
        public PushNotificationCreator(INotificationImplementation implementation) : base(implementation) { }

        public override void SendNotification(string txt)
        {
            implementation.SendNotification(txt);
        }
    }

    class TelegramNotificationCreator : NotificationCreator
    {
        public TelegramNotificationCreator(INotificationImplementation implementation) : base(implementation) { }

        public override void SendNotification(string txt)
        {
            implementation.SendNotification(txt);
        }
    }

    class EmailNotificationCreator : NotificationCreator
    {
        public EmailNotificationCreator(INotificationImplementation implementation) : base(implementation) { }

        public override void SendNotification(string txt)
        {
            implementation.SendNotification(txt);
        }
    }

    class PushNotification : INotificationImplementation
    {
        public void SendNotification(string text)
        {
            new ToastContentBuilder()
                .AddText("Новое push-уведомление!")
                .AddText(text)
                .SetToastDuration(ToastDuration.Short)
                .Show();
        }
    }

    class TelegramNotification : INotificationImplementation
    {
        private string nickname;
        private TelegramBotClient bot;

        public TelegramNotification(string nickname = "1280753537", string bot_token = "7746664717:AAEOqZgxkvMPZv_7Xl5Btgkhv5chebRsr2g")
        {
            this.nickname = nickname;
            this.bot = new TelegramBotClient(bot_token);
        }

        public async void SendNotification(string text)
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

    class EmailNotification : INotificationImplementation
    {
        private string sender_email;
        private string receiver_email;

        public EmailNotification(string sender_email = "notification.ooad@gmail.com", string receiver_email = "parfenovivan55@gmail.com")
        {
            this.sender_email = sender_email;
            this.receiver_email = receiver_email;
        }

        public async void SendNotification(string text)
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
