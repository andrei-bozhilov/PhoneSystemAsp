namespace PhoneSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PhoneSystem.Contracts;

    public class JobTitle : IAuditInfo, IDeletableEntity
    {
        private ICollection<User> users;

        public JobTitle()
        {
            this.Users = new HashSet<User>();
            this.PreserveCreatedOn = false;
        }

        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
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
