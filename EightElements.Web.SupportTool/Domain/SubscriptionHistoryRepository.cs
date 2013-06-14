using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Web.SupportTool.Domain.Models;
using log4net;

namespace EightElements.Web.SupportTool.Domain
{
    public class SubscriptionHistoryRepository
    {       
        #region Constructor
        public SubscriptionHistoryRepository()
        {
            _dbContext = new DbContext(Settings.DatabaseConnectionString);
        }
        #endregion

        #region Methods

        public IList<SubscriptionHistory> GetSubscriptionHistory(int portalId, 
                                                                 DateTime startDate, 
                                                                 DateTime endDate)
        {
            List<SubscriptionHistory> result = new List<SubscriptionHistory>();
            try
            {
                result = _dbContext.GetManyRecords<SubscriptionHistory>(
                                                                "SupportTool_GetSubscriptionHistory",
                                                                portalId,
                                                                startDate,
                                                                endDate).ToList();
            }
            catch (Exception exp)
            {
                log.Error("Get subscription history", exp);
            }   

            return result;
        }      
        #endregion

        #region Fields
        private readonly DbContext _dbContext;
        private static readonly ILog log = LogManager.GetLogger(
                                                            typeof(SubscriptionHistoryRepository));
        #endregion
    }
}