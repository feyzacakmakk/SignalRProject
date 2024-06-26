﻿using Microsoft.EntityFrameworkCore;
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
    public class EfProductDal :GenericRepository<Product>, IProductDal
    {
        private readonly SignalRContext _context;
        public EfProductDal(SignalRContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetProductsWithCategories()
        {
            //Include ile Category'i Product'ın içine dahil ediyoruz
            var values=_context.Products.Include(x=>x.Category).ToList();
            return values;
        }

		public int ProductCount()
		{
			return _context.Products.Count();
		}

		public int ProductCountByCategoryNameDrink()
		{
			return _context.Products.Where(x => x.CategoryID == (_context.Categories.Where(y => y.CategoryName == "İçecek").Select(z => z.CategoryID).FirstOrDefault())).Count();
		}

		public int ProductCountByCategoryNameHamburger()
		{
			return _context.Products.Where(x => x.CategoryID == (_context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryID).FirstOrDefault())).Count();
		}

		public string ProductNameByMaxPrice()
		{
			return _context.Products.Where(x => x.Price == (_context.Products.Max(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
			//(_context.Products.Max(y => y.Price)->max ürün fiyatını veriyorr
			//select ile ürüün adını çekiyoruz
		}

		public string ProductNameByMinPrice()
		{
			return _context.Products.Where(x => x.Price == (_context.Products.Min(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
		}

		public decimal ProductPriceAvg()
		{
			return _context.Products.Average(x => x.Price);
		}

		public decimal ProductPriceAvgHamburger()
		{
			return _context.Products.Where(x => x.CategoryID == (_context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryID).FirstOrDefault())).Average(w=>w.Price);
		}

		public decimal ProductPriceBySteakBurger()
		{
			return _context.Products.Where(x=>x.ProductName=="Steak Burger").Select(y=>y.Price).FirstOrDefault();
		}
	}
}
