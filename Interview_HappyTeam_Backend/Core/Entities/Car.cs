using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public CarModel CarModel { get; set; }
        public Guid CarModelId { get; set; }
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
