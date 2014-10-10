// -----------------------------------------------------------------------
// <copyright file="UGFSeeder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity;
    using UGitFit.DAL;
    using UGitFit.Model;
    using UGitFit.Model.Interfaces;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UGFSeeder : IDisposable
    {
        IRepository _db = null;

        private readonly string MORNING_MEAL = "UGitFit: Good morning [NAME].  What did you have for breakfest?";
        private readonly string LUNCH_MEAL = "UGitFit: Good afternoon [NAME].  What did you have for lunch?";
        private readonly string EVENING_MEAL = "UGitFit: Good evening [NAME].  What did you have for dinner (supper)?";
        private int ENGLISH_ID = 1;
        private int GERMAN_ID = 2;
        private int FRENCH_ID = 3;
        private int SPANISH_ID = 4;

        public static void DropAndRecreateEntireDatabase()
        {
            using (UGFSeeder db = new UGFSeeder(true, new UGFContext()))
            {
                db.SeedClientStatuses();
                db.SeedDaysOfWeek();
                db.SeedLanguages();
                db.SeedMealTypes();
                db.SeedSuspendedReasons();
                db.SeedTimeZones();
                db.SeedUGTTexts();
                db.SeedDefaultSchedules();
            }
        }

        public static void SeedExistingDatabase()
        {
            using (UGFSeeder db = new UGFSeeder(false, new UGFContext()))
            {
                db.SeedClientStatuses();
                db.SeedDaysOfWeek();
                db.SeedLanguages();
                db.SeedMealTypes();
                db.SeedSuspendedReasons();
                db.SeedTimeZones();
                db.SeedUGTTexts();
                db.SeedDefaultSchedules();
            }
        }

        //TEST SET UP: This method will find the phoneNumber specified and will create numberOfRequests
        //for that phone number.  These requests will have no TrackingResponses associated with them
        public static void CreateTrackingRequests(string phoneNumber, int numberOfRequests)
        {
            using (UGFContext db = new UGFContext())
            {
                Person p = db.FindPersonByPhoneNumber(phoneNumber);
                if (p == null)
                    throw new Exception("Unable to find person with phonenumber: " + phoneNumber);

                DateTime backDate = (DateTime.Now - new TimeSpan(((int)numberOfRequests / 3) + 2, 0, 0, 0)).ToShortDateString().ToDate();

                if (p.CreatedDate > backDate)
                {
                    p.CreatedDate = backDate;
                    p.ResumedDate = "";
                    db.SaveChanges();
                }

                IEnumerable<UserSchedule> schedules = p.UserSchedules;
                int currentCount = 0;

                while (backDate <= DateTime.Now)
                {
                    foreach (UserSchedule s in schedules.OrderBy(t => t.OverrideUTCTimeOfDayToSend))
                    {
                        backDate = backDate.SetTimeOfDay(s.OverrideUTCTimeOfDayToSend.ToShortTimeString());

                        string text = s.SourceSchedule.ReferenceText;
                        TrackingRequest request = new TrackingRequest() { PersonId = p.PersonId, DateSent = backDate, TextSent = text, UserScheduleId = s.UserScheduleId };
                        db.TrackingReqeusts.Add(request);
                        currentCount += 1;

                        if (currentCount >= numberOfRequests)
                        {
                            backDate = DateTime.Now;
                            break;
                        }
                    }

                    backDate = backDate.AddDays(1);
                }

                db.SaveChanges();
            }
        }

        public UGFSeeder(bool recreatDB, IRepository db)
        {
            _db = db;

            if (recreatDB)
            {
                //The database needs to exist, these statements will drop all of the objects from the database
                //and than weill create the tables found in CreateTablesSQLStatement
                //This was done because GoDaddy won't let code first create a new database! Grrrrr
                _db.ExecuteEmbeddedSQL(MySQL_UGitFitStatements.DropAndCreateTablesSQLStatement);
                
            }
          

        }

        public void SeedLanguages()
        {
           
            string lang = "ENGLISH";
            int id = ENGLISH_ID;
            if (_db.Languages.Where(l => l.ReferenceText == lang).Count() == 0)
                _db.Languages.Add(new Language() { LanguageId = id, ReferenceText = lang, TranslationXML = this.CreateSeedXMLForTranslations(string.Format("{0}{1}", lang.Substring(0, 1), lang.Substring(1).ToLower())) });


            lang = "FRENCH";

            id = FRENCH_ID;
            if (_db.Languages.Where(l => l.ReferenceText == lang).Count() == 0)
                _db.Languages.Add(new Language() { LanguageId = id, ReferenceText = lang, TranslationXML = this.CreateSeedXMLForTranslations(string.Format("{0}{1}", lang.Substring(0, 1), lang.Substring(1).ToLower())) });

            lang = "GERMAN";
            id = GERMAN_ID;
            if (_db.Languages.Where(l => l.ReferenceText == lang).Count() == 0)
                _db.Languages.Add(new Language() { LanguageId = id, ReferenceText = lang, TranslationXML = this.CreateSeedXMLForTranslations(string.Format("{0}{1}", lang.Substring(0, 1), lang.Substring(1).ToLower())) });

            lang = "SPANISH";
            id = SPANISH_ID;
            if (_db.Languages.Where(l => l.ReferenceText == lang).Count() == 0)
            {
                _db.Languages.Add(new Language() { LanguageId = id, ReferenceText = lang, TranslationXML = this.CreateSeedXMLForTranslations(string.Format("{0}{1}", lang.Substring(0, 1), lang.Substring(1).ToLower())) });
            }
            _db.SaveChanges();

           
        }

        public void SeedDaysOfWeek()
        {

            string day = "Monday";
            if (_db.DaysOfWeek.Where(d => d.ReferenceText == day).Count() == 0)
                _db.DaysOfWeek.Add(new UGitFit.Model.DayOfWeek() { ReferenceText = day, DayOfWeekId = day.ToUpper(), TranslationXML = this.CreateSeedXMLForTranslations(day) });

            day = "Tuesday";
            if (_db.DaysOfWeek.Where(d => d.ReferenceText == day).Count() == 0)
                _db.DaysOfWeek.Add(new UGitFit.Model.DayOfWeek() { ReferenceText = day, DayOfWeekId = day.ToUpper(), TranslationXML = this.CreateSeedXMLForTranslations(day) });

            day = "Wednesday";
            if (_db.DaysOfWeek.Where(d => d.ReferenceText == day).Count() == 0)
                _db.DaysOfWeek.Add(new UGitFit.Model.DayOfWeek() { ReferenceText = day, DayOfWeekId = day.ToUpper(), TranslationXML = this.CreateSeedXMLForTranslations(day) });

            day = "Thursday";
            if (_db.DaysOfWeek.Where(d => d.ReferenceText == day).Count() == 0)
                _db.DaysOfWeek.Add(new UGitFit.Model.DayOfWeek() { ReferenceText = day, DayOfWeekId = day.ToUpper(), TranslationXML = this.CreateSeedXMLForTranslations(day) });

            day = "Friday";
            if (_db.DaysOfWeek.Where(d => d.ReferenceText == day).Count() == 0)
                _db.DaysOfWeek.Add(new UGitFit.Model.DayOfWeek() { ReferenceText = day, DayOfWeekId = day.ToUpper(), TranslationXML = this.CreateSeedXMLForTranslations(day) });

            day = "Saturday";
            if (_db.DaysOfWeek.Where(d => d.ReferenceText == day).Count() == 0)
                _db.DaysOfWeek.Add(new UGitFit.Model.DayOfWeek() { ReferenceText = day, DayOfWeekId = day.ToUpper(), TranslationXML = this.CreateSeedXMLForTranslations(day) });

            day = "Sunday";
            if (_db.DaysOfWeek.Where(d => d.ReferenceText == day).Count() == 0)
                _db.DaysOfWeek.Add(new UGitFit.Model.DayOfWeek() { ReferenceText = day, DayOfWeekId = day.ToUpper(), TranslationXML = this.CreateSeedXMLForTranslations(day) });

            _db.SaveChanges();


        }

        public void SeedMealTypes()
        {
            string mealType = MORNING_MEAL;
            if (_db.MealTypes.Where(m => m.ReferenceText == mealType).Count() == 0)
                _db.MealTypes.Add(new MealType() { ReferenceText = mealType, MealTypeId = Guid.NewGuid(), TranslationXML = this.CreateSeedXMLForTranslations(mealType) });

            mealType = LUNCH_MEAL;
            if (_db.MealTypes.Where(m => m.ReferenceText == mealType).Count() == 0)
                _db.MealTypes.Add(new MealType() { ReferenceText = mealType, MealTypeId = Guid.NewGuid(), TranslationXML = this.CreateSeedXMLForTranslations(mealType) });

            mealType = EVENING_MEAL;
            if (_db.MealTypes.Where(m => m.ReferenceText == mealType).Count() == 0)
                _db.MealTypes.Add(new MealType() { ReferenceText = mealType, MealTypeId = Guid.NewGuid(), TranslationXML = this.CreateSeedXMLForTranslations(mealType) });

            _db.SaveChanges();
        }

        public void SeedClientStatuses()
        {
            string id = ClientStatus.RegisteredAndConfirmedId;
            string status = "Registered and confirmed";
            if (_db.ClientStatuses.Where(c => c.ReferenceText == status).Count() == 0)
                _db.ClientStatuses.Add(new ClientStatus() { ClientStatusId = id, ReferenceText = status, TranslationXML = this.CreateSeedXMLForTranslations(status) });

            id = ClientStatus.PreRegisteredId;
            status = "Pre-registered";
            if (_db.ClientStatuses.Where(c => c.ReferenceText == status).Count() == 0)
                _db.ClientStatuses.Add(new ClientStatus() { ClientStatusId = id, ReferenceText = status, TranslationXML = this.CreateSeedXMLForTranslations(status) });


            id = ClientStatus.RegisteredPreConfirmedId;
            status = "Registered, pending confirmation";
            if (_db.ClientStatuses.Where(c => c.ReferenceText == status).Count() == 0)
                _db.ClientStatuses.Add(new ClientStatus() { ClientStatusId = id, ReferenceText = status, TranslationXML = this.CreateSeedXMLForTranslations(status) });

            id = ClientStatus.PendingPasswordResetId;
            status = "Pending password reset";
            if (_db.ClientStatuses.Where(c => c.ReferenceText == status).Count() == 0)
                _db.ClientStatuses.Add(new ClientStatus() { ClientStatusId = id, ReferenceText = status, TranslationXML = this.CreateSeedXMLForTranslations(status) });

            id = ClientStatus.SpammerSuspectId;
            status = "Spammer Suspect";
            if (_db.ClientStatuses.Where(c => c.ReferenceText == status).Count() == 0)
                _db.ClientStatuses.Add(new ClientStatus() { ClientStatusId = id, ReferenceText = status, TranslationXML = this.CreateSeedXMLForTranslations(status) });

            id = ClientStatus.PreRegistrationTimedOutId;
            status = "Trial Period Expired";
            if (_db.ClientStatuses.Where(c => c.ReferenceText == status).Count() == 0)
                _db.ClientStatuses.Add(new ClientStatus() { ClientStatusId = id, ReferenceText = status, TranslationXML = this.CreateSeedXMLForTranslations(status) });

            id = ClientStatus.SuspendedId;
            status = "Suspended";
            if (_db.ClientStatuses.Where(c => c.ReferenceText == status).Count() == 0)
                _db.ClientStatuses.Add(new ClientStatus() { ClientStatusId = id, ReferenceText = status, TranslationXML = this.CreateSeedXMLForTranslations(status) });


            _db.SaveChanges();
        }

        public void SeedSuspendedReasons()
        {
            string id = "UNRESPONSIVE";
            string reason = "Unresponsive User";
            if (_db.SuspendedReasons.Where(r => r.ReferenceText == reason).Count() == 0)
                _db.SuspendedReasons.Add(new SuspendedReason() { ReferenceText = reason, SuspendedReasonId = id, TranslationXML = this.CreateSeedXMLForTranslations(reason) });

            id = "USER";
            reason = "User suspended themselves";
            if (_db.SuspendedReasons.Where(r => r.ReferenceText == reason).Count() == 0)
                _db.SuspendedReasons.Add(new SuspendedReason() { ReferenceText = reason, SuspendedReasonId = id, TranslationXML = this.CreateSeedXMLForTranslations(reason) });

            id = "ADMIN";
            reason = "Administrator suspended user";
            if (_db.SuspendedReasons.Where(r => r.ReferenceText == reason).Count() == 0)
                _db.SuspendedReasons.Add(new SuspendedReason() { ReferenceText = reason, SuspendedReasonId = id, TranslationXML = this.CreateSeedXMLForTranslations(reason) });

            _db.SaveChanges();
        }

        public void SeedUGTTexts()
        {
            AddUGFText("TRIALEXPIRED", "You're trial period has expired.  Please log into uGitFit.com and sign up for our full service data tracking.");

            AddUGFText("PREMATURE", "We haven't begun tracking data yet, please stand by.");

            AddUGFText("REACTIVEATED", "Your acct has been reactivated.");

            AddUGFText("SPAM", "Unable to process you text.  Text 'Track Me' if you would like to track your meals and spending.");

            AddUGFText("SUSPENDED", "Acct suspended. Text Track Me or goto uGitFit.com to restart.");

            AddUGFText("DATA_SAVED", "You data has been saved.");

            AddUGFText("ACCT_DELETED", "Test Account has been deleted.");

            AddUGFText("TRACK_ME", "Thank you for trying out uGitFit. You will receive 3 tracking request each day. Visit uGitFit.com to view your history. ur reg code is: ");

            _db.SaveChanges();

        }

        private void AddUGFText(string id, string text)
        {
            if (_db.SystemTexts.Where(s => s.TextMessageId == id).Count() == 0)
                _db.SystemTexts.Add(new UGFTextMessage() { ReferenceText = text, TextMessageId = id, TranslationXML = this.CreateSeedXMLForTranslations(text) });
        }

        public void SeedTimeZones()
        {
            string timezone = "EASTERN";
            int tId = 1;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = -4, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });

            timezone = "ATLANTIC";
            tId = 2;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = -3, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });

            timezone = "CENTRAL";
            tId = 3;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = -5, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });

            timezone = "MOUNTAIN";
            tId = 4;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = -6, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });


            timezone = "PACIFIC";
            tId = 5;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = -7, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });

            timezone = "ALASKA";
            tId = 6;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = -8, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });

            timezone = "HAWAII - ALEUTIAN";
            tId = 7;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = 0, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });

            timezone = "UNKNOWN";
            tId = 8;
            if (_db.TimeZones.Where(t => t.ReferenceText == timezone).Count() == 0)
                _db.TimeZones.Add(new UGitFit.Model.TimeZone() { CurrentUTCOffset_InHours = -5, ReferenceText = timezone, TimeZoneId = tId, TranslationXML = this.CreateSeedXMLForTranslations(timezone) });

            _db.SaveChanges();
        }

        public void SeedDefaultSchedules()
        {
            AddDefaultScheduleRecord(this.MORNING_MEAL, "10:05 am");
            AddDefaultScheduleRecord(this.LUNCH_MEAL, "2:35 pm");
            AddDefaultScheduleRecord(this.EVENING_MEAL, "8:32 pm");
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        private void AddDefaultScheduleRecord(string desc, string timeOfDay)
        {
            if (_db.DefaultSchedules.Where(d => d.Description == desc).Count() == 0)
            {
                DefaultSchedule s = new DefaultSchedule()
                {
                    Description = desc,
                    DefaultScheduleId = Guid.NewGuid(),
                    DefaultTimeOfDayToSend = DateTime.Parse(timeOfDay),
                    IsActive = true,
                    MealTypeId = this.FindMealType(desc).MealTypeId,
                    ReferenceText = desc,
                    TranslationXML = this.CreateSeedXMLForTranslations(desc)
                };

                s.AddDayOfWeek(_db.DaysOfWeek);
                _db.DefaultSchedules.Add(s);
            }
        }

        private MealType FindMealType(string description)
        {
            IQueryable<MealType> qMt =
                from m in _db.MealTypes
                where m.ReferenceText == description
                select m;

            return qMt.First();
        }

        private string CreateSeedXMLForTranslations(string phrase)
        {
            TranslatableEntity entity = new TranslatableEntity();
            entity.SetTranslation(GERMAN_ID, phrase + " (GE)");
            entity.SetTranslation(SPANISH_ID, phrase + " (SP)");
            entity.SetTranslation(FRENCH_ID, phrase + " (FR)");

            return entity.TranslationXML;
        }

       
    }
}
