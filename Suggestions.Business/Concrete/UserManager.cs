using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Suggestions.Business.Abstract;
using Suggestions.DataAccess.Concrats;
using Suggestions.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Suggestions.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;

        public UserManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public bool CheckEmail(string Email)
        {
            return _manager.User.DoesEmailExist(Email);
        }

        public bool CheckUser(string Email, string password)
        {
            var user = GetUser(Email);
            if (user == null)
            {
                return false;
            }
            if (user.Password == password)
            {
                return true;
            }
            return false;
        }

        public bool CreateUser(User user)
        {

            if (_manager.User.Create(user) is null)
            {
                return false;
            }
            
            _manager.Save();
            return true;

        }

        public void DeleteUser(User user)
        {
            if (!_manager.User.DoesEmailExist(user.Email))
            {
                _manager.User.Delete(user);
            }

        }

        public bool EmailConfirmation(User user, int? UserCode)
        {
            if (user.ConfirmationCode == UserCode)
            {
                CreateUser(user);
                return true;
            }
            return false;
        }

        public void EmailSendCode(User user)
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
            bodyBuilder.TextBody = $"Onay Kodunuz + {user.ConfirmationCode}";
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Onay Kodu";

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(fromAddress, key);
            client.Send(mimeMessage);
            client.Disconnect(true);

        }

        public List<User> GetAllUser()
        {
            return _manager.User.GetAlUsers();
        }

        public User? GetUser(string Email)
        {
            var Liste = _manager.User.GetAlUsers();
            return Liste.FirstOrDefault(u => u.Email == Email);
        }

        public void UpdateUser(User user)
        {
            if (!_manager.User.DoesEmailExist(user.Email))
            {
                _manager.User.Update(user);
            }
        }
    }
}
