using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Web.Dtos;
using TravelAgency.Web.Services;

namespace TravelAgency.Web.Controllers
{
    [Route("[controller]")]
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
        public async Task<Client> GetClient(long clientId)
        {
            var result = await _clientService.GetClientAsync(clientId);
            return _mapper.Map<Client>(result);
            
        }

        [HttpPut("{clientId}")]
        public async Task<Client> UpdateClient(long clientId, SaveClient client)
        {
            var result = await _clientService.UpdateClientAsync(clientId, client);
            return _mapper.Map<Client>(result);
        }

        [HttpPost("")]
        public async Task<Client> CreateClient(SaveClient client)
        {
            var result = await _clientService.CreateClientAsync(client);
            return _mapper.Map<Client>(result);
        }

        [HttpDelete("{clientId}")]
        public async Task<ActionResult> DeleteClient(long clientId)
        {
            await _clientService.DeleteClientAsync(clientId);
            return new NoContentResult();
        }

        [HttpGet("/clients")]
        public Clients GetClients()
        {
            var result = _clientService.GetClientsAsync();
            return new Clients
            {
                Result = _mapper.ProjectTo<Client>(result)
            };
        }
    }
}