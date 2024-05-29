using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using Suggestions.Business.Abstract;
using Suggestions.DataAccess.Concrats;
using Suggestions.Entities.Models;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Suggestions.Business.Concrete
{
    public class MailManager : IMailService
    {
        protected readonly IRepositoryManager _manager;

        public MailManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public bool CheckEmail(string Email)
        {
            return _manager.User.DoesEmailExist(Email);
        }

        public bool EmailConfirmation(User user, int? UserCode)
        {
            if (user.ConfirmationCode == UserCode)
            {

                return true;
            }
            return false;
        }


        public void EmailSendCode(User user, string type)
        {
            string fromAddress = "suggestionscomfirmed@gmail.com";
            string key = "ouoj oxvm ytlb johd";

            string smtpAddress = "smtp.example.com";
            int portNumber = 587;

            // E-posta gönderici
            var fromEmail = new MailAddress(fromAddress, "Ahmet Can");

            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", fromAddress);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);
            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);
            var bodyBuilder = new BodyBuilder();
            if (type == "Register")
            {

                bodyBuilder.TextBody = $"Onay Kodunuz + {user.ConfirmationCode}";
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Onay Kodu";
            }
            else
            {
                bodyBuilder.TextBody = $"Kaydınız Başarılı Bir Şekilde Oluşturuldu";
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Kayıt Başarılı";
            }


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(fromAddress, key);
            client.Send(mimeMessage);
            client.Disconnect(true);

        }

    }
}
