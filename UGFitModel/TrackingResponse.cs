// -----------------------------------------------------------------------
// <copyright file="TrackingResponse.cs" company="">
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
    public class TrackingResponse
    {
        public TrackingResponse()
        {
            this.DateReceived = DateTime.Now;
            this.TrackingResponseId = Guid.NewGuid();
        }

        [Key]
        public Guid TrackingResponseId {get;set;}

        [StringLength(500)]
        public string ResponseText {get;set;}

       
        public Guid TrackingRequestId { get; set; }

        public DateTime DateReceived { get; set; }

        public virtual TrackingRequest TrackingRequest { get; set; }
    }
}
