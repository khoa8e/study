using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Web.SupportTool.Domain.Models;
using System.Web.Mvc;
using EightElements.Web.SupportTool.Helpers;

namespace EightElements.Web.SupportTool.Model
{
    public class SubscriptionHistoryList
    {
        public SubscriptionHistoryList()
        {
            AvailablePortals = new List<SelectListItem>();
        }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public int PortalId { get; set; }

        public PaginatedList<SubscriptionHistory> PaginatedList { get; set; }

        public IList<SelectListItem> AvailablePortals { get; set; }
        
    }
}