// -----------------------------------------------------------------------
// <copyright file="SuspendedReason.cs" company="">
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
    public class SuspendedReason : TranslatableEntity
    {
        [StringLength(50)]
        public string SuspendedReasonId { get; set; }

        public virtual ICollection<Person> SuspendedPeople { get; set; }
    }
}
