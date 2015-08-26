using PhoneSystem.Models;

namespace PhoneSystem.Web.BindModels.Admin
{
    class PhoneBindModel
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public PhoneStatus PhoneStatus { get; set; }

        public bool HasRouming { get; set; }

        public int? CreditLimit { get; set; }

        public CardType CardType { get; set; }
    }
}
