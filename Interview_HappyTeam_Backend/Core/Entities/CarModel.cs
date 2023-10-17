using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class CarModel
    {
        [Key]
        public Guid CarModelId { get; set; } = new Guid();
        public string Brand { get; set;}
        public string Model { get; set;}
        public double Price { get; set;}
        public bool IsAvailable { get; set;}
        public int Amount { get; set;}
        public ICollection<Car> Cars { get; set;}
    }
}
