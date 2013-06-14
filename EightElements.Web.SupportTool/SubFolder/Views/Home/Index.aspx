<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>        
        <a href="<%=Url.Content("~/Subscriptions/Search") %>">Subscription Details</a>
        <br />
        <a href="<%=Url.Content("~/Transactions/Search") %>">Game Sale</a>
        <br />
        <a href="<%=Url.Content("~/SubscriptionHistory/Search") %>">8e Sieugame portal transaction</a>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
