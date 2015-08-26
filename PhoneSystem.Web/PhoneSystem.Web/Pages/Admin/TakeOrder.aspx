<%@ Page Title="Take order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TakeOrder.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.TakeOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="text-center"><%:this.Title %></h2>
        </div>
        <div class="col-md-2">
            <cc:AdminMenu runat="server" ID="menu" />
            <cc:AdminMenu runat="server" ID="subMenu" />
            <div id="loading" style="display: none;">
                <img src="../../Images/loading.gif" width="50" />
            </div>
        </div>

        <div class="col-md-10">
            <h2>Take card</h2>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="panel app-panel">
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="fullname">Username:</label>
                                    <input type="text" runat="server" id="userNames" data-provide="typeahead" class="form-control">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="phone">Phone:</label>
                                    <input type="text" runat="server" id="phoneNumbers" data-provide="typeahead" class="form-control">
                                </div>
                                <div class="form-group">
                                    <asp:Button Text="Search" runat="server" ID="BtnSearch" CssClass="btn btn-primary" OnClick="BtnSearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <h2>User's info</h2>
                    <div class="panel app-panel">
                        <div class="panel-body bg-warning">
                            <div class="col-md-12">
                                <h2><span class="label label-info">User</span></h2>
                                <asp:Repeater runat="server"
                                    ItemType="PhoneSystem.Web.ViewModels.Admin.TakeOrder.UserViewModel"
                                    ID="UserRepeter">
                                    <ItemTemplate>
                                        <h2><%# Item.UserName %></h2>
                                        <li class="list-group-item">
                                            <strong>ID:</strong>
                                            <span class="label label-success"><%# Item.Id %></span>
                                        </li>
                                        <li class="list-group-item">
                                            <strong>Job Title:</strong>
                                            <span class="label label-success"><%# Item.JobTitleName %></span>
                                        </li>
                                        <li class="list-group-item">
                                            <strong>Department name:</strong>
                                            <span class="label label-success"><%# Item.DepartmentName %></span>
                                        </li>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <ul class="list-group">
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <h2>
                                        <span class="label label-primary">Phones</span>
                                    </h2>
                                </div>
                                <asp:Repeater runat="server"
                                    ItemType="PhoneSystem.Web.ViewModels.Admin.TakeOrder.PhoneInfoViewModel"
                                    ID="PhonesRepeter">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <ul class="list-group">

                                                <h2><%# Item.PhoneNumber %></h2>
                                                <li class="list-group-item">
                                                    <strong>Status:</strong>
                                                    <span class="label label-success"><%# Item.Status %></span>
                                                </li>
                                                <li class="list-group-item">
                                                    <strong>Create at:</strong>
                                                    <span class="label label-success"><%# Item.CreatedAt %></span>
                                                </li>
                                                <li class="list-group-item">
                                                    <strong>Cart type:</strong>
                                                    <span class="label label-success"><%# Item.CardType %></span>
                                                </li>
                                                <li class="list-group-item">
                                                    <strong>Credit limit:</strong>
                                                    <span class="label label-success"><%# Item.CreditLimit %></span>
                                                </li>
                                                <li class="list-group-item">
                                                    <strong>Roaming:</strong>
                                                    <span class="label label-success"><%# Item.HasRouming %></span>
                                                </li>
                                            </ul>
                                            <asp:Button Text="Take" runat="server" ID="BtnTakaCard" CssClass="btn btn-primary" OnClick="BtnTakaCard_Click" CommandArgument="<%# Item.PhoneNumber %>" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <h2>
                                        <span class="label label-danger">Orders</span>
                                    </h2>
                                </div>
                                <asp:Repeater runat="server"
                                    ItemType="PhoneSystem.Web.ViewModels.Admin.TakeOrder.OrdersViewModel"
                                    ID="OrderRepeater">
                                    <ItemTemplate>
                                        <div class="col-md-6">
                                            <h2><%# Item.PhoneNumber %></h2>
                                            <li class="list-group-item">
                                                <strong>Action:</strong>
                                                <span class="label label-success"><%# Item.PhoneAction %></span>
                                            </li>
                                            <li class="list-group-item">
                                                <strong>User:</strong>
                                                <span class="label label-success"><%# Item.FullName %></span>
                                            </li>
                                            <li class="list-group-item">
                                                <strong>Date:</strong>
                                                <span class="label label-success"><%# Item.ActionDate %></span>
                                            </li>
                                            <li class="list-group-item">
                                                <strong>Admin:</strong>
                                                <span class="label label-success"><%# Item.AdminUserName %></span>
                                            </li>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <ul class="list-group">
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <cc:Noty runat="server" ID="NotyTakeOrder" />
                    <asp:Literal runat="server" ID="Script" Mode="PassThrough" />
                    <div id="dvUControl"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
