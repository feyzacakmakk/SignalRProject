using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ContactDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class ContactController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ContactController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7210/api/Contact/");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				//jsonData -> kategorilerin json şeklindeki hali
				var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
				//json şeklindeki kategorileri list olarak çekiyoruz
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateContact()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
		{
		
			//başlangıçta true olsun
			var client = _httpClientFactory.CreateClient();
			//istemci oluşturuyorum
			var jsonData = JsonConvert.SerializeObject(createContactDto);
			//parametreden gelen değeri selilize ediyorum
			StringContent stringContent = new StringContent(jsonData,Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7210/api/Contact/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> DeleteContact(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7210/api/Contact/{id}");
			//$ sembolü ekliyorsam, parametre geçeceğim demek, id'den gelen değişkeni yolluyoruz
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> UpdateContact(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7210/api/Contact/{id}");
			//GETASYNC İLE İLK ÖNCE GÜNCELLEYECEĞİM VERİYİ GETİRECEĞİM
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateContactDto>(jsonData);
				return View(values);
			}
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateContactDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync($"https://localhost:7210/api/Contact/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}


			return View();
		}


	}
}

