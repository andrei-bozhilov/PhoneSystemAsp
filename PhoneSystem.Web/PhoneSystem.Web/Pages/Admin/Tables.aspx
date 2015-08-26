<%@ Page Title="Table managment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tables.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.Tables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="text-center"><%:this.Title %></h2>
        </div>
        <div class="col-md-2">
            <cc:AdminMenu runat="server" ID="menu" />
            <cc:AdminMenu runat="server" ID="subMenu" />
        </div>

        <div class="col-md-10">
            <div class=" jumbotron">
                <h1>Hello admin</h1>
            </div>

        </div>
    </div>

</asp:Content>
