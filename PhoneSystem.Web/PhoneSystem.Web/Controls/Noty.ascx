<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Noty.ascx.cs" Inherits="PhoneSystem.Web.Controls.Noty" %>

<asp:UpdatePanel runat="server" ID="NotyPanel" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="modal fade" id="success-modal-<%:this.ClientID %>">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Success
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <asp:Repeater runat="server" ID="NotySuccess" ItemType="System.String">
                                <ItemTemplate>
                                    <div class="alert alert-success" role="alert">
                                        <span class="glyphicon glyphicon-ok">&nbsp</span><%# Item %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

        <div class="modal fade" id="error-modal-<%:this.ClientID %>">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Error
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>

                            <asp:Repeater runat="server" ID="NotyError" ItemType="System.String">
                                <ItemTemplate>
                                    <div class="alert alert-danger" role="alert">
                                        <span class="glyphicon glyphicon-remove">&nbsp</span><%# Item %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </ContentTemplate>
</asp:UpdatePanel>

