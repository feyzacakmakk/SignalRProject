﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.ContactDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _mapper.Map<List<ResultContactDto>>(_contactService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            _contactService.TAdd(new Contact()
            {
                Mail=createContactDto.Mail,
                Location=createContactDto.Location,
                FooterDescription=createContactDto.FooterDescription,
                PhoneNumber = createContactDto.PhoneNumber
            });
            return Ok("İletişim bilgisi eklendi");

        }

        [HttpDelete]
        public IActionResult DeleteContact(int id)
        {
            var value=_contactService.TGetByID(id);
            _contactService.TDelete(value);
            return Ok("İletişim bilgisi silindi");
        }

        [HttpGet("GetContact")]
        public IActionResult GetContact(int id)
        {
            var value=_contactService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            _contactService.TUpdate(new Contact(){ 
                ContactID=updateContactDto.ContactID,
                FooterDescription = updateContactDto.FooterDescription,
                PhoneNumber = updateContactDto.PhoneNumber,
                Location=updateContactDto.Location,
                Mail=updateContactDto.Mail
            });
            return Ok("İletişim bilgisi güncellendi");
        }
    }
}