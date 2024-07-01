using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication17.Models.Users
{
    public class UserLogin
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Enter a valid email address")]
        [Required(ErrorMessage = "Email is required!"), NotNull]
        [MaxLength(50, ErrorMessage = "Email size 50 simobls!")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required!"), NotNull]
        [RegularExpression(pattern: "[A-Za-z0-9]{0,50}", ErrorMessage = "Password contains invalid simbols!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password size 6 - 20 simobls!")]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
}
