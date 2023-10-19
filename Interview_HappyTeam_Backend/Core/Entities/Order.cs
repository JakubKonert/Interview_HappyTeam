using Interview_HappyTeam_Backend.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public string ClientName { get; set; } = "Interviewer"; //MOCK
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
