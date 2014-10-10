// -----------------------------------------------------------------------
// <copyright file="InvalidPhoneNumberException.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model.ModelExceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string phoneNumber) : base(string.Format("{0} is not a valid phone number", phoneNumber)) { }
    }
}
