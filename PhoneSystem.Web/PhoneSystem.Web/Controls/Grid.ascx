﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Grid.ascx.cs"
    Inherits="PhoneSystem.Web.Controls.Grid" %>
<%@ Register TagPrefix="cc" TagName="FormCreater" Src="~/Controls/FormCreater.ascx" %>

<%--<div>
    <input type="text" runat="server" id="Proba" value="" />
</div>--%>

<div class="row">
    <div class="col-md-6">
        <% if (this.GridButtons == PhoneSystem.Web.Controls.GridButtons.Crud)
            { %>
        <asp:Button runat="server" CssClass="btn btn-primary"
            ID="GridBtnCreate" Text="Create" OnClick="GridBtnCreate_Click" />
        <%} %>
        <div class="dropdown" style="display: inline-block">
            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu" data-toggle="dropdown" aria-expanded="true">
                Filters
                <span class="caret"></span>
            </button>
            <div class="panel app-panel app-dropdown-menu dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                <div class="panel-body">
                    <cc:FormCreater ID="FormCreaterFilter" runat="server" />
                    <div class="pull-right">
                        <asp:Button Text="Search" runat="server" ID="GridBtnSearch" CssClass="btn btn-primary" OnClick="GridBtnSearch_Click" />
                        <asp:Button Text="Clear" runat="server" ID="GridBtnClear" CssClass="btn btn-warning" OnClick="GridBtnClear_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<br />

<table class="table table-bordered table-condensed table-hover table-striped table-responsive">
    <thead>
        <tr>
            <asp:Repeater runat="server" ID="RepeaterHeaders" ItemType="System.String">
                <ItemTemplate>
                    <th>

                        <a href="<%# this.ModifyCurrentUrl(this.SortDir[Item])  %>">
                            <span <%# this.Request.Url.AbsoluteUri.Contains("sort=" + Item + "_desc") 
                            ? "class='glyphicon glyphicon-menu-down' style='color: red'" : (this.Request.Url.AbsoluteUri.Contains("sort=" + Item + "_asc") ? "class='glyphicon glyphicon-menu-up' style='color: green'" : "") %>></span>
                            <%# Item.PascalCaseToText() %>
                        </a>
                    </th>
                </ItemTemplate>
            </asp:Repeater>
            <% if (this.GridButtons != PhoneSystem.Web.Controls.GridButtons.None)
                {
                    this.Response.Write("<th>Actions</th>");
                }
            %>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater runat="server" ID="RepeaterRows">
            <ItemTemplate>
                <tr>
                    <asp:Repeater runat="server" ID="RepeaterCols" DataSource='<%# Eval("ItemArray") %>'
                        OnItemDataBound="RepeaterCols_ItemDataBound">
                        <ItemTemplate>
                            <td <%# Container.ItemIndex == 0 && !this.ShowId ? "style=display:none" : "" %>>
                                <asp:Literal ID="ValuePlaceHolder" runat="server" />
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%              
                        if (this.GridButtons != PhoneSystem.Web.Controls.GridButtons.None)
                        {%>
                    <td>

                        <asp:Button runat="server" CssClass="btn btn-sm btn-info" ID="GridBtnView" Text="View"
                            CommandArgument='<%# Eval("ItemArray[0]")  %>' OnClick="GridBtnView_Click" />

                        <%              
                            if (this.GridButtons == PhoneSystem.Web.Controls.GridButtons.Crud)
                            {%>
                        <asp:Button runat="server" CssClass="btn btn-sm btn-warning" ID="GridBtnEdit" Text="Edit"
                            CommandArgument='<%# Eval("ItemArray[0]")%>' OnClick="GridBtnEdit_Click" />
                        <asp:Button runat="server" CssClass="btn btn-sm btn-danger" ID="GridBtnDelete" Text="Delete"
                            CommandArgument='<%# Eval("ItemArray[0]")%>' OnClick="GridBtnDelete_Click" />
                        <% } %>
                    </td>
                    <% } %>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater ID="RepeaterEmptyRows" runat="server">
            <ItemTemplate>
                <tr>
                    <asp:Repeater ID="RepeaterEmptyCols" runat="server" SelectMethod="RepeaterEmptyCols_GetData">
                        <ItemTemplate>
                            <td style='color: white'>.</td>
                        </ItemTemplate>
                    </asp:Repeater>
                    <% 
                        if (this.GridButtons == PhoneSystem.Web.Controls.GridButtons.Crud)
                        {%>
                    <td style='color: white'>.</td>
                    <% } %>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <%--<% if (this.data.Rows.Count < this.PageSize)
           {
               var rows = this.PageSize - this.data.Rows.Count;
               for (int row = 0; row < rows; row++)
               {
                   this.Response.Write("<tr>");
                   int cols = 0;
                   if (this.data.Rows.Count != 0)
                   {
                       cols = this.data.Rows[0].ItemArray.Count()
                       + (this.GridButtons != PhoneSystem.Web.Controls.GridButtons.None ? 1 : 0);
                   }

                   if (!this.ShowId)
                   {
                       cols = cols - 1;
                   }

                   for (int col = 0; col < cols; col++)
                   {
                       this.Response.Write("<td style='color:white'>.</td>");
                   }

                   this.Response.Write("</tr>");
               }
           } %>--%>
    </tbody>
