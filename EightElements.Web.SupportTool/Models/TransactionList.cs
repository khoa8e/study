using System;
using System.Linq;
using System.Collections.Generic;
using EightElements.Web.SupportTool.Domain.Models;
using EightElements.Web.SupportTool.Helpers;

namespace EightElements.Web.SupportTool.Model
{
    public class TransactionList
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Msisdn { get; private set; }
        public string GameTitle { get; private set; }
        public int PortalId { get; set; }
        public PaginatedList<Transaction> PaginatedList { get; private set; }

        public TransactionList(PaginatedList<Transaction> paginatedList, 
                               DateTime startDate, 
                               DateTime endDate, 
                               string msisdn, 
                               string gameTitle)
        {
            StartDate = startDate;
            EndDate = endDate;
            Msisdn = msisdn;
            GameTitle = gameTitle;
            PaginatedList = paginatedList;
        }
    }
}