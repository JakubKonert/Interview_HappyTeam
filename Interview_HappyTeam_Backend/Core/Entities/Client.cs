using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string Nick {  get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
