<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="TeacherEnrollment.aspx.cs" Inherits="TransportManagement.TeacherEnrollment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/TeacherEnrollment.js"></script>

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

            if (input.files && input.files[0])
            {
                if (input.files[0].size > maxFileSize) {
                    alert("File is too big maximum file size can't be geater than 10 MB");
                    input.value = null;
                    return false;
                }

                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {
                    var image = document.getElementById("TeacherImage");
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

            if ($("[id$=txtTestDate]").val().trim() == "") {
                $("[id$=txtTestDate]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtTestDate]").removeAttr("style");
            }

            if ($("[id$=txtSchoolCode]").val().trim() == "") {
                $("[id$=txtSchoolCode]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtSchoolCode]").removeAttr("style");
            }

            if ($("[id$=txtSchoolName]").val().trim() == "") {
                $("[id$=txtSchoolName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtSchoolName]").removeAttr("style");
            }

            if ($("[id$=txtTeacherName]").val().trim() == "") {
                $("[id$=txtTeacherName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtTeacherName]").removeAttr("style");
            }

            if ($("[id$=txtFatherName]").val().trim() == "") {
                $("[id$=txtFatherName]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtFatherName]").removeAttr("style");
            }

            if ($("[id$=txtAge]").val().trim() == "") {
                $("[id$=txtAge]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=txtAge]").removeAttr("style");
            }

            var ddlGender = document.getElementById("ddlGender");
            if (ddlGender.value == "0") {
                $("[id$=ddlGender]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=ddlGender]").removeAttr("style");
            }

            var ddlInLaw = document.getElementById("ddlInLaw");
            if (ddlInLaw.value == "0") {
                $("[id$=ddlInLaw]").attr("style", "border: red 1px solid;")
                valRes = false;
            }
            else {
                $("[id$=ddlInLaw]").removeAttr("style");
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

<%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/start/jquery-ui.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("#dialog").dialog({
            autoOpen: false,
            modal: true,
            height: 400,
            width: 400,
            title: "Teacher Picture"
        });
        $("[id*=TeacherImage] img").click(function () {
            $('#dialog').html('');
            $('#dialog').append($(this).clone());
            $('#dialog').dialog('open');
        });
    });
</script>--%>
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
                                    <b>Teacher Enrollment</b></h5>
                                <p class="text-muted font-13 m-b-30">
                                    <hr />
                                    <p>
                                    </p>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                Date *</label>                                                
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" Width="125px"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDate" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="SchoolName">
                                                School Name </label>
                                                <asp:TextBox ID="txtSchoolName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="75"></asp:TextBox>               
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSchoolName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="SchoolCode">
                                                School *</label>
                                                <div class="input-group input-group-sm mb-3">
                                                <asp:TextBox ID="txtSchoolCode" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3">                                                
                                                </asp:TextBox>
                                                <span class="input-group-append">                                    
                                                    <asp:LinkButton ID="btnLookupSchool" runat="server" ClientIDMode="Static" OnClick="btnLookupSchool_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
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
                                                <label for="TeacherName">
                                                Teacher Name *</label>
                                                <asp:TextBox ID="txtTeacherName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25" AutoPostBack="True" OnTextChanged="txtTeacherName_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtTeacherName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtTeacherName" ValidChars=" " />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TeacherCode">
                                                Teacher Id </label>
                                                <div class="input-group input-group-sm mb-3">
                                                    <asp:TextBox ID="txtTeacherCode" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="9" Enabled="False"></asp:TextBox>
                                                    <span class="input-group-append">                                    
                                                        <asp:LinkButton ID="btnLookupTeacher" runat="server" ClientIDMode="Static" OnClick="btnLookupTeacher_Click" Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                            <i class="fa fa-search"></i>
                                                        </asp:LinkButton>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TeacherRegNo">
                                                Teacher Manual No. </label>
                                                <asp:TextBox ID="txtTeacherRegNo" AutoComplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="20" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="FatherName">
                                                s/o - d/o - w/o </label>
                                                <asp:DropDownList ID="ddlInLaw" runat="server" CssClass="form-control">                                                
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Gender">
                                                Father / Spouse Name </label>
                                                <asp:TextBox ID="txtFatherName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="25" AutoPostBack="True" OnTextChanged="txtFatherName_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtFatherName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" TargetControlID="txtFatherName" ValidChars=" " />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Age">
                                                Age </label>
                                                <asp:TextBox ID="txtAge" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="2">                                                
                                                </asp:TextBox>
                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtAge_MaskedEditExtender" Mask ="99" runat="server" TargetControlID="txtAge" />--%>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtAge_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAge" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Gender">
                                                Gender </label>
                                                <asp:DropDownList ID="ddlGender"  runat="server" CssClass="form-control">                                                
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="WearGlasses"> </label>
                                                <asp:CheckBox ID="chkWearGlasses" Checked="false" runat="server" Text="Wear Glasses" CssClass="form-control no-border" AutoPostBack="True" OnCheckedChanged="chkWearGlasses_CheckedChanged">                                                
                                                </asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="DecreasedVision"> </label>
                                                <asp:CheckBox ID="chkDecreasedVision" Checked="false" Text="Decreased Vision" runat="server" CssClass="form-control no-border">                                                
                                                </asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="Religion"> Religion </label>
                                                <asp:RadioButtonList ID="rdoReligion" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Muslim</asp:ListItem>
                                                    <asp:ListItem Value="1">Non-Muslim</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                     </div>
                                    <div class="row">
                                        <div class="col-sm-3"  style ="display:none">
                                            <div class="form-group">
                                                <label for="OccularHistory">
                                                Occular History </label>
                                                <asp:CheckBox ID="chkOccularHistory"  Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="chkOccularHistory_CheckedChanged">                                                
                                                </asp:CheckBox>
                                                <asp:TextBox ID="txtOccularHistory" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3"  style ="display:none">
                                            <div class="form-group">
                                                <label for="MedicalHistory">
                                                Medical History </label>
                                                <asp:CheckBox ID="chkMedicalHistory" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="chkMedicalHistory_CheckedChanged">                                                
                                                </asp:CheckBox>
                                                <asp:TextBox ID="txtMedicalHistory" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3"  style ="display:none">
                                            <div class="form-group">
                                                <label for="ChiefComplain">
                                                Chief Complain </label>
                                                <asp:CheckBox ID="chkChiefComplain" Checked="false" runat="server" CssClass="form-control" AutoPostBack="True" OnCheckedChanged="chkChiefComplain_CheckedChanged">                                                
                                                </asp:CheckBox>
                                                <asp:TextBox ID="txtChiefComplain" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label for="TeacherPicture">
                                                Upload Teacher Picture </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkNotRequired" runat="server" Text="Not Comfortable" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:Image ID="TeacherImage" ClientIDMode="Static" runat="server" Height="150px" ImageUrl="~/Captures/TeacherDefaultImage.jpg" meta:resourcekey="EmpImageResource1" Width="120px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:FileUpload ID="btnBrowse" ClientIDMode="Static" runat="server" onchange="ShowPreview(this)" accept="image/*" multiple="false" CssClass="btn btn-default btn-sm" Width="105px" />
                                                <asp:Label ID="lblFileUploadStudent" runat="server"></asp:Label>
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
                                                <asp:LinkButton ID="btnWebCam" runat="server" Text="Capture from Webcam" OnClick="btnWebCam_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:LinkButton ID="btnViewAllPhotos" runat="server" Text="View Selected Teacher Picture(s)" OnClick="btnViewAllPhotos_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                          <div class="col-sm-6">
                                            <label for="TeacherEnrollmentButton">
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
                    </div>
                            </asp:Panel>
                </div>

                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
        <asp:HiddenField ID="hfSchoolIDPKID" runat="server" OnValueChanged="hfSchoolIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfTeacherIDPKID" runat="server" OnValueChanged="hfTeacherIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfLookupResultSchool" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultSchool_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultTeacher" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultTeacher_ValueChanged" runat="server" />

        <asp:HiddenField ID="hfImageBytes" Value="" ClientIDMode="Static" OnValueChanged="hfImageBytes_ValueChanged" runat="server" />

    </ContentTemplate>

    <Triggers>  
        <asp:PostBackTrigger ControlID="btnSave" /> 
        <asp:PostBackTrigger ControlID="btnEdit" /> 
        <asp:PostBackTrigger ControlID="AddButton" />
    </Triggers>   
</asp:UpdatePanel>

</asp:Content>
