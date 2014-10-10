// -----------------------------------------------------------------------
// <copyright file="DomainLogic.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.TrackingDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using UGitFit.Model;
    using UGitFit.Model.Structures;
    using UGitFit.Model.Interfaces;
    using Twilio;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DomainLogic
    {
        IRepository _repository = null;

        public enum ValidatePhoneNumberResultEnumeration
        {
            IsValid = 0,
            InvalidRegCode = 1,
            PhoneNotFound = 2
        }

        public DomainLogic(IRepository repository)
        {
            _repository = repository;
        }

        public string ProcessTextMessageReceived(string country, string state, string city, string cellPhoneNumber, string messageBody, bool debugging)
        {
            InboundTestMessageHandler textMsgHandler = new InboundTestMessageHandler(_repository);
            return textMsgHandler.ProcessTextMessageReceived(country, state, city, cellPhoneNumber, messageBody, debugging);
        }

        public ValidatePhoneNumberResultEnumeration ValidatePhoneNumberAndRegCode(string phoneNumber, string regCode)
        {
            Person p = _repository.FindPersonByPhoneNumber(phoneNumber);

            if (p == null)
                return ValidatePhoneNumberResultEnumeration.PhoneNotFound;

            if (p.RegistrationCode.Equals(regCode, StringComparison.InvariantCultureIgnoreCase))
                return ValidatePhoneNumberResultEnumeration.IsValid;

            return ValidatePhoneNumberResultEnumeration.InvalidRegCode;
        }

        public string SendUserTheirRegistrationCode(string phoneNumber)
        {
            Person p =_repository.FindPersonByPhoneNumber(phoneNumber);
            if (p == null)
                return "FAILURE";

            TextToSend txt = new TextToSend();
            txt.IsDebugNumber = p.PhoneNumbers.First().IsDebugNumber;
            txt.PhoneNumber = p.PhoneNumbers.First().Digits;
            txt.TranslatedText = string.Format("UGitFit RegCode: {0}", p.RegistrationCode);
            txt.ReferenceText = txt.TranslatedText;
            txt.PersonId = p.PersonId;
            txt.FirstName = p.FirstName;
            txt.LastName = p.LastName;

            uGitFitDefaultHandler txtMsgHandler = new uGitFitDefaultHandler();
            txtMsgHandler.SendUserText(txt);

            return p.RegistrationCode;
        }

        public List<TextToSend> SendScheduledTexts(IScheduleTextHandler handler)
        {
            if (handler == null)
                handler = new uGitFitDefaultHandler();

            List<TextToSend> rtnVal = this.GetScheduledTextsNeedingToBeSent(true);

            foreach (TextToSend txt in rtnVal)
            {
                TrackingRequest tr = new TrackingRequest() { DateSent = txt.CurrentLocalTime, PersonId = txt.PersonId, TextSent = txt.TranslatedText, UserScheduleId = txt.UserScheduleId };
                _repository.TrackingReqeusts.Add(tr);
                _repository.SaveChanges();
                               
                handler.SendUserText(txt);
              
            }

            return rtnVal;
        }

        public List<TextToSend> GetScheduledTextsNeedingToBeSent(bool noRedundantPhoneNumbers)
        {
            List<TextToSend> textsToSend = _repository.RetrieveNextScheduledTextsToSend();
            List<TextToSend> rtnVal = new List<TextToSend>();

            List<string> phoneNumbers = new List<string>();

            foreach (TextToSend txt in textsToSend)
            {
                if ((!phoneNumbers.Contains(txt.PhoneNumber)) || noRedundantPhoneNumbers == false)
                {
                    //We don't want to send multiple texts to a single user (eg. it's 11 pm and we just started the service
                    //don't send what did you have for breakfest, what did you have for lunch, what did you have for dinner all at 1 shot.
                    rtnVal.Add(txt);
                    phoneNumbers.Add(txt.PhoneNumber);
                }
            }

            
            return rtnVal;
        }

      
    }
}