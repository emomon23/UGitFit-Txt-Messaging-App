using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UGitFit.Model;

namespace UGitFit.Model.Structures
{
    [Serializable]
    public class TextToSend
    {
        private string _translatedText;

        public Guid PersonId { get; set; }
        public DateTime CurrentLocalTime { get; set; }
        public Guid UserScheduleId {get;set;}
        public int LangaugeId { get; set; }
        public string ReferenceText { get; set; }
        public DateTime LocalTimeToSend { get; set; }

        public bool IsDebugNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public TextToSend() { }

        public TextToSend(double timeZoneOffSet, string XMLTranslations, int languageId)
        {
            this.CurrentLocalTime = DateTime.UtcNow.AddHours(timeZoneOffSet);

            this.LangaugeId = languageId;
            this.TranslatedText = TranslatableEntity.GetTranslation(XMLTranslations, languageId);
        }

        public string TranslatedText
        {
            get
            {
                string rtnVal = _translatedText.isNullOrEmpty() ? this.ReferenceText : _translatedText;

                return rtnVal.Replace("[NAME]", this.FirstName).Replace("[FIRSTNAME]", this.FirstName);
            }

            set
            {
                _translatedText = value;
            }
        }

        public string DisplayWholeName(string resultsIfBothAreNull)
        {
            string result = this.FirstName;

            if (!this.LastName.isNullOrEmpty())
            {
                if (result.Length > 0)
                    result += " ";


                result += this.LastName;
            }

            if (result.isNullOrEmpty())
                result = resultsIfBothAreNull;

            return result;
        }

        public string ToXMLString()
        {
            return string.Format("<TextToSend firstName='{0}' lastName='{1}' localTimeToSend='{2}' localTimeNow='{3}' phoneNumber='{4}' languageId='{5}' textSent='{6}' isDebug='{7}' />",
                            this.FirstName, this.LastName, this.LocalTimeToSend.ToShortTimeString(), this.CurrentLocalTime.ToShortTimeString(), this.PhoneNumber, this.LangaugeId, this.TranslatedText, this.IsDebugNumber);

        }

        public static string ListToXMLString(List<TextToSend> list)
        {
            string rtnVal = "<TextToSendList>";

            foreach (TextToSend txt in list)
            {
                rtnVal += txt.ToXMLString() + "\n";
            }

            rtnVal += "</TextToSendList>";

            return rtnVal;
        }
    }
}
