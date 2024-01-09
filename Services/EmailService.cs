using System.Net;
using System.Net.Mail;

namespace fb_API.Services
{
	public class EmailService : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string message)
		{
			var mail = "johnnartey99@gmail.com";
			var pw = "mquv estr jpog ajkm";

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
