<%@ Page Title="Home Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="text-center"><%:this.Title %></h2>
        </div>
        <div class="col-md-2">
            <cc:AdminMenu runat="server" ID="menu" />
        </div>

        <div class="col-md-10">
            <div class=" jumbotron">
                <h1>Hello admin</h1>
            </div>

            <asp:Repeater runat="server" ID="users" ItemType="System.String" SelectMethod="users_GetData">
                <ItemTemplate>
                    <div><%# Item %></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
