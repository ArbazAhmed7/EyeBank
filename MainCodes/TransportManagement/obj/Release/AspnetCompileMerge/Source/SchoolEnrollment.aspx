<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="SchoolEnrollment.aspx.cs" Inherits="TransportManagement.SchoolEnrollment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/SchoolEnrollment.js"></script>
    
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

        function ShowPreview(input) {
            var maxFileSize = 10485760; //// 10MB -> 10 * 1024 * 1024

            if (input.files && input.files[0]) {
                if (input.files[0].size > maxFileSize) {
                    alert("File is too big maximum file size can't be geater than 10 MB");
                    input.value = null;
                    return false;
                }
                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {
                    var image = document.getElementById("SchoolImage");
                    image.src = e.target.result;
                    $("[id$=hfImageBytes]").val(e.target.result);
                    __doPostBack('UpdatePanel1', '');
                    return false;
                }
                ImageDir.readAsDataURL(input.files[0]);


            }
        }

        //function pageLoad() {
        //    ImageUploadlabelUpdate();
        //}

        //function ImageUploadlabelUpdate() {
        //    if ($("[id$=hfImageBytes]").val()) {
        //        $('[id$=lblFileUploadStudent]').text('File chosen.');
        //    }
        //    else {
        //        $('[id$=lblFileUploadStudent]').text('Choose file.');
        //    }
        //}

        function validateInput() {

            var valRes = true;

            if ($("[id$=txtSchoolName]").val().trim() == "") {
                $("[id$=txtSchoolName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtSchoolName]").removeAttr("style");
            }

            if ($("[id$=txtAddress1]").val().trim() == "") {
                $("[id$=txtAddress1]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtAddress1]").removeAttr("style");
            }

            if ($("[id$=txtDistrict]").val().trim() == "") {
                $("[id$=txtDistrict]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtDistrict]").removeAttr("style");
            }

            if ($("[id$=txtRegisteredStudent]").val().trim() == "") {
                $("[id$=txtRegisteredStudent]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtRegisteredStudent]").removeAttr("style");
            }

            if ($("[id$=txtRegisteredTeacher]").val().trim() == "") {
                $("[id$=txtRegisteredTeacher]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtRegisteredTeacher]").removeAttr("style");
            }

            if ($("[id$=txtPrincipalName]").val().trim() == "") {
                $("[id$=txtPrincipalName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtPrincipalName]").removeAttr("style");
            }


            if ($("[id$=ddlGender]").value == "0") {
                $("[id$=ddlGender]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=ddlGender]").removeAttr("style");
            }

            if ($("[id$=ddlTitle]").value == "0") {
                $("[id$=ddlTitle]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=ddlTitle]").removeAttr("style");
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
                                    <b>School Enrollment</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">Institution Type: </label>  
                                                <asp:RadioButtonList ID="rdoInstitutionType" runat="server" RepeatDirection="Horizontal" Width="600px">
                                                    <asp:ListItem Value="0" Selected="True">School</asp:ListItem>
                                                    <asp:ListItem Value="1">College</asp:ListItem>
                                                    <asp:ListItem Value="2">University</asp:ListItem>
                                                    <asp:ListItem Value="3">Madrasa</asp:ListItem>
                                                </asp:RadioButtonList>                                              
                                            </div>
                                        </div>  
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">
                                                Enrollment Date </label>                                                
                                                <asp:TextBox ID="txtEnrollmentDate" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtEnrollmentDate_CalendarExtender" runat="server" TargetControlID="txtEnrollmentDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>  
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="SchoolName">
                                                School Name *</label>
                                                <asp:TextBox ID="txtSchoolName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="75" AutoPostBack="True" OnTextChanged="txtSchoolName_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSchoolName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="SchoolCode">
                                                School Code *</label>
                                                <div class="input-group input-group-sm mb-3">
                                                    <asp:TextBox ID="txtSchoolCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" MaxLength ="3" AutoComplete="off" Enabled="False" ></asp:TextBox>
                                                    <span class="input-group-append">                                    
                                                        <asp:LinkButton ID="btnLookup" runat="server" OnClick="btnLookup_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                            <i class="fa fa-search"></i>
                                                        </asp:LinkButton>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Address1">
                                                Address 1 </label>
                                                <asp:TextBox ID="txtAddress1" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="40" AutoPostBack="True" OnTextChanged="txtAddress1_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                Address 2 <label for="Address2">
                                                 </label>
                                                <asp:TextBox ID="txtAddress2" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="40" AutoPostBack="True" OnTextChanged="txtAddress2_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="District">
                                                Disctrict </label>
                                                <asp:TextBox ID="txtDistrict" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="20" AutoPostBack="True" OnTextChanged="txtDistrict_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtDistrict_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtDistrict" ValidChars=" " />

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Town">
                                                Town </label>
                                                <asp:TextBox ID="txtTown" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="20" AutoPostBack="True" OnTextChanged="txtTown_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtTown" ValidChars=" " />

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="City">
                                                City </label>
                                                <asp:TextBox ID="txtCity" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="20" AutoPostBack="True" OnTextChanged="txtCity_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtCity_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtCity" ValidChars=" " />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TelephoneLandline">
                                                Telephone (Landline) </label>
                                                <asp:TextBox ID="txtTelephoneLandline" runat="server" CssClass="form-control form-control-sm" MaxLength ="12">                                                
                                                </asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="txtTelephoneLandline_MaskedEditExtender" Mask ="999-99999999" runat="server" TargetControlID="txtTelephoneLandline" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TelephoneCell">
                                                Telephone (Cell) </label>
                                                <asp:TextBox ID="txtTelephoneCell" runat="server" CssClass="form-control form-control-sm" MaxLength ="12">                                                
                                                </asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="txtTelephoneCell_MaskedEditExtender" Mask ="9999-9999999" runat="server" TargetControlID="txtTelephoneCell" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="SchoolLevel">
                                                School Level </label>
                                                <div class="input-group input-group-sm mb-3">
                                                    <asp:CheckBox ID="chkPrimary" Checked="false" runat="server" CssClass="form-control no-border" Text=" Primary">                                                
                                                    </asp:CheckBox>
                                                    <asp:CheckBox ID="chkSecondary" Checked="false" runat="server" CssClass="form-control no-border" Text=" Secondary">                                                
                                                    </asp:CheckBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Gender">
                                                For Gender </label>
                                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">                                                
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="RegisteredStudent">
                                                Registered Student </label>
                                                <asp:TextBox ID="txtRegisteredStudent" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength ="4">                                                
                                                </asp:TextBox>
                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtRegisteredStudent_MaskedEditExtender" Mask ="9999" runat="server" TargetControlID="txtRegisteredStudent" />--%>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtRegisteredStudent_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtRegisteredStudent" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="RegisteredTeacher">
                                                Registered Teacher </label>
                                                <asp:TextBox ID="txtRegisteredTeacher" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength ="3">                                                
                                                </asp:TextBox>
                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtRegisteredTeacher_MaskedEditExtender" Mask ="999" runat="server" TargetControlID="txtRegisteredTeacher" />--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtRegisteredTeacher_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtRegisteredTeacher" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Gender">
                                                Title </label>
                                                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control"> </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="NameofPrincipal">
                                                    Name of Principal </label>
                                                    <asp:TextBox ID="txtPrincipalName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="25" AutoPostBack="True" OnTextChanged="txtPrincipalName_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtPrincipalName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtPrincipalName" ValidChars=" " />

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="PrincipalMobile">
                                                Mobile No. </label>
                                                <asp:TextBox ID="txtPrincipalMobile" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="12"> </asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="txtPrincipalMobile_MaskedEditExtender" Mask ="9999-9999999" runat="server" TargetControlID="txtPrincipalMobile" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="SchoolPicture">
                                                Upload School Picture </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkNotRequired" runat="server" Text="Later" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:Image ID="SchoolImage" ClientIDMode="Static" runat="server" Height="150px" ImageUrl="~/Captures/SchoolDefaultImage.jpg" meta:resourcekey="EmpImageResource1" Width="120px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:FileUpload ID="btnBrowse" ClientIDMode="Static" runat="server" onchange="ShowPreview(this)" accept="image/*" multiple="false" CssClass="btn btn-default btn-sm" Width="98px" />
                                                &nbsp;<asp:Button ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Load Image" CssClass="btn btn-default btn-sm" Visible="False" />                                                
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureDate">
                                                Capture Date </label>                                                
                                                <asp:TextBox ID="txtCaptureDate" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="11" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtCaptureDate" runat="server" TargetControlID="txtCaptureDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>                                   
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="CaptureRemarks">
                                                Capture Remarks </label>
                                                <asp:TextBox ID="txtCaptureRemarks" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="120" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtCaptureRemarks" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtCaptureRemarks" ValidChars=" " />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnSaveImage" runat="server" OnClick="btnSaveImage_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Insert Image"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnViewAllPhotos" runat="server" Text="View Selected School Picture(s)" OnClick="btnViewAllPhotos_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                          <div class="col-sm-6">
                                            <label for="SchoolEnrollmentButton">
                                            &nbsp;</label>
                                            <div class="form-group text-left">
                                                <asp:LinkButton ID="btnEdit" runat="server" OnClientClick="return validateInput()" OnClick="btnEdit_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return validateInput()" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save"></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" OnClientClick="return confirm('Are you sure you want to Delete this record?');" Text="Delete"></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server" OnClick="btnAbort_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort"></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Refresh"></asp:LinkButton>
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
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>

                            
                            </div>
                        </div>

                            </asp:Panel>
                    </div>
                            
                </div>

                </div>
        </div>
 
        <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
        <asp:HiddenField ID="hfSchoolIDPKID" runat="server" OnValueChanged="hfSchoolIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfLookupResult" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResult_ValueChanged" runat="server" />

        <asp:HiddenField ID="hfImageBytes" Value="" ClientIDMode="Static" OnValueChanged="hfImageBytes_ValueChanged" runat="server" />
    </ContentTemplate>

</asp:UpdatePanel>

</asp:Content>
