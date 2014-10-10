using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UGitFit.DAL;
using UGitFit.Model;
using UGitFit.TrackingDomain.Interfaces;
using UGitFit.TrackingDomain;

namespace UGFDT_TwilioListener
{
    public partial class IntegrationTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //This page will display the possible commands.
            string result = "commands: createDb, createRequests, sendTrackingRequests, phone, TRCount, seedDB, connString ";

            try
            {
                if (this.GetBoolValue("connString"))
                {
                    if (this.CommandIsAllowed("connString"))
                    {
                        result = new UGFClassFactory().CreateRepository().DataSource;
                    }
                }

                if (this.GetBoolValue("createDb"))
                {
                    if (this.CommandIsAllowed("createDb"))
                    {
                        UGFSeeder.DropAndRecreateEntireDatabase();
                        result = "Database Created";
                    }
                    else
                        result = "Cannot call createDb in this environment";
                }

                if (this.GetBoolValue("seedDB"))
                {
                    if (this.CommandIsAllowed("seedDB"))
                    {
                        UGFSeeder.SeedExistingDatabase();
                        result = "Seeded";
                    }
                    else
                        result = "Cannot call this command.";
                }

                if (GetBoolValue("createRequests"))
                {
                    if (this.CommandIsAllowed("createRequests"))
                    {
                        string phoneNumber = GetValue("phone");
                        int numberOfRequests = GetIntValue("TRCount");

                        UGitFit.DAL.UGFSeeder.CreateTrackingRequests(phoneNumber, numberOfRequests);
                        result = "crateRequests done.";
                    }
                    else
                        result = "Cannot call createRequests in this environment.";
                }

                if (GetBoolValue("sendTrackingRequests"))
                {
                    if (this.CommandIsAllowed("sendTrackingRequests"))
                    {
                        UGitFit.TrackingDomain.DomainLogic logic = new DomainLogic(new UGitFit.DAL.UGFContext());
                        logic.SendScheduledTexts(null);

                        result = "Scheduled Tracking Requests Processed";
                    }
                    else
                    {
                        result = "Cannot call sendTrackingRequests in this environment";
                    }
                }
            }
            catch (Exception exp)
            {
                result = "Error: " + exp.Message + "<br/><br/>";
                Exception expInner = exp.InnerException;

                while (expInner != null)
                {
                    result += expInner.Message + "<br/><br/>";
                    expInner = expInner.InnerException;
                }
                
            }

            Response.Write(string.Format("<result>{0}</result>", result));
        }

        private bool GetBoolValue(string boolName)
        {
            return GetValue(boolName, false).ToLower() == "true"? true : false;
        }

        private int GetIntValue(string intName)
        {
            string temp = GetValue(intName, false);

            if (!temp.IsNumeric())
                throw new Exception(string.Format("Expecting query string to contain {0} that is numeric", intName));

            return temp.ToInt(0);
        }

        private string GetValue(string valueName)
        {
            return GetValue(valueName, true);
        }

        private string GetValue(string valueName, bool isRequired)
        {
            string result = Request.QueryString[valueName] != null? Request.QueryString[valueName] : string.Empty;

            if (result.isNullOrEmpty() && isRequired)
                throw new Exception(string.Format("Expecting query string to contain {0}", valueName));

            return result;
        }

        private bool CommandIsAllowed(string commandName)
        {
             string valuesAllowed = System.Configuration.ConfigurationManager.AppSettings["IntegrationCommandsAllowed"];
             return valuesAllowed.ToUpper().Contains(commandName.ToUpper());
        }
    }
}