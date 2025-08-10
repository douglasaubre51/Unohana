using MailKit.Net.Smtp;
using MimeKit;

namespace Unohana.Api.Services.Email
{
    public class SendOtpInEmail
    {
        public void Send(string otp, string targetName, string targetMail)
        {
            var message = new MimeMessage();
            message.From.Add(
                new MailboxAddress("douglasaubre51", "douglasaubre@gmail.com")
                );
            message.To.Add(
                new MailboxAddress(targetName, targetMail)
                );
            message.Subject = "Otp for Unohana";
            message.Body = new TextPart("html")
            {
                Text = @$"<html>
                            <body>
                                <h1>Hello {targetName},</h1>
                                <h3>This is your one time password!</h3>
                                <h5>password is valid for 2 minutes!</h5>
                                <h1>{otp}</h1>
                            </body>
                         </html>"
            };
            using var smtpClient = new SmtpClient();
            smtpClient.Connect(
                "smtp.gmail.com",
                587,
                MailKit.Security.SecureSocketOptions.StartTls
                );
            smtpClient.Authenticate(
                "douglasaubre@gmail.com",
                "igbc knrq xuhw deej"
                );
            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}
