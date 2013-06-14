<%@ Page Title="Support Tool - Login" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <h1>
        Login</h1>
    <section id="login">
        <% Html.BeginForm(); %>
        <table>
            <tr>
                <td>
                    <label for="password">
                        Username:</label>
                </td>
                <td>
                    <input name="username" type="text" id="username" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="password">
                        Password:</label>
                </td>
                <td>
                    <input name="password" type="password" id="password" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input id="RememberMe" name="RememberMe" type="checkbox" value="true">
                    <label for="RememberMe">
                        Remember me</label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input type="submit" value="Login" onclick="return CheckForm();" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span id="error">
                        <%
                            if (ViewData["LoginResult"] == "TMF")
                            {
                        %>There have been too many failures.<br />
                        Please try again in 30 mintues.<%
                            }
                            else if (ViewData["LoginResult"] == "IUP")
                            {
                        %>Invalid username or password.<%
                            }   
                        %>
                    </span>
                </td>
            </tr>
        </table>
        <% Html.EndForm(); %>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CheckForm() {
            $("#error").empty();

            if ($("#username").val().length == 0)
                $("#error").append("Please enter your username");
            if ($("#password").val().length == 0) {
                if ($("#error").html().length > 0)
                    $("#error").append("<br />");
                $("#error").append("Please enter you password");
            }
            if ($("#error").html().length > 0)
                return false;
            else
                return true;
        }
    </script>
</asp:Content>
