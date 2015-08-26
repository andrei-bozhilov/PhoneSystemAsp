namespace PhoneSystem.Web.BindModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class GiveOrderOldUserBindingModel
    {
        //[Required]
        public string OldUser { get; set; }

        public string PhoneId { get; set; }
    }
}