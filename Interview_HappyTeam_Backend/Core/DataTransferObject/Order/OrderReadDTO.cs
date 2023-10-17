using Interview_HappyTeam_Backend.Core.Enums;

namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Order
{
    public class OrderReadDTO
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string LocationStartName { get; set; }
        public Guid LocationIdStart { get; set; }
        public string LocationEndName { get; set; }
        public Guid LocationIdEnd { get; set; }
        public Guid CarId { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
