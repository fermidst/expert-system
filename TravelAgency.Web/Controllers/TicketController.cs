﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Web.Dtos;
using TravelAgency.Web.Services;

namespace TravelAgency.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }
        
        [HttpGet("{ticketId}")]
        public async Task<Ticket> GetTicket(long ticketId)
        {
            var result = await _ticketService.GetTicket(ticketId);
            return _mapper.Map<Ticket>(result);
        }

        [HttpPut("{ticketId}")]
        public async Task<Ticket> UpdateTicket(long ticketId, SaveTicket ticket)
        {
            var result = await _ticketService.UpdateTicket(ticketId, ticket);
            return _mapper.Map<Ticket>(result);
        }

        [HttpPost("")]
        public async Task<Ticket> CreateTicket(SaveTicket ticket)
        {
            var result = await _ticketService.CreateTicket(ticket);
            return _mapper.Map<Ticket>(result);
        }

        [HttpDelete("{ticketId}")]
        public async Task<ActionResult> DeleteTicket(long ticketId)
        {
            await _ticketService.DeleteTicket(ticketId);
            return new NoContentResult();
        }
    }
}