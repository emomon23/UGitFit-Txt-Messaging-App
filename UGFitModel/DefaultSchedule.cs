// -----------------------------------------------------------------------
// <copyright file="DefaultSchedule.cs" company="">
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
    public class DefaultSchedule :TranslatableEntity
    {
        public DefaultSchedule() { 
            this.DaysToSend = new List<DayOfWeek>(); 
        }

        public Guid DefaultScheduleId { get; set; }
        public DateTime DefaultTimeOfDayToSend { get; set; }
      
        [StringLength(256)] 
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public Guid MealTypeId { get; set; }
        public int PeriodIntervalInHrs { get; set; }
        
        public virtual ICollection<DayOfWeek> DaysToSend { get; set; }
        public virtual ICollection<UserSchedule> UserSchedules { get; set; }

        public void AddDayOfWeek(UGitFit.Model.DayOfWeek d)
        {
            this.DaysToSend.Add(d);
        }

        public void AddDayOfWeek(IQueryable<DayOfWeek> daysToAdd)
        {
            foreach (UGitFit.Model.DayOfWeek d in daysToAdd)
            {
                this.AddDayOfWeek(d);
            }
        }
    }
}
