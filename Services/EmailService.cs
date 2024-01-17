using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace fb_API.Services
{
	public class EmailService : IEmailSender
	{
		private readonly IConfiguration _configuration;
		public EmailService(IConfiguration config)
		{
			_configuration = config;
		}
		public Task SendEmailAsync(string email, string subject, string message)
		{
			var mail = _configuration["emailService:email"];
			var pw = _configuration["emailService:emailPassword"];

			var client = new SmtpClient("smtp.gmail.com", 587)
			{
				Credentials = new NetworkCredential(mail, pw),
				EnableSsl = true
			};

			return client.SendMailAsync(
								new MailMessage(mail, email, subject, message)
								{
					IsBodyHtml = true
				}
											);

			throw new NotImplementedException();
		}
	}
}
