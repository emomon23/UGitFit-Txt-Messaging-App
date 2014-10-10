// -----------------------------------------------------------------------
// <copyright file="NutrtionHistoryItem.cs" company="">
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
    public class NutrtionHistoryItem
    {
        public NutrtionHistoryItem()
        {
            this.NutritionHistoryId = Guid.NewGuid();
            this.DateCreate = DateTime.Now;
        }

        [Key]
        public Guid NutritionHistoryId { get; set; }

        [MaxLength(300)]
        public string MealText { get; set; }

        [ForeignKey("MealType")]
        public Guid MealTypeId { get; set; }

        [ForeignKey("Person")]
        public Guid PersonId { get; set; }


        public DateTime DateCreate { get; set; }

        public decimal? Cost { get; set; }

        public int ? SurgarGrams { get; set; }

        public int ? FatGrams { get; set; }

        public int? CarbGrams { get; set; }

        public int? Calories { get; set; }
        
        public virtual Person Person { get; set; }
        public virtual MealType MealType { get; set; }

    }
}
