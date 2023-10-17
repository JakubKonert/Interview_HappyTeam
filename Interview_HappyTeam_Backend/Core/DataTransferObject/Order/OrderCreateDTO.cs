namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Order
{
    public class OrderCreateDTO
    {
        public Guid ClientId { get; set; }
        public Guid LocationIdStart { get; set; }
        public Guid LocationIdEnd { get; set; }
        public Guid CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
