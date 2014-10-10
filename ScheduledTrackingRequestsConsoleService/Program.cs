using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using UGitFit.TrackingDomain;
using System.Net;
using System.IO;


namespace ScheduledTrackingRequestsConsoleService
{
    class Program
    {
        static Thread _loopThread;
        static BsmtServer.ugfServiceSoapClient _ugfService = new BsmtServer.ugfServiceSoapClient();
        static UGitFit.TrackingDomain.uGitFitDefaultHandler _smsHandler = new UGitFit.TrackingDomain.uGitFitDefaultHandler();

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Looping Thread to send texts now.");

            _loopThread = new Thread(new ThreadStart(Loop));
            _loopThread.Start();

            Console.WriteLine("Finished starting thread.  App is running...");
            Console.WriteLine("Press anykey to terminate");

            Console.ReadKey();
        }


        static void Loop()
        {
            try
            {
                //DomainLogic logic = new DomainLogic(new UGitFit.DAL.UGFContext());

                while (true)
                {
                    ProcessTextsToSend();
                    Thread.Sleep(new TimeSpan(0, 0, 1, 0, 0));
                }
            }
            catch (Exception exp)
            {
                try
                {
                    Console.WriteLine(string.Format("{0}\n\n{1}\n\n", exp.Message, exp.ToString()));
                }
                catch { }
            }

        }


        private static void ProcessTextsToSend()
        {
            string userName = "uGitFitServiceCaller";
            string pwd = "P@ssw0rd!2E";
           
            BsmtServer.TextToSend[] sendList = _ugfService.RetrieveTextToSend(userName, pwd);

            string logMsg = sendList.Count() > 0 ? string.Format("TxtToSend: {0}. ", sendList.Count()) : ".";
            
            foreach (BsmtServer.TextToSend txtToSend in sendList)
            {
                logMsg += _smsHandler.SendUserText(new UGitFit.Model.Structures.TextToSend() { CurrentLocalTime = txtToSend.CurrentLocalTime, FirstName = txtToSend.FirstName, LastName = txtToSend.LastName, IsDebugNumber = txtToSend.IsDebugNumber, LangaugeId = txtToSend.LangaugeId, LocalTimeToSend = txtToSend.LocalTimeToSend, PersonId = txtToSend.PersonId, TranslatedText = txtToSend.TranslatedText, PhoneNumber = txtToSend.PhoneNumber, ReferenceText = txtToSend.ReferenceText, UserScheduleId = txtToSend.UserScheduleId }) + "; ";
                if (logMsg.Contains("error"))
                    break;

                _ugfService.CreateTrackingRequest(userName, pwd, txtToSend.CurrentLocalTime, txtToSend.PersonId, txtToSend.TranslatedText, txtToSend.UserScheduleId);
            }

            if (logMsg == ".")
                Console.Write(logMsg);
            else
                Console.Write(string.Format("\n\r{0}\n\r", logMsg));

        }
    }
}
