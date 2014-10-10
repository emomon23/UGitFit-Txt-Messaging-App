// -----------------------------------------------------------------------
// <copyright file="UGFRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.Entity;
    using System.Data.EntityModel;
    using UGitFit.Model;
    using UGitFit.Model.Structures;
    using System.Data.EntityClient;
    using System.Data.SqlClient;
    using System.Data.Sql;
    using UGitFit.Model.Interfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UGFContext : DbContext, IRepository
    {
        private string _connString = "";

        public DbSet<Language> Languages { get; set; }
        public DbSet<UGitFit.Model.DayOfWeek> DaysOfWeek { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<UGitFit.Model.TimeZone> TimeZones { get; set; }
        public DbSet<SuspendedReason> SuspendedReasons { get; set; }
        public DbSet<DefaultSchedule> DefaultSchedules { get; set; }
        public DbSet<ClientStatus> ClientStatuses { get; set; }
        public DbSet<BusinessPartner> BusinessPartners { get; set; }
        public DbSet<PartnerClient> PartnerClients { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<UserSchedule> UserSchedules { get; set; }
        public DbSet<UGFTextMessage> SystemTexts { get; set; }
        public DbSet<TrackingRequest> TrackingReqeusts { get; set; }
        public DbSet<TrackingResponse> TrackingResponses { get; set; }
        public DbSet<TimezoneAreacode> AreaCodes { get; set; }
        public DbSet<ExerciseRecord> Exercise { get; set; }
        public DbSet<NutrtionHistoryItem> NutritionHistory { get; set; }
        public DbSet<WeightHistoryItem> WeightHistory { get; set; }

        public MealType GetMealType(Guid trackingRequestId)
        {
            var result =
                from t in this.TrackingReqeusts
                join us in this.UserSchedules
                    on t.UserScheduleId.Value equals us.UserScheduleId
                join s in this.DefaultSchedules
                    on us.ScheduleId equals s.DefaultScheduleId
                join mt in this.MealTypes
                    on s.MealTypeId equals mt.MealTypeId
                where t.UserScheduleId.HasValue
                && t.TrackingRequestId == trackingRequestId
                select mt;

            if (result == null || result.Count() == 0)
                return null;

            return result.First();

        }

        public void ExecuteEmbeddedSQL(string sql)
        {
            this.Database.ExecuteSqlCommand(sql);
        }

        public UGFContext()
            : base("uGFDataTracking")
        {
            _connString = this.Database.Connection.ConnectionString;

        }

        public Person FindPersonByPhoneNumber(string phoneNumber)
        {
            IQueryable<Person> result = (from p in this.People
                                        join pn in this.PhoneNumbers
                                            on p.PersonId equals pn.PersonId
                                        where pn.Digits == phoneNumber
                                        select p).Include(t => t.TimeZone);


            return result.FirstOrDefault();

        }

        public List<TextToSend> RetrieveNextScheduledTextsToSend()
        {
            //Before we begin, we need to suspend all the the 'EvaluationPeriod expired and those who have not responded to a TrackingRequest for sometime
            this.SuspendInactiveClients_AndTrialPeriodOver();

            //Now we can retrieve text messages that should be sent
            List<TextToSend> rtnVal = this.GetTextToSendNow();

            return rtnVal;
        }

     
        public ClientStatus FindClientStatusObject(string clientStatusId)
        {
            IQueryable<ClientStatus> results = this.ClientStatuses.Where(c => c.ClientStatusId == clientStatusId);
            if (results.Count() == 0)
                return null;

            return results.First();
        }

        public Language GetLanguage(string name)
        {
            IQueryable<Language> result =
                from l in this.Languages
                where l.ReferenceText == name
                select l;

            if (result == null || result.Count() == 0)
                return null;

            return result.First();
        }

        public TrackingRequest GetLastTrackingRequest(string phoneNumber)
        {
            var result =
                from t in this.TrackingReqeusts
                join ps in this.People
                  on t.PersonId equals ps.PersonId
                join p in this.PhoneNumbers
                  on ps.PersonId equals p.PersonId
                where p.IsActive == true
                && p.Digits == phoneNumber
                orderby t.DateSent descending
                select t;

            if (result != null && result.Count() > 0)
                return (TrackingRequest)result.First();

            return null;
        }

        public Model.TimeZone GetTimeZone(string name)
        {
            IQueryable<Model.TimeZone> result =
                from t in this.TimeZones
                where t.ReferenceText == name
                select t;

            if (result == null || result.Count() == 0)
                return null;

            return result.First();
        }

        public string DataSource
        {
            get { return _connString; }
        }

        public TrackingRequest GetLastTrackingRequest(Guid personId)
        {
            DateTime dt = this.TrackingReqeusts.Where(tr1 => tr1.PersonId == personId).Max(tr2 => tr2.DateSent);

            IQueryable<TrackingRequest> requests =
                from tr3 in this.TrackingReqeusts
                where tr3.PersonId == personId &&
                tr3.DateSent == dt
                select tr3;

            if (requests != null && requests.Count() > 0)
            {
                List<TrackingRequest> list = requests.ToList();
                return list[list.Count - 1];
            }
            return null;

        }

        public Model.TimeZone FindTimezone(int areaCode, string city, string state)
        {
            var areaCodeSearch =
                from t in this.TimeZones
                join a in this.AreaCodes
                   on t.TimeZoneId equals a.TimeZoneId
                where a.AreaCode == areaCode
                select t;

            if (areaCodeSearch != null && areaCodeSearch.Count() != 0)
                return (Model.TimeZone)areaCodeSearch.First();

            if (!city.isNullOrEmpty())
            {
                var citySearch =
                    from t in this.TimeZones
                    join a in this.AreaCodes
                       on t.TimeZoneId equals a.TimeZoneId
                    where a.City == city && a.StateOrProvice == state
                    select t;

                if (citySearch != null && citySearch.Count() != 0)
                    return (Model.TimeZone)citySearch.First();

            }

            return null;
        }

        public UGitFit.Model.TimeZone FindTimezoneByReferenceText(string referenceText)
        {
            var result =
                from t in this.TimeZones
                where t.ReferenceText == referenceText
                select t;

            if (result != null & result.Count() > 0)
                return (Model.TimeZone)result.First();

            return null;
        }

        public UGitFit.Model.TimeZone FindTimezoneByAreaCode(int areaCode)
        {
            var result =
               from t in this.TimeZones
               join ac in this.AreaCodes
                on t.TimeZoneId equals ac.TimeZoneId
               where ac.AreaCode == areaCode
               select t;

            if (result == null || result.Count() == 0)
                return null;

            return (Model.TimeZone)result.First();
        }

        public TimezoneAreacode FindAreaCode(int areaCode)
        {
            var result =
                from ac in this.AreaCodes
                where ac.AreaCode == areaCode
                select ac;

            if (result != null && result.Count() > 0)
                return (TimezoneAreacode)result.First();

            return null;
        }

        public void UpdateUnknownTimezones(string state, int timezoneId)
        {
            string unknownTimeZoneName = "UNKNOWN";

            string sql = string.Format("UPDATE TimeZoneAreacodes SET TimeZoneId={0} WHERE StateOrProvice='{1}' AND TimeZoneId IN (SELECT TimeZoneId FROM TimeZones WHERE ReferenceText='{2}' AND StateOrProvice='{1}')", timezoneId, state, unknownTimeZoneName);
            this.Database.ExecuteSqlCommand(sql);
        }

        public Model.TimeZone FindTimezoneByState(string state)
        {
            Model.TimeZone rtnVal = null;
            string unknown = "UNKNOWN";

            var result =
                from t in this.TimeZones
                join ac in this.AreaCodes
                 on t.TimeZoneId equals ac.TimeZoneId
                where ac.StateOrProvice == state
                select t;

            if (result == null || result.Count() == 0)
                return null;

            foreach (var tz in result)
            {
                if (tz.ReferenceText != unknown)
                {
                    rtnVal = (Model.TimeZone)tz;
                    break;
                }
            }

            return rtnVal;
        }


        public string TRIALEXPIRED_TXT_MSG {
            get {
                return this.GetDefinedTextMessage("TRIALEXPIRED");
            }
        }

        public string PREMATURE_TXT_MSG
         {
            get {
                return this.GetDefinedTextMessage("PREMATURE");
            }
         }

        public string REACTIVEATED_TXT_MSG
         {
            get {
                return this.GetDefinedTextMessage("REACTIVEATED");
            }
         }

        public string TRACK_ME_TXT_MSG
         {
            get {
                return this.GetDefinedTextMessage("TRACK_ME");
            }
         }

        public string ACCT_DELETED_TXT_MSG
         {
            get {
                return this.GetDefinedTextMessage("ACCT_DELETED");
            }
         }

        public string DATA_SAVED_TXT_MSG
         {
            get {
                return this.GetDefinedTextMessage("DATA_SAVED");
            }
         }

        public string SUSPENDED_TXT_MSG
         {
            get {
                return this.GetDefinedTextMessage("SUSPENDED");
            }
         }

        public string SPAM_TXT_MSG
         {
            get {
                return this.GetDefinedTextMessage("SPAM");
            }
         }
                          
        private string GetDefinedTextMessage(string msgId)
        {
            var msg = this.SystemTexts.Where(m => m.TextMessageId == msgId).FirstOrDefault();

            if (msg == null || msg.ReferenceText.isNullOrEmpty())
                throw new Exception(string.Format("Unable to find {0} in the UGFTextMessages table. ", msgId));

            return msg.ReferenceText;
        }

        private void SuspendInactiveClients_AndTrialPeriodOver()
        {
            string sql = "	UPDATE People p3 SET	p3.ClientStatusId='SUSPENDED', p3.ReasonForSuspensionId = 'UNRESPONSIVE' WHERE p3.PersonId IN (SELECT fu.PersonId FROM (SELECT p2.PersonId FROM People p2 INNER JOIN (SELECT LDR.PersonId, Count(*) As MissedCount FROM (SELECT p.PersonId, MAX(IFNull(trsp.DateReceived, str_to_date('1/01/1900', '%m/%d/%Y'))) AS LastDateReceived FROM People p INNER JOIN TrackingRequests tr ON p.PersonId = tr.PersonId 	LEFT OUTER JOIN TrackingResponses trsp ON tr.TrackingRequestId = trsp.TrackingRequestId GROUP BY p.PersonId) LDR INNER JOIN TrackingRequests tr2 ON LDR.PersonId = tr2.PersonId WHERE tr2.DateSent > LDR.LastDateReceived GROUP BY LDR.PersonId) miss ON p2.PersonId = miss.PersonId WHERE p2.ClientStatusId NOT IN ('SPAMMER_SUSPECT', 'SUSPENDED', 'TRIAL_PERIOD_EXPIRED') AND IFNull(str_to_date(p2.ResumedDate, '%m/%d/%Y'), DATE_SUB(NOW(), INTERVAL 100 DAY)) < DATE_SUB(NOW(), INTERVAL 5 DAY) AND miss.MissedCount > 10) fu )";
            
            this.Database.ExecuteSqlCommand(sql);
         
        }

        private List<TextToSend> GetTextToSendNow()
        {
            //List<PersonLocalTimeSchedule> peopleTimes = this.GetPeopleAndTheirLocalTimes();
            //IEnumerable<Model.ScheduledText> texts =
            //   this.Database.SqlQuery<ScheduledText>("RetrieveRequestScheduleToSend").OrderBy(t => t.PhoneNumber).ThenByDescending(x => x.CurrentLocalTime);

            List<TextToSend> rtnValue = new List<TextToSend>();
            DateTime utc = DateTime.UtcNow;
            DateTime yesterDay = utc.AddHours(-24);

            /* Get ALL of the user schedules where the user is actively particpating, Order by userschedule.OverridUTCTimeOfDayToSend decending
             * (NOTE: If any of the users schedules time of day fields change, they must all change, a time of day set to 
             *  6 AM, 2/3/2004 will be LATER than a time set to 7 PM, 1/2/2003, It's the time of day we're interested in, but that date gets in the way
             *  LINQ doesn't let us to date function.  PAIN IN THE ARSE!)    
             * 
             * JOIN: UserSchedules to People
             *       People to TimeZone, 
             *       People to PhoneNumber
             *       UserSchedule to DefaultUserSchedule
             */
            int debugCount = 0;
            string debugString = "";

            var userSchedules = from us in this.UserSchedules
                                join p in this.People
                                    on us.PersonId equals p.PersonId
                                join tz in this.TimeZones
                                    on p.TimeZoneId equals tz.TimeZoneId
                                join pn in this.PhoneNumbers
                                    on p.PersonId equals pn.PersonId
                                join ds in this.DefaultSchedules
                                    on us.ScheduleId equals ds.DefaultScheduleId
                                where ds.IsActive == true
                                && "SPAMMER_SUSPECT, SUSPENDED, TRIAL_PERIOD_EXPIRED".Contains(p.ClientStatusId) == false
                                && pn.IsActive == true
                                orderby p.PersonId, us.OverrideUTCTimeOfDayToSend descending
                                select new
                                {
                                    PersonId = p.PersonId,
                                    UserScheduleId = us.UserScheduleId,
                                    ReferenceText = ds.ReferenceText,
                                    pn.Digits,
                                    p.LanguageId,
                                    p.FirstName,
                                    p.LastName,
                                    tz.CurrentUTCOffset_InHours,
                                    pn.IsDebugNumber,
                                    TimeOfDayToSend = us.OverrideUTCTimeOfDayToSend,
                                    us.SuspendStartDate,
                                    us.SuspendEndDate,
                                    ds.TranslationXML,

                                };

            debugCount = userSchedules.Count();
            foreach (var t in UserSchedules)
            {
                debugString += t.PersonId + ": ";
            }

            //FOR EACH USER IN OUR SYSTEM, Get the last tracking request sent to them
            var latestTrackingRequests = (from t in this.TrackingReqeusts
                                          group t by t.UserScheduleId
                                              into tr
                                              select new
                                              {
                                                  UserScheduleId = tr.Key,
                                                  DateSent = (from t2 in tr select t2.DateSent).Max()
                                              }).ToList();

            debugCount = latestTrackingRequests.Count();

            var allRecentTrackingRequests = (from t in this.TrackingReqeusts
                                             join us in this.UserSchedules
                                               on t.UserScheduleId equals us.UserScheduleId
                                             where t.DateSent > yesterDay
                                             select new
                                             {
                                                 t.DateSent,
                                                 us.PersonId
                                             }).ToList();

            debugCount = allRecentTrackingRequests.Count();

            Guid controlBreakIndicator = new Guid();

            debugCount = userSchedules.Count();

            foreach (var userSchedule in userSchedules)
            {
                if (controlBreakIndicator != userSchedule.PersonId)
                {
                    DateTime utcTimeToSend = DateTime.Parse(userSchedule.TimeOfDayToSend.ToShortTimeString());
                    DateTime localTimeToSend = utcTimeToSend.AddHours(userSchedule.CurrentUTCOffset_InHours * -1);
                    DateTime localTimeNow = utc.AddHours(userSchedule.CurrentUTCOffset_InHours);

                    if (localTimeToSend < localTimeNow)
                    {


                        //See if we've sent a tracking request based on this userschedule (eg. Lunch userschedule vs Dinner userSchedule) within the last 24 hours
                        bool sendTextNowCandidate = ((from lt in latestTrackingRequests
                                                      where lt.UserScheduleId == userSchedule.UserScheduleId
                                                      && lt.DateSent > yesterDay
                                                      select lt).FirstOrDefault() == null);

                        if (sendTextNowCandidate)
                        {
                            sendTextNowCandidate = ((from rc in allRecentTrackingRequests
                                                     where rc.PersonId == userSchedule.PersonId
                                                     && rc.DateSent >= localTimeToSend
                                                     select rc).FirstOrDefault() == null);
                        }


                        if (sendTextNowCandidate)
                        {
                            rtnValue.Add(new TextToSend(userSchedule.CurrentUTCOffset_InHours, userSchedule.TranslationXML, userSchedule.LanguageId)
                            {
                                IsDebugNumber = userSchedule.IsDebugNumber,
                                PersonId = userSchedule.PersonId,
                                PhoneNumber = userSchedule.Digits,
                                ReferenceText = userSchedule.ReferenceText,
                                UserScheduleId = userSchedule.UserScheduleId,
                                FirstName = userSchedule.FirstName,
                                LastName = userSchedule.LastName,
                                LocalTimeToSend = localTimeToSend
                            });

                            //We've indicated we're going to send a tracking request to this person
                            //Set the controlBreakIndicator so we don't send them more than 1 tracking request today
                            controlBreakIndicator = userSchedule.PersonId;
                        }
                    }
                }
            }


            return rtnValue;
        }
       
    }

}