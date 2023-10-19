namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Order
{
    public class OrderCreateDTO
    {
        public string Car { get; set; }
        public string LocationStart { get; set; }
        public string LocationEnd { get; set; }     
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Country { get; set; }
    }
}
