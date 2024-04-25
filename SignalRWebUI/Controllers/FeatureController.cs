using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.FeatureDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class FeatureController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public FeatureController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7210/api/Feature/");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				//jsonData -> kategorilerin json şeklindeki hali
				var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
				//json şeklindeki kategorileri list olarak çekiyoruz
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateFeature()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
		{
			//başlangıçta true olsun
			var client = _httpClientFactory.CreateClient();
			//istemci oluşturuyorum
			var jsonData = JsonConvert.SerializeObject(createFeatureDto);
			//parametreden gelen değeri selilize ediyorum
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7210/api/Feature/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> DeleteFeature(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7210/api/Feature/{id}");
			//$ sembolü ekliyorsam, parametre geçeceğim demek, id'den gelen değişkeni yolluyoruz
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> UpdateFeature(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7210/api/Feature/{id}");
			//GETASYNC İLE İLK ÖNCE GÜNCELLEYECEĞİM VERİYİ GETİRECEĞİM
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);
				return View(values);
			}
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync($"https://localhost:7210/api/Feature/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}


			return View();
		}

	}
}
