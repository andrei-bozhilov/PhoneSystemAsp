namespace PhoneSystem.Web.ViewModels.Admin.Phones
{
    using PhoneSystem.Infrastucture.Mapping;
    using PhoneSystem.Models;
    using PhoneSystem.Web.Controls.Attibutes;

    public class PhoneEditViewModel : IMapFrom<Phone>
    {
        [CanBeModified(false)]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        [CanBeModified(false)]
        public PhoneStatus PhoneStatus { get; set; }

        public bool HasRouming { get; set; }

        public int? CreditLimit { get; set; }

        public CardType CardType { get; set; }
    }
}