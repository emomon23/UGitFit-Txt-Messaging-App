using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UGitFit.DAL
{
    public class MySQL_UGitFitStatements
    {
        public static string DropAndCreateTablesSQLStatement
        {
            get
            {
                return @"-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.6.10 - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2013-04-02 10:54:08
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping structure for table ugitfit.businesspartners
DROP TABLE IF EXISTS `businesspartners`;
CREATE TABLE IF NOT EXISTS `businesspartners` (
  `BusinessPartnerId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `BusinessPartnerName` varchar(100) DEFAULT NULL,
  `DateCreated` datetime NOT NULL,
  `GroupTag` varchar(50) DEFAULT NULL,
  `ContactName` varchar(50) DEFAULT NULL,
  `ContactEmail` varchar(100) DEFAULT NULL,
  `ForwardDataEmailAddress` varchar(200) DEFAULT NULL,
  `ForwardDataWebServiceURL` varchar(200) DEFAULT NULL,
  `DateDataLastForwardedToBusinessPartner` datetime DEFAULT NULL,
  PRIMARY KEY (`BusinessPartnerId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.clientmentorpersons
DROP TABLE IF EXISTS `clientmentorpersons`;
CREATE TABLE IF NOT EXISTS `clientmentorpersons` (
  `ClientMentor_ClientMentorId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Person_PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`ClientMentor_ClientMentorId`,`Person_PersonId`),
  KEY `ClientMentor_Clients_Target` (`Person_PersonId`),
  CONSTRAINT `ClientMentor_Clients_Source` FOREIGN KEY (`ClientMentor_ClientMentorId`) REFERENCES `clientmentors` (`ClientMentorId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `ClientMentor_Clients_Target` FOREIGN KEY (`Person_PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.clientmentors
DROP TABLE IF EXISTS `clientmentors`;
CREATE TABLE IF NOT EXISTS `clientmentors` (
  `ClientMentorId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `ClientMentorName` varchar(100) DEFAULT NULL,
  `ClientMentorSearchKey` varchar(10) DEFAULT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY (`ClientMentorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.clientstatus
DROP TABLE IF EXISTS `clientstatus`;
CREATE TABLE IF NOT EXISTS `clientstatus` (
  `ClientStatusId` varchar(50) NOT NULL,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`ClientStatusId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.daysofweek
DROP TABLE IF EXISTS `daysofweek`;
CREATE TABLE IF NOT EXISTS `daysofweek` (
  `DayOfWeekId` varchar(20) NOT NULL,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`DayOfWeekId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.defaultscheduledayofweeks
DROP TABLE IF EXISTS `defaultscheduledayofweeks`;
CREATE TABLE IF NOT EXISTS `defaultscheduledayofweeks` (
  `DefaultSchedule_DefaultScheduleId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DayOfWeek_DayOfWeekId` varchar(20) NOT NULL,
  PRIMARY KEY (`DefaultSchedule_DefaultScheduleId`,`DayOfWeek_DayOfWeekId`),
  KEY `DefaultSchedule_DaysToSend_Target` (`DayOfWeek_DayOfWeekId`),
  CONSTRAINT `DefaultSchedule_DaysToSend_Source` FOREIGN KEY (`DefaultSchedule_DefaultScheduleId`) REFERENCES `defaultschedules` (`DefaultScheduleId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `DefaultSchedule_DaysToSend_Target` FOREIGN KEY (`DayOfWeek_DayOfWeekId`) REFERENCES `daysofweek` (`DayOfWeekId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.defaultschedules
DROP TABLE IF EXISTS `defaultschedules`;
CREATE TABLE IF NOT EXISTS `defaultschedules` (
  `DefaultScheduleId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DefaultTimeOfDayToSend` datetime NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `MealTypeId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `PeriodIntervalInHrs` int(11) NOT NULL,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`DefaultScheduleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.exerciserecords
DROP TABLE IF EXISTS `exerciserecords`;
CREATE TABLE IF NOT EXISTS `exerciserecords` (
  `ExerciseRecordId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DateCreated` datetime NOT NULL,
  `ExcerciseNote` varchar(500) DEFAULT NULL,
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `CaloriesBurned` int(11) DEFAULT NULL,
  PRIMARY KEY (`ExerciseRecordId`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `ExerciseRecord_Person` FOREIGN KEY (`PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.languages
DROP TABLE IF EXISTS `languages`;
CREATE TABLE IF NOT EXISTS `languages` (
  `LanguageId` int(11) NOT NULL AUTO_INCREMENT,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`LanguageId`),
  UNIQUE KEY `LanguageId` (`LanguageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.mealtypes
DROP TABLE IF EXISTS `mealtypes`;
CREATE TABLE IF NOT EXISTS `mealtypes` (
  `MealTypeId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`MealTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.nutrtionhistoryitems
DROP TABLE IF EXISTS `nutrtionhistoryitems`;
CREATE TABLE IF NOT EXISTS `nutrtionhistoryitems` (
  `NutritionHistoryId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `MealText` varchar(300) DEFAULT NULL,
  `MealTypeId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DateCreate` datetime NOT NULL,
  `Cost` decimal(18,2) DEFAULT NULL,
  `SurgarGrams` int(11) DEFAULT NULL,
  `FatGrams` int(11) DEFAULT NULL,
  `CarbGrams` int(11) DEFAULT NULL,
  `Calories` int(11) DEFAULT NULL,
  PRIMARY KEY (`NutritionHistoryId`),
  KEY `PersonId` (`PersonId`),
  KEY `MealTypeId` (`MealTypeId`),
  CONSTRAINT `NutrtionHistoryItem_MealType` FOREIGN KEY (`MealTypeId`) REFERENCES `mealtypes` (`MealTypeId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `NutrtionHistoryItem_Person` FOREIGN KEY (`PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.partnerclients
DROP TABLE IF EXISTS `partnerclients`;
CREATE TABLE IF NOT EXISTS `partnerclients` (
  `PartnerClientId` int(11) NOT NULL AUTO_INCREMENT,
  `MemberIdentifier` varchar(50) DEFAULT NULL,
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `BusinessPartnerId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DateDataLastSentToBusinessPartner` datetime DEFAULT NULL,
  PRIMARY KEY (`PartnerClientId`),
  UNIQUE KEY `PartnerClientId` (`PartnerClientId`),
  KEY `BusinessPartnerId` (`BusinessPartnerId`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `PartnerClient_BusinessPartner` FOREIGN KEY (`BusinessPartnerId`) REFERENCES `businesspartners` (`BusinessPartnerId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `PartnerClient_Person` FOREIGN KEY (`PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.people
DROP TABLE IF EXISTS `people`;
CREATE TABLE IF NOT EXISTS `people` (
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `EmailAddress` varchar(150) DEFAULT NULL,
  `ResumedDate` varchar(50) DEFAULT NULL,
  `RegistrationCode` varchar(20) DEFAULT NULL,
  `ForwardDataToUsersEmail` tinyint(1) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `LanguageId` int(11) NOT NULL,
  `TimeZoneId` int(11) NOT NULL,
  `ClientStatusId` varchar(50) DEFAULT NULL,
  `PriorStatusId` varchar(20) DEFAULT NULL,
  `ReasonForSuspensionId` varchar(50) DEFAULT NULL,
  `ConsectiveSpamCount` int(11) NOT NULL,
  `ReplyToSMS` tinyint(1) NOT NULL,
  `DateSMSLastSentToNumber` datetime DEFAULT NULL,
  `DateSMSLastRecieveFromNumber` datetime DEFAULT NULL,
  `TodaysSMSReceivedCount` int(11) NOT NULL,
  `TodaysSMSSentCount` int(11) NOT NULL,
  PRIMARY KEY (`PersonId`),
  KEY `LanguageId` (`LanguageId`),
  KEY `TimeZoneId` (`TimeZoneId`),
  KEY `ClientStatusId` (`ClientStatusId`),
  KEY `ReasonForSuspensionId` (`ReasonForSuspensionId`),
  CONSTRAINT `Person_Language` FOREIGN KEY (`LanguageId`) REFERENCES `languages` (`LanguageId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `Person_Status` FOREIGN KEY (`ClientStatusId`) REFERENCES `clientstatus` (`ClientStatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Person_SuspendedReason` FOREIGN KEY (`ReasonForSuspensionId`) REFERENCES `suspendedreasons` (`SuspendedReasonId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Person_TimeZone` FOREIGN KEY (`TimeZoneId`) REFERENCES `timezones` (`TimeZoneId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.phonenumbers
DROP TABLE IF EXISTS `phonenumbers`;
CREATE TABLE IF NOT EXISTS `phonenumbers` (
  `Digits` varchar(20) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `IsDebugNumber` tinyint(1) NOT NULL,
  PRIMARY KEY (`Digits`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `PhoneNumber_Person` FOREIGN KEY (`PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.suspendedreasons
DROP TABLE IF EXISTS `suspendedreasons`;
CREATE TABLE IF NOT EXISTS `suspendedreasons` (
  `SuspendedReasonId` varchar(50) NOT NULL,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`SuspendedReasonId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.timezoneareacodes
DROP TABLE IF EXISTS `timezoneareacodes`;
CREATE TABLE IF NOT EXISTS `timezoneareacodes` (
  `TimezoneAreaCodeId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `AreaCode` int(11) DEFAULT NULL,
  `StateOrProvice` longtext,
  `City` longtext,
  `DateCreated` datetime NOT NULL,
  `TimeZoneId` int(11) NOT NULL,
  PRIMARY KEY (`TimezoneAreaCodeId`),
  KEY `TimeZoneId` (`TimeZoneId`),
  CONSTRAINT `TimezoneAreacode_TimeZone` FOREIGN KEY (`TimeZoneId`) REFERENCES `timezones` (`TimeZoneId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.timezones
DROP TABLE IF EXISTS `timezones`;
CREATE TABLE IF NOT EXISTS `timezones` (
  `TimeZoneId` int(11) NOT NULL AUTO_INCREMENT,
  `CurrentUTCOffset_InHours` double NOT NULL,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`TimeZoneId`),
  UNIQUE KEY `TimeZoneId` (`TimeZoneId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.trackingrequests
DROP TABLE IF EXISTS `trackingrequests`;
CREATE TABLE IF NOT EXISTS `trackingrequests` (
  `TrackingRequestId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `UserScheduleId` char(36) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DateSent` datetime NOT NULL,
  `TextSent` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`TrackingRequestId`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `TrackingRequest_Person` FOREIGN KEY (`PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.trackingresponses
DROP TABLE IF EXISTS `trackingresponses`;
CREATE TABLE IF NOT EXISTS `trackingresponses` (
  `TrackingResponseId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `ResponseText` varchar(500) DEFAULT NULL,
  `TrackingRequestId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DateReceived` datetime NOT NULL,
  PRIMARY KEY (`TrackingResponseId`),
  KEY `TrackingRequestId` (`TrackingRequestId`),
  CONSTRAINT `TrackingResponse_TrackingRequest` FOREIGN KEY (`TrackingRequestId`) REFERENCES `trackingrequests` (`TrackingRequestId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.ugftextmessages
DROP TABLE IF EXISTS `ugftextmessages`;
CREATE TABLE IF NOT EXISTS `ugftextmessages` (
  `TextMessageId` varchar(50) NOT NULL,
  `ReferenceText` varchar(256) DEFAULT NULL,
  `TranslationXML` longtext,
  PRIMARY KEY (`TextMessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.userscheduledayofweeks
DROP TABLE IF EXISTS `userscheduledayofweeks`;
CREATE TABLE IF NOT EXISTS `userscheduledayofweeks` (
  `UserSchedule_UserScheduleId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `DayOfWeek_DayOfWeekId` varchar(20) NOT NULL,
  PRIMARY KEY (`UserSchedule_UserScheduleId`,`DayOfWeek_DayOfWeekId`),
  KEY `UserSchedule_DaysToSend_Target` (`DayOfWeek_DayOfWeekId`),
  CONSTRAINT `UserSchedule_DaysToSend_Source` FOREIGN KEY (`UserSchedule_UserScheduleId`) REFERENCES `userschedules` (`UserScheduleId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `UserSchedule_DaysToSend_Target` FOREIGN KEY (`DayOfWeek_DayOfWeekId`) REFERENCES `daysofweek` (`DayOfWeekId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.userschedules
DROP TABLE IF EXISTS `userschedules`;
CREATE TABLE IF NOT EXISTS `userschedules` (
  `UserScheduleId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `ScheduleId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `OverrideUTCTimeOfDayToSend` datetime NOT NULL,
  `OverridePeriodIntervalInHrs` int(11) NOT NULL,
  `SuspendStartDate` datetime NOT NULL,
  `SuspendEndDate` datetime NOT NULL,
  PRIMARY KEY (`UserScheduleId`),
  KEY `ScheduleId` (`ScheduleId`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `UserSchedule_Person` FOREIGN KEY (`PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `UserSchedule_SourceSchedule` FOREIGN KEY (`ScheduleId`) REFERENCES `defaultschedules` (`DefaultScheduleId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.weighthistoryitems
DROP TABLE IF EXISTS `weighthistoryitems`;
CREATE TABLE IF NOT EXISTS `weighthistoryitems` (
  `WeightHistoryId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `Weight` decimal(18,2) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `PersonId` char(36) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`WeightHistoryId`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `WeightHistoryItem_Person` FOREIGN KEY (`PersonId`) REFERENCES `people` (`PersonId`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table ugitfit.__migrationhistory
DROP TABLE IF EXISTS `__migrationhistory`;
CREATE TABLE IF NOT EXISTS `__migrationhistory` (
  `MigrationId` varchar(255) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
";
            }
        }

    }
}
