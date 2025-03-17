using System.ComponentModel.DataAnnotations;

namespace UserOfMasters.Data.Models
{
    public class User
    {
        [Required]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? ResetToken { get; set; }

        public DateTime? TokenExpire { get; set; }
    }
}
