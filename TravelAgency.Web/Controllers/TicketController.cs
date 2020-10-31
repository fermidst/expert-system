using System.Threading.Tasks;
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
        public async Task<TicketResponseDto> GetTicket(long ticketId)
        {
            var result = await _ticketService.GetTicketAsync(ticketId);
            return _mapper.Map<TicketResponseDto>(result);
        }

        [HttpPut("{ticketId}")]
        public async Task<TicketResponseDto> UpdateTicket(long ticketId, TicketRequestDto ticketRequestDto)
        {
            var result = await _ticketService.UpdateTicketAsync(ticketId, ticketRequestDto);
            return _mapper.Map<TicketResponseDto>(result);
        }

        [HttpPost("")]
        public async Task<TicketResponseDto> CreateTicket(TicketRequestDto ticketRequestDto)
        {
            var result = await _ticketService.CreateTicketAsync(ticketRequestDto);
            return _mapper.Map<TicketResponseDto>(result);
        }

        [HttpDelete("{ticketId}")]
        public async Task<ActionResult> DeleteTicket(long ticketId)
        {
            await _ticketService.DeleteTicketAsync(ticketId);
            return new NoContentResult();
        }
        
        [HttpGet("/tickets")]
        public Tickets GetClients()
        {
            var result = _ticketService.GetTickets();
            return new Tickets
            {
                Result = _mapper.ProjectTo<TicketResponseDto>(result)
            };
        }
    }
}