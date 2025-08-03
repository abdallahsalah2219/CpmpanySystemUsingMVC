using DAL.Models;
using System.Net;
using System.Net.Mail;

namespace CompanySystem.PL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("salahabdallah661@gmail.com", "vhbvpsqyrmlueeoh");
            client.Send("salahabdallah661@gmail.com",email.Recipients,email.Subject,email.Body);
        }
    }
}
