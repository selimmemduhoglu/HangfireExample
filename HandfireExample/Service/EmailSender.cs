using System.Threading.Tasks;
using System;

namespace HandfireExample.Service
{
    public class EmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Task.Delay(5000); // Simülasyon için 5 saniye bekleme süresi.

            Console.WriteLine($"E-posta gönderildi: {email}, Konu: {subject}, Mesaj: {message}");
        }
    }
}
