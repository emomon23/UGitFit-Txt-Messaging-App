// -----------------------------------------------------------------------
// <copyright file="TestPhoneNumbers.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TestProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestPhoneNumber
    {
        public TestPhoneNumber()
        {
            this.Country = "US";
            this.AreaCodeServiceWorks = true;
            
        }

        public int AreaCode
        {
            get
            {
                if (PhoneNumber.Length > 3)
                {
                    return int.Parse(PhoneNumber.Substring(0, 3));
                }

                throw new Exception("TestPhoneNumber is unable to parse the AreaCode from the phone number");
            }
        }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TimeZone { get; set; }
        public bool AreaCodeServiceWorks { get; set; }
    }

    public class TestPhoneNumberFactory
    {
        private static List<TestPhoneNumber> _theList;
        private static Random _rnd = new Random(DateTime.Now.Millisecond);
        
        public static List<TestPhoneNumber> GetTestPhoneNumbersList()
        {
            if (_theList == null)
            {
                _theList = new List<TestPhoneNumber>();

                _theList.Add(new TestPhoneNumber() { City = "MINNEAPOLIS", State = "MN", PhoneNumber = "9526152342", TimeZone="CENTRAL", AreaCodeServiceWorks=false });
                _theList.Add(new TestPhoneNumber() { City = "St Paul", State = "MN", PhoneNumber = "6512345423", TimeZone = "CENTRAL", AreaCodeServiceWorks=false });
                _theList.Add(new TestPhoneNumber() { City = "Sartell", State = "MN", PhoneNumber = "3203422342", TimeZone = "CENTRAL" });
                _theList.Add(new TestPhoneNumber() { City = "Minneapolis", State = "MN", PhoneNumber = "6127244387", TimeZone = "CENTRAL" });
                _theList.Add(new TestPhoneNumber() { City = "Plymouth", State = "MN", PhoneNumber = "7633424234", TimeZone = "CENTRAL", AreaCodeServiceWorks=false });
                _theList.Add(new TestPhoneNumber() { City = "New York", State = "NY", PhoneNumber = "2125234234", TimeZone = "EASTERN" });
                _theList.Add(new TestPhoneNumber() { City = "New York", State = "NY", PhoneNumber = "3152342524", TimeZone = "EASTERN" });
                _theList.Add(new TestPhoneNumber() { City = "New York", State = "NY", PhoneNumber = "6072342342", TimeZone = "EASTERN" });
                _theList.Add(new TestPhoneNumber() { City = "Klondike", State = "AK", PhoneNumber = "9072342342", TimeZone = "ALASKA" });
                _theList.Add(new TestPhoneNumber() { City = "SomeMontanaCity", State = "MT", PhoneNumber = "4062346223", TimeZone = "MOUNTAIN" });
                _theList.Add(new TestPhoneNumber() { City = "Big Horn", State = "WY", PhoneNumber = "3076235234", TimeZone = "MOUNTAIN" });
                _theList.Add(new TestPhoneNumber() { City = "San Ramon", State = "CA", PhoneNumber = "70762346234", TimeZone = "PACIFIC" });
                _theList.Add(new TestPhoneNumber() { City = "Los Angelas", State = "CA", PhoneNumber = "5109996634", TimeZone = "PACIFIC" });
                _theList.Add(new TestPhoneNumber() { City = "San Deago", State = "CA", PhoneNumber = "8581112255", TimeZone = "PACIFIC", AreaCodeServiceWorks=false });
                _theList.Add(new TestPhoneNumber() { City = "Honolulu", State = "HI", PhoneNumber = "8087772222", TimeZone = "HAWAII - ALEUTIAN" });
                
            }

            return _theList;
        }

        public static TestPhoneNumber GetRandomPhoneNumber()
        {
            int index = _rnd.Next(0, GetTestPhoneNumbersList().Count - 1);
            return _theList[index];
        }

        public static List<TestPhoneNumber> AreaCodeWorksWithServiceFilter(bool worksOrNot)
        {
            return GetTestPhoneNumbersList().Where(t => t.AreaCodeServiceWorks == worksOrNot).ToList();
        }

        public static TestPhoneNumber GetRandomPhoneNumber(string state)
        {
            var results =
                from p in GetTestPhoneNumbersList()
                where p.State == state
                select p;

            if (results == null || results.Count() == 0)
                return null;

            int index = _rnd.Next(0, results.Count() - 1);
            return (TestPhoneNumber)results.ElementAt(index);

        }
    }
}
