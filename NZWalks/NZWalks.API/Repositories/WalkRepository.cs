using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private NZWalksDbContext _nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();

            await _nZWalksDbContext.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            Walk walk = await GetWalkByIdAsync(id);

            if (walk == null)
            {
                return null;
            }

            _nZWalksDbContext.Remove(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await _nZWalksDbContext.Walks.ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
           return await _nZWalksDbContext.Walks.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            Walk existingWalk = await GetWalkByIdAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionID = walk.RegionID;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;

            await _nZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
