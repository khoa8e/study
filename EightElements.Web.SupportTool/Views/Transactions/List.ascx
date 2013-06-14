<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EightElements.Web.SupportTool.Model.TransactionList>" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Domain.Models" %>
<table>
    <tr>
        <td>
            Purchase ID
        </td>
        <td>
            Time (GMT +8)
        </td>
        <td>
            Mobile No
        </td>
        <td>
            Content Name
        </td>
        <td>
            File Size
        </td>
        <td>
            Price
        </td>
        <td>
            Install Status
        </td>
        <td>
            PhoneModel
        </td>
    </tr>
    <%
        foreach (Transaction trx in Model.PaginatedList)
        {
    %><tr>
        <td>
            <%=trx.ID %>
        </td>
        <td>
            <%=trx.Timestamp %>
        </td>
        <td>
            <%=trx.Msisdn %>
        </td>
        <td>
            <%=trx.ContentName %>
        </td>
        <td>
            <%=String.Format("{0:0.00}", trx.FileSize)%>
        </td>
        <td>
            RM<%=trx.Price %>
        </td>
        <td>
            <%=trx.InstallStatus %>
        </td>
        <td>
            <%=trx.PhoneModel %>
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
    %><%=Html.ActionLink("Prev", "Index", new { startDate = Model.StartDate, endDate = Model.EndDate, msisdn = Model.Msisdn, gameTitle = Model.GameTitle, portal = Model.PortalId, page = (Model.PaginatedList.PageIndex - 1) })%><%
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
    %><span><%=(pageNumber + 1) %></span><%
            }
            else
            {
    %><%=Html.ActionLink((pageNumber + 1).ToString(), "Index", new { startDate = Model.StartDate, endDate = Model.EndDate, msisdn = Model.Msisdn, gameTitle = Model.GameTitle, portal = Model.PortalId, page = pageNumber })%><%
            }
        if (Model.PaginatedList.HasNextPage)
        {
    %><%=Html.ActionLink("Next", "Index", new { startDate = Model.StartDate, endDate = Model.EndDate, msisdn = Model.Msisdn, gameTitle = Model.GameTitle, portal = Model.PortalId, page = (Model.PaginatedList.PageIndex + 1) })%><%
        }
        else
        {
    %><span class="pn">Next</span><%
        }
        
    %>
</section>
