using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyCaseController : ControllerBase
    {
        private readonly IMoneyCaseService _moneyCaseService;

        public MoneyCaseController(IMoneyCaseService moneyCaseService)
        {
            _moneyCaseService = moneyCaseService;
        }

        [HttpGet]
        public IActionResult MoneyCasegList()
        {
            return Ok(_moneyCaseService.TTotalMoneyCaseAmount());
        }

        //[HttpPost]
        //public IActionResult CreateMoneyCase(CreateBookingDto createBookingDto)
        //{
        //    Booking booking = new MoneyCase()
        //    {
        //        Name = createBookingDto.Name,
        //        Date = createBookingDto.Date,
        //        Phone = createBookingDto.Phone,
        //        Mail = createBookingDto.Mail,
        //        PersonCount = createBookingDto.PersonCount
        //    };
        //    _moneyCaseService.TAdd(booking);
        //    return Ok("Rezarvasyon yapıldı.");
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteMoneyCase(int id)
        //{
        //    var value = _moneyCaseService.TGetByID(id);
        //    _moneyCaseService.TDelete(value);
        //    return Ok("Rezarvasyon silindi");
        //}

        //[HttpPut]
        //public IActionResult UpdateMoneyCase(UpdateMoneyCaseDto updateMoneyCaseDto)
        //{
        //    MoneyCase moneyCase = new MoneyCase()
        //    {
        //        MoneyCaseID = updateMoneyCaseDto.MoneyCaseID,
        //        TotalAmount = updateMoneyCaseDto.TotalAmount
        //    };
        //    _moneyCaseService.TUpdate(moneyCase);
        //    return Ok("Rezarvasyon güncellendi");
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetMoneyCase(int id)
        //{
        //    var value = _moneyCaseService.TGetByID(id);
        //    return Ok(value);
        //}
    }
}
