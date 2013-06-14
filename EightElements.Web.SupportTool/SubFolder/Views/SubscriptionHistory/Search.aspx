<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <h1>Search</h1>
    <section id="search"><% Html.RenderPartial("SearchSection"); %></section>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<script src="<%=Url.Content("~/Scripts/SearchUI.js") %>" type="text/javascript"></script>
</asp:Content>
