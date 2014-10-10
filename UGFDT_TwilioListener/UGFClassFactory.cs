using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UGitFit.TrackingDomain;
using UGitFit.Model.Interfaces;
using UGitFit.DAL;

namespace UGFDT_TwilioListener
{
    public class UGFClassFactory
    {
        public IRepository CreateRepository()
        {
            return new UGFContext();
        }

        public DomainLogic CreateUGFDomainMgr()
        {
            return new DomainLogic(this.CreateRepository());
        }
    }
}