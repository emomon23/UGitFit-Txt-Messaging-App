// -----------------------------------------------------------------------
// <copyright file="PhoneNumber.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PhoneNumber
    {
        private string _digits;

        public bool IsActive { get; set; }
        public Guid PersonId { get; set; }
        public bool IsDebugNumber { get; set; }
        
        [Key]
        [StringLength(20)]
        public string Digits
        {
            get
            {
                return _digits;
            }

            set
            {
                _digits = value;

                if (!this.isValid())
                    throw new ModelExceptions.InvalidPhoneNumberException(_digits);               
            }
        }

        public virtual Person Person { get; set; }

        public bool isValid()
        {
            return _digits.IsPhoneNumber();
        }
    }
}
