using Interview_HappyTeam_Backend.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public Client Client { get; set; }
        public Guid ClientId { get; set; }
        public Location LocationStart { get; set; }
        public Guid LocationIdStart { get; set; }
        public Location LocationEnd { get; set; }
        public Guid LocationIdEnd { get; set;}
        public Car Car { get; set; }
        public Guid CarId { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
