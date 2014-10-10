// -----------------------------------------------------------------------
// <copyright file="TrackingRequest.cs" company="">
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
    public class TrackingRequest
    {
        public TrackingRequest()
        {
            this.TrackingRequestId = Guid.NewGuid();
            this.DateSent = DateTime.Now;
        }

        [Key]
        public Guid TrackingRequestId { get; set; }
               
        public Guid ? UserScheduleId { get; set; }

        public Guid PersonId { get; set; }

        public DateTime DateSent { get; set; }

       
        [StringLength(500)]
        public string TextSent { get; set; }

      
        public virtual Person Person { get; set; }
        public virtual ICollection<TrackingResponse> Responses { get; set; }
    }
}
