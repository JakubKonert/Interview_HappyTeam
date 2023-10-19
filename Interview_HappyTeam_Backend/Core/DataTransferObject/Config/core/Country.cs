namespace Interview_HappyTeam_Backend.Core.DataTransferObject.Config.core
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainLocation { get; set; }
        public List<Location> Locations { get; set; }
        public bool IsAvailable { get; set; }
    }
}
