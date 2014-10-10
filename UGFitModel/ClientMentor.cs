// -----------------------------------------------------------------------
// <copyright file="ClientMentor.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UGitFit.Model;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// TODO: Update summary.
    [Table("clientmentors")]
    public class ClientMentor
    {
        public ClientMentor()
        {
            this.ClientMentorId = Guid.NewGuid();
            this.DateCreated = DateTime.Now;
            this.ClientMentorSearchKey = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
        }

        [Key]
        public Guid ClientMentorId { get; set; }

        [StringLength(100)]
        public string ClientMentorName { get; set; }

        [StringLength(10)]
        public string ClientMentorSearchKey { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ICollection<Person> Clients { get; set; }


    }
}
