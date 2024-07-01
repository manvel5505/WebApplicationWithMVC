using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using NuGet.Protocol;

namespace WebApplication17.Models.Users
{
    [Table("Users")]
    public class UserRegister
    {
        #region Values
        [Key]
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Username is required!"), NotNull]
        [RegularExpression(pattern: "[A-Za-z]{0,50}", ErrorMessage = "Username contains invalid simbols!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username size 2 - 16 simobls!")]
        [Display(Name = "Username")]
        public string? Username { get; set; }

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
        [Required(ErrorMessage = "Repeat Password is required!"), NotNull]
        [Compare("Password", ErrorMessage = "Password's not equal!")]
        [Display(Name = "RepeatPassword")]
        public string? RepeatPassword { get; set; }
        //public byte[] Photo { get; set; }
        #endregion
    }
}
