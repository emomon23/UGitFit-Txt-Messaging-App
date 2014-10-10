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
    using UGitFit.Model.Interfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class InboundTestMessageHandler
    {
        IRepository _repository = null;

        Person _person = null;
        string _state;
        string _country;
        string _phoneNumber;
        string _city;
        string _body;
        bool _debugging;
        string _bodyUCASE;
        string _result = null;

        public InboundTestMessageHandler(IRepository repository)
        {
            _repository = repository;
        }


        public string ProcessTextMessageReceived(string country, string state, string city, string cellPhoneNumber, string messageBody, bool debugging)
        {
            try
            {
                //messageBody is a
                // Track Me message?
                // Suspend Me / Stop Tracking Message?
                // Response Message?
                // Spam Message?

                //Set all of the class level variables, including Person _person

                Initialize(country, state, city, cellPhoneNumber, messageBody, debugging);
                if (this.TrialPeriodHasExpired())
                    return _result;

                switch (this.DetermineTextMessageType())
                {
                    case Enumerations.TextMessageTypeEnumeration.PrematureResponse:
                        this.ProcessPrematureResponse();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.ReactivateMe:
                        ReactivateUser();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.SpamSuspenct:
                        ProcessSpamText();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.SuspendMe:
                        SuspendUser();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.TrackingResponse:
                        RecordResponse();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.TrackMe:
                        this.CreatePreregisteredUser();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.DeleteAcctEntirely:
                        this.DeleteAcctEntirely();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.Unblacklist:
                        _result = "START";
                        break;
                    case Enumerations.TextMessageTypeEnumeration.ConvertSpamSuspectToPreRegister:
                        this.ConvertSpamSuspectToPreregister();
                        break;
                    case Enumerations.TextMessageTypeEnumeration.TimezoneTest:
                        this.TimeZoneTest();
                        break;
                }

                return _result;
            }
            catch (Exception exp)
            {
                return exp.Message + "<br/>" + exp.ToString();
            }
        }

        private void TimeZoneTest()
        {
            Model.TimeZone tz = this.GetTimeZone();
            _repository.SaveChanges();
            _result = "Your timezone is " + tz.ReferenceText;
        }

        private void ProcessPrematureResponse()
        {
            _result = _repository.PREMATURE_TXT_MSG;
        }

        private void ReactivateUser()
        {
            if (_person.SuspendUser(false, null, false))
            {
                _person.ResumedDate = DateTime.Now.ToShortDateString();
                _repository.SaveChanges();
                _result = _repository.REACTIVEATED_TXT_MSG;
            }
        }

        private Model.TimeZone GetTimezoneFromService(int areaCode)
        {
            Model.TimeZone rtnValTimeZone = null;

            try
            {
                TimeZoneServer.USZipSoapClient caller = new TimeZoneServer.USZipSoapClient("USZipSoap");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(caller.GetInfoByAreaCode(areaCode.ToString()).OuterXml);

                //<NewDataSet><Table><CITY>Wheaton</CITY><STATE>MN</STATE><ZIP>56296</ZIP><AREA_CODE>320</AREA_CODE><TIME_ZONE>C</TIME_ZONE></Table><Table>...
                string queryString = string.Format("Table[AREA_CODE = {0}]", areaCode);
                XmlNodeList list = doc.DocumentElement.SelectNodes(queryString);

                if (list != null)
                {
                    foreach (XmlNode areaCodeNode in list)
                    {
                        string state = areaCodeNode.SelectSingleNode("STATE").InnerXml;
                        string timeZoneChar = areaCodeNode.SelectSingleNode("TIME_ZONE").InnerXml.ToUpper();
                        if (timeZoneChar == "K")
                        {
                            var alaskTimeZone =
                                from at in _repository.TimeZones
                                where at.ReferenceText.Contains("ALASKA")
                                select at;

                            if (alaskTimeZone != null && alaskTimeZone.Count() > 0)
                                rtnValTimeZone = (Model.TimeZone)alaskTimeZone.First();
                        }
                        else
                        {
                            var queryResult =
                                from t in _repository.TimeZones
                                where t.ReferenceText.StartsWith(timeZoneChar)
                                select t;

                            if (queryResult != null && queryResult.Count() > 0)
                            {
                                rtnValTimeZone = (Model.TimeZone)queryResult.First();
                            }
                        }

                        break;
                    }
                }

                _result = string.Format("Your timezone is {0}", rtnValTimeZone.ReferenceText);
            }
            catch (Exception exp)
            {
                _result = "Unable to determine your timezone";
            }


            return rtnValTimeZone;
        }

        private void ProcessSpamText()
        {
            if (_person == null)
            {
                string clientStatusId = ClientStatus.SpammerSuspectId;
                this.CreateNewPersonObject(clientStatusId);
            }

            _person.ConsectiveSpamCount += 1;
            _repository.SaveChanges();

            if (_person.ConsectiveSpamCount < 4)
                _result = _repository.SPAM_TXT_MSG;
            else
                _result = "";
        }

        private void SuspendUser()
        {
            _person.SuspendUser(true, Enumerations.SuspendedReasonEnumeration.UserSuspendedThemselves, true);
            _repository.SaveChanges();

            _result = _repository.SUSPENDED_TXT_MSG ;

        }

        private void RecordResponse()
        {
            TrackingRequest request = _repository.GetLastTrackingRequest(_person.PersonId);
            if (request != null)
            {
                TextMessageParser parser = new TextMessageParser(_body, _person.PersonId, _repository.GetMealType(request.TrackingRequestId).MealTypeId);
                if (parser.ExerciseRecord != null)
                    _repository.Exercise.Add(parser.ExerciseRecord);

                if (parser.NutritionRecord != null)
                    _repository.NutritionHistory.Add(parser.NutritionRecord);

                if (parser.WeightRecord != null)
                    _repository.WeightHistory.Add(parser.WeightRecord);

                TrackingResponse response = new TrackingResponse();
                response.TrackingRequestId = request.TrackingRequestId;
                response.ResponseText = parser.ResponseText;
                
                _repository.TrackingResponses.Add(response);

                _repository.SaveChanges();

                _result = _repository.DATA_SAVED_TXT_MSG ;
            }
        }



        private void DeleteAcctEntirely()
        {
            if (_person != null)
            {
                _repository.People.Remove(_person);
                _repository.SaveChanges();
                _result = _repository.ACCT_DELETED_TXT_MSG ;
            }
            else
            {
                _result = "You may proceed with your test.";
            }


        }

        private Enumerations.TextMessageTypeEnumeration DetermineTextMessageType()
        {
            Enumerations.TextMessageTypeEnumeration result = Enumerations.TextMessageTypeEnumeration.SpamSuspenct;
            int count = this._bodyUCASE.Length;

            if (this._bodyUCASE.Contains(" TRACK ME ") || this._bodyUCASE.Contains(" TRACKME ") || _bodyUCASE == "TRACK")
            {
                // text message conains "Track me" in some form
                if (_person == null)
                {
                    //This is the 1st timie this phone number has texted uGF, it's a track me request.
                    result = Enumerations.TextMessageTypeEnumeration.TrackMe;
                }
                else
                {
                    if (_person.ClientStatusId == ClientStatus.SpammerSuspectId && _person.UserSchedules.Count() == 0)
                    {
                        result = Enumerations.TextMessageTypeEnumeration.ConvertSpamSuspectToPreRegister;
                    }
                    else
                    {
                        //This person is already in the system, assume they want to reactive themselves
                        result = Enumerations.TextMessageTypeEnumeration.ReactivateMe;

                        //_person does not equal null, but user has texted Track Me again?
                        if (_person.IsSuspended() == false && count > 12)
                        {
                            //The user is not suspened and the text message is long, so append what ever they texted 
                            //to uGF to their tracking history
                            result = Enumerations.TextMessageTypeEnumeration.TrackingResponse;
                        }
                    }
                }
            }
            else if (this._bodyUCASE == "UNBLACKLIST ME")
            {
                result = Enumerations.TextMessageTypeEnumeration.Unblacklist;
            }
            else if (((this._bodyUCASE.Contains(" RESUME") && count < 11) || (this._bodyUCASE.Contains("RESUME TRACKING") || this._bodyUCASE.Contains("RESUMETRACKING")) && count < 20) || (this._bodyUCASE.Contains(" REACTIVATE") && count < 16))
            {
                if (_person != null)
                    result = Enumerations.TextMessageTypeEnumeration.ReactivateMe;
                else
                    result = Enumerations.TextMessageTypeEnumeration.SpamSuspenct;
            }
            else if (((this._bodyUCASE.Contains(" STOP TRACKING") || this._bodyUCASE.Contains(" STOPTRACKING")) && this._bodyUCASE.Length < 16) || (this._bodyUCASE.Contains(" SUSPEND") && this._bodyUCASE.Length < 10) || (this._bodyUCASE.Contains(" SUSPEND ME") && count < 15) || (this._bodyUCASE.Contains("SUSPENDME") && count < 13))
            {
                if (_person != null)
                    result = Enumerations.TextMessageTypeEnumeration.SuspendMe;
                else
                    result = Enumerations.TextMessageTypeEnumeration.SpamSuspenct;
            }
            else if (_bodyUCASE.Trim() == "TZ")
            {
                result = Enumerations.TextMessageTypeEnumeration.TimezoneTest;
            }
            else if ((_bodyUCASE.Contains("TEST RESET") || _bodyUCASE.Contains("TESTRESET")) && count < 13)
            {
                result = Enumerations.TextMessageTypeEnumeration.DeleteAcctEntirely;
            }
            else if (_person != null)
            {
                if (_person.ClientStatusId != ClientStatus.SpammerSuspectId)
                {
                    //This needs to be the last else if in the list
                    result = Enumerations.TextMessageTypeEnumeration.TrackingResponse;
                }
            }

            if (result == Enumerations.TextMessageTypeEnumeration.TrackingResponse && _repository.TrackingReqeusts.Where(tr=>tr.PersonId == _person.PersonId).Count() == 0)
                result = Enumerations.TextMessageTypeEnumeration.PrematureResponse;

            if (result != Enumerations.TextMessageTypeEnumeration.SpamSuspenct && _person != null)
                _person.ConsectiveSpamCount = 0;

            return result;
        }


        private void Initialize(string country, string state, string city, string cellPhoneNumber, string messageBody, bool debugging)
        {
            _phoneNumber = cellPhoneNumber.Replace(" ", "").Replace("-", "").Replace(".", "").Replace("(", "").Replace(")", "");
            _person = _repository.FindPersonByPhoneNumber(cellPhoneNumber);
            _state = state.ToUpper();
            _country = country;
            _city = city;
            _debugging = debugging;

            if (!_phoneNumber.IsNumeric())
                throw new Exception(string.Format("Phone number specified is not numeric!"));

            _body = messageBody;
            _bodyUCASE = string.Format(" {0} ", messageBody.ToUpper()).Replace(".", ". ");
        }

        private void ConvertSpamSuspectToPreregister()
        {
            _person.ClientStatusId = ClientStatus.PreRegisteredId;
            this.CopyDefaultSchedulesForUser();
            _repository.SaveChanges();

            _result = _repository.TRACK_ME_TXT_MSG + _person.RegistrationCode;
        }

        private void CreatePreregisteredUser()
        {
            string clientStatusid = ClientStatus.PreRegisteredId;
            this.CreateNewPersonObject(clientStatusid);

            CopyDefaultSchedulesForUser();
            _repository.SaveChanges();

            _result = _repository.TRACK_ME_TXT_MSG +_person.RegistrationCode;

        }

        private void CreateNewPersonObject(string clinicStatusId)
        {
            PhoneNumber phone = new PhoneNumber()
            {
                Digits = _phoneNumber,
                IsActive = true,
                IsDebugNumber = _debugging
            };

            _person = new Person()
            {
                PersonId = Guid.NewGuid(),
                Language = _repository.GetLanguage("ENGLISH"),
                //LastContactDate=DateTime.Now,
                RegistrationCode = Guid.NewGuid().ToString().Substring(0, 5).ToUpper(),
                ClientStatusId = clinicStatusId,
                TimeZoneId = GetTimeZone().TimeZoneId
            };


            phone.PersonId = _person.PersonId;
            _repository.PhoneNumbers.Add(phone);
            _repository.People.Add(_person);
        }

        private bool TrialPeriodHasExpired()
        {
            if (_person != null && _person.TrialPeriodHasExpired)
            {
                _result = _repository.TRIALEXPIRED_TXT_MSG ;
                _person.ConsectiveSpamCount += 1;

                if (_person.ConsectiveSpamCount > 4)
                    _person.ClientStatusId = ClientStatus.SpammerSuspectId;

                _repository.SaveChanges();

                return true;
            }

            return false;
        }

        private Model.TimeZone GetTimeZone()
        {
            Model.TimeZone rtnVal = null;
            int areaCode = _phoneNumber.Substring(0, 3).ToInt(0);

            //Look in the database to see if we have the timezone for this areacode
            rtnVal = _repository.FindTimezoneByAreaCode(areaCode);

            if (rtnVal == null || rtnVal.IsUnknownTimeZone && _state.isNullOrEmpty() == false)
            {
                //We don't know the timezone for this areacode, let's 
                //check the database to see if we have another areacode in the same state as this areacode
                //and get the timezone for that other areacode
                Model.TimeZone stateTimeZone = _repository.FindTimezoneByState(_state);
                if (stateTimeZone != null && stateTimeZone.IsUnknownTimeZone == false)
                {
                    //We either don't have a record for this AREA CODE OR the AREA CODE on file  has an "unknown" timezone
                    //However, we do have an different areacode, in the same state as the areacode specified, and we do know
                    //what timezone the other areacode is in, so update (or insert) this areacode based on the states timezone
                    this.SaveTimeZoneAreaCode(areaCode, stateTimeZone.TimeZoneId);
                    rtnVal = stateTimeZone;
                }
            }

            //See if we need to call the webservice or not
            if (rtnVal == null)
            {
                // Call the 3rd part webservice to get the timzeone for this areacode.
                rtnVal = this.GetTimezoneFromService(areaCode);
                if (rtnVal != null)
                {
                    //The webservice returned a timezone for the specified areacde
                    //Save the areacode in the database and assign it to the speicifed timezone
                    this.SaveTimeZoneAreaCode(areaCode, rtnVal.TimeZoneId);

                    //Find any areacodes we have in our database that have an 'UNKNOWN' timezone
                    //that are in the same state as this areacode and update their timezone to 
                    //this timezone
                    _repository.UpdateUnknownTimezones(_state, rtnVal.TimeZoneId);
                }
                else
                {
                    //The 3rd party webservice did not find a timezone for the areacode,
                    //save the areacode in the database as unknown
                    rtnVal = _repository.FindTimezoneByReferenceText("UNKNOWN");
                    if (rtnVal != null)
                        this.SaveTimeZoneAreaCode(areaCode, rtnVal.TimeZoneId);
                }

            }

            return rtnVal;
        }

        private void SaveTimeZoneAreaCode(int areaCode, int timeZoneId)
        {
            var acList = _repository.AreaCodes.Where(a => a.AreaCode == areaCode);
            TimezoneAreacode areaCodeEntity = null;

            if (acList == null || acList.Count() == 0)
            {
                areaCodeEntity = new TimezoneAreacode() { City = _city, StateOrProvice = _state, TimeZoneId = timeZoneId, AreaCode = areaCode };
                _repository.AreaCodes.Add(areaCodeEntity);
            }

            areaCodeEntity.TimeZoneId = timeZoneId;


        }

        private void CopyDefaultSchedulesForUser()
        {
            IQueryable<DefaultSchedule> defaults = _repository.DefaultSchedules.Include("DaysToSend").Where(d => d.IsActive == true);

            foreach (DefaultSchedule defaultSched in defaults)
            {
                UserSchedule uc = new UserSchedule(defaultSched, _person);
                _repository.UserSchedules.Add(uc);
            }

        }

        private void ProcessException(string methodName, Exception exp)
        {

        }
    }
}
