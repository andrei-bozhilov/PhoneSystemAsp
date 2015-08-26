namespace PhoneSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PhoneSystem.Contracts;

    public class Phone : IAuditInfo, IDeletableEntity
    {
        private ICollection<PhoneNumberOrder> phoneNumberOrders;
        private ICollection<InvoiceData> invoiceData;

        public Phone()
        {
            this.phoneNumberOrders = new HashSet<PhoneNumberOrder>();
            this.invoiceData = new HashSet<InvoiceData>();
            this.PreserveCreatedOn = false;
        }

        public int Id { get; set; }

        [Required]
        [Index]
        [MinLength(10)]
        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        public string PhoneNumber { get; set; }

        public PhoneStatus PhoneStatus { get; set; }

        public bool HasRouming { get; set; }

        public int? CreditLimit { get; set; }

        public CardType CardType { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime? AvailableAfter { get; set; }

        public virtual ICollection<PhoneNumberOrder> PhoneNumberOrders
        {
            get { return this.phoneNumberOrders; }
            set { this.phoneNumberOrders = value; }
        }

        public virtual ICollection<InvoiceData> InvoiceData
        {
            get { return this.invoiceData; }
            set { this.invoiceData = value; }
        }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }
    }
}
