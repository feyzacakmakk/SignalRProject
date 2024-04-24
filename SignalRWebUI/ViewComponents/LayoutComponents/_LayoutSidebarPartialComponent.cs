using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;

namespace SignalRWebUI.ViewComponents.LayoutComponents
{
	public class _LayoutSidebarPartialComponent:ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _LayoutSidebarPartialComponent(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var clientCategory = _httpClientFactory.CreateClient();
			var clientProduct = _httpClientFactory.CreateClient();
			var responseCategoryMessage = await clientCategory.GetAsync("https://localhost:7210/api/Category");
			var responseProductMessage = await clientCategory.GetAsync("https://localhost:7210/api/Product");
			if (responseCategoryMessage.IsSuccessStatusCode && responseProductMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseCategoryMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
				TempData["categoryCount"] = values.Count;

				var jsonData2 = await responseProductMessage.Content.ReadAsStringAsync();
				var values2 = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData2);
				TempData["ProductCount"] = values2.Count;
			}
			return View();
		}
	}
}
