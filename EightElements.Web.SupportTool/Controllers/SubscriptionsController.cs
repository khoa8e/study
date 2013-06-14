using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EightElements.Web.SupportTool.Domain;
using EightElements.Web.SupportTool.Domain.Models;
using EightElements.Web.SupportTool.Helpers;
using EightElements.Web.SupportTool.Model;
using log4net;
//using EightElements.Web.SupportTool.DAL;

namespace EightElements.Web.SupportTool.Controllers
{
    public class SubscriptionsController : Controller
    {        
        #region Methods

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Index(string msisdn, int portal, int? page)
        {
            if (page == null) page = 0;
            return View(GetSubscription(msisdn, portal, page));
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string msisdn = collection["msisdn"];
            int portalId = 0;
            int.TryParse(collection["portal"], out portalId);
            string initMsisdnNumber = SessionHandler
                                      .UserInfo
                                      .Portals
                                      .Where(p => p.Id == portalId)
                                      .FirstOrDefault()
                                      .InitMsisdnNumber;

            if (!string.IsNullOrEmpty(initMsisdnNumber))
            {
                msisdn = initMsisdnNumber + msisdn;
            }

            return View(GetSubscription(msisdn, portalId, 0));
        }

        private SubscriptionList GetSubscription(string msisdn, int portalId, int? page)
        {
            if (page == null) page = 0;

            SubscriptionRepository subscriptionRepository = new SubscriptionRepository();

            var transactions = subscriptionRepository.GetSubscriptionActivity(msisdn, portalId);

            var subscriber = subscriptionRepository.GetSubscriber(msisdn, portalId).ToList();

            //var subscribersList = new List<Subscriber>(subscriber);
            var paginatedSubscriptions = new PaginatedList<SubscriptionActivity>(transactions,
                                                                                (int)page,
                                                                                Settings.PageSize);
            var model = new SubscriptionList(paginatedSubscriptions, msisdn, subscriber);
            model.PortalId = portalId;

            return model;
        }

        public ActionResult CancelSubscription(string msisdn, int portalId)
        {
            SubscriptionRepository subscriptionRepository = new SubscriptionRepository();
            Subscriber subscriber = subscriptionRepository.GetSubscriber(msisdn, portalId)
                                                          .SingleOrDefault();
            
            //get currency by Portal
            Portal currentPortal = SessionHandler
                                   .UserInfo
                                   .Portals
                                   .Where(p => p.Id == portalId)
                                   .FirstOrDefault();

            string currency = currentPortal.Currency;
            string remark = currentPortal.CancelRemark;
            //execute cancel subscription
            subscriptionRepository.CancelSubscription(subscriber.ID, currency, remark);


            string redirectURL = Request.UrlReferrer.AbsolutePath + "?msisdn=" + msisdn;
            redirectURL += "&portal=" + portalId.ToString();

            return new RedirectResult(redirectURL);
        }

        #endregion

        #region Fields
        private static readonly ILog log = LogManager.GetLogger(
                                                        typeof(SubscriptionsController));
        #endregion
    }
}
