using System.Threading.Tasks;
using AutoMapper;
using Jewelry.Web.Dtos;
using Jewelry.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jewelry.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelryController : ControllerBase
    {
        private readonly IJewelryService _jewelryService;
        private readonly IMapper _mapper;
        
        public JewelryController(IJewelryService jewelryService, IMapper mapper)
        {
            _jewelryService = jewelryService;
            _mapper = mapper;
        }
        
        [HttpGet("{jewelryId}")]
        public async Task<JewelryResponseDto> GetTicket(long jewelryId)
        {
            var result = await _jewelryService.GetJewelryAsync(jewelryId);
            return _mapper.Map<JewelryResponseDto>(result);
        }

        [HttpPut("{jewelryId}")]
        public async Task<JewelryResponseDto> UpdateTicket(long jewelryId, JewelryRequestDto jewelryRequestDto)
        {
            var result = await _jewelryService.UpdateJewelryAsync(jewelryId, jewelryRequestDto);
            return _mapper.Map<JewelryResponseDto>(result);
        }

        [HttpPost("")]
        public async Task<JewelryResponseDto> CreateTicket(JewelryRequestDto jewelryRequestDto)
        {
            var result = await _jewelryService.CreateJewelryAsync(jewelryRequestDto);
            return _mapper.Map<JewelryResponseDto>(result);
        }

        [HttpDelete("{jewelryId}")]
        public async Task<ActionResult> DeleteTicket(long jewelryId)
        {
            await _jewelryService.DeleteJewelryAsync(jewelryId);
            return new NoContentResult();
        }
        
        [HttpGet("/api/jewelries")]
        public Jewelries GetClients()
        {
            var result = _jewelryService.GetJewelries();
            return new Jewelries
            {
                Result = _mapper.ProjectTo<JewelryResponseDto>(result)
            };
        }
    }
}