using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
		private readonly SignalRContext _context;
        public EfBookingDal(SignalRContext context) : base(context)
        {
			_context = context;
        }

		public int BookingCount()
		{
			return _context.Bookings.Count();
		}

		public void BookingStatusApproved(int id)
		{
			var values=_context.Bookings.Find(id);
			values.Description = "Rezervasyon Onaylandı.";
			_context.SaveChanges();
		}

		public void BookingStatusCancaled(int id)
		{
			var values = _context.Bookings.Find(id);
			values.Description = "Rezervasyon İptal Edildi.";
			_context.SaveChanges();
		}
	}
}
