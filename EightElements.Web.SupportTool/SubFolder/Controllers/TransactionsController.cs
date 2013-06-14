using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EightElements.Web.SupportTool.Domain;
using EightElements.Web.SupportTool.Domain.Models;
using EightElements.Web.SupportTool.Helpers;
using System.Globalization;
using EightElements.Web.SupportTool.Model;

namespace EightElements.Web.SupportTool.Controllers
{
    public class TransactionsController : Controller
    {
        #region Methods

        public ActionResult Index(DateTime startDate, 
                                  DateTime endDate, 
                                  string msisdn, 
                                  string gameTitle, 
                                  int portal, 
                                  int? page)
        {
            return View(GetTransactions(startDate, endDate, msisdn, gameTitle, portal, page));
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {            
            DateTime startDate;
            DateTime endDate;
            if (String.IsNullOrEmpty(collection["startDate"]) 
                || !DateTime.TryParseExact( collection["startDate"], "MM/dd/yyyy", 
                                            CultureInfo.InvariantCulture, 
                                            DateTimeStyles.None, out startDate))
            {
                startDate = new DateTime(2011, 08, 01);
            }

            if (String.IsNullOrEmpty(collection["endDate"])     //check null
                || !DateTime.TryParseExact( collection["endDate"], "MM/dd/yyyy", 
                                            CultureInfo.InvariantCulture, 
                                            DateTimeStyles.None, out endDate))
            {
                endDate = DateTime.Now;
            }
            int portalId = 0;
            int.TryParse(collection["portal"], out portalId);
            string msisdn = collection["msisdn"];
            string gameTitle = collection["gameTitle"];
            return View(GetTransactions(startDate, endDate, msisdn, gameTitle, portalId, 0));
        }

        public ActionResult Search()
        {
            return View();
        }
        #endregion

        #region Private methods
        private TransactionList GetTransactions(DateTime startDate, 
                                                DateTime endDate, 
                                                string msisdn, 
                                                string gameTitle, 
                                                int portalId, 
                                                int? page)
        {            
            if (page == null) page = 0;

            TransactionRepository transactionRepository = new TransactionRepository();
            var transactions = transactionRepository.GetTransactions(startDate, 
                                                                     endDate, 
                                                                     msisdn, 
                                                                     gameTitle, 
                                                                     portalId);

            var paginatedTransactions = new PaginatedList<Transaction>(transactions, 
                                                                       (int)page, 
                                                                       Settings.PageSize);

            // Khoa.Nguyen: Get GetInstallLastNotify in Store Procedure
            //foreach (Transaction trx in paginatedTransactions)
            //    trx.InstallStatus = transactionRepository.GetInstallLastNotify(trx.ID);

            var model = new TransactionList(paginatedTransactions, 
                                            startDate, 
                                            endDate, 
                                            msisdn, 
                                            gameTitle);
            model.PortalId = portalId;
            //return
            return model;
        }
        #endregion
    }
}
