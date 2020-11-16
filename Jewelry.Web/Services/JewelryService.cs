using System;
using System.Linq;
using System.Threading.Tasks;
using Jewelry.Infrastructure;
using Jewelry.Infrastructure.Models;
using Jewelry.Web.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Jewelry.Web.Services
{
    public class JewelryService : IJewelryService
    {
        private readonly IApplicationDbContext _dbContext;

        public JewelryService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Infrastructure.Models.Jewelry> GetJewelryAsync(long jewelryId)
        {
            return await _dbContext.Jewelries.SingleOrDefaultAsync(jewelry => jewelry.Id == jewelryId);
        }

        public async Task<Infrastructure.Models.Jewelry> UpdateJewelryAsync(long jewelryId, JewelryRequestDto jewelryRequestDto)
        {
            var entity = await _dbContext.Jewelries.SingleOrDefaultAsync(j => j.Id == jewelryId);
            entity.Name = jewelryRequestDto.Name;
            entity.Price = jewelryRequestDto.Price;
            entity.ReceivedDate = jewelryRequestDto.ReceivedDate;
            entity.PhotoUrl = jewelryRequestDto.PhotoUrl;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Infrastructure.Models.Jewelry> CreateJewelryAsync(JewelryRequestDto jewelryRequestDto)
        {
            var entry = await _dbContext.Jewelries.AddAsync(new Infrastructure.Models.Jewelry
            {
                Name = jewelryRequestDto.Name,
                Price = jewelryRequestDto.Price,
                ReceivedDate = jewelryRequestDto.ReceivedDate,
                PhotoUrl = jewelryRequestDto.PhotoUrl
            });
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteJewelryAsync(long jewelryId)
        {
            var entity = await _dbContext.Jewelries.SingleOrDefaultAsync(j => j.Id == jewelryId);
            _dbContext.Jewelries.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public IQueryable<Infrastructure.Models.Jewelry> GetJewelries()
        {
            return _dbContext.Jewelries;
        }
    }
}