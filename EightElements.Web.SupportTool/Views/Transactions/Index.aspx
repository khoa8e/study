<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EightElements.Web.SupportTool.Model.TransactionList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <h1>Transactions</h1>
    
    <a href="#" class="search" onclick="SearchClick()">
        <img src="/Content/Images/search.jpg" class="search" alt="Search" />
    </a>
    <section id="search" class="aboveList"><% Html.RenderPartial("SearchSection"); %></section>
    <section id="transactionList"><% Html.RenderPartial("List", Model); %></section>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
