// -----------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.TrackingDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UGitFit.Model.Interfaces;
    using System.Net;
    using System.Net.Mail;
    using Twilio;
    using UGitFit.Model.Structures;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class uGitFitDefaultHandler : IScheduleTextHandler
    {
        public string SendUserText(TextToSend scheduledText)
        {
            string rtnVal = "Twilio";
            string firstname = "";
            if (!string.IsNullOrEmpty(scheduledText.FirstName))
                firstname = scheduledText.FirstName;

            scheduledText.TranslatedText = scheduledText.TranslatedText.Replace("[NAME]", firstname).Replace(" .", ".");


            if (scheduledText.IsDebugNumber)
            {
                EmailText(scheduledText);
                rtnVal = "Email";
            }
            else
            {
                try
                {
                    string SID = "ACfc3c13b82eae415ba1d063bc27c59e9e";
                    string TOKEN = "5d79636949d45f641314166a356046b7";
                    string FROM = "+16122237675";
                   
                   
                    TwilioRestClient client = new TwilioRestClient(SID, TOKEN);
                    client.SendSmsMessage(FROM, scheduledText.PhoneNumber, scheduledText.TranslatedText);
                }
                catch (Exception exp)
                {
                    rtnVal = string.Format("Twilio error: {0}", exp.Message);

                    Exception expInner = exp.InnerException;
                    while (expInner != null)
                    {
                        rtnVal += "\n\n" + expInner.Message;
                        expInner = expInner.InnerException;
                    }


                }
            }

            return rtnVal;
        }

        private void EmailText(UGitFit.Model.Structures.TextToSend schedText)
        {
            System.Net.Mail.MailMessage msg = new MailMessage(new MailAddress("memo@iemosoft.com"), new MailAddress("ugfdt@hotmail.com"));
            msg.Body = schedText.TranslatedText;

            System.Net.Mail.SmtpClient mailClient = new SmtpClient("smtpout.secureserver.net");
            mailClient.Port = 80;
            mailClient.Credentials = new System.Net.NetworkCredential("memo@iemosoft.com", "Never4get!");

            msg.Subject = string.Format("UGTDT Test Text Msg {0}", schedText.DisplayWholeName("Anonomous User"));
            mailClient.Send(msg);

        }
    }
}
