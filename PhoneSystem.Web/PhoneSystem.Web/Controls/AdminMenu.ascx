<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.ascx.cs" Inherits="PhoneSystem.Web.Controls.AdminMenu" %>

<div class="panel app-panel">
    <ul class="nav nav-pills nav-stacked menu">
        <asp:Repeater runat="server" ID="items" ItemType="System.String" SelectMethod="items_GetData">
            <ItemTemplate>
                <li id="<%# Item.ToLower()%>" class='<%# (this.CurrentPageName == Item.ToLower()) ? "active" : "" %>'>
                    <a href="/Admin/<%# Item %>">
                        <%# Item.PascalCaseToText() %>
                    </a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
