namespace NZWalks.API.Model.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionID { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
