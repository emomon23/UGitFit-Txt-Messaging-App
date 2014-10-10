// -----------------------------------------------------------------------
// <copyright file="MealType.cs" company="">
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
    [Table("mealtypes")]
    public class MealType :TranslatableEntity
    {
        public Guid MealTypeId { get; set; }
        
        public virtual ICollection<NutrtionHistoryItem> NutritionItems { get; set; }
    }
}
