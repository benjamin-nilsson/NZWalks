using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();

        Task<Walk> GetWalkByIdAsync(Guid id);

        Task<Walk> DeleteWalkAsync(Guid id);

        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);

        Task<Walk> AddWalkAsync(Walk walk);
    }
}
