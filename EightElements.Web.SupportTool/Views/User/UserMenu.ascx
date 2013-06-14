<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Domain.Models" %>
<%@ Import Namespace="EightElements.Web.SupportTool" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Helpers" %>
<%if (Settings.AdminUsername.ToUpper().Split(',')
      .Contains(SessionHandler.UserInfo.CMSUser.Username.ToUpper()))
  {
%>
<div>
    <table>
        <tr>
            <td>
                <%=Html.ActionLink("Portal Setting", "UserProfile", "User")%>
            </td>
            <td>
                <%=Html.ActionLink("Manage User", "ManageUser", "User")%>
            </td>
        </tr>
    </table>
</div>
<% } %>
