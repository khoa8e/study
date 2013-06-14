<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Helpers" %>
<% Html.BeginForm("Index", "Subscriptions");
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
            <label for="msisdn">
                Enter a phone number</label>
        </td>
        <td>
            <input type="text" id="msisdn" name="msisdn">
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <input type="submit" value="Show Details" title="Show Details" />
        </td>
    </tr>
</table>
<% } %>