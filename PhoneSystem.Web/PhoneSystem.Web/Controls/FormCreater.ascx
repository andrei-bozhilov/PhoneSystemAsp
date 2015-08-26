<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormCreater.ascx.cs" Inherits="PhoneSystem.Web.Controls.FormCreater" %>

<asp:Repeater runat="server" ItemType="PhoneSystem.Web.Controls.ObjectProperty" ID="RepeaterObject" OnItemDataBound="RepeaterObject_ItemDataBound">
    <HeaderTemplate>
        <div class="form-horizontal">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="form-group">
            <label for="<%# Item.PropertyName %>" class="col-sm-4 control-label"><%# Item.LabelName %></label>
            <div class="col-sm-8">
                <asp:Literal ID="ValuePlaceHolder" runat="server" />
                <%--<input type="text" id="<%# this.Prefix + Item.PropertyName %>" name="<%# this.Prefix + Item.PropertyName %>" class="form-control" <%# Item.CanBeModified ? "" : "readonly='readonly'" %> value="<%# Item.Value %>">--%>
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        <% if (this.HasValidation)
           {%>
        <div class="form-group">
            <div class="col-sm-8 col-sm-push-4">
                <asp:ValidationSummary runat="server" ShowModelStateErrors="true"
                    ForeColor="Red" HeaderText="Please check the following errors:" />
            </div>
        </div>
        <%}%>
       
        </div>
    </FooterTemplate>
</asp:Repeater>
