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
        public IEnumerable<Region> GetAll()
        {
            return _nzWalksDbContext.Regions.ToList();
        }
    }
}
