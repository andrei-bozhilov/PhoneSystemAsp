<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Departments.aspx.cs"
    Inherits="PhoneSystem.Web.Pages.Admin.Departments" %>

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
            <asp:CheckBox Text="Show Deleted" runat="server" ID="CheckBoxShowDeleted" OnCheckedChanged="CheckBoxShowDeleted_CheckedChanged" AutoPostBack="true" />
            <cc:Grid ID="DepartmentGrid" runat="server" OnBtnViewClick="DepartmentGrid_BtnViewClick"
                OnBtnCreateClick="DepartmentGrid_BtnCreateClick" OnBtnEditClick="DepartmentGrid_BtnEditClick"
                OnBtnDeleteClick="DepartmentGrid_BtnDeleteClick"
                GridButtons="Crud">
                <CreateHeaderTemplate>
                    Create New Department
                </CreateHeaderTemplate>
                <CreateBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterCreate" Prefix="Create"></cc:FormCreater>
                </CreateBodyTemplate>
                <CreateFooterTemplate>
                    <asp:Button Text="Create" runat="server" ID="BtnCreate" CssClass="btn btn-primary" OnClick="BtnCreate_Click" />
                </CreateFooterTemplate>

                <ViewHeaderTemplate>
                    View Detail Department
                </ViewHeaderTemplate>
                <ViewBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterView" Prefix="View"></cc:FormCreater>
                </ViewBodyTemplate>

                <EditHeaderTemplate>
                    Edit Department
                </EditHeaderTemplate>
                <EditBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterEdit" Prefix="Edit"></cc:FormCreater>
                </EditBodyTemplate>
                <EditFooterTemplate>
                    <asp:Button Text="Edit" ID="ButtonEdit" OnClick="ButtonEdit_Click" CssClass="btn btn-warning" runat="server" />
                </EditFooterTemplate>

                <DeleteHeaderTemplate>
                    Delete Department Are You Sure?
                </DeleteHeaderTemplate>
                <DeleteBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterDelete" Prefix="Delete"></cc:FormCreater>
                </DeleteBodyTemplate>
                <DeleteFooterTemplate>
                    <asp:Button Text="Delete" CssClass="btn btn-danger" ID="BtnDelete" OnClick="BtnDelete_Click" runat="server" />
                </DeleteFooterTemplate>
            </cc:Grid>
            <cc:Noty ID="NotyDepartments" runat="server" />
        </div>
    </div>

</asp:Content>
