// -----------------------------------------------------------------------
// <copyright file="RepositoryFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TestProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UGitFit.TrackingDomain.Interfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ClassFactory
    {
        public static IRepository _db;



        public static string ConnectionString = "localhost";
        
        public static IRepository CreateRepository()
        {
            if (_db == null)
                _db = new UGitFit.DAL.UGFContext();

            return _db;
        }

        public static UGitFit.TrackingDomain.DomainLogic CreateDomainMgr()
        {
            return new UGitFit.TrackingDomain.DomainLogic(CreateRepository());
        }
    }
}
