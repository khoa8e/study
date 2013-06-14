using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Web.SupportTool.Domain.Models
{
    public class SubscriptionHistory
    {
        public DateTime DateLogged { get; set; }

        public int Visit { get; set; }

        public int Active { get; set; }

        public int Billed { get; set; }

        public int New { get; set; }

        public int Pending { get; set; }

        public int Renewals { get; set; }

        public int Suspended { get; set; }

        public int Total { get; set; }

        public int Canceled { get; set; }

        public int Unsubscribed { get; set; }

        public int Downloads { get; set; }

        public Decimal PaidDownloadRevenue { get; set; }

        public Decimal TurnoverAmount { get; set; }

    }
}