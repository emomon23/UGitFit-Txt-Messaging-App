// -----------------------------------------------------------------------
// <copyright file="ClientStatus.cs" company="">
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
    [Table("ClientStatus")]
    public class ClientStatus : TranslatableEntity
    {
        [StringLength(50)] 
        public string ClientStatusId { get; set; }

       
       // public string ReferenceText { get; set; }

        public static string PendingPasswordResetId = "PENDING_PASSWORD_RESET";
        public static string RegisteredPreConfirmedId="REGISTERED_PRE_CONFIRMED";
        public static string SuspendedId = "SUSPENDED";
         
        public static string PreRegisteredId
        {
            get
            {
                return "PREREGISTERED";
            }
        }

        public static string RegisteredAndConfirmedId
        {
            get
            {
                return "REGISTERED_CONFIRMED";
            }
        }

        public static string PreRegistrationTimedOutId {
            get {
                return "TRIAL_PERIOD_EXPIRED";
            }
        }

        public static string SpammerSuspectId
        {
            get
            {
                return "SPAMMER_SUSPECT";
            }
        }
    }
}
