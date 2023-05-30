using System.ComponentModel.DataAnnotations;

namespace ViewInput.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string? Address { get; set; }
    }
}
