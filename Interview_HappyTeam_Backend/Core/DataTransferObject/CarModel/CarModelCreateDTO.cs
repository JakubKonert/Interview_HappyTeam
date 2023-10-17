using Interview_HappyTeam_Backend.Core.Entities;

namespace Interview_HappyTeam_Backend.Core.DataTransferObject.CarModel
{
    public class CarModelCreateDTO
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public int Amount { get; set; }
    }
}
