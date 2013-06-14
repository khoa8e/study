using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Web.SupportTool.Domain.Models;
using log4net;

namespace EightElements.Web.SupportTool.Domain
{
    public class TransactionRepository
    {
       
        #region Constructor
        public TransactionRepository()
        {
            _dbContext = new DbContext(Settings.DatabaseConnectionString);
        }
        #endregion

        #region Public Methods
        public IList<Transaction> GetTransactions(DateTime startDate,
                                                  DateTime endDate,
                                                  string msisdn,
                                                  string gameTitle,
                                                  int portalId)
        {
            //validate input
            msisdn = msisdn == null ? string.Empty : msisdn;
            gameTitle = gameTitle == null ? string.Empty : gameTitle;
            List<Transaction> result = new List<Transaction>();

            try
            {
                result = _dbContext.GetManyRecords<Transaction>("SupportTool_GetTransactions",
                                                                msisdn,
                                                                gameTitle,
                                                                portalId,
                                                                startDate,
                                                                endDate).ToList();
            }
            catch (Exception exp)
            {
                log.Error("Get transaction", exp);
            }
            //return
            return result;
        }
        #endregion

        #region Fields
        private readonly IDbContext _dbContext;
        private static readonly ILog log = LogManager.GetLogger(typeof(TransactionRepository));
        #endregion
    }
}
