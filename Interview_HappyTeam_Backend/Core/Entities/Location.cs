using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public bool isAvailable { get; set; }
    }
}
