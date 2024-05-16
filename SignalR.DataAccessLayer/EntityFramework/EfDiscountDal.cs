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
    public class EfDiscountDal : GenericRepository<Discount>, IDiscountDal
    {
		private readonly SignalRContext _context;
        public EfDiscountDal(SignalRContext context) : base(context)
        {
			_context = context;
        }

		public void ChangeStatusToFalse(int id)
		{
			var value=_context.Discounts.Find(id);
			value.Status = false;
			_context.SaveChanges();
		}

		public void ChangeStatusToTrue(int id)
		{
			var value = _context.Discounts.Find(id);
			value.Status = true;
			_context.SaveChanges();
		}

		public List<Discount> GetListByStatusTrue()
		{
			var values=_context.Discounts.Where(x=>x.Status==true).ToList();
			return values;
		}
	}
}
