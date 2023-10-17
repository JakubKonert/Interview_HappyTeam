using Interview_HappyTeam_Backend.Core.Entities;

namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Car
{
    public class CarReadDTO
    {
        public Guid Id { get; set; }
        public Guid CarModelId { get; set; }
        public Guid OrderId { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
    }
}
