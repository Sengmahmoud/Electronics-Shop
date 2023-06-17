using Core.Common.Domain;

namespace Core.Dtos
{
    public record UserInputDto:DtoBase<Guid>
    {
        [Required(ErrorMessage = "Field Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [Display(Name = "Mobile")]
        [RegularExpression("01([0-9]{8})", ErrorMessage = @"MobileFromat")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [Display(Name = "PasswordConfirm")]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "Field Required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string? UserName { get; set; }
        public ICollection<UserRoleDto>? UserRoles { get; set; }
        // public string UserName { get; set; }
    }
}
