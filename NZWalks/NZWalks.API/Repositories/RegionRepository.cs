using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nzWalksDbContext;

        public RegionRepository(NZWalksDbContext nzWalksDbContext)
        {
            _nzWalksDbContext = nzWalksDbContext;
        }
        
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nzWalksDbContext.Regions.ToListAsync();  
        }

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            return await _nzWalksDbContext.Regions.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();

            await _nzWalksDbContext.AddAsync(region);
            await _nzWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region =  await GetRegionByIdAsync(id);

            if (region == null)
                throw null;

            // Delete the region
            _nzWalksDbContext.Regions.Remove(region);

            await _nzWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await GetRegionByIdAsync(id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await _nzWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
