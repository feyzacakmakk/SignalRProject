﻿using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using System.Configuration;

namespace SignalRApi.Hubs
{
    public class SignalRHub:Hub
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMenuTableService _menusTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

		public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService)
		{
			_categoryService = categoryService;
			_productService = productService;
			_orderService = orderService;
			_moneyCaseService = moneyCaseService;
			_menusTableService = menuTableService;
			_bookingService = bookingService;
			_notificationService = notificationService;
		}

		static int clientCount= 0;


        public async Task SendNotification()
		{
			var values = _notificationService.TNotificationCountByStatusFalse();
			await Clients.All.SendAsync("ReceiveNotificationCountByStatusFalse", values);

			var notificationListByFalse = _notificationService.TGetAllNotificationByFalse();
			await Clients.All.SendAsync("ReceiveNotificationListByFalse", notificationListByFalse);
		}

		public async Task SendBookingList()
		{
			var values=_bookingService.TGetListAll();
			await Clients.All.SendAsync("ReceiveBookingList", values);

			var values2 = _bookingService.TBookingCount();
			await Clients.All.SendAsync("ReceiveBookingListCount", values2);
		}


		public async Task SendProgress()
		{
			var value = _moneyCaseService.TTotalMoneyCaseAmount();
			await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value.ToString("0.00") + "₺");

			var value2 = _orderService.TActiveOrderCount();
			await Clients.All.SendAsync("ReceiveTActiveOrderCount", value2);

			var value3 = _menusTableService.TMenuTableCount();
			await Clients.All.SendAsync("ReceiveMenuTableCount", value3);

			var value5 = _productService.TProductPriceAvg();
			await Clients.All.SendAsync("ReceiveProductPriceAvg", value5);

			var value6 = _productService.TProductPriceAvgHamburger();
			await Clients.All.SendAsync("ReceiveAvgPriceByHamburger", value6);

			var value7 = _productService.TProductCountByCategoryNameDrink();
			await Clients.All.SendAsync("ReceiveProductCountByCategoryNameDrink", value7);

			var value8 = _orderService.TTotalOrderCount();
			await Clients.All.SendAsync("ReceiveTotalOrderCount", value8);

			var value9 = _productService.TProductPriceBySteakBurger();
			await Clients.All.SendAsync("ReceiveProductPriceBySteakBurger", value9);
		}

		public async Task SendStatistic()
		{
			var value = _categoryService.TCategoryCount();
			await Clients.All.SendAsync("ReceiveCategoryCount", value);

			var value2 = _productService.TProductCount();
			await Clients.All.SendAsync("ReceiveProductCount", value2);

			var value3 = _categoryService.TActiveCategoryCount();
			await Clients.All.SendAsync("ReceiveActiveCategoryCount", value3);

			var value4 = _categoryService.TPassiveCategoryCount();
			await Clients.All.SendAsync("ReceivePassiveCategoryCount", value4);

			var value5 = _productService.TProductCountByCategoryNameHamburger();
			await Clients.All.SendAsync("ReceiveProductCountByCategoryNameHamburger", value5);

			var value6 = _productService.TProductCountByCategoryNameDrink();
			await Clients.All.SendAsync("ReceiveProductCountByCategoryNameDrink", value6);

			var value7 = _productService.TProductPriceAvg();
			await Clients.All.SendAsync("ReceiveProductPriceAvg", value7.ToString("0.00" +"₺"));

			var value8 = _productService.TProductNameByMaxPrice();
			await Clients.All.SendAsync("ReceiveProductNameByMaxPrice", value8);

			var value9 = _productService.TProductNameByMinPrice();
			await Clients.All.SendAsync("ReceiveProductNameByMinPrice", value9);

			var value10 = _productService.TProductPriceAvgHamburger();
			await Clients.All.SendAsync("ReceiveProductPriceAvgHamburger", value10.ToString("0.00" + "₺"));

			var value11 = _orderService.TTotalOrderCount();
			await Clients.All.SendAsync("ReceiveTotalOrderCount", value11);

			var value12 = _orderService.TActiveOrderCount();
			await Clients.All.SendAsync("ReceiveActiveOrderCount", value12);

			var value13 = _orderService.TLastOrderPrice();
			await Clients.All.SendAsync("ReceiveLastOrderPrice", value13);

			var value14 = _moneyCaseService.TTotalMoneyCaseAmount();
			await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", value14);

			var value15 = _menusTableService.TMenuTableCount();
			await Clients.All.SendAsync("ReceiveMenuTableCount", value15);


		}

		public async Task SendMenuTableStatus()
		{
			var value = _menusTableService.TGetListAll();
			await Clients.All.SendAsync("ReceiveMenuTableStatus", value);
		}

		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage",user,message);
		}

		//bu metot client'a bağlı olan client sayısını getiriyor
        public override async Task OnConnectedAsync()
        {
			clientCount++;
			await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
			clientCount--;
			await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
