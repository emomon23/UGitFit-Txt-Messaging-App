using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UGitFit.DAL;
using UGitFit.Model;
using UGitFit.Model.ModelExceptions;
using UGitFit.TrackingDomain;
using UGitFit.TrackingDomain.Interfaces;
using UGitFit.TrackingDomain.TimeZoneServer;

namespace UGFDT_TwilioListener
{
    public partial class TextMsgHandler : System.Web.UI.Page
    {
        private bool IsTestWebPage = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string response = "";

            try
            {
                string body = GetValue("Body");
                string from = GetValue("From");
                string city = GetValue("FromCity");
                string state = GetValue("FromState");
                string zip = GetValue("FromZip");
                string country = GetValue("FromCountry");

                if (string.IsNullOrEmpty(body) == false && string.IsNullOrEmpty(from) == false)
                {
                    //Refactor this!
                    DomainLogic ugitFitDataTracker = new UGFClassFactory().CreateUGFDomainMgr();
                    response = ugitFitDataTracker.ProcessTextMessageReceived(country, state, city, from, body, this.IsTestWebPage);
                }
            }
            catch (Exception exp)
            {
                try
                {
                    string path = string.Format("Error: {0}", exp.Message);
                    //string.IsNullOrEmpty

                }
                catch { }//I"ll

                response = "An unexpected error occurred";
            }

            if (!string.IsNullOrEmpty(response))
            {
                int length = response.Length;

                Response.Clear();
                Response.ContentType = "text/xml";


                if (this.IsTestWebPage)
                    response += string.Format(" (text length: {0})", length);

                string twilioResponse = string.Format("<Response><Sms>{0}</Sms></Response>", response);

                Response.Write(twilioResponse);
            }
        }

        //This is a helper function, used to get a value either from the url query string (for testing purpose)
        //Or from the HTTPRequest object which is how twilio will pass data
        private string GetValue(string name)
        {
            //For testing purpose, check if the value is in the querystring of the URL 1st
            //this is for Mike and Jon to do some quick tests before actually sending a text msg vis tWilio
            string result = Request.QueryString[name];

            if (string.IsNullOrEmpty(result))
            {
                //The values IS NOT in the query string (URL), check the Request object and
                //see if twilio passed us a value for the named property
                result = Request[name];
            }
            else
            {
                this.IsTestWebPage = true;

            }

            return result;
        }

        private bool DropAndCreateDatabase
        {
            get
            {
                return (Request.QueryString["createDb"] != null && Request.QueryString["createDb"].ToLower() == "true") ? true : false;

            }
        }
    }
}