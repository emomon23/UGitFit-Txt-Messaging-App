// -----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity;
    using UGitFit.Model;
    using UGitFit.Model.Structures;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IRepository : IDisposable
    {
        DbSet<Language> Languages { get; set; }
        DbSet<UGitFit.Model.TimeZone> TimeZones { get; set; }
        DbSet<ClientStatus> ClientStatuses { get; set; }
        DbSet<UGitFit.Model.DayOfWeek> DaysOfWeek { get; set; }
        DbSet<BusinessPartner> BusinessPartners { get; set; }
        DbSet<DefaultSchedule> DefaultSchedules { get; set; }
        DbSet<MealType> MealTypes { get; set; }
        DbSet<PartnerClient> PartnerClients { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<PhoneNumber> PhoneNumbers { get; set; }
        DbSet<SuspendedReason> SuspendedReasons { get; set; }
        DbSet<UserSchedule> UserSchedules { get; set; }
        DbSet<UGFTextMessage> SystemTexts { get; set; }
        DbSet<TrackingRequest> TrackingReqeusts { get; set; }
        DbSet<TrackingResponse> TrackingResponses { get; set; }
        DbSet<TimezoneAreacode> AreaCodes { get; set; }
        DbSet<ExerciseRecord> Exercise { get; set; }
        DbSet<NutrtionHistoryItem> NutritionHistory { get; set; }
        DbSet<WeightHistoryItem> WeightHistory { get; set; }

        ClientStatus FindClientStatusObject(string clientStatusId);
        Person FindPersonByPhoneNumber(string phoneNumber);
        Language GetLanguage(string name);
        TrackingRequest GetLastTrackingRequest(Guid personId);
        TrackingRequest GetLastTrackingRequest(string phoneNumber);

        UGitFit.Model.TimeZone FindTimezoneByState(string state);
        UGitFit.Model.TimeZone FindTimezoneByReferenceText(string referenceText);
        UGitFit.Model.TimeZone FindTimezoneByAreaCode(int areaCode);

        void UpdateUnknownTimezones(string state, int timezoneId);
        void ExecuteEmbeddedSQL(string sql);

        TimezoneAreacode FindAreaCode(int areaCode);

        List<TextToSend> RetrieveNextScheduledTextsToSend();

        MealType GetMealType(Guid TrackingRequestId);

        UGitFit.Model.TimeZone GetTimeZone(string name);
        
        int SaveChanges();

        string DataSource { get; }

        string TRIALEXPIRED_TXT_MSG { get; }
       
        string PREMATURE_TXT_MSG { get; }
        
        string REACTIVEATED_TXT_MSG { get; }
       

        string TRACK_ME_TXT_MSG { get; }
      

        string ACCT_DELETED_TXT_MSG { get; }
       

        string DATA_SAVED_TXT_MSG { get; }
       
        string SUSPENDED_TXT_MSG { get; }
       

        string SPAM_TXT_MSG { get; }
     
    }
}
