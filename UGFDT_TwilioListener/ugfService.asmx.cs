using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UGitFit.Model;
using UGitFit.DAL;
using UGitFit.Model.Interfaces;
using UGitFit.Model.Structures;
using UGitFit.TrackingDomain;

namespace UGFDT_TwilioListener
{
    /// <summary>
    /// Summary description for ugfService
    /// </summary>
    [WebService(Namespace = "http://uGitFit.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ugfService : System.Web.Services.WebService
    {
        IRepository _repository;

        public ugfService()
        {
            _repository = new UGFClassFactory().CreateRepository();
        }

        [WebMethod]
        public List<TextToSend> RetrieveTextToSend(string userName, string pwd)
        {
            if (ValidateUsernamePwd(userName, pwd))
            {
               return _repository.RetrieveNextScheduledTextsToSend();
            }

            return new List<TextToSend>();
        }

        [WebMethod]
        public string CreateTrackingRequest(string userName, string pwd, DateTime clientLocalTime, Guid personId, string textSent, Guid userScheduleId)
        {
            if (ValidateUsernamePwd(userName, pwd))
            {
                try
                {
                    TrackingRequest tr = new TrackingRequest() { DateSent = clientLocalTime, PersonId = personId, TextSent = textSent, UserScheduleId = userScheduleId };
                    _repository.TrackingReqeusts.Add(tr);
                    _repository.SaveChanges();
                    return "OK";
                }
                catch (Exception exp)
                {
                    return ExceptionToString(exp);
                }
            }

            return "Invalid usename and/or pwd";
                               
        }

        private bool ValidateUsernamePwd(string userName, string pwd)
        {
            return (userName == "uGitFitServiceCaller" && pwd == "P@ssw0rd!2E");
        }

        private string ExceptionToString(Exception exp)
        {
            string rtnVal = exp.Message;
            Exception expInner = exp.InnerException;

            while (expInner != null)
            {
                rtnVal += "\n\n" + expInner.Message;
                expInner = expInner.InnerException;
            }

            return rtnVal;
        }
    }
}
