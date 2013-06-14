using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Web.SupportTool.Domain.Models
{
    public class Subscriber
    {
        public string Msisdn { get; set; }
        public int ID { get; set; }
        public DateTime Timestamp { get; set; }
        public int Credits { get; set; }
        public string SubscriptionStatus { get; set; }
    }

    public class SubscriptionActivity
    {
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string ContentName { get; set; }
        public string ActionStatus { get; set; }
    }
}
