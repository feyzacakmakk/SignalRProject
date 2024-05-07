using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;
using System.Net.Http;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var client=_httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7210/api/Product/ProductListWithCategory/");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CreateProduct()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage= await client.GetAsync("https://localhost:7210/api/Category/");
			//Kategorileri istediğim için oraya istekte bulunuyorum
			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
			List<SelectListItem> categories = (from x in values
											   select new SelectListItem
											   {
												   Text = x.CategoryName,
												   Value = x.CategoryID.ToString()
											   }).ToList();
			ViewBag.Categories = categories;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			createProductDto.ProductStatus = true;
			//başlangıçta true olsun
			var client = _httpClientFactory.CreateClient();
			//istemci oluşturuyorum
			var jsonData = JsonConvert.SerializeObject(createProductDto);
			//parametreden gelen değeri serilize ediyorum
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7210/api/Product/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> DeleteProduct(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7210/api/Product/{id}");
			//$ sembolü ekliyorsam, parametre geçeceğim demek, id'den gelen değişkeni yolluyoruz
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> UpdateProduct(int id)
		{

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7210/api/Product/{id}");
			//GETASYNC İLE İLK ÖNCE GÜNCELLEYECEĞİM VERİYİ GETİRECEĞİM

			//kategoriler
			var client2 = _httpClientFactory.CreateClient();
			var responseMessage2 = await client.GetAsync("https://localhost:7210/api/Category/");
			//Kategorileri istediğim için oraya istekte bulunuyorum
			var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
			var values2 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData2);
			List<SelectListItem> categories = (from x in values2
											   select new SelectListItem
											   {
												   Text = x.CategoryName,
												   Value = x.CategoryID.ToString()
											   }).ToList();
			ViewBag.Categories2 = categories;
			//

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
				return View(values);
			}
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync($"https://localhost:7210/api/Product/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}


			return View();
		}
	}
}
