<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="HospitalVisitForAfterSurgery.aspx.cs" Inherits="TransportManagement.HospitalVisitForAfterSurgery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/FormsScripts/HospitalVisitForAfterSurgery.js"></script>
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
                            var image = document.getElementById("StudentImage");
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

                    if ($("[id$=txtSchoolName]").val().trim() == "") {
                        $("[id$=txtSchoolName]").attr("style", "border: red 1px solid;")
                        valRes = false;
                    }
                    else {
                        $("[id$=txtSchoolName]").removeAttr("style");
                    }

                    if ($("[id$=txtStudentCode]").val().trim() == "") {
                        $("[id$=txtStudentCode]").attr("style", "border: red 1px solid;")
                        valRes = false;
                    }
                    else {
                        $("[id$=txtStudentCode]").removeAttr("style");
                    }

                    if ($("[id$=txtStudentName]").val().trim() == "") {
                        $("[id$=txtStudentName]").attr("style", "border: red 1px solid;")
                        valRes = false;
                    }
                    else {
                        $("[id$=txtStudentName]").removeAttr("style");
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
    <style type="text/css">
        .auto-style1 {
            width: 85%;
        }
        .auto-style2 {
            width: 100%;
        }
    </style>
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>                                
        <div id="wrapper">
            <div class="content-page">
                <div class="content">
                    <div class="container">         
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-box">
                                    <h5 class="m-t-0 header-title"><b>Hospital visit for after Surgery</b></h5>
                                    <hr />                                                                     
                                    <%--<div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                Date *</label>                                                
                                                <asp:Label ID="lblTestDate" runat="server" ></asp:Label>
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" ></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender_txtTestDate" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>                                                    
                                            </div>
                                        </div>
                                    </div>  --%>                                   

                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="4">
                                                    <asp:ListItem Selected="True" Value="0">New Test</asp:ListItem>
                                                    <asp:ListItem Value="1">Edit Previous Visit Result</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="pnlStudent" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Student Information</div>
                                        <div class="panel-body" runat="server">
                                            <div class="row">
                                                <%--left side--%>
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="SchoolName">School Name *</label>
                                                                <asp:TextBox ID="txtSchoolName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength ="60"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSchoolName_FilteredTextBoxExtender" runat="server" FilterType="Numbers,UppercaseLetters, LowercaseLetters, Custom" ValidChars=" .@" TargetControlID="txtSchoolName" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="SchoolCode">School Code *</label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtSchoolCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" MaxLength ="3" AutoComplete="off" Enabled="False" ></asp:TextBox>
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
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="StudentName">Student Name *</label>
                                                                <asp:TextBox ID="txtStudentName" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="25"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="txtStudentName_FilteredTextBoxExtender" runat="server" FilterType="LowercaseLetters, UppercaseLetters, Custom" ValidChars=" " TargetControlID="txtStudentName" />
                                                            </div>                                                            
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label for="StudentCode">                                                                
                                                                Student Id </label>
                                                                <div class="input-group input-group-sm mb-3">
                                                                    <asp:TextBox ID="txtStudentCode" runat="server" CssClass="form-control border-end-0 border rounded-pill" AutoComplete="off" MaxLength="9">                                                
                                                                    </asp:TextBox>
                                                                    <span class="input-group-append">                                    
                                                                        <asp:LinkButton ID="btnLookupStudent" runat="server" OnClick="btnLookupStudent_Click" ClientIDMode="Static"  Text="Lookup" CssClass="btn btn-outline-secondary border-start-0 border rounded-pill ms-n3">
                                                                            <i class="fa fa-search"></i>
                                                                        </asp:LinkButton>
                                                                    </span>
                                                                    <asp:FilteredTextBoxExtender ID="txtStudentCode_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtStudentCode" />
                                                                </div>
                                                            </div>
                                                        </div>
	                                                </div>
                                                </div>

                                                <%--Right side--%>
                                                <div runat="server" id="pnlStudent_Sub" class="col-xs-12 col-sm-6">
                                                    <table class="auto-style2">
                                                        <tr>
                                                            <td><asp:Label ID="Label7"  runat="server" Text="Father Name" ></asp:Label></td>
                                                            <td><asp:Label ID="lblFatherName_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label8"  runat="server" Text="Age" ></asp:Label></td>
                                                            <td><asp:Label ID="lblAge_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label9"  runat="server" Text="Gender" ></asp:Label></td>
                                                            <td><asp:Label ID="lblGender_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label10"  runat="server" Text="Class" ></asp:Label></td>
                                                            <td><asp:Label ID="lblClass_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label11"  runat="server" Text="School" ></asp:Label></td>
                                                            <td><asp:Label ID="lblSchoolName_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label12"  runat="server" Text="Wearing glasses" ></asp:Label></td>
                                                            <td><asp:Label ID="lblWearingGlasses_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td><asp:Label ID="Label13"  runat="server" Text="Decreased Vision" ></asp:Label></td>
                                                            <td><asp:Label ID="lblDecreasedVision_Student" runat="server" ></asp:Label></td>
                                                        </tr>
                                                    </table>                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="pnl_LastVisitSurgery" class="panel panel-primary">
	                                    <div class="panel-heading" runat="server" >Last visit information from surgery</div>
	                                    <div class="panel-body" runat="server">
                                            <div class="col-xs-12 col-sm-6">
                                                <table class="auto-style2">
                                                    <tr>
                                                        <td><asp:Label ID="Label40"  runat="server" Text="Date of Surgery" ></asp:Label></td>
                                                        <td><asp:Label ID="Label41" runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label38"  runat="server" Text="Name of Hospital" ></asp:Label></td>
                                                        <td><asp:Label ID="Label39" runat="server" ></asp:Label></td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td><asp:Label ID="Label42"  runat="server" Text="Name of Surgery" ></asp:Label></td>
                                                        <td><asp:Label ID="Label43" runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label44"  runat="server" Text="Which Eye: Right or Left" ></asp:Label></td>
                                                        <td><asp:Label ID="Label45" runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label46"  runat="server" Text="Name of Surgeon" ></asp:Label></td>
                                                        <td><asp:Label ID="Label47" runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label48"  runat="server" Text="Comments by Surgeon after surgery" ></asp:Label></td>
                                                        <td><asp:Label ID="Label49" runat="server" ></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label50"  runat="server" Text="Ophthalmologist" ></asp:Label></td>
                                                        <td><asp:Label ID="Label51" runat="server" ></asp:Label></td>
                                                    </tr>

                                                    <tr>
                                                        <td><asp:Label ID="Label52"  runat="server" Text="Orthoptist" ></asp:Label></td>
                                                        <td><asp:Label ID="Label53" runat="server" ></asp:Label></td>
                                                    </tr>

                                                    <tr>
                                                        <td><asp:Label ID="Label54"  runat="server" Text="Surgeon" ></asp:Label></td>
                                                        <td><asp:Label ID="Label55" runat="server" ></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>                                            	
	                                    </div>
                                    </div>

                                    <%--<div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <asp:RadioButtonList ID="rdoOldNewTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdoOldNewTest_SelectedIndexChanged" RepeatDirection="Horizontal" Width="425px" TabIndex="4">
                                                    <asp:ListItem Selected="True" Value="0">New Test</asp:ListItem>
                                                    <asp:ListItem Value="1">Edit Previous Test Result</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <table class="auto-style1">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlTestSummary" style="line-height:3px" runat="server" GroupingText="Visit Summary">
                                                    <table class="auto-style2">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="Label26" runat="server" Text="Visit Date:" ></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSubjectiveRefPrevVisitDate" runat="server"  ></asp:Label>
                                                                     </div>
                                                                </div>

                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblAutoRefResult" runat="server" Text="Autoref Results"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblRight" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblLeft" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAutoRef_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAutoRef_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblVisualAcuity" runat="server" Text="Visual Acuity Distance"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblUnaided" runat="server" Text="Un-aided"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Unaided_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Unaided_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblWithGlasses" runat="server" Text="With Glasses"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_WithGlasses_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_WithGlasses_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                            <asp:Label ID="lblPinHole" runat="server" Text="Pin Hole"></asp:Label>
                                                                    </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_PinHole_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_PinHole_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblVisualAcuityNear" runat="server" Text="Visual Acuity Near"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuityNear_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuityNear_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                 <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Near_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVisualAcuity_Near_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblNeedsCycloRefraction" runat="server" Text="Needs Cyclo Refraction"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                 <div class="form-group">
                                                                    <asp:Label ID="lblNeedsCycloRefraction_Status" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction" runat="server" Text="Subjective Refraction"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblSubjectiveRefraction_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblDistance" runat="server" Text="Visual Acuity - Distance"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDistance_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDistance_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblNearAdd" runat="server" Text="Near add"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                        </tr>  
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblDouchromeTest" runat="server" Text="Douchrome Test"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDouchromeTest_Result" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblObjectiveRefraction" runat="server" Text="Objective Refraction"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                
                                                            </td>
                                                        </tr>  
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy" runat="server" Text="Retinoscopy"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>    
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRetinoscopy_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>   
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCondition" runat="server" Text="Condition"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCondition_Remarks_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCondition_Remarks_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                </div>
                                                                    </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>    
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblFinalPresentation" runat="server" Text="Final Presentation"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblFinalPresentation_Results_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblFinalPresentation_Results_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td> 
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOrthopticAssessment" runat="server" Text="Orthoptic Assessment"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                     <asp:Label ID="lblOrthopticAssessment_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblOrthopticAssessment_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>                                                                
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOrthopticAssessment_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblOrthopticAssessment_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblHirschberg" runat="server" Text="Hirschberg"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblHirschberg_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblHirschberg_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRedGlow" runat="server" Text="Red Glow"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblRedGlow_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblRedGlow_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblPupilReflex" runat="server" Text="Pupil Reflex"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblPupilReflex_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblPupilReflex_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCoverUnCoverTest" runat="server" Text="Cover Un Cover Test"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblCoverUnCoverTest_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblCoverUnCoverTest_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOther" runat="server" Text="Other"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblOtherRemarks" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle" runat="server" Text="Extra Occular Muscle"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_Right" runat="server" Text="Right Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblExtraOccularMuscle_Left" runat="server" Text="Left Eye"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>   
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_Remarks" runat="server" Text="Remarks"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                            <asp:Label ID="lblExtraOccularMuscle_RightEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblExtraOccularMuscle_LeftEye" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblDiag" runat="server" Text="Diagnosis"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblDiagnosis" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblDiagnosisRemarks" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSubDiagnosis" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblNext" runat="server" Text="NextVisit"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblNextVisit" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSurgery" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <asp:Label ID="lblSurgeryDetail" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                        <asp:Label ID="lblSurgeryDetailRemarks" runat="server" Font-Bold="True" Font-Size="9pt"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>

                                    </table>



                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="TransDate">
                                                Visit Date *</label>                                                
                                                <asp:Label ID="Label15" runat="server" ></asp:Label>
                                                <asp:TextBox ID="txtTestDate" runat="server" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" TabIndex="5" ></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTestDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>

                                                <asp:DropDownList ID="ddlPreviousTestResult" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPreviousTestResult_SelectedIndexChanged" AutoPostBack="True" TabIndex="6" > </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div runat="server" id="pnl_AutoRefTestRes" style="display:none" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Autorefraction Test Result</div>
                                        <div class="panel-body" runat="server">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="lblutoRefPrevVisitDate">Current date: </label>
                                                        <asp:Label ID="lblAutoRefCurrentDate" runat="server"  ></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">                                               
                                              <div class="col-xs-12 col-sm-6">
	                                            <table class="auto-style1">
                                                    <tr>
                                                        <td><asp:Label ID="Label28" runat="server"  Text="Right"></asp:Label></td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Spherical"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Cylinderical"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Axix"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSpherical_RightEye" runat="server" ></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCylinderical_RightEye" runat="server" ></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAxix_RightEye" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                              </div>

                                              <div class="col-xs-12 col-sm-6">
	                                            <div id="pnlLeftEye_AutoRef" runat="server">
                                                    <table class="auto-style1">
                                                        <tr>
                                                            <td><asp:Label ID="Label29" runat="server"  Text="Left"></asp:Label></td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="Spherical"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Cylinderical"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Axix"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSpherical_LeftEye" runat="server" ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCylinderical_LeftEye" runat="server" ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAxix_LeftEye" runat="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
	                                            </div>
                                              </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="Div1" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Right Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_RightEye">Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSpherical_RightEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="6" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtSpherical_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="7"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_RightEye_AutoRef" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_RightEye" style="text-align: left">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclinderical_RightEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="8">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtCyclinderical_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="9"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_RightEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_RightEye_AutoRef" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                         
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_RightEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="10" ></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_RightEye_AutoRef" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_RightEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_RightEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixB_RightEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_RightEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_RightEye_AutoRef" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     
                                                                </div>                                                                                                                                                           
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="Div2" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Left Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">                                                                    
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_LeftEye">
                                                                            Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSpherical_LeftEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="11" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtSpherical_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="12" ></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtSpherical_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_LeftEye_AutoRef" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_LeftEye">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclinderical_LeftEye_AutoRef" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="13">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtCyclinderical_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="14"></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtCyclinderical_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_LeftEye_AutoRef" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_LeftEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="15"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_LeftEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixA_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_LeftEye_AutoRef" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_LeftEye_AutoRef" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_LeftEye_MaskedEditExtender" runat="server" Mask="999" TargetControlID="txtAxixB_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtAxixB_LeftEye_AutoRef_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_LeftEye_AutoRef" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>                                                                     
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                     
                                    <div runat="server" id="pnl_VisualAcuity" style="display:none" class="panel panel-primary">
	                                    <div class="panel-heading" runat="server" >Visual Acuity</div>
	                                    <div class="panel-body" runat="server">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6">
	                                                <div id="pnlTest1_RightEye" runat="server">
                                                
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="DistanceVision_RightEye"> Visual Acuity Distance </label>
                                                                
                                                                        <table class="auto-style2">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label30" runat="server"  Text="Right"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label31" runat="server" Text="Un-aided"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label32" runat="server" Text="With Glasses"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label33" runat="server" Text="Pin Hole"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rdoDistanceVision_RightEye_Unaided" runat="server" AutoPostBack="True" Width="125px" TabIndex="16">
                                                                                        <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                        <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                        <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                        <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                        <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                        <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                        <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                        <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                        <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                        <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                        <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                                    </asp:RadioButtonList>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rdoDistanceVision_RightEye_WithGlasses" runat="server" AutoPostBack="True" Width="125px" TabIndex="17">
                                                                                        <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                        <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                        <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                        <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                        <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                        <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                        <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                        <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                        <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                        <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                        <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rdoDistanceVision_RightEye_PinHole" runat="server" AutoPostBack="True" Width="125px" TabIndex="18">
                                                                                        <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                        <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                        <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                        <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                        <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                        <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                        <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                        <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                        <asp:ListItem Value="12" Text="" Enabled="False">HM</asp:ListItem>
                                                                                        <asp:ListItem Value="13" Text="" Enabled="False">PL</asp:ListItem>
                                                                                        <asp:ListItem Value="14" Text="" Enabled="False">NPL</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">                                                                    		
                                                                        <label for="VisualNearVision_RightEye"> Visual Acuity Near </label>
                                                                 
                                                                            <asp:RadioButtonList ID="rdoNearVision_RightEye" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" TabIndex="19" >
                                                                                <asp:ListItem Selected="True" Value="0">N6</asp:ListItem>
                                                                                <asp:ListItem Value="1">N8</asp:ListItem>
                                                                                <asp:ListItem Value="2">N10</asp:ListItem>
                                                                                <asp:ListItem Value="3">N12</asp:ListItem>
                                                                                <asp:ListItem Value="4">N14</asp:ListItem>
                                                                                <asp:ListItem Value="5">N18</asp:ListItem>
                                                                                <asp:ListItem Value="6">Less then N18</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:CheckBox ID="chkNeedsCycloRefraction_RightEye" Text="Needs Cyclo Refraction" runat="server" CssClass="form-control no-border" TabIndex="20" Visible="False">                                                
                                                                        </asp:CheckBox>                                                                    
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtNeedsCycloRefraction_RightEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" TabIndex="21"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>                                                 
	                                                </div>
                                                </div>

                                                <div class="col-xs-12 col-sm-6">
	                                                <div id="pnlTest1_LeftEye" runat="server">
                                                
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="DistanceVision_LeftEye"> Visual Acuity Distance </label>
                                                                 
                                                                        <table class="auto-style2">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label34" runat="server"  Text="Left"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label35" runat="server" Text="Un-aided"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label36" runat="server" Text="With Glasses"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label37" runat="server" Text="Pin Hole"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rdoDistanceVision_LeftEye_Unaided" runat="server" AutoPostBack="True" Width="125px" TabIndex="22">
                                                                                        <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                        <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                        <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                        <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                        <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                        <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                        <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                        <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                        <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                        <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                        <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                                    </asp:RadioButtonList>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rdoDistanceVision_LeftEye_WithGlasses" runat="server" AutoPostBack="True" Width="125px" TabIndex="23">
                                                                                        <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                        <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                        <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                        <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                        <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                        <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                        <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                        <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                        <asp:ListItem Value="12" Text="">HM</asp:ListItem>
                                                                                        <asp:ListItem Value="13" Text="">PL</asp:ListItem>
                                                                                        <asp:ListItem Value="14" Text="">NPL</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rdoDistanceVision_LeftEye_Pinhole" runat="server" AutoPostBack="True" Width="125px" TabIndex="24">
                                                                                        <asp:ListItem Value="0" Text="">6/6</asp:ListItem>
                                                                                        <asp:ListItem Value="1" Text="">6/9</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Text="">6/12</asp:ListItem>
                                                                                        <asp:ListItem Value="3" Text="">6/18</asp:ListItem>
                                                                                        <asp:ListItem Value="4" Text="">6/24</asp:ListItem>
                                                                                        <asp:ListItem Value="5" Text="">6/36</asp:ListItem>
                                                                                        <asp:ListItem Value="6" Text="">6/60</asp:ListItem>
                                                                                        <asp:ListItem Value="7" Text="">5/60</asp:ListItem>
                                                                                        <asp:ListItem Value="8" Text="">4/60</asp:ListItem>
                                                                                        <asp:ListItem Value="9" Text="">3/60</asp:ListItem>
                                                                                        <asp:ListItem Value="10" Text="">2/60</asp:ListItem>
                                                                                        <asp:ListItem Value="11" Text="">1/60</asp:ListItem>
                                                                                        <asp:ListItem Value="12" Text="" Enabled="False">HM</asp:ListItem>
                                                                                        <asp:ListItem Value="13" Text="" Enabled="False">PL</asp:ListItem>
                                                                                        <asp:ListItem Value="14" Text="" Enabled="False">NPL</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <label for="VisualNearVision_LeftEye"> Visual Acuity Near </label>
                                                                        <%--<label for="NearVision_LeftEye"> Near Vision </label>--%>
                                                                            <asp:RadioButtonList ID="rdoNearVision_LeftEye" runat="server" RepeatDirection="Vertical" Width="425px" AutoPostBack="True" TabIndex="25" >
                                                                                <asp:ListItem Selected="True" Value="0">N6</asp:ListItem>
                                                                                <asp:ListItem Value="1">N8</asp:ListItem>
                                                                                <asp:ListItem Value="2">N10</asp:ListItem>
                                                                                <asp:ListItem Value="3">N12</asp:ListItem>
                                                                                <asp:ListItem Value="4">N14</asp:ListItem>
                                                                                <asp:ListItem Value="5">N18</asp:ListItem>
                                                                                <asp:ListItem Value="6">Less then N18</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:CheckBox ID="chkNeedsCycloRefraction_LeftEye" Text="Needs Cyclo Refraction" Checked="false" runat="server" CssClass="form-control no-border" AutoPostBack="True" Visible="False" TabIndex="26">                                                
                                                                        </asp:CheckBox>                                                                    
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtNeedsCycloRefraction_LeftEye" runat="server" AutoComplete="off" CssClass="form-control form-control-sm" MaxLength="200" Visible="False" TabIndex="27"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                   
                                                    </div>
                                                </div>
                                            </div>
	                                    </div>
                                    </div>
                                                                         
                                    <div runat="server" id="pnl_SubjectiveRefTestRes" style="display:none" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Subjective Refraction Results</div>
                                        <div class="panel-body" runat="server">                                                                                         
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="Div3" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Right Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Test2a">2a Test: Best Corrected Visual Acuity</label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_RightEye">Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSphericalRightEyeSR" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="28" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtSpherical_RightEyeSR" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="29"></asp:TextBox>                                                                                    
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_RightEyeSR" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_RightEye" style="text-align: left">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclindericalRightEyeSR" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="30">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtCyclinderical_RightEyeSR" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="31"></asp:TextBox>                                                                                    
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_RightEyeSR" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                         
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_RightEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_RightEyeSR" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="32" ></asp:TextBox>                                                                                    
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_RightEyeSR" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_RightEyeSR" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>                                                                                    
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_RightEyeSR" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Near_RightEye" style="text-align: left">
                                                                            Near Add *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlNear_RightEyeSR" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="38" Enabled="False">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtNear_RightEyeSR" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="39" ></asp:TextBox>                                                                                    
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtNear_RightEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtNear_RightEyeSR" ValidChars="." />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>                                                                                                                                                           
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xs-12 col-sm-6">
	                                                <div runat="server" id="Div4" class="panel panel-default">
                                                        <div class="panel-heading" runat="server" >Left Eye</div>
                                                        <div class="panel-body" runat="server">
                                                            <div class="row">
                                                                <div class="container">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <div class="form-group"><label for="Test2a"> </label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Spherical_LeftEye">Spherical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlSphericalLeftEyeSR" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="33" >
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                        <asp:ListItem>Plano</asp:ListItem>
                                                                                        <asp:ListItem>Error</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtSpherical_LeftEyeSR" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="34" ></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtSpherical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtSpherical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" TargetControlID="txtSpherical_LeftEyeSR" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Cyclinderical_LeftEye">
                                                                            Cyclinderical *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlCyclindericalLeftEyeSR" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="35">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                        <asp:ListItem>Negative</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtCyclinderical_LeftEyeSR" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="36"></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtCyclinderical_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtCyclinderical_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom" TargetControlID="txtCyclinderical_LeftEyeSR" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Axix_LeftEye">
                                                                            Axix *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:TextBox ID="txtAxixA_LeftEyeSR" runat="server" AutoComplete="off" CssClass="form-control border-end-0 border rounded-pill" MaxLength="3" Width="120px" TabIndex="37"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixA_LeftEye_MaskedEditExtender" runat="server" Mask="999" MaskType="Number" TargetControlID="txtAxixA_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers" TargetControlID="txtAxixA_LeftEyeSR" />
                                                                                    <span class="input-group-append">
                                                                                    <asp:TextBox ID="txtAxixB_LeftEyeSR" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="3" Visible="False" Width="120px"></asp:TextBox>
                                                                                    <%--<ajaxToolkit:MaskedEditExtender ID="txtAxixB_LeftEye_MaskedEditExtender" runat="server" Mask="999" TargetControlID="txtAxixB_LeftEye" />--%>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers" TargetControlID="txtAxixB_LeftEyeSR" />
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group">
                                                                            <label for="Near_LeftEye">
                                                                            Near Add *</label>
                                                                            <div class="input-group sm-6">
                                                                                <div class="input-group-prepend">
                                                                                    <asp:DropDownList ID="ddlNear_LeftEye" runat="server" CssClass="form-control border-end-0 border rounded-pill" Width="120px" TabIndex="40" Enabled="False">
                                                                                        <asp:ListItem Selected="True">Positive</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <span class="input-group-append">
                                                                                <asp:TextBox ID="txtNear_LeftEye" runat="server" AutoComplete="off" CssClass="form-control btn btn-outline-secondary border-start-0 border rounded-pill ms-n3" MaxLength="5" Width="120px" TabIndex="41" ></asp:TextBox>
                                                                                <%--<ajaxToolkit:MaskedEditExtender ID="txtNear_LeftEye_MaskedEditExtender" runat="server" Mask="99.99" MaskType="Number" TargetControlID="txtNear_LeftEye" />--%>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtNear_LeftEye_FilteredTextBoxExtender" runat="server" FilterType="Numbers, Custom" TargetControlID="txtNear_LeftEye" ValidChars="." />
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     
                                    <div runat="server" id="pnl_PostOPTCondition" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Post OPT Condition</div>
                                        <div class="panel-body" runat="server">
                                             <div class="row">                                                          
                                                 <div class="col col-lg-4">                                                            
                                                    <asp:RadioButtonList ID="rdoPostOPTCondition" runat="server" TabIndex="16">
                                                        <asp:ListItem Value="0" Text="">Healing</asp:ListItem>
                                                        <asp:ListItem Value="1" Text="">Stable</asp:ListItem>
                                                        <asp:ListItem Value="2" Text="">Good</asp:ListItem>
                                                        <asp:ListItem Value="3" Text="">Worsening</asp:ListItem>                                                        
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="pnl_SquintPostOptCond" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Squint Post OPT Condition</div>
                                        <div class="panel-body" runat="server">
                                             <div class="row">                                                          
                                                 <div class="col col-lg-4">                                                            
                                                    <asp:RadioButtonList ID="rdoSquintPostOptCond" runat="server" TabIndex="32" Width="345px">
                                                        <asp:ListItem Value="0" Text="">Corrected</asp:ListItem>
                                                        <asp:ListItem Value="1" Text="">Over Corrected</asp:ListItem>
                                                        <asp:ListItem Value="2" Text="">Under Corrected</asp:ListItem>
                                                        <asp:ListItem Value="3" Text="">Residual</asp:ListItem>
                                                        <asp:ListItem Value="4" Text="">Consecutive</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="txtSquintDiagOthers">Medicines</label>
                                                        <asp:TextBox ID="txtMedicines" runat="server" CssClass="form-control form-control-sm" MaxLength="120" TabIndex="222"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <label for="txtSquintDiagOthers">Surgeon Remarks</label>
                                                        <asp:TextBox ID="txtSurgeonRemarks" runat="server" CssClass="form-control form-control-sm" MaxLength="120" TabIndex="222"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="pnlFollowupVisit" class="panel panel-primary">
                                        <div class="panel-heading" runat="server" >Follow up visits</div>
                                        <div class="panel-body" runat="server">
                                                <div class="row">                                                          
                                                <div class="col col-lg-3">                                                   
                                                    <asp:RadioButton  ID="rdoNotRequiredFollowup" GroupName="FollowupVisit" Text="Not required" Checked="false" runat="server" CssClass="form-control no-border" OnCheckedChanged="rdoNotRequiredFollowup_CheckedChanged" AutoPostBack="True" />
                                                </div>
                                                </div>
                                                <div class="row">  
                                                    <div class="col col-lg-3">
                                                    <asp:RadioButton  ID="rdoFollowupAfterSixMonths" GroupName="FollowupVisit" Text="Next Visit Date" Checked="false" runat="server" CssClass="form-control no-border" OnCheckedChanged="rdoFollowupAfterSixMonths_CheckedChanged" AutoPostBack="True" />                                                    
                                                </div>
                                                <div class="col col-lg-3">
                                                    <div class="form-group" id="divtxtRoutineCheckupDate" >
                                                        <asp:TextBox ID="txtNextVisitDate" runat="server" TabIndex="228" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" ></asp:TextBox>                                               
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNextVisitDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>                                                    
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div> 
                                    <%--<div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="TransDate">Next Visit Date</label>                                                
                                                <asp:Label ID="Label14" runat="server" ></asp:Label>
                                                <asp:TextBox ID="txtNextVisitDate" runat="server" TabIndex="228" CssClass="form-control form-control-sm" MaxLength="11" AutoComplete="off" Width="125px" ></asp:TextBox>                                               
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNextVisitDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>                                                    
                                            </div>
                                        </div>
                                    </div> --%>                                     
									<div class="row">
                                        <div class="col-sm-6">
                                            <label for="OptometristButton">
                                            </label>
                                            <div class="form-group text-left">
                                                <asp:LinkButton ID="btnEdit" runat="server" Visible="false"   ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Update" OnClick="btnEdit_Click" TabIndex="52"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Save" OnClick="btnSave_Click" TabIndex="53"></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" Visible="false" OnClientClick="return confirm('Are you sure you want to Delete this record?');"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Delete" OnClick="btnDelete_Click" TabIndex="54"></asp:LinkButton>
                                                <asp:LinkButton ID="btnAbort" runat="server"  ClientIDMode="Static" CssClass="btn btn-default btn-sm" Text="Abort" OnClick="btnAbort_Click" TabIndex="55"></asp:LinkButton>
                                                <asp:LinkButton ID="btnRefresh" runat="server"   CssClass="btn btn-default btn-sm"  Text="Refresh" OnClick="btnRefresh_Click" TabIndex="56"></asp:LinkButton>
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
                            </div>
                        </div>
                            
                    </div>
                </div>
            </div>
        </div>
    <asp:HiddenField ID="hfCompanyId" runat="server" />
        <asp:HiddenField ID="hfLoginUserId" runat="server" />
        <asp:HiddenField ID="hfAutoRefTestIDPKID" runat="server" OnValueChanged="hfAutoRefTestIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfStudentIDPKID" runat="server" OnValueChanged="hfStudentIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfSchoolIDPKID" runat="server" OnValueChanged="hfSchoolIDPKID_ValueChanged" Value="0" />
        <asp:HiddenField ID="hfAutoRefTestTransID" runat="server" />
        <asp:HiddenField ID="hfAutoRefTestTransDate" runat="server"/>
        <asp:HiddenField ID="hfLookupResultSchool" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultSchool_ValueChanged" runat="server" />
        <asp:HiddenField ID="hfLookupResultStudent" Value="0" ClientIDMode="Static" OnValueChanged="hfLookupResultStudent_ValueChanged" runat="server" />
    </ContentTemplate>
    <Triggers>

        <asp:PostBackTrigger ControlID="rdoOldNewTest" />

        <asp:PostBackTrigger ControlID="btnEdit" />
        <asp:PostBackTrigger ControlID="btnSave" />
        <asp:PostBackTrigger ControlID="btnDelete" />
        <asp:PostBackTrigger ControlID="btnAbort" />
    </Triggers>    
</asp:UpdatePanel>
</asp:Content>
