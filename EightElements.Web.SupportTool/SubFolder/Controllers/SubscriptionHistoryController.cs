using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EightElements.Web.SupportTool.Domain.Models;
using EightElements.Web.SupportTool.Helpers;
using EightElements.Web.SupportTool.Domain;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using EightElements.Web.SupportTool.Model;

namespace EightElements.Web.SupportTool.Controllers
{
    public class SubscriptionHistoryController : Controller
    {
        #region Methods
        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Index(DateTime startDate, DateTime endDate, string portal, int? page)
        {
            return View(GetSubscriptionHistory(portal, startDate, endDate, page.Value));
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            DateTime startDate;
            DateTime endDate;
            if (String.IsNullOrEmpty(collection["startDate"]) || 
                !DateTime.TryParseExact(collection["startDate"], "MM/dd/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                startDate = new DateTime(2011, 08, 01);
            }

            if (String.IsNullOrEmpty(collection["endDate"]) || 
                !DateTime.TryParseExact(collection["endDate"], "MM/dd/yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                endDate = DateTime.Now;
            }            

            return View(GetSubscriptionHistory(collection["portal"], startDate, endDate, 0));
        }

        public ActionResult DownloadExcel(DateTime startDate, DateTime endDate, string portal)
        {
            int portalId = 0;
            int.TryParse(portal, out portalId);
            SubscriptionHistoryRepository   subscriptionHistoryRepository = 
                                            new SubscriptionHistoryRepository();
            //get data from database
            var subscriptionHistory = subscriptionHistoryRepository.GetSubscriptionHistory
                                                                    (portalId, 
                                                                    startDate, 
                                                                    endDate);
            
            FileInfo template = new FileInfo(
                                    Server.MapPath("~/ExcelTemplate/8eSieugameClubreport.xlsx"));

            using (ExcelPackage pck = new ExcelPackage(template, true))
            {
                ExcelWorksheet wsDaily = pck.Workbook.Worksheets["8e Sieugame-Daily"];
                int startRow = 3;
                foreach (SubscriptionHistory subHistory in subscriptionHistory)
                {
                    int columnIndex = 1;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory
                                                                   .DateLogged
                                                                   .DayOfWeek
                                                                   .ToString();
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory
                                                                   .DateLogged
                                                                   .ToString("MM/dd/yyyy");
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Visit;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Active;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Billed;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.New;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Pending;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Renewals;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Suspended;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Total;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Canceled;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Unsubscribed;
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.Downloads;
                    columnIndex++;//blank column
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.PaidDownloadRevenue;
                    columnIndex++;//blank column
                    wsDaily.Cells[startRow, columnIndex++].Value = subHistory.TurnoverAmount;
                    startRow++;
                }
                Response.Clear();
                pck.SaveAs(Response.OutputStream);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", 
                                    "attachment; filename=8eSieugameClubreport.xlsx");
            }

            return new EmptyResult();
        }
        #endregion Methods

        #region Private methods
        private SubscriptionHistoryList GetSubscriptionHistory(string portal, 
                                                               DateTime startDate, 
                                                               DateTime endDate, 
                                                               int? page)
        {
            int portalId = 0;
            int.TryParse(portal, out portalId);            
            if (page == null) page = 0;

            SubscriptionHistoryRepository subscriptionHistoryRepository = 
                                          new SubscriptionHistoryRepository();

            var subscriptionHistory = subscriptionHistoryRepository.GetSubscriptionHistory
                                                                    (portalId, 
                                                                    startDate, 
                                                                    endDate);

            var paginatedSubscriptionHistory = new PaginatedList<SubscriptionHistory>
                                                   (subscriptionHistory, 
                                                   page.Value, 
                                                   Settings.PageSize);

            SubscriptionHistoryList model = new SubscriptionHistoryList();
            model.StartDate = startDate;
            model.EndDate = endDate;
            model.PortalId = portalId;
            model.PaginatedList = paginatedSubscriptionHistory;
            return model;
        }
        #endregion Private methods      
    }
}
