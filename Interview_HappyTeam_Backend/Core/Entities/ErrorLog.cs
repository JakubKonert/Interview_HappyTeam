using System.ComponentModel.DataAnnotations;

namespace Interview_HappyTeam_Backend.Core.Entities
{
    public class ErrorLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ErrorName { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime occuredDate { get; set; } = DateTime.Now;
    }
}
