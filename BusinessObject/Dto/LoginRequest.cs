using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dto
{
    public class LoginRequest
    {
        [Required]
        public string EmailAddress { get; set; } = null!;

        [Required]
        public string AccountPassword { get; set; } = null!;
    }
}
