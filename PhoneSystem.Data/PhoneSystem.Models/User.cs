namespace PhoneSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PhoneSystem.Contracts;


    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<PhoneNumberOrder> userPhoneNumberOrders;
        private ICollection<PhoneNumberOrder> adminPhoneNumberOrders;
        private ICollection<Phone> phones;

        public User()
        {
            this.UserPhoneNumberOrders = new HashSet<PhoneNumberOrder>();
            this.AdminPhoneNumberOrders = new HashSet<PhoneNumberOrder>();
            this.Phones = new HashSet<Phone>();
            this.PreserveCreatedOn = false;
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }

        public bool IsActive { get; set; }

        [Required]
        public string FullName { get; set; }

        public int EmployeeNumber { get; set; }

        public int? JobTitleId { get; set; }

        public virtual JobTitle JobTitle { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<PhoneNumberOrder> UserPhoneNumberOrders
        {
            get { return this.userPhoneNumberOrders; }
            set { this.userPhoneNumberOrders = value; }
        }

        public virtual ICollection<PhoneNumberOrder> AdminPhoneNumberOrders
        {
            get { return this.adminPhoneNumberOrders; }
            set { this.adminPhoneNumberOrders = value; }
        }

        public virtual ICollection<Phone> Phones
        {
            get { return this.phones; }
            set { this.phones = value; }
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
