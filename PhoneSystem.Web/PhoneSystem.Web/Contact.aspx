<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="PhoneSystem.Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>
    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dvSearch" class="modal hide fade" tabindex="1"
                role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3 id="H3">Contact Search <span id="HeadNextaction"></span>
                    </h3>
                </div>
                <div>
                    <div class="row-fluid">
                        <div class="span3">
                            First Name
                        </div>
                        <div class="span8">
                            <input type="text" autocomplete="off" runat="server" id="txtsearchfirstname" placeholder="Start typing.."
                                class="typeahead" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span3">
                            Last Name
                        </div>
                        <div class="span8">
                            <input type="text" runat="server" id="txtsearchlastname" placeholder="Start typing.."
                                class="typeahead" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span3">
                            Email
                        </div>
                        <div class="span3">
                            <input type="text" runat="server" id="txtsearchemail" placeholder="Start typing.."
                                class="typeahead" />
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span3">
                            Phone
                        </div>
                        <div class="span3">
                            <input type="text" runat="server" id="txtsearchphone" placeholder="Start typing.."
                                class="typeahead" />
                        </div>
                    </div>
                </div>
                <div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="hditemselected" runat="server" />
                    <asp:Button ID="btnSearchSubmit" OnClientClick="javascript:alert('asd');" OnClick="btnSearchSubmit_Click"
                        class="btn" runat="server" Text="Submit" />
                    Search</a>
                <button class="btn" data-dismiss="modal" aria-hidden="true">
                    Close</button>
                </div>
            </div>
            //This is how you will call your modal dialog
 <img alt="search" src="images/search.png" data-toggle="modal" data-target="#dvSearch" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
