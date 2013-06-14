<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Domain.Models" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Helpers" %>
<% Html.BeginForm("Index", "SubscriptionHistory");
   { %>
<table>
    <tr>
        <td>
            Select portal
        </td>
        <td>
            <%=Html.DropDownList("portal", SessionHandler.PortalListItem)%>
        </td>
    </tr>
    <tr>
        <td>
            <label for="startDate">
                Start Date (MM/DD/YYYY)</label>
        </td>
        <td>
            <input type="text" id="startDate" name="startDate" />
        </td>
    </tr>
    <tr>
        <td>
            <label for="endDate">
                End Date (MM/DD/YYYY)</label>
        </td>
        <td>
            <input type="text" id="endDate" name="endDate" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <input type="submit" value="Go" title="Go" />
        </td>
    </tr>
</table>
<% } %>