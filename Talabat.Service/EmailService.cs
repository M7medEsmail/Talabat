using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.Services;

namespace Talabat.Service
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Email email)
        {
            var EmailRequest = new MimeMessage();
            EmailRequest.To.Add(MailboxAddress.Parse(email.To.ToString()));
            EmailRequest.Subject = email.Subject;
            EmailRequest.Body = new TextPart(TextFormat.Html) { Text = email.Body};
            

        }
    }
}
