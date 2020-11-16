using System.Threading.Tasks;
using AutoMapper;
using Jewelry.Web.Dtos;
using Jewelry.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jewelry.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }
        [HttpGet("{clientId}")]
        public async Task<ClientResponseDto> GetClient(long clientId)
        {
            var result = await _clientService.GetClientAsync(clientId);
            return _mapper.Map<ClientResponseDto>(result);
            
        }

        [HttpPut("{clientId}")]
        public async Task<ClientResponseDto> UpdateClient(long clientId, ClientRequestDto client)
        {
            var result = await _clientService.UpdateClientAsync(clientId, client);
            return _mapper.Map<ClientResponseDto>(result);
        }

        [HttpPost("")]
        public async Task<ClientResponseDto> CreateClient(ClientRequestDto client)
        {
            var result = await _clientService.CreateClientAsync(client);
            return _mapper.Map<ClientResponseDto>(result);
        }

        [HttpDelete("{clientId}")]
        public async Task<ActionResult> DeleteClient(long clientId)
        {
            await _clientService.DeleteClientAsync(clientId);
            return new NoContentResult();
        }

        [HttpGet("/api/clients")]
        public Clients GetClients()
        {
            var result = _clientService.GetClientsAsync();
            return new Clients
            {
                Result = _mapper.ProjectTo<ClientResponseDto>(result)
            };
        }
    }
}