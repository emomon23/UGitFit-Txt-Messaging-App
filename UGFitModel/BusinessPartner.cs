// -----------------------------------------------------------------------
// <copyright file="BusinessPartner.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Table("businesspartners")]
    public class BusinessPartner
    {       
        public BusinessPartner()
        {
            this.DateCreated = DateTime.Now;
            this.BusinessPartnerId = Guid.NewGuid();
        }

        [Key]
        public Guid BusinessPartnerId { get; set; }

        [StringLength(100)] 
        public string BusinessPartnerName { get; set; }

        public DateTime DateCreated { get; set; }
    
        [StringLength(50)] 
        public string GroupTag { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(100)]
        public string ContactEmail { get; set; }

        [StringLength(200)]
        public string ForwardDataEmailAddress { get; set; }

        [StringLength(200)]
        public string ForwardDataWebServiceURL { get; set; }

        public DateTime? DateDataLastForwardedToBusinessPartner { get; set; }

        public virtual ICollection<PartnerClient> PartnerClients { get; set; }
    }
}
