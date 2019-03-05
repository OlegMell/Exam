using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibChat
{
    internal class AuthenticationHelper
    {
        private const string PASSWORD = "useruser123";
        private const string MAIL = "yarhavtest@gmail.com";
        private const int PORT = 587;
        private const string HOST = "smtp.gmail.com";
        private readonly SmtpClient smtpClient;
        private readonly MailAddress from = new MailAddress(MAIL);
    }
}
