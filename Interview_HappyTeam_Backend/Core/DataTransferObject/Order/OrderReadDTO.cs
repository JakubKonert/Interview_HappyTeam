using Interview_HappyTeam_Backend.Core.Enums;

namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Order
{
    public class OrderReadDTO
    {
        public string ClientName { get; set; }
        public string LocationStart { get; set; }
        public string LocationEnd { get; set; }
        public string Car { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public string Country { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
