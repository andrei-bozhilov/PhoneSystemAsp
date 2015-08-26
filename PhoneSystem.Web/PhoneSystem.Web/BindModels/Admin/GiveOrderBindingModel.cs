namespace PhoneSystem.Web.BindModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class GiveOrderBindingModel
    {
        public int PhoneId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        public int? JobTitleId { get; set; }

        public int? DepartmentId { get; set; }
    }
}