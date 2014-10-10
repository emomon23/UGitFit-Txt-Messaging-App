// -----------------------------------------------------------------------
// <copyright file="UserSchedule.cs" company="">
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
    public class UserSchedule
    {
        public UserSchedule() { }
        public UserSchedule(DefaultSchedule sourceSchedule, Person p)
        {
            this.UserScheduleId = Guid.NewGuid();
            this.PersonId = p.PersonId;
            this.SuspendEndDate = DateTime.Parse("1/1/1900");
            this.SuspendStartDate = DateTime.Parse("1/1/2900");

            double offset = p.TimeZone.CurrentUTCOffset_InHours;

            this.ScheduleId = sourceSchedule.DefaultScheduleId;
            DateTime offSetTime = sourceSchedule.DefaultTimeOfDayToSend.AddHours(offset);

            
            this.OverrideUTCTimeOfDayToSend = DateTime.Parse(offSetTime.ToShortTimeString());
                
            this.DaysToSend = new List<UGitFit.Model.DayOfWeek>();
            foreach (DayOfWeek d in sourceSchedule.DaysToSend)
            {
                this.DaysToSend.Add(d);
            }

        }

        public Guid UserScheduleId { get; set; }

    
        public Guid PersonId { get; set; }

        [ForeignKey("SourceSchedule")]
        public Guid ScheduleId { get; set; }
        public DateTime  OverrideUTCTimeOfDayToSend { get; set; }
       

        public int OverridePeriodIntervalInHrs { get; set; }

        public DateTime  SuspendStartDate { get; set; }
        public DateTime SuspendEndDate { get; set; }
               
        public virtual DefaultSchedule SourceSchedule { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<DayOfWeek> DaysToSend { get; set; }

       

    }
}
