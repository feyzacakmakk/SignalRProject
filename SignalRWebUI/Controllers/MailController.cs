using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SignalRWebUI.Dtos.MailDtos;
using MailKit.Net.Smtp;

namespace SignalRWebUI.Controllers
{
	public class MailController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(CreateMailDto createMailDto)
		{
			MimeMessage mimeMessage= new MimeMessage();

			MailboxAddress mailboxAddress = new MailboxAddress("SignalR Rezervasyon", "mail adresi");
			mimeMessage.From.Add(mailboxAddress);

			MailboxAddress mailboxAddressTo = new MailboxAddress("User", createMailDto.ReceiverMail);
			mimeMessage.To.Add(mailboxAddressTo);

			var bodybuilder=new BodyBuilder();
			bodybuilder.TextBody=createMailDto.Body;
			mimeMessage.Body = bodybuilder.ToMessageBody();

			mimeMessage.Subject = createMailDto.Subject;

            SmtpClient client = new SmtpClient();
			client.Connect("smtp.gmail.com", 587, false);
			client.Authenticate("mail adresi", "key");
			//mesajı göndereceğim mail adresi

			client.Send(mimeMessage);
			client.Disconnect(true);

			return RedirectToAction("Index","Category");
		}
	}
}
