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

    class PushNotificationCreator
    {
        public PushNotification GenerateNotification(string txt)
        {
            PushNotification notification = new PushNotification(txt);
            return notification;
        }
    }

    class TelegramNotificationCreator
    {
        public TelegramNotification GenerateNotification(string txt)
        {
            TelegramNotification notification = new TelegramNotification(txt);
            return notification;
        }
    }

    class EmailNotificationCreator
    {
        public EmailNotification GenerateNotification(string txt)
        {
            EmailNotification notification = new EmailNotification(txt);
            return notification;
        }
    }

    class PushNotification
    {
        public string text;

        public PushNotification(string txt)
        {
            text = txt;
        }
        public void SendNotification()
        {
            new ToastContentBuilder().
                AddText("Новое push-уведомление!").
                AddText(text).
                SetToastDuration(ToastDuration.Short).
                Show();
        }
    }

    class TelegramNotification
    {
        public string text;
        public string nickname;
        public TelegramBotClient bot;

        public TelegramNotification(string txt)
        {
            text = txt;
            nickname = "1280753537";
            bot = new TelegramBotClient("7746664717:AAEOqZgxkvMPZv_7Xl5Btgkhv5chebRsr2g");
        }
        public async void SendNotification()
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

    class EmailNotification
    {
        public string text;
        public string sender_email;
        public string receiver_email;

        public EmailNotification(string txt)
        {
            text = txt;
            sender_email = "notification.ooad@gmail.com";
            receiver_email = "parfenovivan55@gmail.com";
        }
        public async void SendNotification()
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
