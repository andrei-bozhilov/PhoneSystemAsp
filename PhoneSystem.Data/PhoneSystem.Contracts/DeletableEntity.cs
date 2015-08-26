namespace PhoneSystem.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        [Display(Name = "Deleted?")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deletion date")]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
