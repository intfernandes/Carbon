using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace Core.Models
{

    public class User : IdentityUser
{
        [Key]
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public override string? Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public override string? PhoneNumber { get; set; } = string.Empty;
}
 
}