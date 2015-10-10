<%@ Page Title="Job titles" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="JobTitles.aspx.cs"
    Inherits="PhoneSystem.Web.Pages.Admin.JobTitles" %>

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
            <asp:CheckBox ID="CheckBoxShowDeleted" Text="Show deleted" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxShowDeleted_CheckedChanged" />
            <cc:Grid ID="JobTitleGrid" runat="server" OnBtnCreateClick="JobTitleGrid_BtnCreateClick" OnBtnViewClick="JobTitleGrid_BtnViewClick" OnBtnEditClick="JobTitleGrid_BtnEditClick" OnBtnDeleteClick="JobTitleGrid_BtnDeleteClick" OnNeedDataSource="JobTitleGrid_NeedDataSource" GridButtons="Crud">
                <CreateHeaderTemplate>
                    Create New Job Title
                </CreateHeaderTemplate>
                <CreateBodyTemplate>
                    <cc:FormCreater runat="server" ID="FormCreaterCreate" Prefix="Create"></cc:FormCreater>
                </CreateBodyTemplate>
                <CreateFooterTemplate>
                    <asp:Button Text="Create" runat="server" ID="BtnCreate" CssClass="btn btn-primary" OnClick="BtnCreate_Click" />
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
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Label Text="text" ID="Label1" runat="server" />
            <asp:Button Text="text" runat="server" />
            <cc:Noty ID="NotyJobTitle" runat="server" />
        </div>
    </div>

</asp:Content>