</table>

<nav>
    <ul class="pagination">
        <li>
            <a <%= "href='" +  this.ModifyCurrentUrl(this.CurrentSorting, 1)  + "'" %>>
                <span aria-hidden="true">First</span>
            </a>
        </li>
        <li <%= this.CurrentPage == 1 ? "class='disabled'": "" %>>
            <a <%= this.CurrentPage == 1 ? "": "href='" +  this.ModifyCurrentUrl(this.CurrentSorting, this.CurrentPage - 1)  + "'" %>>
                <span aria-hidden="true">Previous</span>
            </a>
        </li>
        <%
            int from = (this.CurrentPage - 2 < 1) ? 1 : this.CurrentPage - 2;
            int to = (this.CurrentPage + 2 > this.numPages) ? this.numPages : this.CurrentPage + 2;

            if (from + 4 > to && from + 4 <= this.numPages)
            {
                to = from + 4;
            }

            if (to - 4 < from && to - 4 >= 1)
            {
                from = to - 4;
            }

            for (int i = from; i <= to; i++)
            {

        %>
        <li <%= i == this.CurrentPage ? "class='active'": "" %>>
            <a href='<%= this.ModifyCurrentUrl(this.CurrentSorting, i)  %>'>
                <%: i  %>
            </a>
        </li>

        <% } %>

        <li <%= this.CurrentPage == this.numPages ? "class='disabled'": "" %>>
            <a <%= this.CurrentPage == this.numPages ? "": "href='" + this.ModifyCurrentUrl(this.CurrentSorting, this.CurrentPage + 1) + "'" %>>
                <span aria-hidden="true">Next</span>
            </a>
        </li>
        <li>
            <a <%= this.CurrentPage == this.numPages ? "": "href='" + this.ModifyCurrentUrl(this.CurrentSorting, this.numPages) + "'" %>>
                <span aria-hidden="true">Last</span>
            </a>
        </li>
    </ul>
</nav>



<div class="modal fade" id="CreatModal">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <asp:PlaceHolder runat="server" ID="PlaceHolderCreateHeader" />
                </h4>
            </div>
            <div class="modal-body">
                <asp:PlaceHolder runat="server" ID="PlaceHolderCreateBody" />
            </div>
            <div class="modal-footer">
                <asp:PlaceHolder runat="server" ID="PlaceHolderCreateFooter" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="ViewModal">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <asp:PlaceHolder runat="server" ID="PlaceHolderViewHeader" />
                </h4>
            </div>
            <div class="modal-body">
                <asp:PlaceHolder runat="server" ID="PlaceHolderViewBody" />
            </div>
            <div class="modal-footer">
                <asp:PlaceHolder runat="server" ID="PlaceHolderViewFooter" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- /.modal -->


<div class="modal fade" id="EditModal">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <asp:PlaceHolder runat="server" ID="PlaceHolderEditHeader" />
                </h4>
            </div>
            <div class="modal-body">
                <asp:PlaceHolder runat="server" ID="PlaceHolderEditBody" />
            </div>
            <div class="modal-footer">
                <asp:PlaceHolder runat="server" ID="PlaceHolderEditFooter" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<div class="modal fade" id="DeleteModal">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">
                    <asp:PlaceHolder runat="server" ID="PlaceHolderDeleteHeader" />
                </h4>
            </div>
            <div class="modal-body">
                <asp:PlaceHolder runat="server" ID="PlaceHolderDeleteBody" />
            </div>
            <div class="modal-footer">
                <asp:PlaceHolder runat="server" ID="PlaceHolderDeleteFooter" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- /.modal -->


<script type="text/javascript">
    $('.dropdown-menu').click(function (e) {
        e.stopPropagation();
    });
</script>




<asp:Label ID="ScriptPlaceHolder" runat="server" />