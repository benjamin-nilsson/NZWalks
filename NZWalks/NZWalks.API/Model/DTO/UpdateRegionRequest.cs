namespace NZWalks.API.Model.DTO
{
    public class UpdateRegionRequest
    {
        // Can remove some properties you don´t want the client to be able to update
        public string Code { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
