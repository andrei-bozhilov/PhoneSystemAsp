<%@ Page Title="Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.Users" %>

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
            <cc:Grid ID="GridUsers" runat="server" OnBtnViewClick="GridUsers_BtnViewClick" ShowId="false" ShowViewModalBox="false" GridButtons="View">
                <ViewBodyTemplate>
                </ViewBodyTemplate>
            </cc:Grid>
        </div>
    </div>

</asp:Content>
