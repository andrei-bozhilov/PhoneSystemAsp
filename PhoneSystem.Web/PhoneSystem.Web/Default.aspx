<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PhoneSystem.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Hello to Phone Managment System v.0.0.0</h1>
        <asp:HyperLink runat="server" ID="HyperLink" NavigateUrl="~/Account/Login.aspx" CssClass="btn btn-lg btn-primary">
           Please login
        </asp:HyperLink>
        <br />
        <div class="alert alert-info">When you got your card, you have received username and password</div>
    </div>
</asp:Content>
