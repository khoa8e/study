using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Web.SupportTool.Domain.Models;
using EightElements.Web.SupportTool.Helpers;

namespace EightElements.Web.SupportTool.Model
{
    public class SubscriptionList
    {
        public string Msisdn { get; private set; }
        public PaginatedList<SubscriptionActivity> PaginatedList { get; private set; }
        public int PortalId { get; set; }

        public List<Subscriber> Subscriber { get; private set; }

        public SubscriptionList(PaginatedList<SubscriptionActivity> paginatedList, 
                                string msisdn, 
                                List<Subscriber> subscriber)
        {
            Msisdn = msisdn;
            PaginatedList = paginatedList;
            Subscriber = subscriber;
        }
    }
}
