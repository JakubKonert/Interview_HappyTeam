using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class Country
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
