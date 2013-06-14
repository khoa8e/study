<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EightElements.Web.SupportTool.Domain.Models.UserInfo>" %>

<%@ Import Namespace="EightElements.Web.SupportTool.Domain.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <%Html.RenderPartial("UserMenu");%>
        </div>
        <div>
            <h1>
                Portal setting
            </h1>
            <table class="portalSetting">
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
                        <span class="portalId">
                            <%=p.Id %></span>
                    </td>
                    <td>
                        <%=p.Title %>
                    </td>
                    <td>
                        <input type="text" value="<%=p.InitMsisdnNumber %>" />
                    </td>
                    <td>
                        <input type="text" value="<%=p.CancelRemark %>" />
                    </td>
                    <td>
                        <input type="button" class="buttonUpdate" value="Update" />
                    </td>
                </tr>
                <%        
                    }
                %>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/Views/UserProfile.js"></script>
</asp:Content>
