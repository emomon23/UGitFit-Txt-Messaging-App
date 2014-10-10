// -----------------------------------------------------------------------
// <copyright file="ScheduledText.cs" company="">
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
    public class ScheduledText
    {
      
        public Guid TrackingReqeustId { get; set; }
        public DateTime CurrentLocalTime { get; set; }
        public DateTime TimeToSendText { get; set; }
        public DateTime DateThisItemLastSent { get; set; }
        public int HrsLastSent { get; set; }
        public string TimeZoneText { get; set; }
        public DateTime Date_ANYScheduleItem_LastSent { get; set; }
        public string Description { get; set; }
        public Guid MealTypeId { get; set; }
        public string TextMsgReferenceText { get; set; }
        public string TranslationXML { get; set; }
        public Guid PersonId { get; set; }
        public Guid SchduledId { get; set; }
        public Guid UserScheduleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ClientStatusId { get; set; }
        public bool ReplyToSMS { get; set; }
        public int LanguageId { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDebugNumber { get; set; }
        public string TextToSend { get; set; }

        public string DisplayFirstName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName))
                    return "";

                return this.FirstName;
            }
        }

        public string DisplayWholeName(string resultsIfBothAreNull)
        {
            string result = this.FirstName;

            if (!this.LastName.isNullOrEmpty())
            {
                if (result.Length > 0)
                    result += " ";


                result += this.LastName;
            }

            if (result.isNullOrEmpty())
                result = resultsIfBothAreNull;

            return result;
        }

    }
}
