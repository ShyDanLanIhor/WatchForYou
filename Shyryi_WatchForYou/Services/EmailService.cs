using System;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.IO;

namespace Shyryi_WatchForYou.Services
{
    public static class EmailService
    {
        public static void SendConfirmationEmail(string userEmail, string confirmationLink)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("bohdan.shyryi@gmail.com", "qovebkpckuqhqsdc")
            };

            client.Send(new MailMessage
            {
                From = new MailAddress("bohdan.shyryi@gmail.com"),
                To = { new MailAddress(userEmail) },
                IsBodyHtml = true,
                Subject = "Verification",
                Body =
                $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='background-color: #f0f0f0; padding: 20px;'>
                            <h1 style='color: #0070c0;'>Verification</h1>
                            <p>To verify your account, follow the <a href='{confirmationLink}'>link</a></p>
                        </div>
                    </body>
                    </html>
                 "
            });
        }

        public static void SendPasswordOnEmail(string userEmail, string password)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("bohdan.shyryi@gmail.com", "qovebkpckuqhqsdc")
            };

            client.Send(new MailMessage
            {
                From = new MailAddress("bohdan.shyryi@gmail.com"),
                To = { new MailAddress(userEmail) },
                IsBodyHtml = true,
                Subject = "Verification",
                Body =
                $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='background-color: #f0f0f0; padding: 20px;'>
                            <h1 style='color: #0070c0;'>New password</h1>
                            <p>Please, don`t share info about your new pasword: {password}</p>
                        </div>
                    </body>
                    </html>
                 "
            });
        }

        private static bool IsEmailAccountValid(string emailAddressToCheck)
        {
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("bohdan.shyryi@gmail.com", "qovebkpckuqhqsdc");
                    client.EnableSsl = true;

                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress("bohdan.shyryi@gmail.com");
                        message.Subject = "Перевірка існування адреси";
                        message.Body = "Це тестове повідомлення для перевірки існування адреси.";
                        message.To.Add(emailAddressToCheck);

                        client.Send(message);

                        Console.WriteLine("Лист надіслано успішно.");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GenerateUniqueToken(int length)
        {
            using (RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[length];
                cryptoProvider.GetBytes(tokenData);

                string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                char[] tokenChars = new char[length];

                for (int i = 0; i < tokenData.Length; i++)
                {
                    tokenChars[i] = allowedCharacters[tokenData[i] % allowedCharacters.Length];
                }

                return new string(tokenChars);
            }
        }
    }
}
