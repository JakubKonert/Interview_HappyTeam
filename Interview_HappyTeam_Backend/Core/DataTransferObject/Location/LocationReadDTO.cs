using Interview_HappyTeam_Backend.Core.Entities;

namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Location
{
    public class LocationReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public bool isAvailable { get; set; }
    }
}
