<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ClassSection.aspx.cs" Inherits="TransportManagement.ClassSection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/ClassSection.js"></script>
    
    <script type="text/javascript">
        function ShowPopupAfterSaveConfirmation(title, body) {
            $("#PopupAfterSaveConfirmation .modal-title").html(title);
            $("#PopupAfterSaveConfirmation .modal-body").html(body);
            $("#PopupAfterSaveConfirmation").modal("show");
        }

        function HideBootstrapModal() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove(); $('#Div3').hide();
        }

        function pageLoad() {

        }

        function validateInput() {

            var valRes = true;

            if ($("[id$=txtClassNo]").val().trim() == "") {
                $("[id$=txtClassNo]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtClassNo]").removeAttr("style");
            }

            if (!valRes) {
                $("[id$=lbl_error]").text('* Mandatory');
            }
            else {
                $("[id$=lbl_error]").text('');
            }

            return valRes;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="wrapper">
            <div class="content-page">
                <div class="content">
                    <div class="container">
                        <asp:Panel ID="pnlCom" runat="server" DefaultButton="btnSave">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card-box">
                                <h5 class="m-t-0 header-title">
                                    <b>Addition of Class</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="ClassNo">
                                                Class No. *</label>
                                                <div class="input-group input-group-sm mb-3">
                                                <asp:TextBox ID="txtClassNo" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="4"> </asp:TextBox>
                                                <span class="input-group-append">                                    
                                                    <asp:LinkButton ID="btnLookup" runat="server" ClientIDMode="Static" OnClick="btnLookup_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                        <i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="ClassSection"></label>
                                                <asp:TextBox ID="txtClassSection" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="2" Visible="False"></asp:TextBox>
                                            </div>
                                        </div>
                                        </div>
                                    <div class="row">
                                            <div class="col-sm-6">
                                                <label for="ClassSectionButton">
                                                &nbsp;</label>
                                                <div class="form-group text-left">
                                                    <asp:LinkButton ID="btnEdit" runat="server" OnClientClick="return validateInput()" OnClick="btnEdit_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return validateInput()" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort"></asp:LinkButton>
                                                    <asp:LinkButton ID="btnRefresh" runat="server" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClick="btnRefresh_Click" Text="Refresh"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>                                    
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <p class="text-center text-danger">
                                                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                    <!-- Modal Popup -->
                                    <div id="PopupAfterSaveConfirmation" class="modal fade" role="dialog">
                                        <div class="modal-dialog">
                                            <!-- Modal content-->
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal">
                                                        &times;</button>
                                                    <h4 class="modal-title">
                                                    </h4>
                                                </div>
                                                <div class="modal-body">
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button ID="btnConfirmYes" runat="server" OnClick="btnConfirmYes_Click"  Text="Yes" />
                                                    <asp:Button ID="btnConfirmNo" runat="server" OnClick="btnConfirmNo_Click" Text="No" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Modal Popup -->
                                    </div>
                                    
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>

                            
                            </div>
                        </div>
                    </div>
                            </asp:Panel>
                </div>

                </div>
            </div>
</div>
        <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
        <asp:HiddenField ID="hfClassIDPKID" runat="server" OnValueChanged="hfClassIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfLookupResult" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResult_ValueChanged" runat="server" />
    </ContentTemplate>

</asp:UpdatePanel>

</asp:Content>
