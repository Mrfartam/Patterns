//Без паттерна
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

    public TelegramNotificationCreator(string nickname = "1280753537",
        string bot_token = "7746664717:AAEOqZgxkvMPZv_7Xl5Btgkhv5chebRsr2g")
    {
        this.nickname = nickname;
        this.bot_token = bot_token;
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

//С паттерном
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