namespace Vimata.Services.Implementations
{
    using MimeKit;
    using MimeKit.Text;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Vimata.Common;
    using Vimata.Services.Contracts;

    public class VimataEmailService : IEmailService
    {
        private readonly string Smtp = "smtp.abv.bg";
        private readonly int Port = 465;
        private readonly string VimataAddress = "vimata.ellinikon@abv.bg";
        private readonly string Password;

        public VimataEmailService()
        {
            using (StreamReader r = new StreamReader("../Vimata.Common/common-data.json"))
            {
                string json = r.ReadToEnd();
                CommonData data = JsonConvert.DeserializeObject<CommonData>(json);
                this.Password = data.Password;
            }
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(VimataAddress));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(Smtp, Port);
                client.Authenticate(VimataAddress, Password);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
