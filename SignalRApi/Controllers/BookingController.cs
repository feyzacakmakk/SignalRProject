using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.AboutDto;
using SignalR.DtoLayer.BookingDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _bookingService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
        {
            Booking booking = new Booking()
            {
                Name = createBookingDto.Name,
                Date = createBookingDto.Date,
                Phone = createBookingDto.Phone,
                Mail= createBookingDto.Mail,
                PersonCount= createBookingDto.PersonCount,
                Description= createBookingDto.Description
            };
            _bookingService.TAdd(booking);
            return Ok("Rezarvasyon yapıldı.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            _bookingService.TDelete(value);
            return Ok("Rezarvasyon silindi");
        }

        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            Booking booking = new Booking()
            {
                BookingID= updateBookingDto.BookingID,
                Name = updateBookingDto.Name,
                Date = updateBookingDto.Date,
                Phone = updateBookingDto.Phone,
                Mail = updateBookingDto.Mail,
                PersonCount = updateBookingDto.PersonCount
            };
            _bookingService.TUpdate(booking);
            return Ok("Rezarvasyon güncellendi");
        }

        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var value = _bookingService.TGetByID(id);
            return Ok(value);
        }

        [HttpGet("BookingStatusApproved/{id}")]
        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.TBookingStatusApproved(id);
            return Ok("Rezervasyon açıklaması değiştirildi.");
        }

		[HttpGet("BookingStatusCancaled/{id}")]
		public IActionResult BookingStatusCancaled(int id)
		{
			_bookingService.TBookingStatusCancaled(id);
			return Ok("Rezervasyon açıklaması değiştirildi.");
		}
	}
}
