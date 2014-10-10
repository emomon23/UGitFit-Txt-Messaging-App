// -----------------------------------------------------------------------
// <copyright file="ExerciseRecord.cs" company="">
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
    public class ExerciseRecord
    {
        public ExerciseRecord()
        {
            this.ExerciseRecordId = Guid.NewGuid();
            this.DateCreated = DateTime.Now;
        }

        [Key]
        public Guid ExerciseRecordId { get; set; }

        public DateTime DateCreated { get; set; }

        [MaxLength(500)]
        public string ExcerciseNote { get; set; }

        [ForeignKey("Person")]
        public Guid PersonId { get; set; }

        public int? CaloriesBurned { get; set; }

        public virtual Person Person { get; set; }
    }
}
