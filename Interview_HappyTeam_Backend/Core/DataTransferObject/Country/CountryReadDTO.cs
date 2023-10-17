using LocationModel = Interview_HappyTeam_Backend.Core.Entities.Location; 

namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Country
{
    public class CountryReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<LocationModel> Locations { get; set; }
    }
}