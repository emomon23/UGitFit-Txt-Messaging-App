// -----------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Enumerations
    {
       
         public enum TextMessageTypeEnumeration
        {
            ConvertSpamSuspectToPreRegister,
            TrackMe,
            SuspendMe,
            ReactivateMe,
            TrackingResponse,
            SpamSuspenct,
            PrematureResponse,
            DeleteAcctEntirely,
            TimezoneTest,
            Unblacklist,
        }

         public enum ClientStatusEnumeration
         {
             PENDING_PASSWORD_RESET,
             PREREGISTERED,
             REGISTERED_CONFIRMED,
             REGISTERED_PRE_CONFIRMED,
             SPAMMER_SUSPECT,
             SUSPENDED,
             TRIAL_PERIOD_EXPIRED,
         }

         public static string GetClientStatusId(ClientStatusEnumeration enumVal)
         {
             string rtnVal;

             switch (enumVal)
             {
                 case ClientStatusEnumeration.PENDING_PASSWORD_RESET:
                     rtnVal = "PENDING_PASSWORD_RESET";
                     break;
                 case ClientStatusEnumeration.PREREGISTERED:
                     rtnVal = "PREREGISTERED";
                     break;
                 case ClientStatusEnumeration.REGISTERED_CONFIRMED:
                     rtnVal = "REGISTERED_CONFIRMED";
                     break;
                 case ClientStatusEnumeration.REGISTERED_PRE_CONFIRMED:
                     rtnVal = "REGISTERED_PRE_CONFIRMED";
                     break;
                 case ClientStatusEnumeration.SPAMMER_SUSPECT:
                     rtnVal = "SPAMMER_SUSPECT";
                     break;
                 case ClientStatusEnumeration.SUSPENDED:
                     rtnVal = "SUSPENDED";
                     break;
                 default:
                     rtnVal = "TRIAL_PERIOD_EXPIRED";
                     break;
             }

             return rtnVal;
         }

        public enum SuspendedReasonEnumeration
        {
            UserSuspendedThemselves,
            AdminSuspendedUserOnlyAdminCanReactive,
            UserInactivityUserCanReactive
        }

        public static string GetSuspendedReasonId(SuspendedReasonEnumeration enm)
        {
            if (enm == SuspendedReasonEnumeration.AdminSuspendedUserOnlyAdminCanReactive)
                return "ADMIN";
            else if (enm == SuspendedReasonEnumeration.UserInactivityUserCanReactive)
                return "UNRESPONSIVE";

            return "USER";

        }
    }
}
