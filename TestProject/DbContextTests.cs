using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UGitFit.TrackingDomain.Interfaces;
using UGitFit.TrackingDomain;
using UGitFit.Model;
using UGitFit.DAL;
using System.Data.Entity;
using System.Data;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        UGFSeeder _seeder;
        UGitFit.DAL.UGFContext _repository;

               
        public UnitTest1()
        {
            _seeder = new UGFSeeder((ClassFactory._db == null), ClassFactory.CreateRepository());
            _repository = (UGFContext)ClassFactory.CreateRepository();
        }

        

        [TestMethod]
        public void U110_VerifyClinicStutusSave()
        {
            _seeder.SeedClientStatuses();
            Assert.IsTrue(_repository.ClientStatuses.Count() > 2);
        }

        [TestMethod]
        public void U10_VerifyEnglishLanguage()
        {
            _seeder.SeedLanguages();
            Assert.IsTrue(_repository.Languages.Count() > 3);
        }

        [TestMethod]
        public void U20_VerifyDaysOfWeekSaved()
        {
            _seeder.SeedDaysOfWeek();
            Assert.IsTrue(_repository.DaysOfWeek.Count() == 7);
        }

        [TestMethod]
        public void U90_VerifyPeopleFind()
        {
            Assert.IsTrue(_repository.FindPersonByPhoneNumber("12362344") == null);

        }

        [TestMethod]
        public void U100_VerifyPeopleSave()
        {
            string phoneNumber = "6122809917";

            Person p = _repository.FindPersonByPhoneNumber(phoneNumber);
            if (p == null)
            {
                PhoneNumber pn = new PhoneNumber() { Digits = phoneNumber, IsActive = true };
                p = new Person()
                {
                    PersonId = Guid.NewGuid(),
                    ClientStatusId = ClientStatus.PreRegisteredId,
                    Status = _repository.FindClientStatusObject(ClientStatus.PreRegisteredId),
                    EmailAddress = "memo@usinternet.com",
                    FirstName = "Mike",
                    LanguageId = 2,
                    LastName = "Emo",
                    TimeZoneId = 2,
                   
                };

                pn.PersonId = p.PersonId;
                _repository.PhoneNumbers.Add(pn);
                _repository.People.Add(p);
                _repository.SaveChanges();
            }

            Assert.IsTrue(_repository.FindPersonByPhoneNumber(phoneNumber) != null);
        }

        [TestMethod]
        public void U30_VerifyMealTypesSave()
        {
            _seeder.SeedMealTypes();
            Assert.IsTrue(_repository.MealTypes.Count() == 3);
        }

        [TestMethod]
        public void U40_VerifyTimezoneSave()
        {
            _seeder.SeedTimeZones();
            Assert.IsTrue(_repository.TimeZones.Count() > 5);
        }

        [TestMethod]
        public void U50_VerifySuspendedReasonsSave()
        {
            _seeder.SeedSuspendedReasons();
            Assert.IsTrue(_repository.SuspendedReasons.Count() == 3);
        }

        [TestMethod]
        public void U60_VerifyDefaultScheduleSave()
        {
            U30_VerifyMealTypesSave();

            _seeder.SeedDefaultSchedules();
            Assert.IsTrue(_repository.DefaultSchedules.Count() == 3);
        }

        [TestMethod]
        public void U70_VerifySystemTextsSave()
        {
            _seeder.SeedUGTTexts();
            Assert.IsTrue(_repository.SystemTexts.Count() > 1);
        }
      
    }
}
