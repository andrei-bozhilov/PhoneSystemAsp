namespace PhoneSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class InvoiceData
    {
        private ICollection<Service> services;

        public InvoiceData()
        {
            this.services = new HashSet<Service>();
        }
        public int Id { get; set; }
        //PRODUCTUSAGE
        public TimeSpan TotalMinUsed { get; set; }
        //PRODUCTAMOUNT
        public decimal TotalAmount { get; set; }
        //PRODUCTID

        public int PhoneId { get; set; }

        public virtual Phone Phone { get; set; }

        public int InvoiceInfoId { get; set; }

        public virtual InvoiceInfo InvoiceInfo { get; set; }

        public virtual ICollection<Service> Services
        {
            get { return this.services; }
            set { this.services = value; }
        }
    }
}
