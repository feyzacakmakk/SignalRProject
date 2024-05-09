using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpGet]
		public IActionResult NotificationList()
		{
			return Ok(_notificationService.TGetListAll());
		}

		[HttpGet("NotificationCountByStatusFalse")]
		public IActionResult NotificationCountByStatusFalse()
		{
			return Ok(_notificationService.TNotificationCountByStatusFalse());
		}

		[HttpGet("GetAllNotificationByFalse")]
		public IActionResult GetAllNotificationByFalse()
		{
			return Ok(_notificationService.TGetAllNotificationByFalse());
		}

		[HttpPost]
		public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
		{
			Notification notification = new Notification()
			{
				Description = createNotificationDto.Description,
				Icon = createNotificationDto.Icon,
				Status = false, //başlangıçta hepsi false olsun
				Type = createNotificationDto.Type,
				Date = Convert.ToDateTime(DateTime.Now.ToShortDateString())
			};
			_notificationService.TAdd(notification);
			return Ok("Bildirim Eklendi");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteNotification(int id)
		{
			var value=_notificationService.TGetByID(id);
			_notificationService.TDelete(value);
			return Ok("Bildirim Silindi");
		}

		[HttpGet("{id}")]
		public IActionResult GetNotification(int id)
		{
			var value = _notificationService.TGetByID(id);
			return Ok(value);
		}

		[HttpPut]
		public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
		{
			Notification notification = new Notification()
			{
				NotificationID=updateNotificationDto.NotificationID,
				Description = updateNotificationDto.Description,
				Icon = updateNotificationDto.Icon,
				Status = updateNotificationDto.Status,
				Type = updateNotificationDto.Type,
				Date = updateNotificationDto.Date
			};
			_notificationService.TUpdate(notification);
			return Ok("Güncelleme Yapıldı");
		}

		[HttpGet("NotificationStatusChangeToFalse/{id}")]
		public IActionResult NotificationStatusChangeToFalse(int id)
		{
			_notificationService.TNotificationStatusChangeToFalse(id);
			return Ok("Okunmadı olarak işaretlendi");
		}

		[HttpGet("NotificationStatusChangeToTrue/{id}")]
		public IActionResult NotificationStatusChangeToTrue(int id)
		{
			_notificationService.TNotificationStatusChangeToTrue(id);
			return Ok("Okundu olarak işaretlendi");
		}
	}
}
