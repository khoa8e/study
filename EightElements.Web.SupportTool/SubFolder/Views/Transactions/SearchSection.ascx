<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Helpers" %>
<% Html.BeginForm("Index", "Transactions");
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
            <label for="gameTitle">
                Enter a game name</label>
        </td>
        <td>
            <input type="text" id="gameTitle" name="gameTitle">
        </td>
    </tr>
    <tr>
        <td>
            <label for="startDate">
                Start Date (MM/DD/YYYY)</label>
        </td>
        <td>
            <input type="text" id="startDate" name="startDate">
        </td>
    </tr>
    <tr>
        <td>
            <label for="endDate">
                End Date (MM/DD/YYYY)</label>
        </td>
        <td>
            <input type="text" id="endDate" name="endDate">
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
