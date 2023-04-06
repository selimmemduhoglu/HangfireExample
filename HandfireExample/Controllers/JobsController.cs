//using HandfireExample.Service;
//using Hangfire;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace HandfireExample.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class JobsController : ControllerBase
//    {
//        [HttpPost("send-email")]
//        public IActionResult SendEmail()
//        {
//            var jobId = BackgroundJob.Enqueue<EmailSender>(sender => sender.SendEmailAsync("test@example.com", "Hangfire Test", "Hello from Hangfire!"));

//            return Ok($"E-posta gönderme işlemi başlatıldı. Job ID: {jobId}");
//        }
//    }
//}

using System;
using HandfireExample.Service;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        [HttpPost("send-email/enqueue")]
        public IActionResult EnqueueEmail()
        {
            var jobId = BackgroundJob.Enqueue<EmailSender>(sender => sender.SendEmailAsync("test@example.com", "Enqueue Test", "Hello from Enqueue!"));

            return Ok($"E-posta gönderme işlemi başlatıldı (Enqueue). Job ID: {jobId}");
        }

        [HttpPost("send-email/schedule")]
        public IActionResult ScheduleEmail()
        {
            var jobId = BackgroundJob.Schedule<EmailSender>(sender => sender.SendEmailAsync("test@example.com", "Scheduled Test", "Hello from Schedule!"), TimeSpan.FromSeconds(30));

            return Ok($"E-posta gönderme işlemi planlandı (Schedule). Job ID: {jobId}");
        }

        [HttpPost("send-email/recurring")]
        public IActionResult RecurringEmail()
        {
            RecurringJob.AddOrUpdate<EmailSender>("recurring-email-job", sender => sender.SendEmailAsync("test@example.com", "Recurring Test", "Hello from Recurring!"), "*/5 * * * * *");

            return Ok($"E-posta gönderme işlemi tekrarlayan olarak ayarlandı (Recurring).");
        }

        [HttpPost("send-email/continuewith")]
        public IActionResult ContinueWithEmail()
        {
            var jobId = BackgroundJob.Enqueue<EmailSender>(sender => sender.SendEmailAsync("test@example.com", "First Test", "Hello from First Job!"));
            var secondJobId = BackgroundJob.ContinueWith<EmailSender>(jobId, sender => sender.SendEmailAsync("test@example.com", "ContinueWith Test", "Hello from Second Job!"));

            return Ok($"E-posta gönderme işlemleri başlatıldı (ContinueWith). İlk Job ID: {jobId}, İkinci Job ID: {secondJobId}");
        }
    }
}

