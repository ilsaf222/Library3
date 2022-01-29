using Library.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.EmailServices
{
    public class EmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendMessageAsync(List<EmailAddress> addressTo, string subject, string content)
        {
            EmailConfiguration emailConfiguration = new EmailConfiguration
            {
                SmtpPort = configuration.GetValue<int>("EmailProperties:SmtpPort"),
                SmtpPassword = configuration.GetValue<string>("EmailProperties:SmtpPassword"),
                SmtpUsername = configuration.GetValue<string>("EmailProperties:SmtpUsername"),
                SmtpServer = configuration.GetValue<string>("EmailProperties:SmtpServer")
            };

            EmailService service = new(emailConfiguration);

            var addressFrom = new List<EmailAddress>
            {
                new EmailAddress() {
                    Address = configuration.GetValue<string>("EmailProperties:FromAdress"),
                    Name = configuration.GetValue<string>("EmailProperties:AdressName")
                }
            };

            EmailMessage message = new EmailMessage
            {
                Subject = subject,
                Content = content,
                FromAddresses = addressFrom,
                ToAddresses = addressTo
            };

            await service.SendAsync(message);
        }
    }
}
