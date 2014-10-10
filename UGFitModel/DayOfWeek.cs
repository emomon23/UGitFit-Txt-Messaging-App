// -----------------------------------------------------------------------
// <copyright file="DayOfWeek.cs" company="">
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
   [Table("DaysOfWeek")]
    public class DayOfWeek :TranslatableEntity
    {
        [StringLength(20)] 
        public string DayOfWeekId { get; set; }
        
        public virtual ICollection<DefaultSchedule> DefaultSchedules { get; set; }
        public virtual ICollection<UserSchedule> UseSchedules { get; set; }
    }
}    
