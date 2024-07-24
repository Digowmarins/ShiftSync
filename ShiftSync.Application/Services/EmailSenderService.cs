using System.Net;
using System.Net.Mail;

namespace ShiftSync.Application.Services
{
    public class EmailSenderService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body, string nomeUsuario)
        {
            var fromAddress = new MailAddress("Seu email", "ShiftSync");
            var toAddress = new MailAddress(toEmail, nomeUsuario);
            string fromPassword = "Senha do email";

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}