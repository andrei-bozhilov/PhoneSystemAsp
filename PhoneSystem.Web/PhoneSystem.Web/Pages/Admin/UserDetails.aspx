<%@ Page Title="User Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.UserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2>
                <a href="/Admin/Users" class="pull-left">Back</a>
                <span class="text-center block">
                    <%:this.Title %>
                </span>
            </h2>
        </div>
        <div class="col-md-2">
            <cc:AdminMenu runat="server" ID="menu" />
            <cc:AdminMenu runat="server" ID="subMenu" />
        </div>
        <div class="col-md-10">
            <h2 class="text-center">
                <%: this.ViewModel.UserInfo.FullName %>
            </h2>
            <div class="panel app-panel">
                <div class="panel-body">

                    <div class="col-md-6">
                        <cc:FormCreater runat="server" ID="FormCreaterUser" />
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox runat="server" name="isAdmin" ID="CheckBoxIsAdmin"
                                    OnCheckedChanged="CheckBoxIsAdmin_CheckedChanged" AutoPostBack="true" />
                                Is Admin
                            </label>
                        </div>
                        <asp:Button Text="Change user" runat="server" ID="BtnChangeUser"
                            CssClass="btn btn-danger" OnClick="BtnChangeUser_Click" />
                    </div>
                    <div class="col-md-12">
                        <hr />
                    </div>


                    <div class="col-md-12">
                        <h2>Phone info</h2>
                    </div>
                    <asp:Repeater runat="server" ID="RepeaterPhones"
                        ItemType="PhoneSystem.Web.ViewModels.Admin.PhoneInfoViewModel" OnItemDataBound="RepeaterPhones_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-md-6">
                                <cc:FormCreater runat="server" ID="FormCreaterPhones" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>


                    <div class="col-md-12">
                        <h2>Order info</h2>
                    </div>
                    <asp:Repeater runat="server" ID="RepeaterOrders"
                        ItemType="PhoneSystem.Web.ViewModels.Admin.OrdersViewModel" OnItemDataBound="RepeaterOrders_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-md-6">
                                <cc:FormCreater runat="server" ID="FormCreaterOrders" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
