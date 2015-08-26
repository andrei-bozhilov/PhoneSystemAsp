<%@ Page Title="Phonebook" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Phonebook.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.Phonebook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="text-center"><%:this.Title %></h2>
        </div>
        <div class="col-md-2">
            <cc:AdminMenu runat="server" ID="menu" />
            <div id="loading" style="display: none;">
                <img src="../../Images/loading.gif" width="50" />
            </div>
        </div>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanelPhonebook">
            <ContentTemplate>
                <div class="col-md-10">
                    <div class="panel app-panel">
                        <div class="panel-body">
                            <strong>Group by:</strong>
                            <ul class="nav nav-tabs" id="groupBy-navigation">
                                <li role="presentation" class="active">
                                    <a role="button" data-groupby="name">Name</a></li>
                                <li role="presentation">
                                    <a role="button" data-groupby="department">Department</a>
                                </li>
                                <li role="presentation">
                                    <a role="button" data-groupby="jobtitle">Job title</a>
                                </li>
                                <li role="presentation">
                                    <a role="button" data-groupby="phone">Phone</a>
                                </li>
                            </ul>
                            <br />
                            <div class="form-inline" id="PhoneSearch" runat="server">
                                <div class="form-group">
                                    <input type="text" name="phoneNumber" runat="server" id="phoneNumber" data-provide="typeahead" class="form-control" />
                                </div>
                                <input type="submit" name="Search" value="Search" id="Search" class="btn btn-primary" />
                            </div>
                            <asp:Repeater runat="server" ID="RepeaterByLetters" ItemType="System.String">
                                <HeaderTemplate>
                                    <div class="btn-toolbar" role="toolbar" id="get-navigation">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <button type="button" class='btn btn-default' data-get="<%# Item %>">
                                        <%#: Item %>
                                    </button>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>

                            <asp:Repeater runat="server" ID="RepeaterByGroup" OnItemDataBound="RepeaterByGroup_ItemDataBound">
                                <HeaderTemplate>
                                    <hr />
                                    <div class="bg-primary col-md-12">
                                        <div class="col-md-3">
                                            <h4>Username</h4>
                                        </div>
                                        <div class="col-md-3">
                                            <h4>Full name</h4>
                                        </div>
                                        <div class="col-md-3">
                                            <h4>Phone</h4>
                                        </div>
                                        <div class="col-md-3">
                                            <h4>Department and Job title</h4>
                                        </div>
                                    </div>
                                    <br />
                                </HeaderTemplate>
                                <%--if no data--%>
                                <FooterTemplate>
                                    <div class="alert alert-danger" id="MessageNoData" visible="false" runat="server" role="alert">No Data To Display</div>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <h2>
                                        <asp:Literal Text='<%# Eval("Key") %>' runat="server" Mode="Encode" />
                                    </h2>
                                    <asp:Repeater runat="server" ID="RepeaterByElement" ItemType="PhoneSystem.Web.ViewModels.Admin.Phonebook.PhonebookViewModel" DataSource='<%# Bind("Value") %>'>
                                        <ItemTemplate>
                                            <div class="col-md-12 bg-success">
                                                <div class="col-md-3">
                                                    <%#: Item.UserName %>
                                                </div>
                                                <div class="col-md-3">
                                                    <%#: Item.FullName %>
                                                </div>
                                                <div class="col-md-3">
                                                    <%#: Item.PhoneNumber %>
                                                </div>
                                                <div class="col-md-3">
                                                    <div>
                                                        <strong>Department</strong>
                                                        <span class="label label-info"><%#: Item.DepartmentName %></span>
                                                    </div>
                                                    <div>
                                                        <strong>Job title</strong>
                                                        <span class="label label-info"><%#: Item.JobTitle %></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
