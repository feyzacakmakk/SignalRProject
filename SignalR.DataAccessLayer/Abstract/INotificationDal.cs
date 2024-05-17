using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
	public interface INotificationDal:IMessageDal<Notification>
	{
		int NotificationCountByStatusFalse();
		List<Notification> GetAllNotificationByFalse();
		//Durumu false olan yani okunmamış bildirimleri getirecek
		void NotificationStatusChangeToTrue(int id); //okundu
		void NotificationStatusChangeToFalse(int id); //okunmamış
	}
}
