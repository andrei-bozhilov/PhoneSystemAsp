<%@ Page Title="Phones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Phones.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.Phones" %>

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
            <cc:Grid ID="PhoneGrid" runat="server" GridButtons="Crud" OnBtnCreateClick="PhoneGrid_BtnCreateClick"
                 OnBtnViewClick ="PhoneGrid_BtnViewClick" OnBtnEditClick="PhoneGrid_BtnEditClick"
                 OnBtnDeleteClick="PhoneGrid_BtnDeleteClick">
                <CreateHeaderTemplate>
                    Create New Phone
                </CreateHeaderTemplate>
                <CreateBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterCreate" Prefix="Create"></cc:FormCreater>
                </CreateBodyTemplate>
                <CreateFooterTemplate>
                    <asp:Button Text="Create" runat="server" ID="BtnCreat" CssClass="btn btn-primary" OnClick="BtnCreat_Click" />
                </CreateFooterTemplate>
                <ViewHeaderTemplate>
                    View Detail Job Title
                </ViewHeaderTemplate>
                <ViewBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterView" Prefix="View"></cc:FormCreater>
                </ViewBodyTemplate>
                <EditHeaderTemplate>
                    Edit Job Title
                </EditHeaderTemplate>
                <EditBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterEdit" Prefix="Edit"></cc:FormCreater>
                </EditBodyTemplate>
                <EditFooterTemplate>
                    <asp:Button Text="Edit" ID="ButtonEdit" OnClick="ButtonEdit_Click" CssClass="btn btn-warning" runat="server" />
                </EditFooterTemplate>
                <DeleteHeaderTemplate>
                    Delete Job Title Are You Sure?
                </DeleteHeaderTemplate>
                <DeleteBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterDelete" Prefix="Delete"></cc:FormCreater>
                </DeleteBodyTemplate>
                <DeleteFooterTemplate>
                    <asp:Button Text="Delete" CssClass="btn btn-danger" ID="BtnDelete" OnClick="BtnDelete_Click" runat="server" />
                </DeleteFooterTemplate>
            </cc:Grid>
            <cc:Noty ID="NotyPhone" runat="server" />
        </div>
    </div>
</asp:Content>
