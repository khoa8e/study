<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EightElements.Web.SupportTool.Model.SubscriptionHistoryList>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <h1>
        8e Sieugame portal transaction</h1>
    <div class="iconToolbar">
    </div>
    <a href="#" class="search" onclick="SearchClick()">
        <img src="/Content/Images/search.jpg" class="search" alt="Search" />
    </a>
    <%
        string startDate = Model.StartDate.ToString("MM/dd/yyyy");
        string endDate = Model.EndDate.ToString("MM/dd/yyyy");
    %>
    <a href="/SubscriptionHistory/DownloadExcel?startDate=<%=startDate%>&endDate=<%=endDate%>&portal=<%=Model.PortalId%>"
        class="search">
        <img src="/Content/Images/Excel-icon.jpg" class="search" alt="Excel download" />
    </a>
    <section id="search" class="aboveList">
        <% Html.RenderPartial("SearchSection", Model); %>
    </section>
    <section id="subscriptionList">
        <% Html.RenderPartial("List", Model); %>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/SearchUI.js" type="text/javascript"></script>
    <script type="text/javascript">
        var searchShown = false;

        // Define the entry point
        function SearchClick() {
            if (searchShown) {
                HideSearch();
            }
            else {
                ShowSearch();
            }
            return false;
        }

        function ShowSearch() {
            searchShown = true;
            $("#search").slideDown("fast");
        }

        function HideSearch() {
            searchShown = false;
            $("#search").slideUp("fast");
        }
    </script>
</asp:Content>
