// -----------------------------------------------------------------------
// <copyright file="IScheduleTextHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UGitFit.Model.Structures;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IScheduleTextHandler
    {
        string SendUserText(TextToSend scheduledText);
    }
}
