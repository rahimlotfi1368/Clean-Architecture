using GlobalTicketManagement.Application.Contracts.Infrastructure;
using GlobalTicketManagement.Application.Models.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GlobalTicketManagement.Infrastructure.Services.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }
        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _emailSettings = mailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email Sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email sending failed");

            return false;
        }
    }
}
