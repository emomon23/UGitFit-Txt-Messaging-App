// -----------------------------------------------------------------------
// <copyright file="UGFTextMessage.cs" company="">
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
    public class UGFTextMessage : TranslatableEntity
    {
        [Key]
        [StringLength(50)]
        public string TextMessageId { get; set; }
    }
}
