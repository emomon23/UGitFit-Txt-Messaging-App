// -----------------------------------------------------------------------
// <copyright file="TimezoneAreacode.cs" company="">
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TimezoneAreacode
    {
        public TimezoneAreacode()
        {
            this.DateCreated = DateTime.Now;
            this.TimezoneAreaCodeId = Guid.NewGuid();
        }

        [Key]
        public Guid TimezoneAreaCodeId { get; set; }

        public int ? AreaCode { get; set; }
        public string StateOrProvice { get; set; }
        public string City { get; set; }
        public DateTime DateCreated { get; set; }

      
        public int TimeZoneId { get; set; }

        public virtual TimeZone TimeZone { get; set; }

    }
}
