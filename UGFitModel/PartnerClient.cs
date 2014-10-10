// -----------------------------------------------------------------------
// <copyright file="GroupMember.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PartnerClient
    {
        [Key]
        public int PartnerClientId { get; set; }

        [StringLength(50)] 
        public string MemberIdentifier { get; set; }

      
        public Guid PersonId { get; set; }

       
        public Guid BusinessPartnerId { get; set; }

        public DateTime? DateDataLastSentToBusinessPartner { get; set; }

        public virtual BusinessPartner BusinessPartner { get; set; }
        public virtual Person Person { get; set; }
    }
}
