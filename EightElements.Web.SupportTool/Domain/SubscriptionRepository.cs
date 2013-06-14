using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Web.SupportTool.Domain.Models;
using System.Data.SqlClient;
using log4net;
//using EightElements.Web.SupportTool.DAL;

namespace EightElements.Web.SupportTool.Domain
{
    public class SubscriptionRepository
    {       
        #region Constructor
        public SubscriptionRepository()
        {
            _dbContext = new DbContext(Settings.DatabaseConnectionString);
        }
        #endregion

        #region Pulbic Methods
        public IQueryable<Subscriber> GetSubscriber(string msisdn, int portalId)
        {
            msisdn = msisdn == null ? string.Empty : msisdn;

            IQueryable<Subscriber> result = null;
            try
            {
                result = _dbContext.GetManyRecords<Subscriber>("SupportTool_GetSubscriber",
                                                               msisdn,
                                                               portalId).AsQueryable();
            }
            catch (Exception exp)
            {
                log.Error("Get subscriber", exp);
            }
            return result;
        }

        public IList<SubscriptionActivity> GetSubscriptionActivity(string msisdn, int portalId)
        {
            msisdn = msisdn == null ? string.Empty : msisdn;

            List<SubscriptionActivity> result = new List<SubscriptionActivity>();
            try
            {
                result = _dbContext.GetManyRecords<SubscriptionActivity>(
                                    "SupportTool_GetSubscriptionActivity",
                                    msisdn,
                                    portalId).ToList();
            }
            catch (Exception exp)
            {
                log.Error("Get Subscription Activity", exp);
            }       
            //return            
            return result;
        }

        public void CancelSubscription(int subscriptionID, string currency, string remark)
        {
            try
            {
                _dbContext.ExecuteQuery("SupportTool_CancelSubscription",
                                        subscriptionID,
                                        DateTime.Now,
                                        currency,
                                        remark);              
            }
            catch (Exception exp)
            {
                log.Error("Can subscription", exp);
            }                
        }

        #endregion

        #region Fields
        private readonly DbContext _dbContext;
        private static readonly ILog log = LogManager.GetLogger(typeof(SubscriptionRepository));
        #endregion
    }
}
