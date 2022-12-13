using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {

        private readonly NZWalksDbContext _nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty newWalkDifficulty)
        {
            newWalkDifficulty.Id = Guid.NewGuid();

            await _nZWalksDbContext.AddAsync(newWalkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();

            return newWalkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            WalkDifficulty walkDifficulty = await GetWalkDifficultyByIdAsync(id);

            if (walkDifficulty == null)
            {
                return null;
            }

            _nZWalksDbContext.Remove(walkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();

            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetWalkDifficultiesAsync()
        {
            return await _nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id)
        {
            return await _nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            WalkDifficulty existingWalkDifficulty = await GetWalkDifficultyByIdAsync(id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;

            await _nZWalksDbContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }
    }
}
