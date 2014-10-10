// -----------------------------------------------------------------------
// <copyright file="Person.cs" company="">
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
    [Table("People")]
    public class Person
    {
        public Person()
        {
            this.PersonId = Guid.NewGuid();
            this.PhoneNumbers = new List<PhoneNumber>();
            this.UserSchedules = new List<UserSchedule>();
            this.CreatedDate = DateTime.Now;
            this.ReplyToSMS = true;
            this.TodaysSMSReceivedCount = 0;
            this.TodaysSMSSentCount = 0;
            this.WeightHistory = new List<WeightHistoryItem>();
            this.NutritionHistory = new List<NutrtionHistoryItem>();
            this.ExerciseHistory = new List<ExerciseRecord>();
            this.Memberships = new List<PartnerClient>();
            this.Mentors = new List<ClientMentor>();
            this.TrackingRequests = new List<TrackingRequest>();
     
            
        }

        public Guid PersonId { get; set; }

        [StringLength(50)] 
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string EmailAddress { get; set; }

        [StringLength(50)]
        public string ResumedDate { get;  set; }

        [StringLength(20)]
        public string RegistrationCode { get; set; }

        public bool ForwardDataToUsersEmail { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
      
      
        public int LanguageId { get; set; }


        public int TimeZoneId { get; set; }

      
        public string ClientStatusId { get; set; }

        [StringLength(20)]
        public string PriorStatusId { get; set; }

        [ForeignKey("SuspendedReason")]
        public string ReasonForSuspensionId { get; set; }

        public int ConsectiveSpamCount { get; set; }

        public bool ReplyToSMS { get; set; }

        public DateTime ? DateSMSLastSentToNumber { get; set; }
        public DateTime ? DateSMSLastRecieveFromNumber { get; set; }
        public int TodaysSMSReceivedCount { get; set; }
        public int TodaysSMSSentCount { get; set; }

        public virtual Language Language { get; set; }
        public virtual TimeZone TimeZone { get; set; }
        public virtual ClientStatus Status { get; set; }
       

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<WeightHistoryItem> WeightHistory { get; set; }
        public virtual ICollection<NutrtionHistoryItem> NutritionHistory { get; set; }
        public virtual ICollection<ExerciseRecord> ExerciseHistory { get; set; }
        public virtual ICollection<ClientMentor> Mentors { get; set; }
        public virtual List<PartnerClient> Memberships { get; set; }
        public virtual ICollection<UserSchedule> UserSchedules { get; set; }
        public virtual SuspendedReason SuspendedReason { get; set; }
        public virtual ICollection<TrackingRequest> TrackingRequests { get; set; }
        
        public bool SuspendUser(bool suspend, Enumerations.SuspendedReasonEnumeration ? reason, bool callerIsAdmin)
        {
            bool result=true;

            if (suspend)
            {
                this.ClientStatusId = Enumerations.GetClientStatusId(Enumerations.ClientStatusEnumeration.SUSPENDED);

                if (reason.HasValue)
                {
                    this.ReasonForSuspensionId = Enumerations.GetSuspendedReasonId(reason.Value);
                }
            }
            else
            {
                //Remove suspension?  Check if the user can remove their own suspension
                if (this.IsSuspended() && this.ReasonForSuspensionId == Enumerations.GetSuspendedReasonId(Enumerations.SuspendedReasonEnumeration.AdminSuspendedUserOnlyAdminCanReactive) && callerIsAdmin == false)
                    //The user is trying to resume themselves, but they have SPECIFICALLY been suspended by an administrator
                    //Only an admin can resume their usage.
                    result = false;
                else
                {
                    if (! this.PriorStatusId.isNullOrEmpty())
                        this.ClientStatusId = this.PriorStatusId;
                    else
                        this.ClientStatusId = Enumerations.GetClientStatusId(Enumerations.ClientStatusEnumeration.PREREGISTERED);

                    this.ReasonForSuspensionId = null;
                }

            }

            return result;

        }

        public bool TrackMeTextMessageIsValid
        {
            get
            {
                bool result = false;
               

                switch (this.Status.ClientStatusId)
                {
                    case "SUSPENDED":
                        result = true;
                        break;
                    default:
                        result = false;
                        break;

                }

           //     if (result == true && ("ADMIN".Contains(this.SuspendedReason.SuspendedReasonId))) ;
                    result = false;

                return result;
            }
        }

        public bool IsSuspended()
        {
            return this.ClientStatusId == Enumerations.GetClientStatusId(Enumerations.ClientStatusEnumeration.SUSPENDED);
        }

        public bool TrialPeriodHasExpired
        {
            get
            {
               /* TimeSpan ts = DateTime.Now - this.CreatedDate;
                if (this.IsPreregisteredUser && ts.TotalDays > 30)
                    return true;*/

                return false;
            }
        }

        public bool IsPreregisteredUser
        {
            get
            {
                if (this.Status.ClientStatusId == ClientStatus.PreRegisteredId || (this.FirstName.isNullOrEmpty() && this.LastName.isNullOrEmpty() && this.EmailAddress.isNullOrEmpty()))
                    return true;

                return false;
            }
        }
    }
}
