// -----------------------------------------------------------------------
// <copyright file="WeightHistory.cs" company="">
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
    public class WeightHistoryItem
    {
        public WeightHistoryItem()
        {
            this.DateCreated = DateTime.Now;
            this.WeightHistoryId = Guid.NewGuid();
        }

        [Key]
        public Guid WeightHistoryId { get; set; }

        public decimal Weight { get; set; }

        public DateTime DateCreated { get; set; }

    
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
