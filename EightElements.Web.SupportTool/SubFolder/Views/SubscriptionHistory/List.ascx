<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EightElements.Web.SupportTool.Model.SubscriptionHistoryList>" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Domain.Models" %>
<% 
    if (Model.PaginatedList.Count > 0)
    {
%>
<table>
    <td>
    </td>
    <td>
    </td>
    <td>
        Visit
    </td>
    <td>
        Active
    </td>
    <td>
        Billed
    </td>
    <td>
        New
    </td>
    <td>
        Pending
    </td>
    <td>
        Renewals
    </td>
    <td>
        Suspended
    </td>
    <td>
        Total
    </td>
    <td>
        Canceled
    </td>
    <td>
        Unsubscribed
    </td>
    <td>
        Downloads
    </td>
    <td>
        Paid Download Revenue
    </td>
    <td>
        TurnoverAmount
    </td>
    <% 
        foreach (SubscriptionHistory subHistory in Model.PaginatedList)
        {
    %><tr>
        <td>
            <%=subHistory.DateLogged.DayOfWeek.ToString()%>
        </td>
        <td>
            <%=subHistory.DateLogged.ToString("MM/dd/yyyy")%>
        </td>
        <td>
            <%=subHistory.Visit%>
        </td>
        <td>
            <%=subHistory.Active%>
        </td>
        <td>
            <%=subHistory.Billed%>
        </td>
        <td>
            <%=subHistory.New%>
        </td>
        <td>
            <%=subHistory.Pending%>
        </td>
        <td>
            <%=subHistory.Renewals%>
        </td>
        <td>
            <%=subHistory.Suspended%>
        </td>
        <td>
            <%=subHistory.Total%>
        </td>
        <td>
            <%=subHistory.Canceled%>
        </td>
        <td>
            <%=subHistory.Unsubscribed%>
        </td>
        <td>
            <%=subHistory.Downloads%>
        </td>
        <td>            
            <%=String.Format("{0:N0}", subHistory.PaidDownloadRevenue)%>
        </td>
        <td>        
            <%=String.Format("{0:N0}", subHistory.TurnoverAmount)%>
        </td>
    </tr>
    <%
        }
    %>
</table>
<section id="pages">
    <%
        if (Model.PaginatedList.HasPreviousPage)
        {
    %><%=Html.ActionLink("Prev", "Index", new { startDate = Model.StartDate, endDate = Model.EndDate, portal = Model.PortalId,  page = (Model.PaginatedList.PageIndex - 1) })%><%
        }
        else
        {
    %><span class="pn">Prev</span><%
        }
        int firstPage = 0;
        int lastPage = 0;
        if ((Model.PaginatedList.PageIndex - 2) > 0)
        {
            firstPage = (Model.PaginatedList.PageIndex - 2);
        }
        if ((firstPage + 5) > Model.PaginatedList.TotalPages)
        {
            lastPage = Model.PaginatedList.TotalPages;
            if ((lastPage - 5) > 0)
                firstPage = lastPage - 5;
        }
        else
            lastPage = firstPage + 5;
        for (int pageNumber = firstPage; pageNumber < lastPage; pageNumber++)
            if (pageNumber == Model.PaginatedList.PageIndex)
            {
    %><span><%=(pageNumber + 1)%></span><%
            }
            else
            {
    %><%=Html.ActionLink((pageNumber + 1).ToString(), "Index", new { startDate = Model.StartDate, endDate = Model.EndDate, portal = Model.PortalId, page = pageNumber })%><%
            }
        if (Model.PaginatedList.HasNextPage)
        {
    %><%=Html.ActionLink("Next", "Index", new { startDate = Model.StartDate, endDate = Model.EndDate, portal = Model.PortalId, page = (Model.PaginatedList.PageIndex + 1) })%><%
        }
        else
        {
    %><span class="pn">Next</span><%
        }
                
    %>
</section>
<%
    }
    else
    {
%>
<br />
<br />
<br />
<%
    }
%>
