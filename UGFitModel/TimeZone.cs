// -----------------------------------------------------------------------
// <copyright file="TimeZone.cs" company="">
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
    public class TimeZone : TranslatableEntity
    {
        public int TimeZoneId { get; set; }
               
        public double CurrentUTCOffset_InHours { get; set; }
        
        public bool IsUnknownTimeZone
        {
            get
            {
                return (this.ReferenceText == "UNKNOWN");
            }
        }

        public virtual ICollection<TimezoneAreacode> AreaCodes { get; set; }

        public DateTime CurrentTime
        {
            get
            {
                //refactor:  Get this from the config file
                //the server is planned to reside in the CENTRAL timezone, which is 6 hours BEHIND the utc time
                int ServerUTCOffSet = 6;
                DateTime utcTime = DateTime.Now.AddHours(ServerUTCOffSet);

                return utcTime.AddMinutes(this.CurrentUTCOffset_InHours);
            }
           
        }
    }
}
