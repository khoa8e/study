<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EightElements.Web.SupportTool.Model.SubscriptionList>" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Domain.Models" %>
<% if (Model.Msisdn == null || Model.Msisdn == "")
   { %>
Please Enter A Phone Number
<% }
   else if (Model.Subscriber == null || Model.Subscriber.Count == 0)
   { %>
No Current Subscriptions Found
<% }
   else
   { %>
<table>
    <tr>
        <td>
            Msisdn
        </td>
        <td>
            Subscription ID
        </td>
        <td>
            Subscription Date (GMT +8)
        </td>
        <td>
            Credits
        </td>
        <td>
            Status
        </td>
        <td>
            Actions
        </td>
    </tr>
    <tr>
        <td>
            <%=Model.Subscriber[0].Msisdn%>
        </td>
        <td>
            <%=Model.Subscriber[0].ID%>
        </td>
        <td>
            <%=Model.Subscriber[0].Timestamp%>
        </td>
        <td>
            <%=Model.Subscriber[0].Credits%>
        </td>
        <td>
            <%=Model.Subscriber[0].SubscriptionStatus%>
        </td>
        <td>
            <%= Html.ActionLink("Cancel Subscription", "CancelSubscription", new { msisdn = Model.Subscriber[0].Msisdn, portalId = Model.PortalId })%>            
        </td>
    </tr>
</table>
<% } %>
<% if (Model.Msisdn != null && Model.Msisdn != "")
   { %>
<h3>
    Latest Details for
    <%=Model.Msisdn %></h3>
<% if (Model.PaginatedList == null || Model.PaginatedList.Count == 0)
   { %>
No Previous Details Found for
<%= Model.Msisdn %>
<% }
   else
   { %>
<table>
    <tr>
        <td>
            Time (GMT +8)
        </td>
        <td>
            Action
        </td>
        <td>
            Game
        </td>
        <td>
            Status
        </td>
    </tr>
    <%
            foreach (SubscriptionActivity subact in Model.PaginatedList)
            {
    %><tr>
        <td>
            <%=subact.Timestamp%>
        </td>
        <td>
            <%=subact.Action%>
        </td>
        <td>
            <%=subact.ContentName%>
        </td>
        <td>
            <%=subact.ActionStatus%>
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
    %><%=Html.ActionLink("Prev", "Index", new { msisdn = Model.Msisdn, portal = Model.PortalId, page = (Model.PaginatedList.PageIndex - 1) })%><%
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
    %><%=Html.ActionLink((pageNumber + 1).ToString(), "Index", new { msisdn = Model.Msisdn, portal = Model.PortalId, page = pageNumber })%><%
                    }
                if (Model.PaginatedList.HasNextPage)
                {
    %><%=Html.ActionLink("Next", "Index", new { msisdn = Model.Msisdn, portal = Model.PortalId, page = (Model.PaginatedList.PageIndex + 1) })%><%
                }
                else
                {
    %><span class="pn">Next</span><%
                }
                
    %>
</section>
<% }
   } %>
