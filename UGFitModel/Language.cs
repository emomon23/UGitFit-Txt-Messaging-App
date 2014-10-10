// -----------------------------------------------------------------------
// <copyright file="Language.cs" company="">
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
    [Table("languages")]
    public class Language : TranslatableEntity
    {
        public int LanguageId { get; set; }

    }
}
