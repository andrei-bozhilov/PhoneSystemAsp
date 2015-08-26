<%@ Page Title="Give Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GiveOrder.aspx.cs" Inherits="PhoneSystem.Web.Pages.Admin.GiveOrder" %>

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
            <div class="no-print">
                <h2>Give card</h2>
                <div class="panel app-panel">
                    <div class="panel-body">

                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active">
                                <a href="#new-user" aria-controls="new-user" role="tab" data-toggle="tab">New user</a>
                            </li>
                            <li role="presentation">
                                <a href="#old-user" aria-controls="old-user" role="tab" data-toggle="tab">Old user</a>
                            </li>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="new-user">
                                <br />
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-6">
                                            <div class="panel">
                                                <div class="form-group">
                                                    <label for="fullname">Full Name:</label>
                                                    <input type="text" class="form-control" id="fullname" name="fullname"
                                                        placeholder="Enter full name" onkeyup="app.translate($(this))" />
                                                </div>
                                                <div class="form-group">
                                                    <label for="departmentId">Department:</label>
                                                    <asp:DropDownList runat="server" ID="DropDownListDepartments" CssClass="form-control"
                                                        DataTextField="Text" DataValueField="Value" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="(Choose Department)"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label for="phone">Phone:</label>
                                                    <asp:DropDownList runat="server" ID="DropDownListPhone" CssClass="form-control"
                                                        DataTextField="Text" DataValueField="Value" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="(Choose Number)"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="panel">
                                                <div class="form-group">
                                                    <label for="username">Username:</label>
                                                    <input type="text" class="form-control" id="username" name="username">
                                                </div>
                                                <div class="form-group">
                                                    <label for="password">Password:</label>
                                                    <input type="text" class="form-control" id="password" name="password">
                                                </div>
                                                <div class="form-group">
                                                    <label for="jobTitleId">Job Title:</label>
                                                    <asp:DropDownList runat="server" ID="DropDownListJobTitle"
                                                        DataTextField="Text" CssClass="form-control"
                                                        DataValueField="Value" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0" Text="(Choose job title)" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <input type="submit" class="btn btn-primary" runat="server" id="btnCreateNewUser"
                                                        onserverclick="btnCreateNewUser_ServerClick" />
                                                </div>
                                            </div>
                                        </div>
                                        <cc:Noty runat="server" ID="NotyFirstPanel" />
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="old-user">
                                <br />
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="oldUser">Username:</label>
                                                <input type="text" name="oldUser" id="OldUser"
                                                    onchange="$('#MainContent_BtnShowUserInfo').click()" runat="server"
                                                    data-provide="typeahead" class="form-control">
                                                <br />
                                                <asp:Button runat="server" ID="BtnShowUserInfo" OnClick="BtnShowUserInfo_Click"
                                                    Text="Show User Info" CssClass="btn btn-info" />
                                            </div>
                                            <div class="form-group">
                                                <label for="phone">Phone:</label>
                                                <asp:DropDownList runat="server" ID="DropDownListPhoneOldUser" CssClass="form-control"
                                                    DataTextField="Text" DataValueField="Value" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0" Text="(Choose Number)"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Department:</label>
                                                <input type="text" runat="server" id="InputDepartment" class="form-control" disabled>
                                            </div>
                                            <div class="form-group">
                                                <label>Job title:</label>
                                                <input type="text" runat="server" id="InputJobTitle" class="form-control" disabled>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <input type="submit" class="btn btn-primary" runat="server" id="BtnOldUser"
                                                        onserverclick="BtnOldUser_ServerClick" />
                                                </div>
                                            </div>
                                        </div>
                                        <cc:Noty runat="server" ID="NotySecondPanel" />
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <h2 class="no-print">Preview</h2>
            <div class="col-md-10">
            </div>
        </div>
    </div>
</asp:Content>
