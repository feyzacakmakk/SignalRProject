using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.TestimonialDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class TestimonialController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public TestimonialController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7210/api/Testimonial/");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				//jsonData -> kategorilerin json şeklindeki hali
				var values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
				//json şeklindeki kategorileri list olarak çekiyoruz
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateTestimonial()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
		{
			//başlangıçta true olsun
			var client = _httpClientFactory.CreateClient();
			//istemci oluşturuyorum
			var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
			//parametreden gelen değeri selilize ediyorum
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7210/api/Testimonial", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		public async Task<IActionResult> DeleteTestimonial(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7210/api/Testimonial/{id}");
			//$ sembolü ekliyorsam, parametre geçeceğim demek, id'den gelen değişkeni yolluyoruz
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> UpdateTestimonial(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7210/api/Testimonial/{id}");
			//GETASYNC İLE İLK ÖNCE GÜNCELLEYECEĞİM VERİYİ GETİRECEĞİM
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateTestimonialDto>(jsonData);
				return View(values);
			}
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateTestimonialDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync($"https://localhost:7210/api/Testimonial/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}


			return View();
		}

	}
}
