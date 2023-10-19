using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class Config
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public string name { get; set; }
        public string data { get; set; }
        public DateTime createdDate { get; set; }
        public bool isActive { get; set; } = true;
    }
}
