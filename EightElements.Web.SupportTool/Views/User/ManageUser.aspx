<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EightElements.Web.SupportTool.Domain.Models.UserInfo>" %>
<%@ Import Namespace="EightElements.Web.SupportTool.Domain.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <%Html.RenderPartial("UserMenu");%></div>
        <div>
            <h1>
                ManageUser</h1>
            <% Html.BeginForm("AddUserPortal", "User");
               { %>
            <table>
                <tr>
                    <td colspan="2">Add Portal to User</td>
                </tr>
                <tr>
                    <td>
                        User name
                    </td>
                    <td align="left">
                        <%=Html.TextBox("username")%><input type="submit" name="submitButton" value="Search" title="Search by UserName" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Select Portal
                    </td>
                    <td>
                        <%=Html.DropDownList("portal", ViewData["portal"] as List<SelectListItem>)%>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <input type="submit" name="submitButton" value="Add" title="Add" />
                    </td>
                </tr>
            </table>
            <%} %>
        </div>        
        <%
            if (Model != null & Model.CMSUser != null && Model.Portals.Count() > 0)
            {
        %>
        <div>            
            <table class="portalSetting">
                <tr>
                    <td colspan="4" align="left">
                        Full name: <%=Model.CMSUser.FullName%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Portal Id
                    </td>
                    <td>
                        Portal Title
                    </td>
                    <td>
                        Init Msisdn Number
                    </td>
                    <td>
                        Cancel Remark Note
                    </td>
                    <td>
                    </td>
                </tr>
                <% 
                foreach (Portal p in Model.Portals)
                {
                %>
                <tr>
                    <td>
                            <%=p.Id%>
                    </td>
                    <td>
                        <%=p.Title%>
                    </td>
                    <td>
                        <%=p.InitMsisdnNumber%>
                    </td>
                    <td>
                        <%=p.CancelRemark %>
                    </td>
                    <td>
                        <%= Html.ActionLink("Delete", "DeletePortal", new { userId = Model.CMSUser.ID, portalId = p.Id, username = Model.CMSUser.Username})%>
                    </td>
                </tr>
                <%        
                }
                %>
            </table>
        </div>
        <%} %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
