using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UGitFit;
using UGitFit.DAL;
using UGitFit.Model;
using UGitFit.TrackingDomain;

namespace TestProject
{
   // [TestClass]
    public class TimeZoneTest
    {
        UGitFit.DAL.UGFContext _db = (UGitFit.DAL.UGFContext)ClassFactory.CreateRepository();
        UGitFit.TrackingDomain.DomainLogic _logic = ClassFactory.CreateDomainMgr();
        List<TestPhoneNumber> _testNumbers = null;

        Random _rnd = new Random(DateTime.Now.Millisecond);

        public TimeZoneTest()
        {
            _testNumbers = TestPhoneNumberFactory.GetTestPhoneNumbersList().Where(t => t.State == "MN").ToList();
        }

        [TestMethod]
        public void AreaCodeNotFoundInWebService_NoMatchOnState()
        {
            this.RunTimeZoneTest(true, true, null);
        }

        [TestMethod]
        public void TestEasternTimezone()
        {
            TestSpecificTimeZonePhoneNumberForSuccess("EASTERN");
        }

        [TestMethod]
        public void TestCentralTimezone()
        {
            TestSpecificTimeZonePhoneNumberForSuccess("CENTRAL");
        }

         [TestMethod]
        public void TestMountainTimeZone()
        {
            TestSpecificTimeZonePhoneNumberForSuccess("MOUNTAIN");
        }

         [TestMethod]
        public void TestPacificTimezone()
        {
            TestSpecificTimeZonePhoneNumberForSuccess("PACIFIC");
        }

         [TestMethod]
        public void TestAlaskaTimezone()
        {
            TestSpecificTimeZonePhoneNumberForSuccess("ALASKA");
        }

         [TestMethod]
        public void TestHawiiTimezone()
        {
            TestSpecificTimeZonePhoneNumberForSuccess("HAWAII - ALEUTIAN");
        }

        [TestMethod]
        public void MultipleCalls_SameAreaCode_OneRecord()
        {
            TestPhoneNumber phNbr = this.RandomNumber(false);
            RunTimeZoneTest(true, true, phNbr);
            RunTimeZoneTest(false, true, phNbr);

            Assert.IsTrue(_db.AreaCodes.Where(a => a.AreaCode == phNbr.AreaCode).Count() == 1);
        }

        [TestMethod]
        public void AreaCodeNotFoundInWebService_MultipleUnknownInDB_NoMatchOnState()
        {
            List<TestPhoneNumber> pNbrs = _testNumbers.Where(p => p.AreaCodeServiceWorks == false).ToList();
            this.ResetAreaCodes();

            foreach (TestPhoneNumber pNbr in pNbrs)
            {
                this.RunTimeZoneTest(false, true, pNbr);
            }

            Assert.IsTrue(_db.AreaCodes.Count() == pNbrs.Count);
        }

        [TestMethod]
        public void AreaCodeFoundInWebService()
        {
            this.RunTimeZoneTest(true, false, null);
        }

        [TestMethod]
        public void AC_NotFoundInWebService_ButMatchInDB_OnState()
        {
            this.RunTimeZoneTest(true, false, null);

            TestPhoneNumber nbr = this.RandomNumber(false);
            this.RunTimeZoneTest(false, false, nbr);
        }

        [TestMethod]
        public void MatchInDB_OnAreaCode()
        {
            TestPhoneNumber pNbr = this.RunTimeZoneTest(true, false, null);
            this.RunTimeZoneTest(false, false, pNbr);

            Assert.IsTrue(_db.AreaCodes.Count() == 1);
        }


        private void TestSpecificTimeZonePhoneNumberForSuccess(string timezoneToTest)
        {
            TestPhoneNumber phnNbr = TestPhoneNumberFactory.GetTestPhoneNumbersList().Where(t => t.TimeZone == timezoneToTest && t.AreaCodeServiceWorks == true).First();
            this.RunTimeZoneTest(true, false, phnNbr);
        }

        private TestPhoneNumber RunTimeZoneTest(bool resetFirst, bool shouldBeUnknown, TestPhoneNumber ph)
        {
            TestPhoneNumber result = ph;
            if (ph == null)
                result = this.RandomNumber((shouldBeUnknown == false));

            if (resetFirst)
                this.ResetAreaCodes();

            _logic.ProcessTextMessageReceived(result.Country, result.State, result.City, result.PhoneNumber, "TZ");
            this.GetTimeZone(result, shouldBeUnknown);

            return result;
        }

        private void ResetAreaCodes()
        {
            
            foreach (TimezoneAreacode ac in _db.AreaCodes)
            {
                _db.AreaCodes.Remove(ac);
            }

            _db.SaveChanges();
            
        }

        private void GetTimeZone(TestPhoneNumber phNbr, bool shouldBeUnknown)
        {
            _logic.ProcessTextMessageReceived(phNbr.Country, phNbr.State, phNbr.City, phNbr.PhoneNumber, "TZ");
            UGitFit.Model.TimeZone tz = _db.FindTimezoneByAreaCode(phNbr.AreaCode);

            Assert.IsTrue((tz.ReferenceText == "UNKNOWN") == shouldBeUnknown);
            if (!shouldBeUnknown)
            {
                Assert.IsTrue(tz.ReferenceText == phNbr.TimeZone);
            }
          

        }

        private TestPhoneNumber RandomNumber(bool worksOrNot)
        {
            List<TestPhoneNumber> tempList = _testNumbers.Where(t => t.AreaCodeServiceWorks == worksOrNot).ToList();

            return tempList[_rnd.Next(0, tempList.Count - 1)];

        }

       
    }
}
